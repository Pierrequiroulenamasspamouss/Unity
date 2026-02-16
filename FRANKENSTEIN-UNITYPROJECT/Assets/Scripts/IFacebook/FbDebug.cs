public static class FbDebug
{
	public static void Log(string msg)
	{
		if (global::UnityEngine.Debug.isDebugBuild)
		{
			if (global::UnityEngine.Application.isWebPlayer)
			{
				global::UnityEngine.Application.ExternalCall("console.log", msg);
			}
			global::UnityEngine.Debug.Log(msg);
		}
	}

	public static void Info(string msg)
	{
		if (global::UnityEngine.Application.isWebPlayer)
		{
			global::UnityEngine.Application.ExternalCall("console.info", msg);
		}
		global::UnityEngine.Debug.Log(msg);
	}

	public static void Warn(string msg)
	{
		if (global::UnityEngine.Application.isWebPlayer)
		{
			global::UnityEngine.Application.ExternalCall("console.warn", msg);
		}
		global::UnityEngine.Debug.LogWarning(msg);
	}

	public static void Error(string msg)
	{
		if (global::UnityEngine.Application.isWebPlayer)
		{
			global::UnityEngine.Application.ExternalCall("console.error", msg);
		}
		global::UnityEngine.Debug.LogError(msg);
	}
}
