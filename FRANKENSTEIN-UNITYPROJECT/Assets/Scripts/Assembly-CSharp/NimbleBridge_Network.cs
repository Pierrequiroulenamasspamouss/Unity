public class NimbleBridge_Network
{
	public enum NimbleBridge_Network_Status
	{
		STATUS_UNKNOWN = 0,
		STATUS_NONE = 1,
		STATUS_DEAD = 2,
		STATUS_OK = 3
	}

	private NimbleBridge_Network()
	{
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern NimbleBridge_NetworkConnectionHandle NimbleUnity_Network_sendGetRequest(string url, global::System.IntPtr callbackData);
#else
	private static NimbleBridge_NetworkConnectionHandle NimbleUnity_Network_sendGetRequest(string url, global::System.IntPtr callbackData) { return new NimbleBridge_NetworkConnectionHandle(global::System.IntPtr.Zero); }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern NimbleBridge_NetworkConnectionHandle NimbleUnity_Network_sendPostRequest(string url, global::System.IntPtr data, global::System.IntPtr callbackData);
#else
	private static NimbleBridge_NetworkConnectionHandle NimbleUnity_Network_sendPostRequest(string url, global::System.IntPtr data, global::System.IntPtr callbackData) { return new NimbleBridge_NetworkConnectionHandle(global::System.IntPtr.Zero); }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern NimbleBridge_NetworkConnectionHandle NimbleUnity_Network_sendRequest(NimbleBridge_HttpRequest request, global::System.IntPtr callbackData);
#else
	private static NimbleBridge_NetworkConnectionHandle NimbleUnity_Network_sendRequest(NimbleBridge_HttpRequest request, global::System.IntPtr callbackData) { return new NimbleBridge_NetworkConnectionHandle(global::System.IntPtr.Zero); }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_Network_forceRedetectNetworkStatus();
#else
	private static void NimbleBridge_Network_forceRedetectNetworkStatus() { }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern int NimbleBridge_Network_getNetworkStatus();
#else
	private static int NimbleBridge_Network_getNetworkStatus() { return (int)NimbleBridge_Network_Status.STATUS_OK; }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern bool NimbleBridge_Network_isNetworkWifi();
#else
	private static bool NimbleBridge_Network_isNetworkWifi() { return true; }
#endif

	public static NimbleBridge_Network GetComponent()
	{
		return new NimbleBridge_Network();
	}

	public NimbleBridge_NetworkConnectionHandle SendGetRequest(string url, NetworkConnectionCallback callback)
	{
		NetworkCallbackConverter callback2 = new NetworkCallbackConverter(callback);
		global::System.IntPtr callbackData = NimbleBridge_CallbackHelper.Get().AddCallback(callback2);
		return NimbleUnity_Network_sendGetRequest(url, callbackData);
	}

	public NimbleBridge_NetworkConnectionHandle SendPostRequest(string url, byte[] data, NetworkConnectionCallback callback)
	{
		NetworkCallbackConverter callback2 = new NetworkCallbackConverter(callback);
		global::System.IntPtr callbackData = NimbleBridge_CallbackHelper.Get().AddCallback(callback2);
		global::System.IntPtr intPtr = global::System.IntPtr.Zero;
		try
		{
			intPtr = MarshalUtility.ConvertDataToPtr(data);
			return NimbleUnity_Network_sendPostRequest(url, intPtr, callbackData);
		}
		finally
		{
			if (intPtr != global::System.IntPtr.Zero)
			{
				MarshalUtility.DisposeDataPtr(intPtr);
			}
		}
	}

	public NimbleBridge_NetworkConnectionHandle SendRequest(NimbleBridge_HttpRequest request, NetworkConnectionCallback callback)
	{
		NetworkCallbackConverter callback2 = new NetworkCallbackConverter(callback);
		global::System.IntPtr callbackData = NimbleBridge_CallbackHelper.Get().AddCallback(callback2);
		return NimbleUnity_Network_sendRequest(request, callbackData);
	}

	public void ForceRedetectNetworkStatus()
	{
		NimbleBridge_Network_forceRedetectNetworkStatus();
	}

	public NimbleBridge_Network.NimbleBridge_Network_Status GetNetworkStatus()
	{
		return (NimbleBridge_Network.NimbleBridge_Network_Status)NimbleBridge_Network_getNetworkStatus();
	}

	public bool IsNetworkWifi()
	{
		return NimbleBridge_Network_isNetworkWifi();
	}
}
