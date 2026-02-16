namespace Kampai.Util.Logging.Hosted
{
	public class LogglyDtoCache : global::Kampai.Util.Logging.Hosted.ILogglyDtoCache
	{
		public enum DtoProperty
		{
			ClientVersion = 0,
			ClientDeviceType = 1,
			ClientPlatform = 2,
			ConfigUrl = 3,
			ConfigVariant = 4,
			DefinitionVariants = 5,
			DefinitionId = 6,
			UserId = 7,
			SynergyId = 8
		}

		private const string DEFAULT_VALUE = "unknown";

		private bool _initialized;

		private global::System.Collections.Generic.Dictionary<global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty, global::Kampai.Util.Logging.Hosted.CachedValue<string>> cache;

		[Inject]
		public global::Kampai.Game.IUserSessionService userSessionService { get; set; }

		[Inject]
		public global::Kampai.Game.IConfigurationsService configurationsService { get; set; }

		[Inject]
		public global::Kampai.Util.ClientVersion clientVersion { get; set; }

		global::Kampai.Util.Logging.Hosted.LogglyDto global::Kampai.Util.Logging.Hosted.ILogglyDtoCache.GetCachedDto()
		{
			global::Kampai.Util.Logging.Hosted.LogglyDto logglyDto = new global::Kampai.Util.Logging.Hosted.LogglyDto();
			logglyDto.ClientVersion = cache[global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.ClientVersion].GetValue() ?? "unknown";
			logglyDto.ClientDeviceType = cache[global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.ClientDeviceType].GetValue() ?? "unknown";
			logglyDto.ClientPlatform = cache[global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.ClientPlatform].GetValue() ?? "unknown";
			logglyDto.UserId = cache[global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.UserId].GetValue() ?? "unknown";
			logglyDto.SynergyId = cache[global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.SynergyId].GetValue() ?? "unknown";
			logglyDto.ConfigUrl = cache[global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.ConfigUrl].GetValue() ?? "unknown";
			logglyDto.ConfigVariant = cache[global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.ConfigVariant].GetValue() ?? "unknown";
			logglyDto.DefinitionId = cache[global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.DefinitionId].GetValue() ?? "unknown";
			logglyDto.DefinitionVariants = GetDefinitionVariants().GetValue() ?? new string[1] { "unknown" };
			return logglyDto;
		}

		void global::Kampai.Util.Logging.Hosted.ILogglyDtoCache.RefreshConfigurationValues()
		{
			if (_initialized)
			{
				cache[global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.ConfigUrl].SetFresh(false);
				cache[global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.ConfigVariant].SetFresh(false);
				cache[global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.DefinitionVariants].SetFresh(false);
				cache[global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.DefinitionId].SetFresh(false);
			}
		}

		void global::Kampai.Util.Logging.Hosted.ILogglyDtoCache.RefreshUserSessionValues()
		{
			if (_initialized)
			{
				cache[global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.UserId].SetFresh(false);
				cache[global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.SynergyId].SetFresh(false);
			}
		}

		void global::Kampai.Util.Logging.Hosted.ILogglyDtoCache.RefreshClientVersionValues()
		{
			if (_initialized)
			{
				cache[global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.ClientVersion].SetFresh(false);
				cache[global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.ClientDeviceType].SetFresh(false);
				cache[global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.ClientPlatform].SetFresh(false);
			}
		}

		[PostConstruct]
		public void Initialize()
		{
			cache = new global::System.Collections.Generic.Dictionary<global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty, global::Kampai.Util.Logging.Hosted.CachedValue<string>>();
			cache.Add(global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.ClientVersion, new global::Kampai.Util.Logging.Hosted.CachedValue<string>(() => clientVersion.GetClientVersion()));
			cache.Add(global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.ClientDeviceType, new global::Kampai.Util.Logging.Hosted.CachedValue<string>(() => clientVersion.GetClientDeviceType()));
			cache.Add(global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.ClientPlatform, new global::Kampai.Util.Logging.Hosted.CachedValue<string>(() => clientVersion.GetClientPlatform()));
			cache.Add(global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.ConfigUrl, new global::Kampai.Util.Logging.Hosted.CachedValue<string>(() => configurationsService.GetConfigURL()));
			cache.Add(global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.ConfigVariant, new global::Kampai.Util.Logging.Hosted.CachedValue<string>(() => configurationsService.GetConfigVariant()));
			cache.Add(global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.DefinitionVariants, new global::Kampai.Util.Logging.Hosted.CachedValue<string>(() => configurationsService.GetDefinitionVariants()));
			global::System.Func<string> valueSetter = delegate
			{
				global::Kampai.Game.ConfigurationDefinition configurations = configurationsService.GetConfigurations();
				return (configurations != null) ? configurations.definitionId : null;
			};
			cache.Add(global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.DefinitionId, new global::Kampai.Util.Logging.Hosted.CachedValue<string>(valueSetter));
			global::System.Func<string> valueSetter2 = delegate
			{
				global::Kampai.Game.UserSession userSession = userSessionService.UserSession;
				return (userSession != null) ? userSession.UserID : null;
			};
			cache.Add(global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.UserId, new global::Kampai.Util.Logging.Hosted.CachedValue<string>(valueSetter2));
			global::System.Func<string> valueSetter3 = delegate
			{
				global::Kampai.Game.UserSession userSession = userSessionService.UserSession;
				return (userSession != null) ? userSession.SynergyID : null;
			};
			cache.Add(global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.SynergyId, new global::Kampai.Util.Logging.Hosted.CachedValue<string>(valueSetter3));
			_initialized = true;
		}

		private global::Kampai.Util.Logging.Hosted.CachedValue<global::System.Collections.Generic.IList<string>> GetDefinitionVariants()
		{
			global::System.Func<global::System.Collections.Generic.IList<string>> valueSetter = delegate
			{
				string value = cache[global::Kampai.Util.Logging.Hosted.LogglyDtoCache.DtoProperty.DefinitionVariants].GetValue();
				return (value != null) ? value.Split(new char[1] { '_' }, global::System.StringSplitOptions.RemoveEmptyEntries) : null;
			};
			return new global::Kampai.Util.Logging.Hosted.CachedValue<global::System.Collections.Generic.IList<string>>(valueSetter);
		}
	}
}
