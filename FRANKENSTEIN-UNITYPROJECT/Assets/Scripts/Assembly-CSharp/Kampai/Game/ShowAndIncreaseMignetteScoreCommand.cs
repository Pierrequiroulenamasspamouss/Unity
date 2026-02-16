namespace Kampai.Game
{
	public class ShowAndIncreaseMignetteScoreCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Game.Mignette.MignetteGameModel mignetteGameModel { get; set; }

		[Inject]
		public global::Kampai.Game.MignetteCollectionService collectionService { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowHUDSignal showHUDSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetHUDButtonsVisibleSignal setHUDButtonsVisibleSignal { get; set; }

		[Inject]
		public global::Kampai.Game.EjectAllMinionsFromBuildingSignal ejectAllMinionsFromBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.Mignette.DestroyMignetteContextSignal destroyMignetteContextSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			collectionService.IncreaseScoreForMignetteCollection(mignetteGameModel.BuildingId, mignetteGameModel.CurrentGameScore);
			ejectAllMinionsFromBuildingSignal.Dispatch(mignetteGameModel.BuildingId);
			destroyMignetteContextSignal.Dispatch();
			global::Kampai.UI.View.IGUICommand iGUICommand = guiService.BuildCommand(global::Kampai.UI.View.GUIOperation.Load, "MignetteScoreSummary");
			iGUICommand.Args.Add(true);
			iGUICommand.Args.Add(mignetteGameModel.BuildingId);
			iGUICommand.skrimScreen = "MignetteSkrim";
			iGUICommand.darkSkrim = true;
			iGUICommand.disableSkrimButton = true;
			guiService.Execute(iGUICommand);
			showHUDSignal.Dispatch(true);
			setHUDButtonsVisibleSignal.Dispatch(false);
		}
	}
}
