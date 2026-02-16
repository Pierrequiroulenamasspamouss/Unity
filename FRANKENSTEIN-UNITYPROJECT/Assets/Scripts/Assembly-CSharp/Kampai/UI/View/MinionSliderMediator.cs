namespace Kampai.UI.View
{
	public class MinionSliderMediator : global::strange.extensions.mediation.impl.Mediator
	{
		[Inject]
		public global::Kampai.UI.View.MinionSliderView view { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateSliderSignal updateSliderSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ResetDoubleTapSignal resetDoubleTapSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal globalSFXSignal { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

		[Inject]
		public global::Kampai.Game.CallMinionSignal callMinionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.FinishCallMinionSignal finishCallMinionSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.UI.View.UITryHarvestSignal tryHarvestSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetPremiumCurrencySignal setPremiumCurrencySignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateIdleMinionCountSignal updateMinionCountSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateSlotPurchaseButtonSignal updatePurchaseSlotSignal { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingHarvestSignal buildingHarvestSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		public override void OnRegister()
		{
			view.Init(localService, definitionService, routineRunner);
			view.rushButton.ClickedSignal.AddListener(ProcessClick);
			view.callButton.ClickedSignal.AddListener(ProcessClick);
			view.harvestButton.ClickedSignal.AddListener(ProcessClick);
			view.lockedButton.ClickedSignal.AddListener(ProcessClick);
			finishCallMinionSignal.AddListener(FinishCallMinion);
			updateMinionCountSignal.AddListener(UpdateMinionCount);
			updatePurchaseSlotSignal.AddListener(UpdatePurchaseSlot);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.IdleMinionSignal>().AddListener(UpdateMinionCount);
		}

		public override void OnRemove()
		{
			view.rushButton.ClickedSignal.RemoveListener(ProcessClick);
			view.callButton.ClickedSignal.RemoveListener(ProcessClick);
			view.harvestButton.ClickedSignal.RemoveListener(ProcessClick);
			view.lockedButton.ClickedSignal.RemoveListener(ProcessClick);
			finishCallMinionSignal.RemoveListener(FinishCallMinion);
			updateMinionCountSignal.RemoveListener(UpdateMinionCount);
			updatePurchaseSlotSignal.RemoveListener(UpdatePurchaseSlot);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.IdleMinionSignal>().RemoveListener(UpdateMinionCount);
		}

		private void ProcessClick()
		{
			resetDoubleTapSignal.Dispatch(view.identifier);
			if (view.rushButton.GetComponent<global::UnityEngine.UI.Button>().interactable || view.callButton.GetComponent<global::UnityEngine.UI.Button>().interactable)
			{
				switch (view.state)
				{
				case global::Kampai.UI.View.MinionSliderState.Working:
					RushMinion();
					break;
				case global::Kampai.UI.View.MinionSliderState.Available:
					CallMinion();
					break;
				case global::Kampai.UI.View.MinionSliderState.Harvestable:
					Harvest();
					break;
				case global::Kampai.UI.View.MinionSliderState.Locked:
					PurchaseSlot();
					break;
				}
			}
		}

		private void RushMinion()
		{
			if (view.rushButton.isDoubleConfirmed())
			{
				playerService.ProcessRush((int)view.GetRushCost(), true, RushTransactionCallback, view.building.Definition.ItemId);
			}
			else if (view.isLockedHighlighted)
			{
				view.SetRushHighlight(false);
				view.rushButton.ShowConfirmMessage();
			}
		}

		private void RushTransactionCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			if (pct.Success)
			{
				globalSFXSignal.Dispatch("Play_button_premium_01");
				timeEventService.RushEvent(view.minionID);
				global::Kampai.Game.Minion byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Minion>(view.minionID);
				global::Kampai.Game.TaskableBuilding byInstanceId2 = playerService.GetByInstanceId<global::Kampai.Game.TaskableBuilding>(byInstanceId.BuildingID);
				bool alreadyRushed = byInstanceId2 is global::Kampai.Game.ResourceBuilding;
				playerService.GetByInstanceId<global::Kampai.Game.Minion>(view.minionID).AlreadyRushed = alreadyRushed;
				view.ClearSlot();
				updateMinionCountSignal.Dispatch();
			}
		}

		private void FinishCallMinion(global::Kampai.Util.Tuple<int, int, global::UnityEngine.GameObject> tuple)
		{
			if (view.gameObject == tuple.Item3)
			{
				view.minionID = tuple.Item1;
				view.startTime = timeService.GameTimeSeconds();
				view.CallMinion();
			}
			view.ChangeMinionCount(false);
		}

		private void CallMinion()
		{
			callMinionSignal.Dispatch(view.building, view.gameObject);
			globalSFXSignal.Dispatch("Play_whistle_call_01");
			updateMinionCountSignal.Dispatch();
		}

		private void PurchaseSlot()
		{
			if (view.lockedButton.isDoubleConfirmed())
			{
				playerService.ProcessSlotPurchase(view.GetPurchaseCost(), true, view.identifier + 1, PurchaseSlotTransactionCallback, view.building.ID);
			}
			else if (view.isLockedHighlighted)
			{
				view.SetLockedHighlight(false);
				view.lockedButton.ShowConfirmMessage();
			}
		}

		private void Harvest()
		{
			tryHarvestSignal.Dispatch(view.building.ID, delegate
			{
				view.PurchaseSlot();
				updateSliderSignal.Dispatch();
			});
		}

		private void UpdateMinionCount()
		{
			view.SetIdleMinionCount();
		}

		private void UpdatePurchaseSlot()
		{
			view.UpdateLockedButton();
		}

		private void PurchaseSlotTransactionCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			if (pct.Success)
			{
				int slotUnlockLevelByIndex = view.building.GetSlotUnlockLevelByIndex(view.identifier);
				playerService.PurchaseSlotForBuilding(view.building.ID, slotUnlockLevelByIndex);
				globalSFXSignal.Dispatch("Play_button_premium_01");
				view.PurchaseSlot();
				setPremiumCurrencySignal.Dispatch();
				updatePurchaseSlotSignal.Dispatch();
			}
		}
	}
}
