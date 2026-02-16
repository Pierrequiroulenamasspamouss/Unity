namespace Kampai.UI.View
{
	public class CurrencyButtonClickCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.StoreItemDefinition definition { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetGrindCurrencySignal setGrindCurrencySignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetPremiumCurrencySignal setPremiumCurrencySignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal playSFXSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Game.StartPremiumPurchaseSignal startPremiumPurchaseSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ICurrencyService currencyService { get; set; }

		[Inject]
		public global::Kampai.Game.InsufficientInputsSignal insufficientInputsSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowGrindStoreSignal showGrindStoreSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.UI.View.PopupMessageSignal popupMessageSignal { get; set; }

		private void ShowConnectionErrorPopup()
		{
			popupMessageSignal.Dispatch(localService.GetString("NoInternetConnection"));
		}

		public override void Execute()
		{
			bool flag = global::Kampai.Util.NetworkUtil.IsConnected();
			global::Kampai.Game.CurrencyItemDefinition currencyItemDefinition = definitionService.Get<global::Kampai.Game.CurrencyItemDefinition>(definition.ReferencedDefID);
			global::Kampai.Game.PremiumCurrencyItemDefinition premiumCurrencyItemDefinition = currencyItemDefinition as global::Kampai.Game.PremiumCurrencyItemDefinition;
			if (premiumCurrencyItemDefinition != null)
			{
				if (flag)
				{
					global::Kampai.Game.KampaiPendingTransaction kampaiPendingTransaction = new global::Kampai.Game.KampaiPendingTransaction();
					kampaiPendingTransaction.ExternalIdentifier = premiumCurrencyItemDefinition.SKU;
					kampaiPendingTransaction.StoreItemDefinitionId = definition.ID;
					kampaiPendingTransaction.Transaction = definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(definition.TransactionID);
					kampaiPendingTransaction.UTCTimeCreated = timeService.GameTimeSeconds();
					startPremiumPurchaseSignal.Dispatch(kampaiPendingTransaction);
				}
				else
				{
					ShowConnectionErrorPopup();
				}
			}
			else if (playerService.VerifyTransaction(definition.TransactionID))
			{
				playerService.RunEntireTransaction(definition.TransactionID, global::Kampai.Game.TransactionTarget.CURRENCY, GrindCurrencyTransactionCallback);
			}
			else if (flag)
			{
				global::Kampai.Game.Transaction.TransactionDefinition pendingTransaction = definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(definition.TransactionID);
				global::Kampai.Game.PendingCurrencyTransaction pendingCurrencyTransaction = new global::Kampai.Game.PendingCurrencyTransaction(pendingTransaction, false, 0, null, null, GrindFromPremiumCallback);
				currencyService.CurrencyDialogOpened(pendingCurrencyTransaction);
				insufficientInputsSignal.Dispatch(pendingCurrencyTransaction, true);
			}
			else
			{
				ShowConnectionErrorPopup();
			}
			if (currencyItemDefinition.Audio == null || currencyItemDefinition.Audio.Length > 0)
			{
				playSFXSignal.Dispatch(currencyItemDefinition.Audio);
			}
		}

		private void GrindFromPremiumCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			if (!pct.Success)
			{
				showGrindStoreSignal.Dispatch();
			}
		}

		private void GrindCurrencyTransactionCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			if (pct.Success)
			{
				currencyService.CurrencyDialogClosed(true);
				setGrindCurrencySignal.Dispatch();
				setPremiumCurrencySignal.Dispatch();
			}
			else if (pct.ParentSuccess)
			{
				Execute();
			}
			else
			{
				currencyService.CurrencyDialogClosed(false);
			}
		}
	}
}
