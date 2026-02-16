namespace Kampai.UI.View
{
	public class NotificationsMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.NotificationsView>
	{
		[Inject]
		public global::Kampai.Game.IDevicePrefsService service { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal playSFXSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CloseAllOtherMenuSignal closeSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSkrimSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Game.SetupPushNotificationsSignal pushNotificationSignal { get; set; }

		[Inject]
		public ILocalPersistanceService localPersistence { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void OnRegister()
		{
			playSFXSignal.Dispatch("Play_menu_popUp_01");
			base.OnRegister();
			base.view.Init(localService);
			base.view.OnMenuClose.AddListener(Close);
			base.view.YesButton.onClick.AddListener(YesNotify);
			base.view.NoButton.onClick.AddListener(NoNotify);
		}

		public override void OnRemove()
		{
			base.OnRemove();
			base.view.OnMenuClose.RemoveListener(Close);
			base.view.YesButton.onClick.RemoveListener(YesNotify);
			base.view.NoButton.onClick.RemoveListener(NoNotify);
		}

		private void YesNotify()
		{
			EnableNotifications();
			pushNotificationSignal.Dispatch();
			closeSignal.Dispatch(null);
		}

		private void NoNotify()
		{
			DisableNotifications();
			closeSignal.Dispatch(null);
		}

		protected override void Close()
		{
			string dataPlayer = localPersistence.GetDataPlayer("AllowPushNotifications");
			if (string.IsNullOrEmpty(dataPlayer))
			{
				DisableNotifications();
			}
			hideSkrimSignal.Dispatch("NotificationsSkrim");
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "popup_Notification");
		}

		private void EnableNotifications()
		{
			service.GetDevicePrefs().ConstructionNotif = true;
			service.GetDevicePrefs().BlackMarketNotif = true;
			service.GetDevicePrefs().MinionsParadiseNotif = true;
			service.GetDevicePrefs().BaseResourceNotif = true;
			service.GetDevicePrefs().CraftingNotif = true;
			service.GetDevicePrefs().EventNotif = true;
			service.GetDevicePrefs().MarketPlaceNotif = true;
			localPersistence.PutDataPlayer("AllowPushNotifications", "true");
		}

		private void DisableNotifications()
		{
			service.GetDevicePrefs().ConstructionNotif = false;
			service.GetDevicePrefs().BlackMarketNotif = false;
			service.GetDevicePrefs().MinionsParadiseNotif = false;
			service.GetDevicePrefs().BaseResourceNotif = false;
			service.GetDevicePrefs().CraftingNotif = false;
			service.GetDevicePrefs().EventNotif = false;
			service.GetDevicePrefs().MarketPlaceNotif = false;
			localPersistence.PutDataPlayer("AllowPushNotifications", "false");
		}
	}
}
