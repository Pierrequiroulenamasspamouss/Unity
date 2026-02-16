namespace Kampai.Game.Controller
{
	public class AppQuitCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Common.Service.HealthMetrics.IClientHealthService clientHealthService { get; set; }

		[Inject]
		public global::Kampai.Common.LogClientMetricsSignal logClientMetricsSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SavePlayerSignal savePlayerSignal { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		public override void Execute()
		{
			savePlayerSignal.Dispatch(new global::Kampai.Util.Tuple<string, string, bool>("remote", string.Empty, true));
			telemetryService.Send_Telemetry_EVT_USER_DATA_AT_APP_CLOSE();
			clientHealthService.MarkTimerEvent("AppFlow.Quit", global::UnityEngine.Time.time);
			logClientMetricsSignal.Dispatch(false);
		}
	}
}
