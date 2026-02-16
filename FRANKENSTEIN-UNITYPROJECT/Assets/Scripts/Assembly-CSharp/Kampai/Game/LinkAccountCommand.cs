namespace Kampai.Game
{
	public class LinkAccountCommand : global::strange.extensions.command.impl.Command
	{
		public const string ACCOUNT_LINK_ENDPOINT = "/rest/user/{0}/identity";

		[Inject]
		public bool restartOnSuccess { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.ISocialService socialService { get; set; }

		[Inject("game.server.host")]
		public string ServerUrl { get; set; }

		[Inject]
		public global::Kampai.Download.IDownloadService downloadService { get; set; }

		[Inject]
		public global::Kampai.Game.IUserSessionService userSessionService { get; set; }

		[Inject]
		public global::Kampai.Game.SocialLogoutSignal socialLogout { get; set; }

		[Inject]
		public global::Kampai.Game.ReLinkAccountSignal relinkSignal { get; set; }

		[Inject]
		public global::Kampai.Game.LinkAccountSignal linkSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SocialLogoutSignal logoutSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateFacebookStateSignal updateFacebookDialogState { get; set; }

		[Inject]
		public global::Kampai.UI.View.PopupMessageSignal popupMessageSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Game.RegisterUserSignal registerSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ReloadGameSignal reloadGameSignal { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.UserSession userSession = userSessionService.UserSession;
			string userID = userSession.UserID;
			global::Kampai.Game.AccountLinkRequest accountLinkRequest = new global::Kampai.Game.AccountLinkRequest();
			if (socialService.isLoggedIn)
			{
				accountLinkRequest.credentials = socialService.accessToken;
				accountLinkRequest.externalId = socialService.userID;
				accountLinkRequest.identityType = socialService.type.ToString().ToLower();
			}
			global::Kampai.Game.UserIdentity userIdentity = new global::Kampai.Game.UserIdentity();
			userIdentity.ExternalID = socialService.userID;
			userIdentity.Type = (global::Kampai.Game.IdentityType)(int)global::System.Enum.Parse(typeof(global::Kampai.Game.IdentityType), socialService.type.ToString().ToLower());
			userSession.SocialIdentities.Add(userIdentity);
			logger.Debug("attempting to link type {0} for ID {1} ", accountLinkRequest.identityType, accountLinkRequest.externalId);
			global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
			signal.AddListener(OnAccountLinkResponse);
			string uri = ServerUrl + string.Format("/rest/user/{0}/identity", userID);
			global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(uri).WithHeaderParam("user_id", userSession.UserID).WithHeaderParam("session_key", userSession.SessionID).WithContentType("application/json")
				.WithMethod(global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest.METHOD_POST)
				.WithEntity(accountLinkRequest)
				.WithResponseSignal(signal);
			downloadService.Perform(request);
		}

		private global::System.Collections.IEnumerator WaitAFrame(global::System.Action a)
		{
			yield return null;
			a();
		}

		private void OnAccountLinkResponse(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
		{
			string body = response.Body;
			global::Kampai.Game.AccountLinkErrorResponse error = null;
			try
			{
				error = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Kampai.Game.AccountLinkErrorResponse>(body);
			}
			catch (global::Newtonsoft.Json.JsonSerializationException e)
			{
				HandleJsonException(e);
			}
			catch (global::Newtonsoft.Json.JsonReaderException e2)
			{
				HandleJsonException(e2);
			}
			if (response.Success)
			{
				global::Kampai.Game.UserIdentity item = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Kampai.Game.UserIdentity>(body);
				userSessionService.UserSession.SocialIdentities.Add(item);
				if (restartOnSuccess)
				{
					reloadGameSignal.Dispatch();
				}
			}
			else if (error != null && error.error.responseCode == 409)
			{
				if (error.error.details.conflictType.Equals("identity_linked_to_another_user") && error.error.details.conflictIdentityId.Length == 0)
				{
					global::strange.extensions.signal.impl.Signal<bool> signal = new global::strange.extensions.signal.impl.Signal<bool>();
					signal.AddListener(delegate(bool result)
					{
						if (result)
						{
							global::strange.extensions.signal.impl.Signal signal2 = new global::strange.extensions.signal.impl.Signal();
							signal2.AddListener(delegate
							{
								linkSignal.Dispatch(socialService, true);
							});
							userSessionService.setLoginCallback(signal2);
							registerSignal.Dispatch();
						}
						else
						{
							LogoutOnUserCancel();
						}
					});
					global::Kampai.Util.Tuple<string, string, string, global::strange.extensions.signal.impl.Signal<bool>> type = new global::Kampai.Util.Tuple<string, string, string, global::strange.extensions.signal.impl.Signal<bool>>("NewSocialTitle", "NewSocialBody", "img_char_Min_FeedbackChecklist01", signal);
					gameContext.injectionBinder.GetInstance<global::Kampai.Game.QueueConfirmationSignal>().Dispatch(type);
					return;
				}
				logger.Error("Social Account is already linked to an account");
				RemoveSocialIdentity();
				routineRunner.StartCoroutine(WaitAFrame(delegate
				{
					global::strange.extensions.signal.impl.Signal<bool> signal2 = new global::strange.extensions.signal.impl.Signal<bool>();
					signal2.AddListener(delegate(bool result)
					{
						PopUpCallback(result, error.error.details.conflictUserId);
					});
					global::Kampai.Util.Tuple<string, string, string, global::strange.extensions.signal.impl.Signal<bool>> type2 = new global::Kampai.Util.Tuple<string, string, string, global::strange.extensions.signal.impl.Signal<bool>>("SavePopupTitle", "SavePopupBody", "img_char_Min_FeedbackChecklist01", signal2);
					gameContext.injectionBinder.GetInstance<global::Kampai.Game.DisplayConfirmationSignal>().Dispatch(type2);
				}));
				logger.Debug(body);
			}
			else
			{
				RemoveSocialIdentity();
				socialLogout.Dispatch(socialService);
				logger.Error("Error Linking Social Account");
				logger.Debug(body);
			}
		}

		private void LogoutOnUserCancel()
		{
			logoutSignal.Dispatch(socialService);
			if (socialService.type == global::Kampai.Game.SocialServices.GOOGLEPLAY)
			{
				popupMessageSignal.Dispatch(localService.GetString("googleplaylogoutsuccess"));
			}
			updateFacebookDialogState.Dispatch(false);
		}

		private void HandleJsonException(global::System.Exception e)
		{
			logger.Info("OnAccountLinkResponse exception: {0}", e.Message);
		}

		private void PopUpCallback(bool result, string conflictId)
		{
			if (result)
			{
				relinkSignal.Dispatch(socialService, conflictId);
			}
			else
			{
				LogoutOnUserCancel();
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
