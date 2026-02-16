public sealed class FB : global::UnityEngine.ScriptableObject
{
	public sealed class AppEvents
	{
		public static bool LimitEventUsage
		{
			get
			{
				return facebook != null && facebook.LimitEventUsage;
			}
			set
			{
				facebook.LimitEventUsage = value;
			}
		}

		public static void LogEvent(string logEvent, float? valueToSum = null, global::System.Collections.Generic.Dictionary<string, object> parameters = null)
		{
			FacebookImpl.AppEventsLogEvent(logEvent, valueToSum, parameters);
		}

		public static void LogPurchase(float logPurchase, string currency = "USD", global::System.Collections.Generic.Dictionary<string, object> parameters = null)
		{
			FacebookImpl.AppEventsLogPurchase(logPurchase, currency, parameters);
		}
	}

	public sealed class Canvas
	{
		public static void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, global::Facebook.FacebookDelegate callback = null)
		{
			FacebookImpl.Pay(product, action, quantity, quantityMin, quantityMax, requestId, pricepointId, testCurrency, callback);
		}

		public static void SetResolution(int width, int height, bool fullscreen, int preferredRefreshRate = 0, params FBScreen.Layout[] layoutParams)
		{
			FBScreen.SetResolution(width, height, fullscreen, preferredRefreshRate, layoutParams);
		}

		public static void SetAspectRatio(int width, int height, params FBScreen.Layout[] layoutParams)
		{
			FBScreen.SetAspectRatio(width, height, layoutParams);
		}
	}

	public sealed class Android
	{
		public static string KeyHash
		{
			get
			{
				global::Facebook.AndroidFacebook androidFacebook = facebook as global::Facebook.AndroidFacebook;
				return (!(androidFacebook != null)) ? string.Empty : androidFacebook.KeyHash;
			}
		}
	}

	public abstract class RemoteFacebookLoader : global::UnityEngine.MonoBehaviour
	{
		public delegate void LoadedDllCallback(global::Facebook.IFacebook fb);

		private const string facebookNamespace = "Facebook.";

		private const int maxRetryLoadCount = 3;

		private static int retryLoadCount;

		protected abstract string className { get; }

		public static global::System.Collections.IEnumerator LoadFacebookClass(string className, FB.RemoteFacebookLoader.LoadedDllCallback callback)
		{
			string url = string.Format(global::Facebook.IntegratedPluginCanvasLocation.DllUrl, className);
			global::UnityEngine.WWW www = new global::UnityEngine.WWW(url);
			FbDebug.Log("loading dll: " + url);
			yield return www;
			if (www.error != null)
			{
				FbDebug.Error(www.error);
				if (retryLoadCount < 3)
				{
					retryLoadCount++;
				}
				www.Dispose();
				yield break;
			}
			global::UnityEngine.WWW authTokenWww = new global::UnityEngine.WWW(global::Facebook.IntegratedPluginCanvasLocation.KeyUrl);
			yield return authTokenWww;
			if (authTokenWww.error != null)
			{
				FbDebug.Error("Cannot load from " + global::Facebook.IntegratedPluginCanvasLocation.KeyUrl + ": " + authTokenWww.error);
				authTokenWww.Dispose();
				yield break;
			}
			global::System.Reflection.Assembly assembly = global::UnityEngine.Security.LoadAndVerifyAssembly(www.bytes, authTokenWww.text);
			if (assembly == null)
			{
				FbDebug.Error("Could not securely load assembly from " + url);
				www.Dispose();
				yield break;
			}
			global::System.Type facebookClass = assembly.GetType("Facebook." + className);
			if (facebookClass == null)
			{
				FbDebug.Error(className + " not found in assembly!");
				www.Dispose();
				yield break;
			}
			global::Facebook.IFacebook fb = typeof(global::Facebook.FBComponentFactory).GetMethod("GetComponent").MakeGenericMethod(facebookClass).Invoke(null, new object[1] { global::Facebook.IfNotExist.AddNew }) as global::Facebook.IFacebook;
			if (fb == null)
			{
				FbDebug.Error(className + " couldn't be created.");
				www.Dispose();
			}
			else
			{
				callback(fb);
				www.Dispose();
			}
		}

		private global::System.Collections.IEnumerator Start()
		{
			global::System.Collections.IEnumerator loader = LoadFacebookClass(className, OnDllLoaded);
			while (loader.MoveNext())
			{
				yield return loader.Current;
			}
			global::UnityEngine.Object.Destroy(this);
		}

		private void OnDllLoaded(global::Facebook.IFacebook fb)
		{
			facebook = fb;
			FB.OnDllLoaded();
		}
	}

	public abstract class CompiledFacebookLoader : global::UnityEngine.MonoBehaviour
	{
		protected abstract global::Facebook.IFacebook fb { get; }

		private void Start()
		{
			facebook = fb;
			OnDllLoaded();
			global::UnityEngine.Object.Destroy(this);
		}
	}

	public static global::Facebook.InitDelegate OnInitComplete;

	public static global::Facebook.HideUnityDelegate OnHideUnity;

	private static global::Facebook.IFacebook facebook;

	private static string authResponse;

	private static bool isInitCalled;

	private static string appId;

	private static bool cookie;

	private static bool logging;

	private static bool status;

	private static bool xfbml;

	private static bool frictionlessRequests;

	private static global::Facebook.IFacebook FacebookImpl
	{
		get
		{
			if (facebook == null)
			{
				throw new global::System.NullReferenceException("Facebook object is not yet loaded.  Did you call FB.Init()?");
			}
			return facebook;
		}
	}

	public static string AppId
	{
		get
		{
			return appId;
		}
	}

	public static string UserId
	{
		get
		{
			return (facebook == null) ? string.Empty : facebook.UserId;
		}
	}

	public static string AccessToken
	{
		get
		{
			return (facebook == null) ? string.Empty : facebook.AccessToken;
		}
	}

	public static global::System.DateTime AccessTokenExpiresAt
	{
		get
		{
			return (facebook == null) ? global::System.DateTime.MinValue : facebook.AccessTokenExpiresAt;
		}
	}

	public static bool IsLoggedIn
	{
		get
		{
			return facebook != null && facebook.IsLoggedIn;
		}
	}

	public static bool IsInitialized
	{
		get
		{
			return facebook != null && facebook.IsInitialized;
		}
	}

	public static void Init(global::Facebook.InitDelegate onInitComplete, global::Facebook.HideUnityDelegate onHideUnity = null, string authResponse = null)
	{
		Init(onInitComplete, FBSettings.AppId, FBSettings.Cookie, FBSettings.Logging, FBSettings.Status, FBSettings.Xfbml, FBSettings.FrictionlessRequests, onHideUnity, authResponse);
	}

	public static void Init(global::Facebook.InitDelegate onInitComplete, string appId, bool cookie = true, bool logging = true, bool status = true, bool xfbml = false, bool frictionlessRequests = true, global::Facebook.HideUnityDelegate onHideUnity = null, string authResponse = null)
	{
		FB.appId = appId;
		FB.cookie = cookie;
		FB.logging = logging;
		FB.status = status;
		FB.xfbml = xfbml;
		FB.frictionlessRequests = frictionlessRequests;
		FB.authResponse = authResponse;
		OnInitComplete = onInitComplete;
		OnHideUnity = onHideUnity;
		if (!isInitCalled)
		{
			global::Facebook.FBBuildVersionAttribute versionAttributeOfType = global::Facebook.FBBuildVersionAttribute.GetVersionAttributeOfType(typeof(global::Facebook.IFacebook));
			if (versionAttributeOfType == null)
			{
				FbDebug.Warn("Cannot find Facebook SDK Version");
			}
			else
			{
				FbDebug.Info(string.Format("Using SDK {0}, Build {1}", versionAttributeOfType.SdkVersion, versionAttributeOfType.BuildVersion));
			}
			global::Facebook.FBComponentFactory.GetComponent<global::Facebook.AndroidFacebookLoader>();
			isInitCalled = true;
		}
		else
		{
			FbDebug.Warn("FB.Init() has already been called.  You only need to call this once and only once.");
			if (FacebookImpl != null)
			{
				OnDllLoaded();
			}
		}
	}

	private static void OnDllLoaded()
	{
		global::Facebook.FBBuildVersionAttribute versionAttributeOfType = global::Facebook.FBBuildVersionAttribute.GetVersionAttributeOfType(FacebookImpl.GetType());
		if (versionAttributeOfType != null)
		{
			FbDebug.Log(string.Format("Finished loading Facebook dll. Version {0} Build {1}", versionAttributeOfType.SdkVersion, versionAttributeOfType.BuildVersion));
		}
		FacebookImpl.Init(OnInitComplete, appId, cookie, logging, status, xfbml, FBSettings.ChannelUrl, authResponse, frictionlessRequests, OnHideUnity);
	}

	public static void Login(string scope = "", global::Facebook.FacebookDelegate callback = null)
	{
		FacebookImpl.Login(scope, callback);
	}

	public static void Logout()
	{
		FacebookImpl.Logout();
	}

	public static void AppRequest(string message, global::Facebook.OGActionType actionType, string objectId, string[] to, string data = "", string title = "", global::Facebook.FacebookDelegate callback = null)
	{
		FacebookImpl.AppRequest(message, actionType, objectId, to, null, null, null, data, title, callback);
	}

	public static void AppRequest(string message, global::Facebook.OGActionType actionType, string objectId, global::System.Collections.Generic.List<object> filters = null, string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", global::Facebook.FacebookDelegate callback = null)
	{
		FacebookImpl.AppRequest(message, actionType, objectId, null, filters, excludeIds, maxRecipients, data, title, callback);
	}

	public static void AppRequest(string message, string[] to = null, global::System.Collections.Generic.List<object> filters = null, string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", global::Facebook.FacebookDelegate callback = null)
	{
		FacebookImpl.AppRequest(message, null, null, to, filters, excludeIds, maxRecipients, data, title, callback);
	}

	public static void Feed(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", global::System.Collections.Generic.Dictionary<string, string[]> properties = null, global::Facebook.FacebookDelegate callback = null)
	{
		FacebookImpl.FeedRequest(toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference, properties, callback);
	}

	public static void API(string query, global::Facebook.HttpMethod method, global::Facebook.FacebookDelegate callback = null, global::System.Collections.Generic.Dictionary<string, string> formData = null)
	{
		FacebookImpl.API(query, method, formData, callback);
	}

	public static void API(string query, global::Facebook.HttpMethod method, global::Facebook.FacebookDelegate callback, global::UnityEngine.WWWForm formData)
	{
		FacebookImpl.API(query, method, formData, callback);
	}

	[global::System.Obsolete("use FB.ActivateApp()")]
	public static void PublishInstall(global::Facebook.FacebookDelegate callback = null)
	{
		FacebookImpl.PublishInstall(AppId, callback);
	}

	public static void ActivateApp()
	{
		FacebookImpl.ActivateApp(AppId);
	}

	public static void GetDeepLink(global::Facebook.FacebookDelegate callback)
	{
		FacebookImpl.GetDeepLink(callback);
	}

	public static void GameGroupCreate(string name, string description, string privacy = "CLOSED", global::Facebook.FacebookDelegate callback = null)
	{
		FacebookImpl.GameGroupCreate(name, description, privacy, callback);
	}

	public static void GameGroupJoin(string id, global::Facebook.FacebookDelegate callback = null)
	{
		FacebookImpl.GameGroupJoin(id, callback);
	}
}
