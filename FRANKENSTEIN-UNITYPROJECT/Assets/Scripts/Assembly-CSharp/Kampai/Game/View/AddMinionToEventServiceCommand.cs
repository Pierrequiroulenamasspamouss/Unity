namespace Kampai.Game.View
{
	public class AddMinionToEventServiceCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Util.Tuple<int, int, int> inputValues { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

		[Inject]
		public global::Kampai.Common.MinionTaskCompleteSignal taskCompleteSignal { get; set; }

		public override void Execute()
		{
			timeEventService.AddEvent(inputValues.Item1, global::System.Convert.ToInt32(inputValues.Item2), inputValues.Item3, taskCompleteSignal);
		}
	}
}
