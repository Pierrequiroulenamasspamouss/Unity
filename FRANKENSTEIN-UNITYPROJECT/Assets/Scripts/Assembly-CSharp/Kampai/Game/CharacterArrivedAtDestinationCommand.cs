namespace Kampai.Game
{
	public class CharacterArrivedAtDestinationCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public int characterID { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			logger.Debug("Character ID: {0} Arrived", characterID);
		}
	}
}
