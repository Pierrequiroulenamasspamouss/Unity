public class NimbleBridge_Utility
{
#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_Utility_getUTCDateStringFormat(double date);

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_Utility_SHA256HashString(string str);
#endif
#endif

	public static string GetUTCDateStringFormat(double date)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_Utility_getUTCDateStringFormat(date);
#else
        return "";
#endif
	}

	public static string SHA256HashString(string str)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_Utility_SHA256HashString(str);
#else
        return "";
#endif
	}
}
