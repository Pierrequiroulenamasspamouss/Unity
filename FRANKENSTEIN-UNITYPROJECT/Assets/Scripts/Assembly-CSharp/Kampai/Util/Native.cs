namespace Kampai.Util
{
    public static class Native
    {
        private enum LogLevel
        {
            ANDROID_LOG_VERBOSE = 2,
            ANDROID_LOG_DEBUG = 3,
            ANDROID_LOG_INFO = 4,
            ANDROID_LOG_WARN = 5,
            ANDROID_LOG_ERROR = 6,
            ANDROID_LOG_FATAL = 7
        }

        private const string MINIONS_LIB_NAME = "minions";

        private static string bundleVersion;

        private static string authToken;

        private static string bundleIdentifier;

        private static global::System.IO.StreamWriter sw;

        private static int androidOSVersion;

        public static string StaticConfig
        {
            get
            {
#if UNITY_ANDROID && !UNITY_EDITOR
				// ORIGINAL ANDROID CODE
				using (global::UnityEngine.AndroidJavaClass androidJavaClass = new global::UnityEngine.AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					global::UnityEngine.AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<global::UnityEngine.AndroidJavaObject>("currentActivity");
					string text = androidJavaObject.Call<string>("getPackageName", new object[0]);
					global::UnityEngine.AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<global::UnityEngine.AndroidJavaObject>("getResources", new object[0]);
					int num = androidJavaObject2.Call<int>("getIdentifier", new object[3] { "config", "raw", text });
					global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
					global::UnityEngine.AndroidJavaObject androidJavaObject3 = androidJavaObject2.Call<global::UnityEngine.AndroidJavaObject>("openRawResource", new object[1] { num });
					global::UnityEngine.AndroidJavaObject androidJavaObject4 = new global::UnityEngine.AndroidJavaObject("java.io.InputStreamReader", androidJavaObject3);
					global::UnityEngine.AndroidJavaObject androidJavaObject5 = new global::UnityEngine.AndroidJavaObject("java.io.BufferedReader", androidJavaObject4);
					string value;
					while ((value = androidJavaObject5.Call<string>("readLine", new object[0])) != null)
					{
						stringBuilder.Append(value);
					}
					return stringBuilder.ToString();
				}
#else
                // WINDOWS/EDITOR REPLACEMENT
                // Tries to find config.json in StreamingAssets to allow game configuration
                string path = global::System.IO.Path.Combine(global::UnityEngine.Application.streamingAssetsPath, "config.json");
                if (global::System.IO.File.Exists(path))
                {
                    return global::System.IO.File.ReadAllText(path);
                }
                return "{}"; // Return empty JSON if missing to prevent crash
#endif
            }
        }

        public static string BundleVersion
        {
            get
            {
#if UNITY_ANDROID && !UNITY_EDITOR
				// ORIGINAL ANDROID CODE
				if (bundleVersion == null)
				{
					using (global::UnityEngine.AndroidJavaObject androidJavaObject = GetCurrentActivity())
					{
						using (global::UnityEngine.AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<global::UnityEngine.AndroidJavaObject>("getPackageManager", new object[0]))
						{
							using (global::UnityEngine.AndroidJavaObject androidJavaObject3 = androidJavaObject2.Call<global::UnityEngine.AndroidJavaObject>("getPackageInfo", new object[2] { BundleIdentifier, 0 }))
							{
								bundleVersion = androidJavaObject3.Get<string>("versionName");
							}
						}
					}
				}
				return bundleVersion;
#else
                // WINDOWS/EDITOR REPLACEMENT
                return "1.0.0";
#endif
            }
        }

        public static string BundleIdentifier
        {
            get
            {
#if UNITY_ANDROID && !UNITY_EDITOR
				// ORIGINAL ANDROID CODE
				if (bundleIdentifier == null)
				{
					using (global::UnityEngine.AndroidJavaObject androidJavaObject = GetCurrentActivity())
					{
						bundleIdentifier = androidJavaObject.Call<string>("getPackageName", new object[0]);
					}
				}
				return bundleIdentifier;
#else
                // WINDOWS/EDITOR REPLACEMENT
                // Prevents the NullReferenceException you were seeing
                return "com.ea.gp.minions";
#endif
            }
        }

#if UNITY_ANDROID && !UNITY_EDITOR
		// Only declare external DLL import on Android to prevent DllNotFoundException on Windows
		[global::System.Runtime.InteropServices.DllImport("minions")]
		private static extern int Minions_Util_Native_Log(int logLevel, string tag, string msg);
#endif

        public static bool IsUserMusicPlaying()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			// ORIGINAL ANDROID CODE
			using (global::UnityEngine.AndroidJavaClass androidJavaClass = new global::UnityEngine.AndroidJavaClass("com.ea.gp.minions.MainActivity"))
			{
				return androidJavaClass.CallStatic<bool>("isUserMusicPlaying", new object[0]);
			}
#else
            return false;
#endif
        }

        public static void clearAuthToken()
        {
            authToken = null;
        }

        public static string getAuthToken()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			// ORIGINAL ANDROID CODE
			if (authToken == null)
			{
				using (global::UnityEngine.AndroidJavaObject androidJavaObject = GetCurrentActivity())
				{
					using (global::UnityEngine.AndroidJavaClass androidJavaClass = new global::UnityEngine.AndroidJavaClass("com.prime31.PlayGameServicesPluginBase"))
					{
						using (global::UnityEngine.AndroidJavaObject androidJavaObject2 = androidJavaClass.CallStatic<global::UnityEngine.AndroidJavaObject>("instance", new object[0]))
						{
							using (global::UnityEngine.AndroidJavaObject androidJavaObject3 = androidJavaObject2.Get<global::UnityEngine.AndroidJavaObject>("helper"))
							{
								using (global::UnityEngine.AndroidJavaObject androidJavaObject4 = androidJavaObject3.Call<global::UnityEngine.AndroidJavaObject>("getApiClient", new object[0]))
								{
									using (global::UnityEngine.AndroidJavaClass androidJavaClass2 = new global::UnityEngine.AndroidJavaClass("com.google.android.gms.plus.Plus"))
									{
										using (global::UnityEngine.AndroidJavaObject androidJavaObject5 = androidJavaClass2.GetStatic<global::UnityEngine.AndroidJavaObject>("AccountApi"))
										{
											using (global::UnityEngine.AndroidJavaClass androidJavaClass3 = new global::UnityEngine.AndroidJavaClass("com.google.android.gms.auth.GoogleAuthUtil"))
											{
												authToken = androidJavaClass3.CallStatic<string>("getToken", new object[3]
												{
													androidJavaObject,
													androidJavaObject5.Call<string>("getAccountName", new object[1] { androidJavaObject4 }),
													"oauth2:https://www.googleapis.com/auth/plus.login"
												});
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return authToken;
#else
            return "dummy_auth_token";
#endif
        }

        public static int GetGoogleAccountCount()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			// ORIGINAL ANDROID CODE
			int num = 0;
			using (global::UnityEngine.AndroidJavaObject androidJavaObject = GetCurrentActivity())
			{
				using (global::UnityEngine.AndroidJavaClass androidJavaClass = new global::UnityEngine.AndroidJavaClass("android.accounts.AccountManager"))
				{
					using (global::UnityEngine.AndroidJavaObject androidJavaObject2 = androidJavaClass.CallStatic<global::UnityEngine.AndroidJavaObject>("get", new object[1] { androidJavaObject }))
					{
						return androidJavaObject2.Call<global::UnityEngine.AndroidJavaObject[]>("getAccountsByType", new object[1] { "com.google" }).Length;
					}
				}
			}
#else
            return 0;
#endif
        }

        public static void LogError(string text)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			Minions_Util_Native_Log(6, "Minions", text);
#else
            global::UnityEngine.Debug.LogError(text);
#endif
            AndroidFileLog(text);
        }

        public static void LogWarning(string text)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			Minions_Util_Native_Log(5, "Minions", text);
#else
            global::UnityEngine.Debug.LogWarning(text);
#endif
            AndroidFileLog(text);
        }

        public static void LogInfo(string text)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			Minions_Util_Native_Log(4, "Minions", text);
#else
            global::UnityEngine.Debug.Log(text);
#endif
            AndroidFileLog(text);
        }

        public static void LogDebug(string text)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			Minions_Util_Native_Log(3, "Minions", text);
#else
            global::UnityEngine.Debug.Log(text);
#endif
            AndroidFileLog(text);
        }

        public static void LogVerbose(string text)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			Minions_Util_Native_Log(2, "Minions", text);
#else
            global::UnityEngine.Debug.Log(text);
#endif
            AndroidFileLog(text);
        }

        public static string GetFilePath()
        {
            return global::Kampai.Util.GameConstants.PERSISTENT_DATA_PATH + "/log.txt";
        }

        public static uint GetMemoryUsage()
        {
            return global::UnityEngine.Profiler.usedHeapSize;
        }

        public static void Crash()
        {
            global::UnityEngine.Debug.Log("CRASH?");
        }

        public static void ScheduleLocalNotification(string type, int secondsFromNow, string title, string text, string stackedTitle, string stackedText, string action, string sound, string launchImage, int badgeNumber = 1)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			// ORIGINAL ANDROID CODE
			using (global::UnityEngine.AndroidJavaClass androidJavaClass = GetNotificationManagerHelper())
			{
				androidJavaClass.CallStatic("scheduleNotification", (long)global::System.TimeSpan.FromSeconds(secondsFromNow).TotalMilliseconds, type, launchImage, string.IsNullOrEmpty(sound) ? null : string.Format("{0}/{1}", global::Kampai.Util.GameConstants.PERSISTENT_DATA_PATH, sound), title, text, stackedTitle, stackedText, badgeNumber);
			}
#endif
        }

        public static void CancelLocalNotification(string type)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			// ORIGINAL ANDROID CODE
			using (global::UnityEngine.AndroidJavaClass androidJavaClass = GetNotificationManagerHelper())
			{
				androidJavaClass.CallStatic("cancelNotification", type, false);
			}
#endif
        }

        public static void CancelAllLocalNotifications()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			// ORIGINAL ANDROID CODE
			using (global::UnityEngine.AndroidJavaClass androidJavaClass = GetNotificationManagerHelper())
			{
				androidJavaClass.CallStatic("cancelAllNotifications");
			}
#endif
        }

        public static string GetDeviceLanguage()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			// ORIGINAL ANDROID CODE
			string text = "en";
			using (global::UnityEngine.AndroidJavaClass androidJavaClass = new global::UnityEngine.AndroidJavaClass("java/util/Locale"))
			{
				using (global::UnityEngine.AndroidJavaObject androidJavaObject = androidJavaClass.CallStatic<global::UnityEngine.AndroidJavaObject>("getDefault", new object[0]))
				{
					return androidJavaObject.Call<string>("toString", new object[0]);
				}
			}
