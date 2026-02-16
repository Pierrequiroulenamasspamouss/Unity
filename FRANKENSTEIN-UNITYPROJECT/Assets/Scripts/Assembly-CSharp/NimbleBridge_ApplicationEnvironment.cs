public class NimbleBridge_ApplicationEnvironment
{
	public const string NOTIFICATION_AGE_COMPLIANCE_REFRESHED = "nimble.notification.age_compliance_refreshed";

	private NimbleBridge_ApplicationEnvironment()
	{
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_ApplicationEnvironment_getApplicationBundleId();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_ApplicationEnvironment_getApplicationName();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_ApplicationEnvironment_getApplicationVersion();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_ApplicationEnvironment_getApplicationLanguageCode();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_ApplicationEnvironment_setApplicationLanguageCode(string languageCode);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_ApplicationEnvironment_getDocumentPath();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_ApplicationEnvironment_getCachePath();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_ApplicationEnvironment_getTempPath();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_ApplicationEnvironment_getCarrier();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_ApplicationEnvironment_getDeviceString();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_ApplicationEnvironment_getMACAddress();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_ApplicationEnvironment_getIPAddress();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern bool NimbleBridge_ApplicationEnvironment_isAppCracked();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern bool NimbleBridge_ApplicationEnvironment_isDeviceJailbroken();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern int NimbleBridge_ApplicationEnvironment_getAgeCompliance();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_ApplicationEnvironment_refreshAgeCompliance();
#endif

	public static NimbleBridge_ApplicationEnvironment GetComponent()
	{
		return new NimbleBridge_ApplicationEnvironment();
	}

	public string GetApplicationBundleId()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_ApplicationEnvironment_getApplicationBundleId();
#else
		return "com.dummy.bundle.id";
#endif
	}

	public string GetApplicationName()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_ApplicationEnvironment_getApplicationName();
#else
		return "MinionsDummy";
#endif
	}

	public string GetApplicationVersion()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_ApplicationEnvironment_getApplicationVersion();
#else
		return "1.0.0";
#endif
	}

	public string GetApplicationLanguageCode()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_ApplicationEnvironment_getApplicationLanguageCode();
#else
		return "en";
#endif
	}

	public void SetApplicationLanguageCode(string languageCode)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_ApplicationEnvironment_setApplicationLanguageCode(languageCode);
#endif
	}

	public string GetDocumentPath()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_ApplicationEnvironment_getDocumentPath();
#else
		return global::UnityEngine.Application.persistentDataPath;
#endif
	}

	public string GetCachePath()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_ApplicationEnvironment_getCachePath();
#else
		return global::UnityEngine.Application.temporaryCachePath;
#endif
	}

	public string GetTempPath()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_ApplicationEnvironment_getTempPath();
#else
		return global::UnityEngine.Application.temporaryCachePath;
#endif
	}

	public string GetCarrier()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_ApplicationEnvironment_getCarrier();
#else
		return "AT&T";
#endif
	}

	public string GetDeviceString()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_ApplicationEnvironment_getDeviceString();
#else
		return "PC";
#endif
	}

	public string GetMACAddress()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_ApplicationEnvironment_getMACAddress();
#else
		return "00:00:00:00:00:00";
#endif
	}

	public string GetIPAddress()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_ApplicationEnvironment_getIPAddress();
#else
		return "127.0.0.1";
#endif
	}

	public bool IsAppCracked()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_ApplicationEnvironment_isAppCracked();
#else
		return false;
#endif
	}

	public bool IsDeviceJailbroken()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_ApplicationEnvironment_isDeviceJailbroken();
#else
		return false;
#endif
	}

	public int GetAgeCompliance()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_ApplicationEnvironment_getAgeCompliance();
#else
		return 1;
#endif
	}

	public void RefreshAgeCompliance()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_ApplicationEnvironment_refreshAgeCompliance();
#endif
	}
}
