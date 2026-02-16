namespace Kampai.UI.View
{
	public class SocialPartyInviteAlertMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.SocialPartyInviteAlertView>
	{
		private int pageNumber;

		private global::System.Collections.Generic.IList<global::Kampai.Game.SocialEventInvitation> friendInvites;

		public global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse> acceptSignal = new global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse>();

		public global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse> declineSignal = new global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse>();

		private global::Kampai.Game.SocialTeam cachedTeam;

		[Inject]
		public global::Kampai.Game.ITimedSocialEventService timedSocialEventService { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject(global::Kampai.Game.SocialServices.FACEBOOK)]
		public global::Kampai.Game.ISocialService facebookService { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowSocialPartyFillOrderSignal showPartyFillOrderSignal { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowSocialPartyStartSignal showSocialPartyStartSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowSocialPartyRewardSignal socialPartyRewardSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal globalSFX { get; set; }

		[Inject]
		public global::Kampai.Common.NetworkConnectionLostSignal networkConnectionLostSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSignal { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			base.view.Init();
			acceptSignal.AddListener(AcceptResponse);
			declineSignal.AddListener(DeclineResponse);
			base.view.acceptButton.ClickedSignal.AddListener(AcceptButton);
			base.view.declineButton.ClickedSignal.AddListener(DeclineButton);
			base.view.nextButton.ClickedSignal.AddListener(NextButton);
			base.view.previousButton.ClickedSignal.AddListener(PreviousButton);
			base.view.closeButton.ClickedSignal.AddListener(CloseButton);
			base.view.OnMenuClose.AddListener(CloseAnimationComplete);
			base.view.acceptButtonText.text = localService.GetString("AcceptButtonText");
			base.view.declineButtonText.text = localService.GetString("DeclineButtonText");
			base.view.title.text = localService.GetString("socialpartyinvitealerttitle");
		}

		public override void OnRemove()
		{
			base.OnRemove();
			friendInvites = null;
			acceptSignal.RemoveListener(AcceptResponse);
			declineSignal.RemoveListener(DeclineResponse);
			base.view.acceptButton.ClickedSignal.RemoveListener(AcceptButton);
			base.view.declineButton.ClickedSignal.RemoveListener(DeclineButton);
			base.view.nextButton.ClickedSignal.RemoveListener(NextButton);
			base.view.previousButton.ClickedSignal.RemoveListener(PreviousButton);
			base.view.closeButton.ClickedSignal.RemoveListener(CloseButton);
			base.view.OnMenuClose.RemoveListener(CloseAnimationComplete);
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			pageNumber = 1;
			global::Kampai.Game.SocialTeamResponse socialEventStateCached = timedSocialEventService.GetSocialEventStateCached(timedSocialEventService.GetCurrentSocialEvent().ID);
			cachedTeam = socialEventStateCached.Team;
			friendInvites = socialEventStateCached.UserEvent.Invitations;
			base.view.previousButton.gameObject.SetActive(false);
			base.view.nextButton.gameObject.SetActive((friendInvites.Count > 1) ? true : false);
			UpdateScreenValues();
		}

		private bool IsPlayerAlreadyOnTeam(int num)
		{
			global::Kampai.Game.SocialTeamResponse socialEventStateCached = timedSocialEventService.GetSocialEventStateCached(timedSocialEventService.GetCurrentSocialEvent().ID);
			return socialEventStateCached.Team != null && socialEventStateCached.Team.Members != null && socialEventStateCached.Team.Members.Count > num;
		}

		private void EnableButtons(bool isEnabled)
		{
			global::UnityEngine.UI.Button component = base.view.acceptButton.GetComponent<global::UnityEngine.UI.Button>();
			component.interactable = isEnabled;
			component = base.view.declineButton.GetComponent<global::UnityEngine.UI.Button>();
			component.interactable = isEnabled;
			component = base.view.nextButton.GetComponent<global::UnityEngine.UI.Button>();
			component.interactable = isEnabled;
			component = base.view.previousButton.GetComponent<global::UnityEngine.UI.Button>();
			component.interactable = isEnabled;
			component = base.view.closeButton.GetComponent<global::UnityEngine.UI.Button>();
			component.interactable = isEnabled;
		}

		public void AcceptButton()
		{
			EnableButtons(false);
			if (IsPlayerAlreadyOnTeam(1))
			{
				global::strange.extensions.signal.impl.Signal<bool> signal = new global::strange.extensions.signal.impl.Signal<bool>();
				signal.AddOnce(ConfirmationResponse);
				global::Kampai.Util.Tuple<string, string, string, global::strange.extensions.signal.impl.Signal<bool>> type = new global::Kampai.Util.Tuple<string, string, string, global::strange.extensions.signal.impl.Signal<bool>>("socialpartyjointeamconfirmationtitle", "socialpartyjointeamconfirmationdescription", "img_char_Min_FeedbackChecklist01", signal);
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.DisplayConfirmationSignal>().Dispatch(type);
			}
			else
			{
				AcceptTeam();
			}
		}

		public void AcceptResponse(global::Kampai.Game.SocialTeamResponse response, global::Kampai.Game.ErrorResponse error)
		{
			EnableButtons(true);
			if (error != null)
			{
				networkConnectionLostSignal.Dispatch();
				return;
			}
			if (cachedTeam != null && cachedTeam.Members != null && cachedTeam.Members.Count == 1)
			{
				global::Kampai.Game.SocialTeam team = timedSocialEventService.GetSocialEventStateCached(timedSocialEventService.GetCurrentSocialEvent().ID).Team;
				foreach (global::Kampai.Game.SocialOrderProgress item in cachedTeam.OrderProgress)
				{
					bool flag = false;
					foreach (global::Kampai.Game.SocialOrderProgress item2 in team.OrderProgress)
					{
						if (item.OrderId == item2.OrderId)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse>();
						signal.AddListener(FillOrderComplete);
						timedSocialEventService.FillOrder(team.SocialEventId, team.ID, item.OrderId, signal);
					}
				}
			}
			CloseMenu();
		}

		public void FillOrderComplete(global::Kampai.Game.SocialTeamResponse response, global::Kampai.Game.ErrorResponse error)
		{
			if (error != null)
			{
				networkConnectionLostSignal.Dispatch();
			}
			else if (!response.UserEvent.RewardClaimed && response.Team.OrderProgress.Count == timedSocialEventService.GetCurrentSocialEvent().Orders.Count)
			{
				socialPartyRewardSignal.Dispatch(timedSocialEventService.GetCurrentSocialEvent().ID);
			}
		}

		public void DeclineButton()
		{
			EnableButtons(false);
			global::Kampai.Game.SocialEventInvitation currentFriendInvite = GetCurrentFriendInvite();
			timedSocialEventService.RejectInvitation(currentFriendInvite.EventID, currentFriendInvite.Team.TeamID, declineSignal);
		}

		public void DeclineResponse(global::Kampai.Game.SocialTeamResponse response, global::Kampai.Game.ErrorResponse error)
		{
			EnableButtons(true);
			if (error != null)
			{
				networkConnectionLostSignal.Dispatch();
				return;
			}
			if (response != null && response.UserEvent != null)
			{
				friendInvites = response.UserEvent.Invitations;
			}
			else
			{
				friendInvites = null;
			}
			if (friendInvites == null || friendInvites.Count == 0)
			{
				CloseMenu();
				return;
			}
			pageNumber--;
			NextButton();
		}

		public void ConfirmationResponse(bool bAccept)
		{
			if (bAccept)
			{
				AcceptTeam();
			}
			else
			{
				EnableButtons(true);
			}
		}

		private void AcceptTeam()
		{
			global::Kampai.Game.SocialEventInvitation currentFriendInvite = GetCurrentFriendInvite();
			timedSocialEventService.JoinSocialTeam(currentFriendInvite.EventID, currentFriendInvite.Team.TeamID, acceptSignal);
		}

		public void CloseButton()
		{
			EnableButtons(true);
			CloseMenu();
		}

		public void NextButton()
		{
			pageNumber++;
			base.view.previousButton.gameObject.SetActive(true);
			if (pageNumber >= friendInvites.Count)
			{
				pageNumber = friendInvites.Count;
				base.view.nextButton.gameObject.SetActive(false);
			}
			UpdateScreenValues();
		}

		public void PreviousButton()
		{
			pageNumber--;
			base.view.nextButton.gameObject.SetActive(true);
			if (pageNumber <= 1)
			{
				pageNumber = 1;
				base.view.previousButton.gameObject.SetActive(false);
			}
			UpdateScreenValues();
		}

		private void UpdateScreenValues()
		{
			global::Kampai.Game.SocialEventInvitation currentFriendInvite = GetCurrentFriendInvite();
			global::Kampai.Game.FacebookService facebookService = this.facebookService as global::Kampai.Game.FacebookService;
			global::Kampai.Game.FBDownloadPicture fBDownloadPicture = new global::Kampai.Game.FBDownloadPicture(logger);
			base.view.playerImage.sprite = null;
			StartCoroutine(fBDownloadPicture.GetPicture(currentFriendInvite.inviter.ExternalID, this.facebookService.accessToken, 256, 256, OnFacebookPictureComplete));
			string empty = string.Empty;
			if (facebookService.friends != null)
			{
				if (facebookService.friends.ContainsKey(currentFriendInvite.inviter.ExternalID) && facebookService.friends[currentFriendInvite.inviter.ExternalID] != null)
				{
					empty = facebookService.friends[currentFriendInvite.inviter.ExternalID].name;
				}
				else
				{
					logger.Error("fbService.friends[invitation.inviter.ExternalID] is null");
				}
			}
			base.view.playerName.text = empty;
			base.view.socialPartyDescription.text = empty + localService.GetString("socialpartyinvitealertdescription");
			base.view.recipesFilledDescription.text = currentFriendInvite.Team.CompletedOrdersCount + "/" + timedSocialEventService.GetCurrentSocialEvent().Orders.Count + localService.GetString("socialpartyinviterecipesfilled");
		}

		private void CloseMenu()
		{
			base.view.Close();
		}

		public void CloseAnimationComplete()
		{
			hideSignal.Dispatch("Social Invite");
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "popup_SocialParty_InviteAlert");
			if (IsPlayerAlreadyOnTeam(0))
			{
				showPartyFillOrderSignal.Dispatch();
			}
			else
			{
				showSocialPartyStartSignal.Dispatch();
			}
		}

		protected override void Close()
		{
			CloseButton();
		}

		private global::Kampai.Game.SocialEventInvitation GetCurrentFriendInvite()
		{
			return friendInvites[pageNumber - 1];
		}

		public void OnFacebookPictureComplete(global::UnityEngine.Texture texture, string id)
		{
			global::Kampai.Game.SocialEventInvitation currentFriendInvite = GetCurrentFriendInvite();
			if (texture != null && currentFriendInvite.inviter.ExternalID == id)
			{
				global::UnityEngine.Sprite sprite = global::UnityEngine.Sprite.Create(texture as global::UnityEngine.Texture2D, new global::UnityEngine.Rect(0f, 0f, texture.width, texture.height), new global::UnityEngine.Vector2(0f, 0f));
				base.view.playerImage.sprite = sprite;
			}
		}
	}
}
