public class NimbleBridge_MTXCatalogItem : global::System.Runtime.InteropServices.SafeHandle
{
	public enum Type
	{
		UNKNOWN = 0,
		NONCONSUMABLE = 1,
		CONSUMABLE = 2
	}

	public override bool IsInvalid
	{
		get
		{
			return base.IsClosed || handle == global::System.IntPtr.Zero;
		}
	}

	private NimbleBridge_MTXCatalogItem()
		: base(global::System.IntPtr.Zero, true)
	{
	}

	internal NimbleBridge_MTXCatalogItem(global::System.IntPtr handle)
		: base(global::System.IntPtr.Zero, true)
	{
		SetHandle(handle);
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_MTXCatalogItem_Dispose(NimbleBridge_MTXCatalogItem wrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_MTXCatalogItem_getSku(NimbleBridge_MTXCatalogItem wrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_MTXCatalogItem_getTitle(NimbleBridge_MTXCatalogItem wrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_MTXCatalogItem_getDescription(NimbleBridge_MTXCatalogItem wrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern float NimbleBridge_MTXCatalogItem_getPriceDecimal(NimbleBridge_MTXCatalogItem wrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_MTXCatalogItem_getPriceWithCurrencyAndFormat(NimbleBridge_MTXCatalogItem wrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern int NimbleBridge_MTXCatalogItem_getItemType(NimbleBridge_MTXCatalogItem wrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_MTXCatalogItem_getMetaDataUrl(NimbleBridge_MTXCatalogItem wrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_MTXCatalogItem_getAdditionalInfo(NimbleBridge_MTXCatalogItem wrapper);
#endif

	protected override bool ReleaseHandle()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_MTXCatalogItem_Dispose(this);
#endif
		return true;
	}

	public string GetSku()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_MTXCatalogItem_getSku(this);
#else
        return "";
#endif
	}

	public string GetTitle()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_MTXCatalogItem_getTitle(this);
#else
        return "";
#endif
	}

	public string GetDescription()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_MTXCatalogItem_getDescription(this);
#else
        return "";
#endif
	}

	public float GetPriceDecimal()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_MTXCatalogItem_getPriceDecimal(this);
#else
        return 0.0f;
#endif
	}

	public string GetPriceWithCurrencyAndFormat()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_MTXCatalogItem_getPriceWithCurrencyAndFormat(this);
#else
        return "";
#endif
	}

	public NimbleBridge_MTXCatalogItem.Type GetItemType()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return (NimbleBridge_MTXCatalogItem.Type)NimbleBridge_MTXCatalogItem_getItemType(this);
#else
        return Type.UNKNOWN;
#endif
	}

	public string GetMetaDataUrl()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_MTXCatalogItem_getMetaDataUrl(this);
#else
        return "";
#endif
	}

	public string GetAdditionalInfo()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_MTXCatalogItem_getAdditionalInfo(this);
#else
        return "";
#endif
	}
}
