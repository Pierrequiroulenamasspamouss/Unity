namespace __preref_Kampai_Main_PostDownloadManifestCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Main.PostDownloadManifestCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[17]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.SetupManifestSignal), delegate(object target, object val)
				{
					((global::Kampai.Main.PostDownloadManifestCommand)target).setupManifestSignal = (global::Kampai.Common.SetupManifestSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Main.PostDownloadManifestCommand)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Main.PostDownloadManifestCommand)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ReconcileDLCSignal), delegate(object target, object val)
				{
					((global::Kampai.Main.PostDownloadManifestCommand)target).reconcileDLCSignal = (global::Kampai.Common.ReconcileDLCSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.DLCModel), delegate(object target, object val)
				{
					((global::Kampai.Main.PostDownloadManifestCommand)target).dlcModel = (global::Kampai.Download.DLCModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.IManifestService), delegate(object target, object val)
				{
					((global::Kampai.Main.PostDownloadManifestCommand)target).manifestService = (global::Kampai.Common.IManifestService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ITelemetryService), delegate(object target, object val)
				{
					((global::Kampai.Main.PostDownloadManifestCommand)target).telemetryService = (global::Kampai.Common.ITelemetryService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Main.PostDownloadManifestCommand)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.CheckAvailableStorageSignal), delegate(object target, object val)
				{
					((global::Kampai.Main.PostDownloadManifestCommand)target).checkAvailableStorageSignal = (global::Kampai.Common.CheckAvailableStorageSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.LoginUserSignal), delegate(object target, object val)
				{
					((global::Kampai.Main.PostDownloadManifestCommand)target).loginSignal = (global::Kampai.Game.LoginUserSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.ShowDLCPanelSignal), delegate(object target, object val)
				{
					((global::Kampai.Main.PostDownloadManifestCommand)target).showDLCPanelSignal = (global::Kampai.Download.ShowDLCPanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.LaunchDownloadSignal), delegate(object target, object val)
				{
					((global::Kampai.Main.PostDownloadManifestCommand)target).launchDownloadSignal = (global::Kampai.Download.LaunchDownloadSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.IVideoService), delegate(object target, object val)
				{
					((global::Kampai.Main.PostDownloadManifestCommand)target).videoService = (global::Kampai.Common.IVideoService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.IBackgroundDownloadDlcService), delegate(object target, object val)
				{
					((global::Kampai.Main.PostDownloadManifestCommand)target).backgroundDownloadDlcService = (global::Kampai.Download.IBackgroundDownloadDlcService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IConfigurationsService), delegate(object target, object val)
				{
					((global::Kampai.Main.PostDownloadManifestCommand)target).configurationsService = (global::Kampai.Game.IConfigurationsService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Main.PostDownloadManifestCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Main.PostDownloadManifestCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[17];
		}
	}
}
