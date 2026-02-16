namespace Kampai.Game
{
	public class KevinFrolicsCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.KevinWanderSignal kevinWanderSignal { get; set; }

		[Inject]
		public global::Kampai.Common.IRandomService randomService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.KevinCharacter firstInstanceByDefintion = playerService.GetFirstInstanceByDefintion<global::Kampai.Game.KevinCharacter, global::Kampai.Game.KevinCharacterDefinition>();
			if (firstInstanceByDefintion != null)
			{
				global::Kampai.Game.LocationIncidentalAnimationDefinition locationIncidentalAnimationDefinition = NextAnimation(firstInstanceByDefintion, firstInstanceByDefintion.CurrentFrolicLocation);
				if (locationIncidentalAnimationDefinition != null)
				{
					kevinWanderSignal.Dispatch(locationIncidentalAnimationDefinition);
				}
				else
				{
					logger.Error("Cannot find an animation for kevin");
				}
			}
			else
			{
				logger.Error("Cannot find kevin");
			}
		}

		private global::Kampai.Game.LocationIncidentalAnimationDefinition NextAnimation(global::Kampai.Game.KevinCharacter kevin, global::Kampai.Game.FloatLocation current)
		{
			global::Kampai.Game.Transaction.WeightedDefinition wanderWeightedDeck = kevin.Definition.WanderWeightedDeck;
			global::Kampai.Game.Transaction.WeightedInstance weightedInstance = playerService.GetWeightedInstance(wanderWeightedDeck.ID, wanderWeightedDeck);
			int num = 10;
			global::Kampai.Game.LocationIncidentalAnimationDefinition locationIncidentalAnimationDefinition = null;
			do
			{
				global::Kampai.Util.QuantityItem quantityItem = weightedInstance.NextPick(randomService);
				foreach (global::Kampai.Game.LocationIncidentalAnimationDefinition wanderAnimation in kevin.Definition.WanderAnimations)
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
