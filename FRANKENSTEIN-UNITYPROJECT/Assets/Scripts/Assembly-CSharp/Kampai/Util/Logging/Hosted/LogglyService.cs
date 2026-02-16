//namespace Kampai.Util.Logging.Hosted
//{
//	public class LogglyService : global::Kampai.Util.Logging.Hosted.ILogglyService
//	{
//		private sealed class ShipAllOnInterval_003Ec__IteratorC0 : global::System.IDisposable, global::System.Collections.IEnumerator, global::System.Collections.Generic.IEnumerator<object>
//		{
//			internal float interval;

//			internal global::Kampai.Util.Logging.Hosted.ILogglyService _003Cthis_003E__0;

//			internal int _0024PC;

//			internal object _0024current;

//			internal float _003C_0024_003Einterval;

//			internal global::Kampai.Util.Logging.Hosted.LogglyService _003C_003Ef__this;

//			object global::System.Collections.Generic.IEnumerator<object>.Current
//			{
//				[global::System.Diagnostics.DebuggerHidden]
//				get
//				{
//					return _0024current;
//				}
//			}

//			object global::System.Collections.IEnumerator.Current
//			{
//				[global::System.Diagnostics.DebuggerHidden]
//				get
//				{
//					return _0024current;
//				}
//			}

//			public bool MoveNext()
//			{
//				uint num = (uint)_0024PC;
//				_0024PC = -1;
//				switch (num)
//				{
//				case 0u:
//					_0024current = new global::UnityEngine.WaitForSeconds(interval);
//					_0024PC = 1;
//					return true;
//				case 1u:
//					if (_003C_003Ef__this.isKillSwitchOn || !_003C_003Ef__this.isLoggingEnabledForUser)
//					{
//						if (_003C_003Ef__this.logBuffer.ByteCount > 0)
//						{
//							_003C_003Ef__this.logBuffer.Clear();
//						}
//					}
//					else if (_003C_003Ef__this.logBuffer.ByteCount > 0)
//					{
//						_003Cthis_003E__0 = _003C_003Ef__this;
//						_003Cthis_003E__0.ShipAll(true);
//					}
//					goto case 0u;
//				default:
//					return false;
//				}
//			}

//			[global::System.Diagnostics.DebuggerHidden]
//			public void Dispose()
//			{
//				_0024PC = -1;
//			}

//			[global::System.Diagnostics.DebuggerHidden]
//			public void Reset()
//			{
//				throw new global::System.NotSupportedException();
//			}
//		}

//		private sealed class Process_003Ec__IteratorC1 : global::System.IDisposable, global::System.Collections.IEnumerator, global::System.Collections.Generic.IEnumerator<object>
//		{
//			internal float interval;

//			internal int _0024PC;

//			internal object _0024current;

//			internal float _003C_0024_003Einterval;

//			internal global::Kampai.Util.Logging.Hosted.LogglyService _003C_003Ef__this;

//			object global::System.Collections.Generic.IEnumerator<object>.Current
//			{
//				[global::System.Diagnostics.DebuggerHidden]
//				get
//				{
//					return _0024current;
//				}
//			}

//			object global::System.Collections.IEnumerator.Current
//			{
//				[global::System.Diagnostics.DebuggerHidden]
//				get
//				{
//					return _0024current;
//				}
//			}

//			public bool MoveNext()
//			{
//				uint num = (uint)_0024PC;
//				_0024PC = -1;
//				switch (num)
//				{
//				case 0u:
//					_0024current = new global::UnityEngine.WaitForSeconds(interval);
//					_0024PC = 1;
//					return true;
//				case 1u:
//					if (_003C_003Ef__this.isKillSwitchOn || !_003C_003Ef__this.isLoggingEnabledForUser)
//					{
//						if (_003C_003Ef__this.logBuffer.ByteCount > 0)
//						{
//							_003C_003Ef__this.logBuffer.Clear();
//						}
//					}
//					else
//					{
//						_003C_003Ef__this.logBuffer.Process(_003C_003Ef__this.BuildDto);
//					}
//					goto case 0u;
//				default:
//					return false;
//				}
//			}

