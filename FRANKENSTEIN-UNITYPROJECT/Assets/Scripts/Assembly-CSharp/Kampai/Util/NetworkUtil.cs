namespace Kampai.Util
{
    public static class NetworkUtil
    {
        private const string MISC_UTILS_CLASS = "com.ea.gp.minions.utils.Misc";

        public static bool IsConnected()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            using (global::UnityEngine.AndroidJavaClass androidJavaClass =
                new global::UnityEngine.AndroidJavaClass(MISC_UTILS_CLASS))
            {
                return androidJavaClass.CallStatic<bool>("isConnected");
            }
#else
            // PC / Mac / Editor fallback
            return global::UnityEngine.Application.internetReachability
                != global::UnityEngine.NetworkReachability.NotReachable;
#endif
        }

        public static bool IsNetworkWiFi()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            using (global::UnityEngine.AndroidJavaClass androidJavaClass =
                new global::UnityEngine.AndroidJavaClass(MISC_UTILS_CLASS))
            {
                return androidJavaClass.CallStatic<bool>("isNetworkWiFi");
            }
#else
            // Non-Android fallback (no JNI crash)
            return global::UnityEngine.Application.internetReachability
                == global::UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork;
#endif
        }

        public static global::UnityEngine.NetworkReachability GetNetworkReachability()
        {
            return global::UnityEngine.Application.internetReachability;
        }
    }
}