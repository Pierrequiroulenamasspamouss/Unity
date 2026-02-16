namespace Kampai.Main
{
	public class HALService : global::Kampai.Main.ILocalizationService
	{
		private const char OPEN_KEY_DELIM = '{';

		private const char CLOSE_KEY_DELIM = '}';

		private const string KEY_NOT_FOUND = "KEY NOT FOUND";

		private global::System.Collections.Generic.Dictionary<string, global::Kampai.Main.ILocalString> localStringDict;

		private string jsonPath;

		private bool isLanguageSupported = true;

		private string language;

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public void Initialize(string langCode)
		{
			jsonPath = GetResourcePath(langCode);
			if (string.IsNullOrEmpty(jsonPath))
			{
				isLanguageSupported = false;
				jsonPath = "EN-US";
			}
			language = ExtractLanguageFromLocale(jsonPath);
			try
			{
				localStringDict = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::System.Collections.Generic.Dictionary<string, global::Kampai.Main.ILocalString>>(global::Kampai.Util.Native.GetStreamingTextAsset(string.Format("{0}{1}.json", "Loc_Text_Preinstalled/", jsonPath)), new global::Newtonsoft.Json.JsonConverter[1] { GetStringConverter() });
			}
			catch (global::System.IO.FileNotFoundException ex)
			{
				logger.Error("Error obtaining preinstalled localization file: {0}", ex.ToString());
				localStringDict = new global::System.Collections.Generic.Dictionary<string, global::Kampai.Main.ILocalString>();
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::Kampai.Main.ILocalString> item in localStringDict)
			{
				ParseLocalString(item.Value);
			}
		}

		public bool IsInitialized()
		{
			return localStringDict != null;
		}

		public void Update()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::Kampai.Main.ILocalString> item in global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::System.Collections.Generic.Dictionary<string, global::Kampai.Main.ILocalString>>(global::Kampai.Util.KampaiResources.Load<global::UnityEngine.TextAsset>(jsonPath).ToString(), new global::Newtonsoft.Json.JsonConverter[1] { GetStringConverter() }))
			{
				ParseLocalString(item.Value);
				localStringDict[item.Key] = item.Value;
			}
		}

		public string GetLanguage()
		{
			return language;
		}

		public bool IsLanguageSupported()
		{
			return isLanguageSupported;
		}

		public bool Contains(string key)
		{
			if (key == null || !localStringDict.ContainsKey(key))
			{
				return false;
			}
			return true;
		}

		public string GetString(string key, params object[] args)
		{
			if (key == null)
			{
				string text = string.Format("{0}: {1}", "KEY NOT FOUND", "null key");
				logger.Log(global::Kampai.Util.Logger.Level.Warning, text);
				return text;
			}
			if (!localStringDict.ContainsKey(key))
			{
				string text2 = string.Format("{0}: {1}", "KEY NOT FOUND", key);
				logger.Log(global::Kampai.Util.Logger.Level.Warning, text2);
				return text2;
			}
			global::Kampai.Main.LocalQuantityString localQuantityString = localStringDict[key] as global::Kampai.Main.LocalQuantityString;
			if (localQuantityString != null)
			{
				return localQuantityString.GetStringFormat(args);
			}
			global::Kampai.Main.LocalString localString = localStringDict[key] as global::Kampai.Main.LocalString;
			if (localString != null)
			{
				return localString.GetStringFormat(args);
			}
			string text3 = string.Format("{0}: {1}", "KEY NOT FOUND", key);
			logger.Log(global::Kampai.Util.Logger.Level.Warning, text3);
			return text3;
		}

		private global::Newtonsoft.Json.JsonConverter GetStringConverter()
		{
			return new global::Kampai.Main.LocalStringConverter();
		}

		private void ParseLocalString(global::Kampai.Main.ILocalString iLocalString)
		{
			global::Kampai.Main.LocalString localString = iLocalString as global::Kampai.Main.LocalString;
			if (localString == null)
			{
				return;
			}
			string text = localString.GetString();
			int num = text.IndexOf('{', 0);
			while (num != -1)
			{
				int num2 = text.IndexOf('}', num);
				string text2 = text.Substring(num + 1, num2 - num - 1);
				num = text.IndexOf('{', num2);
				if (localStringDict.ContainsKey(text2))
				{
					global::Kampai.Main.LocalString localString2 = localStringDict[text2] as global::Kampai.Main.LocalString;
					if (localString2 != null)
					{
						ParseLocalString(localStringDict[text2]);
						string tag = string.Format("{0}{1}{2}", '{', text2, '}');
						string tagValue = localString2.GetString();
						localString.SetKeyValue(tag, tagValue);
					}
				}
			}
		}

		public string GetLanguageKey()
		{
			return jsonPath;
		}

		public static string GetResourcePath(string languageCode)
		{
			languageCode = languageCode.ToLower();
			string text = languageCode;
			if (languageCode.Contains("_"))
			{
				text = languageCode.Split('_')[0];
			}
			else if (languageCode.Contains("-"))
			{
				text = languageCode.Split('-')[0];
			}
			if (text.Equals("en"))
			{
				return "EN-US";
			}
			if (text.Equals("fr"))
			{
				return "FR-FR";
			}
			if (text.Equals("de"))
			{
				return "DE-DE";
			}
			if (text.Equals("es"))
			{
				return "ES-ES";
			}
			if (text.Equals("it"))
			{
				return "IT-IT";
			}
			if (text.Equals("pt"))
			{
				return "PT-BR";
			}
			if (text.Equals("nl"))
			{
				return "NL-NL";
			}
			if (text.Equals("ko"))
			{
				return "KO-KR";
			}
			if (text.Equals("ru"))
			{
				return "RU-RU";
			}
			if (text.Equals("ja"))
			{
				return "JA";
			}
			if (languageCode.Equals("zh-hans") || languageCode.Equals("zh_hans") || languageCode.Equals("zh_cn") || languageCode.Equals("zh-cn"))
			{
				return "ZH-CN";
			}
			if (languageCode.Equals("zh-hant") || languageCode.Equals("zh_hant") || languageCode.Equals("zh_tw") || languageCode.Equals("zh-tw") || languageCode.Equals("zh_hk") || languageCode.Equals("zh-hk"))
			{
				return "ZH-TW";
			}
			if (text.Equals("zh"))
			{
				return "ZH-CN";
			}
			if (text.Equals("tr"))
			{
				return "TR";
			}
			if (text.Equals("id") || languageCode.Equals("in_id"))
			{
				return "ID";
			}
			return string.Empty;
		}

		public static string ExtractLanguageFromLocale(string locale)
		{
			return ((!locale.Contains("-")) ? locale : locale.Substring(0, locale.IndexOf('-'))).ToLower();
		}
	}
}