//			[global::System.Diagnostics.DebuggerHidden]
//			public void Dispose()
//			{
//				_0024PC = -1;
//			}

//			[global::System.Diagnostics.DebuggerHidden]
//			public void Reset()
//			{
//				throw new global::System.NotSupportedException();
//			}
//		}

//		private const string HOST = "https://logs-01.loggly.com";

//		private const string TOKEN = "05946d11-d631-48e1-a74e-b344673d86f9";

//		private const string RESOURCE = "/bulk/{0}/tag/client.{1},{2}";

//		private const int RETRY_ATTEMPTS = 1;

//		private const bool DEFAULT_KILL_SWITCH_STATE = false;

//		private const global::Kampai.Util.Logger.Level DEFAULT_LEVEL = global::Kampai.Util.Logger.Level.Debug;

//		private const int DEFAULT_MAX_BUFFER_SIZE_BYTES = 1048576;

//		private const bool DEFAULT_LOGGING_ENABLED_STATE = true;

//		private bool isKillSwitchOn;

//		private global::Kampai.Util.Logger.Level allowedLevel = global::Kampai.Util.Logger.Level.Debug;

//		private bool isLoggingEnabledForUser = true;

//		private bool isBufferFull;

//		private global::Kampai.Game.IConfigurationsService configurationsService;

//		private global::Kampai.Game.IUserSessionService userSessionService;

//		private global::Kampai.Util.Logging.Hosted.ILogglyDtoCache logglyDtoCache;

//		private bool newUser;

//		[Inject]
//		public global::Kampai.Util.Logging.Hosted.ILogBuffer logBuffer { get; set; }

//		[Inject]
//		public global::Kampai.Download.IDownloadService downloadService { get; set; }

//		[Inject("game.server.environment")]
//		public string serverEnvironment { get; set; }

//		[Inject]
//		public global::Kampai.Util.ClientVersion clientVersion { get; set; }

//		void global::Kampai.Util.Logging.Hosted.ILogglyService.Initialize(global::Kampai.Game.IConfigurationsService configurationsService, global::Kampai.Game.IUserSessionService userSessionService, global::Kampai.Util.Logging.Hosted.ILogglyDtoCache logglyDtoCache, ILocalPersistanceService localPersistService)
//		{
//			this.configurationsService = configurationsService;
//			this.userSessionService = userSessionService;
//			this.logglyDtoCache = logglyDtoCache;
//			string data = localPersistService.GetData("UserID");
//			newUser = string.IsNullOrEmpty(data);
//			this.logglyDtoCache.RefreshClientVersionValues();
//			this.logglyDtoCache.RefreshConfigurationValues();
//			this.logglyDtoCache.RefreshUserSessionValues();
//		}

//		[global::System.Diagnostics.DebuggerHidden]
//		global::System.Collections.IEnumerator global::Kampai.Util.Logging.Hosted.ILogglyService.ShipAllOnInterval(float interval)
//		{
//			global::Kampai.Util.Logging.Hosted.LogglyService.ShipAllOnInterval_003Ec__IteratorC0 shipAllOnInterval_003Ec__IteratorC = new global::Kampai.Util.Logging.Hosted.LogglyService.ShipAllOnInterval_003Ec__IteratorC0();
//			shipAllOnInterval_003Ec__IteratorC.interval = interval;
//			shipAllOnInterval_003Ec__IteratorC._003C_0024_003Einterval = interval;
//			shipAllOnInterval_003Ec__IteratorC._003C_003Ef__this = this;
//			return shipAllOnInterval_003Ec__IteratorC;
//		}

