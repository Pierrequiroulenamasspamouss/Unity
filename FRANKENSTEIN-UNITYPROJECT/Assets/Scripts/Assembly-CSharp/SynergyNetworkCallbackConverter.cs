internal class SynergyNetworkCallbackConverter : NimbleBridge_Callback
{
	internal SynergyNetworkConnectionCallback m_callback;

	internal SynergyNetworkCallbackConverter(SynergyNetworkConnectionCallback callback)
	{
		m_callback = callback;
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern void NimbleUnity_SynergyNetworkConnectionCallback_getData(global::System.IntPtr callback, out global::System.IntPtr connectionHandle);
#endif

	public void Callback(global::System.IntPtr id)
	{
		try
		{
			if (m_callback != null)
			{
#if UNITY_ANDROID && !UNITY_EDITOR
				global::System.IntPtr connectionHandle;
				NimbleUnity_SynergyNetworkConnectionCallback_getData(id, out connectionHandle);
				m_callback(new NimbleBridge_SynergyNetworkConnectionHandle(connectionHandle));
#else
                m_callback(new NimbleBridge_SynergyNetworkConnectionHandle(global::System.IntPtr.Zero));
#endif
			}
		}
		finally
		{
			NimbleBridge_CallbackHelper.Get().NotifyCallbackComplete(id);
		}
	}
}
