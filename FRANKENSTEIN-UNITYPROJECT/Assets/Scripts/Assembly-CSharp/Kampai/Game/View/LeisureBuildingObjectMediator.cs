namespace Kampai.Game.View
{
	public class LeisureBuildingObjectMediator : global::strange.extensions.mediation.impl.EventMediator
	{
		private global::strange.extensions.signal.impl.Signal<global::Kampai.Game.View.CharacterObject, int> addToBuildingSignal;

		[Inject]
		public global::Kampai.Game.View.LeisureBuildingObjectView view { get; set; }

		[Inject(global::Kampai.Game.GameElement.MINION_MANAGER)]
		public global::UnityEngine.GameObject minionManager { get; set; }

		[Inject]
		public global::Kampai.Game.RouteMinionToLeisureSignal routeMinionToLeisureSignal { get; set; }

		[Inject]
		public global::Kampai.Game.LeisureBuildingCooldownSignal cooldownSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingChangeStateSignal changeStateSignal { get; set; }

		[Inject]
		public global::Kampai.Game.TeleportMinionToLeisureSignal teleportMinionToLeisureSignal { get; set; }

		[Inject]
		public global::Kampai.Game.MinionStateChangeSignal minionStateChangeSignal { get; set; }

		[Inject]
		public global::Kampai.Game.KillFunSignal killFunSignal { get; set; }

		[Inject]
		public global::Kampai.Game.KillFunWithMinionStateSignal killFunWithMinionStateSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void OnRegister()
		{
			addToBuildingSignal = new global::strange.extensions.signal.impl.Signal<global::Kampai.Game.View.CharacterObject, int>();
			routeMinionToLeisureSignal.AddListener(RouteMinionToLeisure);
			killFunSignal.AddListener(KillFun);
			killFunWithMinionStateSignal.AddListener(KillFunWithMinionState);
			cooldownSignal.AddListener(CoolDownCompleted);
			teleportMinionToLeisureSignal.AddListener(TeleportMinionToLeisure);
			addToBuildingSignal.AddListener(AddToBuilding);
			SetupInjections();
		}

		public override void OnRemove()
		{
			routeMinionToLeisureSignal.RemoveListener(RouteMinionToLeisure);
			killFunSignal.RemoveListener(KillFun);
			killFunWithMinionStateSignal.RemoveListener(KillFunWithMinionState);
			cooldownSignal.RemoveListener(CoolDownCompleted);
			teleportMinionToLeisureSignal.RemoveListener(TeleportMinionToLeisure);
			addToBuildingSignal.RemoveListener(AddToBuilding);
		}

		private void SetupInjections()
		{
			view.SetupInjections(minionStateChangeSignal);
		}

		private void RouteMinionToLeisure(global::Kampai.Game.View.MinionObject minionObject, global::Kampai.Game.View.RouteInstructions routeInfo, int routeIndex)
		{
			if (view.leisureBuilding.ID == routeInfo.TargetBuilding.ID)
			{
				view.PathMinionToLeisureBuilding(minionObject, routeInfo.Path, routeInfo.Rotation, routeIndex, addToBuildingSignal);
			}
		}

		private void CoolDownCompleted(int buildingInstance)
		{
			if (view.leisureBuilding.ID == buildingInstance)
			{
				CleanBuildingState();
				view.FreeAllMinions();
			}
		}

		private void AddToBuilding(global::Kampai.Game.View.CharacterObject characterObject, int routeIndex)
		{
			view.AddCharacterToBuildingActions(characterObject, routeIndex);
		}

		private void TeleportMinionToLeisure(global::Kampai.Game.Minion minion)
		{
			if (minion.BuildingID == view.leisureBuilding.ID)
			{
				global::Kampai.Game.View.MinionManagerView component = minionManager.GetComponent<global::Kampai.Game.View.MinionManagerView>();
				global::Kampai.Game.View.MinionObject mo = component.Get(minion.ID);
				int minionRouteIndex = view.leisureBuilding.GetMinionRouteIndex(minion.ID);
				if (minionRouteIndex == -1)
				{
					logger.Error("Minion {0} doesn't exist on this building {1}, but you are still trying to add him.", minion.ID, view.leisureBuilding.ID);
				}
				view.AddCharacterToBuildingActions(mo, minionRouteIndex);
			}
		}

		private void KillFun(int buildingInstanceID)
		{
			if (view.leisureBuilding.ID == buildingInstanceID)
			{
				timeEventService.RemoveEvent(view.leisureBuilding.ID);
				SetMinionLastLeisureTimes();
				CleanBuildingState();
				view.FreeAllMinions();
			}
		}

		private void KillFunWithMinionState(global::Kampai.Game.Minion minion, global::Kampai.Game.MinionState targetMinionState)
		{
			if (view.leisureBuilding.ID == minion.BuildingID)
			{
				timeEventService.RemoveEvent(view.leisureBuilding.ID);
				SetMinionLastLeisureTimes();
				CleanBuildingState();
				view.FreeAllMinions(minion.ID, targetMinionState);
			}
		}

		private void SetMinionLastLeisureTimes()
		{
			int lastLeisureTime = timeService.GameTimeSeconds();
			foreach (int minion in view.leisureBuilding.MinionList)
			{
				global::Kampai.Game.Minion byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Minion>(minion);
				byInstanceId.LastLeisureTime = lastLeisureTime;
			}
		}

		private void CleanBuildingState()
		{
			changeStateSignal.Dispatch(view.leisureBuilding.ID, global::Kampai.Game.BuildingState.Idle);
			foreach (int minion in view.leisureBuilding.MinionList)
			{
				if (!view.IsMinionInBuilding(minion))
				{
					global::Kampai.Game.View.MinionManagerView component = minionManager.GetComponent<global::Kampai.Game.View.MinionManagerView>();
					global::Kampai.Game.View.MinionObject minionObject = component.Get(minion);
					minionObject.ClearActionQueue();
				}
				global::Kampai.Game.Minion byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Minion>(minion);
				byInstanceId.BuildingID = -1;
				minionStateChangeSignal.Dispatch(minion, global::Kampai.Game.MinionState.Idle);
			}
			view.leisureBuilding.CleanMinionQueue();
		}
	}
}
