namespace Kampai.UI.View
{
	public class MignetteCallMinionsMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.MignetteCallMinionsView>
	{
		private global::Kampai.Game.MignetteBuilding currentMignetteBuilding;

		private bool leaveSkrim;

		private int currentIndex;

		private int mignetteBuildingCount;

		private global::System.Collections.Generic.List<global::Kampai.Game.MignetteBuilding> validMignetteBuildingList = new global::System.Collections.Generic.List<global::Kampai.Game.MignetteBuilding>();

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSkrim { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.CallMinionSignal callMinionSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideAllWayFindersSignal hideAllWayFindersSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowHUDSignal showHUDSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowStoreSignal showStoreSignal { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void OnRegister()
		{
			leaveSkrim = false;
			base.OnRegister();
			base.view.OnMenuClose.AddListener(OnMenuClose);
			base.view.callMinionsButton.ClickedSignal.AddListener(OnCallMinions);
			base.view.leftArrow.ClickedSignal.AddListener(MoveToPreviousBuilding);
			base.view.rightArrow.ClickedSignal.AddListener(MoveToNextBuilding);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.CameraAutoPanCompleteSignal>().AddListener(PanComplete);
		}

		public override void OnRemove()
		{
			base.OnRemove();
			base.view.OnMenuClose.RemoveListener(OnMenuClose);
			base.view.callMinionsButton.ClickedSignal.RemoveListener(OnCallMinions);
			base.view.leftArrow.ClickedSignal.RemoveListener(MoveToPreviousBuilding);
			base.view.rightArrow.ClickedSignal.RemoveListener(MoveToNextBuilding);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.CameraAutoPanCompleteSignal>().RemoveListener(PanComplete);
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			global::Kampai.Game.MignetteBuilding building = args.Get<global::Kampai.Game.MignetteBuilding>();
			Init(building);
		}

		private void Init(global::Kampai.Game.MignetteBuilding building)
		{
			if (building != null)
			{
				currentMignetteBuilding = building;
				base.view.Init(currentMignetteBuilding, localService, playerService);
				InitMignetteBuildingList(currentMignetteBuilding);
			}
		}

		private void MoveToPreviousBuilding()
		{
			if (currentIndex <= 0)
			{
				currentIndex = mignetteBuildingCount - 1;
			}
			else
			{
				currentIndex--;
			}
			OpenBuildingMenu();
		}

		private void OpenBuildingMenu()
		{
			base.view.SetArrowButtonsState(false);
			global::Kampai.Game.MignetteBuilding building = validMignetteBuildingList[currentIndex];
			base.view.RecreateModal(building);
			PanAndShowBuildingMenu(building);
		}

		private void PanComplete()
		{
			base.view.SetArrowButtonsState(true);
		}

		private void MoveToNextBuilding()
		{
			if (currentIndex >= mignetteBuildingCount - 1)
			{
				currentIndex = 0;
			}
			else
			{
				currentIndex++;
			}
			OpenBuildingMenu();
		}

		private void PanAndShowBuildingMenu(global::Kampai.Game.Building building)
		{
			global::UnityEngine.GameObject instance = gameContext.injectionBinder.GetInstance<global::UnityEngine.GameObject>(global::Kampai.Game.GameElement.BUILDING_MANAGER);
			global::Kampai.Game.View.BuildingManagerView component = instance.GetComponent<global::Kampai.Game.View.BuildingManagerView>();
			global::Kampai.Game.View.BuildingObject buildingObject = component.GetBuildingObject(building.ID);
			global::UnityEngine.Vector3 position = buildingObject.transform.position;
			global::Kampai.Game.CameraOffset modalOffset = building.Definition.ModalOffset;
			if (modalOffset != null)
			{
				global::UnityEngine.Vector3 type = new global::UnityEngine.Vector3(position.x + modalOffset.x, position.y, position.z + modalOffset.z);
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.CameraAutoMoveSignal>().Dispatch(type, modalOffset.zoom, new global::Kampai.Game.CameraMovementSettings(global::Kampai.Game.CameraMovementSettings.Settings.KeepUIOpen, building, null));
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.CameraInstanceFocusSignal>().Dispatch(building.ID, position);
			}
		}

		private void InitMignetteBuildingList(global::Kampai.Game.MignetteBuilding currentMignetteBuilding)
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.MignetteBuilding> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.MignetteBuilding>();
			for (int i = 0; i < instancesByType.Count; i++)
			{
				global::Kampai.Game.MignetteBuilding mignetteBuilding = instancesByType[i];
				validMignetteBuildingList.Add(mignetteBuilding);
				if (currentMignetteBuilding.ID == mignetteBuilding.ID)
				{
					currentIndex = i;
				}
			}
			mignetteBuildingCount = validMignetteBuildingList.Count;
			if (mignetteBuildingCount <= 1)
			{
				base.view.SetArrowButtonsVisibleAndActive(false);
			}
		}

		private void OnCallMinions()
		{
			leaveSkrim = true;
			hideAllWayFindersSignal.Dispatch();
			showHUDSignal.Dispatch(false);
			showStoreSignal.Dispatch(false);
			currentMignetteBuilding = validMignetteBuildingList[currentIndex];
			int minionSlotsOwned = currentMignetteBuilding.MinionSlotsOwned;
			for (int i = 0; i < minionSlotsOwned; i++)
			{
				callMinionSignal.Dispatch(currentMignetteBuilding, base.view.gameObject);
			}
			base.view.Close();
		}

		protected override void Close()
		{
			base.view.Close();
		}

		private void OnMenuClose()
		{
			if (!leaveSkrim)
			{
				hideSkrim.Dispatch("MignetteSkrim");
			}
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "MignetteCallMinionsMenu");
		}
	}
}
