namespace Kampai.Game.View
{
	public class TryHarvestBuildingCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.View.BuildingObject buildingObj { get; set; }

		[Inject]
		public global::System.Action callback { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.UI.View.SpawnDooberSignal tweenSignal { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingHarvestSignal buildingHarvestSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateResourceIconCountSignal updateResourceIconCountSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal globalSFXSignal { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingChangeStateSignal buildingChangeStateSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CleanupDebrisSignal cleanupSignal { get; set; }

		[Inject(global::Kampai.Game.GameElement.BUILDING_MANAGER)]
		public global::UnityEngine.GameObject buildingManager { get; set; }

		public override void Execute()
		{
			if (playerService.HasStorageBuilding())
			{
				global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(buildingObj.ID);
				if (!TryHarvestTaskable(byInstanceId))
				{
					TryHarvestCrafting(byInstanceId);
				}
			}
		}

		private bool TryHarvestTaskable(global::Kampai.Game.Building building)
		{
			global::Kampai.Game.TaskableBuilding taskableBuilding = building as global::Kampai.Game.TaskableBuilding;
			if (taskableBuilding == null)
			{
				return false;
			}
			global::Kampai.Game.ResourceBuilding resourceBuilding = taskableBuilding as global::Kampai.Game.ResourceBuilding;
			if (taskableBuilding.GetNumCompleteMinions() > 0 || (resourceBuilding != null && resourceBuilding.AvailableHarvest > 0))
			{
				global::Kampai.Game.TaskableBuildingDefinition definition = taskableBuilding.Definition;
				questService.HarvestTaskableComplete(definition.ID);
				int iD = taskableBuilding.ID;
				global::UnityEngine.Vector3 type = new global::UnityEngine.Vector3(taskableBuilding.Location.x, 0f, taskableBuilding.Location.y);
				int transactionID = taskableBuilding.GetTransactionID(definitionService);
				if (playerService.FinishTransaction(transactionID, global::Kampai.Game.TransactionTarget.HARVEST, new global::Kampai.Game.TransactionArg(iD)))
				{
					global::Kampai.Game.DestructibleBuilding destructibleBuilding = taskableBuilding as global::Kampai.Game.DestructibleBuilding;
					if (destructibleBuilding != null)
					{
						cleanupSignal.Dispatch(iD, true);
					}
					int harvestItemDefinitionIdFromTransactionId = definitionService.GetHarvestItemDefinitionIdFromTransactionId(transactionID);
					tweenSignal.Dispatch(type, global::Kampai.UI.View.DestinationType.STORAGE, harvestItemDefinitionIdFromTransactionId, true);
					buildingManager.GetComponent<global::Kampai.Game.View.BuildingManagerMediator>().LastHarvestedBuildingID = iD;
					buildingManager.GetComponent<global::Kampai.Game.View.BuildingManagerMediator>().HarvestTimer = 1f;
					if (resourceBuilding == null)
					{
						if (taskableBuilding.GetMinionsInBuilding() <= 0)
						{
							return false;
						}
						int minionByIndex = taskableBuilding.GetMinionByIndex(0);
						playerService.GetByInstanceId<global::Kampai.Game.Minion>(minionByIndex).AlreadyRushed = false;
					}
					buildingHarvestSignal.Dispatch(iD);
					callback();
					int newHarvestAvailable = GetNewHarvestAvailable(resourceBuilding, taskableBuilding);
					updateResourceIconCountSignal.Dispatch(new global::Kampai.Util.Tuple<int, int>(iD, harvestItemDefinitionIdFromTransactionId), newHarvestAvailable);
					buildingObj.Bounce();
					questService.UpdateHarvestTask(harvestItemDefinitionIdFromTransactionId, global::Kampai.Game.QuestTaskTransition.Complete);
				}
				return true;
			}
			return false;
		}

		private int GetNewHarvestAvailable(global::Kampai.Game.ResourceBuilding resourceBuilding, global::Kampai.Game.TaskableBuilding taskBuilding)
		{
			int num = 0;
			if (resourceBuilding != null)
			{
				num = resourceBuilding.AvailableHarvest;
				if (taskBuilding.GetMinionsInBuilding() == 0)
				{
					if (resourceBuilding.AvailableHarvest == 0)
					{
						buildingChangeStateSignal.Dispatch(taskBuilding.ID, global::Kampai.Game.BuildingState.Idle);
					}
				}
				else
				{
					buildingChangeStateSignal.Dispatch(taskBuilding.ID, global::Kampai.Game.BuildingState.Working);
				}
			}
			else
			{
				num = taskBuilding.GetNumCompleteMinions();
			}
			return num;
		}

		private void TryHarvestCrafting(global::Kampai.Game.Building building)
		{
			global::Kampai.Game.CraftingBuilding craftingBuilding = building as global::Kampai.Game.CraftingBuilding;
			if (craftingBuilding == null)
			{
				return;
			}
			int count = craftingBuilding.CompletedCrafts.Count;
			if (count <= 0)
			{
				return;
			}
			global::UnityEngine.Vector3 type = new global::UnityEngine.Vector3(craftingBuilding.Location.x, 0f, craftingBuilding.Location.y);
			int num = craftingBuilding.CompletedCrafts[0];
			global::Kampai.Game.IngredientsItemDefinition ingredientsItemDefinition = definitionService.Get<global::Kampai.Game.IngredientsItemDefinition>(num);
			if (!playerService.FinishTransaction(ingredientsItemDefinition.TransactionId, global::Kampai.Game.TransactionTarget.HARVEST, new global::Kampai.Game.TransactionArg(craftingBuilding.ID)))
			{
				return;
			}
			tweenSignal.Dispatch(type, global::Kampai.UI.View.DestinationType.STORAGE, ingredientsItemDefinition.ID, true);
			questService.UpdateHarvestTask(num, global::Kampai.Game.QuestTaskTransition.Complete);
			int num2 = 0;
			for (int i = 1; i < count; i++)
			{
				if (craftingBuilding.CompletedCrafts[i] == num)
				{
					num2++;
				}
			}
			craftingBuilding.CompletedCrafts.RemoveAt(GetLastIndexOfItem(craftingBuilding));
			count = craftingBuilding.CompletedCrafts.Count;
			int count2 = craftingBuilding.RecipeInQueue.Count;
			global::Kampai.Game.BuildingState param = global::Kampai.Game.BuildingState.Idle;
			if (count > 0 && count2 > 0)
			{
				param = global::Kampai.Game.BuildingState.HarvestableAndWorking;
			}
			else if (count > 0)
			{
				param = global::Kampai.Game.BuildingState.Harvestable;
			}
			else if (count2 > 0)
			{
				param = global::Kampai.Game.BuildingState.Working;
			}
			buildingChangeStateSignal.Dispatch(craftingBuilding.ID, param);
			TryHarvestCraftingDone(craftingBuilding.ID, num2, num);
		}

		private int GetLastIndexOfItem(global::Kampai.Game.CraftingBuilding craftBuilding)
		{
			for (int num = craftBuilding.CompletedCrafts.Count - 1; num >= 0; num--)
			{
				if (craftBuilding.CompletedCrafts[num] == craftBuilding.CompletedCrafts[0])
				{
					return num;
				}
			}
			return 0;
		}

		private void TryHarvestCraftingDone(int buildingId, int newCount, int itemDefId)
		{
			buildingObj.Bounce();
			buildingHarvestSignal.Dispatch(buildingId);
			callback();
			updateResourceIconCountSignal.Dispatch(new global::Kampai.Util.Tuple<int, int>(buildingId, itemDefId), newCount);
		}
	}
}
