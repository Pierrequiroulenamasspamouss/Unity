namespace Kampai.UI.View
{
	public class CurrencyWarningDialogMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.CurrencyWarningDialogView>
	{
		private global::Kampai.UI.View.CurrencyWarningModel model;

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Game.ICurrencyService currencyService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowPremiumStoreSignal showPremiumStoreSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetPremiumCurrencySignal setPremiumCurrencySignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetGrindCurrencySignal setGrindCurrencySignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSkrim { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			base.view.PurchaseButton.ClickedSignal.AddListener(PurchaseButtonClicked);
			base.view.CancelButton.ClickedSignal.AddListener(CancelButtonClicked);
		}

		public override void OnRemove()
		{
			base.OnRemove();
			base.view.PurchaseButton.ClickedSignal.RemoveListener(PurchaseButtonClicked);
			base.view.CancelButton.ClickedSignal.RemoveListener(CancelButtonClicked);
		}

		protected override void Close()
		{
			CancelButtonClicked();
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			global::Kampai.UI.View.CurrencyWarningModel currencyQuantity = args.Get<global::Kampai.UI.View.CurrencyWarningModel>();
			SetCurrencyQuantity(currencyQuantity);
		}

		private void SetCurrencyQuantity(global::Kampai.UI.View.CurrencyWarningModel model)
		{
			this.model = model;
			base.view.SetCurrencyNeeded(model.Cost, model.Amount);
		}

		private bool CheckCurrencyType()
		{
			if (model.Type.Equals(global::Kampai.Game.StoreItemType.GrindCurrency) && playerService.CanAffordExchange(model.Amount))
			{
				playerService.ExchangePremiumForGrind(model.Amount, TransactionCallback);
				return true;
			}
			if (model.Type.Equals(global::Kampai.Game.StoreItemType.PremiumCurrency) && playerService.GetQuantity(global::Kampai.Game.StaticItem.PREMIUM_CURRENCY_ID) >= model.Amount)
			{
				Close(true);
				return true;
			}
			return false;
		}

		private void PurchaseButtonClicked()
		{
			if (base.view.PurchaseButton.isDoubleConfirmed() && !CheckCurrencyType())
			{
				currencyService.CurrencyDialogOpened(new global::Kampai.Game.PendingCurrencyTransaction(null, false, model.Amount, null, null, PremiumStoreClosedCallback));
				showPremiumStoreSignal.Dispatch();
			}
		}

		private void PremiumStoreClosedCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			CheckCurrencyType();
		}

		private void TransactionCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			if (pct.Success)
			{
				Close(true);
			}
			else if (pct.ParentSuccess)
			{
				PurchaseButtonClicked();
			}
			else
			{
				Close();
			}
			setPremiumCurrencySignal.Dispatch();
			setGrindCurrencySignal.Dispatch();
		}

		private void CancelButtonClicked()
		{
			Close();
		}

		private void Close(bool success = false)
		{
			hideSkrim.Dispatch("CurrencySkrim");
			currencyService.CurrencyDialogClosed(success);
			if (model.Type.Equals(global::Kampai.Game.StoreItemType.GrindCurrency))
			{
				guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "GrindCurrencyWarning");
			}
			else if (model.Type.Equals(global::Kampai.Game.StoreItemType.PremiumCurrency))
			{
				guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "PremiumCurrencyWarning");
			}
		}
	}
}
