public class Upsight
{
#if UNITY_ANDROID
	private static global::UnityEngine.AndroidJavaObject _plugin;
#else
	private static object _plugin; // Dummy for non-Android platforms
#endif

	static Upsight()
	{
#if UNITY_ANDROID
		if (global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.Android)
		{
			return;
		}
		UpsightManager.noop();
		using (global::UnityEngine.AndroidJavaClass androidJavaClass = new global::UnityEngine.AndroidJavaClass("com.upsight.unity.UpsightPlugin"))
		{
			_plugin = androidJavaClass.CallStatic<global::UnityEngine.AndroidJavaObject>("instance", new object[0]);
		}
#endif
	}

	public static void setLogLevel(UpsightLogLevel logLevel)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("setLogLevel", logLevel.ToString());
		}
	}

	public static string getPluginVersion()
	{
		if (global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.Android)
		{
			return "UnityEditor";
		}
		return _plugin.Call<string>("getPluginVersion", new object[0]);
	}

	public static void init(string appToken, string appSecret, string gcmProjectNumber = null)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("init", appToken, appSecret, gcmProjectNumber);
		}
	}

	public static void requestAppOpen()
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("requestAppOpen");
		}
	}

	public static void sendContentRequest(string placementID, bool showsOverlayImmediately, bool shouldAnimate = true, global::System.Collections.Generic.Dictionary<string, object> dimensions = null)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			global::UnityEngine.AndroidJavaObject androidJavaObject = dictionaryToJavaHashMap(dimensions);
			_plugin.Call("sendContentRequest", placementID, showsOverlayImmediately, shouldAnimate, androidJavaObject);
		}
	}

#if UNITY_ANDROID
	public static global::UnityEngine.AndroidJavaObject dictionaryToJavaHashMap(global::System.Collections.Generic.Dictionary<string, object> dictionary)
	{
		global::UnityEngine.AndroidJavaObject result = null;
		if (dictionary != null)
		{
			global::UnityEngine.AndroidJavaClass androidJavaClass = new global::UnityEngine.AndroidJavaClass("net.minidev.json.parser.JSONParser");
			int num = androidJavaClass.GetStatic<int>("MODE_JSON_SIMPLE");
			string text = global::MiniJSON.Json.Serialize(dictionary);
			global::UnityEngine.AndroidJavaObject androidJavaObject = new global::UnityEngine.AndroidJavaObject("net.minidev.json.parser.JSONParser", num);
			result = androidJavaObject.Call<global::UnityEngine.AndroidJavaObject>("parse", new object[1] { text });
		}
		return result;
	}
#endif

	public static void preloadContentRequest(string placementID, global::System.Collections.Generic.Dictionary<string, object> dimensions = null)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			global::UnityEngine.AndroidJavaObject androidJavaObject = dictionaryToJavaHashMap(dimensions);
			_plugin.Call("preloadContentRequest", placementID, androidJavaObject);
		}
	}

	public static void getContentBadgeNumber(string placementID)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("sendMetadataRequest", placementID);
		}
	}

	public static bool getOptOutStatus()
	{
		if (global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.Android)
		{
			return false;
		}
		return _plugin.Call<bool>("getOptOutStatus", new object[0]);
	}

	public static void setOptOutStatus(bool optOutStatus)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("setOptOutStatus", optOutStatus);
		}
	}

	public static void trackInAppPurchase(string sku, int quantity, UpsightAndroidPurchaseResolution resolutionType, double price, string orderId, string store)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("trackInAppPurchase", sku, quantity, (int)resolutionType, price, orderId, store);
		}
	}

	public static void reportCustomEvent(global::System.Collections.Generic.Dictionary<string, object> properties)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("reportCustomEvent", global::MiniJSON.Json.Serialize(properties));
		}
	}

	public static void registerForPushNotifications()
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("registerForPushNotifications");
		}
	}

	public static void deregisterForPushNotifications()
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("deregisterForPushNotifications");
		}
	}

	public static void setShouldOpenContentRequestsFromPushNotifications(bool shouldOpen)
	{
	}

	public static void setShouldOpenUrlsFromPushNotifications(bool shouldOpen)
	{
	}
}
