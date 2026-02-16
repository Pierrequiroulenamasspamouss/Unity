namespace Kampai.Util.Logging.Hosted
{
	public interface ILogglyDtoCache
	{
		global::Kampai.Util.Logging.Hosted.LogglyDto GetCachedDto();

		void RefreshConfigurationValues();

		void RefreshUserSessionValues();

		void RefreshClientVersionValues();
	}
}
