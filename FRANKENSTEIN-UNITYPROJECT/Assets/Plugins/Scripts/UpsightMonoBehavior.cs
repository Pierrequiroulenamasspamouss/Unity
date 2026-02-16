public class UpsightMonoBehavior : global::UnityEngine.MonoBehaviour
{
	public string androidAppToken;

	public string androidAppSecret;

	public string gcmProjectNumber;

	public string iosAppToken;

	public string iosAppSecret;

	public bool registerForPushNotifications;

	private void Start()
	{
		Upsight.init(androidAppToken, androidAppSecret, gcmProjectNumber);
		Upsight.requestAppOpen();
		if (registerForPushNotifications)
		{
			Upsight.registerForPushNotifications();
		}
	}
}
