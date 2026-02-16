namespace __preref_Kampai_Main_MainCompleteCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Main.MainCompleteCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[28]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.Service.Audio.IFMODService), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).fmodService = (global::Kampai.Common.Service.Audio.IFMODService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.AutoSavePlayerStateSignal), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).autoSavePlayerSignal = (global::Kampai.Game.AutoSavePlayerStateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ReloadConfigurationsPeriodicSignal), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).reloadConfigs = (global::Kampai.Game.ReloadConfigurationsPeriodicSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.LoadDevicePrefsSignal), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).loadDevicePrefsSignal = (global::Kampai.Main.LoadDevicePrefsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.LoadSharedBundlesSignal), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).bundleSignal = (global::Kampai.Main.LoadSharedBundlesSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.Service.HealthMetrics.IClientHealthService), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).clientHealthService = (global::Kampai.Common.Service.HealthMetrics.IClientHealthService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.LogTapEventMetricsSignal), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).logTapEventMetricsSignal = (global::Kampai.Common.LogTapEventMetricsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.LoadLocalizationServiceSignal), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).localServiceSignal = (global::Kampai.Main.LoadLocalizationServiceSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SetupPushNotificationsSignal), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).setupPushNotificationsSignal = (global::Kampai.Game.SetupPushNotificationsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ITelemetryService), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).telemetryService = (global::Kampai.Common.ITelemetryService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.LoadExternalScenesSignal), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).loadExternalScenesSignal = (global::Kampai.Main.LoadExternalScenesSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.IAssetBundlesService), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).assetBundlesService = (global::Kampai.Main.IAssetBundlesService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimedSocialEventService), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).socialEventService = (global::Kampai.Game.ITimedSocialEventService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDLCService), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).dlcService = (global::Kampai.Game.IDLCService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.DLCModel), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).dlcModel = (global::Kampai.Download.DLCModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.NimbleOTSignal), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).nimbleOTSignal = (global::Kampai.Game.NimbleOTSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ICoroutineProgressMonitor), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).coroutineProgressMonitor = (global::Kampai.Util.ICoroutineProgressMonitor)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.LoadAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).loadAudioSignal = (global::Kampai.Main.LoadAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.ShowDLCPanelSignal), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).showDLCPanelSignal = (global::Kampai.Download.ShowDLCPanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.LaunchDownloadSignal), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).launchDownloadSignal = (global::Kampai.Download.LaunchDownloadSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Splash.SplashProgressUpdateSignal), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).splashProgressDoneSignal = (global::Kampai.Splash.SplashProgressUpdateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Splash.ILoadInService), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).loadInService = (global::Kampai.Splash.ILoadInService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.FastCommandPool), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).fastCommandPool = (global::Kampai.Util.FastCommandPool)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Main.MainCompleteCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[28];
		}
	}
}
