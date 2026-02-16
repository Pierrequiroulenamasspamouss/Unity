namespace DeltaDNA
{
	internal static class ClientInfo
	{
		private static string platform;

		private static string deviceName;

		private static string deviceModel;

		private static string deviceType;

		private static string operatingSystem;

		private static string operatingSystemVersion;

		private static string manufacturer;

		private static string timezoneOffset;

		private static string countryCode;

		private static string languageCode;

		private static string locale;

		public static string Platform
		{
			get
			{
				return platform ?? (platform = GetPlatform());
			}
		}

		public static string DeviceName
		{
			get
			{
				return deviceName ?? (deviceName = GetDeviceName());
			}
		}

		public static string DeviceModel
		{
			get
			{
				return deviceModel ?? (deviceModel = GetDeviceModel());
			}
		}

		public static string DeviceType
		{
			get
			{
				return deviceType ?? (deviceType = GetDeviceType());
			}
		}

		public static string OperatingSystem
		{
			get
			{
				return operatingSystem ?? (operatingSystem = GetOperatingSystem());
			}
		}

		public static string OperatingSystemVersion
		{
			get
			{
				return operatingSystemVersion ?? (operatingSystemVersion = GetOperatingSystemVersion());
			}
		}

		public static string Manufacturer
		{
			get
			{
				return manufacturer ?? (manufacturer = GetManufacturer());
			}
		}

		public static string TimezoneOffset
		{
			get
			{
				return timezoneOffset ?? (timezoneOffset = GetCurrentTimezoneOffset());
			}
		}

		public static string CountryCode
		{
			get
			{
				return countryCode ?? (countryCode = GetCountryCode());
			}
		}

		public static string LanguageCode
		{
			get
			{
				return languageCode ?? (languageCode = GetLanguageCode());
			}
		}

		public static string Locale
		{
			get
			{
				return locale ?? (locale = GetLocale());
			}
		}

		private static bool RuntimePlatformIs(string platformName)
		{
			return global::System.Enum.IsDefined(typeof(global::UnityEngine.RuntimePlatform), platformName) && global::UnityEngine.Application.platform.ToString() == platformName;
		}

		private static float ScreenSizeInches()
		{
			float num = (float)global::UnityEngine.Screen.width / global::UnityEngine.Screen.dpi;
			float num2 = (float)global::UnityEngine.Screen.height / global::UnityEngine.Screen.dpi;
			return (float)global::System.Math.Sqrt(num * num + num2 * num2);
		}

		private static bool IsTablet()
		{
			return ScreenSizeInches() > 6f;
		}

		private static string GetPlatform()
		{
			if (RuntimePlatformIs("OSXEditor"))
			{
				return "MAC_CLIENT";
			}
			if (RuntimePlatformIs("OSXPlayer"))
			{
				return "MAC_CLIENT";
			}
			if (RuntimePlatformIs("WindowsPlayer"))
			{
				return "PC_CLIENT";
			}
			if (RuntimePlatformIs("OSXWebPlayer"))
			{
				return "WEB";
			}
			if (RuntimePlatformIs("OSXDashboardPlayer"))
			{
				return "MAC_CLIENT";
			}
			if (RuntimePlatformIs("WindowsWebPlayer"))
			{
				return "WEB";
			}
			if (RuntimePlatformIs("WindowsEditor"))
			{
				return "PC_CLIENT";
			}
			if (RuntimePlatformIs("IPhonePlayer"))
			{
				string text = global::UnityEngine.SystemInfo.deviceModel;
				if (text.Contains("iPad"))
				{
					return "IOS_TABLET";
				}
				return "IOS_MOBILE";
			}
			if (RuntimePlatformIs("PS3"))
			{
				return "PS3";
			}
			if (RuntimePlatformIs("XBOX360"))
			{
				return "XBOX360";
			}
			if (RuntimePlatformIs("Android"))
			{
				return (!IsTablet()) ? "ANDROID_MOBILE" : "ANDROID_TABLET";
			}
			if (RuntimePlatformIs("NaCL"))
			{
				return "WEB";
			}
			if (RuntimePlatformIs("LinuxPlayer"))
			{
				return "PC_CLIENT";
			}
			if (RuntimePlatformIs("FlashPlayer"))
			{
				return "WEB";
			}
			if (RuntimePlatformIs("MetroPlayerX86") || RuntimePlatformIs("MetroPlayerX64") || RuntimePlatformIs("MetroPlayerARM"))
			{
				if (global::UnityEngine.SystemInfo.deviceType == global::UnityEngine.DeviceType.Handheld)
				{
					return (!IsTablet()) ? "WINDOWS_MOBILE" : "WINDOWS_TABLET";
				}
				return "PC_CLIENT";
			}
			if (RuntimePlatformIs("WP8Player"))
			{
				return (!IsTablet()) ? "WINDOWS_MOBILE" : "WINDOWS_TABLET";
			}
			if (RuntimePlatformIs("BB10Player") || RuntimePlatformIs("BlackBerryPlayer"))
			{
				return (!IsTablet()) ? "BLACKBERRY_MOBILE" : "BLACKBERRY_TABLET";
			}
			if (RuntimePlatformIs("TizenPlayer"))
			{
				return (!IsTablet()) ? "ANDROID_MOBILE" : "ANDROID_TABLET";
			}
			if (RuntimePlatformIs("PSP2"))
			{
				return "PSVITA";
			}
			if (RuntimePlatformIs("PS4"))
			{
				return "PS4";
			}
			if (RuntimePlatformIs("PSMPlayer"))
			{
				return "WEB";
			}
			if (RuntimePlatformIs("XboxOne"))
			{
				return "XBOXONE";
			}
			if (RuntimePlatformIs("SamsungTVPlayer"))
			{
				return "ANDROID_CONSOLE";
			}
			return "UNKNOWN";
		}

		private static string GetDeviceName()
		{
			string text = global::UnityEngine.SystemInfo.deviceModel;
			switch (text)
			{
			case "iPhone1,1":
				return "iPhone 1G";
			case "iPhone1,2":
				return "iPhone 3G";
			case "iPhone2,1":
				return "iPhone 3GS";
			case "iPhone3,1":
				return "iPhone 4";
			case "iPhone3,2":
				return "iPhone 4";
			case "iPhone3,3":
				return "iPhone 4";
			case "iPhone4,1":
				return "iPhone 4S";
			case "iPhone5,1":
				return "iPhone 5";
			case "iPhone5,2":
				return "iPhone 5";
			case "iPhone5,3":
				return "iPhone 5C";
			case "iPhone5,4":
				return "iPhone 5C";
			case "iPhone6,1":
				return "iPhone 5S";
			case "iPhone6,2":
				return "iPhone 5S";
			case "iPhone7,2":
				return "iPhone 6";
			case "iPhone7,1":
				return "iPhone 6 Plus";
			case "iPod1,1":
				return "iPod Touch 1G";
			case "iPod2,1":
				return "iPod Touch 2G";
			case "iPod3,1":
				return "iPod Touch 3G";
			case "iPod4,1":
				return "iPod Touch 4G";
			case "iPod5,1":
				return "iPod Touch 5G";
			case "iPad1,1":
				return "iPad 1G";
			case "iPad2,1":
				return "iPad 2";
			case "iPad2,2":
				return "iPad 2";
			case "iPad2,3":
				return "iPad 2";
			case "iPad2,4":
				return "iPad 2";
			case "iPad3,1":
				return "iPad 3G";
			case "iPad3,2":
				return "iPad 3G";
			case "iPad3,3":
				return "iPad 3G";
			case "iPad3,4":
				return "iPad 4G";
			case "iPad3,5":
				return "iPad 4G";
			case "iPad3,6":
				return "iPad 4G";
			case "iPad4,1":
				return "iPad Air";
			case "iPad4,2":
				return "iPad Air";
			case "iPad4,3":
				return "iPad Air";
			case "iPad5,3":
				return "iPad Air 2";
			case "iPad5,4":
				return "iPad Air 2";
			case "iPad2,5":
				return "iPad Mini 1G";
			case "iPad2,6":
				return "iPad Mini 1G";
			case "iPad2,7":
				return "iPad Mini 1G";
			case "iPad4,4":
				return "iPad Mini 2G";
			case "iPad4,5":
				return "iPad Mini 2G";
			case "iPad4,6":
				return "iPad Mini 2G";
			case "iPad4,7":
				return "iPad Mini 3";
			case "iPad4,8":
				return "iPad Mini 3";
			case "iPad4,9":
				return "iPad Mini 3";
			case "Amazon KFSAWA":
				return "Fire HDX 8.9 (4th Gen)";
			case "Amazon KFASWI":
				return "Fire HD 7 (4th Gen)";
			case "Amazon KFARWI":
				return "Fire HD 6 (4th Gen)";
			case "Amazon KFAPWA":
			case "Amazon KFAPWI":
				return "Kindle Fire HDX 8.9 (3rd Gen)";
			case "Amazon KFTHWA":
			case "Amazon KFTHWI":
				return "Kindle Fire HDX 7 (3rd Gen)";
			case "Amazon KFSOWI":
				return "Kindle Fire HD 7 (3rd Gen)";
			case "Amazon KFJWA":
			case "Amazon KFJWI":
				return "Kindle Fire HD 8.9 (2nd Gen)";
			case "Amazon KFTT":
				return "Kindle Fire HD 7 (2nd Gen)";
			case "Amazon KFOT":
				return "Kindle Fire (2nd Gen)";
			case "Amazon Kindle Fire":
				return "Kindle Fire (1st Gen)";
			default:
				return text;
			}
		}

		private static string GetDeviceModel()
		{
			return global::UnityEngine.SystemInfo.deviceModel;
		}

		private static string GetDeviceType()
		{
			if (RuntimePlatformIs("SamsungTVPlayer"))
			{
				return "TV";
			}
			switch (global::UnityEngine.SystemInfo.deviceType)
			{
			case global::UnityEngine.DeviceType.Console:
				return "CONSOLE";
			case global::UnityEngine.DeviceType.Desktop:
				return "PC";
			case global::UnityEngine.DeviceType.Handheld:
			{
				string text = global::UnityEngine.SystemInfo.deviceModel;
				if (text.StartsWith("iPhone"))
				{
					return "MOBILE_PHONE";
				}
				if (text.StartsWith("iPad"))
				{
					return "TABLET";
				}
				return (!IsTablet()) ? "MOBILE_PHONE" : "TABLET";
			}
			default:
				return "UNKNOWN";
			}
		}

		private static string GetOperatingSystem()
		{
			string text = global::UnityEngine.SystemInfo.operatingSystem.ToUpper();
			if (text.Contains("WINDOWS"))
			{
				return "WINDOWS";
			}
			if (text.Contains("OSX"))
			{
				return "OSX";
			}
			if (text.Contains("MAC"))
			{
				return "OSX";
			}
			if (text.Contains("IOS") || text.Contains("IPHONE") || text.Contains("IPAD"))
			{
				return "IOS";
			}
			if (text.Contains("LINUX"))
			{
				return "LINUX";
			}
			if (text.Contains("ANDROID"))
			{
				if (global::UnityEngine.SystemInfo.deviceModel.ToUpper().Contains("AMAZON"))
				{
					return "FIREOS";
				}
				return "ANDROID";
			}
			if (text.Contains("BLACKBERRY"))
			{
				return "BLACKBERRY";
			}
			return "UNKNOWN";
		}

		private static string GetOperatingSystemVersion()
		{
			try
			{
				global::System.Text.RegularExpressions.Regex regex = new global::System.Text.RegularExpressions.Regex("[\\d|\\.]+");
				string input = global::UnityEngine.SystemInfo.operatingSystem;
				global::System.Text.RegularExpressions.Match match = regex.Match(input);
				if (match.Success)
				{
					return match.Groups[0].ToString();
				}
				return string.Empty;
			}
			catch (global::System.Exception)
			{
				return null;
			}
		}

		private static string GetManufacturer()
		{
			return null;
		}

		private static string GetCurrentTimezoneOffset()
		{
			try
			{
				global::System.TimeZone currentTimeZone = global::System.TimeZone.CurrentTimeZone;
				global::System.DateTime now = global::System.DateTime.Now;
				global::System.TimeSpan utcOffset = currentTimeZone.GetUtcOffset(now);
				return string.Format("{0}{1:D2}", (utcOffset.Hours < 0) ? string.Empty : "+", utcOffset.Hours);
			}
			catch (global::System.Exception)
			{
				return null;
			}
		}

		private static string GetCountryCode()
		{
			return null;
		}

		private static string GetLanguageCode()
		{
			switch (global::UnityEngine.Application.systemLanguage)
			{
			case global::UnityEngine.SystemLanguage.Afrikaans:
				return "af";
			case global::UnityEngine.SystemLanguage.Arabic:
				return "ar";
			case global::UnityEngine.SystemLanguage.Basque:
				return "eu";
			case global::UnityEngine.SystemLanguage.Belarusian:
				return "be";
			case global::UnityEngine.SystemLanguage.Bulgarian:
				return "bg";
			case global::UnityEngine.SystemLanguage.Catalan:
				return "ca";
			case global::UnityEngine.SystemLanguage.Chinese:
				return "zh";
			case global::UnityEngine.SystemLanguage.Czech:
				return "cs";
			case global::UnityEngine.SystemLanguage.Danish:
				return "da";
			case global::UnityEngine.SystemLanguage.Dutch:
				return "nl";
			case global::UnityEngine.SystemLanguage.English:
				return "en";
			case global::UnityEngine.SystemLanguage.Estonian:
				return "et";
			case global::UnityEngine.SystemLanguage.Faroese:
				return "fo";
			case global::UnityEngine.SystemLanguage.Finnish:
				return "fi";
			case global::UnityEngine.SystemLanguage.French:
				return "fr";
			case global::UnityEngine.SystemLanguage.German:
				return "de";
			case global::UnityEngine.SystemLanguage.Greek:
				return "el";
			case global::UnityEngine.SystemLanguage.Hebrew:
				return "he";
			case global::UnityEngine.SystemLanguage.Hugarian:
				return "hu";
			case global::UnityEngine.SystemLanguage.Icelandic:
				return "is";
			case global::UnityEngine.SystemLanguage.Indonesian:
				return "id";
			case global::UnityEngine.SystemLanguage.Italian:
				return "it";
			case global::UnityEngine.SystemLanguage.Japanese:
				return "ja";
			case global::UnityEngine.SystemLanguage.Korean:
				return "ko";
			case global::UnityEngine.SystemLanguage.Latvian:
				return "lv";
			case global::UnityEngine.SystemLanguage.Lithuanian:
				return "lt";
			case global::UnityEngine.SystemLanguage.Norwegian:
				return "nn";
			case global::UnityEngine.SystemLanguage.Polish:
				return "pl";
			case global::UnityEngine.SystemLanguage.Portuguese:
				return "pt";
			case global::UnityEngine.SystemLanguage.Romanian:
				return "ro";
			case global::UnityEngine.SystemLanguage.Russian:
				return "ru";
			case global::UnityEngine.SystemLanguage.SerboCroatian:
				return "sr";
			case global::UnityEngine.SystemLanguage.Slovak:
				return "sk";
			case global::UnityEngine.SystemLanguage.Slovenian:
				return "sl";
			case global::UnityEngine.SystemLanguage.Spanish:
				return "es";
			case global::UnityEngine.SystemLanguage.Swedish:
				return "sv";
			case global::UnityEngine.SystemLanguage.Thai:
				return "th";
			case global::UnityEngine.SystemLanguage.Turkish:
				return "tr";
			case global::UnityEngine.SystemLanguage.Ukrainian:
				return "uk";
			case global::UnityEngine.SystemLanguage.Vietnamese:
				return "vi";
			default:
				return "en";
			}
		}

		private static string GetLocale()
		{
			if (CountryCode != null)
			{
				return string.Format("{0}_{1}", LanguageCode, CountryCode);
			}
			return string.Format("{0}_ZZ", LanguageCode);
		}
	}
}
