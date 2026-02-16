public class StartMinionTaskCommand : global::strange.extensions.command.impl.Command
{
	[Inject]
	public global::Kampai.Util.Tuple<int, int, int> parameters { get; set; }

	public int buildingID { get; set; }

	public int minionID { get; set; }

	[Inject]
	public global::Kampai.Game.IPlayerService playerService { get; set; }

	public int startTime { get; set; }

	[Inject]
	public global::Kampai.Game.StartMinionRouteSignal startMinionRouteSignal { get; set; }

	[Inject(global::Kampai.Game.GameElement.MINION_MANAGER)]
	public global::UnityEngine.GameObject minionManager { get; set; }

	[Inject(global::Kampai.Game.GameElement.BUILDING_MANAGER)]
	public global::UnityEngine.GameObject buildingManager { get; set; }

	[Inject]
	public global::Kampai.Util.PathFinder pathFinder { get; set; }

	[Inject]
	public global::Kampai.Game.IQuestService questService { get; set; }

	[Inject]
	public global::Kampai.Common.AddMinionToEventServiceSignal addMinionSignal { get; set; }

	[Inject]
	public global::Kampai.Util.ILogger logger { get; set; }

	[Inject]
	public global::Kampai.Game.ITimeService timeService { get; set; }

	[Inject]
	public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

	[Inject]
	public global::Kampai.Game.IDefinitionService definitionService { get; set; }

	[Inject]
	public global::Kampai.Game.IPrestigeService characterService { get; set; }

	[Inject]
	public global::Kampai.Game.MinionStateChangeSignal minionStateChangeSignal { get; set; }

	[Inject]
	public global::Kampai.Game.BuildingChangeStateSignal changeState { get; set; }

	[Inject]
	public global::Kampai.Game.TeleportMinionToBuildingSignal teleportMinionToBuildingSignal { get; set; }

	[Inject]
	public global::Kampai.Game.KillFunWithMinionStateSignal killFunWithMinionStateSignal { get; set; }

	[Inject]
	public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

	public override void Execute()
	{
		buildingID = parameters.Item1;
		minionID = parameters.Item2;
		startTime = parameters.Item3;
		global::Kampai.Game.View.BuildingManagerView component = buildingManager.GetComponent<global::Kampai.Game.View.BuildingManagerView>();
		if (buildingManager == null || component == null)
		{
			logger.Fatal(global::Kampai.Util.FatalCode.CMD_NULL_REF, 0);
		}
		global::Kampai.Game.View.MinionManagerView component2 = minionManager.GetComponent<global::Kampai.Game.View.MinionManagerView>();
		if (minionManager == null || component2 == null)
		{
			logger.Fatal(global::Kampai.Util.FatalCode.CMD_NULL_REF, 1);
		}
		global::Kampai.Game.View.TaskableBuildingObject taskableBuildingObject = component.GetBuildingObject(buildingID) as global::Kampai.Game.View.TaskableBuildingObject;
		object obj;
		if (taskableBuildingObject != null)
		{
			global::Kampai.Game.TaskableBuilding byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.TaskableBuilding>(taskableBuildingObject.ID);
			obj = byInstanceId;
		}
		else
		{
			obj = null;
		}
		global::Kampai.Game.TaskableBuilding taskableBuilding = (global::Kampai.Game.TaskableBuilding)obj;
		if (taskableBuilding == null)
		{
			return;
		}
		global::Kampai.Game.BuildingState state = taskableBuilding.State;
		if (state != global::Kampai.Game.BuildingState.Idle && state != global::Kampai.Game.BuildingState.Working && state != global::Kampai.Game.BuildingState.Harvestable)
		{
			return;
		}
		int minionsInBuilding = taskableBuilding.GetMinionsInBuilding();
		int minionSlotsOwned = taskableBuilding.GetMinionSlotsOwned();
		if (minionsInBuilding < minionSlotsOwned)
		{
			global::UnityEngine.GameObject gameObject = component2.GetGameObject(minionID);
			int num = HandlePathing(gameObject, taskableBuildingObject, taskableBuilding);
			changeState.Dispatch(taskableBuilding.ID, global::Kampai.Game.BuildingState.Working);
			taskableBuilding.StateStartTime = num;
			taskableBuilding.AddMinion(minionID, num);
			if (questService == null)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.CMD_NULL_REF, 2);
			}
			questService.StartMinionTask(taskableBuilding.Definition.ID);
		}
	}

	private int HandlePathing(global::UnityEngine.GameObject minionGo, global::Kampai.Game.View.TaskableBuildingObject tbo, global::Kampai.Game.TaskableBuilding building)
	{
		global::UnityEngine.Vector3 position = minionGo.transform.position;
		global::UnityEngine.Vector3 routePosition = tbo.GetRoutePosition(building.GetMinionsInBuilding(), building, position);
		global::UnityEngine.Vector3 routeRotation = tbo.GetRouteRotation(building.GetMinionsInBuilding());
		global::System.Collections.Generic.IList<global::UnityEngine.Vector3> list = pathFinder.FindPath(position, routePosition, 4, true);
		if (list == null)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Vector3> list2 = new global::System.Collections.Generic.List<global::UnityEngine.Vector3>();
			list2.Add(routePosition);
			list = list2;
		}
		global::Kampai.Game.View.RouteInstructions type = new global::Kampai.Game.View.RouteInstructions
		{
			MinionId = minionID,
			Path = list,
			Rotation = routeRotation.y,
			TargetBuilding = building,
			StartTime = startTime
		};
		startMinionRouteSignal.Dispatch(type);
		return SetupMinionState(minionID, minionGo, building);
	}

	private int SetupMinionState(int minionID, global::UnityEngine.GameObject minionGo, global::Kampai.Game.TaskableBuilding building)
	{
		global::Kampai.Game.View.MinionObject component = minionGo.GetComponent<global::Kampai.Game.View.MinionObject>();
		float num = component.GetAction<global::Kampai.Game.View.ConstantSpeedPathAction>().Duration();
		int num2 = timeService.GameTimeSeconds();
		global::Kampai.Game.Minion byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Minion>(minionID);
		if (byInstanceId == null)
		{
			logger.Fatal(global::Kampai.Util.FatalCode.CMD_NO_SUCH_MINION, "{0}", minionID);
		}
		byInstanceId.BuildingID = buildingID;
		if (building is global::Kampai.Game.MignetteBuilding)
		{
			minionStateChangeSignal.Dispatch(byInstanceId.ID, global::Kampai.Game.MinionState.PlayingMignette);
			routineRunner.StartCoroutine(WaitThenTeleportMinion(byInstanceId.ID));
		}
		else
		{
			byInstanceId.TaskDuration = BuildingUtil.GetHarvestTimeForTaskableBuilding(building, definitionService);
			byInstanceId.UTCTaskStartTime = num2 + (int)num + 1;
			if (!(building is global::Kampai.Game.DebrisBuilding))
			{
				addMinionSignal.Dispatch(global::Kampai.Util.Tuple.Create(byInstanceId.ID, byInstanceId.UTCTaskStartTime, byInstanceId.TaskDuration));
			}
			if (byInstanceId.State == global::Kampai.Game.MinionState.Leisure)
			{
				killFunWithMinionStateSignal.Dispatch(byInstanceId, global::Kampai.Game.MinionState.Tasking);
			}
			else
			{
				minionStateChangeSignal.Dispatch(byInstanceId.ID, global::Kampai.Game.MinionState.Tasking);
			}
		}
		return num2;
	}

	private global::System.Collections.IEnumerator WaitThenTeleportMinion(int minionID)
	{
		yield return new global::UnityEngine.WaitForSeconds(0.2f);
		teleportMinionToBuildingSignal.Dispatch(minionID);
	}
}
