namespace __preref_Kampai_Download_LaunchDownloadCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Download.LaunchDownloadCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[13]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(bool), delegate(object target, object val)
				{
					((global::Kampai.Download.LaunchDownloadCommand)target).shouldLoadAudio = (bool)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ReconcileDLCSignal), delegate(object target, object val)
				{
					((global::Kampai.Download.LaunchDownloadCommand)target).reconcileSignal = (global::Kampai.Common.ReconcileDLCSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.DLCModel), delegate(object target, object val)
				{
					((global::Kampai.Download.LaunchDownloadCommand)target).dlcModel = (global::Kampai.Download.DLCModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.IManifestService), delegate(object target, object val)
				{
					((global::Kampai.Download.LaunchDownloadCommand)target).manifestService = (global::Kampai.Common.IManifestService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.DownloadResponseSignal), delegate(object target, object val)
				{
					((global::Kampai.Download.LaunchDownloadCommand)target).downloadResponseSignal = (global::Kampai.Download.DownloadResponseSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.DownloadInitializeSignal), delegate(object target, object val)
				{
					((global::Kampai.Download.LaunchDownloadCommand)target).initSignal = (global::Kampai.Download.DownloadInitializeSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Download.LaunchDownloadCommand)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Download.LaunchDownloadCommand)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.DownloadProgressSignal), delegate(object target, object val)
				{
					((global::Kampai.Download.LaunchDownloadCommand)target).progressSignal = (global::Kampai.Download.DownloadProgressSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.DownloadDLCPartSignal), delegate(object target, object val)
				{
					((global::Kampai.Download.LaunchDownloadCommand)target).downloadDLCPartSignal = (global::Kampai.Download.DownloadDLCPartSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.DLCDownloadFinishedSignal), delegate(object target, object val)
				{
					((global::Kampai.Download.LaunchDownloadCommand)target).downloadFinishedSignal = (global::Kampai.Download.DLCDownloadFinishedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Download.LaunchDownloadCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Download.LaunchDownloadCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[13];
		}
	}
}
