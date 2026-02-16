public class NimbleBridge_MTX
{
	private class TransactionCallbackConverter : NimbleBridge_Callback
	{
		private MTXTransactionCallback m_callback;

		public TransactionCallbackConverter(MTXTransactionCallback callback)
		{
			m_callback = callback;
		}

#if UNITY_ANDROID && !UNITY_EDITOR
		[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
		private static extern void NimbleUnity_MTXTransactionCallback_getData(global::System.IntPtr callback, out global::System.IntPtr transaction);
#else
		private static void NimbleUnity_MTXTransactionCallback_getData(global::System.IntPtr callback, out global::System.IntPtr transaction) { transaction = global::System.IntPtr.Zero; }
#endif

		public void Callback(global::System.IntPtr id)
		{
			try
			{
				if (m_callback != null)
				{
					global::System.IntPtr transaction;
					NimbleUnity_MTXTransactionCallback_getData(id, out transaction);
                    m_callback(new NimbleBridge_MTXTransaction(transaction));
				}
			}
			finally
			{
				NimbleBridge_CallbackHelper.Get().NotifyCallbackComplete(id);
			}
		}
	}

	public const int ERROR_BILLING_UNAVAILABLE = 10001;

	public const int ERROR_USER_CANCELED = 10002;

	public const int ERROR_ITEM_ALREADY_PURCHASED = 10003;

	public const int ERROR_ITEM_UNAVAILABLE = 10004;

	public const int ERROR_PURCHASE_VERIFICATION_FAILED = 10005;

	public const string NOTIFICATION_REFRESH_CATALOG_FINISHED = "nimble.notification.mtx.refreshcatalogfinished";

	public const string NOTIFICATION_RESTORE_PURCHASED_TRANSACTIONS_FINISHED = "nimble.notification.mtx.restorepurchasedtransactionsfinished";

	public const string NOTIFICATION_TRANSACTIONS_RECOVERED = "nimble.notification.mtx.transactionsrecovered";

	private NimbleBridge_MTX()
	{
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_MTX_deleteTransactionArray(global::System.IntPtr array);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_MTX_deleteItemArray(global::System.IntPtr array);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern NimbleBridge_Error NimbleUnity_MTX_purchaseItem(string sku, global::System.IntPtr receiptCallbackData, global::System.IntPtr purchaseCallbackData);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern NimbleBridge_Error NimbleUnity_MTX_itemGranted(string transactionId, int itemType, global::System.IntPtr callbackData);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern void NimbleUnity_MTX_finalizeTransaction(string transactionId, global::System.IntPtr callbackData);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_MTX_restorePurchasedTransactions();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern global::System.IntPtr NimbleBridge_MTX_getPurchasedTransactions();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern global::System.IntPtr NimbleBridge_MTX_getPendingTransactions();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern global::System.IntPtr NimbleBridge_MTX_getRecoveredTransactions();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern NimbleBridge_Error NimbleUnity_MTX_resumeTransaction(string transactionId, global::System.IntPtr receiptCallbackData, global::System.IntPtr purchaseCallbackData, global::System.IntPtr itemGrantedCallbackData, global::System.IntPtr finalizeCallbackData);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_MTX_refreshAvailableCatalogItems();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern global::System.IntPtr NimbleBridge_MTX_getAvailableCatalogItems();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_MTX_setPlatformParameters(global::System.IntPtr parameters);
#endif

	public static NimbleBridge_MTX GetComponent()
	{
		return new NimbleBridge_MTX();
	}

	public NimbleBridge_Error PurchaseItem(string sku, MTXTransactionCallback receiptCallback, MTXTransactionCallback purchaseCallback)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_MTX.TransactionCallbackConverter callback = new NimbleBridge_MTX.TransactionCallbackConverter(receiptCallback);
		NimbleBridge_MTX.TransactionCallbackConverter callback2 = new NimbleBridge_MTX.TransactionCallbackConverter(purchaseCallback);
		global::System.IntPtr receiptCallbackData = NimbleBridge_CallbackHelper.Get().AddCallback(callback);
		global::System.IntPtr purchaseCallbackData = NimbleBridge_CallbackHelper.Get().AddCallback(callback2);
		return NimbleUnity_MTX_purchaseItem(sku, receiptCallbackData, purchaseCallbackData);
#else
        return new NimbleBridge_Error(NimbleBridge_Error.Code.UNKNOWN, "Platform not supported");
#endif
	}

	public NimbleBridge_Error ItemGranted(string transactionId, NimbleBridge_MTXCatalogItem.Type itemType, MTXTransactionCallback callback)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_MTX.TransactionCallbackConverter callback2 = new NimbleBridge_MTX.TransactionCallbackConverter(callback);
		global::System.IntPtr callbackData = NimbleBridge_CallbackHelper.Get().AddCallback(callback2);
		return NimbleUnity_MTX_itemGranted(transactionId, (int)itemType, callbackData);
#else
        if (callback != null) callback(new NimbleBridge_MTXTransaction(global::System.IntPtr.Zero));
        return new NimbleBridge_Error(NimbleBridge_Error.Code.UNKNOWN, "Platform not supported");
#endif
	}

	public void FinalizeTransaction(string transactionId, MTXTransactionCallback callback)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_MTX.TransactionCallbackConverter callback2 = new NimbleBridge_MTX.TransactionCallbackConverter(callback);
		global::System.IntPtr callbackData = NimbleBridge_CallbackHelper.Get().AddCallback(callback2);
		NimbleUnity_MTX_finalizeTransaction(transactionId, callbackData);
#else
        if (callback != null) callback(new NimbleBridge_MTXTransaction(global::System.IntPtr.Zero));
#endif
	}

	public void RestorePurchasedTransactions()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_MTX_restorePurchasedTransactions();
