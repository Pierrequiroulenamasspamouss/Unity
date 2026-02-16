namespace Kampai.Util
{
	public class TestLogger : global::Kampai.Util.ILogger
	{
		public bool OutputToDebugLog;

		public global::Kampai.Util.TestLogger EnableOutput()
		{
			OutputToDebugLog = true;
			return this;
		}

		public void Log(global::Kampai.Util.Logger.Level level, string format, params object[] args)
		{
			if (OutputToDebugLog)
			{
				global::UnityEngine.Debug.Log(level.ToString() + ": " + string.Format(format, args));
			}
		}

		public void Log(global::Kampai.Util.Logger.Level level, string text)
		{
			if (OutputToDebugLog)
			{
				global::UnityEngine.Debug.Log(level.ToString() + ": " + text);
			}
		}

		public void Log(global::Kampai.Util.Logger.Level level, bool toScreen, string text)
		{
			Log(level, text);
		}

		public void LogNullArgument()
		{
			Log(global::Kampai.Util.Logger.Level.Error, "Null argument");
		}

		public void Verbose(string text)
		{
			Log(global::Kampai.Util.Logger.Level.Verbose, text);
		}

		public void Verbose(string format, params object[] args)
		{
			Log(global::Kampai.Util.Logger.Level.Verbose, format, args);
		}

		public void Debug(string text)
		{
			Log(global::Kampai.Util.Logger.Level.Debug, text);
		}

		public void Debug(string format, params object[] args)
		{
			Log(global::Kampai.Util.Logger.Level.Debug, format, args);
		}

		public void Info(string text)
		{
			Log(global::Kampai.Util.Logger.Level.Info, text);
		}

		public void Info(string format, params object[] args)
		{
			Log(global::Kampai.Util.Logger.Level.Info, format, args);
		}

		public void Warning(string text)
		{
			Log(global::Kampai.Util.Logger.Level.Warning, text);
		}

		public void Warning(string format, params object[] args)
		{
			Log(global::Kampai.Util.Logger.Level.Warning, format, args);
		}

		public void Error(string text)
		{
			Log(global::Kampai.Util.Logger.Level.Error, text);
		}

		public void Error(string format, params object[] args)
		{
			Log(global::Kampai.Util.Logger.Level.Error, format, args);
		}

		public void Fatal(global::Kampai.Util.FatalCode code, string format, params object[] args)
		{
			Log(global::Kampai.Util.Logger.Level.Error, "FATAL " + code.ToString() + string.Format(format, args));
		}

		public void Fatal(global::Kampai.Util.FatalCode code, int referenceId, string format, params object[] args)
		{
			Log(global::Kampai.Util.Logger.Level.Error, "FATAL " + code.ToString() + " - " + referenceId + " " + string.Format(format, args));
		}

		public void FatalNullArgument(global::Kampai.Util.FatalCode code)
		{
			Log(global::Kampai.Util.Logger.Level.Error, "FATAL " + code.ToString() + " Null argument");
		}

		public void Fatal(global::Kampai.Util.FatalCode code)
		{
			Log(global::Kampai.Util.Logger.Level.Error, "FATAL " + code);
		}

		public void FatalNoThrow(global::Kampai.Util.FatalCode code)
		{
			Fatal(code);
		}

		public void FatalNoThrow(global::Kampai.Util.FatalCode code, int referencedId)
		{
			Fatal(code, referencedId);
		}

		public void FatalNoThrow(global::Kampai.Util.FatalCode code, string format, params object[] args)
		{
			Fatal(code, format, args);
		}

		public void FatalNoThrow(global::Kampai.Util.FatalCode code, int referencedId, string format, params object[] args)
		{
			Fatal(code, referencedId, format, args);
		}

		public void SetAllowedLevel(int level)
		{
		}

		public bool IsAllowedLevel(global::Kampai.Util.Logger.Level level)
		{
			return true;
		}

		public virtual void Fatal(global::Kampai.Util.FatalCode code, int referencedId)
		{
			Log(global::Kampai.Util.Logger.Level.Error, "FATAL " + code.ToString() + ":" + referencedId);
		}

		public void EventStart(string eventName)
		{
		}

		public void EventStop(string eventName)
		{
		}

		public void LogEventList()
		{
		}
	}
}
