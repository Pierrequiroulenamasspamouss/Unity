namespace Kampai.UI.View
{
	public class SettingsMenuMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.SettingsMenuView>
	{
		[Inject(global::Kampai.Game.SocialServices.FACEBOOK)]
		public global::Kampai.Game.ISocialService facebookService { get; set; }

		[Inject(global::Kampai.Game.SocialServices.GOOGLEPLAY)]
		public global::Kampai.Game.ISocialService googleService { get; set; }

		[Inject]
		public global::Kampai.Common.ICoppaService coppaService { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowSocialPartyFBConnectSignal showFacebookPopupSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SocialLoginSignal socialLoginSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SocialLogoutSignal socialLogoutSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateFacebookStateSignal facebookStateSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CloseAllOtherMenuSignal closeSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SaveDevicePrefsSignal saveSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SocialLoginSuccessSignal loginSuccess { get; set; }

		[Inject]
		public global::Kampai.Game.SocialLoginFailureSignal loginFailure { get; set; }

		[Inject]
		public global::Kampai.UI.View.PopupMessageSignal popupMessageSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.UI.View.OpenRateAppPageSignal openRateAppPageSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowStoreSignal showStoreSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.TogglePopupForHUDSignal togglePopupSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.Common.IVideoService videoService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Common.PickControllerModel model { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			base.view.facebookButton.ClickedSignal.AddListener(FacebookButton);
			base.view.googleButton.ClickedSignal.AddListener(GoogleButton);
			base.view.rateAppButton.ClickedSignal.AddListener(RateAppButton);
			base.view.closeButton.ClickedSignal.AddListener(Close);
			facebookStateSignal.AddListener(setFacebookStatus);
			setFacebookStatus(facebookService.isLoggedIn);
			closeSignal.AddListener(Close);
			loginSuccess.AddListener(LoginSuccess);
			loginFailure.AddListener(LoginFailure);
			base.view.settings.ClickedSignal.AddListener(ShowSettings);
			base.view.about.ClickedSignal.AddListener(ShowAbout);
			base.view.help.ClickedSignal.AddListener(ShowHelp);
			base.view.playMovieButton.ClickedSignal.AddListener(PlayMovieButton);
			init();
		}

		public override void OnRemove()
		{
			base.OnRemove();
			base.view.facebookButton.ClickedSignal.RemoveListener(FacebookButton);
			base.view.rateAppButton.ClickedSignal.RemoveListener(RateAppButton);
			base.view.googleButton.ClickedSignal.RemoveListener(GoogleButton);
			base.view.closeButton.ClickedSignal.RemoveListener(Close);
			closeSignal.RemoveListener(Close);
			loginSuccess.RemoveListener(LoginSuccess);
			loginFailure.RemoveListener(LoginFailure);
			base.view.settings.ClickedSignal.RemoveListener(ShowSettings);
			base.view.about.ClickedSignal.RemoveListener(ShowAbout);
			base.view.help.ClickedSignal.RemoveListener(ShowHelp);
			facebookStateSignal.RemoveListener(setFacebookStatus);
			base.view.playMovieButton.ClickedSignal.RemoveListener(PlayMovieButton);
		}

		private void init()
		{
			base.view.RateUsText.text = localService.GetString("RateUsMenu");
			base.view.playMovieText.text = localService.GetString("PlayMovie");
		}

		protected override void OnEnable()
		{
			togglePopupSignal.Dispatch(true);
			base.OnEnable();
			UpdateLoginButtonText();
			logger.Info("facebook killswitch : {0}", facebookService.isKillSwitchEnabled);
			logger.Info("google+ killswitch : {0}", googleService.isKillSwitchEnabled);
			base.view.facebookButton.gameObject.SetActive(!coppaService.Restricted() && !facebookService.isKillSwitchEnabled);
			base.view.googleButton.gameObject.SetActive(!coppaService.Restricted() && !googleService.isKillSwitchEnabled);
			showStoreSignal.Dispatch(false);
			ShowPanel(base.view.settingsPanel);
			base.view.settingClicked.SetActive(true);
			if (playerService.GetHighestFtueCompleted() < 7)
			{
				questService.PauseQuestScripts();
			}
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			showStoreSignal.Dispatch(true);
		}

		protected override void Close()
		{
			if (base.view.gameObject.activeInHierarchy)
			{
				base.view.gameObject.SetActive(false);
				saveSignal.Dispatch();
				togglePopupSignal.Dispatch(false);
				if (playerService.GetHighestFtueCompleted() < 7)
				{
					questService.ResumeQuestScripts();
				}
				model.Enabled = true;
			}
		}

		private void Close(global::UnityEngine.GameObject ignore)
		{
			Close();
		}

		private void LoginSuccess(global::Kampai.Game.ISocialService socialService)
		{
			switch (socialService.type)
			{
			case global::Kampai.Game.SocialServices.FACEBOOK:
				popupMessageSignal.Dispatch(localService.GetString("fbLoginSuccess"));
				break;
			case global::Kampai.Game.SocialServices.GOOGLEPLAY:
				popupMessageSignal.Dispatch(localService.GetString("googleplayloginsuccess"));
				break;
			}
			UpdateLoginButtonText();
		}

		private void LoginFailure(global::Kampai.Game.ISocialService socialService)
		{
			switch (socialService.type)
			{
			case global::Kampai.Game.SocialServices.FACEBOOK:
				popupMessageSignal.Dispatch(localService.GetString("fbLoginFailure"));
				break;
			case global::Kampai.Game.SocialServices.GOOGLEPLAY:
				popupMessageSignal.Dispatch(localService.GetString("GooglePlayLoginFailure"));
				break;
			}
			UpdateLoginButtonText();
		}

		private void UpdateLoginButtonText()
		{
			base.view.facebookButtonText.text = localService.GetString((!facebookService.isLoggedIn) ? "facebooklogin" : "facebooklogout");
			base.view.googleButtonText.text = localService.GetString((!googleService.isLoggedIn) ? "googleplaylogin" : "googleplaylogout");
		}

		private void FacebookButton()
		{
			SocialButton(facebookService, base.view.facebookButtonText, "facebooklogin");
		}

		private void GoogleButton()
		{
			SocialButton(googleService, base.view.googleButtonText, "googleplaylogin");
		}

		private void SocialButton(global::Kampai.Game.ISocialService service, global::UnityEngine.UI.Text buttonTextView, string loggedInKey)
		{
			if (service.isLoggedIn)
			{
				socialLogoutSignal.Dispatch(service);
				buttonTextView.text = localService.GetString(loggedInKey);
				return;
			}
			facebookService.LoginSource = "Settings";
			if (service.type == global::Kampai.Game.SocialServices.FACEBOOK)
			{
				showFacebookPopupSignal.Dispatch(delegate(bool connected)
				{
					if (connected && loginSuccess != null)
					{
						loginSuccess.Dispatch(facebookService);
					}
				});
			}
			else
			{
				socialLoginSignal.Dispatch(service);
			}
		}

		private void PlayMovieButton()
		{
			videoService.playIntro(false, true);
		}

		private void RateAppButton()
		{
			Close();
			openRateAppPageSignal.Dispatch();
		}

		private void setFacebookStatus(bool loggedOn)
		{
			base.view.facebookButtonText.text = localService.GetString((!loggedOn) ? "facebooklogin" : "facebooklogout");
		}

		private void ShowSettings()
		{
			ShowPanel(base.view.settingsPanel);
			base.view.settingClicked.SetActive(true);
		}

		private void ShowAbout()
		{
			ShowPanel(base.view.aboutPanel);
			base.view.aboutClicked.SetActive(true);
		}

		private void ShowHelp()
		{
			ShowPanel(base.view.helpPanel);
			base.view.helpClicked.SetActive(true);
		}

		private void ShowPanel(global::UnityEngine.GameObject panel)
		{
			if (!panel.activeSelf)
			{
				if (base.view.settingsPanel.activeSelf)
				{
					base.view.settingsPanel.SetActive(false);
					base.view.settingClicked.SetActive(false);
				}
				if (base.view.aboutPanel.activeSelf)
				{
					base.view.aboutPanel.SetActive(false);
					base.view.aboutClicked.SetActive(false);
				}
				if (base.view.helpPanel.activeSelf)
				{
					base.view.helpPanel.SetActive(false);
					base.view.helpClicked.SetActive(false);
				}
				panel.SetActive(true);
			}
		}
	}
}
