namespace Kampai.UI.View
{
	public class ResourceModalMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.ResourceModalView>
	{
		private global::Kampai.UI.View.ModalSettings modalSettings = new global::Kampai.UI.View.ModalSettings();

		private global::System.Collections.Generic.List<global::Kampai.Game.ResourceBuilding> validResourceBuildingList = new global::System.Collections.Generic.List<global::Kampai.Game.ResourceBuilding>();

		private int currentIndex;

		private int resourceBuildingCount;

		private bool initialized;

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateSliderSignal updateSliderSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ResetDoubleTapSignal resetDoubleTapSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal globalSFX { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateUIButtonsSignal updateLevelSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.UI.View.FTUEProgressSignal ftueSignal { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			ftueSignal.Dispatch();
			base.view.LeftArrow.ClickedSignal.AddListener(MoveToPreviousBuilding);
			base.view.RightArrow.ClickedSignal.AddListener(MoveToNextBuilding);
			updateSliderSignal.AddListener(UpdateDisplay);
			base.view.OnMenuClose.AddListener(OnMenuClose);
			updateLevelSignal.AddListener(LevelUnlockItems);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.CameraAutoPanCompleteSignal>().AddListener(PanComplete);
		}

		public override void OnRemove()
		{
			base.OnRemove();
			updateSliderSignal.RemoveListener(UpdateDisplay);
			base.view.OnMenuClose.RemoveListener(OnMenuClose);
			updateLevelSignal.RemoveListener(LevelUnlockItems);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.CameraAutoPanCompleteSignal>().RemoveListener(PanComplete);
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			if (!initialized)
			{
				initialized = true;
				global::Kampai.Game.ResourceBuilding resourceBuilding = args.Get<global::Kampai.Game.ResourceBuilding>();
				modalSettings.enableRushButtons = !args.Contains<global::Kampai.UI.DisableRushButtons>();
				modalSettings.enableHarvestButtons = playerService.HasStorageBuilding();
				modalSettings.enableCallButtons = !args.Contains<global::Kampai.UI.DisableCallButtons>();
				modalSettings.enableLockedButtons = !args.Contains<global::Kampai.UI.DisableLockedButton>();
				modalSettings.enableRushThrob = args.Contains<global::Kampai.UI.ThrobRushButtons>();
				modalSettings.enableCallThrob = args.Contains<global::Kampai.UI.ThrobCallButtons>();
				modalSettings.enableLockedThrob = args.Contains<global::Kampai.UI.ThrobLockedButtons>();
				Init(resourceBuilding);
				InitResourceBuildingList(resourceBuilding);
			}
		}

		private void MoveToPreviousBuilding()
		{
			if (currentIndex <= 0)
			{
				currentIndex = resourceBuildingCount - 1;
			}
			else
			{
				currentIndex--;
			}
			OpenBuildingMenu();
		}

		private void OpenBuildingMenu()
		{
			base.view.SetArrowButtonState(false);
			global::Kampai.Game.ResourceBuilding building = validResourceBuildingList[currentIndex];
			RecreateModal(building);
			PanAndShowBuildingMenu(building);
		}

		private void PanComplete()
		{
			base.view.SetArrowButtonState(true);
		}

		private void MoveToNextBuilding()
		{
			if (currentIndex >= resourceBuildingCount - 1)
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

		private void InitResourceBuildingList(global::Kampai.Game.ResourceBuilding currentResourceBuilding)
		{
			global::System.Collections.Generic.List<global::Kampai.Game.ResourceBuilding> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.ResourceBuilding>();
			for (int i = 0; i < instancesByType.Count; i++)
			{
				global::Kampai.Game.ResourceBuilding resourceBuilding = instancesByType[i];
				if (resourceBuilding.State != global::Kampai.Game.BuildingState.Construction && resourceBuilding.State != global::Kampai.Game.BuildingState.Inventory && resourceBuilding.State != global::Kampai.Game.BuildingState.Inactive && resourceBuilding.State != global::Kampai.Game.BuildingState.Cooldown && resourceBuilding.State != global::Kampai.Game.BuildingState.Complete)
				{
					if (currentResourceBuilding.ID == resourceBuilding.ID)
					{
						currentIndex = validResourceBuildingList.Count;
					}
					validResourceBuildingList.Add(resourceBuilding);
				}
			}
			resourceBuildingCount = validResourceBuildingList.Count;
			if (resourceBuildingCount <= 1)
			{
				base.view.LeftArrow.gameObject.SetActive(false);
				base.view.RightArrow.gameObject.SetActive(false);
			}
		}

		private void Init(global::Kampai.Game.ResourceBuilding building)
		{
			if (building == null)
			{
				return;
			}
			global::System.Collections.Generic.List<global::Kampai.Game.Minion> list = new global::System.Collections.Generic.List<global::Kampai.Game.Minion>();
			foreach (int minion in building.MinionList)
			{
				if (!building.CompleteMinionQueue.Contains(minion))
				{
					list.Add(playerService.GetByInstanceId<global::Kampai.Game.Minion>(minion));
				}
			}
			base.view.Init(building, list, localService, definitionService, playerService, modalSettings, routineRunner);
		}

		private void RecreateModal(global::Kampai.Game.ResourceBuilding building)
		{
			if (building == null)
			{
				return;
			}
			global::System.Collections.Generic.List<global::Kampai.Game.Minion> list = new global::System.Collections.Generic.List<global::Kampai.Game.Minion>();
			foreach (int minion in building.MinionList)
			{
				if (!building.CompleteMinionQueue.Contains(minion))
				{
					list.Add(playerService.GetByInstanceId<global::Kampai.Game.Minion>(minion));
				}
			}
			modalSettings.enableRushThrob = false;
			modalSettings.enableCallThrob = false;
			modalSettings.enableLockedThrob = false;
			base.view.RecreateModal(building, list, modalSettings);
		}

		protected override void Close()
		{
			globalSFX.Dispatch("Play_menu_disappear_01");
			base.view.Close();
		}

		private void OnMenuClose()
		{
			hideSignal.Dispatch("BuildingSkrim");
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "screen_BaseResource");
		}

		private void UpdateDisplay()
		{
			base.view.UpdateDisplay();
		}

		private void LevelUnlockItems()
		{
			uint quantity = playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID);
			base.view.LevelUpUnlock(quantity);
		}
	}
}
