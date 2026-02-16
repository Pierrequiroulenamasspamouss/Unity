public class NimbleBridge_SynergyRequest : global::System.Runtime.InteropServices.SafeHandle
{
	private class SynergyRequestPreparingCallbackConverter : NimbleBridge_Callback
	{
		public SynergyRequestPreparingCallback m_callback;

		public SynergyRequestPreparingCallbackConverter(SynergyRequestPreparingCallback callback)
		{
			m_callback = callback;
		}

#if UNITY_ANDROID && !UNITY_EDITOR
		[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
		private static extern void NimbleUnity_SynergyRequestPreparingCallback_getData(global::System.IntPtr id, out global::System.IntPtr request);
#else
		private static void NimbleUnity_SynergyRequestPreparingCallback_getData(global::System.IntPtr id, out global::System.IntPtr request) { request = global::System.IntPtr.Zero; }
#endif

		public void Callback(global::System.IntPtr id)
		{
			try
			{
				if (m_callback != null)
				{
					global::System.IntPtr request;
					NimbleUnity_SynergyRequestPreparingCallback_getData(id, out request);
					m_callback(new NimbleBridge_SynergyRequest(request));
				}
			}
			finally
			{
				NimbleBridge_CallbackHelper.Get().NotifyCallbackComplete(id);
			}
		}
	}

	public override bool IsInvalid
	{
		get
		{
			return base.IsClosed || handle == global::System.IntPtr.Zero;
		}
	}

	private NimbleBridge_SynergyRequest()
		: base(global::System.IntPtr.Zero, true)
	{
	}

	internal NimbleBridge_SynergyRequest(global::System.IntPtr handle)
		: base(global::System.IntPtr.Zero, true)
	{
		SetHandle(handle);
	}

	public NimbleBridge_SynergyRequest(string api, NimbleBridge_HttpRequest.Method method, SynergyRequestPreparingCallback callback)
		: this()
	{
		global::System.IntPtr callbackData = NimbleBridge_CallbackHelper.Get().AddCallback(new NimbleBridge_SynergyRequest.SynergyRequestPreparingCallbackConverter(callback));
		global::System.IntPtr intPtr = NimbleUnity_SynergyRequest_SynergyRequest(api, (int)method, callbackData);
		SetHandle(intPtr);
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_SynergyRequest_Dispose(NimbleBridge_SynergyRequest synergyRequestWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern global::System.IntPtr NimbleUnity_SynergyRequest_SynergyRequest(string api, int method, global::System.IntPtr callbackData);
#else
	private static global::System.IntPtr NimbleUnity_SynergyRequest_SynergyRequest(string api, int method, global::System.IntPtr callbackData) { return global::System.IntPtr.Zero; }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern NimbleBridge_HttpRequest NimbleBridge_SynergyRequest_getHttpRequest(NimbleBridge_SynergyRequest synergyRequestWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_SynergyRequest_getBaseUrl(NimbleBridge_SynergyRequest synergyRequestWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_SynergyRequest_getApi(NimbleBridge_SynergyRequest synergyRequestWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern global::System.IntPtr NimbleBridge_SynergyRequest_getUrlParameters(NimbleBridge_SynergyRequest synergyRequestWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_SynergyRequest_getJsonData(NimbleBridge_SynergyRequest synergyRequestWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_SynergyRequest_setHttpRequest(NimbleBridge_SynergyRequest synergyRequestWrapper, NimbleBridge_HttpRequest requestWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_SynergyRequest_setBaseUrl(NimbleBridge_SynergyRequest synergyRequestWrapper, string url);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_SynergyRequest_setApi(NimbleBridge_SynergyRequest synergyRequestWrapper, string api);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_SynergyRequest_setUrlParameters(NimbleBridge_SynergyRequest synergyRequestWrapper, global::System.IntPtr parameters);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_SynergyRequest_setJsonData(NimbleBridge_SynergyRequest synergyRequestWrapper, string jsonData);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern int NimbleBridge_SynergyRequest_getMethod(NimbleBridge_SynergyRequest synergyRequestWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_SynergyRequest_setMethod(NimbleBridge_SynergyRequest synergyRequestWrapper, int method);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern global::System.IntPtr NimbleBridge_SynergyRequest_getPrepareRequestCallback(NimbleBridge_SynergyRequest synergyRequestWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern void NimbleUnity_SynergyRequest_setPrepareRequestCallback(NimbleBridge_SynergyRequest synergyRequestWrapper, global::System.IntPtr callbackData);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_SynergyRequest_send(NimbleBridge_SynergyRequest synergyRequestWrapper);
#endif

	protected override bool ReleaseHandle()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_SynergyRequest_Dispose(this);
#endif
		return true;
	}

	public NimbleBridge_HttpRequest GetHttpRequest()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_SynergyRequest_getHttpRequest(this);
#else
		return null;
#endif
	}

	public string GetBaseUrl()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_SynergyRequest_getBaseUrl(this);
#else
		return "";
#endif
	}

	public string GetApi()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_SynergyRequest_getApi(this);
#else
		return "";
#endif
	}

	public global::System.Collections.Generic.Dictionary<string, string> GetUrlParameters()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		global::System.IntPtr mapPtr = NimbleBridge_SynergyRequest_getUrlParameters(this);
		return MarshalUtility.ConvertPtrToDictionary(mapPtr);
#else
		return new global::System.Collections.Generic.Dictionary<string, string>();
#endif
	}

	public string GetJsonData()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_SynergyRequest_getJsonData(this);
#else
		return "{}";
#endif
	}

	public void SetHttpRequest(NimbleBridge_HttpRequest request)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_SynergyRequest_setHttpRequest(this, request);
#endif
	}

	public void SetBaseUrl(string url)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_SynergyRequest_setBaseUrl(this, url);
#endif
	}

	public void SetApi(string api)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_SynergyRequest_setApi(this, api);
#endif
	}

	public void SetUrlParameters(global::System.Collections.Generic.Dictionary<string, string> parameters)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		global::System.IntPtr intPtr = global::System.IntPtr.Zero;
		try
		{
			intPtr = MarshalUtility.ConvertDictionaryToPtr(parameters);
			NimbleBridge_SynergyRequest_setUrlParameters(this, intPtr);
		}
		finally
		{
			if (intPtr != global::System.IntPtr.Zero)
			{
				MarshalUtility.DisposeMapPtr(intPtr);
			}
		}
#endif
	}

	public void SetJsonData(string jsonData)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_SynergyRequest_setJsonData(this, jsonData);
#endif
	}

	public NimbleBridge_HttpRequest.Method GetMethod()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return (NimbleBridge_HttpRequest.Method)NimbleBridge_SynergyRequest_getMethod(this);
#else
		return NimbleBridge_HttpRequest.Method.HTTP_GET;
#endif
	}

	public void SetMethod(NimbleBridge_HttpRequest.Method method)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_SynergyRequest_setMethod(this, (int)method);
#endif
	}

	public SynergyRequestPreparingCallback GetPrepareRequestCallback()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		global::System.IntPtr callback = NimbleBridge_SynergyRequest_getPrepareRequestCallback(this);
		NimbleBridge_SynergyRequest.SynergyRequestPreparingCallbackConverter synergyRequestPreparingCallbackConverter = (NimbleBridge_SynergyRequest.SynergyRequestPreparingCallbackConverter)NimbleBridge_CallbackHelper.Get().GetCallback(callback);
		return synergyRequestPreparingCallbackConverter.m_callback;
#else
		return null;
#endif
	}

	public void SetPrepareRequestCallback(SynergyRequestPreparingCallback callback)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		global::System.IntPtr callbackData = NimbleBridge_CallbackHelper.Get().AddCallback(new NimbleBridge_SynergyRequest.SynergyRequestPreparingCallbackConverter(callback));
		NimbleUnity_SynergyRequest_setPrepareRequestCallback(this, callbackData);
#endif
	}

	public void Send()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_SynergyRequest_send(this);
#endif
	}
}
