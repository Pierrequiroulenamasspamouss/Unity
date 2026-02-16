namespace Kampai.Game
{
	public class DisablePickControllerCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Common.PickControllerModel model { get; set; }

		[Inject]
		public global::Kampai.Game.UnitializeCameraSignal unitializeSignal { get; set; }

		public override void Execute()
		{
			if (!model.Blocked)
			{
				model.InvalidateMovement = true;
				unitializeSignal.Dispatch();
			}
		}
	}
}
