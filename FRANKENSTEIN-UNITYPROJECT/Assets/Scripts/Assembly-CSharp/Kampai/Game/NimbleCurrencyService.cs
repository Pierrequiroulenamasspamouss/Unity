using System.Collections.Generic;

namespace Kampai.Game
{
	public class NimbleCurrencyService : global::Kampai.Game.CurrencyService, global::System.IDisposable
	{
		private enum State
		{
			Idle = 0,
			CatalogRefresh = 1,
			TransactionPending = 2
		}

		private sealed class DeferredCallbackEntry
		{
			public global::Kampai.Game.NimbleCurrencyService.DeferredCallback callback { get; set; }

			public string tag { get; set; }
		}

		private sealed class NimbleAdditionalInfo
		{
			[global::Newtonsoft.Json.JsonProperty(PropertyName = "orderId")]
			public string OrderId;

			[global::Newtonsoft.Json.JsonProperty(PropertyName = "purchaseData")]
			public string PurchaseData;

			[global::Newtonsoft.Json.JsonProperty(PropertyName = "purchaseState")]
			public int PurchaseState;

			[global::Newtonsoft.Json.JsonProperty(PropertyName = "purchaseTime")]
			public long PurchaseTime;

			[global::Newtonsoft.Json.JsonProperty(PropertyName = "token")]
			public string Token;
		}

		private sealed class NimbleAdditionalInfoAmazon
		{
			[global::Newtonsoft.Json.JsonProperty(PropertyName = "amazonUid")]
			public string AmazonUserID;
		}

		private delegate void DeferredCallback(global::Kampai.Game.NimbleCurrencyService ncs);

		private static string TAG = "[NCS] ";

		private static global::Kampai.Game.NimbleCurrencyService sInstance;

		private static global::Kampai.Game.NimbleCurrencyService.State s_CurrentState;

		private global::System.Action OnIdleState;

		private static bool transactionHandlingPaused;

		private static global::System.Collections.Generic.Queue<global::Kampai.Game.NimbleCurrencyService.DeferredCallbackEntry> s_deferredCallbacks = new global::System.Collections.Generic.Queue<global::Kampai.Game.NimbleCurrencyService.DeferredCallbackEntry>();

		private NimbleBridge_NotificationListener m_mtxCatalogRefreshListener;

		private NimbleBridge_NotificationListener m_mtxTransactionsRecoveredListener;

		private bool _disposed;

