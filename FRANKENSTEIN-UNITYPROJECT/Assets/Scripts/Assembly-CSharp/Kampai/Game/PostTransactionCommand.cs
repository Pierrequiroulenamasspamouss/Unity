namespace Kampai.Game
{
	public class PostTransactionCommand : global::strange.extensions.command.impl.Command
	{
		[Inject(global::Kampai.UI.View.UIElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable uiContext { get; set; }

		[Inject(global::Kampai.Game.GameElement.NAMED_CHARACTER_MANAGER)]
		public global::UnityEngine.GameObject namedCharacterManager { get; set; }

		[Inject]
		public global::Kampai.Game.Transaction.TransactionUpdateData update { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.ILandExpansionService landExpansionService { get; set; }

		[Inject]
		public global::Kampai.UI.View.SpawnDooberSignal tweenSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.Common.IRandomService randomService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.UnlockCharacterModel unlockCharacterModel { get; set; }

		public override void Execute()
		{
			if (update.Target != global::Kampai.Game.TransactionTarget.NO_VISUAL && update.Target != global::Kampai.Game.TransactionTarget.REWARD_BUILDING)
			{
				RunScreenTween();
			}
			sendTelemetry();
			questService.UpdateDeliveryTask();
			questService.UpdateHarvestTask();
			CreateNewMinions();
		}

		private void RunScreenTween()
		{
			if (update.InstanceId == 0 && update.Outputs == null)
			{
				return;
			}
			global::UnityEngine.Vector3 startLocation = GetStartLocation(update);
			bool type = !update.fromGlass;
			global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> outputs = update.Outputs;
			foreach (global::Kampai.Util.QuantityItem item in outputs)
			{
				if (item.ID == 0)
				{
					tweenSignal.Dispatch(startLocation, global::Kampai.UI.View.DestinationType.GRIND, -1, type);
				}
				if (item.ID == 2)
				{
					tweenSignal.Dispatch(startLocation, global::Kampai.UI.View.DestinationType.XP, -1, type);
				}
				if (item.ID == 1)
				{
					tweenSignal.Dispatch(startLocation, global::Kampai.UI.View.DestinationType.PREMIUM, -1, type);
				}
			}
		}

		private global::UnityEngine.Vector3 GetStartLocation(global::Kampai.Game.Transaction.TransactionUpdateData update)
		{
			global::UnityEngine.Vector3 result = global::UnityEngine.Vector3.zero;
			int instanceId = update.InstanceId;
			global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(instanceId);
			if (byInstanceId != null)
			{
				global::Kampai.Game.Location location = byInstanceId.Location;
				if (location != null)
				{
					result = new global::UnityEngine.Vector3(location.x, 0f, location.y);
				}
			}
			else
			{
				if (instanceId != 301)
				{
					return update.startPosition;
				}
				global::Kampai.Game.View.NamedCharacterManagerView component = namedCharacterManager.GetComponent<global::Kampai.Game.View.NamedCharacterManagerView>();
				global::Kampai.Game.View.CharacterObject characterObject = component.Get(instanceId);
				result = new global::UnityEngine.Vector3(characterObject.transform.position.x, 0f, characterObject.transform.position.z);
			}
			return result;
		}

		private void sendTelemetry()
		{
			string text = update.Source;
			int instanceId = update.InstanceId;
			if (instanceId != 0 && string.IsNullOrEmpty(text))
			{
				global::Kampai.Game.Instance byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Instance>(instanceId);
				if (byInstanceId != null)
				{
					text = byInstanceId.Definition.LocalizedKey;
				}
			}
			else if (text == null && update.TransactionId != 0)
			{
				global::Kampai.Game.Transaction.TransactionDefinition definition = null;
				if (definitionService.TryGet<global::Kampai.Game.Transaction.TransactionDefinition>(update.TransactionId, out definition) && !string.IsNullOrEmpty(definition.LocalizedKey))
				{
					text = definition.LocalizedKey;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "unknown";
			}
			if (update.Inputs != null)
			{
				sendSpentTelemetry(text);
			}
			if (update.Outputs != null)
			{
				sendEarnedTelemetry(text);
			}
		}

		private void sendSpentTelemetry(string sourceName)
		{
			string highLevel = string.Empty;
			string specific = string.Empty;
			string type = string.Empty;
			string other = string.Empty;
			determineTaxonomy(update, out highLevel, out specific, out type, out other);
			foreach (global::Kampai.Util.QuantityItem input in update.Inputs)
			{
				if (input.ID == 0)
				{
					uint quantity = input.Quantity;
					telemetryService.Send_Telemetry_EVT_IGE_FREE_CREDITS_PURCHASE_REVENUE((int)quantity, sourceName, global::Kampai.Game.PurchaseAwarePlayerService.PurchasedCurrencyUsed(logger, playerService, false, quantity), highLevel, specific, type, other);
					continue;
				}
				if (input.ID == 1)
				{
					uint quantity2 = input.Quantity;
					telemetryService.Send_Telemetry_EVT_IGE_PAID_CREDITS_PURCHASE_REVENUE((int)quantity2, sourceName, global::Kampai.Game.PurchaseAwarePlayerService.PurchasedCurrencyUsed(logger, playerService, true, quantity2), highLevel, specific, type, other);
					continue;
				}
				global::Kampai.Game.IngredientsItemDefinition definition = null;
				if (definitionService.TryGet<global::Kampai.Game.IngredientsItemDefinition>(input.ID, out definition))
				{
					SendCraftableEarnedSpentTelemetry(false, sourceName, (int)input.Quantity, definition);
				}
			}
		}

		private void determineTaxonomy(global::Kampai.Game.Transaction.TransactionUpdateData update, out string highLevel, out string specific, out string type, out string other)
		{
			highLevel = string.Empty;
			specific = string.Empty;
			type = string.Empty;
			other = string.Empty;
			if (update.Outputs != null)
			{
				foreach (global::Kampai.Util.QuantityItem output in update.Outputs)
				{
					if (output.ID != 0 && output.ID <= 8)
					{
						continue;
					}
					global::Kampai.Game.TaxonomyDefinition definition = null;
					int iD = output.ID;
					if (update.Target == global::Kampai.Game.TransactionTarget.LAND_EXPANSION || update.Target == global::Kampai.Game.TransactionTarget.REPAIR_BRIDGE)
					{
						global::Kampai.Game.LandExpansionConfig definition2 = null;
						if (definitionService.TryGet<global::Kampai.Game.LandExpansionConfig>(output.ID, out definition2))
						{
							global::System.Collections.Generic.List<global::Kampai.Game.LandExpansionBuilding> list = landExpansionService.GetBuildingsByExpansionID(definition2.expansionId) as global::System.Collections.Generic.List<global::Kampai.Game.LandExpansionBuilding>;
							if (list != null && list.Count > 0)
							{
								iD = list[0].Definition.ID;
							}
						}
					}
					if (definitionService.TryGet<global::Kampai.Game.TaxonomyDefinition>(iD, out definition))
					{
						highLevel = definition.TaxonomyHighLevel;
						specific = definition.TaxonomySpecific;
						type = definition.TaxonomyType;
						other = definition.TaxonomyOther;
					}
				}
			}
			if (update.InstanceId == 0)
			{
				return;
			}
			global::Kampai.Game.Instance byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Instance>(update.InstanceId);
			if (byInstanceId != null)
			{
				global::Kampai.Game.TaxonomyDefinition taxonomyDefinition = byInstanceId.Definition as global::Kampai.Game.TaxonomyDefinition;
				if (taxonomyDefinition != null)
				{
					highLevel = taxonomyDefinition.TaxonomyHighLevel;
					specific = taxonomyDefinition.TaxonomySpecific;
					type = taxonomyDefinition.TaxonomyType;
					other = taxonomyDefinition.TaxonomyOther;
				}
			}
			else
			{
				global::Kampai.Game.TaxonomyDefinition definition3 = null;
				if (definitionService.TryGet<global::Kampai.Game.TaxonomyDefinition>(update.InstanceId, out definition3))
				{
					highLevel = definition3.TaxonomyHighLevel;
					specific = definition3.TaxonomySpecific;
					type = definition3.TaxonomyType;
					other = definition3.TaxonomyOther;
				}
			}
		}

		private void sendEarnedTelemetry(string sourceName)
		{
			foreach (global::Kampai.Util.QuantityItem output in update.Outputs)
			{
				if (output.ID == 0)
				{
					telemetryService.Send_Telemetry_EVT_IGE_FREE_CREDITS_EARNED((int)output.Quantity, questService.GetEventName(sourceName), update.IsFromPremiumSource);
					continue;
				}
				if (output.ID == 1)
				{
					telemetryService.Send_Telemetry_EVT_IGE_PAID_CREDITS_EARNED((int)output.Quantity, sourceName, update.IsFromPremiumSource);
					continue;
				}
				global::Kampai.Game.IngredientsItemDefinition definition = null;
				if (definitionService.TryGet<global::Kampai.Game.IngredientsItemDefinition>(output.ID, out definition))
				{
					SendCraftableEarnedSpentTelemetry(true, sourceName, (int)output.Quantity, definition);
				}
			}
			if (update.TransactionId != 5037 || update.NewItems == null)
			{
				return;
			}
			foreach (global::Kampai.Game.Instance newItem in update.NewItems)
			{
				SendCraftableEarnedSpentTelemetry(true, sourceName, 1, newItem.Definition);
			}
		}

		private void SendCraftableEarnedSpentTelemetry(bool earned, string sourceName, int quantity, global::Kampai.Game.Definition def)
		{
			string localizedKey = def.LocalizedKey;
			string text = string.Empty;
			string specific = text;
			string type = text;
			string other = text;
			global::Kampai.Game.TaxonomyDefinition taxonomyDefinition = def as global::Kampai.Game.TaxonomyDefinition;
			if (taxonomyDefinition != null)
			{
				text = SafeString(taxonomyDefinition.TaxonomyHighLevel);
				specific = SafeString(taxonomyDefinition.TaxonomySpecific);
				type = SafeString(taxonomyDefinition.TaxonomyType);
				other = SafeString(taxonomyDefinition.TaxonomyOther);
			}
			if (earned)
			{
				telemetryService.Send_Telemetry_EVT_IGE_RESOURCE_CRAFTABLE_EARNED(quantity, sourceName, localizedKey, text, specific, type, other);
			}
			else
			{
				telemetryService.Send_Telemetry_EVT_IGE_RESOURCE_CRAFTABLE_SPENT(quantity, sourceName, localizedKey, text, specific, type, other);
			}
		}

		private string SafeString(string input)
		{
			return input ?? string.Empty;
		}

		private void CreateNewMinions()
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.Instance> newItems = update.NewItems;
			if (newItems == null)
			{
				return;
			}
			global::Kampai.Game.Minion minion = null;
			int i = 0;
			for (int count = newItems.Count; i < count; i++)
			{
				global::Kampai.Game.QuantityInstance quantityInstance = newItems[i] as global::Kampai.Game.QuantityInstance;
				if (quantityInstance != null && quantityInstance.ID == 5)
				{
					global::Kampai.Game.Transaction.WeightedInstance weightedInstance = playerService.GetWeightedInstance(4007);
					for (int j = 0; j < quantityInstance.Quantity; j++)
					{
						global::Kampai.Util.QuantityItem quantityItem = weightedInstance.NextPick(randomService);
						global::Kampai.Game.MinionDefinition def = definitionService.Get<global::Kampai.Game.MinionDefinition>(quantityItem.ID);
						minion = new global::Kampai.Game.Minion(def);
						playerService.Add(minion);
						unlockCharacterModel.minionUnlocks.Add(minion);
					}
				}
				else
				{
					minion = newItems[i] as global::Kampai.Game.Minion;
					if (minion != null)
					{
						unlockCharacterModel.minionUnlocks.Add(minion);
					}
				}
			}
		}
	}
}
