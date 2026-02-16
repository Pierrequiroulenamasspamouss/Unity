namespace Kampai.UI.View
{
	public class NotificationsView : global::Kampai.UI.View.PopupMenuView
	{
		public global::UnityEngine.UI.Text TitleText;

		public global::UnityEngine.UI.Text MessageText;

		public global::UnityEngine.UI.Text YesButtonText;

		public global::UnityEngine.UI.Text NoButtonText;

		public global::UnityEngine.UI.Button YesButton;

		public global::UnityEngine.UI.Button NoButton;

		private global::Kampai.Main.ILocalizationService localService;

		internal void Init(global::Kampai.Main.ILocalizationService locService)
		{
			base.Init();
			localService = locService;
			TitleText.text = localService.GetString("NotificationTitle");
			MessageText.text = localService.GetString("NotificationMessage");
			YesButtonText.text = localService.GetString("NotificationYes");
			NoButtonText.text = localService.GetString("NotificationNo");
			Open();
		}
	}
}
