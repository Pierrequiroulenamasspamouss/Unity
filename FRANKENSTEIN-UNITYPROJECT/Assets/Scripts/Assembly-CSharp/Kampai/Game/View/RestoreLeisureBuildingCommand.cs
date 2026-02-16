namespace Kampai.Game.View
{
	public class RestoreLeisureBuildingCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.LeisureBuilding building { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingChangeStateSignal changeStateSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Game.KillFunSignal killFunSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			logger.Debug("Restoring a Leisure Building");
			global::Kampai.Game.BuildingState newState = global::Kampai.Game.BuildingState.Inactive;
			int num = timeService.GameTimeSeconds() - building.UTCLastTaskingTimeStarted;
			if (num > building.Definition.LeisureTimeDuration || building.UTCLastTaskingTimeStarted == 0)
			{
				newState = global::Kampai.Game.BuildingState.Idle;
			}
			else
			{
				timeEventService.AddEvent(building.ID, building.UTCLastTaskingTimeStarted, building.Definition.LeisureTimeDuration, killFunSignal);
				newState = global::Kampai.Game.BuildingState.Working;
			}
			if (newState != global::Kampai.Game.BuildingState.Inactive)
			{
				routineRunner.StartCoroutine(WaitAFrame(delegate
				{
					changeStateSignal.Dispatch(building.ID, newState);
				}));
			}
		}

		private global::System.Collections.IEnumerator WaitAFrame(global::System.Action a)
		{
			yield return null;
			killFunSignal.Dispatch(building.ID);
			a();
		}
	}
}
