namespace Kampai.UI.View
{
	public class SocialPartyFillOrderMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.SocialPartyFillOrderView>
	{
		private global::Kampai.Game.FBUser[] friends;

		private global::Kampai.Game.SocialTeam team;

		private global::System.DateTime finishTime;

		private bool bShowFinishMenu;

		private int count;

		private static int MAX_FRIENDS = 4;

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowSocialPartyFillOrderButtonSignal showPartyFillOrderButton { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowSocialPartyFillOrderProfileButtonSignal showPartyFillOrderProfile { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject(global::Kampai.Game.SocialServices.FACEBOOK)]
		public global::Kampai.Game.ISocialService facebookService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimedSocialEventService timedSocialEventService { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowSocialPartyStartSignal showSocialPartyStartSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowSocialPartyInviteAlertSignal showSocialPartyInviteAlertSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowSocialPartyEventEndSignal showSocialPartyEventEndSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal globalSFX { get; set; }

		[Inject]
		public global::Kampai.Common.ICoppaService coppaService { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SocialPartyFillOrderSetupUISignal socialPartyFillOrderSetupUISignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SocialPartyFillOrderButtonMediatorUpdateSignal socialPartyFillOrderButtonMediatorUpdateSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SocialPartyFillOrderProfileButtonMediatorUpdateSignal socialPartyFillOrderProfileButtonMediatorUpdateSignal { get; set; }

		[Inject]
		public global::Kampai.Common.NetworkConnectionLostSignal networkConnectionLostSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideHUDAndIconsSignal hideHUDSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SocialLoginSuccessSignal loginSuccess { get; set; }

		public override void OnRemove()
		{
			base.OnRemove();
			base.view.leaveTeamButton.ClickedSignal.RemoveListener(LeaveTeamButton);
			base.view.messageAlertButton.ClickedSignal.RemoveListener(MessageAlertButton);
			base.view.teamPanelButton.ClickedSignal.RemoveListener(OpenTeam);
			base.view.closeTeamButton.ClickedSignal.RemoveListener(CloseTeam);
			base.view.OnMenuClose.RemoveListener(CloseAnimationComplete);
			socialPartyFillOrderSetupUISignal.RemoveListener(RefreshUI);
			loginSuccess.RemoveListener(OnLoginSuccess);
			hideHUDSignal.Dispatch(true);
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			bShowFinishMenu = false;
			count = 0;
		}

		public override void OnRegister()
		{
			base.OnRegister();
			base.view.Init();
			base.view.leaveTeamButton.ClickedSignal.AddListener(LeaveTeamButton);
			base.view.messageAlertButton.ClickedSignal.AddListener(MessageAlertButton);
			base.view.teamPanelButton.ClickedSignal.AddListener(OpenTeam);
			base.view.closeTeamButton.ClickedSignal.AddListener(CloseTeam);
			base.view.OnMenuClose.AddListener(CloseAnimationComplete);
			socialPartyFillOrderSetupUISignal.AddListener(RefreshUI);
			loginSuccess.AddListener(OnLoginSuccess);
			base.view.messageAlertButton.gameObject.SetActive(false);
			LoadTeam();
			hideHUDSignal.Dispatch(false);
		}

		private void OnLoginSuccess(global::Kampai.Game.ISocialService socialService)
		{
			if (socialService.type == global::Kampai.Game.SocialServices.FACEBOOK)
			{
				LoadTeam();
			}
		}

		private void LoadTeam()
		{
			global::Kampai.Game.TimedSocialEventDefinition currentSocialEvent = timedSocialEventService.GetCurrentSocialEvent();
			global::Kampai.Game.SocialTeamResponse socialEventStateCached = timedSocialEventService.GetSocialEventStateCached(currentSocialEvent.ID);
			if (socialEventStateCached != null && socialEventStateCached.Team != null)
			{
				OnGetTeamSuccess(socialEventStateCached, null);
				return;
			}
			global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse>();
			signal.AddListener(OnGetTeamSuccess);
			timedSocialEventService.GetSocialEventState(timedSocialEventService.GetCurrentSocialEvent().ID, signal);
		}

		private void OpenTeam()
		{
			base.view.OpenTeam();
		}

		private void CloseTeam()
		{
			base.view.CloseTeam();
		}

		public void OnGetTeamSuccess(global::Kampai.Game.SocialTeamResponse response, global::Kampai.Game.ErrorResponse error)
		{
			if (error != null)
			{
				networkConnectionLostSignal.Dispatch();
			}
			else if (response != null && response.Team != null)
			{
				team = response.Team;
				if (response.Team != null && response.Team.Members != null && response.Team.Members.Count > 1)
				{
					base.view.leaveTeamButton.gameObject.SetActive(true);
				}
				else
				{
					base.view.leaveTeamButton.gameObject.SetActive(false);
				}
				if (response.UserEvent.Invitations != null && response.UserEvent.Invitations.Count > 0)
				{
					base.view.messageAlertButton.gameObject.SetActive(true);
				}
				else
				{
					base.view.messageAlertButton.gameObject.SetActive(false);
				}
				GetFBFriendsInTeam();
				SetupUI();
			}
		}

		public void GetFBFriendsInTeam()
		{
			global::Kampai.Game.FacebookService facebookService = this.facebookService as global::Kampai.Game.FacebookService;
			if (!facebookService.isLoggedIn)
			{
				return;
			}
			friends = new global::Kampai.Game.FBUser[MAX_FRIENDS];
			if (team == null || team.Members == null)
			{
				return;
			}
			int num = 0;
			int num2 = ((team.Members.Count <= MAX_FRIENDS) ? team.Members.Count : MAX_FRIENDS);
			for (int i = 0; i < num2; i++)
			{
				global::Kampai.Game.UserIdentity userIdentity = team.Members[i];
				global::Kampai.Game.FBUser fBUser = null;
				if (userIdentity != null && userIdentity.ExternalID != null)
				{
					fBUser = facebookService.GetFriend(userIdentity.ExternalID);
				}
				if (fBUser != null)
				{
					fBUser.DownloadTexture();
					friends[num] = fBUser;
					num++;
				}
			}
		}

		private SocialPartyFillOrderButtonMediator.SocialPartyFillOrderButtonMediatorData GetData(global::Kampai.Game.SocialEventOrderDefinition orderDefinition, int index)
		{
			global::Kampai.Game.SocialOrderProgress progress = (team.OrderProgress as global::System.Collections.Generic.List<global::Kampai.Game.SocialOrderProgress>).Find((global::Kampai.Game.SocialOrderProgress p) => p.OrderId == orderDefinition.OrderID);
			return new SocialPartyFillOrderButtonMediator.SocialPartyFillOrderButtonMediatorData
			{
				team = team,
				progress = progress,
				orderDefintion = orderDefinition,
				parent = base.view.SocialFillOrderButtonContainer,
				index = index
			};
		}

		public void RefreshUI(global::Kampai.Game.SocialTeam socialTeam)
		{
			int num = 0;
			if (socialTeam != null)
			{
				team = socialTeam;
			}
			foreach (global::Kampai.Game.SocialEventOrderDefinition order in team.Definition.Orders)
			{
				SocialPartyFillOrderButtonMediator.SocialPartyFillOrderButtonMediatorData data = GetData(order, num);
				socialPartyFillOrderButtonMediatorUpdateSignal.Dispatch(data);
				num++;
			}
			UpdateProgress();
		}

		public void SetupUI()
		{
			if (team != null)
			{
				int num = 0;
				foreach (global::Kampai.Game.SocialEventOrderDefinition order in team.Definition.Orders)
				{
					SocialPartyFillOrderButtonMediator.SocialPartyFillOrderButtonMediatorData data = GetData(order, num);
					showPartyFillOrderButton.Dispatch(data, num);
					num++;
				}
				if (!coppaService.Restricted())
				{
					int num2 = team.Members.Count;
					int num3 = 0;
					foreach (global::Kampai.Game.UserIdentity member in team.Members)
					{
						SocialPartyFillOrderProfileButtonMediator.SocialPartyFillOrderProfileButtonMediatorData type = new SocialPartyFillOrderProfileButtonMediator.SocialPartyFillOrderProfileButtonMediatorData
						{
							identity = member,
							parent = base.view.SocialFillTeamPanel,
							index = num3
						};
						showPartyFillOrderProfile.Dispatch(type);
						num3++;
					}
					for (int i = num2; i < 4; i++)
					{
						SocialPartyFillOrderProfileButtonMediator.SocialPartyFillOrderProfileButtonMediatorData type2 = new SocialPartyFillOrderProfileButtonMediator.SocialPartyFillOrderProfileButtonMediatorData
						{
							identity = null,
							parent = base.view.SocialFillTeamPanel,
							index = num3
						};
						showPartyFillOrderProfile.Dispatch(type2);
						num3++;
					}
				}
				long num4 = timedSocialEventService.GetCurrentSocialEvent().FinishTime;
				finishTime = new global::System.DateTime(1970, 1, 1, 0, 0, 0, 0, global::System.DateTimeKind.Utc);
				finishTime = finishTime.AddSeconds(num4).ToLocalTime();
				UpdateTime();
				UpdateProgress();
			}
			setupText();
		}

		private void UpdateProgress()
		{
			int num = team.Definition.Orders.Count;
			int num2 = team.OrderProgress.Count;
			base.view.progressBar.transform.localScale = new global::UnityEngine.Vector3((float)num2 / (float)num, 1f, 1f);
			if (num == num2)
			{
				base.view.ordersRemaining.text = localService.GetString("socialpartyfillordercompleted");
			}
			else
			{
				base.view.ordersRemaining.text = string.Format("{0} / {1}", num2, num);
			}
		}

		private void setupText()
		{
			base.view.leaveTeamButtonText.text = localService.GetString("socialpartyfillorderleavebutton");
			base.view.teamTitle.text = localService.GetString("socialpartyfillorderteamtitle");
			base.view.teamOrderBoardText.text = localService.GetString("socialpartyfillorderboard");
			base.view.descriptionText.text = localService.GetString("socialpartyfillorderdescription");
			base.view.questTitle.text = localService.GetString("Rewards");
			base.view.teamButtonText.text = localService.GetString("socialpartyfillorderteamtitle");
			global::Kampai.Game.TimedSocialEventDefinition currentSocialEvent = timedSocialEventService.GetCurrentSocialEvent();
			global::Kampai.Game.Transaction.TransactionDefinition reward = currentSocialEvent.GetReward(definitionService);
			foreach (global::Kampai.Util.QuantityItem output in reward.Outputs)
			{
				if (output.ID == 0)
				{
					base.view.grindRewardText.text = output.Quantity.ToString();
				}
				else if (output.ID == 1)
				{
					base.view.premiumRewardText.text = output.Quantity.ToString();
				}
			}
		}

		public void UpdateTime()
		{
			count++;
			global::System.DateTime dateTime = new global::System.DateTime(1970, 1, 1, 0, 0, 0, 0, global::System.DateTimeKind.Utc).AddSeconds(timeService.GameTimeSeconds()).ToLocalTime();
			global::System.TimeSpan timeSpan = finishTime.Subtract(dateTime);
			if (dateTime < finishTime)
			{
				base.view.timeRemaining.text = string.Format("{0:00}:{1:00}:{2:00}:{3:00}", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
				return;
			}
			base.view.timeRemaining.text = localService.GetString("socialpartyfillordereventfinished");
			if (!bShowFinishMenu && count > 10)
			{
				bShowFinishMenu = true;
				UnloadFillOrderScreen();
				showSocialPartyEventEndSignal.Dispatch();
			}
		}

		private void UnloadFillOrderScreen()
		{
			hideSignal.Dispatch("SocialSkrim");
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "SocialPartyFillOrderScreen");
		}

		public void Update()
		{
			UpdateTime();
		}

		public void LeaveTeamResponse(bool bLeave)
		{
			if (bLeave)
			{
				global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse>();
				signal.AddListener(LeaveSocialTeamServerResponse);
				timedSocialEventService.LeaveSocialTeam(team.SocialEventId, team.ID, signal);
			}
		}

		public void LeaveSocialTeamServerResponse(global::Kampai.Game.SocialTeamResponse newTeam, global::Kampai.Game.ErrorResponse error)
		{
			if (error != null)
			{
				networkConnectionLostSignal.Dispatch();
				return;
			}
			UnloadFillOrderScreen();
			if (newTeam.Team != null)
			{
				team = newTeam.Team;
				SetupUI();
				return;
			}
			global::Kampai.Game.SocialTeamResponse socialEventStateCached = timedSocialEventService.GetSocialEventStateCached(timedSocialEventService.GetCurrentSocialEvent().ID);
			if (socialEventStateCached.UserEvent != null && socialEventStateCached.UserEvent.Invitations != null && socialEventStateCached.UserEvent.Invitations.Count > 0 && facebookService.isLoggedIn)
			{
				showSocialPartyInviteAlertSignal.Dispatch();
			}
			else
			{
				showSocialPartyStartSignal.Dispatch();
			}
		}

		public void MessageAlertButton()
		{
			globalSFX.Dispatch("Play_button_click_01");
			UnloadFillOrderScreen();
			showSocialPartyInviteAlertSignal.Dispatch();
		}

		public void LeaveTeamButton()
		{
			global::strange.extensions.signal.impl.Signal<bool> signal = new global::strange.extensions.signal.impl.Signal<bool>();
			signal.AddListener(LeaveTeamResponse);
			global::Kampai.Util.Tuple<string, string, string, global::strange.extensions.signal.impl.Signal<bool>> type = new global::Kampai.Util.Tuple<string, string, string, global::strange.extensions.signal.impl.Signal<bool>>("socialpartyleaveteamconfirmationtitle", "socialpartyleaveteamconfirmationdescription", "img_char_Min_FeedbackChecklist01", signal);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.DisplayConfirmationSignal>().Dispatch(type);
		}

		public void CloseAnimationComplete()
		{
			UnloadFillOrderScreen();
		}

		public void QuitButton()
		{
			globalSFX.Dispatch("Play_button_click_01");
			base.view.Close();
		}

		protected override void Close()
		{
			QuitButton();
		}
	}
}
