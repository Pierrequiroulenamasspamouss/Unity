namespace Kampai.UI.View
{
	public class BuyMarketplacePanelView : global::Kampai.UI.View.PopupMenuView
	{
		internal enum RefreshButtonState
		{
			None = 0,
			RefreshReady = 1,
			RefreshPending = 2,
			StopSpinning = 3
		}

		private global::System.Collections.IEnumerator waitFrame;

		public global::Kampai.UI.View.ButtonView ArrowButtonView;

		public global::Kampai.UI.View.DoubleConfirmButtonView RefreshPremiumButtonView;

		public global::Kampai.UI.View.ButtonView RefreshButtonView;

		public global::Kampai.UI.View.ButtonView StopButtonView;

		public global::UnityEngine.UI.Text RefreshCostText;

		public global::UnityEngine.UI.Text RefreshTitleText;

		public global::Kampai.UI.View.KampaiScrollView ScrollView;

		private global::Kampai.Main.ILocalizationService localService;

		internal global::strange.extensions.signal.impl.Signal<bool> OnOpenSignal = new global::strange.extensions.signal.impl.Signal<bool>();

		internal global::strange.extensions.signal.impl.Signal OnCloseSignal = new global::strange.extensions.signal.impl.Signal();

		internal global::Kampai.UI.View.BuyMarketplacePanelView.RefreshButtonState refreshButtonState;

		private global::Kampai.Util.IRoutineRunner routineRunner;

		public bool IsOpen { get; set; }

		public void Init(global::Kampai.Main.ILocalizationService localizationService, global::Kampai.Util.IRoutineRunner routineRunner)
		{
			base.Init();
			this.routineRunner = routineRunner;
			refreshButtonState = global::Kampai.UI.View.BuyMarketplacePanelView.RefreshButtonState.None;
			localService = localizationService;
			StopButtonView.PlaySoundOnClick = (RefreshPremiumButtonView.PlaySoundOnClick = false);
			RefreshPremiumButtonView.EnableDoubleConfirm();
			SetRefreshCost(0);
		}

		public bool HasSlot(global::Kampai.Game.MarketplaceBuyItem slot)
		{
			foreach (global::strange.extensions.mediation.impl.View item in ScrollView)
			{
				global::Kampai.UI.View.BuyMarketplaceSlotView buyMarketplaceSlotView = item as global::Kampai.UI.View.BuyMarketplaceSlotView;
				if (buyMarketplaceSlotView == null || slot.ID != buyMarketplaceSlotView.slotId)
				{
					continue;
				}
				return true;
			}
			return false;
		}

		internal void SetupRefreshButtonState(global::Kampai.UI.View.BuyMarketplacePanelView.RefreshButtonState state, int timeRemaining = 0)
		{
			switch (state)
			{
			case global::Kampai.UI.View.BuyMarketplacePanelView.RefreshButtonState.RefreshReady:
				SetRefreshTitleText(localService.GetString("BuyPanelRefreshUserPrompt"));
				if (state != refreshButtonState)
				{
					RefreshButtonView.gameObject.SetActive(true);
					if (waitFrame == null)
					{
						waitFrame = WaitAFrame();
						routineRunner.StartCoroutine(waitFrame);
					}
					StopButtonView.gameObject.SetActive(false);
				}
				break;
			case global::Kampai.UI.View.BuyMarketplacePanelView.RefreshButtonState.RefreshPending:
			{
				global::System.TimeSpan timeSpan = global::System.TimeSpan.FromSeconds(timeRemaining);
				int minutes = timeSpan.Minutes;
				int seconds = timeSpan.Seconds;
				SetRefreshTitleText(string.Format("{0} {1:0}:{2:00}", localService.GetString("BuyPanelRefreshTitle"), minutes, seconds));
				if (state != refreshButtonState)
				{
					if (waitFrame != null)
					{
						routineRunner.StopCoroutine(waitFrame);
						waitFrame = null;
					}
					RefreshPremiumButtonView.gameObject.SetActive(true);
					RefreshButtonView.gameObject.SetActive(false);
					StopButtonView.gameObject.SetActive(false);
				}
				break;
			}
			case global::Kampai.UI.View.BuyMarketplacePanelView.RefreshButtonState.StopSpinning:
				SetRefreshTitleText(localService.GetString("BuyPanelStopSpinningUserPrompt"));
				if (state != refreshButtonState)
				{
					RefreshPremiumButtonView.ResetAnim();
					StopButtonView.gameObject.SetActive(true);
					RefreshButtonView.gameObject.SetActive(false);
					if (waitFrame == null)
					{
						waitFrame = WaitAFrame();
						routineRunner.StartCoroutine(waitFrame);
					}
				}
				break;
			}
			refreshButtonState = state;
		}

		internal void SetRefreshTitleText(string text)
		{
			if (RefreshTitleText != null)
			{
				RefreshTitleText.text = text;
			}
		}

		internal void SetRefreshCost(int cost)
		{
			RefreshCostText.text = cost.ToString();
		}

		internal global::System.Collections.IEnumerator WaitAFrame()
		{
			yield return new global::UnityEngine.WaitForEndOfFrame();
			RefreshPremiumButtonView.gameObject.SetActive(false);
			waitFrame = null;
		}

		internal void SetOpen(bool show, bool dispatchSignals = true, bool isInstant = false)
		{
			if (show)
			{
				if (isInstant)
				{
					float normalizedTime = 1f;
					int layer = -1;
					animator.Play("Open", layer, normalizedTime);
					isOpened = true;
				}
				else
				{
					Open();
				}
				if (dispatchSignals)
				{
					OnOpenSignal.Dispatch(isInstant);
				}
			}
			else
			{
				Close();
				if (dispatchSignals)
				{
					OnCloseSignal.Dispatch();
				}
			}
			IsOpen = show;
		}
	}
}
