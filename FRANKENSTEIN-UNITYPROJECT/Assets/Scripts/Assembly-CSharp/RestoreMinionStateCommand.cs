public class RestoreMinionStateCommand : global::strange.extensions.command.impl.Command
{
	[Inject]
	public int minionID { get; set; }

	[Inject]
	public global::Kampai.Game.IPlayerService playerService { get; set; }

	[Inject]
	public global::Kampai.Game.IDefinitionService definitionService { get; set; }

	[Inject]
	public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

	[Inject]
	public global::Kampai.Game.ITimeService timeService { get; set; }

	[Inject]
	public global::Kampai.Common.MinionTaskCompleteSignal taskCompleteSignal { get; set; }

	[Inject]
	public global::Kampai.Game.StartTeleportTaskSignal teleportTaskSignal { get; set; }

	[Inject]
	public global::Kampai.Game.EnableMinionRendererSignal enableRendererSignal { get; set; }

	[Inject]
	public global::Kampai.Game.RestoreMinionAtTikiBarSignal restoreMinionAtTikiBarSignal { get; set; }

	[Inject]
	public global::Kampai.Game.TeleportMinionToLeisureSignal teleportMinionToLeisureSignal { get; set; }

	[Inject]
	public global::Kampai.Util.ILogger logger { get; set; }

	public override void Execute()
	{
		global::Kampai.Game.Minion byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Minion>(minionID);
		if (byInstanceId == null)
		{
			logger.Fatal(global::Kampai.Util.FatalCode.CMD_RESTORE_MINION);
			return;
		}
		if (byInstanceId.State == global::Kampai.Game.MinionState.Tasking)
		{
			RestoreTaskingMinionState(byInstanceId);
		}
		else if (byInstanceId.State == global::Kampai.Game.MinionState.Selected || byInstanceId.State == global::Kampai.Game.MinionState.Selectable || byInstanceId.State == global::Kampai.Game.MinionState.WaitingOnMagnetFinger || byInstanceId.State == global::Kampai.Game.MinionState.PlayingMignette || byInstanceId.State == global::Kampai.Game.MinionState.Unselectable)
		{
			byInstanceId.State = global::Kampai.Game.MinionState.Idle;
		}
		else if (byInstanceId.State == global::Kampai.Game.MinionState.Questing)
		{
			if (byInstanceId.CostumeID == 99 || byInstanceId.CostumeID == 0)
			{
				byInstanceId.State = global::Kampai.Game.MinionState.Idle;
			}
			else
			{
				restoreMinionAtTikiBarSignal.Dispatch(byInstanceId);
			}
		}
		else if (byInstanceId.State == global::Kampai.Game.MinionState.Leisure)
		{
			RestoreLeisureMinionState(byInstanceId);
		}
		if (byInstanceId.State == global::Kampai.Game.MinionState.Idle || byInstanceId.State.Equals(global::Kampai.Game.MinionState.Uninitialized))
		{
			enableRendererSignal.Dispatch(byInstanceId.ID, true);
		}
	}

	private void RestoreTaskingMinionState(global::Kampai.Game.Minion minion)
	{
		global::Kampai.Game.TaskableBuilding byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.TaskableBuilding>(minion.BuildingID);
		if (minion.TaskDuration == 0 || minion.UTCTaskStartTime + minion.TaskDuration <= timeService.GameTimeSeconds())
		{
			if (byInstanceId != null)
			{
				if (!(byInstanceId is global::Kampai.Game.ResourceBuilding))
				{
					minion.AlreadyRushed = true;
					teleportTaskSignal.Dispatch(minion, byInstanceId);
					byInstanceId.AddMinion(minionID, minion.UTCTaskStartTime);
				}
			}
			else
			{
				minion.State = global::Kampai.Game.MinionState.Idle;
			}
			taskCompleteSignal.Dispatch(minionID);
		}
		else
		{
			teleportTaskSignal.Dispatch(minion, byInstanceId);
			timeEventService.AddEvent(minionID, minion.UTCTaskStartTime, BuildingUtil.GetHarvestTimeForTaskableBuilding(byInstanceId, definitionService), taskCompleteSignal);
			byInstanceId.AddMinion(minionID, minion.UTCTaskStartTime);
		}
	}

	private void RestoreLeisureMinionState(global::Kampai.Game.Minion minion)
	{
		global::Kampai.Game.LeisureBuilding byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.LeisureBuilding>(minion.BuildingID);
		if (byInstanceId.State == global::Kampai.Game.BuildingState.Working && byInstanceId.UTCLastTaskingTimeStarted > 0)
		{
			byInstanceId.AddMinion(minion.ID, byInstanceId.UTCLastTaskingTimeStarted);
			teleportMinionToLeisureSignal.Dispatch(minion);
		}
		else
		{
			minion.State = global::Kampai.Game.MinionState.Idle;
		}
	}
}
