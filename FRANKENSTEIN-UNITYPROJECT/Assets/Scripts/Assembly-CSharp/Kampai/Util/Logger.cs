namespace Kampai.Util
{
	public class Logger : global::Kampai.Util.ILogger
	{
		public enum Level
		{
			Verbose = 0,
			Debug = 1,
			Info = 2,
			Warning = 3,
			Error = 4,
			None = 5
		}

		private global::Kampai.Util.Logger.Level allowedLevel;

		[Inject]
		public global::Kampai.Download.IDownloadService downloadService { get; set; }

		[Inject]
		public global::Kampai.UI.View.LogToScreenSignal logToScreenSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject(global::Kampai.Util.BaseElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable baseContext { get; set; }

		public virtual bool IsAllowedLevel(global::Kampai.Util.Logger.Level level)
		{
			return allowedLevel <= level;
		}

		public virtual void SetAllowedLevel(int level)
		{
			allowedLevel = (global::Kampai.Util.Logger.Level)level;
			Log(global::Kampai.Util.Logger.Level.Info, "Set log level: {0}", allowedLevel);
		}

		public virtual void Log(global::Kampai.Util.Logger.Level level, string format, params object[] args)
		{
			string text = string.Format(format, args);
			LogIt(level, text);
		}

		public virtual void Log(global::Kampai.Util.Logger.Level level, string text)
		{
			LogIt(level, text);
		}

		public virtual void Log(global::Kampai.Util.Logger.Level level, bool toScreen, string text)
		{
			if (toScreen)
			{
				logToScreenSignal.Dispatch(text);
			}
			LogIt(level, text);
		}

		public virtual void LogNullArgument()
		{
			Log(global::Kampai.Util.Logger.Level.Error, "Null arguments");
		}

		public virtual void Verbose(string text)
		{
			Log(global::Kampai.Util.Logger.Level.Verbose, text);
		}

		public virtual void Verbose(string format, params object[] args)
		{
			Log(global::Kampai.Util.Logger.Level.Verbose, format, args);
		}

		public virtual void Debug(string text)
		{
			Log(global::Kampai.Util.Logger.Level.Debug, text);
		}

		public virtual void Debug(string format, params object[] args)
		{
			Log(global::Kampai.Util.Logger.Level.Debug, format, args);
		}

		public virtual void Info(string text)
		{
			Log(global::Kampai.Util.Logger.Level.Info, text);
		}

		public virtual void Info(string format, params object[] args)
		{
			Log(global::Kampai.Util.Logger.Level.Info, format, args);
		}

		public virtual void Warning(string text)
		{
			Log(global::Kampai.Util.Logger.Level.Warning, text);
		}

		public virtual void Warning(string format, params object[] args)
		{
			Log(global::Kampai.Util.Logger.Level.Warning, format, args);
		}

		public virtual void Error(string text)
		{
			Log(global::Kampai.Util.Logger.Level.Error, text);
		}

		public virtual void Error(string format, params object[] args)
		{
			Log(global::Kampai.Util.Logger.Level.Error, format, args);
		}

		public void EventStart(string eventName)
		{
			LogIt(global::Kampai.Util.Logger.Level.Debug, string.Format("EventStart: {0}", eventName));
		}

		public void EventStop(string eventName)
		{
			LogIt(global::Kampai.Util.Logger.Level.Debug, string.Format("EventStop: {0}", eventName));
		}

		public void LogEventList()
		{
		}

		protected virtual void LogIt(global::Kampai.Util.Logger.Level level, string text, bool isFatal = false)
		{
			if (!string.IsNullOrEmpty(text) && text.Contains("License error. This plugin is only supported in Unity Pro!")) return;

			if (IsAllowedLevel(level))
			{
				switch (level)
				{
				case global::Kampai.Util.Logger.Level.Info:
					global::Kampai.Util.Native.LogInfo(text);
					break;
				case global::Kampai.Util.Logger.Level.Debug:
					global::Kampai.Util.Native.LogDebug(text);
					break;
				case global::Kampai.Util.Logger.Level.Warning:
					global::Kampai.Util.Native.LogWarning(text);
					break;
				case global::Kampai.Util.Logger.Level.Error:
					global::Kampai.Util.Native.LogError(text);
					break;
				default:
					global::Kampai.Util.Native.LogVerbose(text);
					break;
				}
			}
		}

		public void Fatal(global::Kampai.Util.FatalCode code, string format, params object[] args)
		{
			Fatal(code, 0, format, args);
		}

		public void FatalNoThrow(global::Kampai.Util.FatalCode code, string format, params object[] args)
		{
			FatalNoThrow(code, 0, format, args);
		}

		public virtual void FatalNoThrow(global::Kampai.Util.FatalCode code, int referencedId, string format, params object[] args)
		{
			string text = string.Format("[ERROR {0}-{1}] {2}", (int)code, referencedId, string.Format(format, args));
			string text2 = new global::System.Diagnostics.StackTrace(1, true).ToString();
			LogIt(global::Kampai.Util.Logger.Level.Error, text, true);
			LogIt(global::Kampai.Util.Logger.Level.Error, text2, true);
			if (downloadService != null)
			{
				downloadService.Shutdown();
			}
			string code2 = string.Format("{0}-{1}", (int)code, referencedId);
			string title;
			string message;
			if (global::Kampai.Util.GracefulErrors.IsGracefulError(code))
			{
				global::Kampai.Util.GracefulMessage gracefulError = global::Kampai.Util.GracefulErrors.GetGracefulError(code);
				title = localService.GetString(gracefulError.Title);
				message = localService.GetString(gracefulError.Description, args);
			}
			else if (code.IsNetworkError())
			{
				title = string.Format(localService.GetString("FatalNetworkTitle"));
				message = localService.GetString("FatalNetworkMessage");
			}
			else
			{
				title = string.Format(localService.GetString("FatalTitle"));
				message = localService.GetString("FatalMessage");
			}
			global::Kampai.Util.FatalView.SetFatalText(code2, message, title);
			baseContext.injectionBinder.GetInstance<global::Kampai.Main.LoadNewGameLevelSignal>().Dispatch("Fatal");
		}

		public void Fatal(global::Kampai.Util.FatalCode code, int referencedId, string format, params object[] args)
		{
			FatalNoThrow(code, referencedId, format, args);
			throw new global::Kampai.Util.FatalInvocationException();
		}

		public void FatalNullArgument(global::Kampai.Util.FatalCode code)
		{
			Fatal(code, "Null argument");
		}

		public void Fatal(global::Kampai.Util.FatalCode code)
		{
			Fatal(code, code.ToString());
		}

		public void FatalNoThrow(global::Kampai.Util.FatalCode code)
		{
			FatalNoThrow(code, 0, code.ToString());
		}

		public void Fatal(global::Kampai.Util.FatalCode code, int referencedId)
		{
			Fatal(code, referencedId, string.Empty);
		}

		public void FatalNoThrow(global::Kampai.Util.FatalCode code, int referencedId)
		{
			FatalNoThrow(code, referencedId, string.Empty);
		}
	}
}