#else
            return "en";
#endif
        }

        public static bool AutorotationIsOSAllowed()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			// ORIGINAL ANDROID CODE
			using (global::UnityEngine.AndroidJavaClass androidJavaClass = new global::UnityEngine.AndroidJavaClass("com.ea.gp.minions.utils.Misc"))
			{
				int num = androidJavaClass.CallStatic<int>("getAutorotateSetting", new object[0]);
				return num != 0;
			}
#else
            return true;
#endif
        }

        public static void SetBackupFlag(string path, bool shouldBackup)
        {
        }

        public static void AndroidFileLog(string text)
        {
            string filePath = GetFilePath();
            if (sw == null)
            {
                if (!global::System.IO.File.Exists(filePath))
                {
                    sw = global::System.IO.File.CreateText(filePath);
                }
                else
                {
                    sw = global::System.IO.File.AppendText(filePath);
                }
            }
            if (sw != null)
            {
                sw.WriteLine(text + "\n");
            }
        }

        public static int GetAndroidOSVersion()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			// ORIGINAL ANDROID CODE
			if (androidOSVersion == 0)
			{
				using (global::UnityEngine.AndroidJavaClass androidJavaClass = new global::UnityEngine.AndroidJavaClass("android.os.Build$VERSION"))
				{
					androidOSVersion = androidJavaClass.GetStatic<int>("SDK_INT");
				}
			}
			return androidOSVersion;
