namespace Kampai.Game
{
	public class GamePauseCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.SavePlayerSignal savePlayerSignal { get; set; }

		[Inject]
		public global::Kampai.Common.LogClientMetricsSignal clientMetricsSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ReengageNotificationSignal reengageNotificationSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ScheduleJobsCompleteNotificationsSignal scheduleJobsCompleteNotificationsSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IDevicePrefsService devicePrefsService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Common.Service.HealthMetrics.IClientHealthService clientHealthService { get; set; }

		[Inject]
		public global::Kampai.Game.ICurrencyService currencyService { get; set; }

		[Inject]
		public global::Kampai.Game.CancelBuildingMovementSignal cancelBuildingMovementSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ClientVersion clientVersion { get; set; }

		[Inject]
		public global::Kampai.Common.NimbleTelemetryEventsPostedSignal nimbleTelemetryEventsPostedSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Util.Logging.Hosted.ILogglyService logglyService { get; set; }

		public override void Execute()
		{
			logger.Info("GamePauseCommand");
			reengageNotificationSignal.Dispatch();
			scheduleJobsCompleteNotificationsSignal.Dispatch();
			devicePrefsService.GetDevicePrefs().SleepTime = timeService.DeviceTimeSeconds();
			currencyService.PauseTransactionsHandling();
			savePlayerSignal.Dispatch(new global::Kampai.Util.Tuple<string, string, bool>("remote", string.Empty, true));
			cancelBuildingMovementSignal.Dispatch(false);
			clientVersion.RemoveOverrideVersion();
			clientHealthService.MarkTimerEvent("AppFlow.Pause", global::UnityEngine.Time.time);
			clientMetricsSignal.Dispatch(false);
			nimbleTelemetryEventsPostedSignal.Dispatch();
			logglyService.ShipAll(true);
		}
	}
}
