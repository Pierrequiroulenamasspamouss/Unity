namespace Kampai.Util
{
	public class TelemetryLogger : global::Kampai.Util.Logger
	{
		[Inject]
		public global::Kampai.Common.LogClientMetricsSignal clientMetricsSignal { get; set; }

		public override void FatalNoThrow(global::Kampai.Util.FatalCode code, int subcode, string format, params object[] args)
		{
			try
			{
				string eventName = string.Format("AppFlow.Fatal.{0}", code);
				global::Kampai.Common.Service.HealthMetrics.IClientHealthService instance = base.baseContext.injectionBinder.CrossContextBinder.GetInstance<global::Kampai.Common.Service.HealthMetrics.IClientHealthService>();
				if (instance != null)
				{
					instance.MarkMeterEvent(eventName);
					clientMetricsSignal.Dispatch(true);
				}
			}
			catch (global::System.Exception ex)
			{
				global::Kampai.Util.Native.LogError(string.Format("Unable to report fatal code metric: {0}", ex.Message));
			}
			string text = global::System.Enum.GetName(typeof(global::Kampai.Util.FatalCode), code);
			if (string.IsNullOrEmpty(text))
			{
				text = "UNKNOWN";
			}
			string nameOfError = string.Format("{0}-{1}-{2}", text, (int)code, subcode);
			string errorDetails = (string.IsNullOrEmpty(format) ? string.Empty : ((args == null) ? format : string.Format(format, args)));
			try
			{
				global::Kampai.Common.ITelemetryService instance2 = base.baseContext.injectionBinder.CrossContextBinder.GetInstance<global::Kampai.Common.ITelemetryService>();
				if (instance2 != null)
				{
					if (code.IsNetworkError())
					{
						instance2.Send_Telemetry_EVT_GAME_ERROR_CONNECTIVITY(nameOfError, errorDetails);
					}
					else
					{
						instance2.Send_Telemetry_EVT_GAME_ERROR_GAMEPLAY(nameOfError, errorDetails);
					}
				}
			}
			catch (global::System.Exception ex2)
			{
				global::Kampai.Util.Native.LogError(string.Format("Unable to log telemetry fatal code: {0}", ex2.Message));
			}
			base.FatalNoThrow(code, subcode, format, args);
		}
	}
}
