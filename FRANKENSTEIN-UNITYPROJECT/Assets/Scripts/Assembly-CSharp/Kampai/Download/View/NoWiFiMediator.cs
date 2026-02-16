namespace Kampai.Download.View
{
	public class NoWiFiMediator : global::strange.extensions.mediation.impl.Mediator
	{
		private bool canShowSettings;

		[Inject]
		public global::Kampai.Download.View.NoWiFiView view { get; set; }

		[Inject]
		public global::Kampai.Download.DLCModel dlcModel { get; set; }

		[Inject]
		public global::Kampai.Download.DownloadDLCPartSignal downloadDLCPartSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Download.ShowNoWiFiPanelSignal showNoWiFiPanelSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Download.Model.DownloadUIModel uiModel { get; set; }

		[Inject]
		public global::Kampai.Common.NetworkModel networkModel { get; set; }

		[Inject]
		public global::Kampai.Common.NetworkConnectionLostSignal networkConnectionLostSignal { get; set; }

		public override void OnRegister()
		{
			canShowSettings = global::Kampai.Util.Native.CanShowNetworkSettings();
			view.Init(!canShowSettings);
			if (canShowSettings)
			{
				view.continueButton2.ClickedSignal.AddListener(ContinueButton);
				view.exitButton2.ClickedSignal.AddListener(ExitButton);
				view.settingsButton.ClickedSignal.AddListener(SettingsButton);
			}
			else
			{
				view.continueButton1.ClickedSignal.AddListener(ContinueButton);
				view.exitButton1.ClickedSignal.AddListener(ExitButton);
			}
			networkConnectionLostSignal.AddListener(Close);
		}

		private void OnEnable()
		{
			uiModel.PopupIsOpen = true;
			routineRunner.StartCoroutine(WaitForWiFi());
		}

		private void OnDisable()
		{
			uiModel.PopupIsOpen = false;
		}

		private global::System.Collections.IEnumerator WaitForWiFi()
		{
			while (!global::Kampai.Util.NetworkUtil.IsNetworkWiFi() && uiModel.PopupIsOpen && !networkModel.isConnectionLost)
			{
				logger.Debug("Waiting for wifi...");
				yield return new global::UnityEngine.WaitForSeconds(1f);
			}
			if (uiModel.PopupIsOpen)
			{
				Close();
				if (!networkModel.isConnectionLost)
				{
					downloadDLCPartSignal.Dispatch();
				}
			}
		}

		public override void OnRemove()
		{
			networkConnectionLostSignal.RemoveListener(Close);
			if (canShowSettings)
			{
				view.continueButton2.ClickedSignal.RemoveListener(ContinueButton);
				view.exitButton2.ClickedSignal.RemoveListener(ExitButton);
				view.settingsButton.ClickedSignal.RemoveListener(SettingsButton);
			}
			else
			{
				view.continueButton1.ClickedSignal.RemoveListener(ContinueButton);
				view.exitButton1.ClickedSignal.RemoveListener(ExitButton);
			}
		}

		private void ContinueButton()
		{
			dlcModel.AllowDownloadVia3G = true;
			Close();
			downloadDLCPartSignal.Dispatch();
		}

		private void SettingsButton()
		{
			global::Kampai.Util.Native.OpenNetworkSettings();
		}

		private void ExitButton()
		{
			global::UnityEngine.Application.Quit();
		}

		private void Close()
		{
			showNoWiFiPanelSignal.Dispatch(false);
		}
	}
}
