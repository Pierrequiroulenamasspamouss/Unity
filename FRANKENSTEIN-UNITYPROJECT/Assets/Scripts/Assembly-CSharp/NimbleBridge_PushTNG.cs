public class NimbleBridge_PushTNG
{
	public delegate void OnRegistrationSuccessDelegate(int statusCode, string registrationToken);

	public delegate void OnConnectionErrorDelegate(int statusCode, string errorMessage);

	public delegate void OnTrackingSuccessDelegate(int statusCode, string trackingData);

	public delegate void OnGetInAppSuccessDelegate(int statusCode, string inAppNotificationData);

	private NimbleBridge_PushTNG()
	{
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_PushTNG_start(string userAlias, double dateOfBirth, bool sandbox, NimbleBridge_PushTNG.OnRegistrationSuccessDelegate registrationSuccessDelegate, NimbleBridge_PushTNG.OnConnectionErrorDelegate connectionErrorDelegate, NimbleBridge_PushTNG.OnTrackingSuccessDelegate trackingSuccessDelegate, NimbleBridge_PushTNG.OnGetInAppSuccessDelegate getInAppSuccessDelegate);
#endif

	public static NimbleBridge_PushTNG GetComponent()
	{
		return new NimbleBridge_PushTNG();
	}

	public void Start(string userAlias, double dateOfBirth, bool sandbox)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_PushTNG_start(userAlias, dateOfBirth, sandbox, null, null, null, null);
#endif
	}

	public void Start(string userAlias, double dateOfBirth, bool sandbox, NimbleBridge_PushTNG.OnRegistrationSuccessDelegate registrationSuccessDelegate, NimbleBridge_PushTNG.OnConnectionErrorDelegate connectionErrorDelegate, NimbleBridge_PushTNG.OnTrackingSuccessDelegate trackingSuccessDelegate, NimbleBridge_PushTNG.OnGetInAppSuccessDelegate getInAppSuccessDelegate)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_PushTNG_start(userAlias, dateOfBirth, sandbox, registrationSuccessDelegate, connectionErrorDelegate, trackingSuccessDelegate, getInAppSuccessDelegate);
#endif
	}
}
