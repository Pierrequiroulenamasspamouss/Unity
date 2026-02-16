public class NimbleBridge_SynergyNetwork
{
	public enum NimbleBridge_SynergyNetwork_Status
	{
		STATUS_UNKNOWN = 0,
		STATUS_NONE = 1,
		STATUS_DEAD = 2,
		STATUS_OK = 3
	}

	private NimbleBridge_SynergyNetwork()
	{
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern NimbleBridge_SynergyNetworkConnectionHandle NimbleUnity_SynergyNetwork_sendGetRequest(string baseUrl, string api, global::System.IntPtr urlParameters, global::System.IntPtr callbackData);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern NimbleBridge_SynergyNetworkConnectionHandle NimbleUnity_SynergyNetwork_sendPostRequest(string baseUrl, string api, global::System.IntPtr urlParameters, string jsonPostBody, global::System.IntPtr callbackData);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern global::System.IntPtr NimbleUnity_SynergyNetwork_sendPostRequest_withHeaders(string baseUrl, string api, global::System.IntPtr additionalHeaders, global::System.IntPtr urlParameters, string jsonPostBody, global::System.IntPtr callbackData);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern void NimbleUnity_SynergyNetwork_sendRequest(NimbleBridge_SynergyRequest request, global::System.IntPtr callbackData);
#endif

	public static NimbleBridge_SynergyNetwork GetComponent()
	{
		return new NimbleBridge_SynergyNetwork();
	}

	public NimbleBridge_SynergyNetworkConnectionHandle SendGetRequest(string baseUrl, string api, global::System.Collections.Generic.Dictionary<string, string> urlParameters, SynergyNetworkConnectionCallback callback)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		SynergyNetworkCallbackConverter callback2 = new SynergyNetworkCallbackConverter(callback);
		global::System.IntPtr callbackData = NimbleBridge_CallbackHelper.Get().AddCallback(callback2);
		global::System.IntPtr intPtr = global::System.IntPtr.Zero;
		try
		{
			intPtr = MarshalUtility.ConvertDictionaryToPtr(urlParameters);
			return NimbleUnity_SynergyNetwork_sendGetRequest(baseUrl, api, intPtr, callbackData);
		}
		finally
		{
			if (intPtr != global::System.IntPtr.Zero)
			{
				MarshalUtility.DisposeMapPtr(intPtr);
			}
		}
#else
        return new NimbleBridge_SynergyNetworkConnectionHandle(global::System.IntPtr.Zero);
#endif
	}

	public NimbleBridge_SynergyNetworkConnectionHandle SendPostRequest(string baseUrl, string api, global::System.Collections.Generic.Dictionary<string, string> urlParameters, string jsonPostBody, SynergyNetworkConnectionCallback callback)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		SynergyNetworkCallbackConverter callback2 = new SynergyNetworkCallbackConverter(callback);
		global::System.IntPtr callbackData = NimbleBridge_CallbackHelper.Get().AddCallback(callback2);
		global::System.IntPtr intPtr = global::System.IntPtr.Zero;
		try
		{
			intPtr = MarshalUtility.ConvertDictionaryToPtr(urlParameters);
			return NimbleUnity_SynergyNetwork_sendPostRequest(baseUrl, api, intPtr, jsonPostBody, callbackData);
		}
		finally
		{
			if (intPtr != global::System.IntPtr.Zero)
			{
				MarshalUtility.DisposeMapPtr(intPtr);
			}
		}
#else
        return new NimbleBridge_SynergyNetworkConnectionHandle(global::System.IntPtr.Zero);
#endif
	}

	public void SendRequest(NimbleBridge_SynergyRequest request, SynergyNetworkConnectionCallback callback)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		SynergyNetworkCallbackConverter callback2 = new SynergyNetworkCallbackConverter(callback);
		global::System.IntPtr callbackData = NimbleBridge_CallbackHelper.Get().AddCallback(callback2);
		NimbleUnity_SynergyNetwork_sendRequest(request, callbackData);
#endif
	}
}