//		[global::System.Diagnostics.DebuggerHidden]
//		global::System.Collections.IEnumerator global::Kampai.Util.Logging.Hosted.ILogglyService.Process(float interval)
//		{
//			global::Kampai.Util.Logging.Hosted.LogglyService.Process_003Ec__IteratorC1 process_003Ec__IteratorC = new global::Kampai.Util.Logging.Hosted.LogglyService.Process_003Ec__IteratorC1();
//			process_003Ec__IteratorC.interval = interval;
//			process_003Ec__IteratorC._003C_0024_003Einterval = interval;
//			process_003Ec__IteratorC._003C_003Ef__this = this;
//			return process_003Ec__IteratorC;
//		}

//		void global::Kampai.Util.Logging.Hosted.ILogglyService.Log(global::Kampai.Util.Logger.Level level, string text, bool isFatal)
//		{
//			if (!isKillSwitchOn && isLoggingEnabledForUser && IsAllowedLevel(level) && !isBufferFull)
//			{
//				if (isFatal)
//				{
//					global::Kampai.Util.Logging.Hosted.Log log = new global::Kampai.Util.Logging.Hosted.Log();
//					log.Level = level;
//					log.Text = text;
//					global::Kampai.Util.Logging.Hosted.Log log2 = log;
//					logBuffer.Add(log2);
//					((global::Kampai.Util.Logging.Hosted.ILogglyService)this).ShipAll(true);
//				}
//				else
//				{
//					global::Kampai.Util.Logging.Hosted.Log log = new global::Kampai.Util.Logging.Hosted.Log();
//					log.Level = level;
//					log.Text = text;
//					global::Kampai.Util.Logging.Hosted.Log log3 = log;
//					logBuffer.Add(log3);
//				}
//			}
//		}

//		void global::Kampai.Util.Logging.Hosted.ILogglyService.ShipAll(bool forceRequest)
//		{
//			if (!isKillSwitchOn && isLoggingEnabledForUser)
//			{
//				global::Kampai.Util.Logging.Hosted.ProcessedLogs processedLogsAndClear = logBuffer.GetProcessedLogsAndClear(BuildDto);
//				if (isBufferFull)
//				{
//					global::Kampai.Util.Logging.Hosted.Log log = new global::Kampai.Util.Logging.Hosted.Log();
//					log.Level = global::Kampai.Util.Logger.Level.Error;
//					log.Text = "Log buffer was full!  Logs were discarded.";
//					global::Kampai.Util.Logging.Hosted.Log log2 = log;
//					global::Kampai.Util.Logging.Hosted.LogglyDto value = BuildDto(log2);
//					string text = global::Newtonsoft.Json.JsonConvert.SerializeObject(value);
//					processedLogsAndClear.Json = processedLogsAndClear.Json + text + global::System.Environment.NewLine;
//				}
//				isBufferFull = false;
//				global::Kampai.Util.Native.LogInfo(string.Format("Preparing to ship {0} log{1} to Loggly...", processedLogsAndClear.Count, (processedLogsAndClear.Count == 1) ? string.Empty : "s"));
//				SendRequest(processedLogsAndClear.Json, forceRequest);
//			}
//		}

//		void global::Kampai.Util.Logging.Hosted.ILogglyService.OnConfigurationsLoaded(bool init)
//		{
//			if (logglyDtoCache != null)
//			{
//				logglyDtoCache.RefreshConfigurationValues();
//			}
//			SetLogLevel();
//		}

//		void global::Kampai.Util.Logging.Hosted.ILogglyService.OnUserSessionGranted()
//		{
//			if (logglyDtoCache != null)
//			{
//				logglyDtoCache.RefreshUserSessionValues();
//			}
//			isLoggingEnabledForUser = IsLoggingEnabledForUser();
//			SetLogLevelOverride();
//			global::Kampai.Util.Native.LogInfo(string.Format("Loggly service is {0}.", (!isLoggingEnabledForUser) ? "DISABLED" : "ENABLED"));
//		}

