namespace DeltaDNA
{
	public sealed class SDK : global::DeltaDNA.Singleton<global::DeltaDNA.SDK>
	{
		private static readonly string PF_KEY_USER_ID = "DDSDK_USER_ID";

		private static readonly string PF_KEY_FIRST_RUN = "DDSDK_FIRST_RUN";

		private static readonly string PF_KEY_HASH_SECRET = "DDSDK_HASH_SECRET";

		private static readonly string PF_KEY_CLIENT_VERSION = "DDSDK_CLIENT_VERSION";

		private static readonly string PF_KEY_PUSH_NOTIFICATION_TOKEN = "DDSDK_PUSH_NOTIFICATION_TOKEN";

		private static readonly string PF_KEY_ANDROID_REGISTRATION_ID = "DDSDK_ANDROID_REGISTRATION_ID";

		private static readonly string EV_KEY_NAME = "eventName";

		private static readonly string EV_KEY_USER_ID = "userID";

		private static readonly string EV_KEY_SESSION_ID = "sessionID";

		private static readonly string EV_KEY_TIMESTAMP = "eventTimestamp";

		private static readonly string EV_KEY_PARAMS = "eventParams";

		private static readonly string EP_KEY_PLATFORM = "platform";

		private static readonly string EP_KEY_SDK_VERSION = "sdkVersion";

		public static readonly string AUTO_GENERATED_USER_ID = null;

		private bool initialised;

		private string collectURL;

		private string engageURL;

		private global::DeltaDNA.EventStore eventStore;

		private global::DeltaDNA.EngageArchive engageArchive;

		private static global::System.Func<global::System.DateTime?> TimestampFunc = DefaultTimestampFunc;

		private static object _lock = new object();

		public global::DeltaDNA.Settings Settings { get; set; }

		public global::DeltaDNA.TransactionBuilder Transaction { get; private set; }

		public string EnvironmentKey { get; private set; }

		public string CollectURL
		{
			get
			{
				return collectURL;
			}
			private set
			{
				collectURL = ValidateURL(value);
			}
		}

		public string EngageURL
		{
			get
			{
				return engageURL;
			}
			private set
			{
				engageURL = ValidateURL(value);
			}
		}

		public string SessionID { get; private set; }

		public string Platform { get; private set; }

		public string UserID
		{
			get
			{
				string text = global::UnityEngine.PlayerPrefs.GetString(PF_KEY_USER_ID, null);
				if (string.IsNullOrEmpty(text))
				{
					global::DeltaDNA.Logger.LogDebug("No existing User ID found.");
					return null;
				}
				return text;
			}
			private set
			{
				if (!string.IsNullOrEmpty(value))
				{
					global::UnityEngine.PlayerPrefs.SetString(PF_KEY_USER_ID, value);
					global::UnityEngine.PlayerPrefs.Save();
				}
			}
		}

		public bool IsInitialised
		{
			get
			{
				return initialised;
			}
		}

		public bool IsUploading { get; private set; }

		public string HashSecret
		{
			get
			{
				string text = global::UnityEngine.PlayerPrefs.GetString(PF_KEY_HASH_SECRET, null);
				if (string.IsNullOrEmpty(text))
				{
					global::DeltaDNA.Logger.LogDebug("Event hashing not enabled.");
					return null;
				}
				return text;
			}
			set
			{
				global::DeltaDNA.Logger.LogDebug("Setting Hash Secret '" + value + "'");
				global::UnityEngine.PlayerPrefs.SetString(PF_KEY_HASH_SECRET, value);
				global::UnityEngine.PlayerPrefs.Save();
			}
		}

		public string ClientVersion
		{
			get
			{
				string text = global::UnityEngine.PlayerPrefs.GetString(PF_KEY_CLIENT_VERSION, null);
				if (string.IsNullOrEmpty(text))
				{
					global::DeltaDNA.Logger.LogWarning("No client game version set.");
					return null;
				}
				return text;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					global::DeltaDNA.Logger.LogDebug("Setting ClientVersion '" + value + "'");
					global::UnityEngine.PlayerPrefs.SetString(PF_KEY_CLIENT_VERSION, value);
					global::UnityEngine.PlayerPrefs.Save();
				}
			}
		}

