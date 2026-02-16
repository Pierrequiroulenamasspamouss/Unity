public class NimbleBridge_NetworkConnectionHandle : global::System.Runtime.InteropServices.SafeHandle
{
	public override bool IsInvalid
	{
		get
		{
			return base.IsClosed || handle == global::System.IntPtr.Zero;
		}
	}

	private NimbleBridge_NetworkConnectionHandle()
		: base(global::System.IntPtr.Zero, true)
	{
	}

	internal NimbleBridge_NetworkConnectionHandle(global::System.IntPtr handle)
		: base(global::System.IntPtr.Zero, true)
	{
		SetHandle(handle);
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_NetworkConnectionHandleWrapper_Dispose(NimbleBridge_NetworkConnectionHandle handleWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_NetworkConnectionHandle_wait(NimbleBridge_NetworkConnectionHandle networkConnectionHandleWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_NetworkConnectionHandle_cancel(NimbleBridge_NetworkConnectionHandle networkConnectionHandleWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern NimbleBridge_HttpRequest NimbleBridge_NetworkConnectionHandle_getRequest(NimbleBridge_NetworkConnectionHandle networkConnectionHandleWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern NimbleBridge_HttpResponse NimbleBridge_NetworkConnectionHandle_getResponse(NimbleBridge_NetworkConnectionHandle networkConnectionHandleWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern global::System.IntPtr NimbleBridge_NetworkConnectionHandle_getHeaderCallback(NimbleBridge_NetworkConnectionHandle networkConnectionHandleWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern void NimbleUnity_NetworkConnectionHandle_setHeaderCallback(NimbleBridge_NetworkConnectionHandle networkConnectionHandleWrapper, global::System.IntPtr callbackData);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern global::System.IntPtr NimbleBridge_NetworkConnectionHandle_getProgressCallback(NimbleBridge_NetworkConnectionHandle networkConnectionHandleWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern void NimbleUnity_NetworkConnectionHandle_setProgressCallback(NimbleBridge_NetworkConnectionHandle networkConnectionHandleWrapper, global::System.IntPtr callbackData);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern global::System.IntPtr NimbleBridge_NetworkConnectionHandle_getCompletionCallback(NimbleBridge_NetworkConnectionHandle networkConnectionHandleWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern void NimbleUnity_NetworkConnectionHandle_setCompletionCallback(NimbleBridge_NetworkConnectionHandle networkConnectionHandleWrapper, global::System.IntPtr callbackData);
#endif

	protected override bool ReleaseHandle()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_NetworkConnectionHandleWrapper_Dispose(this);
#endif
		return true;
	}

	public void Wait()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_NetworkConnectionHandle_wait(this);
#endif
	}

	public void Cancel()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_NetworkConnectionHandle_cancel(this);
#endif
	}

	public NimbleBridge_HttpRequest GetRequest()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_NetworkConnectionHandle_getRequest(this);
#else
        return new NimbleBridge_HttpRequest();
#endif
	}

	public NimbleBridge_HttpResponse GetResponse()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_NetworkConnectionHandle_getResponse(this);
#else
        return new NimbleBridge_HttpResponse();
#endif
	}

	public NetworkConnectionCallback GetHeaderCallback()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		global::System.IntPtr callback = NimbleBridge_NetworkConnectionHandle_getHeaderCallback(this);
		NetworkCallbackConverter networkCallbackConverter = (NetworkCallbackConverter)NimbleBridge_CallbackHelper.Get().GetCallback(callback);
		return networkCallbackConverter.m_callback;
#else
        return null; // Stub
#endif
	}

	public void SetHeaderCallback(NetworkConnectionCallback networkConnectionCallback)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		global::System.IntPtr callbackData = NimbleBridge_CallbackHelper.Get().AddCallback(new NetworkCallbackConverter(networkConnectionCallback));
		NimbleUnity_NetworkConnectionHandle_setHeaderCallback(this, callbackData);
#endif
	}

	public NetworkConnectionCallback GetProgressCallback()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		global::System.IntPtr callback = NimbleBridge_NetworkConnectionHandle_getProgressCallback(this);
		NetworkCallbackConverter networkCallbackConverter = (NetworkCallbackConverter)NimbleBridge_CallbackHelper.Get().GetCallback(callback);
		return networkCallbackConverter.m_callback;
#else
        return null;
#endif
	}

	public void SetProgressCallback(NetworkConnectionCallback networkConnectionCallback)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		global::System.IntPtr callbackData = NimbleBridge_CallbackHelper.Get().AddCallback(new NetworkCallbackConverter(networkConnectionCallback));
		NimbleUnity_NetworkConnectionHandle_setProgressCallback(this, callbackData);
#endif
	}

	public NetworkConnectionCallback GetCompletionCallback()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		global::System.IntPtr callback = NimbleBridge_NetworkConnectionHandle_getCompletionCallback(this);
		NetworkCallbackConverter networkCallbackConverter = (NetworkCallbackConverter)NimbleBridge_CallbackHelper.Get().GetCallback(callback);
		return networkCallbackConverter.m_callback;
#else
        return null;
#endif
	}

	public void SetCompletionCallback(NetworkConnectionCallback networkConnectionCallback)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		global::System.IntPtr callbackData = NimbleBridge_CallbackHelper.Get().AddCallback(new NetworkCallbackConverter(networkConnectionCallback));
		NimbleUnity_NetworkConnectionHandle_setCompletionCallback(this, callbackData);
#endif
	}
}
