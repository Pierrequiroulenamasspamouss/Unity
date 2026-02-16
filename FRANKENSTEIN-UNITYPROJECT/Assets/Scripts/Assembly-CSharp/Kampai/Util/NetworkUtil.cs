namespace Kampai.Util
{
	public static class NetworkUtil
	{
		private const string MISC_UTILS_CLASS = "com.ea.gp.minions.utils.Misc";

		public static bool IsConnected()
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			using (global::UnityEngine.AndroidJavaClass androidJavaClass = new global::UnityEngine.AndroidJavaClass("com.ea.gp.minions.utils.Misc"))
			{
				return androidJavaClass.CallStatic<bool>("isConnected", new object[0]);
            #else
            // PC/Mac Fix: Ask Unity if internet is reachable
            return global::UnityEngine.Application.internetReachability != global::UnityEngine.NetworkReachability.NotReachable;
#endif
     
		}

        //public static bool IsNetworkWiFi()
        //{
        //	bool flag = false;
        //	using (global::UnityEngine.AndroidJavaClass androidJavaClass = new global::UnityEngine.AndroidJavaClass("com.ea.gp.minions.utils.Misc"))
        //	{
        //		return androidJavaClass.CallStatic<bool>("isNetworkWiFi", new object[0]);
        //	}
        //}
        public static bool IsNetworkWiFi()
        {
            // FIX MOCK SERVER : On force "Vrai" pour éviter le crash JNI sur PC
            return true;
        }

        public static global::UnityEngine.NetworkReachability GetNetworkReachability()
		{
			return global::UnityEngine.Application.internetReachability;
		}
	}
}

