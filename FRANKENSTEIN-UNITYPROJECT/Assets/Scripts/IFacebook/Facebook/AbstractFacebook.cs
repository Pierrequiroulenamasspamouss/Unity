namespace Facebook
{
	public abstract class AbstractFacebook : global::UnityEngine.MonoBehaviour, global::Facebook.IFacebook
	{
		internal const string AccessTokenKey = "access_token";

		protected bool isInitialized;

		protected bool isLoggedIn;

		protected string userId;

		protected string accessToken;

		protected global::System.DateTime accessTokenExpiresAt;

		protected bool limitEventUsage;

		private global::System.Collections.Generic.List<global::Facebook.FacebookDelegate> authDelegates;

		private int nextAsyncId;

		private global::System.Collections.Generic.Dictionary<string, global::Facebook.FacebookDelegate> facebookDelegates;

		public bool IsInitialized
		{
			get
			{
				return isInitialized;
			}
		}

		public bool IsLoggedIn
		{
			get
			{
				return isLoggedIn;
			}
		}

		public string UserId
		{
			get
			{
				return userId;
			}
			set
			{
				userId = value;
			}
		}

		public virtual string AccessToken
		{
			get
			{
				return accessToken;
			}
			set
			{
				accessToken = value;
			}
		}

		public virtual global::System.DateTime AccessTokenExpiresAt
		{
			get
			{
				return accessTokenExpiresAt;
			}
		}

		public abstract int DialogMode { get; set; }

		public abstract bool LimitEventUsage { get; set; }

		private void Awake()
		{
			global::UnityEngine.Object.DontDestroyOnLoad(this);
			isInitialized = false;
			isLoggedIn = false;
			userId = string.Empty;
			accessToken = string.Empty;
			accessTokenExpiresAt = global::System.DateTime.MinValue;
			authDelegates = new global::System.Collections.Generic.List<global::Facebook.FacebookDelegate>();
			nextAsyncId = 0;
			facebookDelegates = new global::System.Collections.Generic.Dictionary<string, global::Facebook.FacebookDelegate>();
			OnAwake();
		}

		protected abstract void OnAwake();

		public abstract void Init(global::Facebook.InitDelegate onInitComplete, string appId, bool cookie = false, bool logging = true, bool status = true, bool xfbml = false, string channelUrl = "", string authResponse = null, bool frictionlessRequests = false, global::Facebook.HideUnityDelegate hideUnityDelegate = null);

		public abstract void Login(string scope = "", global::Facebook.FacebookDelegate callback = null);

		public abstract void Logout();

		public virtual void GetAuthResponse(global::Facebook.FacebookDelegate callback = null)
		{
			AddAuthDelegate(callback);
		}

		[global::System.Obsolete]
		public virtual void AppRequest(string message, string[] to = null, global::System.Collections.Generic.List<object> filters = null, string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", global::Facebook.FacebookDelegate callback = null)
		{
			AppRequest(message, null, null, to, filters, excludeIds, maxRecipients, data, title, callback);
		}

		public abstract void AppRequest(string message, global::Facebook.OGActionType actionType, string objectId, string[] to = null, global::System.Collections.Generic.List<object> filters = null, string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", global::Facebook.FacebookDelegate callback = null);

		public abstract void FeedRequest(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", global::System.Collections.Generic.Dictionary<string, string[]> properties = null, global::Facebook.FacebookDelegate callback = null);

		public abstract void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, global::Facebook.FacebookDelegate callback = null);

		public abstract void GameGroupCreate(string name, string description, string privacy = "CLOSED", global::Facebook.FacebookDelegate callback = null);

		public abstract void GameGroupJoin(string id, global::Facebook.FacebookDelegate callback = null);

		public virtual void API(string query, global::Facebook.HttpMethod method, global::System.Collections.Generic.Dictionary<string, string> formData = null, global::Facebook.FacebookDelegate callback = null)
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = ((formData == null) ? new global::System.Collections.Generic.Dictionary<string, string>() : CopyByValue(formData));
			if (!dictionary.ContainsKey("access_token") && !query.Contains("access_token="))
			{
				dictionary["access_token"] = AccessToken;
			}
			global::Facebook.AsyncRequestString.Request(GetGraphUrl(query), method, dictionary, callback);
		}

		public virtual void API(string query, global::Facebook.HttpMethod method, global::UnityEngine.WWWForm formData, global::Facebook.FacebookDelegate callback = null)
		{
			if (formData == null)
			{
				formData = new global::UnityEngine.WWWForm();
			}
			formData.AddField("access_token", AccessToken);
			global::Facebook.AsyncRequestString.Request(GetGraphUrl(query), method, formData, callback);
		}

		public abstract void PublishInstall(string appId, global::Facebook.FacebookDelegate callback = null);

		public abstract void ActivateApp(string appId = null);

		public abstract void GetDeepLink(global::Facebook.FacebookDelegate callback);

		public abstract void AppEventsLogEvent(string logEvent, float? valueToSum = null, global::System.Collections.Generic.Dictionary<string, object> parameters = null);

		public abstract void AppEventsLogPurchase(float logPurchase, string currency = "USD", global::System.Collections.Generic.Dictionary<string, object> parameters = null);

		protected void AddAuthDelegate(global::Facebook.FacebookDelegate callback)
		{
			authDelegates.Add(callback);
		}

		protected void OnAuthResponse(FBResult result)
		{
			global::System.Collections.Generic.Dictionary<string, object> dictionary = new global::System.Collections.Generic.Dictionary<string, object>();
			dictionary["is_logged_in"] = IsLoggedIn;
			dictionary["user_id"] = UserId;
			dictionary["access_token"] = AccessToken;
			dictionary["access_token_expires_at"] = AccessTokenExpiresAt;
			FBResult result2 = new FBResult(global::Facebook.MiniJSON.Json.Serialize(dictionary), result.Error);
			foreach (global::Facebook.FacebookDelegate authDelegate in authDelegates)
			{
				if (authDelegate != null)
				{
					authDelegate(result2);
				}
			}
			authDelegates.Clear();
		}

		protected string AddFacebookDelegate(global::Facebook.FacebookDelegate callback)
		{
			nextAsyncId++;
			facebookDelegates.Add(nextAsyncId.ToString(), callback);
			return nextAsyncId.ToString();
		}

		protected void OnFacebookResponse(string uniqueId, FBResult result)
		{
			global::Facebook.FacebookDelegate value;
			if (facebookDelegates.TryGetValue(uniqueId, out value))
			{
				if (value != null)
				{
					value(result);
				}
				facebookDelegates.Remove(uniqueId);
			}
		}

		protected global::System.Collections.Generic.Dictionary<string, string> CopyByValue(global::System.Collections.Generic.Dictionary<string, string> data)
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>(data.Count);
			foreach (global::System.Collections.Generic.KeyValuePair<string, string> datum in data)
			{
				dictionary[datum.Key] = string.Copy(datum.Value);
			}
			return dictionary;
		}

		private string GetGraphUrl(string query)
		{
			if (!query.StartsWith("/"))
			{
				query = "/" + query;
			}
			return global::Facebook.IntegratedPluginCanvasLocation.GraphUrl + query;
		}
	}
}
