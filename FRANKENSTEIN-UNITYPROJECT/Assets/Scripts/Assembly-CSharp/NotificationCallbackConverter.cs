internal class NotificationCallbackConverter : NimbleBridge_Callback
{
	private NotificationCallback m_callback;

	private NimbleBridge_NotificationListener m_listener;

	internal NotificationCallbackConverter(NotificationCallback callback, NimbleBridge_NotificationListener listener)
	{
		m_callback = callback;
		m_listener = listener;
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern void NimbleUnity_NotificationCallback_getData(global::System.IntPtr callback, out global::System.IntPtr name, out global::System.IntPtr userData);
#endif

	public void Callback(global::System.IntPtr id)
	{
		try
		{
			if (m_callback != null)
			{
#if UNITY_ANDROID && !UNITY_EDITOR
				global::System.IntPtr name;
				global::System.IntPtr userData;
				NimbleUnity_NotificationCallback_getData(id, out name, out userData);
				string name2 = string.Empty;
				string aJSON = string.Empty;
				if (name != global::System.IntPtr.Zero)
				{
					name2 = global::System.Runtime.InteropServices.Marshal.PtrToStringAuto(name);
				}
				if (userData != global::System.IntPtr.Zero)
				{
					aJSON = global::System.Runtime.InteropServices.Marshal.PtrToStringAuto(userData);
				}
				global::SimpleJSON.JSONNode jSONNode = global::SimpleJSON.JSON.Parse(aJSON);
				global::System.Collections.Generic.Dictionary<string, object> userData2 = MarshalUtility.ConvertJsonToDictionary((global::SimpleJSON.JSONClass)jSONNode);
				m_callback(name2, userData2, m_listener);
#else
                // Stub: Do nothing or fire with empty data
                m_callback("StubNotification", new global::System.Collections.Generic.Dictionary<string, object>(), m_listener);
#endif
			}
		}
		finally
		{
			NimbleBridge_CallbackHelper.Get().NotifyCallbackComplete(id);
		}
	}
}
