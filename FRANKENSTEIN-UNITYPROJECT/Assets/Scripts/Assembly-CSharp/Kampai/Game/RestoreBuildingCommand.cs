namespace Kampai.Game
{
	public class RestoreBuildingCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.Building building { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingChangeStateSignal buildingChangeStateSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Game.RestoreScaffoldingViewSignal restoreScaffoldingViewSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RestoreRibbonViewSignal restoreRibbonViewSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RestorePlatformViewSignal restorePlatformViewSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RestoreBuildingViewSignal restoreBuildingViewSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

		[Inject]
		public global::Kampai.Game.OrderBoardRefillTicketSignal refillTicketSignal { get; set; }

		[Inject]
		public global::Kampai.Game.OrderBoardSetNewTicketSignal setNewTicketSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RestoreTaskableBuildingSignal restoreTaskingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RestoreCraftingBuildingsSignal craftingRestoreSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RestoreLeisureBuildingSignal restoreLeisureBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CleanupDebrisSignal cleanupDebris { get; set; }

		[Inject]
		public global::Kampai.Common.ScheduleCooldownSignal scheduleCooldownSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Game.IDLCService dlcService { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.BuildingState state = building.State;
			global::Kampai.Game.BuildingDefinition definition = building.Definition;
			int num = timeService.GameTimeSeconds() - building.StateStartTime;
			switch (state)
			{
			case global::Kampai.Game.BuildingState.Inactive:
			case global::Kampai.Game.BuildingState.Construction:
				HandleInConstruction(num);
				break;
			case global::Kampai.Game.BuildingState.Complete:
				HandleCompletedConstruction();
				break;
			case global::Kampai.Game.BuildingState.Working:
			case global::Kampai.Game.BuildingState.Harvestable:
			case global::Kampai.Game.BuildingState.HarvestableAndWorking:
			{
				restoreBuildingViewSignal.Dispatch(building);
				global::Kampai.Game.TaskableBuilding taskableBuilding = building as global::Kampai.Game.TaskableBuilding;
				if (taskableBuilding != null)
				{
					restoreTaskingSignal.Dispatch(taskableBuilding);
				}
				global::Kampai.Game.CraftingBuilding craftingBuilding = building as global::Kampai.Game.CraftingBuilding;
				if (craftingBuilding != null)
				{
					craftingRestoreSignal.Dispatch(craftingBuilding);
				}
				global::Kampai.Game.LeisureBuilding leisureBuilding = building as global::Kampai.Game.LeisureBuilding;
				if (leisureBuilding != null && state == global::Kampai.Game.BuildingState.Working)
				{
					restoreLeisureBuildingSignal.Dispatch(leisureBuilding);
				}
				break;
			}
			default:
			{
				Qualitative qualitative = definition as Qualitative;
				if (qualitative != null)
				{
					qualitative.Quality = dlcService.GetDownloadQualityLevel().ToUpper();
				}
				restoreBuildingViewSignal.Dispatch(building);
				break;
			}
			case global::Kampai.Game.BuildingState.Inventory:
				break;
			}
			if (building.GetType() == typeof(global::Kampai.Game.OrderBoard) && state != global::Kampai.Game.BuildingState.Complete && state != global::Kampai.Game.BuildingState.Construction && state != global::Kampai.Game.BuildingState.Inactive)
			{
				RestoreBlackMarket((global::Kampai.Game.OrderBoard)building);
			}
			CheckCooldownState(state, num);
			CleanupDebris();
		}

		private void HandleInConstruction(float passedTime)
		{
			int constructionTime = building.Definition.ConstructionTime;
			restoreBuildingViewSignal.Dispatch(building);
			restorePlatformViewSignal.Dispatch(building);
			if (passedTime > (float)constructionTime)
			{
				restoreScaffoldingViewSignal.Dispatch(building, false);
				restoreRibbonViewSignal.Dispatch(building);
				buildingChangeStateSignal.Dispatch(building.ID, global::Kampai.Game.BuildingState.Complete);
			}
			else
			{
				restoreScaffoldingViewSignal.Dispatch(building, true);
			}
		}

		private void HandleCompletedConstruction()
		{
			restoreBuildingViewSignal.Dispatch(building);
			restorePlatformViewSignal.Dispatch(building);
			restoreScaffoldingViewSignal.Dispatch(building, false);
			restoreRibbonViewSignal.Dispatch(building);
		}

		private void CleanupDebris()
		{
			global::Kampai.Game.DebrisBuilding debrisBuilding = building as global::Kampai.Game.DebrisBuilding;
			if (debrisBuilding != null && debrisBuilding.PaidInputCostToClear)
			{
				cleanupDebris.Dispatch(debrisBuilding.ID, false);
			}
		}

		private void CheckCooldownState(global::Kampai.Game.BuildingState buildingState, int passedTime)
		{
			if (buildingState != global::Kampai.Game.BuildingState.Cooldown || !(building is global::Kampai.Game.IBuildingWithCooldown))
			{
				return;
			}
			int cooldown = ((global::Kampai.Game.IBuildingWithCooldown)building).GetCooldown();
			if (passedTime >= cooldown)
			{
				buildingChangeStateSignal.Dispatch(building.ID, global::Kampai.Game.BuildingState.Idle);
				return;
			}
			bool second = false;
			global::Kampai.Game.MignetteBuilding mignetteBuilding = building as global::Kampai.Game.MignetteBuilding;
			if (mignetteBuilding != null)
			{
				second = true;
			}
			scheduleCooldownSignal.Dispatch(new global::Kampai.Util.Tuple<int, bool>(building.ID, second), false);
		}

		private void RestoreBlackMarket(global::Kampai.Game.OrderBoard blackMarketBuilding)
		{
			int num = 0;
			num = blackMarketBuilding.Definition.LevelBands[blackMarketBuilding.CurrentLevelBandIndex].ResurfaceTime;
			foreach (global::Kampai.Game.OrderBoardTicket ticket in blackMarketBuilding.tickets)
			{
				if (ticket.StartTime <= 0)
				{
					continue;
				}
				if (timeService.GameTimeSeconds() >= ticket.StartTime + num)
				{
					int inverseTicketIndex = -ticket.BoardIndex;
					routineRunner.StartCoroutine(WaitAFrame(delegate
					{
						setNewTicketSignal.Dispatch(inverseTicketIndex, false);
					}));
				}
				else if (timeService.GameTimeSeconds() < ticket.StartTime + num)
				{
					timeEventService.AddEvent(-ticket.BoardIndex, ticket.StartTime, num, refillTicketSignal);
				}
			}
		}

		private global::System.Collections.IEnumerator WaitAFrame(global::System.Action a)
		{
			yield return null;
			a();
		}
	}
}
