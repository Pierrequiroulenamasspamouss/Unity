namespace DeltaDNA
{
	public static class Logger
	{
		public enum Level
		{
			DEBUG = 0,
			INFO = 1,
			WARNING = 2,
			ERROR = 3
		}

		private static global::DeltaDNA.Logger.Level sLogLevel = global::DeltaDNA.Logger.Level.WARNING;

		internal static void SetLogLevel(global::DeltaDNA.Logger.Level logLevel)
		{
			sLogLevel = logLevel;
		}

		internal static void LogDebug(string msg)
		{
			if (sLogLevel <= global::DeltaDNA.Logger.Level.DEBUG)
			{
				Log(msg, global::DeltaDNA.Logger.Level.DEBUG);
			}
		}

		internal static void LogInfo(string msg)
		{
			if (sLogLevel <= global::DeltaDNA.Logger.Level.INFO)
			{
				Log(msg, global::DeltaDNA.Logger.Level.INFO);
			}
		}

		internal static void LogWarning(string msg)
		{
			if (sLogLevel <= global::DeltaDNA.Logger.Level.WARNING)
			{
				Log(msg, global::DeltaDNA.Logger.Level.WARNING);
			}
		}

		internal static void LogError(string msg)
		{
			if (sLogLevel <= global::DeltaDNA.Logger.Level.ERROR)
			{
				Log(msg, global::DeltaDNA.Logger.Level.ERROR);
			}
		}

		private static void Log(string msg, global::DeltaDNA.Logger.Level level)
		{
			string text = "[DDSDK] ";
			switch (level)
			{
			case global::DeltaDNA.Logger.Level.ERROR:
				global::UnityEngine.Debug.LogError(text + msg);
				break;
			case global::DeltaDNA.Logger.Level.WARNING:
				global::UnityEngine.Debug.LogWarning(text + msg);
				break;
			default:
				global::UnityEngine.Debug.Log(text + msg);
				break;
			}
		}
	}
}
