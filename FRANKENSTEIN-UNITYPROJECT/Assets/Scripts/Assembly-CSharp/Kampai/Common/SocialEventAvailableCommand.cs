namespace Kampai.Common
{
	public class SocialEventAvailableCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public bool addStuartToStage { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.Game.ITimedSocialEventService timedSocialEventService { get; set; }

		[Inject]
		public global::Kampai.Game.OpenStageBuildingSignal openStageBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SocialEventResponseSignal socialEventResponseSignal { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.StageBuilding firstInstanceByDefinitionId = playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.StageBuilding>(3054);
			global::Kampai.Game.BuildingState state = firstInstanceByDefinitionId.State;
			if (state == global::Kampai.Game.BuildingState.Inactive || state == global::Kampai.Game.BuildingState.Construction || state == global::Kampai.Game.BuildingState.Disabled || state == global::Kampai.Game.BuildingState.Broken || state == global::Kampai.Game.BuildingState.Complete || state == global::Kampai.Game.BuildingState.Inaccessible)
			{
				return;
			}
			global::Kampai.Game.TimedSocialEventDefinition currentSocialEvent = timedSocialEventService.GetCurrentSocialEvent();
			if (currentSocialEvent != null)
			{
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.SocialEventWayfinderSignal>().Dispatch();
				if (addStuartToStage)
				{
					global::Kampai.Game.SocialTeamResponse socialEventStateCached = timedSocialEventService.GetSocialEventStateCached(currentSocialEvent.ID);
					StuartIdle(socialEventStateCached);
					if (socialEventStateCached == null)
					{
						socialEventResponseSignal.AddOnce(SocialTeamResponse);
					}
				}
			}
			else
			{
				global::System.Collections.Generic.IList<int> pastEventsWithUnclaimedReward = timedSocialEventService.GetPastEventsWithUnclaimedReward();
				if (pastEventsWithUnclaimedReward.Count > 0)
				{
					gameContext.injectionBinder.GetInstance<global::Kampai.Game.SocialEventWayfinderSignal>().Dispatch();
				}
				if (addStuartToStage)
				{
					gameContext.injectionBinder.GetInstance<global::Kampai.Game.AddStuartToStageSignal>().Dispatch(global::Kampai.Game.StuartStageAnimationType.IDLEOFFSTAGE);
				}
			}
		}

		private void SocialTeamResponse(global::Kampai.Game.SocialTeamResponse socialTeamResponse, global::Kampai.Game.ErrorResponse errorResponse)
		{
			if (socialTeamResponse == null)
			{
				return;
			}
			global::Kampai.Game.SocialTeam team = socialTeamResponse.Team;
			if (team == null)
			{
				return;
			}
			global::Kampai.Game.TimedSocialEventDefinition definition = team.Definition;
			if (definition != null)
			{
				global::Kampai.Game.TimedSocialEventDefinition currentSocialEvent = timedSocialEventService.GetCurrentSocialEvent();
				if (currentSocialEvent.ID == definition.ID)
				{
					StuartIdle(socialTeamResponse);
				}
			}
		}

		private void StuartIdle(global::Kampai.Game.SocialTeamResponse socialTeamResponse)
		{
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.AddStuartToStageSignal>().Dispatch(global::Kampai.Game.StuartStageAnimationType.IDLEOFFSTAGE);
			if (socialTeamResponse == null)
			{
				return;
			}
			global::Kampai.Game.TimedSocialEventDefinition currentSocialEvent = timedSocialEventService.GetCurrentSocialEvent();
			if (currentSocialEvent != null)
			{
				global::Kampai.Game.SocialTeam team = socialTeamResponse.Team;
				global::Kampai.Game.SocialTeamUserEvent userEvent = socialTeamResponse.UserEvent;
				if (userEvent == null || !userEvent.RewardClaimed || team.OrderProgress == null || team.OrderProgress.Count < currentSocialEvent.Orders.Count)
				{
					gameContext.injectionBinder.GetInstance<global::Kampai.Game.AddStuartToStageSignal>().Dispatch(global::Kampai.Game.StuartStageAnimationType.IDLEONSTAGE);
				}
			}
		}
	}
}
