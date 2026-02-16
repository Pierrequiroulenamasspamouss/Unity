namespace Kampai.Game.View
{
	public class OpenStageBuildingCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.StageBuilding building { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingChangeStateSignal stateChangeSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.ITimedSocialEventService timedSocialEventService { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowSocialPartyRewardSignal showRewardSignal { get; set; }

		[Inject(global::Kampai.UI.View.UIElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable uiContext { get; set; }

		[Inject(global::Kampai.Game.SocialServices.FACEBOOK)]
		public global::Kampai.Game.ISocialService facebookService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoMoveToBuildingSignal moveToBuildingSignal { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService prestigeService { get; set; }

		[Inject]
		public global::Kampai.Game.StageService stageService { get; set; }

		[Inject]
		public global::Kampai.Common.NetworkConnectionLostSignal networkConnectionLostSignal { get; set; }

		[Inject]
		public global::Kampai.Common.ICoppaService coppaService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localeService { get; set; }

		public override void Execute()
		{
			stateChangeSignal.Dispatch(building.ID, global::Kampai.Game.BuildingState.Working);
			global::Kampai.Game.StuartCharacter firstInstanceByDefinitionId = playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.StuartCharacter>(70001);
			if (firstInstanceByDefinitionId == null)
			{
				return;
			}
			if (playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID) < building.Definition.SocialEventMinimumLevel)
			{
				string aspirationalMessageSocialEvent = building.Definition.AspirationalMessageSocialEvent;
				uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.PopupMessageSignal>().Dispatch(localeService.GetString(aspirationalMessageSocialEvent, building.Definition.SocialEventMinimumLevel));
				return;
			}
			global::Kampai.Game.Prestige prestigeFromMinionInstance = prestigeService.GetPrestigeFromMinionInstance(firstInstanceByDefinitionId);
			if (prestigeFromMinionInstance.state == global::Kampai.Game.PrestigeState.Taskable || prestigeFromMinionInstance.CurrentPrestigeLevel > 0)
			{
				OpenGUI();
			}
		}

		private void OpenGUI()
		{
			if (timedSocialEventService.GetCurrentSocialEvent() != null)
			{
				global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse>();
				signal.AddListener(OnGetSocialEventStateResponse);
				timedSocialEventService.GetSocialEventState(timedSocialEventService.GetCurrentSocialEvent().ID, signal);
				return;
			}
			global::System.Collections.Generic.IList<int> pastEventsWithUnclaimedReward = timedSocialEventService.GetPastEventsWithUnclaimedReward();
			if (pastEventsWithUnclaimedReward.Count > 0)
			{
				showRewardSignal.Dispatch(pastEventsWithUnclaimedReward[0]);
			}
			else
			{
				uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.ShowSocialPartyNoEventSignal>().Dispatch();
			}
		}

		public void OnGetSocialEventStateResponse(global::Kampai.Game.SocialTeamResponse response, global::Kampai.Game.ErrorResponse error)
		{
			if (error != null)
			{
				networkConnectionLostSignal.Dispatch();
				return;
			}
			global::Kampai.Game.SocialTeamUserEvent userEvent = response.UserEvent;
			global::Kampai.Game.SocialTeam team = response.Team;
			global::System.Collections.Generic.IList<int> pastEventsWithUnclaimedReward = timedSocialEventService.GetPastEventsWithUnclaimedReward();
			if (pastEventsWithUnclaimedReward.Count > 0)
			{
				showRewardSignal.Dispatch(pastEventsWithUnclaimedReward[0]);
			}
			else if (userEvent != null && !userEvent.RewardClaimed && team != null && team.OrderProgress != null && team.OrderProgress.Count == timedSocialEventService.GetCurrentSocialEvent().Orders.Count)
			{
				showRewardSignal.Dispatch(timedSocialEventService.GetCurrentSocialEvent().ID);
			}
			else if (team != null)
			{
				if (userEvent != null && userEvent.RewardClaimed && team.OrderProgress != null && team.OrderProgress.Count == timedSocialEventService.GetCurrentSocialEvent().Orders.Count)
				{
					uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.ShowSocialPartyEventCompletedSignal>().Dispatch();
				}
				else
				{
					uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.ShowSocialPartyFillOrderSignal>().Dispatch();
				}
			}
			else
			{
				CheckForFriendRequests(userEvent);
			}
		}

		private void CheckForFriendRequests(global::Kampai.Game.SocialTeamUserEvent userEvent)
		{
			if (facebookService.isLoggedIn && userEvent != null && userEvent.Invitations != null && userEvent.Invitations.Count > 0)
			{
				uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.ShowSocialPartyInviteAlertSignal>().Dispatch();
			}
			else
			{
				SocialEventStart();
			}
		}

		private void SocialEventStart()
		{
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.BuildingZoomSignal>().Dispatch(new global::Kampai.Game.BuildingZoomSettings(global::Kampai.Game.ZoomType.IN, global::Kampai.Game.BuildingZoomType.STAGE, ZoomComplete));
		}

		private void ZoomComplete()
		{
			uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.ShowSocialPartyStartSignal>().Dispatch();
		}
	}
}
