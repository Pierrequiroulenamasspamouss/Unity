namespace Kampai.Game
{
	public class GotoCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.GotoArgument gotoArgument { get; set; }

		[Inject]
		public global::Kampai.UI.View.OpenStoreHighlightItemSignal openStoreSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoMoveToBuildingSignal moveToBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.UI.View.BuildMenuOpenedSignal buildMenuOpened { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		public override void Execute()
		{
			if (gotoArgument.BuildingId > 0)
			{
				global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(gotoArgument.BuildingId);
				moveToBuildingSignal.Dispatch(byInstanceId, new global::Kampai.Game.PanInstructions(byInstanceId));
			}
			else if (gotoArgument.BuildingDefId > 0)
			{
				GotoBuildingDefinition(gotoArgument.BuildingDefId, gotoArgument.ForceStore);
			}
			else if (gotoArgument.ItemId > 0)
			{
				int buildingDefintionIDFromItemDefintionID = definitionService.GetBuildingDefintionIDFromItemDefintionID(gotoArgument.ItemId);
				if (buildingDefintionIDFromItemDefintionID > 0)
				{
					GotoBuildingDefinition(buildingDefintionIDFromItemDefintionID, gotoArgument.ForceStore);
					return;
				}
				logger.Warning("No building for item {0}", gotoArgument.ItemId);
			}
			else
			{
				logger.Warning("Nothing to goto");
			}
		}

		private void GotoBuildingDefinition(int definition, bool forceStore)
		{
			if (forceStore)
			{
				buildMenuOpened.Dispatch();
				openStoreSignal.Dispatch(definition, true);
				return;
			}
			global::System.Collections.Generic.List<global::Kampai.Game.Building> list = new global::System.Collections.Generic.List<global::Kampai.Game.Building>();
			foreach (global::Kampai.Game.Building item in playerService.GetByDefinitionId<global::Kampai.Game.Building>(definition))
			{
				if (item.State != global::Kampai.Game.BuildingState.Inventory)
				{
					list.Add(item);
				}
			}
			if (list.Count > 0)
			{
				global::Kampai.Game.Building building = list[global::UnityEngine.Random.Range(0, list.Count - 1)];
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.PanAndOpenModalSignal>().Dispatch(building.ID);
			}
			else
			{
				buildMenuOpened.Dispatch();
				openStoreSignal.Dispatch(definition, true);
			}
		}
	}
}
