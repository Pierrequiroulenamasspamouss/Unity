namespace Kampai.Download.View
{
	public class DLCProgressBarView : global::strange.extensions.mediation.impl.View
	{
		public global::UnityEngine.UI.Text titleText;

		public global::UnityEngine.UI.Text updateText;

		public global::UnityEngine.UI.Image progressBarFill;

		public void UpdateProgress(global::Kampai.Download.DLCLoadScreenModel model)
		{
			float x = model.CurrentProgress / model.TotalSize;
			progressBarFill.rectTransform.anchorMax = new global::UnityEngine.Vector2(x, 1f);
		}
	}
}
