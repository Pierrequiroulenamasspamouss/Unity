namespace Kampai.UI.View
{
	public class QuestLineProgressBarMediator : global::strange.extensions.mediation.impl.Mediator
	{
		[Inject]
		public global::Kampai.UI.View.QuestLineProgressBarView view { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateQuestPanelWithNewQuestSignal updateQuestPanelSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateQuestLineProgressSignal updateQuestLineProgressSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.QuestDetailIDSignal idSignal { get; set; }

		public override void OnRegister()
		{
			updateQuestLineProgressSignal.AddListener(OnQuestSelected);
			updateQuestPanelSignal.AddListener(OnQuestSelected);
			idSignal.AddListener(OnQuestSelected);
		}

		public override void OnRemove()
		{
			updateQuestLineProgressSignal.RemoveListener(OnQuestSelected);
			updateQuestPanelSignal.RemoveListener(OnQuestSelected);
			idSignal.RemoveListener(OnQuestSelected);
		}

		private void OnQuestSelected(int questId)
		{
			global::Kampai.Game.Quest quest = playerService.GetByInstanceId<global::Kampai.Game.Quest>(questId);
			int questLineID = quest.GetActiveDefinition().QuestLineID;
			string title = localService.GetString("QuestLineTitle3");
			if (questLineID == 0)
			{
				view.UpdateProgress(0, 1);
				view.SetTitle(title);
				return;
			}
			global::Kampai.Game.PrestigeDefinition definition = null;
			if (definitionService.TryGet<global::Kampai.Game.PrestigeDefinition>(quest.GetActiveDefinition().SurfaceID, out definition))
			{
				string text = localService.GetString(definition.LocalizedKey);
				title = localService.GetString("QuestLineTitle2", text);
			}
			view.SetTitle(title);
			global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.QuestLine> questLines = questService.GetQuestLines();
			global::Kampai.Game.QuestLine questLine = questLines[questLineID];
			int count = questLine.Quests.Count;
			int num = (questLine.Quests as global::System.Collections.Generic.List<global::Kampai.Game.QuestDefinition>).FindIndex((global::Kampai.Game.QuestDefinition x) => x.ID == quest.GetActiveDefinition().ID);
			int num2 = questLine.Quests.Count - 1 - num;
			num2 += ((quest.state == global::Kampai.Game.QuestState.Complete) ? 1 : 0);
			view.UpdateProgress(num2, count);
		}
	}
}
