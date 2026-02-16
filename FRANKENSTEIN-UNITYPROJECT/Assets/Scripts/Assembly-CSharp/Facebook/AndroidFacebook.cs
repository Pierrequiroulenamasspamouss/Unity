namespace Facebook
{
	internal sealed class AndroidFacebook : global::Facebook.AbstractFacebook, global::Facebook.IFacebook
	{
		public const int BrowserDialogMode = 0;

		private const string AndroidJavaFacebookClass = "com.facebook.unity.FB";

		private const string CallbackIdKey = "callback_id";

		private string keyHash;

		private global::Facebook.FacebookDelegate deepLinkDelegate;

		private global::UnityEngine.AndroidJavaClass fbJava;

		private global::Facebook.InitDelegate onInitComplete;

		public string KeyHash
		{
			get
			{
				return keyHash;
			}
		}

		public override int DialogMode
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public override bool LimitEventUsage
		{
			get
			{
				return limitEventUsage;
			}
			set
			{
				limitEventUsage = value;
				CallFB("SetLimitEventUsage", value.ToString());
			}
		}

		private global::UnityEngine.AndroidJavaClass FB
		{
			get
			{
				if (fbJava == null)
				{
					fbJava = new global::UnityEngine.AndroidJavaClass("com.facebook.unity.FB");
					if (fbJava == null)
					{
						throw new global::UnityEngine.MissingReferenceException(string.Format("AndroidFacebook failed to load {0} class", "com.facebook.unity.FB"));
					}
				}
				return fbJava;
			}
		}

		private void CallFB(string method, string args)
		{
			FB.CallStatic(method, args);
		}

		protected override void OnAwake()
		{
			keyHash = string.Empty;
		}

		private bool IsErrorResponse(string response)
		{
			return false;
		}

		public override void Init(global::Facebook.InitDelegate onInitComplete, string appId, bool cookie = false, bool logging = true, bool status = true, bool xfbml = false, string channelUrl = "", string authResponse = null, bool frictionlessRequests = false, global::Facebook.HideUnityDelegate hideUnityDelegate = null)
		{
			if (string.IsNullOrEmpty(appId))
			{
				throw new global::System.ArgumentException("appId cannot be null or empty!");
			}
			global::System.Collections.Generic.Dictionary<string, object> dictionary = new global::System.Collections.Generic.Dictionary<string, object>();
			dictionary.Add("appId", appId);
			if (cookie)
			{
				dictionary.Add("cookie", true);
			}
			if (!logging)
			{
				dictionary.Add("logging", false);
			}
			if (!status)
			{
				dictionary.Add("status", false);
			}
			if (xfbml)
			{
				dictionary.Add("xfbml", true);
			}
			if (!string.IsNullOrEmpty(channelUrl))
			{
				dictionary.Add("channelUrl", channelUrl);
			}
			if (!string.IsNullOrEmpty(authResponse))
			{
				dictionary.Add("authResponse", authResponse);
			}
			if (frictionlessRequests)
			{
				dictionary.Add("frictionlessRequests", true);
			}
			string text = global::Facebook.MiniJSON.Json.Serialize(dictionary);
			this.onInitComplete = onInitComplete;
			CallFB("Init", text.ToString());
		}

		public void OnInitComplete(string message)
		{
			isInitialized = true;
			OnLoginComplete(message);
			if (onInitComplete != null)
			{
				onInitComplete();
			}
		}

		public override void Login(string scope = "", global::Facebook.FacebookDelegate callback = null)
		{
			global::System.Collections.Generic.Dictionary<string, object> dictionary = new global::System.Collections.Generic.Dictionary<string, object>();
			dictionary.Add("scope", scope);
			string args = global::Facebook.MiniJSON.Json.Serialize(dictionary);
			AddAuthDelegate(callback);
			CallFB("Login", args);
		}

		public void OnLoginComplete(string message)
		{
			global::System.Collections.Generic.Dictionary<string, object> dictionary = (global::System.Collections.Generic.Dictionary<string, object>)global::Facebook.MiniJSON.Json.Deserialize(message);
			if (dictionary.ContainsKey("user_id"))
			{
				isLoggedIn = true;
				userId = (string)dictionary["user_id"];
				accessToken = (string)dictionary["access_token"];
				accessTokenExpiresAt = FromTimestamp(int.Parse((string)dictionary["expiration_timestamp"]));
			}
			if (dictionary.ContainsKey("key_hash"))
			{
				keyHash = (string)dictionary["key_hash"];
			}
			OnAuthResponse(new FBResult(message));
		}

		public void OnGroupCreateComplete(string message)
		{
			global::System.Collections.Generic.Dictionary<string, object> dictionary = (global::System.Collections.Generic.Dictionary<string, object>)global::Facebook.MiniJSON.Json.Deserialize(message);
			string uniqueId = (string)dictionary["callback_id"];
			dictionary.Remove("callback_id");
			OnFacebookResponse(uniqueId, new FBResult(global::Facebook.MiniJSON.Json.Serialize(dictionary)));
		}

		public void OnAccessTokenRefresh(string message)
		{
			global::System.Collections.Generic.Dictionary<string, object> dictionary = (global::System.Collections.Generic.Dictionary<string, object>)global::Facebook.MiniJSON.Json.Deserialize(message);
			if (dictionary.ContainsKey("access_token"))
			{
				accessToken = (string)dictionary["access_token"];
				accessTokenExpiresAt = FromTimestamp(int.Parse((string)dictionary["expiration_timestamp"]));
			}
		}

		public override void Logout()
		{
			CallFB("Logout", string.Empty);
		}

		public void OnLogoutComplete(string message)
		{
			isLoggedIn = false;
			userId = string.Empty;
			accessToken = string.Empty;
		}

		public override void AppRequest(string message, global::Facebook.OGActionType actionType, string objectId, string[] to = null, global::System.Collections.Generic.List<object> filters = null, string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", global::Facebook.FacebookDelegate callback = null)
		{
			if (string.IsNullOrEmpty(message))
			{
				throw new global::System.ArgumentNullException("message", "message cannot be null or empty!");
			}
			if (actionType != null && string.IsNullOrEmpty(objectId))
			{
				throw new global::System.ArgumentNullException("objectId", "You cannot provide an actionType without an objectId");
			}
			if (actionType == null && !string.IsNullOrEmpty(objectId))
			{
				throw new global::System.ArgumentNullException("actionType", "You cannot provide an objectId without an actionType");
			}
			global::System.Collections.Generic.Dictionary<string, object> dictionary = new global::System.Collections.Generic.Dictionary<string, object>();
			dictionary["message"] = message;
			if (callback != null)
			{
				dictionary["callback_id"] = AddFacebookDelegate(callback);
			}
			if (actionType != null && !string.IsNullOrEmpty(objectId))
			{
				dictionary["action_type"] = actionType.ToString();
				dictionary["object_id"] = objectId;
			}
			if (to != null)
			{
				dictionary["to"] = string.Join(",", to);
			}
			if (filters != null && filters.Count > 0)
			{
				string text = filters[0] as string;
				if (text != null)
				{
					dictionary["filters"] = text;
				}
			}
			if (maxRecipients.HasValue)
			{
				dictionary["max_recipients"] = maxRecipients.Value;
			}
			if (!string.IsNullOrEmpty(data))
			{
				dictionary["data"] = data;
			}
			if (!string.IsNullOrEmpty(title))
			{
				dictionary["title"] = title;
			}
			CallFB("AppRequest", global::Facebook.MiniJSON.Json.Serialize(dictionary));
		}

		public void OnAppRequestsComplete(string message)
		{
			global::System.Collections.Generic.Dictionary<string, object> dictionary = (global::System.Collections.Generic.Dictionary<string, object>)global::Facebook.MiniJSON.Json.Deserialize(message);
			if (!dictionary.ContainsKey("callback_id"))
			{
				return;
			}
			global::System.Collections.Generic.Dictionary<string, object> dictionary2 = new global::System.Collections.Generic.Dictionary<string, object>();
			string uniqueId = (string)dictionary["callback_id"];
			dictionary.Remove("callback_id");
			if (dictionary.Count > 0)
			{
				global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>(dictionary.Count - 1);
				foreach (string key in dictionary.Keys)
				{
					if (!key.StartsWith("to"))
					{
						dictionary2[key] = dictionary[key];
					}
					else
					{
						list.Add((string)dictionary[key]);
					}
				}
				dictionary2.Add("to", list);
				dictionary.Clear();
				OnFacebookResponse(uniqueId, new FBResult(global::Facebook.MiniJSON.Json.Serialize(dictionary2)));
			}
			else
			{
				OnFacebookResponse(uniqueId, new FBResult(global::Facebook.MiniJSON.Json.Serialize(dictionary2), "Malformed request response.  Please file a bug with facebook here: https://developers.facebook.com/bugs/create"));
			}
		}

		public override void FeedRequest(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", global::System.Collections.Generic.Dictionary<string, string[]> properties = null, global::Facebook.FacebookDelegate callback = null)
		{
			global::System.Collections.Generic.Dictionary<string, object> dictionary = new global::System.Collections.Generic.Dictionary<string, object>();
			if (callback != null)
			{
				dictionary["callback_id"] = AddFacebookDelegate(callback);
			}
			if (!string.IsNullOrEmpty(toId))
			{
				dictionary.Add("to", toId);
			}
			if (!string.IsNullOrEmpty(link))
			{
				dictionary.Add("link", link);
			}
			if (!string.IsNullOrEmpty(linkName))
			{
				dictionary.Add("name", linkName);
			}
			if (!string.IsNullOrEmpty(linkCaption))
			{
				dictionary.Add("caption", linkCaption);
			}
			if (!string.IsNullOrEmpty(linkDescription))
			{
				dictionary.Add("description", linkDescription);
			}
			if (!string.IsNullOrEmpty(picture))
			{
				dictionary.Add("picture", picture);
			}
			if (!string.IsNullOrEmpty(mediaSource))
			{
				dictionary.Add("source", mediaSource);
			}
			if (!string.IsNullOrEmpty(actionName) && !string.IsNullOrEmpty(actionLink))
			{
				global::System.Collections.Generic.Dictionary<string, object> dictionary2 = new global::System.Collections.Generic.Dictionary<string, object>();
				dictionary2.Add("name", actionName);
				dictionary2.Add("link", actionLink);
				dictionary.Add("actions", new global::System.Collections.Generic.Dictionary<string, object>[1] { dictionary2 });
			}
			if (!string.IsNullOrEmpty(reference))
			{
				dictionary.Add("ref", reference);
			}
			if (properties != null)
			{
				global::System.Collections.Generic.Dictionary<string, object> dictionary3 = new global::System.Collections.Generic.Dictionary<string, object>();
				foreach (global::System.Collections.Generic.KeyValuePair<string, string[]> property in properties)
				{
					if (property.Value.Length >= 1)
					{
						if (property.Value.Length == 1)
						{
							dictionary3.Add(property.Key, property.Value[0]);
							continue;
						}
						global::System.Collections.Generic.Dictionary<string, object> dictionary4 = new global::System.Collections.Generic.Dictionary<string, object>();
						dictionary4.Add("text", property.Value[0]);
						dictionary4.Add("href", property.Value[1]);
						dictionary3.Add(property.Key, dictionary4);
					}
				}
				dictionary.Add("properties", dictionary3);
			}
			CallFB("FeedRequest", global::Facebook.MiniJSON.Json.Serialize(dictionary));
		}

		public void OnFeedRequestComplete(string message)
		{
			global::System.Collections.Generic.Dictionary<string, object> dictionary = (global::System.Collections.Generic.Dictionary<string, object>)global::Facebook.MiniJSON.Json.Deserialize(message);
			if (!dictionary.ContainsKey("callback_id"))
			{
				return;
			}
			global::System.Collections.Generic.Dictionary<string, object> dictionary2 = new global::System.Collections.Generic.Dictionary<string, object>();
			string uniqueId = (string)dictionary["callback_id"];
			dictionary.Remove("callback_id");
			if (dictionary.Count > 0)
			{
				foreach (string key in dictionary.Keys)
				{
					dictionary2[key] = dictionary[key];
				}
				dictionary.Clear();
				OnFacebookResponse(uniqueId, new FBResult(global::Facebook.MiniJSON.Json.Serialize(dictionary2)));
			}
			else
			{
				OnFacebookResponse(uniqueId, new FBResult(global::Facebook.MiniJSON.Json.Serialize(dictionary2), "Malformed request response.  Please file a bug with facebook here: https://developers.facebook.com/bugs/create"));
			}
		}

		public override void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, global::Facebook.FacebookDelegate callback = null)
		{
			throw new global::System.PlatformNotSupportedException("There is no Facebook Pay Dialog on Android");
		}

		public override void GameGroupCreate(string name, string description, string privacy = "CLOSED", global::Facebook.FacebookDelegate callback = null)
		{
			global::System.Collections.Generic.Dictionary<string, object> dictionary = new global::System.Collections.Generic.Dictionary<string, object>();
			dictionary["name"] = name;
			dictionary["description"] = description;
			dictionary["privacy"] = privacy;
			if (callback != null)
			{
				dictionary["callback_id"] = AddFacebookDelegate(callback);
			}
			CallFB("GameGroupCreate", global::Facebook.MiniJSON.Json.Serialize(dictionary));
		}

		public override void GameGroupJoin(string id, global::Facebook.FacebookDelegate callback = null)
		{
			global::System.Collections.Generic.Dictionary<string, object> dictionary = new global::System.Collections.Generic.Dictionary<string, object>();
			dictionary["id"] = id;
			if (callback != null)
			{
				dictionary["callback_id"] = AddFacebookDelegate(callback);
			}
			CallFB("GameGroupJoin", global::Facebook.MiniJSON.Json.Serialize(dictionary));
		}

		public override void GetDeepLink(global::Facebook.FacebookDelegate callback)
		{
			if (callback != null)
			{
				deepLinkDelegate = callback;
				CallFB("GetDeepLink", string.Empty);
			}
		}

		public void OnGetDeepLinkComplete(string message)
		{
			global::System.Collections.Generic.Dictionary<string, object> dictionary = (global::System.Collections.Generic.Dictionary<string, object>)global::Facebook.MiniJSON.Json.Deserialize(message);
			if (deepLinkDelegate != null)
			{
				object value = string.Empty;
				dictionary.TryGetValue("deep_link", out value);
				deepLinkDelegate(new FBResult(value.ToString()));
			}
		}

		public override void AppEventsLogEvent(string logEvent, float? valueToSum = null, global::System.Collections.Generic.Dictionary<string, object> parameters = null)
		{
			global::System.Collections.Generic.Dictionary<string, object> dictionary = new global::System.Collections.Generic.Dictionary<string, object>();
			dictionary["logEvent"] = logEvent;
			if (valueToSum.HasValue)
			{
				dictionary["valueToSum"] = valueToSum.Value;
			}
			if (parameters != null)
			{
				dictionary["parameters"] = ToStringDict(parameters);
			}
			CallFB("AppEvents", global::Facebook.MiniJSON.Json.Serialize(dictionary));
		}

		public override void AppEventsLogPurchase(float logPurchase, string currency = "USD", global::System.Collections.Generic.Dictionary<string, object> parameters = null)
		{
			global::System.Collections.Generic.Dictionary<string, object> dictionary = new global::System.Collections.Generic.Dictionary<string, object>();
			dictionary["logPurchase"] = logPurchase;
			dictionary["currency"] = (string.IsNullOrEmpty(currency) ? "USD" : currency);
			if (parameters != null)
			{
				dictionary["parameters"] = ToStringDict(parameters);
			}
			CallFB("AppEvents", global::Facebook.MiniJSON.Json.Serialize(dictionary));
		}

		public override void PublishInstall(string appId, global::Facebook.FacebookDelegate callback = null)
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>(2);
			dictionary["app_id"] = appId;
			if (callback != null)
			{
				dictionary["callback_id"] = AddFacebookDelegate(callback);
			}
			CallFB("PublishInstall", global::Facebook.MiniJSON.Json.Serialize(dictionary));
		}

		public void OnPublishInstallComplete(string message)
		{
			global::System.Collections.Generic.Dictionary<string, object> dictionary = (global::System.Collections.Generic.Dictionary<string, object>)global::Facebook.MiniJSON.Json.Deserialize(message);
			if (dictionary.ContainsKey("callback_id"))
			{
				OnFacebookResponse((string)dictionary["callback_id"], new FBResult(string.Empty));
			}
		}

		public override void ActivateApp(string appId = null)
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>(1);
			if (!string.IsNullOrEmpty(appId))
			{
				dictionary["app_id"] = appId;
			}
			CallFB("ActivateApp", global::Facebook.MiniJSON.Json.Serialize(dictionary));
		}

		private global::System.Collections.Generic.Dictionary<string, string> ToStringDict(global::System.Collections.Generic.Dictionary<string, object> dict)
		{
			if (dict == null)
			{
				return null;
			}
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			foreach (global::System.Collections.Generic.KeyValuePair<string, object> item in dict)
			{
				dictionary[item.Key] = item.Value.ToString();
			}
			return dictionary;
		}

		private global::System.DateTime FromTimestamp(int timestamp)
		{
			return new global::System.DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(timestamp);
		}
	}
}