#endif
	}

	public NimbleBridge_MTXTransaction[] GetPurchasedTransactions()
	{
		global::System.Collections.Generic.List<NimbleBridge_MTXTransaction> list = new global::System.Collections.Generic.List<NimbleBridge_MTXTransaction>();
#if UNITY_ANDROID && !UNITY_EDITOR
		global::System.IntPtr intPtr = NimbleBridge_MTX_getPurchasedTransactions();
		global::System.IntPtr intPtr2 = global::System.Runtime.InteropServices.Marshal.ReadIntPtr(intPtr);
		int num = 1;
		while (intPtr2 != global::System.IntPtr.Zero)
		{
			list.Add(new NimbleBridge_MTXTransaction(intPtr2));
			intPtr2 = global::System.Runtime.InteropServices.Marshal.ReadIntPtr(intPtr, num * global::System.Runtime.InteropServices.Marshal.SizeOf(typeof(global::System.IntPtr)));
			num++;
		}
		NimbleBridge_MTX_deleteTransactionArray(intPtr);
#endif
		return list.ToArray();
	}

	public NimbleBridge_MTXTransaction[] GetPendingTransactions()
	{
		global::System.Collections.Generic.List<NimbleBridge_MTXTransaction> list = new global::System.Collections.Generic.List<NimbleBridge_MTXTransaction>();
#if UNITY_ANDROID && !UNITY_EDITOR
		global::System.IntPtr intPtr = NimbleBridge_MTX_getPendingTransactions();
		global::System.IntPtr intPtr2 = global::System.Runtime.InteropServices.Marshal.ReadIntPtr(intPtr);
		int num = 1;
		while (intPtr2 != global::System.IntPtr.Zero)
		{
			list.Add(new NimbleBridge_MTXTransaction(intPtr2));
			intPtr2 = global::System.Runtime.InteropServices.Marshal.ReadIntPtr(intPtr, num * global::System.Runtime.InteropServices.Marshal.SizeOf(typeof(global::System.IntPtr)));
			num++;
		}
		NimbleBridge_MTX_deleteTransactionArray(intPtr);
#endif
		return list.ToArray();
	}

	public NimbleBridge_MTXTransaction[] GetRecoveredTransactions()
	{
		global::System.Collections.Generic.List<NimbleBridge_MTXTransaction> list = new global::System.Collections.Generic.List<NimbleBridge_MTXTransaction>();
#if UNITY_ANDROID && !UNITY_EDITOR
		global::System.IntPtr intPtr = NimbleBridge_MTX_getRecoveredTransactions();
		if (intPtr == global::System.IntPtr.Zero)
		{
			return list.ToArray();
		}
		global::System.IntPtr intPtr2 = global::System.Runtime.InteropServices.Marshal.ReadIntPtr(intPtr);
		int num = 1;
		while (intPtr2 != global::System.IntPtr.Zero)
		{
			list.Add(new NimbleBridge_MTXTransaction(intPtr2));
			intPtr2 = global::System.Runtime.InteropServices.Marshal.ReadIntPtr(intPtr, num * global::System.Runtime.InteropServices.Marshal.SizeOf(typeof(global::System.IntPtr)));
			num++;
		}
		NimbleBridge_MTX_deleteTransactionArray(intPtr);
#endif
		return list.ToArray();
	}

	public NimbleBridge_Error ResumeTransaction(string transactionId, MTXTransactionCallback receiptCallback, MTXTransactionCallback purchaseCallback, MTXTransactionCallback itemGrantedCallback, MTXTransactionCallback finalizeCallback)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_MTX.TransactionCallbackConverter callback = new NimbleBridge_MTX.TransactionCallbackConverter(receiptCallback);
		NimbleBridge_MTX.TransactionCallbackConverter callback2 = new NimbleBridge_MTX.TransactionCallbackConverter(purchaseCallback);
		NimbleBridge_MTX.TransactionCallbackConverter callback3 = new NimbleBridge_MTX.TransactionCallbackConverter(itemGrantedCallback);
		NimbleBridge_MTX.TransactionCallbackConverter callback4 = new NimbleBridge_MTX.TransactionCallbackConverter(finalizeCallback);
		global::System.IntPtr receiptCallbackData = NimbleBridge_CallbackHelper.Get().AddCallback(callback);
		global::System.IntPtr purchaseCallbackData = NimbleBridge_CallbackHelper.Get().AddCallback(callback2);
		global::System.IntPtr itemGrantedCallbackData = NimbleBridge_CallbackHelper.Get().AddCallback(callback3);
		global::System.IntPtr finalizeCallbackData = NimbleBridge_CallbackHelper.Get().AddCallback(callback4);
		return NimbleUnity_MTX_resumeTransaction(transactionId, receiptCallbackData, purchaseCallbackData, itemGrantedCallbackData, finalizeCallbackData);
