namespace Kampai.UI.View
{
	public class CraftingModalMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.CraftingModalView>
	{
		private global::System.Collections.Generic.List<global::Kampai.Game.CraftingBuilding> validCraftingBuildingList = new global::System.Collections.Generic.List<global::Kampai.Game.CraftingBuilding>();

		private int currentIndex;

		private int craftingBuildingCount;

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RefreshQueueSlotSignal refreshSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CraftingModalClosedSignal closedSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CraftingQueuePositionUpdateSignal queuePositionSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideHUDAndIconsSignal hideHUDSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideItemPopupSignal closeItemPopupSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ResetDoubleTapSignal resetDoubleTapSignal { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			base.view.OnMenuClose.AddListener(OnMenuClose);
			base.view.backArrow.ClickedSignal.AddListener(BackArrow);
			base.view.forwardArrow.ClickedSignal.AddListener(ForwardArrow);
			refreshSignal.AddListener(RefreshQueue);
			queuePositionSignal.AddListener(UpdateQueuePosition);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.CameraAutoPanCompleteSignal>().AddListener(PanComplete);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.ToggleVignetteSignal>().Dispatch(true, null);
			resetDoubleTapSignal.AddListener(ResetDoubleTap);
			hideHUDSignal.Dispatch(false);
		}

		public override void OnRemove()
		{
			base.OnRemove();
			base.view.OnMenuClose.RemoveListener(OnMenuClose);
			base.view.backArrow.ClickedSignal.RemoveListener(BackArrow);
			base.view.forwardArrow.ClickedSignal.RemoveListener(ForwardArrow);
			refreshSignal.RemoveListener(RefreshQueue);
			queuePositionSignal.RemoveListener(UpdateQueuePosition);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.CameraAutoPanCompleteSignal>().RemoveListener(PanComplete);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.ToggleVignetteSignal>().Dispatch(false, null);
			resetDoubleTapSignal.RemoveListener(ResetDoubleTap);
			hideHUDSignal.Dispatch(true);
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			int num = args.Get<int>();
			Init(num);
			InitCraftingBuildingList(num);
		}

		protected override void Close()
		{
			closeItemPopupSignal.Dispatch();
			soundFXSignal.Dispatch("Play_menu_disappear_01");
			base.view.Close();
		}

		private void RefreshQueue(bool purchasing)
		{
			if (purchasing)
			{
				playerService.BuyCraftingSlot(base.view.building.ID);
			}
			base.view.RefreshQueue();
		}

		private void UpdateQueuePosition()
		{
			base.view.UpdateQueuePosition();
		}

		private void BackArrow()
		{
			if (currentIndex <= 0)
			{
				currentIndex = craftingBuildingCount - 1;
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
			global::Kampai.Game.CraftingBuilding craftingBuilding = validCraftingBuildingList[currentIndex];
			base.view.RePopulateModal(craftingBuilding);
			base.view.SetTitle(localService.GetString(craftingBuilding.Definition.LocalizedKey));
			PanAndShowBuildingMenu(craftingBuilding);
		}

		private void PanComplete()
		{
			base.view.SetArrowButtonState(true);
		}

		private void ForwardArrow()
		{
			if (currentIndex >= craftingBuildingCount - 1)
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
			}
		}

		private void InitCraftingBuildingList(int craftingBuildingID)
		{
			global::System.Collections.Generic.List<global::Kampai.Game.CraftingBuilding> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.CraftingBuilding>();
			for (int i = 0; i < instancesByType.Count; i++)
			{
				global::Kampai.Game.CraftingBuilding craftingBuilding = instancesByType[i];
				if (craftingBuilding.State != global::Kampai.Game.BuildingState.Construction && craftingBuilding.State != global::Kampai.Game.BuildingState.Inventory && craftingBuilding.State != global::Kampai.Game.BuildingState.Inactive && craftingBuilding.State != global::Kampai.Game.BuildingState.Cooldown && craftingBuilding.State != global::Kampai.Game.BuildingState.Complete)
				{
					if (craftingBuildingID == craftingBuilding.ID)
					{
						currentIndex = validCraftingBuildingList.Count;
					}
					validCraftingBuildingList.Add(craftingBuilding);
				}
			}
			craftingBuildingCount = validCraftingBuildingList.Count;
			if (craftingBuildingCount <= 1)
			{
				base.view.backArrow.gameObject.SetActive(false);
				base.view.forwardArrow.gameObject.SetActive(false);
			}
		}

		private void OnMenuClose()
		{
			closedSignal.Dispatch();
			hideSignal.Dispatch("CraftingSkrim");
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "screen_CraftingMenu");
		}

		private void ResetDoubleTap(int id)
		{
			base.view.ResetDoubleTap(id);
		}

		private void Init(int buildingID)
		{
			global::Kampai.Game.CraftingBuilding byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.CraftingBuilding>(buildingID);
			base.view.Init(definitionService, questService, byInstanceId);
			base.view.SetTitle(localService.GetString(byInstanceId.Definition.LocalizedKey));
		}
	}
}
