public class NimbleBridge_SynergyEnvironment
{
	public enum NetworkConnectionType
	{
		NETWORK_CONNECTION_UNKNOWN = 0,
		NETWORK_CONNECTION_NONE = 1,
		NETWORK_CONNECTION_WIFI = 2,
		NETWORK_CONNECTION_WIRELESS = 3
	}

	public enum SynergyAppVersionCheckResult
	{
		SYNERGY_APP_VERSION_OK = 0,
		SYNERGY_APP_VERSION_UPDATE_RECOMMENDED = 1,
		SYNERGY_APP_VERSION_UPDATE_REQUIRED = 2
	}

	public const string SERVER_URL_KEY_SYNERGY_DRM = "synergy.drm";

	public const string SERVER_URL_KEY_SYNERGY_DIRECTOR = "synergy.director";

	public const string SERVER_URL_KEY_SYNERGY_MESSAGE_TO_USER = "synergy.m2u";

	public const string SERVER_URL_KEY_SYNERGY_PRODUCT = "synergy.product";

	public const string SERVER_URL_KEY_SYNERGY_TRACKING = "synergy.tracking";

	public const string SERVER_URL_KEY_SYNERGY_USER = "synergy.user";

	public const string SERVER_URL_KEY_SYNERGY_CENTRAL_IP_GEOLOCATION = "synergy.cipgl";

	public const string SERVER_URL_KEY_SYNERGY_S2S = "synergy.s2s";

	public const string SERVER_URL_KEY_ORIGIN_FRIENDS = "friends.url";

	public const string SERVER_URL_KEY_ORIGIN_AVATAR = "avatars.url";

	public const string SERVER_URL_KEY_ORIGIN_CASUAL_APP = "origincasualapp.url";

	public const string SERVER_URL_KEY_ORIGIN_CASUAL_SERVER = "origincasualserver.url";

	public const string SERVER_URL_KEY_AKAMAI = "akamai.url";

	public const string SERVER_URL_KEY_DYNAMIC_MORE_GAMES = "dmg.url";

	public const string SERVER_URL_KEY_MAYHEM = "mayhem.url";

	public const string SYNERGY_SERVER_FLAGS_CAN_SEND_MAC_ADDRESS = "sendMacAddress";

	public const string SYNERGY_SERVER_FLAGS_CAN_SEND_UDID = "sendUDID";

	public const string SYNERGY_ENVIRONMENT_NOTIFICATION_STARTUP_REQUESTS_STARTED = "nimble.environment.notification.startup_requests_started";

	public const string SYNERGY_ENVIRONMENT_NOTIFICATION_STARTUP_REQUESTS_FINISHED = "nimble.environment.notification.startup_requests_finished";

	public const string SYNERGY_ENVIRONMENT_NOTIFICATION_STARTUP_ENVIRONMENT_DATA_CHANGED = "nimble.environment.notification.startup_environment_data_changed";

	public const string SYNERGY_ENVIRONMENT_NOTIFICATION_APP_VERSION_CHECK_FINISHED = "nimble.environment.notification.app_version_check_finished";

	public const string SYNERGY_ENVIRONMENT_NOTIFICATION_RESTORED_FROM_PERSISTENT = "nimble.environment.notification.restored_from_persistent";

	private NimbleBridge_SynergyEnvironment()
	{
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_SynergyEnvironment_getEADeviceId();
#else
	private static string NimbleBridge_SynergyEnvironment_getEADeviceId() { return ""; }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_SynergyEnvironment_getSynergyId();
#else
	private static string NimbleBridge_SynergyEnvironment_getSynergyId() { return ""; }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_SynergyEnvironment_getSellId();
#else
	private static string NimbleBridge_SynergyEnvironment_getSellId() { return ""; }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_SynergyEnvironment_getProductId();
#else
	private static string NimbleBridge_SynergyEnvironment_getProductId() { return ""; }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_SynergyEnvironment_getEAHardwareId();
#else
	private static string NimbleBridge_SynergyEnvironment_getEAHardwareId() { return ""; }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_SynergyEnvironment_getServerUrlWithKey(string key);
#else
	private static string NimbleBridge_SynergyEnvironment_getServerUrlWithKey(string key) { return ""; }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern bool NimbleBridge_SynergyEnvironment_getSynergyServerFlags(string flagName);
#else
	private static bool NimbleBridge_SynergyEnvironment_getSynergyServerFlags(string flagName) { return false; }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern int NimbleBridge_SynergyEnvironment_getLatestAppVersionCheckResult();
#else
	private static int NimbleBridge_SynergyEnvironment_getLatestAppVersionCheckResult() { return (int)SynergyAppVersionCheckResult.SYNERGY_APP_VERSION_OK; }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern bool NimbleBridge_SynergyEnvironment_isDataAvailable();
#else
	private static bool NimbleBridge_SynergyEnvironment_isDataAvailable() { return false; }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern bool NimbleBridge_SynergyEnvironment_isUpdateInProgress();
#else
	private static bool NimbleBridge_SynergyEnvironment_isUpdateInProgress() { return false; }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern NimbleBridge_Error NimbleBridge_SynergyEnvironment_checkAndInitiateSynergyEnvironmentUpdate();
#else
	private static NimbleBridge_Error NimbleBridge_SynergyEnvironment_checkAndInitiateSynergyEnvironmentUpdate() { return new NimbleBridge_Error(NimbleBridge_Error.Code.OK, ""); }
#endif

	public static NimbleBridge_SynergyEnvironment GetComponent()
	{
		return new NimbleBridge_SynergyEnvironment();
	}

	public string GetEADeviceId()
	{
		return NimbleBridge_SynergyEnvironment_getEADeviceId();
	}

	public string GetSynergyId()
	{
		return NimbleBridge_SynergyEnvironment_getSynergyId();
	}

	public string GetSellId()
	{
		return NimbleBridge_SynergyEnvironment_getSellId();
	}

	public string GetProductId()
	{
		return NimbleBridge_SynergyEnvironment_getProductId();
	}

	public string GetEAHardwareId()
	{
		return NimbleBridge_SynergyEnvironment_getEAHardwareId();
	}

	public string GetServerUrlWithKey(string key)
	{
		return NimbleBridge_SynergyEnvironment_getServerUrlWithKey(key);
	}

	public bool GetSynergyServerFlags(string flagName)
	{
		return NimbleBridge_SynergyEnvironment_getSynergyServerFlags(flagName);
	}

	public NimbleBridge_SynergyEnvironment.SynergyAppVersionCheckResult GetLatestAppVersionCheckResult()
	{
		return (NimbleBridge_SynergyEnvironment.SynergyAppVersionCheckResult)NimbleBridge_SynergyEnvironment_getLatestAppVersionCheckResult();
	}

	public bool IsDataAvailable()
	{
		return NimbleBridge_SynergyEnvironment_isDataAvailable();
	}

	public bool IsUpdateInProgress()
	{
		return NimbleBridge_SynergyEnvironment_isUpdateInProgress();
	}

	public NimbleBridge_Error CheckAndInitiateSynergyEnvironmentUpdate()
	{
		return NimbleBridge_SynergyEnvironment_checkAndInitiateSynergyEnvironmentUpdate();
	}
}
