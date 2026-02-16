namespace Kampai.UI.View
{
	public class ItemListMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.ItemListView>
	{
		private global::UnityEngine.GameObject dragIcon;

		private bool placingNonIconBuilding;

		private bool placingIconBuilding;

		private bool dragingLockedItem = true;

		private global::UnityEngine.UI.ScrollRect scrollRect;

		private bool isDragging;

		private global::System.Collections.Generic.Dictionary<global::Kampai.Game.StoreItemType, global::Kampai.UI.View.StoreTab> storeTabs;

		private bool isSubMenuOpen;

		private global::System.Collections.Generic.Queue<global::UnityEngine.Vector2> HorizontalDrag;

		[Inject]
		public global::Kampai.Common.PickControllerModel model { get; set; }

		[Inject]
		public global::Kampai.UI.View.BuildMenuDefinitionLoadedSignal defLoadedSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.AddStoreTabSignal addTabSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.OnTabClickedSignal tabClickSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.MoveTabMenuSignal moveTabSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.MoveBuildMenuSignal moveBaseMenuSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.StoreButtonClickSignal storeButtonSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetNewUnlockForStoreTabSignal setNewUnlockForTabSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal audioSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject(global::Kampai.Main.MainElement.UI_GLASSCANVAS)]
		public global::UnityEngine.GameObject glassCanvas { get; set; }

		[Inject(global::Kampai.Main.MainElement.CAMERA)]
		public global::UnityEngine.Camera myCamera { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingUtilities buildingUtil { get; set; }

		[Inject]
		public global::Kampai.Game.DisableCameraBehaviourSignal disableCameraSignal { get; set; }

		[Inject]
		public global::Kampai.Game.EnableCameraBehaviourSignal enableCameraSignal { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.Common.DragAndDropPickSignal dragAndDropSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.BuildMenuOpenedSignal buildMenuOpened { get; set; }

		[Inject]
		public global::Kampai.UI.View.HighlightStoreItemSignal highlightStoreItemSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.DragFromStoreSignal dragFromStoreSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideStoreHighlightSignal hideHightlightSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateUIButtonsSignal updateStoreButtonsSignal { get; set; }

		[Inject]
		public global::Kampai.UI.IBuildMenuService buildMenuService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoZoomSignal autoZoomSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.PopupMessageSignal popupMessageSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Common.AppPauseSignal pauseSignal { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			base.view.Init();
			defLoadedSignal.AddListener(OnDefinitionLoaded);
			updateStoreButtonsSignal.AddListener(UpdateStoreButtons);
			addTabSignal.AddListener(AddStoreTab);
			tabClickSignal.AddListener(OnTabClicked);
			buildMenuOpened.AddListener(OnBuildMenuOpened);
			highlightStoreItemSignal.AddListener(HighlightStoreItem);
			hideHightlightSignal.AddListener(OnHideHighlight);
			base.view.Title.ClickedSignal.AddListener(OnItemMenuTitleClicked);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.PurchaseNewBuildingSignal>().AddListener(NewBuildingPurchased);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.CreateInventoryBuildingSignal>().AddListener(BuildingDraggedFromInventory);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.SendBuildingToInventorySignal>().AddListener(BuildingSentToInventory);
			scrollRect = base.view.ScrollViewParent.parent.GetComponent<global::UnityEngine.UI.ScrollRect>();
			storeTabs = new global::System.Collections.Generic.Dictionary<global::Kampai.Game.StoreItemType, global::Kampai.UI.View.StoreTab>();
			HorizontalDrag = new global::System.Collections.Generic.Queue<global::UnityEngine.Vector2>();
			moveBaseMenuSignal.AddListener(BaseMenuMoved);
			pauseSignal.AddListener(ResetIconDragState);
		}

		public override void OnRemove()
		{
			base.OnRemove();
			defLoadedSignal.RemoveListener(OnDefinitionLoaded);
			updateStoreButtonsSignal.RemoveListener(UpdateStoreButtons);
			addTabSignal.RemoveListener(AddStoreTab);
			tabClickSignal.RemoveListener(OnTabClicked);
			buildMenuOpened.RemoveListener(OnBuildMenuOpened);
			highlightStoreItemSignal.RemoveListener(HighlightStoreItem);
			hideHightlightSignal.RemoveListener(OnHideHighlight);
			base.view.Title.ClickedSignal.RemoveListener(OnItemMenuTitleClicked);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.PurchaseNewBuildingSignal>().RemoveListener(NewBuildingPurchased);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.CreateInventoryBuildingSignal>().RemoveListener(BuildingDraggedFromInventory);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.SendBuildingToInventorySignal>().RemoveListener(BuildingSentToInventory);
			moveBaseMenuSignal.RemoveListener(BaseMenuMoved);
			pauseSignal.RemoveListener(ResetIconDragState);
		}

		public void BaseMenuMoved(bool show)
		{
			if (!show && model.InvalidateMovement)
			{
				Close();
			}
		}

		internal void OnDefinitionLoaded(global::System.Collections.Generic.Dictionary<global::Kampai.Game.StoreItemType, global::System.Collections.Generic.List<global::Kampai.Game.Definition>> storeMenuDefs)
		{
			global::Kampai.UI.View.StoreButtonView storeButtonView = null;
			foreach (global::System.Collections.Generic.KeyValuePair<global::Kampai.Game.StoreItemType, global::System.Collections.Generic.List<global::Kampai.Game.Definition>> storeMenuDef in storeMenuDefs)
			{
				global::Kampai.Game.StoreItemType key = storeMenuDef.Key;
				storeMenuDef.Value.Sort(delegate(global::Kampai.Game.Definition x, global::Kampai.Game.Definition definition2)
				{
					global::Kampai.Game.StoreItemDefinition storeItemDefinition2 = x as global::Kampai.Game.StoreItemDefinition;
					global::Kampai.Game.StoreItemDefinition storeItemDefinition3 = definition2 as global::Kampai.Game.StoreItemDefinition;
					if (storeItemDefinition2 == null)
					{
						return 1;
					}
					if (storeItemDefinition3 == null)
					{
						return -1;
					}
					int levelItemUnlocksAt = definitionService.GetLevelItemUnlocksAt(storeItemDefinition2.ReferencedDefID);
					int levelItemUnlocksAt2 = definitionService.GetLevelItemUnlocksAt(storeItemDefinition3.ReferencedDefID);
					if (levelItemUnlocksAt < levelItemUnlocksAt2)
					{
						return -1;
					}
					return (levelItemUnlocksAt > levelItemUnlocksAt2) ? 1 : 0;
				});
				foreach (global::Kampai.Game.Definition item in storeMenuDef.Value)
				{
					global::Kampai.Game.StoreItemDefinition storeItemDefinition = item as global::Kampai.Game.StoreItemDefinition;
					if (storeItemDefinition != null)
					{
						global::Kampai.Game.Definition definition = definitionService.Get(storeItemDefinition.ReferencedDefID);
						if (definition != null)
						{
							global::Kampai.Game.Transaction.TransactionDefinition transaction = definitionService.Get(storeItemDefinition.TransactionID) as global::Kampai.Game.Transaction.TransactionDefinition;
							storeButtonView = global::Kampai.UI.View.StoreButtonBuilder.Build(definition, transaction, storeItemDefinition, base.view.ScrollViewParent, localService, definitionService, logger);
							storeButtonView.pointerDownSignal.AddListener(OnPointerDown);
							storeButtonView.pointerDragSignal.AddListener(OnPointerDrag);
							storeButtonView.pointerUpSignal.AddListener(OnPointerUp);
							base.view.AddStoreButton(key, storeButtonView);
						}
					}
				}
			}
			global::UnityEngine.RectTransform rectTransform = storeButtonView.transform as global::UnityEngine.RectTransform;
			float y = rectTransform.sizeDelta.y;
			float paddingInPixels = storeButtonView.PaddingInPixels;
			base.view.SetupButtonHeight(y, paddingInPixels);
			buildMenuService.RetoreBuidMenuState(base.view.GetAllButtonViews());
		}

		private bool ShouldRenderStoreDef(global::Kampai.Game.StoreItemDefinition storeDef)
		{
			global::System.Collections.Generic.ICollection<global::Kampai.Game.Building> byDefinitionId = playerService.GetByDefinitionId<global::Kampai.Game.Building>(storeDef.ReferencedDefID);
			foreach (global::Kampai.Game.Building item in byDefinitionId)
			{
				if (item.State == global::Kampai.Game.BuildingState.Inventory)
				{
					return true;
				}
			}
			return false;
		}

		private void NewBuildingPurchased(global::Kampai.Game.Building building)
		{
			global::Kampai.Game.StoreItemType type = base.view.UpdateStoreButtonState(building.Definition.ID, true, true);
			buildMenuService.RemoveNewUnlockedItem(type, building.Definition.ID);
			buildMenuService.DecreaseInventoryItemCountOnTab(type);
		}

		private void BuildingDraggedFromInventory(global::Kampai.Game.Building building, global::Kampai.Game.Location location)
		{
			global::Kampai.Game.StoreItemType type = base.view.UpdateStoreButtonState(building.Definition.ID, true, false);
			buildMenuService.RemoveNewUnlockedItem(type, building.Definition.ID);
			buildMenuService.DecreaseInventoryItemCountOnTab(type);
		}

		private void BuildingSentToInventory(int buildingInstanceID)
		{
			global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(buildingInstanceID);
			global::Kampai.Game.StoreItemType storeItemType = base.view.UpdateStoreButtonState(byInstanceId.Definition.ID, false, false);
			SetBadgeCountForStoreItemType(storeItemType);
			buildMenuService.IncreaseInventoryItemCountOnTab(storeItemType);
		}

		private void UpdateStoreButtons()
		{
			buildMenuService.ClearAllNewUnlockItems();
			buildMenuService.UpdateNewUnlockList(base.view.GetAllButtonViews());
		}

		internal void OnHideHighlight()
		{
			foreach (global::Kampai.Game.StoreItemType key in storeTabs.Keys)
			{
				global::System.Collections.Generic.List<global::Kampai.UI.View.StoreButtonView> storeButtonViews = base.view.GetStoreButtonViews(key);
				foreach (global::Kampai.UI.View.StoreButtonView item in storeButtonViews)
				{
					item.SetHighlight(false);
				}
			}
		}

		internal void OnItemMenuTitleClicked()
		{
			moveTabSignal.Dispatch(true);
			isSubMenuOpen = false;
			base.view.MoveSubMenu(false);
		}

		protected override void Close()
		{
			OnItemMenuTitleClicked();
		}

		internal void AddStoreTab(global::Kampai.UI.View.StoreTab tab)
		{
			storeTabs.Add(tab.Type, tab);
		}

		internal void OnTabClicked(global::Kampai.Game.StoreItemType type, string localizedTitle)
		{
			base.view.TabIcon.maskSprite = global::Kampai.UI.View.StoreTabBuilder.SetTabIcon(type);
			global::UnityEngine.RectTransform rectTransform = scrollRect.content.transform as global::UnityEngine.RectTransform;
			rectTransform.anchoredPosition = global::UnityEngine.Vector2.zero;
			RefreshWhatButtonsShouldBeVisible(type);
			if (base.view.SetupItemMenu(type, localizedTitle))
			{
				moveTabSignal.Dispatch(false);
				audioSignal.Dispatch("Play_shop_pane_in_01");
				base.view.MoveSubMenu(true);
				isSubMenuOpen = true;
			}
			else
			{
				audioSignal.Dispatch("Play_action_locked_01");
			}
			if (type == global::Kampai.Game.StoreItemType.BaseResource && questService.GetQuestMap().ContainsKey(2000000002) && questService.GetQuestMap()[2000000002].state == global::Kampai.Game.QuestState.RunningTasks && playerService.GetInstancesByDefinitionID(3015).Count == 0)
			{
				popupMessageSignal.Dispatch(localService.GetString("BuildingHelperDialog"));
			}
		}

		private void RefreshWhatButtonsShouldBeVisible(global::Kampai.Game.StoreItemType type)
		{
			global::System.Collections.Generic.List<global::Kampai.UI.View.StoreButtonView> storeButtonViews = base.view.GetStoreButtonViews(type);
			if (storeButtonViews == null)
			{
				return;
			}
			foreach (global::Kampai.UI.View.StoreButtonView item in storeButtonViews)
			{
				if (item.storeItemDefinition.OnlyShowIfInInventory)
				{
					bool shouldBerendered = ShouldRenderStoreDef(item.storeItemDefinition);
					item.SetShouldBerendered(shouldBerendered);
				}
			}
		}

		private void OnBuildMenuOpened()
		{
			foreach (global::Kampai.Game.StoreItemType key in storeTabs.Keys)
			{
				SetBadgeCountForStoreItemType(key);
				RefreshWhatButtonsShouldBeVisible(key);
			}
			if (isSubMenuOpen)
			{
				base.view.RefreshStoreButtonLayout();
			}
		}

		internal void SetBadgeCountForStoreItemType(global::Kampai.Game.StoreItemType type)
		{
			global::System.Collections.Generic.List<global::Kampai.UI.View.StoreButtonView> storeButtonViews = base.view.GetStoreButtonViews(type);
			foreach (global::Kampai.UI.View.StoreButtonView item in storeButtonViews)
			{
				int iD = item.definition.ID;
				int inventoryCountByDefinitionID = playerService.GetInventoryCountByDefinitionID(iD);
				item.SetBadge(inventoryCountByDefinitionID);
			}
		}

		protected override void OnCloseAllMenu(global::UnityEngine.GameObject exception)
		{
			ResetIconDragState();
		}

		private void ResetIconDragState()
		{
			if (dragIcon != null)
			{
				base.view.Title.ClickedSignal.AddListener(OnItemMenuTitleClicked);
				isDragging = false;
				global::UnityEngine.Object.Destroy(dragIcon);
				dragIcon = null;
				OnHideHighlight();
			}
		}

		internal void HighlightStoreItem(global::Kampai.Game.StoreItemDefinition definition)
		{
			if (!storeTabs.ContainsKey(definition.Type))
			{
				return;
			}
			global::Kampai.UI.View.StoreTab storeTab = storeTabs[definition.Type];
			OnTabClicked(storeTab.Type, storeTab.LocalizedName);
			global::System.Collections.Generic.List<global::Kampai.UI.View.StoreButtonView> storeButtonViews = base.view.GetStoreButtonViews(definition.Type);
			float num = 0f;
			foreach (global::Kampai.UI.View.StoreButtonView item in storeButtonViews)
			{
				if (item.gameObject.activeSelf)
				{
					global::UnityEngine.RectTransform rectTransform = item.transform as global::UnityEngine.RectTransform;
					item.SetHighlight(false);
					if (item.definition.ID == definition.ReferencedDefID)
					{
						item.SetHighlight(true);
						global::UnityEngine.RectTransform rectTransform2 = scrollRect.content.transform as global::UnityEngine.RectTransform;
						rectTransform2.anchoredPosition = new global::UnityEngine.Vector2(0f, num);
						break;
					}
					num += rectTransform.rect.height + item.PaddingInPixels;
				}
			}
		}

		private void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData, global::Kampai.Game.Definition definition, global::Kampai.Game.Transaction.TransactionDefinition transactionDef, bool canPurchase)
		{
			placingNonIconBuilding = false;
			placingIconBuilding = false;
			if (global::UnityEngine.Input.touchCount > 1)
			{
				if (dragIcon != null)
				{
					global::UnityEngine.Object.Destroy(dragIcon);
					dragIcon = null;
					base.view.Title.ClickedSignal.AddListener(OnItemMenuTitleClicked);
				}
				return;
			}
			isDragging = true;
			if (!canPurchase)
			{
				audioSignal.Dispatch("Play_action_locked_01");
				dragIcon = null;
				scrollRect.OnBeginDrag(eventData);
				dragingLockedItem = true;
				return;
			}
			audioSignal.Dispatch("Play_button_click_01");
			dragingLockedItem = false;
			if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.name.CompareTo("img_ItemIcon") == 0 && dragIcon == null)
			{
				popoutIcon(definition, eventData);
			}
			else
			{
				scrollRect.OnBeginDrag(eventData);
			}
		}

		private void popoutIcon(global::Kampai.Game.Definition definition, global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Kampai.Game.DisplayableDefinition displayableDefinition = definition as global::Kampai.Game.DisplayableDefinition;
			base.view.Title.ClickedSignal.RemoveListener(OnItemMenuTitleClicked);
			dragIcon = new global::UnityEngine.GameObject("DragIcon");
			dragIcon.transform.SetParent(glassCanvas.transform);
			dragIcon.layer = 5;
			global::Kampai.UI.View.KampaiIngoreRaycastImage kampaiIngoreRaycastImage = dragIcon.AddComponent<global::Kampai.UI.View.KampaiIngoreRaycastImage>();
			kampaiIngoreRaycastImage.sprite = UIUtils.LoadSpriteFromPath(displayableDefinition.Image);
			kampaiIngoreRaycastImage.material = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.Material>("CircleIconAlphaMaskMat");
			kampaiIngoreRaycastImage.maskSprite = UIUtils.LoadSpriteFromPath(displayableDefinition.Mask);
			global::UnityEngine.RectTransform component = dragIcon.GetComponent<global::UnityEngine.RectTransform>();
			component.localPosition = new global::UnityEngine.Vector3(0f, 0f, 0f);
			global::Kampai.UI.View.KampaiImage component2 = dragIcon.GetComponent<global::Kampai.UI.View.KampaiImage>();
			component.anchoredPosition = new global::UnityEngine.Vector2(eventData.position.x / UIUtils.GetHeightScale(), eventData.position.y / UIUtils.GetHeightScale() + component2.sprite.rect.height * component2.pixelsPerUnit / 2f);
			component.localScale = global::UnityEngine.Vector3.one;
			component.anchorMin = new global::UnityEngine.Vector2(0f, 0f);
			component.anchorMax = new global::UnityEngine.Vector2(0f, 0f);
			component.pivot = new global::UnityEngine.Vector2(0.5f, 0.5f);
			float num = global::UnityEngine.Mathf.Max(component2.sprite.rect.width, component2.sprite.rect.height);
			component.sizeDelta = new global::UnityEngine.Vector2(component2.sprite.rect.width / num * 100f, component2.sprite.rect.height / num * 100f);
		}

		private void OnPointerDrag(global::UnityEngine.EventSystems.PointerEventData eventData, global::Kampai.Game.Definition definition, global::Kampai.Game.Transaction.TransactionDefinition transactionDef, int badgeCount)
		{
			if (isDragging && dragIcon == null)
			{
				scrollRect.OnDrag(eventData);
			}
			if (dragingLockedItem)
			{
				return;
			}
			float num = 0.4f;
			int num2 = 10;
			float num3 = 0.3f;
			float num4 = 3f;
			if (dragIcon == null)
			{
				if (HorizontalDrag.Count >= num2)
				{
					HorizontalDrag.Dequeue();
				}
				HorizontalDrag.Enqueue(eventData.position);
				float num5 = 0f;
				float num6 = 0f;
				global::UnityEngine.Vector2[] array = HorizontalDrag.ToArray();
				for (int i = 0; i < HorizontalDrag.Count; i++)
				{
					if (i != 0)
					{
						num6 += array[i].x - array[i - 1].x;
						num5 += array[i].y - array[i - 1].y;
					}
				}
				float value = ((!(num6 <= 0.001f)) ? (num5 / num6) : 100f);
				if (global::System.Math.Abs(value) <= num3 && num6 >= num4 && !placingIconBuilding && !placingNonIconBuilding)
				{
					popoutIcon(definition, eventData);
				}
				if (eventData.pointerCurrentRaycast.gameObject == null && !placingNonIconBuilding && !dragingLockedItem && dragIcon == null && !placingIconBuilding)
				{
					isDragging = false;
					scrollRect.OnEndDrag(eventData);
					dragFromStoreSignal.Dispatch(definition, transactionDef, eventData.position, badgeCount);
					moveBaseMenuSignal.Dispatch(false);
					placingNonIconBuilding = true;
					float currentPercentage = myCamera.GetComponent<global::Kampai.Game.View.ZoomView>().GetCurrentPercentage();
					if (currentPercentage > num)
					{
						autoZoomSignal.Dispatch(num);
					}
				}
			}
			else if (eventData.pointerCurrentRaycast.gameObject == null)
			{
				if (dragIcon != null && !placingIconBuilding && !placingNonIconBuilding)
				{
					base.view.Title.ClickedSignal.AddListener(OnItemMenuTitleClicked);
					isDragging = false;
					global::UnityEngine.Object.Destroy(dragIcon);
					dragIcon = null;
					placingIconBuilding = true;
					float currentPercentage2 = myCamera.GetComponent<global::Kampai.Game.View.ZoomView>().GetCurrentPercentage();
					if (currentPercentage2 > num)
					{
						autoZoomSignal.Dispatch(num);
					}
					scrollRect.OnEndDrag(eventData);
					moveBaseMenuSignal.Dispatch(false);
					dragFromStoreSignal.Dispatch(definition, transactionDef, eventData.position, badgeCount);
				}
			}
			else if (dragIcon != null)
			{
				setDragIconPosition(eventData);
			}
		}

		private void setDragIconPosition(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::UnityEngine.RectTransform component = dragIcon.GetComponent<global::UnityEngine.RectTransform>();
			component.anchoredPosition = eventData.position / UIUtils.GetHeightScale();
			global::Kampai.UI.View.KampaiImage component2 = dragIcon.GetComponent<global::Kampai.UI.View.KampaiImage>();
			component.anchoredPosition = new global::UnityEngine.Vector2(component.anchoredPosition.x, component.anchoredPosition.y + component2.sprite.rect.height * component2.pixelsPerUnit / 2f);
		}

		private void OnPointerUp(global::UnityEngine.EventSystems.PointerEventData eventData, global::Kampai.Game.Definition definition, global::Kampai.Game.Transaction.TransactionDefinition transactionDef)
		{
			dragingLockedItem = true;
			placingIconBuilding = false;
			placingNonIconBuilding = false;
			HorizontalDrag = new global::System.Collections.Generic.Queue<global::UnityEngine.Vector2>();
			if (dragIcon != null)
			{
				base.view.Title.ClickedSignal.AddListener(OnItemMenuTitleClicked);
				isDragging = false;
				global::UnityEngine.Object.Destroy(dragIcon);
				dragIcon = null;
			}
			else
			{
				scrollRect.OnEndDrag(eventData);
			}
		}
	}
}
