namespace Kampai.UI.View
{
	public class SocialPartyRewardMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.SocialPartyRewardView>
	{
		private global::Kampai.Game.Transaction.TransactionDefinition reward;

		public global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse> claimRewardSignal = new global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse>();

		private int rewardID;

		private static bool isRegistered;

		private int eventID;

		private global::Kampai.Game.TimedSocialEventDefinition timedSocialEventDefinition;

		[Inject]
		public global::Kampai.Game.ITimedSocialEventService timedSocialEventService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SocialOrderBoardCompleteSignal orderBoardCompleteSignal { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.UI.View.RemoveWayFinderSignal removeWayFinderSignal { get; set; }

		[Inject]
		public global::Kampai.Common.NetworkConnectionLostSignal networkConnectionLostSignal { get; set; }

		[Inject(global::Kampai.UI.View.UIElement.CAMERA)]
		public global::UnityEngine.Camera uiCamera { get; set; }

		[Inject]
		public global::Kampai.UI.View.SpawnDooberSignal tweenSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSignal { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			soundFXSignal.Dispatch("Play_celebration_confetti_01");
			base.view.collectButton.ClickedSignal.AddListener(CollectButton);
			base.view.OnMenuClose.AddListener(CloseAnimationComplete);
			claimRewardSignal.AddListener(ClaimRewardResponse);
			isRegistered = true;
		}

		public override void OnRemove()
		{
			base.OnRemove();
			reward = null;
			base.view.collectButton.ClickedSignal.RemoveListener(CollectButton);
			base.view.OnMenuClose.RemoveListener(CloseAnimationComplete);
			claimRewardSignal.RemoveListener(ClaimRewardResponse);
			isRegistered = false;
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			eventID = args.Get<int>();
			timedSocialEventDefinition = timedSocialEventService.GetSocialEvent(eventID);
			rewardID = timedSocialEventDefinition.ID;
			reward = timedSocialEventDefinition.GetReward(definitionService);
			base.view.Init(reward, localService, definitionService, playerService);
			base.view.collectButtonText.text = localService.GetString("socialpartyrewardcollectbutton");
			base.view.content.text = localService.GetString("socialpartyirewarddetails");
			base.view.title.text = localService.GetString("socialpartyrewardtitle");
		}

		protected override void Close()
		{
			CollectButton();
		}

		public void CollectButton()
		{
			global::UnityEngine.UI.Button component = base.view.collectButton.GetComponent<global::UnityEngine.UI.Button>();
			component.interactable = false;
			if (reward != null)
			{
				playerService.RunEntireTransaction(reward, global::Kampai.Game.TransactionTarget.NO_VISUAL, CollectTransactionCallback);
			}
			else
			{
				logger.Info("Reward is null, nothing to do.");
			}
		}

		public void CollectTransactionCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			if (!pct.Success)
			{
				logger.Error("CollectTransactionCallback PendingCurrencyTransaction was a failure.");
			}
			if (timedSocialEventDefinition == null)
			{
				logger.Info("SocialPartyRewardMediator: No current social event, reward will not be claimed.");
				CloseMenu();
				return;
			}
			global::Kampai.Game.SocialTeamResponse socialEventStateCached = timedSocialEventService.GetSocialEventStateCached(eventID);
			if (socialEventStateCached == null)
			{
				logger.Error("SocialPartyRewardMediator: Failed to get cached event state for social event definition id {0}.", eventID);
				CloseMenu();
			}
			else if (socialEventStateCached.Team == null)
			{
				logger.Error("SocialPartyRewardMediator: Team is null.");
				CloseMenu();
			}
			else
			{
				DooberUtil.CheckForTween(base.view.transactionDefinition, base.view.viewList, true, uiCamera, tweenSignal, definitionService);
				timedSocialEventService.ClaimReward(eventID, timedSocialEventService.GetSocialEventStateCached(eventID).Team.ID, claimRewardSignal);
			}
		}

		public void ClaimRewardResponse(global::Kampai.Game.SocialTeamResponse response, global::Kampai.Game.ErrorResponse error)
		{
			if (error != null)
			{
				networkConnectionLostSignal.Dispatch();
				return;
			}
			if (response != null && response.Team != null && response.Team.Members != null)
			{
				telemetryService.Send_Telemetry_EVT_SOCIAL_EVENT_COMPLETION(response.Team.Members.Count);
			}
			CloseMenu();
			if (timedSocialEventService.GetCurrentSocialEvent() == null || timedSocialEventService.GetCurrentSocialEvent().ID == rewardID)
			{
				global::Kampai.Game.StageBuilding firstInstanceByDefinitionId = playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.StageBuilding>(3054);
				if (firstInstanceByDefinitionId != null)
				{
					removeWayFinderSignal.Dispatch(firstInstanceByDefinitionId.ID);
				}
			}
			orderBoardCompleteSignal.Dispatch();
		}

		private void CloseMenu()
		{
			base.view.Close();
		}

		public void CloseAnimationComplete()
		{
			if (isRegistered)
			{
				hideSignal.Dispatch("SocialReward");
				guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "popup_SocialPartyReward");
				isRegistered = false;
			}
		}
	}
}
