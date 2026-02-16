namespace __preref_Kampai_Util_Logging_Hosted_LogglyLogger
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Util.Logging.Hosted.LogglyLogger();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[6]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.Logging.Hosted.ILogglyService), delegate(object target, object val)
				{
					((global::Kampai.Util.Logging.Hosted.LogglyLogger)target).logglyService = (global::Kampai.Util.Logging.Hosted.ILogglyService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.LogClientMetricsSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.Logging.Hosted.LogglyLogger)target).clientMetricsSignal = (global::Kampai.Common.LogClientMetricsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.IDownloadService), delegate(object target, object val)
				{
					((global::Kampai.Util.Logging.Hosted.LogglyLogger)target).downloadService = (global::Kampai.Download.IDownloadService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.LogToScreenSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.Logging.Hosted.LogglyLogger)target).logToScreenSignal = (global::Kampai.UI.View.LogToScreenSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.Util.Logging.Hosted.LogglyLogger)target).localService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.Util.Logging.Hosted.LogglyLogger)target).baseContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				})
			};
			SetterNames = new object[6]
			{
				null,
				null,
				null,
				null,
				null,
				global::Kampai.Util.BaseElement.CONTEXT
			};
		}
	}
}
