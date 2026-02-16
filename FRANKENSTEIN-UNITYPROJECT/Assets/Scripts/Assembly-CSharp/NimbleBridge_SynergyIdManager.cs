public class NimbleBridge_SynergyIdManager
{
	public const string NOTIFICATION_SYNERGY_ID_CHANGED = "nimble.synergyidmanager.notification.synergy_id_changed";

	public const string NOTIFICATION_ANONYMOUS_SYNERGY_ID_CHANGED = "nimble.synergyidmanager.notification.anonymous_synergy_id_changed";

	private NimbleBridge_SynergyIdManager()
	{
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_SynergyIdManager_getSynergyId();
#else
	private static string NimbleBridge_SynergyIdManager_getSynergyId() { return ""; }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_SynergyIdManager_getAnonymousSynergyId();
#else
	private static string NimbleBridge_SynergyIdManager_getAnonymousSynergyId() { return ""; }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern NimbleBridge_Error NimbleBridge_SynergyIdManager_login(string userSynergyId, string authenticatorIdentifier);
#else
	private static NimbleBridge_Error NimbleBridge_SynergyIdManager_login(string userSynergyId, string authenticatorIdentifier) { return new NimbleBridge_Error(NimbleBridge_Error.Code.OK, ""); }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern NimbleBridge_Error NimbleBridge_SynergyIdManager_logout(string authenticatorIdentifier);
#else
	private static NimbleBridge_Error NimbleBridge_SynergyIdManager_logout(string authenticatorIdentifier) { return new NimbleBridge_Error(NimbleBridge_Error.Code.OK, ""); }
#endif

	public static NimbleBridge_SynergyIdManager GetComponent()
	{
		return new NimbleBridge_SynergyIdManager();
	}

	public string GetSynergyId()
	{
		return NimbleBridge_SynergyIdManager_getSynergyId();
	}

	public string GetAnonymousSynergyId()
	{
		return NimbleBridge_SynergyIdManager_getAnonymousSynergyId();
	}

	public NimbleBridge_Error Login(string userSynergyId, string authenticatorIdentifier)
	{
		return NimbleBridge_SynergyIdManager_login(userSynergyId, authenticatorIdentifier);
	}

	public NimbleBridge_Error Logout(string authenticatorIdentifier)
	{
		return NimbleBridge_SynergyIdManager_logout(authenticatorIdentifier);
	}
}