//		void global::Kampai.Util.Logging.Hosted.ILogglyService.UpdateKillSwitch()
//		{
//			if (configurationsService != null)
//			{
//				isKillSwitchOn = configurationsService.isKillSwitchOn(global::Kampai.Game.KillSwitch.LOGGLY);
//				global::Kampai.Util.Native.LogInfo(string.Format("Loggly killswitch updated.  Killswitch is {0}.", (!isKillSwitchOn) ? "OFF" : "ON"));
//			}
//		}

//		[PostConstruct]
//		public void Initialize()
//		{
//			logBuffer.SetMaxByteCount(1048576);
//			logBuffer.BufferFull += OnBufferFull;
//		}

//		private void SendRequest(string json, bool forceRequest)
//		{
//			string uri = string.Format("https://logs-01.loggly.com/bulk/{0}/tag/client.{1},{2}", "05946d11-d631-48e1-a74e-b344673d86f9", serverEnvironment, clientVersion.GetClientPlatform());
//			byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(json);
//			global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
//			signal.AddListener(RequestComplete);
//			global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(uri).WithContentType("application/json").WithMethod(global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest.METHOD_POST).WithBody(bytes)
//				.WithResponseSignal(signal)
//				.WithRetry(true, 1);
//			downloadService.Perform(request, forceRequest);
//		}

//		private void RequestComplete(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
//		{
//			if (response.Success)
//			{
//				global::Kampai.Util.Native.LogInfo("Loggly request completed successfully.");
//			}
//			else
//			{
//				global::Kampai.Util.Native.LogError("Loggly request failed.");
//			}
//		}

//		private void SetLogLevel()
//		{
//			if (configurationsService != null)
//			{
//				global::Kampai.Game.ConfigurationDefinition configurations = configurationsService.GetConfigurations();
//				if (configurations != null)
//				{
//					allowedLevel = (global::Kampai.Util.Logger.Level)configurations.logLevel;
//					global::Kampai.Util.Logging.Hosted.LogglyConfiguration logglyConfig = configurations.logglyConfig;
//					if (logglyConfig != null)
//					{
//						allowedLevel = (global::Kampai.Util.Logger.Level)logglyConfig.logLevel;
//					}
//				}
//			}
//			SetLogLevelOverride();
//			global::Kampai.Util.Native.LogInfo(string.Format("Loggly log level set to {0}", allowedLevel));
//		}

//		private void SetLogLevelOverride()
//		{
//			if (userSessionService != null)
//			{
//				global::Kampai.Game.UserSession userSession = userSessionService.UserSession;
//				if (userSession != null && userSession.LogEnabled)
//				{
//					global::Kampai.Util.Logger.Level logLevel = (global::Kampai.Util.Logger.Level)userSession.LogLevel;
//					allowedLevel = logLevel;
//					global::Kampai.Util.Native.LogInfo(string.Format("Loggly log level override set to {0} for this user.", allowedLevel));
//				}
//			}
//		}

//		private bool IsAllowedLevel(global::Kampai.Util.Logger.Level level)
//		{
//			return allowedLevel <= level;
//		}

//		private global::Kampai.Util.Logging.Hosted.LogglyDto BuildDto(global::Kampai.Util.Logging.Hosted.Log log)
//		{
//			global::Kampai.Util.Logging.Hosted.LogglyDto logglyDto = new global::Kampai.Util.Logging.Hosted.LogglyDto();
//			if (logglyDtoCache != null)
//			{
//				logglyDto = logglyDtoCache.GetCachedDto();
//			}
//			logglyDto.NewUser = newUser.ToString();
//			logglyDto.LogLevel = log.Level.ToString();
//			logglyDto.Message = log.Text;
//			logglyDto.Timestamp = global::Kampai.Util.Logging.Hosted.LogglyUtil.FormatDateTimeISO(global::System.DateTime.Now);
//			logglyDto.Geolocation = string.Empty;
//			return logglyDto;
//		}

