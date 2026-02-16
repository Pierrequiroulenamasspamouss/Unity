namespace __preref_Kampai_Util_Logging_Hosted_LogglyService
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Util.Logging.Hosted.LogglyService();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = new global::System.Action<object>[1]
			{
				delegate(object t)
				{
					((global::Kampai.Util.Logging.Hosted.LogglyService)t).Initialize();
				}
			};
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[4]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.Logging.Hosted.ILogBuffer), delegate(object target, object val)
				{
					((global::Kampai.Util.Logging.Hosted.LogglyService)target).logBuffer = (global::Kampai.Util.Logging.Hosted.ILogBuffer)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.IDownloadService), delegate(object target, object val)
				{
					((global::Kampai.Util.Logging.Hosted.LogglyService)target).downloadService = (global::Kampai.Download.IDownloadService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(string), delegate(object target, object val)
				{
					((global::Kampai.Util.Logging.Hosted.LogglyService)target).serverEnvironment = (string)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ClientVersion), delegate(object target, object val)
				{
					((global::Kampai.Util.Logging.Hosted.LogglyService)target).clientVersion = (global::Kampai.Util.ClientVersion)val;
				})
			};
			SetterNames = new object[4] { null, null, "game.server.environment", null };
		}
	}
}
