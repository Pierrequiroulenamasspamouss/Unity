namespace Kampai.Game
{
	public class ConfirmBuildingMovementCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Common.PickControllerModel pickControllerModel { get; set; }

		[Inject(global::Kampai.Game.GameElement.BUILDING_MANAGER)]
		public global::UnityEngine.GameObject buildingManager { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingUtilities buildingUtil { get; set; }

		[Inject]
		public global::Kampai.Game.PlaceBuildingSignal placeBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.DeselectBuildingSignal deselectBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.DisableCameraBehaviourSignal disableCameraSignal { get; set; }

		[Inject]
		public global::Kampai.Game.EnableCameraBehaviourSignal enableCameraSignal { get; set; }

		[Inject]
		public global::Kampai.Game.DebugUpdateGridSignal gridSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CancelBuildingMovementSignal cancelBuildingMovementSignal { get; set; }

		[Inject]
		public global::Kampai.Game.PurchaseNewBuildingSignal purchaseNewBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingChangeStateSignal buildingChangeStateSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CreateInventoryBuildingSignal createInventoryBuildingSignal { get; set; }

		[Inject]
		public ILocalPersistanceService localPersistService { get; set; }

		[Inject]
		public global::Kampai.Game.Scaffolding currentScaffolding { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetPremiumCurrencySignal setPremiumCurrencySignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetGrindCurrencySignal setGrindCurrencySignal { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal globalAudioSignal { get; set; }

		public override void Execute()
		{
			if (pickControllerModel.SelectedBuilding == -1)
			{
				if (buildingUtil.ValidateScaffoldingPlacement(currentScaffolding.Definition, currentScaffolding.Location))
				{
					if (currentScaffolding.Building != null)
					{
						currentScaffolding.Building.Location = currentScaffolding.Location;
						createInventoryBuildingSignal.Dispatch(currentScaffolding.Building, currentScaffolding.Location);
						buildingChangeStateSignal.Dispatch(currentScaffolding.Building.ID, global::Kampai.Game.BuildingState.Idle);
						PlaceCurrentScaffolding();
					}
					else
					{
						PurchaseBuilding();
					}
					globalAudioSignal.Dispatch("Play_building_place_01");
				}
				else
				{
					cancelBuildingMovementSignal.Dispatch(true);
				}
			}
			else
			{
				MoveBuilding();
			}
		}

		private void PurchaseBuilding()
		{
			global::Kampai.Game.TransactionArg transactionArg = new global::Kampai.Game.TransactionArg();
			transactionArg.Add(currentScaffolding.Location);
			transactionArg.Source = "ItemPurchase";
			global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition = definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(currentScaffolding.Transaction.ID);
			global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition2 = transactionDefinition.CopyTransaction();
			foreach (global::Kampai.Util.QuantityItem output in transactionDefinition2.Outputs)
			{
				global::Kampai.Game.BuildingDefinition buildingDefinition = definitionService.Get<global::Kampai.Game.BuildingDefinition>(output.ID);
				if (buildingDefinition == null)
				{
					continue;
				}
				global::System.Collections.Generic.ICollection<global::Kampai.Game.Building> byDefinitionId = playerService.GetByDefinitionId<global::Kampai.Game.Building>(buildingDefinition.ID);
				int num = byDefinitionId.Count * buildingDefinition.IncrementalCost;
				if (num <= 0)
				{
					continue;
				}
				foreach (global::Kampai.Util.QuantityItem input in transactionDefinition2.Inputs)
				{
					if (input.ID == 0 || input.ID == 1)
					{
						input.Quantity += (uint)num;
					}
				}
			}
			playerService.RunEntireTransaction(transactionDefinition2, global::Kampai.Game.TransactionTarget.NO_VISUAL, TransactionCallback, transactionArg);
		}

		private void TransactionCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			if (pct.Success)
			{
				global::Kampai.Game.Building building = null;
				foreach (global::Kampai.Game.Instance output in pct.GetOutputs())
				{
					if (output.Definition.ID == currentScaffolding.Definition.ID)
					{
						building = (global::Kampai.Game.Building)output;
						break;
					}
				}
				if (building != null)
				{
					building.Location = currentScaffolding.Location;
					purchaseNewBuildingSignal.Dispatch(building);
					setPremiumCurrencySignal.Dispatch();
					setGrindCurrencySignal.Dispatch();
					if (building.State != global::Kampai.Game.BuildingState.Idle && building.State != global::Kampai.Game.BuildingState.Construction)
					{
						buildingChangeStateSignal.Dispatch(building.ID, global::Kampai.Game.BuildingState.Inactive);
					}
					currentScaffolding.Building = building;
					PlaceCurrentScaffolding();
				}
			}
			else if (pct.ParentSuccess)
			{
				PurchaseBuilding();
			}
			else
			{
				cancelBuildingMovementSignal.Dispatch(false);
			}
		}

		private void PlaceCurrentScaffolding()
		{
			pickControllerModel.SelectedBuilding = null;
			placeBuildingSignal.Dispatch(currentScaffolding.Building.ID, currentScaffolding.Location);
			deselectBuildingSignal.Dispatch(-1);
			gridSignal.Dispatch();
		}

		private void MoveBuilding()
		{
			if (!pickControllerModel.SelectedBuilding.HasValue)
			{
				return;
			}
			global::Kampai.Game.View.BuildingManagerView component = buildingManager.GetComponent<global::Kampai.Game.View.BuildingManagerView>();
			int value = pickControllerModel.SelectedBuilding.Value;
			global::Kampai.Game.View.BuildingObject buildingObject = component.GetBuildingObject(value);
			if (!(buildingObject == null))
			{
				global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(value);
				global::Kampai.Game.Location location = new global::Kampai.Game.Location(buildingObject.transform.position);
				if (buildingUtil.ValidateLocation(byInstanceId, location))
				{
					placeBuildingSignal.Dispatch(value, location);
					globalAudioSignal.Dispatch("Play_building_place_01");
					deselectBuildingSignal.Dispatch(value);
					disableCameraSignal.Dispatch(8);
					enableCameraSignal.Dispatch(1);
					gridSignal.Dispatch();
					LocalPersistMoveBuildingAction();
				}
			}
		}

		private void LocalPersistMoveBuildingAction()
		{
			if (!localPersistService.HasKey("didyouknow_MoveBuilding"))
			{
				localPersistService.PutDataInt("didyouknow_MoveBuilding", 1);
			}
		}
	}
}
