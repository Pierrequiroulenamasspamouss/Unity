namespace Kampai.Game
{
	public abstract class CurrencyService : global::Kampai.Game.ICurrencyService
	{
		protected global::System.Collections.Generic.Stack<global::Kampai.Game.PendingCurrencyTransaction> pendingTransactions = new global::System.Collections.Generic.Stack<global::Kampai.Game.PendingCurrencyTransaction>();

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.FinishPremiumPurchaseSignal finishPremiumPurchaseSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CancelPremiumPurchaseSignal cancelPremiumPurchaseSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CurrencyDialogClosedSignal currencyDialogClosedSignal { get; set; }

		public string LastPlatformStoreTransactionID { get; set; }

		public abstract string GetPriceWithCurrencyAndFormat(string SKU);

		public abstract void RequestPurchase(global::Kampai.Game.KampaiPendingTransaction item);

		public abstract void ReceiptValidationCallback(global::Kampai.Game.Mtx.ReceiptValidationResult result);

		public abstract void PauseTransactionsHandling();

		public abstract void ResumeTransactionsHandling();

		public void PurchaseCanceledCallback(string SKU, uint errorCode)
		{
			cancelPremiumPurchaseSignal.Dispatch(SKU, errorCode);
			CurrencyDialogClosed(false);
		}

		public void PurchaseSucceededAndValidatedCallback(string SKU)
		{
			if (playerService.PlayerAlreadyHasPlatformStoreTransactionID(LastPlatformStoreTransactionID))
			{
				PurchaseCanceledCallback(SKU, 20019u);
				return;
			}
			playerService.AddPlatformStoreTransactionID(LastPlatformStoreTransactionID);
			finishPremiumPurchaseSignal.Dispatch(SKU);
		}

		public void PurchaseDeferredCallback(string SKU)
		{
			CurrencyDialogClosed(false);
		}

		public void CurrencyDialogClosed(bool success)
		{
			if (pendingTransactions.Count > 0)
			{
				global::Kampai.Game.PendingCurrencyTransaction pendingCurrencyTransaction = pendingTransactions.Pop();
				pendingCurrencyTransaction.ParentSuccess = success;
				pendingCurrencyTransaction.GetCallback()(pendingCurrencyTransaction);
			}
			currencyDialogClosedSignal.Dispatch();
		}

		public void CurrencyDialogOpened(global::Kampai.Game.PendingCurrencyTransaction pendingTransaction)
		{
			pendingTransactions.Push(pendingTransaction);
		}
	}
}