#else
            return 21; // Simulate Android 5.0+ to bypass OS checks
#endif
        }

        public static global::UnityEngine.AndroidJavaObject GetCurrentActivity()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			// ORIGINAL ANDROID CODE
			using (global::UnityEngine.AndroidJavaClass androidJavaClass = new global::UnityEngine.AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				return androidJavaClass.GetStatic<global::UnityEngine.AndroidJavaObject>("currentActivity");
			}
#else
            return null;
#endif
        }

        public static global::UnityEngine.AndroidJavaClass GetNotificationManagerHelper()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			// ORIGINAL ANDROID CODE
			return new global::UnityEngine.AndroidJavaClass("com.ea.gp.minions.notifications.NotificationManagerHelper");
#else
            return null;
#endif
        }

        public static string GetPersistentDataPath()
        {
            // Safe to use Unity path on all platforms
            string text = global::UnityEngine.Application.persistentDataPath;
#if UNITY_ANDROID && !UNITY_EDITOR
			// ORIGINAL ANDROID FALLBACK LOGIC
			if (string.IsNullOrEmpty(text))
			{
				using (global::UnityEngine.AndroidJavaClass androidJavaClass = new global::UnityEngine.AndroidJavaClass("com.ea.gp.minions.utils.FileUtils"))
				{
					text = androidJavaClass.CallStatic<string>("getPersistentDataPath", new object[0]);
				}
			}
#endif
            return text;
        }

        public static ulong GetAvailableStorage(string path)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			// ORIGINAL ANDROID CODE
			ulong num = 0uL;
			using (global::UnityEngine.AndroidJavaClass androidJavaClass = new global::UnityEngine.AndroidJavaClass("com.ea.gp.minions.utils.FileUtils"))
			{
				return (ulong)((!string.IsNullOrEmpty(path)) ? androidJavaClass.CallStatic<long>("getAvailableStorage", new object[1] { path }) : androidJavaClass.CallStatic<long>("getAvailableInternalStorage", new object[0]));
			}
