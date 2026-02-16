namespace Kampai.Game.Mignette
{
	public class SetCurrentMignetteScore : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public int newScore { get; set; }

		[Inject]
		public global::Kampai.Game.Mignette.MignetteGameModel mignetteGameModel { get; set; }

		[Inject]
		public global::Kampai.Game.Mignette.MignetteScoreUpdatedSignal mignetteScoreUpdatedSignal { get; set; }

		public override void Execute()
		{
			mignetteGameModel.CurrentGameScore = newScore;
			mignetteScoreUpdatedSignal.Dispatch(newScore);
		}
	}
}
