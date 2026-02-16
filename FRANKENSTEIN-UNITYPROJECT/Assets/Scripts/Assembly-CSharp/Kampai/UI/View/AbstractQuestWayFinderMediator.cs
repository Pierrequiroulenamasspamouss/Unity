namespace Kampai.UI.View
{
	public abstract class AbstractQuestWayFinderMediator : global::Kampai.UI.View.AbstractWayFinderMediator
	{
		[Inject]
		public global::Kampai.UI.View.ShowQuestPanelSignal ShowQuestPanelSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowQuestRewardSignal ShowQuestRewardSignal { get; set; }

		protected override void GoToClicked()
		{
			global::Kampai.UI.View.IQuestWayFinderView questWayFinderView = View as global::Kampai.UI.View.IQuestWayFinderView;
			if (questWayFinderView.IsTargetObjectVisible())
			{
				HandleClick();
			}
			else
			{
				base.PanToInstance();
			}
		}

		private void HandleClick()
		{
			global::Kampai.UI.View.AbstractQuestWayFinderView abstractQuestWayFinderView = View as global::Kampai.UI.View.AbstractQuestWayFinderView;
			abstractQuestWayFinderView.ClickedOnce = true;
			global::Kampai.Game.Quest currentActiveQuest = abstractQuestWayFinderView.CurrentActiveQuest;
			if (currentActiveQuest == null)
			{
				return;
			}
			bool flag = currentActiveQuest.IsProcedurallyGenerated();
			switch (currentActiveQuest.state)
			{
			case global::Kampai.Game.QuestState.Notstarted:
			case global::Kampai.Game.QuestState.RunningStartScript:
			case global::Kampai.Game.QuestState.RunningTasks:
			case global::Kampai.Game.QuestState.RunningCompleteScript:
				if (flag)
				{
					SelectTSM();
				}
				else
				{
					ShowQuestPanelSignal.Dispatch(currentActiveQuest.ID);
				}
				break;
			case global::Kampai.Game.QuestState.Harvestable:
				if (flag)
				{
					SelectTSM();
				}
				else
				{
					ShowQuestRewardSignal.Dispatch(currentActiveQuest.ID);
				}
				break;
			}
			abstractQuestWayFinderView.SetNextQuest();
		}

		private void SelectTSM()
		{
			base.GameContext.injectionBinder.GetInstance<global::Kampai.Game.TSMCharacterSelectedSignal>().Dispatch();
		}
	}
}
