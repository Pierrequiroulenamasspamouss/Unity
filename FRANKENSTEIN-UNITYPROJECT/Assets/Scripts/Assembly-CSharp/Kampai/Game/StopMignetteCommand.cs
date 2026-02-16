namespace Kampai.Game
{
	public class StopMignetteCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public bool isInterrupted { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowHUDSignal showHUDSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowStoreSignal showStoreSignal { get; set; }

		[Inject]
		public global::Kampai.Common.PickControllerModel pickControllerModel { get; set; }

		[Inject]
		public global::Kampai.Game.EjectAllMinionsFromBuildingSignal ejectAllMinionsFromBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.Mignette.MignetteGameModel mignetteGameModel { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Game.Mignette.DestroyMignetteContextSignal destroyMignetteContextSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			showHUDSignal.Dispatch(true);
			showStoreSignal.Dispatch(true);
			pickControllerModel.Enabled = true;
			int buildingId = mignetteGameModel.BuildingId;
			if (!isInterrupted)
			{
				ejectAllMinionsFromBuildingSignal.Dispatch(buildingId);
			}
			global::Kampai.Game.MignetteBuilding byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.MignetteBuilding>(buildingId);
			global::Kampai.Game.MignetteBuildingDefinition mignetteBuildingDefinition = byInstanceId.MignetteBuildingDefinition;
			if (mignetteBuildingDefinition.ShowMignetteHUD)
			{
				guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "MignetteHUD");
			}
			destroyMignetteContextSignal.Dispatch();
			if (!isInterrupted)
			{
				questService.UpdateMignetteTask(byInstanceId, global::Kampai.Game.QuestTaskTransition.Complete);
			}
			mignetteGameModel.IsMignetteActive = false;
			string localizedKey = mignetteBuildingDefinition.LocalizedKey;
			int currentGameScore = mignetteGameModel.CurrentGameScore;
			float elapsedTime = mignetteGameModel.ElapsedTime;
			telemetryService.Send_Telemetry_EVT_MINI_GAME_PLAYED(localizedKey, currentGameScore, elapsedTime);
		}
	}
}
