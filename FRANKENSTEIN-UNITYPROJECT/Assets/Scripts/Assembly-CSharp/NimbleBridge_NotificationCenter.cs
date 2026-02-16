public class NimbleBridge_NotificationCenter
{
#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_NotificationCenter_registerListener(string notification, NimbleBridge_NotificationListener listenerWrapper);

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_NotificationCenter_unregisterListener(NimbleBridge_NotificationListener listenerWrapper);
#endif
#endif

	public static void RegisterListener(string notification, NimbleBridge_NotificationListener listener)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_NotificationCenter_registerListener(notification, listener);
#endif
	}

	public static void UnregisterListener(NimbleBridge_NotificationListener listener)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_NotificationCenter_unregisterListener(listener);
#endif
	}
}
