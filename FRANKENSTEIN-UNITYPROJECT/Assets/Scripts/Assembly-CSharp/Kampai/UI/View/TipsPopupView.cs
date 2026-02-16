namespace Kampai.UI.View
{
	public class TipsPopupView : global::strange.extensions.mediation.impl.View
	{
		public global::UnityEngine.UI.Text TipsText;

		public global::Kampai.UI.View.ButtonView closeButton;

		internal void Display(string text)
		{
			TipsText.text = text;
			base.gameObject.SetActive(true);
		}
	}
}
