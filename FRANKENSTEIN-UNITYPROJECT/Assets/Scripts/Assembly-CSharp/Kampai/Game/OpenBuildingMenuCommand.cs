namespace Kampai.Game
{
	public class OpenBuildingMenuCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.View.BuildingObject BuildingObject { get; set; }

		[Inject]
		public global::Kampai.Game.Building Building { get; set; }

		[Inject(global::Kampai.Game.GameElement.BUILDING_MANAGER)]
		public global::UnityEngine.GameObject BuildingManager { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService PlayerService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger Logger { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoMoveSignal AutoMoveSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CameraInstanceFocusSignal BuildingFocusSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowBuildingDetailMenuSignal ShowBuildingDetailmenuSignal { get; set; }

		[Inject]
		public global::Kampai.Game.OpenStageBuildingSignal openStageBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowBridgeUISignal BridgeSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService TimeService { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowNeedXMinionsSignal ShowNeedXMinionsSignal { get; set; }

		[Inject]
		public global::Kampai.Game.OpenStorageBuildingSignal OpenStorageBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingZoomSignal BuildingZoomSignal { get; set; }

		[Inject]
		public global::Kampai.Game.Mignette.MignetteGameModel MignetteGameModel { get; set; }

		[Inject]
		public global::Kampai.UI.View.SpawnDooberModel SpawnDooberModel { get; set; }

		[Inject]
		public global::Kampai.Game.MailboxSelectedSignal MailboxSelectedSignal { get; set; }

		[Inject]
		public global::Kampai.Common.RepairBuildingSignal RepairBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Common.PickControllerModel PickControllerModel { get; set; }

		[Inject]
		public global::Kampai.Game.OpenOrderBoardSignal openOrderBoardSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IMarketplaceService marketplaceService { get; set; }

		[Inject]
		public global::Kampai.Game.OrderBoardUpdateTicketOnBoardSignal updateTicketOnBoardSignal { get; set; }

		[Inject(global::Kampai.UI.View.UIElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable uiContext { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		public override void Execute()
		{
			TryOpenMenu(BuildingObject, Building);
		}

		private void TryOpenMenu(global::Kampai.Game.View.BuildingObject buildingObject, global::Kampai.Game.Building building)
		{
			global::Kampai.Game.View.BuildingManagerMediator component = BuildingManager.GetComponent<global::Kampai.Game.View.BuildingManagerMediator>();
			if ((component.LastHarvestedBuildingID == building.ID && component.HarvestTimer > 0f) || PickControllerModel.IsInstanceIgnored(buildingObject.ID))
			{
				return;
			}
			if (building.State == global::Kampai.Game.BuildingState.Broken)
			{
				RepairBuildingSignal.Dispatch(building);
				if (building is global::Kampai.Game.OrderBoard)
				{
					routineRunner.StartCoroutine(UpdateTickets());
				}
			}
			else
			{
				TryOpenBuildingMenu(buildingObject, building);
			}
		}

		private global::System.Collections.IEnumerator UpdateTickets()
		{
			yield return null;
			updateTicketOnBoardSignal.Dispatch();
		}

		private void TryOpenBuildingMenu(global::Kampai.Game.View.BuildingObject buildingObject, global::Kampai.Game.Building building)
		{
			global::Kampai.Game.BuildingState state = building.State;
			if ((state != global::Kampai.Game.BuildingState.Idle && state != global::Kampai.Game.BuildingState.Working && state != global::Kampai.Game.BuildingState.Cooldown) || PickControllerModel.SelectedBuilding.HasValue || PickControllerModel.HeldTimer >= 0.5f)
			{
				return;
			}
			global::Kampai.Game.MailboxBuilding mailboxBuilding = building as global::Kampai.Game.MailboxBuilding;
			if (mailboxBuilding != null)
			{
				MailboxSelectedSignal.Dispatch();
				return;
			}
			global::Kampai.Game.MignetteBuilding mignetteBuilding = building as global::Kampai.Game.MignetteBuilding;
			if (mignetteBuilding != null)
			{
				if (MignetteGameModel.IsMignetteActive || mignetteBuilding.AreAllMinionSlotsFilled() || SpawnDooberModel.DooberCounter > 0 || PlayerService.GetQuantity(global::Kampai.Game.StaticItem.XP_ID) >= PlayerService.GetQuantity(global::Kampai.Game.StaticItem.XP_TO_LEVEL_UP_ID))
				{
					return;
				}
				if (!HasEnoughFreeMinionsToAssignToBuilding(mignetteBuilding))
				{
					ShowNeedXMinionsSignal.Dispatch(mignetteBuilding.GetMinionSlotsOwned());
					return;
				}
			}
			global::Kampai.Game.DebrisBuilding debrisBuilding = building as global::Kampai.Game.DebrisBuilding;
			if (debrisBuilding != null && debrisBuilding.PaidInputCostToClear)
			{
				Logger.Warning("Already bought debris building: {0}", building.ID);
				return;
			}
			global::Kampai.Game.TikiBarBuilding tikiBarBuilding = building as global::Kampai.Game.TikiBarBuilding;
			if (tikiBarBuilding != null)
			{
				BuildingZoomSignal.Dispatch(new global::Kampai.Game.BuildingZoomSettings(global::Kampai.Game.ZoomType.IN, global::Kampai.Game.BuildingZoomType.TIKIBAR));
				return;
			}
			if (debrisBuilding == null && building.State != global::Kampai.Game.BuildingState.Cooldown)
			{
				global::Kampai.Game.TaskableBuilding taskableBuilding = building as global::Kampai.Game.TaskableBuilding;
				if (taskableBuilding != null && PickControllerModel.SelectedMinions.Count > 0 && taskableBuilding.GetMinionsInBuilding() < taskableBuilding.Definition.WorkStations)
				{
					return;
				}
			}
			if (!(building is global::Kampai.Game.DecorationBuilding) && mignetteBuilding == null)
			{
				buildingObject.Bounce();
			}
			global::Kampai.Game.CraftingBuilding craftingBuilding = building as global::Kampai.Game.CraftingBuilding;
			if (craftingBuilding != null && craftingBuilding.CompletedCrafts.Count > 0)
			{
				return;
			}
			global::Kampai.Game.StorageBuilding storageBuilding = building as global::Kampai.Game.StorageBuilding;
			if (storageBuilding != null)
			{
				if (marketplaceService.AreThereSoldItems())
				{
					uiContext.injectionBinder.GetInstance<global::Kampai.Game.OpenSellBuildingModalSignal>().Dispatch();
				}
				else
				{
					OpenStorageBuildingSignal.Dispatch(storageBuilding, false);
				}
				return;
			}
			global::Kampai.Game.OrderBoard orderBoard = building as global::Kampai.Game.OrderBoard;
			if (orderBoard != null && orderBoard.menuEnabled)
			{
				openOrderBoardSignal.Dispatch(orderBoard);
				return;
			}
			global::Kampai.Game.BridgeBuilding bridgeBuilding = building as global::Kampai.Game.BridgeBuilding;
			if (bridgeBuilding != null)
			{
				BridgeSignal.Dispatch(bridgeBuilding.ID);
				return;
			}
			global::Kampai.Game.StageBuilding stageBuilding = building as global::Kampai.Game.StageBuilding;
			if (stageBuilding != null)
			{
				openStageBuildingSignal.Dispatch(stageBuilding);
			}
			ShowBuildingDetailMenu(buildingObject, building);
		}

		private void ShowBuildingDetailMenu(global::Kampai.Game.View.BuildingObject buildingObject, global::Kampai.Game.Building building)
		{
			if (building.Definition.ModalOffset != null)
			{
				PanAndShowBuildingMenu(buildingObject, building);
			}
			else
			{
				ShowBuildingDetailmenuSignal.Dispatch(building);
			}
		}

		private void PanAndShowBuildingMenu(global::Kampai.Game.View.BuildingObject buildingObject, global::Kampai.Game.Building building)
		{
			global::UnityEngine.Vector3 position = buildingObject.transform.position;
			global::Kampai.Game.CameraOffset modalOffset = building.Definition.ModalOffset;
			if (modalOffset != null)
			{
				global::UnityEngine.Vector3 type = new global::UnityEngine.Vector3(position.x + modalOffset.x, position.y, position.z + modalOffset.z);
				AutoMoveSignal.Dispatch(type, modalOffset.zoom, new global::Kampai.Game.CameraMovementSettings(global::Kampai.Game.CameraMovementSettings.Settings.ShowMenu, building, null));
				global::Kampai.Game.CraftingBuilding craftingBuilding = building as global::Kampai.Game.CraftingBuilding;
				if (craftingBuilding == null)
				{
					BuildingFocusSignal.Dispatch(building.ID, position);
				}
			}
			else
			{
				Logger.Log(global::Kampai.Util.Logger.Level.Error, "Attempting to pan to building with no modal offset configured: " + buildingObject.gameObject.name);
			}
		}

		private bool HasEnoughFreeMinionsToAssignToBuilding(global::Kampai.Game.TaskableBuilding building)
		{
			int num = 0;
			foreach (global::Kampai.Game.Minion item in PlayerService.GetInstancesByType<global::Kampai.Game.Minion>())
			{
				if (item.State == global::Kampai.Game.MinionState.Idle || item.State == global::Kampai.Game.MinionState.Selectable || item.State == global::Kampai.Game.MinionState.Selected || item.State == global::Kampai.Game.MinionState.Leisure)
				{
					num++;
				}
			}
			return num >= building.GetMinionSlotsOwned() - building.GetMinionsInBuilding();
		}
	}
}
