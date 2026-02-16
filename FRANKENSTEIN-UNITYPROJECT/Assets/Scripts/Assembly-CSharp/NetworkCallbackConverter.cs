internal class NetworkCallbackConverter : NimbleBridge_Callback
{
	internal NetworkConnectionCallback m_callback;

	internal NetworkCallbackConverter(NetworkConnectionCallback callback)
	{
		m_callback = callback;
	}

	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern void NimbleUnity_NetworkConnectionCallback_getData(global::System.IntPtr callback, out global::System.IntPtr connectionHandle);

	public void Callback(global::System.IntPtr id)
	{
		try
		{
			if (m_callback != null)
			{
				global::System.IntPtr connectionHandle;
				NimbleUnity_NetworkConnectionCallback_getData(id, out connectionHandle);
				m_callback(new NimbleBridge_NetworkConnectionHandle(connectionHandle));
			}
		}
		finally
		{
			NimbleBridge_CallbackHelper.Get().NotifyCallbackComplete(id);
		}
	}
}