//		private bool IsLoggingEnabledForUser()
//		{
//			if (configurationsService != null && userSessionService != null)
//			{
//				return IsUserInSample() || IsLoggingOverrideEnabled();
//			}
//			return false;
//		}

//		private bool IsUserInSample()
//		{
//			global::Kampai.Game.ConfigurationDefinition configurations = configurationsService.GetConfigurations();
//			if (configurations != null)
//			{
//				global::Kampai.Util.Logging.Hosted.LogglyConfiguration logglyConfig = configurations.logglyConfig;
//				if (logglyConfig != null)
//				{
//					bool flag = global::Kampai.Util.ServiceSampleUtil.IsServiceEnabled(global::Kampai.Util.ServiceSampleUtil.ServiceType.Loggly, logglyConfig.samplePercentage, userSessionService);
//					global::Kampai.Util.Native.LogInfo(string.Format("User {0} is part of Loggly sample group.  Percentage={1}", (!flag) ? "is not" : "is", logglyConfig.samplePercentage));
//					return flag;
//				}
//			}
//			return false;
//		}

//		private bool IsLoggingOverrideEnabled()
//		{
//			global::Kampai.Game.UserSession userSession = userSessionService.UserSession;
//			if (userSession != null)
//			{
//				bool logEnabled = userSession.LogEnabled;
//				global::Kampai.Util.Native.LogInfo(string.Format("Loggly override is {0} for this user.", (!logEnabled) ? "disabled" : "enabled"));
//				return logEnabled;
//			}
//			return false;
//		}

//		private void OnBufferFull(object sender, global::System.EventArgs args)
//		{
//			isBufferFull = true;
//		}
//	}
//}
using Kampai.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kampai.Util.Logging.Hosted
{
    public class LogglyService : ILogglyService
    {
        // --- REQUIRED PROPERTIES FOR REFLECTION (To fix CS1061 errors) ---
        // Even if we don't use them, the PreReflected.cs file looks for them.

        [Inject]
        public ILogBuffer logBuffer { get; set; }

        [Inject]
        public Kampai.Download.IDownloadService downloadService { get; set; }

        [Inject("game.server.environment")]
        public string serverEnvironment { get; set; }

        [Inject]
        public ClientVersion clientVersion { get; set; }

        // --- INTERFACE PROPERTIES ---
        public string Url { get; set; }
        public string Key { get; set; }
        public string Tag { get; set; }

        public LogglyService()
        {
        }

        // --- FIX FOR CS1501 (Method Signature Mismatch) ---
        // This matches the signature expected by PreReflected.cs
        [PostConstruct]
        public void Initialize()
        {
            // Do nothing, just satisfy the compiler
        }

        // --- INTERFACE IMPLEMENTATION (All Stubbed Out) ---

        public void Initialize(IConfigurationsService configurationsService, IUserSessionService userSessionService, ILogglyDtoCache logglyDtoCache, ILocalPersistanceService localPersistService)
        {
            // Do nothing
        }

        public void Init(string url, string key, string tag)
        {
            return;
        }

        public void Send(LogglyDto dto)
        {
            return;
        }

        public void Send(List<LogglyDto> dtos)
        {
            return;
        }

        public void Log(Logger.Level level, string text, bool isFatal)
        {
            return;
        }

        public IEnumerator ShipAllOnInterval(float interval)
        {
            // Return null or empty enumeration to satisfy coroutine
            yield break;
        }

        public IEnumerator Process(float interval)
        {
            yield break;
        }

        public void ShipAll(bool forceRequest)
        {
            return;
        }

        public void OnConfigurationsLoaded(bool init)
        {
            return;
        }

        public void OnUserSessionGranted()
        {
            return;
        }

        public void UpdateKillSwitch()
        {
            return;
        }
    }
}