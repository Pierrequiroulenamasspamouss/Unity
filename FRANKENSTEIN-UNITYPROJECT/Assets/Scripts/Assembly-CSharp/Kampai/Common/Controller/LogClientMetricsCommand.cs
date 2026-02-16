namespace Kampai.Common.Controller
{
	public class LogClientMetricsCommand : global::strange.extensions.command.impl.Command
	{
		private sealed class ClientMeterMetricsDTO : global::Kampai.Util.IFastJSONSerializable
		{
			public global::System.Collections.Generic.Dictionary<string, int> meterEvents { get; set; }

			public void Serialize(global::Newtonsoft.Json.JsonWriter writer)
			{
				writer.WriteStartObject();
				writer.WritePropertyName("meterEvents");
				writer.WriteStartObject();
				global::System.Collections.Generic.Dictionary<string, int>.Enumerator enumerator = meterEvents.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						global::System.Collections.Generic.KeyValuePair<string, int> current = enumerator.Current;
						writer.WritePropertyName(current.Key);
						writer.WriteValue(current.Value);
					}
				}
				finally
				{
					enumerator.Dispose();
				}
				writer.WriteEndObject();
				writer.WriteEndObject();
			}
		}

		private const string METER_METRICS_RESOURCE = "/rest/healthMetrics/meters";

		private const string TIMER_METRICS_RESOURCE = "/rest/healthMetrics/timers";

		[Inject]
		public bool forceRequest { get; set; }

		[Inject]
		public global::Kampai.Common.Service.HealthMetrics.IClientHealthService clientHealthService { get; set; }

		[Inject]
		public global::Kampai.Download.IDownloadService downloadService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IConfigurationsService configService { get; set; }

		[Inject]
		public global::Kampai.Game.IUserSessionService userService { get; set; }

		public override void Execute()
		{
			if (global::Kampai.Util.ClientHealthUtil.isHealthMetricsEnabled(configService, userService))
			{
				SendClientMeterMetrics();
				SendClientTimerMetrics();
			}
		}

		private void SendClientMeterMetrics()
		{
			global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
			signal.AddListener(MeterMetricPOSTComplete);
			global::System.Collections.Generic.Dictionary<string, int> meterEvents = clientHealthService.MeterEvents;
			if (meterEvents != null && meterEvents.Count > 0)
			{
				global::Kampai.Common.Controller.LogClientMetricsCommand.ClientMeterMetricsDTO clientMeterMetricsDTO = new global::Kampai.Common.Controller.LogClientMetricsCommand.ClientMeterMetricsDTO();
				clientMeterMetricsDTO.meterEvents = meterEvents;
				global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(global::Kampai.Util.GameConstants.Server.SERVER_URL + "/rest/healthMetrics/meters").WithContentType("application/json").WithMethod(global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest.METHOD_POST).WithEntity(clientMeterMetricsDTO)
					.WithResponseSignal(signal);
				downloadService.Perform(request, forceRequest);
			}
		}

		private void MeterMetricPOSTComplete(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
		{
			logger.Log(global::Kampai.Util.Logger.Level.Info, "Post Health Metrics response: " + response.Code);
			if (response.Success)
			{
				clientHealthService.ClearMeterEvents();
			}
			else
			{
				logger.Warning("MeterMetricPOSTComplete failed response");
			}
		}

		private void SendClientTimerMetrics()
		{
			global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
			signal.AddOnce(SendClientTimerMetricsComplete);
			global::System.Collections.Generic.Dictionary<string, float> timerEvents = clientHealthService.TimerEvents;
			if (timerEvents != null && timerEvents.Count > 0)
			{
				global::Kampai.Common.Controller.ClientTimerMetricsDTO clientTimerMetricsDTO = new global::Kampai.Common.Controller.ClientTimerMetricsDTO();
				clientTimerMetricsDTO.timerEvents = timerEvents;
				global::Kampai.Common.Controller.ClientTimerMetricsDTO entity = clientTimerMetricsDTO;
				global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(global::Kampai.Util.GameConstants.Server.SERVER_URL + "/rest/healthMetrics/timers").WithContentType("application/json").WithMethod(global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest.METHOD_POST).WithEntity(entity)
					.WithResponseSignal(signal);
				downloadService.Perform(request, forceRequest);
			}
		}

		private void SendClientTimerMetricsComplete(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
		{
			logger.Log(global::Kampai.Util.Logger.Level.Info, "Post Health Metrics response: " + response.Code);
			if (response.Success)
			{
				clientHealthService.ClearTimerEvents();
			}
			else
			{
				logger.Warning("SendClientTimerMetricsComplete failed response");
			}
		}
	}
}