		public string PushNotificationToken
		{
			get
			{
				string text = global::UnityEngine.PlayerPrefs.GetString(PF_KEY_PUSH_NOTIFICATION_TOKEN, null);
				if (string.IsNullOrEmpty(text))
				{
					if (global::DeltaDNA.ClientInfo.Platform.Contains("IOS"))
					{
						global::DeltaDNA.Logger.LogWarning("No Apple push notification token set, sending push notifications to iOS devices will be unavailable.");
					}
					return null;
				}
				return text;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					global::UnityEngine.PlayerPrefs.SetString(PF_KEY_PUSH_NOTIFICATION_TOKEN, value);
					global::UnityEngine.PlayerPrefs.Save();
				}
			}
		}

		public string AndroidRegistrationID
		{
			get
			{
				string text = global::UnityEngine.PlayerPrefs.GetString(PF_KEY_ANDROID_REGISTRATION_ID, null);
				if (string.IsNullOrEmpty(text))
				{
					if (global::DeltaDNA.ClientInfo.Platform.Contains("ANDROID"))
					{
						global::DeltaDNA.Logger.LogWarning("No Android registration id set, sending push notifications to Android devices will be unavailable.");
					}
					return null;
				}
				return text;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					global::UnityEngine.PlayerPrefs.SetString(PF_KEY_ANDROID_REGISTRATION_ID, value);
					global::UnityEngine.PlayerPrefs.Save();
				}
			}
		}

		private SDK()
		{
			Settings = new global::DeltaDNA.Settings();
			Transaction = new global::DeltaDNA.TransactionBuilder(this);
			eventStore = new global::DeltaDNA.EventStore(global::DeltaDNA.Settings.EVENT_STORAGE_PATH.Replace("{persistent_path}", global::UnityEngine.Application.persistentDataPath));
			engageArchive = new global::DeltaDNA.EngageArchive(global::DeltaDNA.Settings.ENGAGE_STORAGE_PATH.Replace("{persistent_path}", global::UnityEngine.Application.persistentDataPath));
		}

		[global::System.Obsolete("Prefer 'StartSDK' instead, Init will be removed in a future update.")]
		public void Init(string envKey, string collectURL, string engageURL, string userID)
		{
			StartSDK(envKey, collectURL, engageURL, userID);
		}

		public void StartSDK(string envKey, string collectURL, string engageURL, string userID)
		{
			lock (_lock)
			{
				SetUserID(userID);
				EnvironmentKey = envKey;
				CollectURL = collectURL;
				EngageURL = engageURL;
				Platform = global::DeltaDNA.ClientInfo.Platform;
				SessionID = GetSessionID();
				initialised = true;
				TriggerDefaultEvents();
				if (Settings.BackgroundEventUpload && !IsInvoking("Upload"))
				{
					InvokeRepeating("Upload", Settings.BackgroundEventUploadStartDelaySeconds, Settings.BackgroundEventUploadRepeatRateSeconds);
				}
			}
		}

		public void NewSession()
		{
			SessionID = GetSessionID();
		}

		public void StopSDK()
		{
			global::DeltaDNA.Logger.LogDebug("Stopping SDK");
			RecordEvent("gameEnded");
			CancelInvoke();
			Upload();
			initialised = false;
		}

		[global::System.Obsolete("Prefer 'RecordEvent' instead, Trigger will be removed in a future update.")]
		public void TriggerEvent(string eventName)
		{
			RecordEvent(eventName, new global::System.Collections.Generic.Dictionary<string, object>());
		}

		public void RecordEvent(string eventName)
		{
			RecordEvent(eventName, new global::System.Collections.Generic.Dictionary<string, object>());
		}

		[global::System.Obsolete("Prefer 'RecordEvent' instead, Trigger will be removed in a future update.")]
		public void TriggerEvent(string eventName, global::DeltaDNA.EventBuilder eventParams)
		{
			RecordEvent(eventName, (eventParams != null) ? eventParams.ToDictionary() : new global::System.Collections.Generic.Dictionary<string, object>());
		}

		public void RecordEvent(string eventName, global::DeltaDNA.EventBuilder eventParams)
		{
			RecordEvent(eventName, (eventParams != null) ? eventParams.ToDictionary() : new global::System.Collections.Generic.Dictionary<string, object>());
		}

		[global::System.Obsolete("Prefer 'RecordEvent' instead, Trigger will be removed in a future update.")]
		public void TriggerEvent(string eventName, global::System.Collections.Generic.Dictionary<string, object> eventParams)
		{
			RecordEvent(eventName, eventParams);
		}

		public void RecordEvent(string eventName, global::System.Collections.Generic.Dictionary<string, object> eventParams)
		{
			if (!initialised)
			{
				throw new global::DeltaDNA.NotStartedException("You must first start the SDK via the StartSDK method");
			}
			global::System.Collections.Generic.Dictionary<string, object> dictionary = new global::System.Collections.Generic.Dictionary<string, object>();
			dictionary[EV_KEY_NAME] = eventName;
			dictionary[EV_KEY_USER_ID] = UserID;
			dictionary[EV_KEY_SESSION_ID] = SessionID;
			string currentTimestamp = GetCurrentTimestamp();
			if (!string.IsNullOrEmpty(currentTimestamp))
			{
				dictionary[EV_KEY_TIMESTAMP] = GetCurrentTimestamp();
			}
			if (!eventParams.ContainsKey(EP_KEY_PLATFORM))
			{
				eventParams.Add(EP_KEY_PLATFORM, Platform);
			}
			if (!eventParams.ContainsKey(EP_KEY_SDK_VERSION))
			{
				eventParams.Add(EP_KEY_SDK_VERSION, global::DeltaDNA.Settings.SDK_VERSION);
			}
			dictionary[EV_KEY_PARAMS] = eventParams;
			if (!string.IsNullOrEmpty(UserID) && !eventStore.Push(global::DeltaDNA.MiniJSON.Json.Serialize(dictionary)))
			{
				global::DeltaDNA.Logger.LogWarning("Event Store full, unable to handle event");
			}
		}

		public void RequestEngagement(string decisionPoint, global::System.Collections.Generic.Dictionary<string, object> engageParams, global::System.Action<global::System.Collections.Generic.Dictionary<string, object>> callback)
		{
			if (!initialised)
			{
				throw new global::DeltaDNA.NotStartedException("You must first start the SDK via the StartSDK method");
			}
			if (string.IsNullOrEmpty(EngageURL))
			{
				global::DeltaDNA.Logger.LogWarning("Engage URL not configured, can not make engagement.");
			}
			else if (string.IsNullOrEmpty(decisionPoint))
			{
				global::DeltaDNA.Logger.LogWarning("No decision point set, can not make engagement.");
			}
			else
			{
				StartCoroutine(EngageCoroutine(decisionPoint, engageParams, callback));
			}
		}

		public void RequestImageMessage(string decisionPoint, global::System.Collections.Generic.Dictionary<string, object> engageParams, global::DeltaDNA.Messaging.IPopup popup, global::System.Action<global::System.Collections.Generic.Dictionary<string, object>> callback = null)
		{
			global::System.Action<global::System.Collections.Generic.Dictionary<string, object>> callback2 = delegate(global::System.Collections.Generic.Dictionary<string, object> response)
			{
				if (response != null)
				{
					if (response.ContainsKey("image"))
					{
						global::System.Collections.Generic.Dictionary<string, object> configuration = response["image"] as global::System.Collections.Generic.Dictionary<string, object>;
						popup.Prepare(configuration);
					}
					if (callback != null)
					{
						callback(response);
					}
				}
			};
			RequestEngagement(decisionPoint, engageParams, callback2);
		}

		public void Upload()
		{
			if (!initialised)
			{
				throw new global::DeltaDNA.NotStartedException("You must first start the SDK via the StartSDK method");
			}
			if (IsUploading)
			{
				global::DeltaDNA.Logger.LogWarning("Event upload already in progress, aborting");
			}
			else
			{
				StartCoroutine(UploadCoroutine());
			}
		}

		public void SetLoggingLevel(global::DeltaDNA.Logger.Level level)
		{
			global::DeltaDNA.Logger.SetLogLevel(level);
		}

		public void ClearPersistentData()
		{
			global::UnityEngine.PlayerPrefs.DeleteKey(PF_KEY_USER_ID);
			global::UnityEngine.PlayerPrefs.DeleteKey(PF_KEY_FIRST_RUN);
			global::UnityEngine.PlayerPrefs.DeleteKey(PF_KEY_HASH_SECRET);
			global::UnityEngine.PlayerPrefs.DeleteKey(PF_KEY_CLIENT_VERSION);
			global::UnityEngine.PlayerPrefs.DeleteKey(PF_KEY_PUSH_NOTIFICATION_TOKEN);
			global::UnityEngine.PlayerPrefs.DeleteKey(PF_KEY_ANDROID_REGISTRATION_ID);
			eventStore.ClearAll();
			engageArchive.Clear();
		}

		public void UseCollectTimestamp(bool useCollect)
		{
			if (!useCollect)
			{
				SetTimestampFunc(DefaultTimestampFunc);
				return;
			}
			SetTimestampFunc(() => (global::System.DateTime?)null);
		}

		public void SetTimestampFunc(global::System.Func<global::System.DateTime?> TimestampFunc)
		{
			global::DeltaDNA.SDK.TimestampFunc = TimestampFunc;
		}

		public override void OnDestroy()
		{
			if (eventStore != null)
			{
				eventStore.Dispose();
			}
			if (engageArchive != null)
			{
				engageArchive.Save();
			}
			base.OnDestroy();
		}

		private string GetSessionID()
		{
			return global::System.Guid.NewGuid().ToString();
		}

		private string GetUserID()
		{
			string text = global::DeltaDNA.Settings.LEGACY_SETTINGS_STORAGE_PATH.Replace("{persistent_path}", global::UnityEngine.Application.persistentDataPath);
			if (global::System.IO.File.Exists(text))
			{
				global::DeltaDNA.Logger.LogDebug("Found a legacy file in " + text);
				using (global::System.IO.Stream stream = global::DeltaDNA.Utils.OpenStream(text))
				{
					try
					{
						global::System.Collections.Generic.List<byte> list = new global::System.Collections.Generic.List<byte>();
						byte[] array = new byte[1024];
						while (stream.Read(array, 0, array.Length) > 0)
						{
							list.AddRange(array);
						}
						byte[] array2 = list.ToArray();
						string json = global::System.Text.Encoding.UTF8.GetString(array2, 0, array2.Length);
						global::System.Collections.Generic.Dictionary<string, object> dictionary = global::DeltaDNA.MiniJSON.Json.Deserialize(json) as global::System.Collections.Generic.Dictionary<string, object>;
						if (dictionary.ContainsKey("userID"))
						{
							global::DeltaDNA.Logger.LogDebug("Found a legacy user id for player");
							return dictionary["userID"] as string;
						}
					}
					catch (global::System.Exception ex)
					{
						global::DeltaDNA.Logger.LogWarning("Problem reading legacy user id: " + ex.Message);
					}
				}
			}
			global::DeltaDNA.Logger.LogDebug("Creating a new user id for player");
			return global::System.Guid.NewGuid().ToString();
		}

		private static global::System.DateTime? DefaultTimestampFunc()
		{
			return global::System.DateTime.UtcNow;
		}

		private static string GetCurrentTimestamp()
		{
			global::System.DateTime? dateTime = TimestampFunc();
			if (dateTime.HasValue)
			{
				return dateTime.Value.ToString(global::DeltaDNA.Settings.EVENT_TIMESTAMP_FORMAT, global::System.Globalization.CultureInfo.InvariantCulture);
			}
			return null;
		}

		private global::System.Collections.IEnumerator UploadCoroutine()
		{
			IsUploading = true;
			try
			{
				eventStore.Swap();
				global::System.Collections.Generic.List<string> events = eventStore.Read();
				if (events.Count <= 0)
				{
					yield break;
				}
				global::DeltaDNA.Logger.LogDebug("Starting event upload");
				yield return StartCoroutine(PostEvents(resultCallback: delegate(bool succeeded)
				{
					if (succeeded)
					{
						global::DeltaDNA.Logger.LogDebug("Event upload successful");
						eventStore.ClearOut();
					}
					else
					{
						global::DeltaDNA.Logger.LogWarning("Event upload failed - try again later");
					}
				}, events: events.ToArray()));
			}
			finally
			{
				IsUploading = false;
			}
		}

		private global::System.Collections.IEnumerator EngageCoroutine(string decisionPoint, global::System.Collections.Generic.Dictionary<string, object> engageParams, global::System.Action<global::System.Collections.Generic.Dictionary<string, object>> callback)
		{
			global::DeltaDNA.Logger.LogDebug("Starting engagement for '" + decisionPoint + "'");
			global::System.Collections.Generic.Dictionary<string, object> engageRequest = new global::System.Collections.Generic.Dictionary<string, object>
			{
				{ "userID", UserID },
				{ "decisionPoint", decisionPoint },
				{ "sessionID", SessionID },
				{
					"version",
					global::DeltaDNA.Settings.ENGAGE_API_VERSION
				},
				{
					"sdkVersion",
					global::DeltaDNA.Settings.SDK_VERSION
				},
				{ "platform", Platform },
				{
					"timezoneOffset",
					global::System.Convert.ToInt32(global::DeltaDNA.ClientInfo.TimezoneOffset)
				}
			};
			if (global::DeltaDNA.ClientInfo.Locale != null)
			{
				engageRequest.Add("locale", global::DeltaDNA.ClientInfo.Locale);
			}
			if (engageParams != null)
			{
				engageRequest.Add("parameters", engageParams);
			}
			string engageJSON = null;
			try
			{
				engageJSON = global::DeltaDNA.MiniJSON.Json.Serialize(engageRequest);
			}
			catch (global::System.Exception ex)
			{
				global::DeltaDNA.Logger.LogWarning("Problem serialising engage request data: " + ex.Message);
				yield break;
			}
			string decisionPoint2 = default(string);
			global::System.Action<global::System.Collections.Generic.Dictionary<string, object>> callback2 = default(global::System.Action<global::System.Collections.Generic.Dictionary<string, object>>);
			global::System.Action<string> requestCb = delegate(string response)
			{
				bool flag = false;
				if (response != null)
				{
					global::DeltaDNA.Logger.LogDebug("Using live engagement: " + response);
					engageArchive[decisionPoint2] = response;
				}
				else if (engageArchive.Contains(decisionPoint2))
				{
					global::DeltaDNA.Logger.LogWarning("Engage request failed, using cached response.");
					flag = true;
					response = engageArchive[decisionPoint2];
				}
				else
				{
					global::DeltaDNA.Logger.LogWarning("Engage request failed");
				}
				global::System.Collections.Generic.Dictionary<string, object> dictionary = global::DeltaDNA.MiniJSON.Json.Deserialize(response) as global::System.Collections.Generic.Dictionary<string, object>;
				if (flag)
				{
					dictionary["isCachedResponse"] = flag;
				}
				if (callback2 != null)
				{
					callback2(dictionary);
				}
			};
			yield return StartCoroutine(EngageRequest(engageJSON, requestCb));
		}

		private global::System.Collections.IEnumerator PostEvents(string[] events, global::System.Action<bool> resultCallback)
		{
			string bulkEvent = "{\"eventList\":[" + string.Join(",", events) + "]}";
			string url = ((HashSecret == null) ? FormatURI(global::DeltaDNA.Settings.COLLECT_URL_PATTERN, CollectURL, EnvironmentKey) : FormatURI(hash: GenerateHash(bulkEvent, HashSecret), uriPattern: global::DeltaDNA.Settings.COLLECT_HASH_URL_PATTERN, apiHost: CollectURL, envKey: EnvironmentKey));
			int attempts = 0;
			bool succeeded = false;
			int num;
			do
			{
				global::System.Action<int, string> httpCb = delegate(int status, string response)
				{
					switch (status)
					{
					case 200:
					case 204:
						succeeded = true;
						return;
					case 100:
						if (string.IsNullOrEmpty(response))
						{
							succeeded = true;
							return;
						}
						break;
					}
					global::DeltaDNA.Logger.LogDebug("Error uploading events, Collect returned: " + status + " " + response);
				};
				yield return StartCoroutine(HttpPOST(url, bulkEvent, httpCb));
				yield return new global::UnityEngine.WaitForSeconds(Settings.HttpRequestRetryDelaySeconds);
				if (succeeded)
				{
					break;
				}
				attempts = (num = attempts + 1);
			}
			while (num < Settings.HttpRequestMaxRetries);
			resultCallback(succeeded);
		}

		private global::System.Collections.IEnumerator EngageRequest(string engagement, global::System.Action<string> callback)
		{
			string url = ((HashSecret == null) ? FormatURI(global::DeltaDNA.Settings.ENGAGE_URL_PATTERN, EngageURL, EnvironmentKey) : FormatURI(hash: GenerateHash(engagement, HashSecret), uriPattern: global::DeltaDNA.Settings.ENGAGE_HASH_URL_PATTERN, apiHost: EngageURL, envKey: EnvironmentKey));
			global::System.Action<string> callback2 = default(global::System.Action<string>);
			global::System.Action<int, string> httpCb = delegate(int status, string response)
			{
				if (status == 200 || status == 100)
				{
					if (callback2 != null)
					{
						callback2(response);
					}
				}
				else
				{
					global::DeltaDNA.Logger.LogDebug("Error requesting engagement, Engage returned: " + status);
					if (callback2 != null)
					{
						callback2(null);
					}
				}
			};
			yield return StartCoroutine(HttpPOST(url, engagement, httpCb));
		}

		private global::System.Collections.IEnumerator HttpGET(string url, global::System.Action<int, string> responseCallback = null)
		{
			global::DeltaDNA.Logger.LogDebug("HttpGET " + url);
			global::UnityEngine.WWW www = new global::UnityEngine.WWW(url);
			yield return www;
			int statusCode = 0;
			if (www.error == null)
			{
				statusCode = 200;
				if (responseCallback != null)
				{
					responseCallback(statusCode, www.text);
				}
			}
			else
			{
				statusCode = ReadWWWResponse(www.error);
				if (responseCallback != null)
				{
					responseCallback(statusCode, null);
				}
			}
		}

		private global::System.Collections.IEnumerator HttpPOST(string url, string json, global::System.Action<int, string> responseCallback = null)
		{
			global::DeltaDNA.Logger.LogDebug("HttpPOST " + url + " " + json);
			global::UnityEngine.WWWForm form = new global::UnityEngine.WWWForm();
			global::System.Collections.Hashtable headers = form.headers;
			headers["Content-Type"] = "application/json";
			byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(json);
			global::UnityEngine.WWW www = new global::UnityEngine.WWW(url, bytes, global::DeltaDNA.Utils.HashtableToDictionary<string, string>(headers));
			yield return www;
			int statusCode = ReadWWWStatusCode(www);
			if (www.error == null)
			{
				if (responseCallback != null)
				{
					responseCallback(statusCode, www.text);
				}
				yield break;
			}
			global::DeltaDNA.Logger.LogDebug("WWW.error: " + www.error);
			if (responseCallback != null)
			{
				responseCallback(statusCode, null);
			}
		}

		private static int ReadWWWResponse(string response)
		{
			global::System.Text.RegularExpressions.MatchCollection matchCollection = global::System.Text.RegularExpressions.Regex.Matches(response, "^.*\\s(\\d{3})\\s.*$");
			if (matchCollection.Count > 0 && matchCollection[0].Groups.Count > 0)
			{
				return global::System.Convert.ToInt32(matchCollection[0].Groups[1].Value);
			}
			return 0;
		}

		private int ReadWWWStatusCode(global::UnityEngine.WWW www)
		{
			int result = 0;
			string key = "NULL";
			if (!www.responseHeaders.ContainsKey(key))
			{
				result = ((!string.IsNullOrEmpty(www.error)) ? ReadWWWResponse(www.error) : 200);
			}
			else
			{
				string input = www.responseHeaders[key];
				global::System.Text.RegularExpressions.MatchCollection matchCollection = global::System.Text.RegularExpressions.Regex.Matches(input, "^HTTP.*\\s(\\d{3})\\s.*$");
				if (matchCollection.Count > 0 && matchCollection[0].Groups.Count > 0)
				{
					result = global::System.Convert.ToInt32(matchCollection[0].Groups[1].Value);
				}
			}
			return result;
		}

		private static string FormatURI(string uriPattern, string apiHost, string envKey, string hash = null)
		{
			string text = uriPattern.Replace("{host}", apiHost);
			text = text.Replace("{env_key}", envKey);
			return text.Replace("{hash}", hash);
		}

		private static string ValidateURL(string url)
		{
			if (!url.ToLower().StartsWith("http://"))
			{
				url = "http://" + url;
			}
			return url;
		}

		private static string GenerateHash(string data, string secret)
		{
			global::DeltaDNA.MD5 mD = global::DeltaDNA.MD5.Create();
			byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(data + secret);
			byte[] array = mD.ComputeHash(bytes);
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		private void SetUserID(string userID)
		{
			if (string.IsNullOrEmpty(userID))
			{
				if (string.IsNullOrEmpty(UserID))
				{
					UserID = GetUserID();
				}
			}
			else if (UserID != userID)
			{
				global::UnityEngine.PlayerPrefs.DeleteKey(PF_KEY_FIRST_RUN);
				UserID = userID;
			}
		}

		private void TriggerDefaultEvents()
		{
			if (Settings.OnFirstRunSendNewPlayerEvent && global::UnityEngine.PlayerPrefs.GetInt(PF_KEY_FIRST_RUN, 1) > 0)
			{
				global::DeltaDNA.Logger.LogDebug("Sending 'newPlayer' event");
				global::DeltaDNA.EventBuilder eventParams = new global::DeltaDNA.EventBuilder().AddParam("userCountry", global::DeltaDNA.ClientInfo.CountryCode);
				RecordEvent("newPlayer", eventParams);
				global::UnityEngine.PlayerPrefs.SetInt(PF_KEY_FIRST_RUN, 0);
			}
			if (Settings.OnInitSendGameStartedEvent)
			{
				global::DeltaDNA.Logger.LogDebug("Sending 'gameStarted' event");
				global::DeltaDNA.EventBuilder eventParams2 = new global::DeltaDNA.EventBuilder().AddParam("clientVersion", ClientVersion).AddParam("pushNotificationToken", PushNotificationToken).AddParam("androidRegistrationID", AndroidRegistrationID);
				RecordEvent("gameStarted", eventParams2);
			}
			if (Settings.OnInitSendClientDeviceEvent)
			{
				global::DeltaDNA.Logger.LogDebug("Sending 'clientDevice' event");
				global::DeltaDNA.EventBuilder eventParams3 = new global::DeltaDNA.EventBuilder().AddParam("deviceName", global::DeltaDNA.ClientInfo.DeviceName).AddParam("deviceType", global::DeltaDNA.ClientInfo.DeviceType).AddParam("hardwareVersion", global::DeltaDNA.ClientInfo.DeviceModel)
					.AddParam("operatingSystem", global::DeltaDNA.ClientInfo.OperatingSystem)
					.AddParam("operatingSystemVersion", global::DeltaDNA.ClientInfo.OperatingSystemVersion)
					.AddParam("manufacturer", global::DeltaDNA.ClientInfo.Manufacturer)
					.AddParam("timezoneOffset", global::DeltaDNA.ClientInfo.TimezoneOffset)
					.AddParam("userLanguage", global::DeltaDNA.ClientInfo.LanguageCode);
				RecordEvent("clientDevice", eventParams3);
			}
		}
	}
}
