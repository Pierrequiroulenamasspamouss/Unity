namespace __preref_Kampai_Game_GamePauseCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.GamePauseCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[15]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SavePlayerSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GamePauseCommand)target).savePlayerSignal = (global::Kampai.Game.SavePlayerSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.LogClientMetricsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GamePauseCommand)target).clientMetricsSignal = (global::Kampai.Common.LogClientMetricsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ReengageNotificationSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GamePauseCommand)target).reengageNotificationSignal = (global::Kampai.Game.ReengageNotificationSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ScheduleJobsCompleteNotificationsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GamePauseCommand)target).scheduleJobsCompleteNotificationsSignal = (global::Kampai.Game.ScheduleJobsCompleteNotificationsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDevicePrefsService), delegate(object target, object val)
				{
					((global::Kampai.Game.GamePauseCommand)target).devicePrefsService = (global::Kampai.Game.IDevicePrefsService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeService), delegate(object target, object val)
				{
					((global::Kampai.Game.GamePauseCommand)target).timeService = (global::Kampai.Game.ITimeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.Service.HealthMetrics.IClientHealthService), delegate(object target, object val)
				{
					((global::Kampai.Game.GamePauseCommand)target).clientHealthService = (global::Kampai.Common.Service.HealthMetrics.IClientHealthService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ICurrencyService), delegate(object target, object val)
				{
					((global::Kampai.Game.GamePauseCommand)target).currencyService = (global::Kampai.Game.ICurrencyService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CancelBuildingMovementSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GamePauseCommand)target).cancelBuildingMovementSignal = (global::Kampai.Game.CancelBuildingMovementSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ClientVersion), delegate(object target, object val)
				{
					((global::Kampai.Game.GamePauseCommand)target).clientVersion = (global::Kampai.Util.ClientVersion)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.NimbleTelemetryEventsPostedSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GamePauseCommand)target).nimbleTelemetryEventsPostedSignal = (global::Kampai.Common.NimbleTelemetryEventsPostedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.GamePauseCommand)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.Logging.Hosted.ILogglyService), delegate(object target, object val)
				{
					((global::Kampai.Game.GamePauseCommand)target).logglyService = (global::Kampai.Util.Logging.Hosted.ILogglyService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.GamePauseCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.GamePauseCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[15];
		}
	}
}
