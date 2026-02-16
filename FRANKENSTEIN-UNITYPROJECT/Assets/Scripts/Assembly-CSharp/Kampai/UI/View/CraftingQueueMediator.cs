namespace Kampai.UI.View
{
	public class CraftingQueueMediator : global::strange.extensions.mediation.impl.Mediator
	{
		private bool isMidRecipeDrag;

		private GoTween activeScaleTween;

		[Inject]
		public global::Kampai.UI.View.CraftingQueueView view { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.UI.View.SpawnDooberSignal tweenSignal { get; set; }

		[Inject(global::Kampai.UI.View.UIElement.CAMERA)]
		public global::UnityEngine.Camera uiCamera { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateQueueIcon updateQueueSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal globalSFXSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetPremiumCurrencySignal setPremiumCurrencySignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RemoveCraftingQueueSignal removeCraftingQueueSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RefreshQueueSlotSignal purchaseSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ResetDoubleTapSignal resetDoubleTapSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CraftingRecipeDragStartSignal recipeDragStartSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CraftingRecipeDragStopSignal recipeDragStopSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.UI.View.CraftingRecipeUpdateSignal recipeUpdateSignal { get; set; }

		public override void OnRegister()
		{
			view.Init(definitionService, timeEventService);
			view.inProgressRush.ClickedSignal.AddListener(RushButton);
			view.lockedPurchase.ClickedSignal.AddListener(UnlockButton);
			updateQueueSignal.AddListener(UpdateView);
			recipeDragStartSignal.AddListener(OnRecipeDragStart);
			recipeDragStopSignal.AddListener(OnRecipeDragStop);
			view.onPointerEnterSignal.AddListener(OnPointerEnter);
			view.onPointerExitSignal.AddListener(OnPointerExit);
		}

		public override void OnRemove()
		{
			view.inProgressRush.ClickedSignal.RemoveListener(RushButton);
			view.lockedPurchase.ClickedSignal.RemoveListener(UnlockButton);
			updateQueueSignal.RemoveListener(UpdateView);
			recipeDragStartSignal.RemoveListener(OnRecipeDragStart);
			recipeDragStopSignal.RemoveListener(OnRecipeDragStop);
			view.onPointerEnterSignal.RemoveListener(OnPointerEnter);
			view.onPointerExitSignal.RemoveListener(OnPointerExit);
		}

		private void UpdateView()
		{
			view.Init(definitionService, timeEventService);
		}

		private void RushButton()
		{
			if (global::UnityEngine.Input.touchCount <= 1)
			{
				resetDoubleTapSignal.Dispatch(view.index);
				if (view.isLocked)
				{
					playerService.ProcessRush(view.purchaseCost, true, PurchaseTransactionCallback);
				}
				else if (!playerService.isStorageFull() && view.inProgressRush.isDoubleConfirmed())
				{
					playerService.ProcessRush(view.rushCost, true, RushTransactionCallback, view.itemDef.ID);
				}
			}
		}

		private void UnlockButton()
		{
			if (view.lockedPurchase.isDoubleConfirmed() && view.isLocked)
			{
				playerService.ProcessSlotPurchase(view.purchaseCost, true, view.index + 1, PurchaseTransactionCallback, view.building.ID);
			}
		}

		private void RushTransactionCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			if (pct.Success)
			{
				view.building.LastRushedSlot = view.index;
				globalSFXSignal.Dispatch("Play_button_premium_01");
				if (view.inProduction)
				{
					HandleTween(uiCamera.WorldToScreenPoint(view.inProgressImage.transform.position));
					timeEventService.RushEvent(view.building.ID);
				}
				else
				{
					HandleTween(uiCamera.WorldToScreenPoint(view.availableImage.transform.position));
					removeCraftingQueueSignal.Dispatch(new global::Kampai.Util.Tuple<int, int>(view.building.ID, view.index));
					setPremiumCurrencySignal.Dispatch();
				}
			}
		}

		private void HandleTween(global::UnityEngine.Vector3 origin)
		{
			global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition = definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(view.itemDef.TransactionId);
			foreach (global::Kampai.Util.QuantityItem output in transactionDefinition.Outputs)
			{
				tweenSignal.Dispatch(origin, DooberUtil.GetDestinationType(output.ID, definitionService), output.ID, false);
			}
			routineRunner.DelayAction(new global::UnityEngine.WaitForSeconds(2.5f), delegate
			{
				recipeUpdateSignal.Dispatch(view.itemDef.ID);
			});
		}

		private void PurchaseTransactionCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			if (pct.Success)
			{
				globalSFXSignal.Dispatch("Play_button_premium_01");
				purchaseSignal.Dispatch(true);
				setPremiumCurrencySignal.Dispatch();
			}
			else if (pct.ParentSuccess)
			{
				RushButton();
			}
		}

		private void OnRecipeDragStart(int recipeDefId)
		{
			isMidRecipeDrag = true;
		}

		private void OnRecipeDragStop(int recipeDefId)
		{
			isMidRecipeDrag = false;
			TweenScale(false);
		}

		private void OnPointerEnter(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (isMidRecipeDrag && view.index >= view.building.RecipeInQueue.Count && !view.isLocked)
			{
				TweenScale(true);
			}
		}

		private void OnPointerExit(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			TweenScale(false);
		}

		private void TweenScale(bool isFocused)
		{
			if (activeScaleTween != null)
			{
				activeScaleTween.destroy();
			}
			global::UnityEngine.Vector3 one = global::UnityEngine.Vector3.one;
			if (view.isLocked || view.index > 0)
			{
				one *= 0.8f;
			}
			if (isMidRecipeDrag && isFocused)
			{
				one *= 1.15f;
			}
			if (one != view.transform.localScale)
			{
				activeScaleTween = Go.to(view.transform, 0.2f, new GoTweenConfig().scale(one).onComplete(delegate(AbstractGoTween tween)
				{
					tween.destroy();
					activeScaleTween = null;
				}));
			}
		}
	}
}
