namespace __preref_Kampai_Download_View_NoWiFiMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Download.View.NoWiFiMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[11]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.View.NoWiFiView), delegate(object target, object val)
				{
					((global::Kampai.Download.View.NoWiFiMediator)target).view = (global::Kampai.Download.View.NoWiFiView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.DLCModel), delegate(object target, object val)
				{
					((global::Kampai.Download.View.NoWiFiMediator)target).dlcModel = (global::Kampai.Download.DLCModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.DownloadDLCPartSignal), delegate(object target, object val)
				{
					((global::Kampai.Download.View.NoWiFiMediator)target).downloadDLCPartSignal = (global::Kampai.Download.DownloadDLCPartSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.Download.View.NoWiFiMediator)target).localService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Download.View.NoWiFiMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.ShowNoWiFiPanelSignal), delegate(object target, object val)
				{
					((global::Kampai.Download.View.NoWiFiMediator)target).showNoWiFiPanelSignal = (global::Kampai.Download.ShowNoWiFiPanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Download.View.NoWiFiMediator)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.Model.DownloadUIModel), delegate(object target, object val)
				{
					((global::Kampai.Download.View.NoWiFiMediator)target).uiModel = (global::Kampai.Download.Model.DownloadUIModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.NetworkModel), delegate(object target, object val)
				{
					((global::Kampai.Download.View.NoWiFiMediator)target).networkModel = (global::Kampai.Common.NetworkModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.NetworkConnectionLostSignal), delegate(object target, object val)
				{
					((global::Kampai.Download.View.NoWiFiMediator)target).networkConnectionLostSignal = (global::Kampai.Common.NetworkConnectionLostSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Download.View.NoWiFiMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[11]
			{
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW
			};
		}
	}
}