#else
        return new NimbleBridge_Error(NimbleBridge_Error.Code.UNKNOWN, "Platform not supported");
#endif
	}

	public void RefreshAvailableCatalogItems()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
		    NimbleBridge_MTX_refreshAvailableCatalogItems();
        }
        catch (global::System.Exception) {}
#endif
	}

	public NimbleBridge_MTXCatalogItem[] GetAvailableCatalogItems()
	{
		global::System.Collections.Generic.List<NimbleBridge_MTXCatalogItem> list = new global::System.Collections.Generic.List<NimbleBridge_MTXCatalogItem>();
#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
		    global::System.IntPtr intPtr = NimbleBridge_MTX_getAvailableCatalogItems();
		    if (intPtr != global::System.IntPtr.Zero)
		    {
			    global::System.IntPtr intPtr2 = global::System.Runtime.InteropServices.Marshal.ReadIntPtr(intPtr);
			    int num = 1;
			    while (intPtr2 != global::System.IntPtr.Zero)
			    {
				    list.Add(new NimbleBridge_MTXCatalogItem(intPtr2));
				    intPtr2 = global::System.Runtime.InteropServices.Marshal.ReadIntPtr(intPtr, num * global::System.Runtime.InteropServices.Marshal.SizeOf(typeof(global::System.IntPtr)));
				    num++;
			    }
			    NimbleBridge_MTX_deleteItemArray(intPtr);
		    }
        }
        catch (global::System.Exception) {}
#endif
		return list.ToArray();
	}

	public void SetPlatformParameters(global::System.Collections.Generic.Dictionary<string, string> parameters)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		global::System.IntPtr intPtr = global::System.IntPtr.Zero;
		try
		{
			intPtr = MarshalUtility.ConvertDictionaryToPtr(parameters);
			NimbleBridge_MTX_setPlatformParameters(intPtr);
		}
        catch (global::System.Exception) {}
		finally
		{
			if (intPtr != global::System.IntPtr.Zero)
			{
				MarshalUtility.DisposeMapPtr(intPtr);
			}
		}
#endif
	}
}
