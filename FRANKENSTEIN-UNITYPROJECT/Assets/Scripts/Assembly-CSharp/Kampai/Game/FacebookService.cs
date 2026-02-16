namespace Kampai.Game
{
	public class FacebookService : global::Kampai.Game.ISocialService
	{
		private global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> _initSuccessSignal;

		private global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> _initFailSignal;

		private global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> _loginSuccessSignal;

		private global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> _loginFailureSignal;

		private global::strange.extensions.signal.impl.Signal<global::System.Collections.Generic.List<string>> _inviteSignalSuccess;

		private global::strange.extensions.signal.impl.Signal<global::System.Collections.Generic.List<string>> _inviteSignalFailure;

		private global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> _getFriendsSignalSuccess;

		private global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> _getFriendsSignalFailure;

		private global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> success = new global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService>();

		private global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> failure = new global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService>();

		private bool killSwitchFlag;

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public ILocalPersistanceService localPersistence { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.Game.IConfigurationsService configurationsService { get; set; }

		public global::System.Collections.Generic.Dictionary<string, global::Kampai.Game.FBUser> friends { get; set; }

		public string LoginSource { get; set; }

		public bool isLoggedIn
		{
			get
			{
				return FB.IsLoggedIn;
			}
		}

		public bool isKillSwitchEnabled
		{
			get
			{
				return killSwitchFlag;
			}
		}

		public string userID
		{
			get
			{
				return FB.UserId;
			}
		}

		public string userName
		{
			get
			{
				return string.Empty;
			}
		}

		public global::Kampai.Game.SocialServices type
		{
			get
			{
				return global::Kampai.Game.SocialServices.FACEBOOK;
			}
		}

		public string accessToken
		{
			get
			{
				return FB.AccessToken;
			}
		}

		public global::System.DateTime tokenExpiry
		{
			get
			{
				return FB.AccessTokenExpiresAt;
			}
		}

		public void Login(global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> successSignal, global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> failureSignal)
		{
			logger.Debug("FacebookService login");
			if (!isKillSwitchEnabled)
			{
				friends = new global::System.Collections.Generic.Dictionary<string, global::Kampai.Game.FBUser>();
				localPersistence.PutData("SocialInProgress", "True");
				_loginSuccessSignal = successSignal;
				_loginFailureSignal = failureSignal;
				FB.Login("user_friends,public_profile,publish_actions", AuthCallback);
			}
			else
			{
				failureSignal.Dispatch(this);
			}
		}

		public void Init(global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> successSignal, global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> failureSignal)
		{
			logger.Debug("Facebook Init Called");
			updateKillSwitchFlag();
			_initSuccessSignal = successSignal;
			_initFailSignal = failureSignal;
			if (!FB.IsInitialized)
			{
				FB.Init(SetInit, global::Kampai.Util.GameConstants.Facebook.APP_ID);
			}
			else
			{
				SetInit();
			}
			localPersistence.PutData("SocialInProgress", "False");
			if (isLoggedIn)
			{
				success.AddListener(downloadFriendsSuccess);
				failure.AddListener(downloadFriendsFailure);
				DownloadFriends(100, success, failure);
			}
		}

		private void downloadFriendsSuccess(global::Kampai.Game.ISocialService service)
		{
			success.RemoveListener(downloadFriendsSuccess);
			failure.RemoveListener(downloadFriendsFailure);
		}

		private void downloadFriendsFailure(global::Kampai.Game.ISocialService service)
		{
			success.RemoveListener(downloadFriendsSuccess);
			failure.RemoveListener(downloadFriendsFailure);
		}

		public void FriendInvite(string message, string title, string data, int maxRecipients, global::strange.extensions.signal.impl.Signal<global::System.Collections.Generic.List<string>> successSignal, global::strange.extensions.signal.impl.Signal<global::System.Collections.Generic.List<string>> failureSignal)
		{
			logger.Debug("test check FriendInvite");
			if (!isKillSwitchEnabled)
			{
				_inviteSignalSuccess = successSignal;
				_inviteSignalFailure = failureSignal;
				FB.AppRequest(message, null, null, null, maxRecipients, data, title, AppRequestCallback);
			}
			else
			{
				failureSignal.Dispatch(null);
			}
		}

		public void SendRequest(string message, string title, string data, global::System.Collections.Generic.IList<string> ids, global::strange.extensions.signal.impl.Signal<global::System.Collections.Generic.List<string>> successSignal, global::strange.extensions.signal.impl.Signal<global::System.Collections.Generic.List<string>> failureSignal)
		{
			logger.Debug("SendRequest");
			if (!isKillSwitchEnabled)
			{
				_inviteSignalSuccess = successSignal;
				_inviteSignalFailure = failureSignal;
				string[] to = null;
				if (ids != null)
				{
					to = (ids as global::System.Collections.Generic.List<string>).ToArray();
				}
				FB.AppRequest(message, to, null, null, 100, data, title, AppRequestCallback);
			}
			else
			{
				failureSignal.Dispatch(null);
			}
		}

		public void SendRequestToAll(string message, string title, string data, global::strange.extensions.signal.impl.Signal<global::System.Collections.Generic.List<string>> successSignal, global::strange.extensions.signal.impl.Signal<global::System.Collections.Generic.List<string>> failureSignal)
		{
			if (!isKillSwitchEnabled)
			{
				_inviteSignalSuccess = successSignal;
				_inviteSignalFailure = failureSignal;
				global::System.Collections.Generic.Dictionary<string, global::Kampai.Game.FBUser>.KeyCollection keys = friends.Keys;
				string[] array = new string[keys.Count];
				keys.CopyTo(array, 0);
				FB.AppRequest(message, array, null, null, 100, data, title, AppRequestCallback);
			}
			else
			{
				failureSignal.Dispatch(null);
			}
		}

		private void AppRequestCallback(FBResult result)
		{
			logger.Debug("AppRequestCallback");
			if (result != null && result.Error == null)
			{
				logger.Debug("result error null");
				global::System.Collections.Generic.Dictionary<string, object> dictionary = global::MiniJSON.Json.Deserialize(result.Text) as global::System.Collections.Generic.Dictionary<string, object>;
				object value = 0;
				if (dictionary.TryGetValue("cancelled", out value))
				{
					logger.Debug("invite cancelled");
					if (_inviteSignalFailure != null)
					{
						_inviteSignalFailure.Dispatch(null);
					}
				}
				else
				{
					if (!dictionary.TryGetValue("request", out value))
					{
						return;
					}
					global::System.Collections.Generic.List<string> list = null;
					object value2;
					if (dictionary.TryGetValue("to", out value2))
					{
						global::System.Collections.Generic.List<object> list2 = value2 as global::System.Collections.Generic.List<object>;
						list = new global::System.Collections.Generic.List<string>();
						foreach (object item in list2)
						{
							list.Add(item as string);
						}
					}
					logger.Debug("invite succeeded");
					if (_inviteSignalSuccess != null)
					{
						_inviteSignalSuccess.Dispatch(list);
					}
				}
			}
			else
			{
				if (_inviteSignalFailure != null)
				{
					_inviteSignalFailure.Dispatch(null);
				}
				if (result.Error != null)
				{
					logger.Error("Facebook: FriendInvite failure " + result.Error);
				}
				else
				{
					logger.Error("Facebook: FriendInvite with no result");
				}
			}
		}

		public void GetUserInfo()
		{
			FB.API("/me?fields=id,first_name", global::Facebook.HttpMethod.GET, GetUserInfoCallback);
		}

		private void GetUserInfoCallback(FBResult result)
		{
			if (result != null && result.Error == null)
			{
				logger.Debug(result.Text);
			}
			else if (result.Error != null)
			{
				logger.Debug(result.Error);
			}
			else
			{
				logger.Debug("Facebook.GetUserInfoCallback with no result");
			}
		}

		public void DownloadFriends(int friendLimit, global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> success, global::strange.extensions.signal.impl.Signal<global::Kampai.Game.ISocialService> failure)
		{
			logger.Debug("FacebookService DownloadFriends");
			if (!isKillSwitchEnabled)
			{
				if (friends == null)
				{
					friends = new global::System.Collections.Generic.Dictionary<string, global::Kampai.Game.FBUser>();
				}
				friendLimit = 0;
				string query = string.Format("me/friends?fields=name,id&access_token={0}", FB.AccessToken);
				FB.API(query, global::Facebook.HttpMethod.GET, DownloadFriendsCallback);
				_getFriendsSignalFailure = failure;
				_getFriendsSignalSuccess = success;
			}
			else
			{
				failure.Dispatch(this);
			}
		}

		private void DownloadFriendsCallback(FBResult result)
		{
			if (result != null && result.Error == null)
			{
				global::System.Collections.Generic.Dictionary<string, object> dictionary = global::MiniJSON.Json.Deserialize(result.Text) as global::System.Collections.Generic.Dictionary<string, object>;
				if (!dictionary.ContainsKey("data"))
				{
					if (_getFriendsSignalFailure != null)
					{
						_getFriendsSignalFailure.Dispatch(this);
					}
					return;
				}
				global::System.Collections.Generic.List<object> list = (global::System.Collections.Generic.List<object>)dictionary["data"];
				for (int i = 0; i < list.Count; i++)
				{
					global::System.Collections.Generic.Dictionary<string, object> dictionary2 = (global::System.Collections.Generic.Dictionary<string, object>)list[i];
					string name = dictionary2["name"] as string;
					string text = dictionary2["id"] as string;
					string picture = "https://graph.facebook.com/" + text + "/picture?width=256&height=256";
					if (!friends.ContainsKey(text))
					{
						global::Kampai.Game.FBUser value = new global::Kampai.Game.FBUser(name, text, picture);
						friends.Add(text, value);
					}
				}
				if (_getFriendsSignalSuccess != null)
				{
					_getFriendsSignalSuccess.Dispatch(this);
				}
			}
			else if (result != null && result.Error != null)
			{
				logger.Debug("Facebook: " + result.Error);
				if (_getFriendsSignalFailure != null)
				{
					_getFriendsSignalFailure.Dispatch(this);
				}
			}
			else
			{
				logger.Debug("Facebook: Facebook.GetFriendsCallback with no result");
				if (_getFriendsSignalFailure != null)
				{
					_getFriendsSignalFailure.Dispatch(this);
				}
			}
		}

		public global::Kampai.Game.FBUser GetFriend(string fbid)
		{
			if (friends != null && friends.ContainsKey(fbid))
			{
				return friends[fbid];
			}
			return null;
		}

		public void Logout()
		{
			logger.Debug("Facebook logout");
			FB.Logout();
			friends = null;
		}

		private void SetInit()
		{
			logger.Debug("Facebook Set Init Called");
			logger.Debug("Facebook UserID: " + FB.UserId);
			logger.Debug("Is Logged In: {0}", FB.IsLoggedIn.ToString());
			logger.Debug("Access Token: " + FB.AccessToken);
			logger.Debug("Access Expiry: {0} ", FB.AccessTokenExpiresAt.ToString());
			if (FB.IsInitialized)
			{
				_initSuccessSignal.Dispatch(this);
			}
			else
			{
				_initFailSignal.Dispatch(this);
			}
		}

		private void AuthCallback(FBResult result)
		{
			localPersistence.PutData("SocialInProgress", "False");
			logger.Debug(result.Text);
			if (FB.IsLoggedIn)
			{
				logger.Debug(FB.UserId);
				logger.Debug("********** FB **********");
				logger.Debug(FB.UserId);
				logger.Debug(FB.AccessToken);
				_loginSuccessSignal.Dispatch(this);
				GetUserInfo();
				success.AddListener(downloadFriendsSuccess);
				failure.AddListener(downloadFriendsFailure);
				DownloadFriends(100, success, failure);
			}
			else
			{
				logger.Debug("User cancelled login");
				logger.Debug("Error result = " + result.Error);
				FB.Init(SetInit, global::Kampai.Util.GameConstants.Facebook.APP_ID);
				localPersistence.PutData("SocialInProgress", "False");
				_loginFailureSignal.Dispatch(this);
			}
		}

		public void updateKillSwitchFlag()
		{
			killSwitchFlag = configurationsService.isKillSwitchOn(global::Kampai.Game.KillSwitch.FACEBOOK);
		}

		public void SendLoginTelemetry(string loginLocation)
		{
			telemetryService.Send_Telemetry_EVT_EBISU_LOGIN_FACEBOOK(loginLocation, LoginSource);
		}
	}
}
