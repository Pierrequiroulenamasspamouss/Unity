namespace Kampai.Game
{
	public class ScheduleGagCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public int BuildingID { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService player { get; set; }

		public override void Execute()
		{
		}
	}
}
