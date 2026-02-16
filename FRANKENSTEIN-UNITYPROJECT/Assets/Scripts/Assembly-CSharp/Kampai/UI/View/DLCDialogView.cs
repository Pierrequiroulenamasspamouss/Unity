namespace Kampai.UI.View
{
	public class DLCDialogView : global::strange.extensions.mediation.impl.View
	{
		public global::UnityEngine.UI.Text dialogText;

		public global::Kampai.UI.View.ButtonView acceptButton;

		public global::Kampai.UI.View.ButtonView cancelButton;

		internal void Init()
		{
			global::UnityEngine.RectTransform rectTransform = base.transform as global::UnityEngine.RectTransform;
			rectTransform.offsetMin = global::UnityEngine.Vector2.zero;
			rectTransform.offsetMax = global::UnityEngine.Vector2.zero;
		}
	}
}
