public class SendMinionToLeisureCommand : global::strange.extensions.command.impl.Command
{
	[Inject]
	public global::Kampai.Util.Tuple<int, int, int> parameters { get; set; }

	public int buildingID { get; set; }

	public int minionID { get; set; }

	public int startTime { get; set; }

	[Inject]
	public global::Kampai.Game.IPlayerService playerService { get; set; }

	[Inject(global::Kampai.Game.GameElement.MINION_MANAGER)]
	public global::UnityEngine.GameObject minionManager { get; set; }

	[Inject(global::Kampai.Game.GameElement.BUILDING_MANAGER)]
	public global::UnityEngine.GameObject buildingManager { get; set; }

	[Inject]
	public global::Kampai.Util.PathFinder pathFinder { get; set; }

	[Inject]
	public global::Kampai.Util.ILogger logger { get; set; }

	[Inject]
	public global::Kampai.Game.ITimeService timeService { get; set; }

	[Inject]
	public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

	[Inject]
	public global::Kampai.Game.LeisureBuildingCooldownSignal leisureCoolDownSignal { get; set; }

	[Inject]
	public global::Kampai.Game.MinionStateChangeSignal minionStateChangeSignal { get; set; }

	[Inject]
	public global::Kampai.Game.BuildingChangeStateSignal changeState { get; set; }

	[Inject]
	public global::Kampai.Game.RouteMinionToLeisureSignal routeMinionToLeisureSignal { get; set; }

	[Inject]
	public global::Kampai.Game.KillFunWithMinionStateSignal killFunWithMinionStateSignal { get; set; }

	public override void Execute()
	{
		buildingID = parameters.Item1;
		minionID = parameters.Item2;
		startTime = parameters.Item3;
		global::Kampai.Game.View.BuildingManagerView component = buildingManager.GetComponent<global::Kampai.Game.View.BuildingManagerView>();
		global::Kampai.Game.View.MinionManagerView component2 = minionManager.GetComponent<global::Kampai.Game.View.MinionManagerView>();
		global::Kampai.Game.View.LeisureBuildingObjectView leisureBuildingObjectView = component.GetBuildingObject(buildingID) as global::Kampai.Game.View.LeisureBuildingObjectView;
		global::Kampai.Game.LeisureBuilding leisureBuilding = leisureBuildingObjectView.leisureBuilding;
		if (leisureBuilding != null && !(leisureBuildingObjectView == null))
		{
			global::Kampai.Game.Minion byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Minion>(minionID);
			if (byInstanceId.State == global::Kampai.Game.MinionState.Leisure)
			{
				killFunWithMinionStateSignal.Dispatch(byInstanceId, global::Kampai.Game.MinionState.Leisure);
			}
			global::Kampai.Game.View.MinionObject minionObject = component2.Get(minionID);
			int utcTime = HandlePathing(minionObject, leisureBuildingObjectView, leisureBuilding);
			changeState.Dispatch(leisureBuilding.ID, global::Kampai.Game.BuildingState.Working);
			if (leisureBuilding.UTCLastTaskingTimeStarted == 0)
			{
				timeEventService.AddEvent(buildingID, utcTime, leisureBuilding.Definition.LeisureTimeDuration, leisureCoolDownSignal);
			}
			leisureBuilding.AddMinion(minionID, utcTime);
		}
	}

	private int HandlePathing(global::Kampai.Game.View.MinionObject minionObject, global::Kampai.Game.View.LeisureBuildingObjectView leisureBuildingObject, global::Kampai.Game.LeisureBuilding building)
	{
		global::UnityEngine.Vector3 position = minionObject.transform.position;
		int minionsInBuilding = building.GetMinionsInBuilding();
		global::UnityEngine.Vector3 routePosition = leisureBuildingObject.GetRoutePosition(minionsInBuilding, building, position);
		global::UnityEngine.Vector3 routeRotation = leisureBuildingObject.GetRouteRotation(minionsInBuilding);
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
		routeMinionToLeisureSignal.Dispatch(minionObject, type, minionsInBuilding);
		int result = timeService.GameTimeSeconds();
		global::Kampai.Game.Minion byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Minion>(minionID);
		if (byInstanceId == null)
		{
			logger.Fatal(global::Kampai.Util.FatalCode.CMD_NO_SUCH_MINION, "{0}", minionID);
		}
		byInstanceId.BuildingID = buildingID;
		minionStateChangeSignal.Dispatch(byInstanceId.ID, global::Kampai.Game.MinionState.Leisure);
		return result;
	}
}
