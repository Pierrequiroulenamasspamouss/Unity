namespace Kampai.UI.View
{
	public class ShowQuestRewardCommand : global::strange.extensions.command.impl.Command
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
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.UI.View.CloseAllOtherMenuSignal closeSignal { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowHUDSignal showHUDSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			closeSignal.Dispatch(null);
			global::Kampai.Game.Quest byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Quest>(questInstanceID);
			if (byInstanceId != null)
			{
				global::Kampai.UI.View.IGUICommand iGUICommand = guiService.BuildCommand(global::Kampai.UI.View.GUIOperation.Queue, "popup_QuestReward");
				iGUICommand.skrimScreen = "QuestRewardSkrim";
				iGUICommand.darkSkrim = true;
				iGUICommand.Args.Add(questInstanceID);
				guiService.Execute(iGUICommand);
				playSFXSignal.Dispatch("Play_menu_popUp_01");
				showHUDSignal.Dispatch(true);
				if ((byInstanceId != null && byInstanceId.state == global::Kampai.Game.QuestState.Complete) || byInstanceId.state == global::Kampai.Game.QuestState.Harvestable)
				{
					gameContext.injectionBinder.GetInstance<global::Kampai.Game.RemoveQuestWorldIconSignal>().Dispatch(byInstanceId);
				}
			}
		}
	}
}
