namespace Kampai.Util.Logging.Hosted
{
	public interface ILogglyService
	{
		void Log(global::Kampai.Util.Logger.Level level, string text, bool isFatal);

		global::System.Collections.IEnumerator ShipAllOnInterval(float interval);

		global::System.Collections.IEnumerator Process(float interval);

		void ShipAll(bool forceRequest);

		void OnConfigurationsLoaded(bool init);

		void OnUserSessionGranted();

		void UpdateKillSwitch();

		void Initialize(global::Kampai.Game.IConfigurationsService configurationsService, global::Kampai.Game.IUserSessionService userSessionService, global::Kampai.Util.Logging.Hosted.ILogglyDtoCache logglyDtoCache, ILocalPersistanceService localPersistService);
	}
}
