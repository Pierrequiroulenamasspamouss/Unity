public class NimbleBridge_Base
{
	public enum NimbleBridge_Base_NimbleConfiguration
	{
		CONFIGURATION_UNKNOWN = 0,
		CONFIGURATION_INTEGRATION = 1,
		CONFIGURATION_STAGE = 2,
		CONFIGURATION_LIVE = 3,
		CONFIGURATION_CUSTOMIZED = 4
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_Base_setupNimble();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_Base_teardownNimble();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern int NimbleBridge_Base_getConfiguration();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_Base_restartWithConfiguration(int configuration);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_Base_configurationToName(int configuration);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern int NimbleBridge_Base_configurationFromName(string config);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_Base_getSdkVersion();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_Base_getReleaseVersion();
#endif

	public static void SetupNimble()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_Base_setupNimble();
#endif
	}

	public static void TeardownNimble()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_Base_teardownNimble();
#endif
	}

	public static NimbleBridge_Base.NimbleBridge_Base_NimbleConfiguration GetConfiguration()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return (NimbleBridge_Base.NimbleBridge_Base_NimbleConfiguration)NimbleBridge_Base_getConfiguration();
#else
		return NimbleBridge_Base_NimbleConfiguration.CONFIGURATION_UNKNOWN;
#endif
	}

	public static void RestartWithConfiguration(NimbleBridge_Base.NimbleBridge_Base_NimbleConfiguration configuration)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_Base_restartWithConfiguration((int)configuration);
#endif
	}

	public static string ConfigurationToName(NimbleBridge_Base.NimbleBridge_Base_NimbleConfiguration configuration)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_Base_configurationToName((int)configuration);
#else
		return "Unknown";
#endif
	}

	public static NimbleBridge_Base.NimbleBridge_Base_NimbleConfiguration ConfigurationFromName(string config)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return (NimbleBridge_Base.NimbleBridge_Base_NimbleConfiguration)NimbleBridge_Base_configurationFromName(config);
#else
		return NimbleBridge_Base_NimbleConfiguration.CONFIGURATION_UNKNOWN;
#endif
	}

	public static string GetSdkVersion()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_Base_getSdkVersion();
#else
		return "0.0.0";
#endif
	}

	public static string GetReleaseVersion()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_Base_getReleaseVersion();
#else
		return "0.0.0";
#endif
	}
}