#else
            return 1073741824; // 1GB of fake space
#endif
        }

        public static bool CanShowNetworkSettings()
        {
            return true;
        }

        public static void OpenNetworkSettings()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			// ORIGINAL ANDROID CODE
			using (global::UnityEngine.AndroidJavaClass androidJavaClass = new global::UnityEngine.AndroidJavaClass("com.ea.gp.minions.utils.Misc"))
			{
				androidJavaClass.CallStatic("openNetworkSettings");
			}
#endif
        }

        public static byte[] GetStreamingAsset(string path)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			// ORIGINAL ANDROID CODE
			byte[] result = null;
			using (global::UnityEngine.AndroidJavaClass androidJavaClass = new global::UnityEngine.AndroidJavaClass("com.ea.gp.minions.utils.FileUtils"))
			{
				try
				{
					result = androidJavaClass.CallStatic<byte[]>("openAsset", new object[1] { path });
				}
				catch (global::UnityEngine.AndroidJavaException ex)
				{
					global::UnityEngine.AndroidJavaClass androidJavaClass2 = new global::UnityEngine.AndroidJavaClass("java.io.FileNotFoundException");
					if (androidJavaClass2.GetType().IsInstanceOfType(ex))
					{
						throw new global::System.IO.FileNotFoundException(ex.ToString());
					}
					LogError("Error opening asset: " + ex.ToString());
				}
			}
			return result;
#else
            // WINDOWS/EDITOR REPLACEMENT
            // Allows reading files from StreamingAssets folder
            string fullPath = global::System.IO.Path.Combine(global::UnityEngine.Application.streamingAssetsPath, path);
            if (global::System.IO.File.Exists(fullPath))
            {
                return global::System.IO.File.ReadAllBytes(fullPath);
            }
            return null;
#endif
        }

        public static string GetStreamingTextAsset(string path)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			// ORIGINAL ANDROID CODE
			string result = null;
			using (global::UnityEngine.AndroidJavaClass androidJavaClass = new global::UnityEngine.AndroidJavaClass("com.ea.gp.minions.utils.FileUtils"))
			{
				try
				{
					result = androidJavaClass.CallStatic<string>("openTextAsset", new object[1] { path });
				}
				catch (global::UnityEngine.AndroidJavaException ex)
				{
					global::UnityEngine.AndroidJavaClass androidJavaClass2 = new global::UnityEngine.AndroidJavaClass("java.io.FileNotFoundException");
					if (androidJavaClass2.GetType().IsInstanceOfType(ex))
					{
						throw new global::System.IO.FileNotFoundException(ex.ToString());
					}
					LogError("Error opening text asset: " + ex.ToString());
				}
			}
			return result;
#else
            // WINDOWS/EDITOR REPLACEMENT
            string fullPath = global::System.IO.Path.Combine(global::UnityEngine.Application.streamingAssetsPath, path);
            if (global::System.IO.File.Exists(fullPath))
            {
                return global::System.IO.File.ReadAllText(fullPath);
            }
            return null;
#endif
        }

        public static void Exit()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			// ORIGINAL ANDROID CODE
			using (global::UnityEngine.AndroidJavaObject androidJavaObject = GetCurrentActivity())
			{
				androidJavaObject.Call<bool>("moveTaskToBack", new object[1] { true });
			}
#else
            global::UnityEngine.Application.Quit();
#endif
        }
    }
}