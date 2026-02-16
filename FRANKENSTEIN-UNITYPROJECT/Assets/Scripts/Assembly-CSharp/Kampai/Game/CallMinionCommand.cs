namespace Kampai.Game
{
	public class CallMinionCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.Building building { get; set; }

		[Inject]
		public global::Kampai.Util.PathFinder pathFinder { get; set; }

		[Inject(global::Kampai.Game.GameElement.BUILDING_MANAGER)]
		public global::UnityEngine.GameObject buildingManager { get; set; }

		[Inject(global::Kampai.Game.GameElement.MINION_MANAGER)]
		public global::UnityEngine.GameObject minionManager { get; set; }

		[Inject]
		public global::Kampai.Game.FinishCallMinionSignal finishSignal { get; set; }

		[Inject]
		public global::UnityEngine.GameObject signalSender { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.StartMinionTaskSignal startMinionTaskSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal playMinionNoAnimAudioSignal { get; set; }

		[Inject]
		public global::Kampai.Common.DeselectMinionSignal deselectSignal { get; set; }

		[Inject]
		public global::Kampai.Game.KillFunSignal killFunSignal { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.TaskableBuilding taskableBuilding = building as global::Kampai.Game.TaskableBuilding;
			int harvestTimeForTaskableBuilding = BuildingUtil.GetHarvestTimeForTaskableBuilding(taskableBuilding, definitionService);
			if (taskableBuilding == null)
			{
				return;
			}
			global::Kampai.Game.View.MinionManagerView component = minionManager.GetComponent<global::Kampai.Game.View.MinionManagerView>();
			global::Kampai.Game.Minion closestMinionToLocation = component.GetClosestMinionToLocation(building.Location);
			if (closestMinionToLocation == null)
			{
				return;
			}
			if (closestMinionToLocation.State == global::Kampai.Game.MinionState.Selected)
			{
				global::Kampai.Game.DebrisBuilding debrisBuilding = building as global::Kampai.Game.DebrisBuilding;
				if (debrisBuilding == null)
				{
					return;
				}
				deselectSignal.Dispatch(closestMinionToLocation.ID);
			}
			else if (closestMinionToLocation.State == global::Kampai.Game.MinionState.Leisure)
			{
				killFunSignal.Dispatch(closestMinionToLocation.BuildingID);
			}
			playMinionNoAnimAudioSignal.Dispatch("Play_minion_confirm_pathToBldg_01");
			startMinionTaskSignal.Dispatch(new global::Kampai.Util.Tuple<int, int, int>(taskableBuilding.ID, closestMinionToLocation.ID, timeService.GameTimeSeconds()));
			finishSignal.Dispatch(new global::Kampai.Util.Tuple<int, int, global::UnityEngine.GameObject>(closestMinionToLocation.ID, harvestTimeForTaskableBuilding, signalSender));
		}
	}
}
