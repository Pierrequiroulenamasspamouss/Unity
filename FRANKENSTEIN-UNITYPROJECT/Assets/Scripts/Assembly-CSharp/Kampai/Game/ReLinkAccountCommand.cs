namespace Kampai.Game
{
	public class ReLinkAccountCommand : global::strange.extensions.command.impl.Command
	{
		public const string ACCOUNT_LINK_ENDPOINT = "/rest/user/{0}/identity/{1}";

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.ISocialService socialService { get; set; }

		[Inject]
		public string toUserId { get; set; }

		[Inject("game.server.host")]
		public string ServerUrl { get; set; }

		[Inject]
		public global::Kampai.Download.IDownloadService downloadService { get; set; }

		[Inject]
		public global::Kampai.Game.IUserSessionService userSessionService { get; set; }

		[Inject]
		public ILocalPersistanceService LocalPersistService { get; set; }

		[Inject]
		public IEncryptionService encryptionService { get; set; }

		[Inject]
		public global::Kampai.Main.ReloadGameSignal reloadGameSiganl { get; set; }

		[Inject]
		public global::Kampai.Game.ITimedSocialEventService socialEventService { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.UserSession userSession = userSessionService.UserSession;
			string arg = global::UnityEngine.WWW.EscapeURL(userSession.UserID);
			string plainText = LocalPersistService.GetData("AnonymousID");
			encryptionService.TryDecrypt(plainText, "Kampai!", out plainText);
			string arg2 = global::UnityEngine.WWW.EscapeURL(plainText);
			global::Kampai.Game.AccountReLinkRequest accountReLinkRequest = new global::Kampai.Game.AccountReLinkRequest();
			if (socialService.isLoggedIn)
			{
				accountReLinkRequest.credentials = socialService.accessToken;
				accountReLinkRequest.externalId = socialService.userID;
				accountReLinkRequest.identityType = socialService.type.ToString().ToLower();
			}
			global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
			signal.AddListener(OnAccountLinkResponse);
			accountReLinkRequest.toUserId = toUserId;
			string uri = ServerUrl + string.Format("/rest/user/{0}/identity/{1}", arg, arg2);
			global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(uri).WithHeaderParam("user_id", userSession.UserID).WithHeaderParam("session_key", userSession.SessionID).WithContentType("application/json")
				.WithMethod(global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest.METHOD_POST)
				.WithEntity(accountReLinkRequest)
				.WithResponseSignal(signal);
			downloadService.Perform(request);
		}

		private void OnAccountLinkResponse(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
		{
			string body = response.Body;
			int code = response.Code;
			if (response.Success)
			{
				logger.Debug("Relink Success: {0}", body);
				global::Kampai.Game.UserIdentity userIdentity = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Kampai.Game.UserIdentity>(body);
				LocalPersistService.DeleteKey("freezeTime");
				socialEventService.ClearCache();
				LocalPersistService.PutData("UserID", userIdentity.UserID);
				LocalPersistService.PutData("LoadMode", "externalLogin");
				userSessionService.UserSession.UserID = userIdentity.UserID;
				userSessionService.UserSession.SocialIdentities.Add(userIdentity);
				reloadGameSiganl.Dispatch();
			}
			else if (code == 409)
			{
				logger.Error("Social Account is already linked to an account");
				RemoveSocialIdentity();
				logger.Debug(body);
			}
			else
			{
				RemoveSocialIdentity();
				logger.Error("Error ReLinking Social Account");
				logger.Debug(body);
			}
		}

		private void RemoveSocialIdentity()
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.UserIdentity> socialIdentities = userSessionService.UserSession.SocialIdentities;
			for (int i = 0; i < socialIdentities.Count; i++)
			{
				if (socialIdentities[i].ExternalID == socialService.userID)
				{
					socialIdentities.RemoveAt(i);
					break;
				}
			}
		}
	}
}
