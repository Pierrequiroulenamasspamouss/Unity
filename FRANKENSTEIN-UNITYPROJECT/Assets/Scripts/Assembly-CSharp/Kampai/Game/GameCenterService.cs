namespace Kampai.Game
{
	public class GameCenterService : global::Kampai.Game.ISocialService, global::Kampai.Game.ISynergyService
	{
		private global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> successSignal;

		private global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> failureSignal;

		private global::Kampai.Game.GameCenterAuthToken token;

		private bool killSwitchFlag;

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject(global::Kampai.Main.MainElement.MANAGER_PARENT)]
		public global::UnityEngine.GameObject managers { get; set; }

		[Inject]
		public global::Kampai.Game.GameCenterAuthTokenCompleteSignal authCompleteSignal { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.Game.IConfigurationsService configurationsService { get; set; }

		public string LoginSource { get; set; }

		public string userID
		{
			get
			{
				return global::UnityEngine.Social.localUser.id;
			}
		}

		public global::Kampai.Game.SocialServices type
		{
			get
			{
				return global::Kampai.Game.SocialServices.GAMECENTER;
			}
		}

		public bool isLoggedIn
		{
			get
			{
				return global::UnityEngine.Social.localUser.authenticated;
			}
		}

		public bool isKillSwitchEnabled
		{
			get
			{
				return killSwitchFlag;
			}
		}

		public string accessToken
		{
			get
			{
				logger.Debug("accessToken = {0}", global::Newtonsoft.Json.JsonConvert.SerializeObject(token));
				return global::Newtonsoft.Json.JsonConvert.SerializeObject(token);
			}
		}

		public string userName
		{
			get
			{
				return string.Empty;
			}
		}

		public global::System.DateTime tokenExpiry
		{
			get
			{
				return default(global::System.DateTime);
			}
		}

		public void Init(global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> successSignal, global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> failureSignal)
		{
			logger.Debug("Game Center Login");
			updateKillSwitchFlag();
			this.successSignal = successSignal;
			this.failureSignal = failureSignal;
			if (!isKillSwitchEnabled)
			{
				global::UnityEngine.Social.localUser.Authenticate(ProcessAuthentication);
			}
			else
			{
				failureSignal.Dispatch(this);
			}
		}

		private void ProcessAuthentication(bool success)
		{
			if (managers == null)
			{
				return;
			}
			if (success)
			{
				logger.Debug("Game Center Login Success");
				global::UnityEngine.GameObject gameObject = managers.FindChild("GameCenterAuthManager");
				if (gameObject != null)
				{
					authCompleteSignal.AddOnce(delegate(global::Kampai.Util.Tuple<string, string, string, string> authTuple)
					{
						token = new global::Kampai.Game.GameCenterAuthToken
						{
							publicKeyUrl = authTuple.Item1,
							signature = authTuple.Item2,
							salt = authTuple.Item3,
							timestamp = authTuple.Item4,
							bundleId = global::Kampai.Util.Native.BundleIdentifier,
							playerId = global::UnityEngine.Social.localUser.id
						};
						successSignal.Dispatch(this);
					});
					global::Kampai.Util.Native.getAuthToken();
				}
			}
			else
			{
				logger.Debug("Game Center Login Failure");
				failureSignal.Dispatch(this);
			}
		}

		public void Login(global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> successSignal, global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> failureSignal)
		{
			Init(successSignal, failureSignal);
		}

		public void Logout()
		{
		}

		public void updateKillSwitchFlag()
		{
			killSwitchFlag = configurationsService.isKillSwitchOn(global::Kampai.Game.KillSwitch.GAMECENTER);
			logger.Info("Game Center killswitch {0}", killSwitchFlag);
		}

		public void SendLoginTelemetry(string loginLocation)
		{
			telemetryService.Send_Telemetry_EVT_EBISU_LOGIN_GAMECENTER(loginLocation);
		}
	}
}
