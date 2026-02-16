namespace Kampai.UI.View
{
	public class BuyMarketplacePanelMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.BuyMarketplacePanelView>
	{
		private int refreshTimeSeconds;

		private global::Kampai.Game.MarketplaceRefreshTimer refreshTimer;

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.UI.View.CloseAllOtherMenuSignal closeAllMenuSignal { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.OpenSellBuildingModalSignal openSellBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.Game.RefreshSaleItemsSuccessSignal refreshSuccessSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RushRefreshTimerSuccessSignal rushSuccessSignal { get; set; }

		[Inject]
		public global::Kampai.Game.HaltSlotMachine haltSlotMachine { get; set; }

		[Inject]
		public global::Kampai.UI.View.MarketplaceOpenBuyPanelSignal openBuyPanel { get; set; }

		[Inject]
		public global::Kampai.UI.View.MarketplaceCloseBuyPanelSignal closeBuyPanel { get; set; }

		[Inject]
		public global::Kampai.Common.ICoppaService coppaService { get; set; }

		[Inject]
		public global::Kampai.Game.SocialLoginSuccessSignal loginSuccess { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RemoveStorageBuildingItemDescriptionSignal removeItemDescriptionSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		public override void OnRegister()
		{
			base.view.Init(localService, routineRunner);
			global::Kampai.Game.MarketplaceRefreshTimerDefinition marketplaceRefreshTimerDefinition = definitionService.Get<global::Kampai.Game.MarketplaceRefreshTimerDefinition>(1000008093);
			refreshTimeSeconds = marketplaceRefreshTimerDefinition.RefreshTimeSeconds;
			openBuyPanel.AddListener(OpenPanel);
			base.view.OnOpenSignal.AddListener(OpenPanel);
			base.view.OnCloseSignal.AddListener(Close);
			base.view.ArrowButtonView.ClickedSignal.AddListener(Close);
			base.view.RefreshButtonView.ClickedSignal.AddListener(OnRefreshButtonClick);
			base.view.RefreshPremiumButtonView.ClickedSignal.AddListener(OnRefreshButtonClick);
			base.view.StopButtonView.ClickedSignal.AddListener(OnRefreshButtonClick);
			refreshSuccessSignal.AddListener(LoadScrollViewItems);
			refreshSuccessSignal.AddListener(UpdateRefreshTime);
			rushSuccessSignal.AddListener(UpdateRefreshTime);
			haltSlotMachine.AddListener(CompleteSlotMachine);
			loginSuccess.AddListener(OnLoginSuccess);
			base.view.SetRefreshCost(marketplaceRefreshTimerDefinition.RushCost);
		}

		public override void OnRemove()
		{
			openBuyPanel.RemoveListener(OpenPanel);
			base.view.OnOpenSignal.RemoveListener(OpenPanel);
			base.view.OnCloseSignal.RemoveListener(Close);
			base.view.ArrowButtonView.ClickedSignal.RemoveListener(Close);
			base.view.RefreshButtonView.ClickedSignal.RemoveListener(OnRefreshButtonClick);
			base.view.RefreshPremiumButtonView.ClickedSignal.RemoveListener(OnRefreshButtonClick);
			base.view.StopButtonView.ClickedSignal.RemoveListener(OnRefreshButtonClick);
			refreshSuccessSignal.RemoveListener(LoadScrollViewItems);
			refreshSuccessSignal.RemoveListener(UpdateRefreshTime);
			rushSuccessSignal.RemoveListener(UpdateRefreshTime);
			haltSlotMachine.RemoveListener(CompleteSlotMachine);
			loginSuccess.RemoveListener(OnLoginSuccess);
			UpdateButtonSpinningState();
			StopAllCoroutines();
		}

		private void OpenPanel(bool isInstant)
		{
			base.view.SetOpen(true, false, isInstant);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.StartMarketplaceRefreshTimerSignal>().Dispatch(false);
			UpdateRefreshTime();
			telemetryService.Send_Telemtry_EVT_MARKETPLACE_VIEWED("VIEW");
			InvokeRepeating("UpdateRefreshTime", 0.001f, 1f);
			LoadScrollViewItems();
			base.uiAddedSignal.Dispatch(base.view.gameObject, Close);
			removeItemDescriptionSignal.Dispatch();
			soundFXSignal.Dispatch("Play_shop_pane_in_01");
		}

		protected override void Close()
		{
			CancelInvoke("UpdateRefreshTime");
			base.view.SetOpen(false, false);
			closeBuyPanel.Dispatch();
			base.uiRemovedSignal.Dispatch(GetViewGameObject());
			if (base.view.refreshButtonState == global::Kampai.UI.View.BuyMarketplacePanelView.RefreshButtonState.StopSpinning)
			{
				base.view.AnimationDoneCallback("Close", StopSlotSpinning);
			}
			soundFXSignal.Dispatch("Play_shop_pane_out_01");
		}

		internal void CompleteSlotMachine()
		{
			StopAllCoroutines();
			StopSlotSpinning();
			soundFXSignal.Dispatch("Play_marketplace_slotEnd_01");
			base.view.RefreshPremiumButtonView.ResetAnim();
		}

		internal void StopSlotSpinning()
		{
			foreach (global::Kampai.UI.View.BuyMarketplaceSlotView itemView in base.view.ScrollView.ItemViewList)
			{
				itemView.StopSlotMachine();
			}
			UpdateButtonSpinningState();
		}

		internal void UpdateButtonSpinningState()
		{
			base.view.ScrollView.EnableScrolling(false, true);
			if (base.view.refreshButtonState != global::Kampai.UI.View.BuyMarketplacePanelView.RefreshButtonState.StopSpinning)
			{
				return;
			}
			base.view.SetupRefreshButtonState(global::Kampai.UI.View.BuyMarketplacePanelView.RefreshButtonState.RefreshPending);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.StartMarketplaceRefreshTimerSignal>().Dispatch(true);
			UpdateRefreshTime();
			foreach (global::Kampai.UI.View.BuyMarketplaceSlotView itemView in base.view.ScrollView.ItemViewList)
			{
				if (itemView.BuyButtonView.isActiveAndEnabled && itemView.BuyButtonView.animator != null)
				{
					itemView.BuyButtonView.animator.enabled = true;
				}
			}
		}

		private void OnRefreshButtonClick()
		{
			switch (base.view.refreshButtonState)
			{
			case global::Kampai.UI.View.BuyMarketplacePanelView.RefreshButtonState.RefreshReady:
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.RefreshSaleItemsSignal>().Dispatch(new global::Kampai.Util.Tuple<bool, bool>(true, false));
				base.view.SetupRefreshButtonState(global::Kampai.UI.View.BuyMarketplacePanelView.RefreshButtonState.StopSpinning);
				break;
			case global::Kampai.UI.View.BuyMarketplacePanelView.RefreshButtonState.RefreshPending:
				if (base.view.RefreshPremiumButtonView.isDoubleConfirmed())
				{
					soundFXSignal.Dispatch("Play_button_premium_01");
					gameContext.injectionBinder.GetInstance<global::Kampai.Game.RefreshSaleItemsSignal>().Dispatch(new global::Kampai.Util.Tuple<bool, bool>(false, false));
					base.view.RefreshPremiumButtonView.ResetTapState();
				}
				break;
			case global::Kampai.UI.View.BuyMarketplacePanelView.RefreshButtonState.StopSpinning:
				CompleteSlotMachine();
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.RefreshSaleItemsSignal>().Dispatch(new global::Kampai.Util.Tuple<bool, bool>(false, true));
				break;
			}
		}

		private void UpdateRefreshTime()
		{
			if (base.view.refreshButtonState != global::Kampai.UI.View.BuyMarketplacePanelView.RefreshButtonState.StopSpinning)
			{
				int num = 0;
				if (refreshTimer != null)
				{
					num = refreshTimer.UTCStartTime + refreshTimeSeconds - timeService.GameTimeSeconds();
				}
				else
				{
					refreshTimer = playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.MarketplaceRefreshTimer>(1000008093);
				}
				if (num <= 0)
				{
					num = 0;
				}
				if (num <= 0)
				{
					base.view.SetupRefreshButtonState(global::Kampai.UI.View.BuyMarketplacePanelView.RefreshButtonState.RefreshReady);
				}
				else
				{
					base.view.SetupRefreshButtonState(global::Kampai.UI.View.BuyMarketplacePanelView.RefreshButtonState.RefreshPending, num);
				}
			}
		}

		private void LoadScrollViewItems()
		{
			bool isCOPPAGated = coppaService.Restricted();
			global::System.Collections.Generic.List<global::Kampai.Game.MarketplaceBuyItem> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.MarketplaceBuyItem>();
			if (base.view.ScrollView.ItemViewList.Count == instancesByType.Count)
			{
				bool flag = false;
				foreach (global::Kampai.UI.View.BuyMarketplaceSlotView itemView in base.view.ScrollView.ItemViewList)
				{
					if (itemView.SetupBuyItem(isCOPPAGated, instancesByType[itemView.slotIndex], soundFXSignal))
					{
						flag = true;
					}
				}
				if (flag)
				{
					StopAllCoroutines();
					base.view.ScrollView.TweenToPosition(new global::UnityEngine.Vector2(0f, 1f), 0f);
					base.view.ScrollView.EnableScrolling(false, false);
					StartCoroutine("EnableScrollView");
				}
			}
			else
			{
				base.view.ScrollView.ClearItems();
				if (instancesByType != null)
				{
					base.view.ScrollView.AddList(instancesByType, CreateMarketplaceItem);
				}
			}
		}

		internal global::System.Collections.IEnumerator EnableScrollView()
		{
			yield return new global::UnityEngine.WaitForSeconds(3.7f);
			UpdateButtonSpinningState();
		}

		private global::Kampai.UI.View.BuyMarketplaceSlotView CreateMarketplaceItem(int index, global::Kampai.Game.MarketplaceBuyItem item)
		{
			global::UnityEngine.GameObject original = global::Kampai.Util.KampaiResources.Load("cmp_StorageBuildingBuySlot") as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(original) as global::UnityEngine.GameObject;
			global::Kampai.UI.View.BuyMarketplaceSlotView component = gameObject.GetComponent<global::Kampai.UI.View.BuyMarketplaceSlotView>();
			component.slotIndex = index;
			component.slotId = item.ID;
			component.BuyItem = item;
			return component;
		}

		private void OnLoginSuccess(global::Kampai.Game.ISocialService socialService)
		{
			if (socialService.type == global::Kampai.Game.SocialServices.FACEBOOK)
			{
				LoadScrollViewItems();
			}
		}
	}
}
