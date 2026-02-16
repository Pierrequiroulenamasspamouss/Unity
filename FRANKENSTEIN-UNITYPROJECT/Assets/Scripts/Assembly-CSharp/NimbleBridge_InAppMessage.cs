public class NimbleBridge_InAppMessage
{
	private NimbleBridge_InAppMessage()
	{
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_InAppMessage_showInAppMessage();
#endif

	public static NimbleBridge_InAppMessage GetComponent()
	{
		return new NimbleBridge_InAppMessage();
	}

	public void ShowInAppMessage()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_InAppMessage_showInAppMessage();
#endif
	}
}
