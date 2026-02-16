namespace Kampai.Game.View
{
	public class RestoreTaskableBuildingCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.TaskableBuilding building { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.HarvestReadySignal harvestSignal { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingChangeStateSignal changeStateSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			logger.Debug("Restoring a Tasking Building");
			global::Kampai.Game.BuildingState newState = global::Kampai.Game.BuildingState.Inactive;
			if (building is global::Kampai.Game.MignetteBuilding)
			{
				newState = global::Kampai.Game.BuildingState.Idle;
			}
			else if (building.GetAvailableHarvest() > 0)
			{
				newState = global::Kampai.Game.BuildingState.Harvestable;
				if (building is global::Kampai.Game.ResourceBuilding)
				{
					harvestSignal.Dispatch(building.ID);
				}
			}
			else if (building.GetMinionsInBuilding() > 0)
			{
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
			a();
		}
	}
}
