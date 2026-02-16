namespace Kampai.Game
{
	public class BobFrolicsCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.BobIdleInTownHallSignal bobIdleInTownHallSignal { get; set; }

		[Inject]
		public global::Kampai.Common.IRandomService randomService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.BobCharacter firstInstanceByDefintion = playerService.GetFirstInstanceByDefintion<global::Kampai.Game.BobCharacter, global::Kampai.Game.BobCharacterDefinition>();
			if (firstInstanceByDefintion != null)
			{
				global::Kampai.Game.LocationIncidentalAnimationDefinition locationIncidentalAnimationDefinition = NextAnimation(firstInstanceByDefintion, firstInstanceByDefintion.CurrentFrolicLocation);
				if (locationIncidentalAnimationDefinition != null)
				{
					bobIdleInTownHallSignal.Dispatch(locationIncidentalAnimationDefinition);
				}
				else
				{
					logger.Error("Cannot find an animation for bob");
				}
			}
			else
			{
				logger.Error("Cannot find bob");
			}
		}

		private global::Kampai.Game.LocationIncidentalAnimationDefinition NextAnimation(global::Kampai.Game.BobCharacter bob, global::Kampai.Game.FloatLocation current)
		{
			global::Kampai.Game.Transaction.WeightedDefinition wanderWeightedDeck = bob.Definition.WanderWeightedDeck;
			global::Kampai.Game.Transaction.WeightedInstance weightedInstance = playerService.GetWeightedInstance(wanderWeightedDeck.ID, wanderWeightedDeck);
			int num = 10;
			global::Kampai.Game.LocationIncidentalAnimationDefinition locationIncidentalAnimationDefinition = null;
			do
			{
				global::Kampai.Util.QuantityItem quantityItem = weightedInstance.NextPick(randomService);
				foreach (global::Kampai.Game.LocationIncidentalAnimationDefinition wanderAnimation in bob.Definition.WanderAnimations)
				{
					if (wanderAnimation.ID == quantityItem.ID)
					{
						locationIncidentalAnimationDefinition = wanderAnimation;
					}
				}
			}
			while (num-- > 0 && locationIncidentalAnimationDefinition.Location.Equals(current));
			if (locationIncidentalAnimationDefinition == null)
			{
				logger.Error("Weighted deck {0} has illegal location animation defs", wanderWeightedDeck.ID);
			}
			return locationIncidentalAnimationDefinition;
		}
	}
}
