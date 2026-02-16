public class NimbleBridge_SynergyNetworkConnectionHandle : global::System.Runtime.InteropServices.SafeHandle
{
	public override bool IsInvalid
	{
		get
		{
			return base.IsClosed || handle == global::System.IntPtr.Zero;
		}
	}

	private NimbleBridge_SynergyNetworkConnectionHandle()
		: base(global::System.IntPtr.Zero, true)
	{
	}

	internal NimbleBridge_SynergyNetworkConnectionHandle(global::System.IntPtr handle)
		: base(global::System.IntPtr.Zero, true)
	{
		SetHandle(handle);
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_SynergyNetworkConnectionHandleWrapper_Dispose(NimbleBridge_SynergyNetworkConnectionHandle handleWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_SynergyNetworkConnectionHandle_wait(NimbleBridge_SynergyNetworkConnectionHandle synergyNetworkConnectionHandleWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_SynergyNetworkConnectionHandle_cancel(NimbleBridge_SynergyNetworkConnectionHandle synergyNetworkConnectionHandleWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern NimbleBridge_SynergyRequest NimbleBridge_SynergyNetworkConnectionHandle_getRequest(NimbleBridge_SynergyNetworkConnectionHandle synergyNetworkConnectionHandleWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern NimbleBridge_SynergyResponse NimbleBridge_SynergyNetworkConnectionHandle_getResponse(NimbleBridge_SynergyNetworkConnectionHandle synergyNetworkConnectionHandleWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern global::System.IntPtr NimbleBridge_SynergyNetworkConnectionHandle_getHeaderCallback(NimbleBridge_SynergyNetworkConnectionHandle synergyNetworkConnectionHandleWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern void NimbleUnity_SynergyNetworkConnectionHandle_setHeaderCallback(NimbleBridge_SynergyNetworkConnectionHandle synergyNetworkConnectionHandleWrapper, global::System.IntPtr callbackData);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern global::System.IntPtr NimbleBridge_SynergyNetworkConnectionHandle_getProgressCallback(NimbleBridge_SynergyNetworkConnectionHandle synergyNetworkConnectionHandleWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern void NimbleUnity_SynergyNetworkConnectionHandle_setProgressCallback(NimbleBridge_SynergyNetworkConnectionHandle synergyNetworkConnectionHandleWrapper, global::System.IntPtr callbackData);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern global::System.IntPtr NimbleBridge_SynergyNetworkConnectionHandle_getCompletionCallback(NimbleBridge_SynergyNetworkConnectionHandle synergyNetworkConnectionHandleWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern void NimbleUnity_SynergyNetworkConnectionHandle_setCompletionCallback(NimbleBridge_SynergyNetworkConnectionHandle synergyNetworkConnectionHandleWrapper, global::System.IntPtr callbackData);
#endif

	protected override bool ReleaseHandle()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_SynergyNetworkConnectionHandleWrapper_Dispose(this);
#endif
		return true;
	}

	public void Wait()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_SynergyNetworkConnectionHandle_wait(this);
#endif
	}

	public void Cancel()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_SynergyNetworkConnectionHandle_cancel(this);
#endif
	}

	public NimbleBridge_SynergyRequest GetRequest()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_SynergyNetworkConnectionHandle_getRequest(this);
#else
		return new NimbleBridge_SynergyRequest(global::System.IntPtr.Zero);
#endif
	}

	public NimbleBridge_SynergyResponse GetResponse()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_SynergyNetworkConnectionHandle_getResponse(this);
#else
		return new NimbleBridge_SynergyResponse();
#endif
	}

	public SynergyNetworkConnectionCallback GetHeaderCallback()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		global::System.IntPtr callback = NimbleBridge_SynergyNetworkConnectionHandle_getHeaderCallback(this);
		SynergyNetworkCallbackConverter synergyNetworkCallbackConverter = (SynergyNetworkCallbackConverter)NimbleBridge_CallbackHelper.Get().GetCallback(callback);
		return synergyNetworkCallbackConverter.m_callback;
#else
		return null;
#endif
	}

	public void SetHeaderCallback(SynergyNetworkConnectionCallback synergyNetworkConnectionCallback)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		global::System.IntPtr callbackData = NimbleBridge_CallbackHelper.Get().AddCallback(new SynergyNetworkCallbackConverter(synergyNetworkConnectionCallback));
		NimbleUnity_SynergyNetworkConnectionHandle_setHeaderCallback(this, callbackData);
#endif
	}

	public SynergyNetworkConnectionCallback GetProgressCallback()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		global::System.IntPtr callback = NimbleBridge_SynergyNetworkConnectionHandle_getProgressCallback(this);
		SynergyNetworkCallbackConverter synergyNetworkCallbackConverter = (SynergyNetworkCallbackConverter)NimbleBridge_CallbackHelper.Get().GetCallback(callback);
		return synergyNetworkCallbackConverter.m_callback;
#else
		return null;
#endif
	}

	public void SetProgressCallback(SynergyNetworkConnectionCallback synergyNetworkConnectionCallback)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		global::System.IntPtr callbackData = NimbleBridge_CallbackHelper.Get().AddCallback(new SynergyNetworkCallbackConverter(synergyNetworkConnectionCallback));
		NimbleUnity_SynergyNetworkConnectionHandle_setProgressCallback(this, callbackData);
#endif
	}

	public SynergyNetworkConnectionCallback GetCompletionCallback()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		global::System.IntPtr callback = NimbleBridge_SynergyNetworkConnectionHandle_getCompletionCallback(this);
		SynergyNetworkCallbackConverter synergyNetworkCallbackConverter = (SynergyNetworkCallbackConverter)NimbleBridge_CallbackHelper.Get().GetCallback(callback);
		return synergyNetworkCallbackConverter.m_callback;
#else
		return null;
#endif
	}

	public void SetCompletionCallback(SynergyNetworkConnectionCallback synergyNetworkConnectionCallback)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		global::System.IntPtr callbackData = NimbleBridge_CallbackHelper.Get().AddCallback(new SynergyNetworkCallbackConverter(synergyNetworkConnectionCallback));
		NimbleUnity_SynergyNetworkConnectionHandle_setCompletionCallback(this, callbackData);
#endif
	}
}
