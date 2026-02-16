namespace Kampai.UI.View
{
	public class MignetteCooldownMenuView : global::Kampai.UI.View.PopupMenuView
	{
		public global::Kampai.UI.View.ProgressBarModal modal;

		private int startTime;

		private int endTime;

		private global::UnityEngine.Vector2 fillPosition;

		internal void StartTime(int startTime, int endTime)
		{
			SetTimeRemainingText(endTime - startTime);
			this.startTime = startTime;
			this.endTime = endTime;
			fillPosition = modal.fillImage.rectTransform.anchorMax;
		}

		internal void UpdateTime(int timeRemaining)
		{
			int num = endTime - startTime;
			float num2 = 1f - (float)timeRemaining / (float)num;
			fillPosition.x = num2;
			modal.fillImage.rectTransform.anchorMax = fillPosition;
			modal.percentageText.text = string.Format("{0}%", (int)(num2 * 100f));
			SetTimeRemainingText(timeRemaining);
		}

		internal void SetTimeRemainingText(int time)
		{
			int num = time / 3600;
			int num2 = time / 60 % 60;
			int num3 = time % 60;
			modal.timeRemainingText.text = string.Format("{0}:{1}:{2}", num.ToString("00"), num2.ToString("00"), num3.ToString("00"));
		}

		internal void SetRushCost(int rushCost)
		{
			modal.rushText.text = string.Format("{0}", rushCost.ToString());
		}
	}
}
