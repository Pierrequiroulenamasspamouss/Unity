namespace Kampai.Game
{
	public class FinishPremiumPurchaseCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public string ExternalIdentifier { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public ILocalPersistanceService localPersistService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.ICurrencyService currencyService { get; set; }

		[Inject]
		public global::Kampai.Common.Service.HealthMetrics.IClientHealthService clientHealthService { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetGrindCurrencySignal setGrindCurrencySignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetPremiumCurrencySignal setPremiumCurrencySignal { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.Game.SavePlayerSignal savePlayerSignal { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.Transaction.TransactionDefinition reward = GetReward();
			RecordPurchase(reward);
			if (reward == null)
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "{0} unknown SKU", ExternalIdentifier);
				return;
			}
			if (reward.Inputs != null && reward.Inputs.Count > 0)
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "Reward contains inputs {0}", reward.ID);
			}
			telemetryService.SendInAppPurchaseEventOnProductDelivery(ExternalIdentifier, reward);
			localPersistService.PutDataPlayer("IsSpender", "true");
			DispatchSignals();
		}

		private global::Kampai.Game.Transaction.TransactionDefinition GetReward()
		{
			global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition = null;
			if (!string.IsNullOrEmpty(ExternalIdentifier))
			{
				global::Kampai.Game.KampaiPendingTransaction kampaiPendingTransaction = playerService.ProcessPendingTransaction(ExternalIdentifier, true);
				if (kampaiPendingTransaction != null)
				{
					transactionDefinition = kampaiPendingTransaction.Transaction;
				}
			}
			if (transactionDefinition == null)
			{
				logger.Log(global::Kampai.Util.Logger.Level.Info, "{0} not found in pending transactions, trying to run transaction again", ExternalIdentifier);
				foreach (global::Kampai.Game.StoreItemDefinition item in definitionService.GetAll<global::Kampai.Game.StoreItemDefinition>())
				{
					global::Kampai.Game.PremiumCurrencyItemDefinition definition = null;
					if (definitionService.TryGet<global::Kampai.Game.PremiumCurrencyItemDefinition>(item.ReferencedDefID, out definition) && definition.SKU.Trim().ToLower().Equals(ExternalIdentifier.Trim().ToLower()))
					{
						global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition2 = definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(item.TransactionID);
						global::Kampai.Game.TransactionArg transactionArg = new global::Kampai.Game.TransactionArg();
						transactionArg.IsFromPremiumSource = true;
						playerService.RunEntireTransaction(transactionDefinition2.ID, global::Kampai.Game.TransactionTarget.CURRENCY, null, transactionArg);
						transactionDefinition = transactionDefinition2;
					}
				}
			}
			return transactionDefinition;
		}

		private void RecordPurchase(global::Kampai.Game.Transaction.TransactionDefinition reward)
		{
			global::Kampai.Game.IPurchaseRecorder purchaseRecorder = playerService as global::Kampai.Game.IPurchaseRecorder;
			if (purchaseRecorder != null)
			{
				int premiumOutputForTransaction = global::Kampai.Game.Transaction.TransactionUtil.GetPremiumOutputForTransaction(reward);
				int grindOutputForTransaction = global::Kampai.Game.Transaction.TransactionUtil.GetGrindOutputForTransaction(reward);
				if (premiumOutputForTransaction > 0)
				{
					purchaseRecorder.AddPurchasedCurrency(true, (uint)premiumOutputForTransaction);
				}
				if (grindOutputForTransaction > 0)
				{
					purchaseRecorder.AddPurchasedCurrency(false, (uint)grindOutputForTransaction);
				}
			}
			else
			{
				logger.Error("Premium purchase occured without a purchase recorder");
			}
		}

		private void DispatchSignals()
		{
			setGrindCurrencySignal.Dispatch();
			setPremiumCurrencySignal.Dispatch();
			currencyService.CurrencyDialogClosed(true);
			clientHealthService.MarkMeterEvent("AppFlow.Purchase");
			savePlayerSignal.Dispatch(new global::Kampai.Util.Tuple<string, string, bool>("remote", string.Empty, false));
		}
	}
}
