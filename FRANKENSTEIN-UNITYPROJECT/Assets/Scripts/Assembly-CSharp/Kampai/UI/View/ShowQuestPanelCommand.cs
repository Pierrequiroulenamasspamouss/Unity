namespace Kampai.UI.View
{
	public class ShowQuestPanelCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public int questInstanceID { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal playSFXSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService prestigeService { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingChangeStateSignal changeState { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowHUDSignal showHUDSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RemoveWayFinderSignal RemoveWayFinderSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.Quest byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Quest>(questInstanceID);
			if (byInstanceId.state == global::Kampai.Game.QuestState.RunningTasks || byInstanceId.state == global::Kampai.Game.QuestState.RunningCompleteScript || byInstanceId.state == global::Kampai.Game.QuestState.Harvestable)
			{
				CreateQuestPanel();
			}
			if (byInstanceId.state == global::Kampai.Game.QuestState.Notstarted || byInstanceId.state == global::Kampai.Game.QuestState.RunningStartScript)
			{
				global::Kampai.Game.QuestDefinition activeDefinition = byInstanceId.GetActiveDefinition();
				for (int i = 0; i < byInstanceId.Steps.Count; i++)
				{
					if (activeDefinition.QuestSteps[i].Type == global::Kampai.Game.QuestStepType.BridgeRepair)
					{
						changeState.Dispatch(byInstanceId.Steps[i].TrackedID, global::Kampai.Game.BuildingState.Working);
					}
				}
				questService.GoToNextQuestState(byInstanceId);
				int surfaceID = byInstanceId.Definition.SurfaceID;
				global::Kampai.Game.QuestSurfaceType surfaceType = byInstanceId.Definition.SurfaceType;
				if (surfaceID > 0 && surfaceType == global::Kampai.Game.QuestSurfaceType.Character)
				{
					global::Kampai.Game.Prestige prestige = prestigeService.GetPrestige(surfaceID);
					if (prestigeService.IsTaskableWhileQuesting(prestige))
					{
						RemoveWayFinderSignal.Dispatch(byInstanceId.QuestIconTrackedInstanceId);
						prestigeService.ChangeToPrestigeState(prestige, global::Kampai.Game.PrestigeState.TaskableWhileQuesting);
					}
				}
			}
			questService.UpdateDeliveryTask();
			questService.UpdateHarvestTask(0, global::Kampai.Game.QuestTaskTransition.Start);
		}

		private void CreateQuestPanel()
		{
			showHUDSignal.Dispatch(true);
			global::Kampai.UI.View.IGUICommand iGUICommand = guiService.BuildCommand(global::Kampai.UI.View.GUIOperation.Queue, "screen_QuestPanel");
			iGUICommand.skrimScreen = "QuestPanelSkrim";
			iGUICommand.darkSkrim = true;
			iGUICommand.singleSkrimClose = true;
			iGUICommand.Args.Add(questInstanceID);
			guiService.Execute(iGUICommand);
		}
	}
}
