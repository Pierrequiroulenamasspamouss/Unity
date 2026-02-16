namespace Kampai.UI.View
{
	public class PopupConfirmationView : global::Kampai.UI.View.PopupMenuView
	{
		public global::UnityEngine.UI.Text title;

		public global::UnityEngine.UI.Text description;

		public global::Kampai.UI.View.ButtonView Accept;

		public global::Kampai.UI.View.ButtonView Decline;

		public override void Init()
		{
			base.Init();
			Open();
		}
	}
}
