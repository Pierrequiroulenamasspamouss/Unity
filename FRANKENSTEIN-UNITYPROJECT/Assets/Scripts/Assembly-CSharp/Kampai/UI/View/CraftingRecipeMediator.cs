namespace Kampai.UI.View
{
	public class CraftingRecipeMediator : global::strange.extensions.mediation.impl.Mediator
	{
		private GoTween tween;

		private global::UnityEngine.GameObject dragIcon;

		private global::UnityEngine.GameObject dragGlow;

		private global::UnityEngine.GameObject dragPrefab;

		private global::Kampai.Game.CraftingBuilding craftingBuilding;

		private global::Kampai.Game.IngredientsItemDefinition currentItemDef;

		private global::UnityEngine.RectTransform dragTransform;

		private global::UnityEngine.Vector2 initialIconPosition;

		private bool midDrag;

		private global::System.Collections.IEnumerator PointerDownWait;

		[Inject]
		public global::Kampai.UI.View.CraftingRecipeView view { get; set; }

		[Inject(global::Kampai.Main.MainElement.UI_GLASSCANVAS)]
		public global::UnityEngine.GameObject glassCanvas { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal playSFXSignal { get; set; }

		[Inject]
		public global::Kampai.Common.AppPauseSignal pauseSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingChangeStateSignal changeStateSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CraftingCompleteSignal craftingComplete { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateQueueIcon updateQueueSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CraftingQueuePositionUpdateSignal queuePositionSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RefreshDynamicRecipeSignal refreshDynamicRecipeSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CraftingModalClosedSignal closedSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CraftingRecipeUpdateSignal updateSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CraftingUpdateReagentsSignal craftingUpdateReagentsSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.DisplayItemPopupSignal displayItemPopupSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideItemPopupSignal hideItemPopupSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RushDialogConfirmationSignal rushedSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ResetDoubleTapSignal resetDoubleTapSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.UI.View.PopupMessageSignal popupMessageSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CraftingRecipeDragStartSignal dragStartSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CraftingRecipeDragStopSignal dragStopSignal { get; set; }

		public override void OnRegister()
		{
			pauseSignal.AddListener(OnPause);
			closedSignal.AddListener(HandleClose);
			updateSignal.AddListener(OnUpdate);
			craftingUpdateReagentsSignal.AddListener(UpdateReagents);
			refreshDynamicRecipeSignal.AddListener(DisableDynamic);
			rushedSignal.AddListener(ItemRushed);
			hideItemPopupSignal.AddListener(RemovePopupDelay);
			view.pointerDownSignal.AddListener(PointerDown);
			view.pointerDragSignal.AddListener(PointerDrag);
			view.pointerUpSignal.AddListener(PointerUp);
			Init();
		}

		public override void OnRemove()
		{
			pauseSignal.RemoveListener(OnPause);
			closedSignal.RemoveListener(HandleClose);
			updateSignal.RemoveListener(OnUpdate);
			craftingUpdateReagentsSignal.RemoveListener(UpdateReagents);
			refreshDynamicRecipeSignal.RemoveListener(DisableDynamic);
			rushedSignal.RemoveListener(ItemRushed);
			hideItemPopupSignal.RemoveListener(RemovePopupDelay);
			view.pointerDownSignal.RemoveListener(PointerDown);
			view.pointerDragSignal.RemoveListener(PointerDrag);
			view.pointerUpSignal.RemoveListener(PointerUp);
		}

		private void Init()
		{
			view.Init(definitionService, playerService);
			craftingBuilding = playerService.GetByInstanceId<global::Kampai.Game.CraftingBuilding>(view.instanceID);
			dragPrefab = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.GameObject>("cmp_DragIcon");
		}

		private void OnPause()
		{
			if (midDrag && dragIcon != null)
			{
				HandleClose();
			}
			hideItemPopupSignal.Dispatch();
		}

		private void DisableDynamic(int ID)
		{
			view.DisableDynamic(ID);
		}

		private void PointerDown(global::UnityEngine.EventSystems.PointerEventData eventData, global::Kampai.Game.IngredientsItemDefinition iid)
		{
			resetDoubleTapSignal.Dispatch(-1);
			hideItemPopupSignal.Dispatch();
			displayItemPopupSignal.Dispatch(iid.ID, view.unlockedImage.rectTransform, global::Kampai.UI.View.UIPopupType.CRAFTING);
			if (IsPointerDownValid())
			{
				global::UnityEngine.GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;
				if (!(gameObject == null))
				{
					currentItemDef = null;
					playSFXSignal.Dispatch("Play_pick_item_01");
					dragIcon = global::UnityEngine.Object.Instantiate(dragPrefab) as global::UnityEngine.GameObject;
					dragIcon.transform.SetParent(glassCanvas.transform, false);
					global::Kampai.UI.View.KampaiIngoreRaycastImage component = dragIcon.transform.Find("img_RecipeItem").gameObject.GetComponent<global::Kampai.UI.View.KampaiIngoreRaycastImage>();
					component.sprite = UIUtils.LoadSpriteFromPath(iid.Image);
					component.maskSprite = UIUtils.LoadSpriteFromPath(iid.Mask);
					dragGlow = dragIcon.transform.Find("backing_glow").gameObject;
					dragTransform = dragIcon.GetComponent<global::UnityEngine.RectTransform>();
					dragTransform.localPosition = global::UnityEngine.Vector3.zero;
					dragTransform.localScale = new global::UnityEngine.Vector3(1.25f, 1.25f, 1.25f);
					dragTransform.anchorMin = global::UnityEngine.Vector2.zero;
					dragTransform.anchorMax = global::UnityEngine.Vector2.zero;
					dragTransform.pivot = new global::UnityEngine.Vector2(0.5f, 0.5f);
					dragTransform.sizeDelta = new global::UnityEngine.Vector2(100f, 100f);
					dragTransform.anchoredPosition = eventData.position / UIUtils.GetHeightScale();
					initialIconPosition = eventData.position / UIUtils.GetHeightScale();
					midDrag = true;
					dragStartSignal.Dispatch(iid.ID);
				}
			}
		}

		private bool IsPointerDownValid()
		{
			if (global::UnityEngine.Input.touchCount > 1)
			{
				return false;
			}
			if (midDrag)
			{
				return false;
			}
			if (!view.isUnlocked)
			{
				playSFXSignal.Dispatch("Play_action_locked_01");
				return false;
			}
			return true;
		}

		private void PointerDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (midDrag && dragIcon != null)
			{
				dragTransform.anchoredPosition = eventData.position / UIUtils.GetHeightScale();
				global::UnityEngine.GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;
				if (gameObject != null && gameObject.name.Equals("DragArea"))
				{
					dragGlow.SetActive(true);
				}
				else
				{
					dragGlow.SetActive(false);
				}
			}
		}

		private void PointerUp(global::UnityEngine.EventSystems.PointerEventData eventData, global::Kampai.Game.IngredientsItemDefinition itemDef)
		{
			if (PointerDownWait != null)
			{
				return;
			}
			PointerDownWait = PopupDelay();
			routineRunner.StartCoroutine(PointerDownWait);
			if (!midDrag)
			{
				return;
			}
			global::UnityEngine.GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;
			if (gameObject != null)
			{
				bool flag = gameObject.name == "DragArea";
				global::Kampai.UI.View.CraftingQueueView craftingQueueView = (flag ? null : gameObject.GetComponentInParent<global::Kampai.UI.View.CraftingQueueView>());
				if (flag || craftingQueueView != null)
				{
					if (craftingBuilding.RecipeInQueue.Count < craftingBuilding.Slots)
					{
						if (flag || (!craftingQueueView.isLocked && craftingQueueView.index >= craftingBuilding.RecipeInQueue.Count))
						{
							playSFXSignal.Dispatch("Play_place_item_01");
							currentItemDef = itemDef;
							RunTransaction();
							return;
						}
					}
					else
					{
						popupMessageSignal.Dispatch(localService.GetString("CraftQueueFull"));
					}
				}
			}
			TweenBackToOrigin();
		}

		private void TweenBackToOrigin()
		{
			tween = Go.to(dragTransform, 0.25f, new GoTweenConfig().setEaseType(GoEaseType.Linear).vector2Prop("anchoredPosition", initialIconPosition).onComplete(delegate
			{
				HandleClose();
			}));
		}

		private global::System.Collections.IEnumerator PopupDelay()
		{
			yield return new global::UnityEngine.WaitForSeconds(0.5f);
			if (PointerDownWait != null)
			{
				hideItemPopupSignal.Dispatch();
			}
		}

		private void RemovePopupDelay()
		{
			if (PointerDownWait != null)
			{
				routineRunner.StopCoroutine(PointerDownWait);
				PointerDownWait = null;
			}
		}

		private void ItemRushed()
		{
			if (currentItemDef != null && craftingBuilding != null)
			{
				RunTransaction();
			}
		}

		private void RunTransaction()
		{
			playerService.StartTransaction(currentItemDef.TransactionId, global::Kampai.Game.TransactionTarget.INGREDIENT, TransactionCallback, new global::Kampai.Game.TransactionArg(craftingBuilding.ID));
		}

		private void TransactionCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			if (pct.Success)
			{
				int iD = craftingBuilding.ID;
				if (craftingBuilding.RecipeInQueue.Count == 0)
				{
					timeEventService.AddEvent(iD, global::System.Convert.ToInt32(timeService.GameTimeSeconds()), (int)currentItemDef.TimeToHarvest, craftingComplete);
					craftingBuilding.CraftingStartTime = timeService.GameTimeSeconds();
					changeStateSignal.Dispatch(iD, global::Kampai.Game.BuildingState.Working);
				}
				global::Kampai.Game.DynamicIngredientsDefinition definition;
				if (definitionService.TryGet<global::Kampai.Game.DynamicIngredientsDefinition>(currentItemDef.ID, out definition))
				{
					int iD2 = definition.ID;
					craftingBuilding.DynamicCrafts.Add(iD2);
					if (iD2 == view.recipeID)
					{
						view.gameObject.SetActive(false);
					}
					refreshDynamicRecipeSignal.Dispatch(iD2);
				}
				playerService.UpdateCraftingQueue(iD, currentItemDef.ID);
				updateQueueSignal.Dispatch();
				queuePositionSignal.Dispatch();
				craftingUpdateReagentsSignal.Dispatch();
				HandleClose();
			}
			else if (pct.ParentSuccess)
			{
				RunTransaction();
			}
			else
			{
				HandleClose();
			}
			currentItemDef = null;
		}

		private void OnUpdate(int recipeDefId)
		{
			if (recipeDefId == view.recipeID && view.isUnlocked)
			{
				view.SetQuantity();
			}
		}

		private void UpdateReagents()
		{
			view.SetImageBorder();
		}

		private void HandleClose()
		{
			if (midDrag)
			{
				midDrag = false;
				global::UnityEngine.Object.Destroy(dragIcon);
				if (tween != null)
				{
					tween.destroy();
				}
				dragStopSignal.Dispatch(view.recipeID);
			}
		}
	}
}