		[Inject]
		public global::Kampai.Game.Mtx.IMtxReceiptValidationService receiptValidationService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.UI.View.PremiumCurrencyCatalogUpdatedSignal premiumCurrencyCatalogUpdatedSignal { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.Common.ISwrveService swrveService { get; set; }

		[Inject]
		public ILocalPersistanceService localPersistanceService { get; set; }

		[Inject]
		public global::Kampai.Game.ProcessNextPendingTransactionSignal processNextPendingTransactionSignal { get; set; }

		private global::Kampai.Game.NimbleCurrencyService.State CurrentState
		{
			get
			{
				return s_CurrentState;
			}
			set
			{
				base.logger.Debug("{0}CurrentState switched: {1}->{2}", TAG, s_CurrentState, value);
				s_CurrentState = value;
				if (s_CurrentState == global::Kampai.Game.NimbleCurrencyService.State.Idle && OnIdleState != null)
				{
					global::System.Action onIdleState = OnIdleState;
					OnIdleState = null;
					onIdleState();
				}
			}
		}

		private bool TransactionHandlingPaused
		{
			get
			{
				return transactionHandlingPaused;
			}
			set
			{
				base.logger.Debug("{0}TransactionHandlingPaused flag set: {1}->{2}", TAG, transactionHandlingPaused, value);
				transactionHandlingPaused = value;
			}
		}

		public NimbleCurrencyService()
		{
			if (sInstance != null)
			{
				UnregisterNimbleListeners(sInstance);
			}
			sInstance = this;
		}

		private bool IfIdleSwitchStateTo(global::Kampai.Game.NimbleCurrencyService.State newState, string callerTag)
		{
			if (CurrentState != global::Kampai.Game.NimbleCurrencyService.State.Idle)
			{
				base.logger.Error("{0}{1} IfIdleSwitchStateTo(): skip setting of {2} state because current state is {3}, not Idle", TAG, callerTag, newState, CurrentState);
				return false;
			}
			CurrentState = newState;
			return true;
		}

		private void SetIdleStateIfCurrentIs(global::Kampai.Game.NimbleCurrencyService.State expectedState)
		{
			if (CurrentState == expectedState)
			{
				CurrentState = global::Kampai.Game.NimbleCurrencyService.State.Idle;
				return;
			}
			base.logger.Error("{0}SetIdleStateIfCurrentIs(): skip setting of Idle state because expected({1}) does not match to current ({2})", TAG, expectedState, CurrentState);
		}

		private bool NeedDeferCallback()
		{
			if (TransactionHandlingPaused)
			{
				base.logger.Debug("{0}NeedDeferCallback(): defer reason: transaction handing is paused", TAG);
				return true;
			}
			return false;
		}

		private static void DeferCallbackInvocation(global::Kampai.Game.NimbleCurrencyService.DeferredCallback callback, string tag)
		{
			s_deferredCallbacks.Enqueue(new global::Kampai.Game.NimbleCurrencyService.DeferredCallbackEntry
			{
				callback = callback,
				tag = tag
			});
		}

		private void ProcessDeferredCallbacks()
		{
			global::System.Collections.Generic.Queue<global::Kampai.Game.NimbleCurrencyService.DeferredCallbackEntry> queue = new global::System.Collections.Generic.Queue<global::Kampai.Game.NimbleCurrencyService.DeferredCallbackEntry>(s_deferredCallbacks);
			base.logger.Debug("{0}ProcessDeferredCallbacks(): clear s_deferredCallbacks list before processing", TAG);
			s_deferredCallbacks.Clear();
			base.logger.Debug("{0}ProcessDeferredCallbacks(): deferred callbacks count {1}", TAG, queue.Count);
			while (queue.Count > 0)
			{
				global::Kampai.Game.NimbleCurrencyService.DeferredCallbackEntry deferredCallbackEntry = queue.Dequeue();
				base.logger.Debug("{0}ProcessDeferredCallbacks(): Executing deferred callback {1}", TAG, deferredCallbackEntry.tag);
				deferredCallbackEntry.callback(this);
			}
			if (s_deferredCallbacks.Count <= 0)
			{
				return;
			}
			base.logger.Error("{0}ProcessDeferredCallbacks(): Unexpected deferred callbacks count {1}. It possibly can cause hang of transaction handling.", TAG, s_deferredCallbacks.Count);
			foreach (global::Kampai.Game.NimbleCurrencyService.DeferredCallbackEntry s_deferredCallback in s_deferredCallbacks)
			{
				base.logger.Error("{0}ProcessDeferredCallbacks(): Unexpected deferred callback {1}", TAG, s_deferredCallback.tag);
			}
		}

		private bool CurrentInstance()
		{
			return sInstance == this;
		}

		[PostConstruct]
		public void PostConstruct()
		{
			CheckDisposed();
			base.logger.Debug("{0}PostConstruct() current state: {1}, TransactionHandlingPaused {2}", TAG, CurrentState, TransactionHandlingPaused);
			localPersistanceService.PutData("MtxPurchaseInProgress", "False");
			if (CurrentState == global::Kampai.Game.NimbleCurrencyService.State.Idle)
			{
				global::System.Collections.Generic.List<NimbleBridge_MTXCatalogItem> availableCatalogItems = GetAvailableCatalogItems();
				if (availableCatalogItems == null || availableCatalogItems.Count == 0)
				{
					RefreshAvailableCatalogItems();
				}
			}
		}

		public override string GetPriceWithCurrencyAndFormat(string SKU)
		{
			CheckDisposed();
			using (NimbleBridge_MTXCatalogItem nimbleBridge_MTXCatalogItem = GetCatalogItem(SKU))
			{
				if (nimbleBridge_MTXCatalogItem != null)
				{
					return nimbleBridge_MTXCatalogItem.GetPriceWithCurrencyAndFormat();
				}
			}
			base.logger.Error("{0}GetPriceWithCurrencyAndFormat(): price not found for sku {1}", TAG, SKU);
			return localService.GetString("StoreBuy");
		}

		public override void RequestPurchase(global::Kampai.Game.KampaiPendingTransaction item)
		{
			CheckDisposed();
			string externalIdentifier = item.ExternalIdentifier;
			base.logger.Debug("{0}PurchaseItem() sku {1}", TAG, externalIdentifier);
			if (!IfIdleSwitchStateTo(global::Kampai.Game.NimbleCurrencyService.State.TransactionPending, "RequestPurchase()"))
			{
				OnPurchaseError(externalIdentifier, uint.MaxValue);
				return;
			}
			using (NimbleBridge_Error nimbleBridge_Error = NimbleBridge_MTX.GetComponent().PurchaseItem(externalIdentifier, OnUnverifiedReceiptReceived, OnPurchaseComplete))
			{
				if (nimbleBridge_Error != null && !nimbleBridge_Error.IsNull())
				{
					base.logger.Error("{0}PurchaseItem(): NimbleBridge_MTX.PurchaseItem error: code {1}, cause {2}", TAG, nimbleBridge_Error.GetCode(), nimbleBridge_Error.GetCause());
					SetIdleStateIfCurrentIs(global::Kampai.Game.NimbleCurrencyService.State.TransactionPending);
					if (nimbleBridge_Error.GetCode() == (NimbleBridge_Error.Code)20008u)
					{
						base.logger.Info("{0}PurchaseItem(): resume recover transaction on TRANSACTION_PENDING error", TAG);
						ResumeRecoveredTransaction();
					}
					else
					{
						OnPurchaseError(externalIdentifier, (uint)nimbleBridge_Error.GetCode());
					}
				}
				else
				{
					localPersistanceService.PutData("MtxPurchaseInProgress", "True");
				}
			}
		}

		public override void PauseTransactionsHandling()
		{
			CheckDisposed();
			base.logger.Debug("{0}PauseTransactionsHandling(): pause transaction handling", TAG);
			TransactionHandlingPaused = true;
		}

		public override void ResumeTransactionsHandling()
		{
			CheckDisposed();
			TransactionHandlingPaused = false;
			base.logger.Debug("{0}ResumeTransactionsHandling()", TAG);
			if (CurrentState == global::Kampai.Game.NimbleCurrencyService.State.CatalogRefresh)
			{
				base.logger.Debug("{0}ResumeTransactionsHandling(): wait for catalog refresh update before resuming.", TAG);
				OnIdleState = delegate
				{
					ResumePendingTransactions();
				};
			}
			else
			{
				base.logger.Debug("{0}ResumeTransactionsHandling(): resume pending transactions", TAG);
				ResumePendingTransactions();
			}
		}

		private NimbleBridge_MTXCatalogItem GetCatalogItem(string sku)
		{
			global::System.Collections.Generic.List<NimbleBridge_MTXCatalogItem> catalogItems = GetCatalogItems();
			return catalogItems.Find((NimbleBridge_MTXCatalogItem item) => item.GetSku().Equals(sku));
		}

		private global::System.Collections.Generic.List<NimbleBridge_MTXCatalogItem> GetCatalogItems()
		{
			global::System.Collections.Generic.List<NimbleBridge_MTXCatalogItem> availableCatalogItems = GetAvailableCatalogItems();
			if (availableCatalogItems == null || availableCatalogItems.Count == 0)
			{
				RefreshAvailableCatalogItems();
				return new global::System.Collections.Generic.List<NimbleBridge_MTXCatalogItem>();
			}
			return availableCatalogItems;
		}

		private static global::System.Collections.Generic.List<NimbleBridge_MTXCatalogItem> RemoveAndroidTestPurchases(NimbleBridge_MTXCatalogItem[] items)
		{
			if (items == null)
			{
				return null;
			}
			global::System.Collections.Generic.List<NimbleBridge_MTXCatalogItem> list = new global::System.Collections.Generic.List<NimbleBridge_MTXCatalogItem>(items.Length);
			foreach (NimbleBridge_MTXCatalogItem nimbleBridge_MTXCatalogItem in items)
			{
				if (!nimbleBridge_MTXCatalogItem.GetSku().StartsWith("android.test"))
				{
					list.Add(nimbleBridge_MTXCatalogItem);
				}
			}
			return list;
		}

		private global::System.Collections.Generic.List<NimbleBridge_MTXCatalogItem> GetAvailableCatalogItems()
		{
            try
            {
			    NimbleBridge_MTXCatalogItem[] availableCatalogItems = NimbleBridge_MTX.GetComponent().GetAvailableCatalogItems();
			    return RemoveAndroidTestPurchases(availableCatalogItems);
            }
            catch (global::System.Exception)
            {
                base.logger.Warning(TAG + "Native Nimble library not found. Using mock/empty catalog.");
                return new global::System.Collections.Generic.List<NimbleBridge_MTXCatalogItem>();
            }
		}

		private void RefreshAvailableCatalogItems()
		{
			if (IfIdleSwitchStateTo(global::Kampai.Game.NimbleCurrencyService.State.CatalogRefresh, "RefreshAvailableCatalogItems()"))
			{
				base.logger.Debug("{0}RefreshAvailableCatalogItems()", TAG);
                try
                {
				    if (m_mtxCatalogRefreshListener == null)
				    {
					    RegisterCatalogRefreshListener();
				    }
				    NimbleBridge_MTX.GetComponent().RefreshAvailableCatalogItems();
                }
                catch (global::System.Exception)
                {
                    base.logger.Warning(TAG + "Native Nimble library not found. Skipping catalog refresh.");
                    // Force a fake success response to unblock state
                    OnMTXCatalogRefreshed(
                        "nimble.notification.mtx.refreshcatalogfinished",
                        new global::System.Collections.Generic.Dictionary<string, object> { { "result", "0" } },
                        m_mtxCatalogRefreshListener
                    );
                }
			}
		}

		private void RegisterCatalogRefreshListener()
		{
			base.logger.Debug("{0}RegisterCatalogRefreshListener(): register REFRESH_CATALOG_FINISHED listener", TAG);
			m_mtxCatalogRefreshListener = new NimbleBridge_NotificationListener(OnMTXCatalogRefreshed);
			NimbleBridge_NotificationCenter.RegisterListener("nimble.notification.mtx.refreshcatalogfinished", m_mtxCatalogRefreshListener);
		}

		private void OnMTXCatalogRefreshed(string name, global::System.Collections.Generic.Dictionary<string, object> userData, NimbleBridge_NotificationListener listener)
		{
			if (!CurrentInstance())
			{
				base.logger.Debug("{0}OnMTXCatalogRefreshed(): callback called for old instance, redirect to new instance.", TAG);
				sInstance.OnMTXCatalogRefreshed(name, userData, null);
				return;
			}
			if (NeedDeferCallback())
			{
				base.logger.Debug("{0}OnMTXCatalogRefreshed(): defer callback because transaction handling is paused", TAG);
				DeferCallbackInvocation(delegate(global::Kampai.Game.NimbleCurrencyService ncs)
				{
					ncs.OnMTXCatalogRefreshed(name, userData, null);
				}, "OnMTXCatalogRefreshed");
				return;
			}
			base.logger.Debug("{0}OnMTXCatalogRefreshed(): name {1}", TAG, name);
			if (name == "nimble.notification.mtx.refreshcatalogfinished")
			{
				SetIdleStateIfCurrentIs(global::Kampai.Game.NimbleCurrencyService.State.CatalogRefresh);
				if (userData != null)
				{
					if ((string)userData["result"] == "1")
					{
						base.logger.Debug("{0}OnMTXCatalogRefreshed(): catalog items have been refreshed", TAG);
						premiumCurrencyCatalogUpdatedSignal.Dispatch();
					}
					else
					{
						base.logger.Error("{0}OnMTXCatalogRefreshed(): Failed to refresh catalog items", TAG);
					}
				}
			}
			else
			{
				base.logger.Error("{0}OnMTXCatalogRefreshed(): unexpected event {1}", TAG, name);
			}
		}

		private void OnUnverifiedReceiptReceived(NimbleBridge_MTXTransaction transaction)
		{
			if (!CurrentInstance())
			{
				base.logger.Debug("{0}OnUnverifiedReceiptReceived(): callback called for old instance, redirect to new instance.", TAG);
				sInstance.OnUnverifiedReceiptReceived(transaction);
			}
			else if (NeedDeferCallback())
			{
				base.logger.Debug("{0}OnUnverifiedReceiptReceived(): defer callback because transaction handling is paused", TAG);
				DeferCallbackInvocation(delegate(global::Kampai.Game.NimbleCurrencyService ncs)
				{
					ncs.OnUnverifiedReceiptReceived(transaction);
				}, "OnUnverifiedReceiptReceived");
			}
			else
			{
				base.logger.Debug("{0}OnUnverifiedReceiptReceived() transaction for sku {1}", TAG, transaction.GetItemSku());
			}
		}

		private void OnPurchaseComplete(NimbleBridge_MTXTransaction transaction)
		{
			base.logger.Debug("{0}OnPurchaseComplete() transaction for sku {1}", TAG, transaction.GetItemSku());
			if (!CurrentInstance())
			{
				base.logger.Debug("{0}OnPurchaseComplete(): callback called for old instance, redirect to new instance.", TAG);
				sInstance.OnPurchaseComplete(transaction);
				return;
			}
			if (NeedDeferCallback())
			{
				base.logger.Debug("{0}OnPurchaseComplete(): defer callback because transaction handling is paused", TAG);
				DeferCallbackInvocation(delegate(global::Kampai.Game.NimbleCurrencyService ncs)
				{
					ncs.OnPurchaseComplete(transaction);
				}, "OnPurchaseComplete");
				return;
			}
			bool flag = false;
			bool flag2 = false;
			using (NimbleBridge_Error nimbleBridge_Error = transaction.GetError())
			{
				if (nimbleBridge_Error != null && !nimbleBridge_Error.IsNull())
				{
					LogMtxError(nimbleBridge_Error, "OnPurchaseComplete()");
					if (TransactionCanceled(transaction))
					{
						base.logger.Debug("{0}OnPurchaseComplete() canceled transaction for sku {1}", TAG, transaction.GetItemSku());
						flag = true;
					}
					else if (nimbleBridge_Error.GetCode() == (NimbleBridge_Error.Code)20018u)
					{
						base.logger.Debug("{0}OnPurchaseComplete() ask-to-buy used for sku {1}", TAG, transaction.GetItemSku());
						flag2 = true;
					}
				}
			}
			global::Kampai.Game.Mtx.IMtxReceipt mtxReceipt = ((!flag && !flag2) ? GetReceipt(transaction) : null);
			if (flag2)
			{
				PurchaseDeferredCallback(transaction.GetItemSku());
			}
			else if (mtxReceipt != null)
			{
				base.logger.Debug("{0}OnPurchaseComplete() receipt exists, start validation.  Sku: {1}", TAG, transaction.GetItemSku());
				base.logger.Debug("{0}OnPurchaseComplete() receipt {1}", TAG, mtxReceipt);
				ValidateReceipt(transaction, mtxReceipt);
			}
			else
			{
				base.logger.Debug("{0}OnPurchaseComplete() no receipt, start tr-n finalization.  Sku: {1}", TAG, transaction.GetItemSku());
				FinalizeTransaction(transaction.GetTransactionId());
			}
			telemetryService.SendInAppPurchaseEventOnPurchaseComplete(GetIapTelemetryEvent(transaction));
		}

		private static bool TransactionCanceled(NimbleBridge_MTXTransaction transaction)
		{
			using (NimbleBridge_Error nimbleBridge_Error = transaction.GetError())
			{
				if (nimbleBridge_Error != null && !nimbleBridge_Error.IsNull())
				{
					return nimbleBridge_Error.GetCode() == (NimbleBridge_Error.Code)20003u;
				}
			}
			return false;
		}

		private global::Kampai.Game.Mtx.IMtxReceipt GetReceipt(NimbleBridge_MTXTransaction transaction)
		{
			try
			{
				global::Kampai.Game.NimbleCurrencyService.NimbleAdditionalInfo nimbleAdditionalInfo = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Kampai.Game.NimbleCurrencyService.NimbleAdditionalInfo>(transaction.GetAdditionalInfo());
				if (nimbleAdditionalInfo != null && !string.IsNullOrEmpty(nimbleAdditionalInfo.PurchaseData))
				{
					global::Kampai.Game.Mtx.GooglePlayReceipt googlePlayReceipt = new global::Kampai.Game.Mtx.GooglePlayReceipt();
					googlePlayReceipt.signedData = nimbleAdditionalInfo.PurchaseData;
					googlePlayReceipt.signature = transaction.GetReceipt();
					return googlePlayReceipt;
				}
			}
			catch (global::Newtonsoft.Json.JsonSerializationException ex)
			{
				base.logger.Error("{0}GetReceipt(): json exception {1}", TAG, ex);
			}
			return null;
		}

		private void ValidateReceipt(NimbleBridge_MTXTransaction transaction, global::Kampai.Game.Mtx.IMtxReceipt receipt)
		{
			receiptValidationService.AddPendingReceipt(transaction.GetItemSku(), transaction.GetTransactionId(), receipt);
			receiptValidationService.ValidatePendingReceipt();
			try
			{
				global::Kampai.Game.NimbleCurrencyService.NimbleAdditionalInfo nimbleAdditionalInfo = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Kampai.Game.NimbleCurrencyService.NimbleAdditionalInfo>(transaction.GetAdditionalInfo());
				LastPlatformStoreTransactionID = nimbleAdditionalInfo.OrderId ?? string.Empty;
			}
			catch (global::Newtonsoft.Json.JsonSerializationException ex)
			{
				base.logger.Error("ValidateReceipt(): json exception {0}", ex);
			}
			catch (global::Newtonsoft.Json.JsonReaderException ex2)
			{
				base.logger.Error("ValidateReceipt(): json exception {0}", ex2);
			}
		}

		public override void ReceiptValidationCallback(global::Kampai.Game.Mtx.ReceiptValidationResult result)
		{
			CheckDisposed();
			base.logger.Debug("{0}ReceiptValidationCallback() for sku {1}, result code {2}", TAG, result.sku, result.code);
			if (!CurrentInstance())
			{
				base.logger.Debug("{0}OnPurchaseComplete(): callback called for old instance, redirect to new instance.", TAG);
				sInstance.ReceiptValidationCallback(result);
				return;
			}
			if (NeedDeferCallback())
			{
				base.logger.Debug("{0}OnPurchaseComplete(): defer callback because transaction handling is paused", TAG);
				DeferCallbackInvocation(delegate(global::Kampai.Game.NimbleCurrencyService ncs)
				{
					ncs.ReceiptValidationCallback(result);
				}, "ReceiptValidationCallback");
				return;
			}
			switch (result.code)
			{
			case global::Kampai.Game.Mtx.ReceiptValidationResult.Code.SUCCESS:
			case global::Kampai.Game.Mtx.ReceiptValidationResult.Code.RECEIPT_DUPLICATE:
				PurchaseSucceededAndValidatedCallback(result.sku);
				OnMtxTransactionFulfilment(result.mtxTransactionId);
				break;
			case global::Kampai.Game.Mtx.ReceiptValidationResult.Code.RECEIPT_INVALID:
				OnReceiptValidationError(result.mtxTransactionId);
				OnPurchaseError(result.sku, 30000u);
				break;
			case global::Kampai.Game.Mtx.ReceiptValidationResult.Code.VALIDATION_UNAVAILABLE:
				break;
			}
		}

		private void OnPurchaseError(string sku, uint errorCode)
		{
			PurchaseCanceledCallback(sku, errorCode);
		}

		private void OnMtxTransactionFulfilment(string mtxTransactionId)
		{
			base.logger.Debug("{0}OnMtxTransactionFulfilment(): mtxTransactionId {1}", TAG, mtxTransactionId);
			using (NimbleBridge_Error nimbleBridge_Error = NimbleBridge_MTX.GetComponent().ItemGranted(mtxTransactionId, NimbleBridge_MTXCatalogItem.Type.CONSUMABLE, ItemGrantedCallback))
			{
				if (nimbleBridge_Error != null && !nimbleBridge_Error.IsNull())
				{
					base.logger.Error("{0}OnMtxTransactionFulfilment(): ItemGranted() error {1} - {2}", TAG, nimbleBridge_Error.GetCode(), nimbleBridge_Error.GetReason());
					FinalizeTransaction(mtxTransactionId);
				}
			}
			receiptValidationService.RemovePendingReceipt(mtxTransactionId);
		}

		private void OnReceiptValidationError(string mtxTransactionId)
		{
			FinalizeTransaction(mtxTransactionId);
			receiptValidationService.RemovePendingReceipt(mtxTransactionId);
		}

		private void ItemGrantedCallback(NimbleBridge_MTXTransaction transaction)
		{
			if (!CurrentInstance())
			{
				base.logger.Debug("{0}ItemGrantedCallback(): callback called for old instance, redirect to new instance.", TAG);
				sInstance.ItemGrantedCallback(transaction);
			}
			else if (NeedDeferCallback())
			{
				base.logger.Debug("{0}ItemGrantedCallback(): defer callback because transaction handling is paused", TAG);
				DeferCallbackInvocation(delegate(global::Kampai.Game.NimbleCurrencyService ncs)
				{
					ncs.ItemGrantedCallback(transaction);
				}, "ItemGrantedCallback");
			}
			else
			{
				FinalizeTransaction(transaction.GetTransactionId());
			}
		}

		private void FinalizeTransaction(string transactionId)
		{
			base.logger.Debug("{0}FinalizeTransaction(): finalizing transaction, id = {1}", TAG, transactionId);
			NimbleBridge_MTX.GetComponent().FinalizeTransaction(transactionId, FinalizeCallback);
		}

		private void FinalizeCallback(NimbleBridge_MTXTransaction transaction)
		{
			base.logger.Debug("{0}FinalizeCallback(): transaction finalized, id = {1}", TAG, transaction.GetTransactionId());
			if (!CurrentInstance())
			{
				base.logger.Debug("{0}FinalizeCallback(): callback called for old instance, redirect to new instance.", TAG);
				sInstance.FinalizeCallback(transaction);
				return;
			}
			if (NeedDeferCallback())
			{
				base.logger.Debug("{0}FinalizeCallback(): defer callback because transaction handling is paused", TAG);
				DeferCallbackInvocation(delegate(global::Kampai.Game.NimbleCurrencyService ncs)
				{
					ncs.FinalizeCallback(transaction);
				}, "FinalizeCallback");
				return;
			}
			using (NimbleBridge_Error nimbleBridge_Error = transaction.GetError())
			{
				if (nimbleBridge_Error != null && !nimbleBridge_Error.IsNull())
				{
					LogMtxError(nimbleBridge_Error, "FinalizeCallback()");
					if (TransactionCanceled(transaction))
					{
						base.logger.Debug("{0}FinalizeCallback() canceled transaction for sku {1}", TAG, transaction.GetItemSku());
						PurchaseCanceledCallback(transaction.GetItemSku(), (uint)nimbleBridge_Error.GetCode());
					}
					else if (nimbleBridge_Error.GetCode() == (NimbleBridge_Error.Code)20001u)
					{
						base.logger.Debug("{0}FinalizeCallback() do not notify client about ITEM_ALREADY_OWNED error(try to resume transaction), sku {1}", TAG, transaction.GetItemSku());
						NimbleBridge_MTX.GetComponent().RestorePurchasedTransactions();
					}
					else
					{
						base.logger.Error("{0}FinalizeCallback() notify client code about tr-n error", TAG);
						OnPurchaseError(transaction.GetItemSku(), (uint)nimbleBridge_Error.GetCode());
					}
				}
			}
			localPersistanceService.PutData("MtxPurchaseInProgress", "False");
			SetIdleStateIfCurrentIs(global::Kampai.Game.NimbleCurrencyService.State.TransactionPending);
		}

		private void ResumePendingTransactions()
		{
			if (s_deferredCallbacks.Count > 0)
			{
				base.logger.Debug("{0}ResumePendingTransactions(): process deferred callbacks", TAG);
				ProcessDeferredCallbacks();
			}
			if (receiptValidationService.HasPendingReceipts())
			{
				base.logger.Debug("{0}ResumePendingTransactions(): proceed validation of pending receipts", TAG);
				receiptValidationService.ValidatePendingReceipt();
			}
			else
			{
				if (ResumeRecoveredTransaction())
				{
					return;
				}
				if (CurrentState != global::Kampai.Game.NimbleCurrencyService.State.Idle)
				{
					base.logger.Debug("{0}ResumePendingTransactions(): waiting for NCS to settle", TAG);
					OnIdleState = delegate
					{
						ResumePendingTransactions();
					};
					return;
				}
				processNextPendingTransactionSignal.Dispatch();
				if (CurrentState != global::Kampai.Game.NimbleCurrencyService.State.Idle)
				{
					base.logger.Debug("{0}ResumePendingTransactions(): processing pending Kampai trns", TAG);
					OnIdleState = delegate
					{
						ResumePendingTransactions();
					};
				}
			}
		}

		private bool ResumeRecoveredTransaction()
		{
			base.logger.Debug("{0}ResumeRecoveredTransaction()", TAG);
            try
            {
			    if (m_mtxTransactionsRecoveredListener == null)
			    {
				    RegisterTransactionRecoveredListener();
			    }
			    NimbleBridge_MTXTransaction[] recoveredTransactions = NimbleBridge_MTX.GetComponent().GetRecoveredTransactions();
			    base.logger.Debug("{0}ResumeRecoveredTransaction(): GetRecoveredTransactions().Length: {1}", TAG, recoveredTransactions.Length);
			    if (recoveredTransactions.Length == 0)
			    {
				    base.logger.Debug("{0}ResumeRecoveredTransaction(): Nothing to do, no recovered transactions.", TAG);
				    return false;
			    }
			    if (!IfIdleSwitchStateTo(global::Kampai.Game.NimbleCurrencyService.State.TransactionPending, "ResumeRecoveredTransaction()"))
			    {
				    OnIdleState = delegate
				    {
					    ResumePendingTransactions();
				    };
				    return true;
			    }
			    ResumeFirstRecoveredTransaction(recoveredTransactions[0]);
			    return true;
            }
            catch (global::System.Exception)
            {
                // Native library missing
                return false;
            }
		}

		private void RegisterTransactionRecoveredListener()
		{
			base.logger.Debug("{0}RegisterTransactionRecoveringListener: register TRANSACTIONS_RECOVERED listener", TAG);
            try 
            {
			    m_mtxTransactionsRecoveredListener = new NimbleBridge_NotificationListener(OnMTXTransactionsRecovered);
			    NimbleBridge_NotificationCenter.RegisterListener("nimble.notification.mtx.transactionsrecovered", m_mtxTransactionsRecoveredListener);
            }
            catch(global::System.Exception) {}
		}

		private void OnMTXTransactionsRecovered(string name, global::System.Collections.Generic.Dictionary<string, object> userData, NimbleBridge_NotificationListener listener)
		{
			if (name == "nimble.notification.mtx.transactionsrecovered")
			{
				base.logger.Debug("{0}OnMTXTransactionsRecovered(): resuming recovered transactions", TAG);
				ResumeRecoveredTransaction();
			}
			else
			{
				base.logger.Error("{0}OnMTXTransactionsRecovered(): unexpected event {1}", TAG, name);
			}
		}

		private void ResumeFirstRecoveredTransaction(NimbleBridge_MTXTransaction transaction)
		{
			base.logger.Debug("{0}ResumeFirstRecoveredTransaction(): recovered tr-n: sku {1}, tr-n id: {2}, state: {3}", TAG, transaction.GetItemSku(), transaction.GetTransactionId(), transaction.GetTransactionState());
			OnIdleState = delegate
			{
				ResumePendingTransactions();
			};
			bool flag = false;
			if (transaction.GetTransactionState() == NimbleBridge_MTXTransaction.State.WAITING_FOR_PLATFORM_RESPONSE)
			{
				flag = true;
			}
			using (NimbleBridge_Error nimbleBridge_Error = NimbleBridge_MTX.GetComponent().ResumeTransaction(transaction.GetTransactionId(), OnUnverifiedReceiptReceived, OnPurchaseComplete, ItemGrantedCallback, FinalizeCallback))
			{
				if (nimbleBridge_Error != null && !nimbleBridge_Error.IsNull())
				{
					LogMtxError(nimbleBridge_Error, "ResumeFirstRecoveredTransaction()");
					SetIdleStateIfCurrentIs(global::Kampai.Game.NimbleCurrencyService.State.TransactionPending);
				}
			}
			if (flag)
			{
				base.logger.Debug("{0}ResumeFirstRecoveredTransaction(): Google Play specific: force call RestorePurchasedTransactions on WAITING_FOR_PLATFORM_RESPONSE tr-n status to properly resume transaction", TAG);
				NimbleBridge_MTX.GetComponent().RestorePurchasedTransactions();
			}
		}

		private void LogMtxError(NimbleBridge_Error error, string callerTag)
		{
			if (error != null)
			{
				if (error.IsNull())
				{
					return;
				}
				base.logger.Error("{0}{1}: Nimble error {2} - {3}", TAG, callerTag, error.GetCode(), error.GetReason());
				using (NimbleBridge_Error nimbleBridge_Error = error.GetCause())
				{
					if (nimbleBridge_Error != null && !nimbleBridge_Error.IsNull())
					{
						base.logger.Error("{0}{1}: Nimble error cause {2} - {3}", TAG, callerTag, nimbleBridge_Error.GetCode(), nimbleBridge_Error.GetReason());
					}
					else
					{
						base.logger.Error("{0}{1}: Nimble error cause is null", TAG, callerTag);
					}
					return;
				}
			}
			base.logger.Error("{0}{1}: error is null", TAG, callerTag);
		}

		private static void UnregisterNimbleListeners(global::Kampai.Game.NimbleCurrencyService instance)
		{
            try
            {
			    if (instance.m_mtxCatalogRefreshListener != null)
			    {
				    NimbleBridge_NotificationCenter.UnregisterListener(instance.m_mtxCatalogRefreshListener);
				    instance.m_mtxCatalogRefreshListener = null;
			    }
			    if (instance.m_mtxTransactionsRecoveredListener != null)
			    {
				    NimbleBridge_NotificationCenter.UnregisterListener(instance.m_mtxTransactionsRecoveredListener);
				    instance.m_mtxTransactionsRecoveredListener = null;
			    }
            }
            catch (global::System.Exception) {}
		}

		private global::Kampai.Common.IapTelemetryEvent GetIapTelemetryEvent(NimbleBridge_MTXTransaction transaction)
		{
			global::Kampai.Common.IapTelemetryEvent iapTelemetryEvent = new global::Kampai.Common.IapTelemetryEvent();
			using (NimbleBridge_Error nimbleBridge_Error = transaction.GetError())
			{
				if (nimbleBridge_Error != null && !nimbleBridge_Error.IsNull())
				{
					iapTelemetryEvent.nimbleMtxErrorCode = (uint)nimbleBridge_Error.GetCode();
				}
			}
			iapTelemetryEvent.productId = transaction.GetItemSku();
			iapTelemetryEvent.productPrice = transaction.GetPriceDecimal();
			string additionalInfo = transaction.GetAdditionalInfo();
			global::System.Collections.Generic.Dictionary<string, object> dictionary = global::MiniJSON.Json.Deserialize(additionalInfo) as global::System.Collections.Generic.Dictionary<string, object>;
			object value;
			if (dictionary.TryGetValue("localCurrency", out value))
			{
				iapTelemetryEvent.currency = value.ToString();
			}
			else
			{
				iapTelemetryEvent.currency = string.Empty;
			}
			global::Kampai.Game.Mtx.GooglePlayReceipt googlePlayReceipt = GetReceipt(transaction) as global::Kampai.Game.Mtx.GooglePlayReceipt;
			if (googlePlayReceipt != null)
			{
				iapTelemetryEvent.googlePurchaseData = googlePlayReceipt.signedData;
				iapTelemetryEvent.googleDataSignature = googlePlayReceipt.signature;
			}
			iapTelemetryEvent.googleOrderId = GetGooglePlayOrderId(transaction);
			return iapTelemetryEvent;
		}

		private string GetGooglePlayOrderId(NimbleBridge_MTXTransaction transaction)
		{
			try
			{
				global::Kampai.Game.NimbleCurrencyService.NimbleAdditionalInfo nimbleAdditionalInfo = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Kampai.Game.NimbleCurrencyService.NimbleAdditionalInfo>(transaction.GetAdditionalInfo());
				return nimbleAdditionalInfo.OrderId ?? string.Empty;
			}
			catch (global::Newtonsoft.Json.JsonSerializationException ex)
			{
				base.logger.Error("GetGooglePlayOrderId(): json exception {0}", ex);
			}
			return string.Empty;
		}

		private void CheckDisposed()
		{
			if (_disposed)
			{
				throw new global::System.ObjectDisposedException(typeof(global::Kampai.Game.NimbleCurrencyService).ToString());
			}
		}

		protected virtual void Dispose(bool fromDispose)
		{
			if (fromDispose)
			{
                try
                {
				    if (m_mtxCatalogRefreshListener != null)
				    {
					    m_mtxCatalogRefreshListener.Dispose();
				    }
				    if (m_mtxTransactionsRecoveredListener != null)
				    {
					    m_mtxTransactionsRecoveredListener.Dispose();
				    }
                }
                catch (global::System.Exception) {}
			}
		}

		public void Dispose()
		{
			CheckDisposed();
			Dispose(true);
		}

		~NimbleCurrencyService()
		{
			Dispose(false);
		}
	}
}
