namespace Kampai.Game
{
	public class GooglePlayService : global::Kampai.Game.ISocialService, global::Kampai.Game.ISynergyService
	{
		private global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> successSignal;

		private global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> failureSignal;

		private bool killSwitchFlag;

		private bool attemptToAuthenticate;

		private GPGManager gpgManager;

		[Inject]
		public ILocalPersistanceService localPersistence { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.Game.IConfigurationsService configurationsService { get; set; }

		public string LoginSource { get; set; }

		public string userID
		{
			get
			{
				return PlayGameServices.getLocalPlayerInfo().playerId;
			}
		}

		public string userName
		{
			get
			{
				return PlayGameServices.getLocalPlayerInfo().name;
			}
		}

		public bool isLoggedIn
		{
			get
			{
				return PlayGameServices.isSignedIn();
			}
		}

		public string accessToken
		{
			get
			{
				if (PlayGameServices.isSignedIn())
				{
					return global::Kampai.Util.Native.getAuthToken();
				}
				return string.Empty;
			}
		}

		public bool isKillSwitchEnabled
		{
			get
			{
				return killSwitchFlag;
			}
		}

		public global::System.DateTime tokenExpiry
		{
			get
			{
				return default(global::System.DateTime);
			}
		}

		public global::Kampai.Game.SocialServices type
		{
			get
			{
				return global::Kampai.Game.SocialServices.GOOGLEPLAY;
			}
		}

		public void Init(global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> successSignal, global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> failureSignal)
		{
			updateKillSwitchFlag();
			if (gpgManager == null)
			{
				gpgManager = new GPGManager();
			}
			GPGManager.authenticationSucceededEvent += AuthSuccess;
			GPGManager.authenticationFailedEvent += AuthFailure;
			this.successSignal = successSignal;
			this.failureSignal = failureSignal;
			logger.Debug("GOOGLE PLAY INIT START");
			logger.Debug(global::UnityEngine.StackTraceUtility.ExtractStackTrace());
			localPersistence.PutData("SocialInProgress", "False");
			PlayGameServices.enableDebugLog(global::UnityEngine.Debug.isDebugBuild);
			if (!PlayGameServices.isSignedIn())
			{
				logger.Debug("GOOGLE PLAY USER NOT SIGNED IN");
				int dataInt = localPersistence.GetDataInt("GoogleFailCount");
				int dataInt2 = localPersistence.GetDataInt("GoogleSuccessCount");
				if (dataInt >= 1 || dataInt2 >= 1)
				{
					logger.Debug("GOOGLE PLAY MAX ATTEMPTS");
					return;
				}
				logger.Debug("GOOGLE PLAY USER NOT SIGNED IN - BELOW MAX");
				if (global::Kampai.Util.Native.GetGoogleAccountCount() > 0)
				{
					if (isKillSwitchEnabled)
					{
						failureSignal.Dispatch(this);
						return;
					}
					attemptToAuthenticate = true;
					localPersistence.PutData("SocialInProgress", "True");
					PlayGameServices.authenticate();
				}
				else
				{
					AuthFailure("No Google Accounts Setup");
				}
			}
			else
			{
				logger.Debug("GOOGLE PLAY USER ALREADY LOGGED IN");
				attemptToAuthenticate = true;
				successSignal.Dispatch(this);
			}
		}

		public void Login(global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> successSignal, global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> failureSignal)
		{
			if (isKillSwitchEnabled)
			{
				failureSignal.Dispatch(this);
				return;
			}
			global::Kampai.Util.Native.clearAuthToken();
			this.successSignal = successSignal;
			this.failureSignal = failureSignal;
			attemptToAuthenticate = true;
			localPersistence.PutData("SocialInProgress", "True");
			PlayGameServices.authenticate();
		}

		public void AuthSuccess(string userID)
		{
			if (attemptToAuthenticate)
			{
				attemptToAuthenticate = false;
				localPersistence.PutData("SocialInProgress", "False");
				logger.Debug("GOOGLE PLAY AUTH SUCCESS");
				logger.Debug(global::UnityEngine.StackTraceUtility.ExtractStackTrace());
				logger.Debug("GP PLAYERID: " + PlayGameServices.getLocalPlayerInfo().playerId);
				logger.Debug("GP USERID: " + userID);
				logger.Debug("GP NAME:" + PlayGameServices.getLocalPlayerInfo().name);
				int dataInt = localPersistence.GetDataInt("GoogleSuccessCount");
				localPersistence.PutDataInt("GoogleSuccessCount", ++dataInt);
				successSignal.Dispatch(this);
			}
		}

		public void AuthFailure(string error)
		{
			if (attemptToAuthenticate)
			{
				attemptToAuthenticate = false;
				localPersistence.PutData("SocialInProgress", "False");
				logger.Debug("Fail msg = " + error);
				logger.Debug("GOOGLE PLAY AUTH FAILURE");
				logger.Debug(global::UnityEngine.StackTraceUtility.ExtractStackTrace());
				int dataInt = localPersistence.GetDataInt("GoogleFailCount");
				localPersistence.PutDataInt("GoogleFailCount", ++dataInt);
				failureSignal.Dispatch(this);
			}
		}

		public void Logout()
		{
			attemptToAuthenticate = false;
			PlayGameServices.signOut();
		}

		public void SendLoginTelemetry(string loginLocation)
		{
			telemetryService.Send_Telemetry_EVT_EBISU_LOGIN_GOOGLEPLAY(loginLocation);
		}

		public void updateKillSwitchFlag()
		{
			killSwitchFlag = configurationsService.isKillSwitchOn(global::Kampai.Game.KillSwitch.GOOGLEPLAY);
		}
	}
}
