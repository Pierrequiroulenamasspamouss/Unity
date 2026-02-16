namespace Kampai.UI.View
{
	public class QuestPanelProgressBarView : global::strange.extensions.mediation.impl.View
	{
		public global::UnityEngine.UI.Text TimeRemainingText;

		private int endTime;

		private global::Kampai.Game.ITimeService timeService;

		private int timeRemaining;

		internal void Init(int UTCEndTime, global::Kampai.Game.ITimeService timeService)
		{
			endTime = UTCEndTime;
			this.timeService = timeService;
		}

		public void Update()
		{
			if (timeService != null)
			{
				UpdateTime(timeService.GameTimeSeconds());
			}
		}

		internal void UpdateTime(int currentTime)
		{
			timeRemaining = endTime - currentTime;
			TimeRemainingText.text = UIUtils.FormatTime(timeRemaining);
		}
	}
}
