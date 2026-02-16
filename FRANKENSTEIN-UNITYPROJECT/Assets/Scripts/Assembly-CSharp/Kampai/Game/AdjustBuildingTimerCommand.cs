namespace Kampai.Game
{
	public class AdjustBuildingTimerCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.Building building { get; set; }

		[Inject]
		public int taskIndex { get; set; }

		public override void Execute()
		{
		}
	}
}
