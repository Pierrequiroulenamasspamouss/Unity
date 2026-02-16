namespace Kampai.UI.View
{
	public class ResourceIconModal : global::Kampai.UI.View.WorldToGlassUIModal
	{
		public global::Kampai.UI.View.KampaiImage TextBackground;

		public global::Kampai.UI.View.KampaiImage Image;

		public global::UnityEngine.UI.Text Text;

		public global::strange.extensions.signal.impl.Signal ClickedSignal = new global::strange.extensions.signal.impl.Signal();

		public void OnClickEvent()
		{
			ClickedSignal.Dispatch();
		}
	}
}
