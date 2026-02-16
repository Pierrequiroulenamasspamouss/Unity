namespace __preref_Kampai_Download_DownloadPanelMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Download.DownloadPanelMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[7]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.DownloadPanelView), delegate(object target, object val)
				{
					((global::Kampai.Download.DownloadPanelMediator)target).view = (global::Kampai.Download.DownloadPanelView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.DLCDownloadFinishedSignal), delegate(object target, object val)
				{
					((global::Kampai.Download.DownloadPanelMediator)target).downloadFinishedSignal = (global::Kampai.Download.DLCDownloadFinishedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.ShowNoWiFiPanelSignal), delegate(object target, object val)
				{
					((global::Kampai.Download.DownloadPanelMediator)target).showNoWiFiPanelSignal = (global::Kampai.Download.ShowNoWiFiPanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Download.DownloadPanelMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.ShowDLCPanelSignal), delegate(object target, object val)
				{
					((global::Kampai.Download.DownloadPanelMediator)target).showPanelSignal = (global::Kampai.Download.ShowDLCPanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ReloadGameSignal), delegate(object target, object val)
				{
					((global::Kampai.Download.DownloadPanelMediator)target).reloadSignal = (global::Kampai.Main.ReloadGameSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Download.DownloadPanelMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[7]
			{
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
