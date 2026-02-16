namespace Kampai.UI.View
{
	public class COPPAAgeGatePanelMediator : global::strange.extensions.mediation.impl.Mediator
	{
		[Inject]
		public global::Kampai.UI.View.COPPAAgeGatePanelView view { get; set; }

		[Inject]
		public global::Kampai.Game.SaveDevicePrefsSignal saveSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Common.ICoppaService coppaService { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.Game.UserAgeForCOPPAReceivedSignal userAgeForCOPPAReceivedSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowHUDSignal showHUDSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void OnRegister()
		{
			view.Init(soundFXSignal, timeService);
			view.AcceptButton.ClickedSignal.AddListener(OnAccept);
			view.TOSButton.ClickedSignal.AddListener(TermsOfServiceClicked);
			view.EULAButton.ClickedSignal.AddListener(EulaClicked);
			view.PrivacyButton.ClickedSignal.AddListener(PrivacyPolicyClicked);
			view.DeclineButton.ClickedSignal.AddListener(OnDecline);
			routineRunner.StartCoroutine(WaitAFrame());
		}

		public override void OnRemove()
		{
			view.AcceptButton.ClickedSignal.RemoveListener(OnAccept);
			view.TOSButton.ClickedSignal.RemoveListener(TermsOfServiceClicked);
			view.EULAButton.ClickedSignal.RemoveListener(EulaClicked);
			view.PrivacyButton.ClickedSignal.RemoveListener(PrivacyPolicyClicked);
			view.DeclineButton.ClickedSignal.RemoveListener(OnDecline);
			saveSignal.Dispatch();
			global::System.DateTime birthdate;
			if (!coppaService.GetBirthdate(out birthdate))
			{
				birthdate = global::System.DateTime.Now;
			}
			telemetryService.Send_Telemetry_EVT_AGE_GATE_SET(birthdate.Year, birthdate.Month);
			telemetryService.COPPACompliance();
			showHUDSignal.Dispatch(true);
		}

		private void OnAccept()
		{
			global::System.DateTime now = global::System.DateTime.Now;
			int num = now.Year - (int)view.AgeSlider.value;
			global::System.DateTime userBirthdate = new global::System.DateTime(num, now.Month, 1);
			coppaService.SetUserBirthdate(userBirthdate);
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "COPPA_Age_Gate_Panel");
			userAgeForCOPPAReceivedSignal.Dispatch(new global::Kampai.Util.Tuple<int, int>(num, now.Month));
		}

		private void OnDecline()
		{
			global::System.DateTime now = global::System.DateTime.Now;
			global::System.DateTime userBirthdate = new global::System.DateTime(now.Year, now.Month, 1);
			coppaService.SetUserBirthdate(userBirthdate);
			userAgeForCOPPAReceivedSignal.Dispatch(new global::Kampai.Util.Tuple<int, int>(now.Year, now.Month));
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "COPPA_Age_Gate_Panel");
		}

		private global::System.Collections.IEnumerator WaitAFrame()
		{
			yield return null;
			showHUDSignal.Dispatch(false);
		}

		private void TermsOfServiceClicked()
		{
			global::Kampai.Util.LegalDocuments.TermsOfServiceClicked(localService, soundFXSignal, logger);
		}

		private void PrivacyPolicyClicked()
		{
			global::Kampai.Util.LegalDocuments.PrivacyPolicyClicked(localService, soundFXSignal, logger);
		}

		private void EulaClicked()
		{
			global::Kampai.Util.LegalDocuments.EulaClicked(localService, soundFXSignal, logger);
		}
	}
}
