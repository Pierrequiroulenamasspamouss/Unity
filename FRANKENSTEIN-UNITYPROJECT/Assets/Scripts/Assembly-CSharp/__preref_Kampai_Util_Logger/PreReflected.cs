namespace __preref_Kampai_Util_Logger
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Util.Logger();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[4]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.IDownloadService), delegate(object target, object val)
				{
					((global::Kampai.Util.Logger)target).downloadService = (global::Kampai.Download.IDownloadService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.LogToScreenSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.Logger)target).logToScreenSignal = (global::Kampai.UI.View.LogToScreenSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.Util.Logger)target).localService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.Util.Logger)target).baseContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				})
			};
			SetterNames = new object[4]
			{
				null,
				null,
				null,
				global::Kampai.Util.BaseElement.CONTEXT
			};
		}
	}
}
