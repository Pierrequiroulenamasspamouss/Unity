namespace Kampai.UI.View
{
	public class DLCIndicatorView : global::strange.extensions.mediation.impl.View
	{
		public global::Kampai.UI.View.ButtonView button;

		public global::UnityEngine.UI.Text downloadSize;

		public global::UnityEngine.UI.Image progressBar;

		private global::Kampai.UI.View.UIAnimator animator;

		private static global::UnityEngine.Vector2 closedLocation = new global::UnityEngine.Vector2(208f, 94f);

		private static global::UnityEngine.Vector2 openLocation = new global::UnityEngine.Vector2(0f, 94f);

		internal void Init()
		{
			global::UnityEngine.RectTransform rectTransform = base.transform as global::UnityEngine.RectTransform;
			rectTransform.anchoredPosition = closedLocation;
			animator = new global::Kampai.UI.View.UIAnimator();
		}

		internal void Open()
		{
			animator.Play(new global::Kampai.UI.View.UIAnchorPositionSlideAnim(base.transform, 1f, openLocation, GoEaseType.CubicOut));
		}

		internal void Close()
		{
			animator.Play(new global::Kampai.UI.View.UIAnchorPositionSlideAnim(base.transform, 1f, closedLocation, GoEaseType.CubicOut));
		}
	}
}
