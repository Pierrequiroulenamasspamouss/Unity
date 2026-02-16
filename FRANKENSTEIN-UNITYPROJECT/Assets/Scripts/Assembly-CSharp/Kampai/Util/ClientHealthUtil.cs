namespace Kampai.Util
{
	public static class ClientHealthUtil
	{
		public static bool isHealthMetricsEnabled(global::Kampai.Game.IConfigurationsService configService, global::Kampai.Game.IUserSessionService sessionService)
		{
			int healthMetricPercentage = configService.GetConfigurations().healthMetricPercentage;
			return global::Kampai.Util.ServiceSampleUtil.IsServiceEnabled(global::Kampai.Util.ServiceSampleUtil.ServiceType.ClientHealthMetrics, healthMetricPercentage, sessionService);
		}
	}
}
