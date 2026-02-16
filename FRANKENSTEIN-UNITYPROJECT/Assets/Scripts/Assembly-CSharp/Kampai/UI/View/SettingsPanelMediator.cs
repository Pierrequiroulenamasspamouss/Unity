namespace Kampai.UI.View
{
	public class SettingsPanelMediator : global::strange.extensions.mediation.impl.Mediator
	{
		private float lastSoundPlayed;

		private global::UnityEngine.UI.Toggle doubleConfirmToggle;

		[Inject]
		public global::Kampai.UI.View.SettingsPanelView view { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Game.UpdateVolumeSignal updateVolumeSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IDevicePrefsService prefs { get; set; }

		[Inject]
		public global::Kampai.UI.View.DisplayDLCDialogSignal displayDialogSignal { get; set; }

		[Inject]
		public ILocalPersistanceService localPersistService { get; set; }

		[Inject]
		public global::Kampai.Game.IDLCService dlcService { get; set; }

		[Inject("game.server.environment")]
		public string ServerEnv { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Util.ClientVersion clientVersion { get; set; }

		[Inject]
		public global::Kampai.Game.SaveDevicePrefsSignal saveDevicePrefsSignal { get; set; }

		private void OnEnable()
		{
			view.notificationsButton.ClickedSignal.AddListener(NotificationsButton);
			view.DLCButton.ClickedSignal.AddListener(DLCButton);
			view.doubleConfirmButton.ClickedSignal.AddListener(OnDoubleConfirm);
			Init();
			setServer(ServerEnv);
			setBuild(clientVersion.GetClientVersion());
			view.notificationsPanel.SetActive(false);
			view.MusicSlider.value = ((!global::Kampai.Audio.AudioSettingsModel.MusicMute) ? prefs.GetDevicePrefs().MusicVolume : 0f);
			view.SFXSlider.value = prefs.GetDevicePrefs().SFXVolume;
			view.musicValue.text = ((int)(100f * view.MusicSlider.value)).ToString();
			view.soundValue.text = ((int)(100f * view.SFXSlider.value)).ToString();
			view.volumeSliderChangedSignal.AddListener(OnVolumeChanged);
		}

		private void OnDisable()
		{
			view.notificationsButton.ClickedSignal.RemoveListener(NotificationsButton);
			view.DLCButton.ClickedSignal.RemoveListener(DLCButton);
			view.volumeSliderChangedSignal.RemoveListener(OnVolumeChanged);
			view.doubleConfirmButton.ClickedSignal.RemoveListener(OnDoubleConfirm);
		}

		private void Init()
		{
			string displayQualityLevel = dlcService.GetDisplayQualityLevel();
			if (displayQualityLevel.Equals("DLCHDPack"))
			{
				view.DLCText.text = localService.GetString("DLCSDPack");
			}
			else
			{
				view.DLCText.text = localService.GetString("DLCHDPack");
			}
			view.notificationsText.text = localService.GetString("NotificationsLabel");
			doubleConfirmToggle = view.doubleConfirmButton.GetComponent<global::UnityEngine.UI.Toggle>();
			if (localPersistService.HasKeyPlayer("DoublePurchaseConfirm"))
			{
				doubleConfirmToggle.isOn = localPersistService.GetDataIntPlayer("DoublePurchaseConfirm") != 0;
			}
			else
			{
				doubleConfirmToggle.isOn = true;
				localPersistService.PutDataIntPlayer("DoublePurchaseConfirm", 1);
			}
			view.doubleConfirmText.text = localService.GetString("DoubleConfirm");
		}

		private void OnVolumeChanged(bool isMusicSlider)
		{
			float value = view.MusicSlider.value;
			if (value != 0.9876f && prefs.GetDevicePrefs().MusicVolume != value)
			{
				if (global::Kampai.Audio.AudioSettingsModel.MusicMute)
				{
					global::Kampai.Audio.AudioSettingsModel.NeedMute = false;
					global::Kampai.Audio.AudioSettingsModel.MusicMute = false;
				}
				prefs.GetDevicePrefs().MusicVolume = value;
				view.musicValue.text = ((int)(100f * value)).ToString();
			}
			float value2 = view.SFXSlider.value;
			if (value2 != 0.9876f && prefs.GetDevicePrefs().SFXVolume != value2)
			{
				prefs.GetDevicePrefs().SFXVolume = value2;
				view.soundValue.text = ((int)(100f * value2)).ToString();
			}
			float num = timeService.SecondsSinceAppStartAsFloat() - lastSoundPlayed;
			if (num >= 0.17f)
			{
				if (value2 > 0f)
				{
					if (isMusicSlider)
					{
						soundFXSignal.Dispatch("Play_minion_confirm_select_02");
					}
					else
					{
						soundFXSignal.Dispatch("Play_minion_confirm_select_01");
					}
					lastSoundPlayed = timeService.SecondsSinceAppStartAsFloat();
				}
				saveDevicePrefsSignal.Dispatch();
			}
			updateVolumeSignal.Dispatch();
		}

		private void NotificationsButton()
		{
			view.notificationsPanel.SetActive(true);
		}

		private void DLCButton()
		{
			soundFXSignal.Dispatch("Play_button_click_01");
			displayDialogSignal.Dispatch(localService.GetString("DLCConfirmationDialog"));
		}

		private void OnDoubleConfirm()
		{
			localPersistService.PutDataIntPlayer("DoublePurchaseConfirm", doubleConfirmToggle.isOn ? 1 : 0);
		}

		private void setServer(string serverString)
		{
			view.server.text = localService.GetString("server") + serverString;
		}

		private void setBuild(string buildID)
		{
			view.buildNumber.text = localService.GetString("buildNumber") + buildID;
		}
	}
}
