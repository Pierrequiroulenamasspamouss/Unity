namespace Kampai.UI
{
	public class DragFromStoreCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.Definition definition { get; set; }

		[Inject]
		public global::Kampai.Game.Transaction.TransactionDefinition transactionDef { get; set; }

		[Inject]
		public global::UnityEngine.Vector3 eventPosition { get; set; }

		[Inject]
		public int badgeCount { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Common.PickControllerModel pickControllerModel { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject(global::Kampai.Main.MainElement.CAMERA)]
		public global::UnityEngine.Camera mainCamera { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Common.DragAndDropPickSignal dragAndDropSignal { get; set; }

		public override void Execute()
		{
			global::strange.extensions.injector.api.ICrossContextInjectionBinder crossContextInjectionBinder = gameContext.injectionBinder;
			global::Kampai.Game.BuildingDefinition buildingDefinition = definition as global::Kampai.Game.BuildingDefinition;
			if (buildingDefinition == null)
			{
				return;
			}
			pickControllerModel.LastBuildingStorePosition = eventPosition;
			global::UnityEngine.Vector3 pos = BuildingUtil.UIToWorldCoords(mainCamera, eventPosition);
			global::Kampai.Game.Scaffolding instance = crossContextInjectionBinder.GetInstance<global::Kampai.Game.Scaffolding>();
			instance.Definition = buildingDefinition;
			instance.Location = new global::Kampai.Game.Location(pos);
			instance.Transaction = transactionDef;
			instance.Building = null;
			logger.Debug(transactionDef.ToString());
			global::System.Collections.Generic.ICollection<global::Kampai.Game.Instance> byDefinitionId = playerService.GetByDefinitionId<global::Kampai.Game.Instance>(buildingDefinition.ID);
			if (byDefinitionId.Count != 0)
			{
				foreach (global::Kampai.Game.Instance item in byDefinitionId)
				{
					global::Kampai.Game.Building building = item as global::Kampai.Game.Building;
					if (building != null && building.State == global::Kampai.Game.BuildingState.Inventory)
					{
						instance.Building = building;
						break;
					}
				}
			}
			global::Kampai.Game.CreateDummyBuildingSignal instance2 = crossContextInjectionBinder.GetInstance<global::Kampai.Game.CreateDummyBuildingSignal>();
			pos = new global::UnityEngine.Vector3(pos.x, 0f, pos.z);
			instance2.Dispatch(buildingDefinition, pos, badgeCount != 0);
			SetPickController(-1);
		}

		private void SetPickController(int? selectedBuildingID)
		{
			pickControllerModel.InvalidateMovement = false;
			pickControllerModel.StartHitObject = null;
			pickControllerModel.CurrentMode = global::Kampai.Common.PickControllerModel.Mode.DragAndDrop;
			pickControllerModel.SelectedBuilding = selectedBuildingID;
			dragAndDropSignal.Dispatch(1, eventPosition);
		}
	}
}
