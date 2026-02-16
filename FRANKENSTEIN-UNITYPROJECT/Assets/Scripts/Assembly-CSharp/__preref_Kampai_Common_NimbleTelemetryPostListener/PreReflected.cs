namespace __preref_Kampai_Common_NimbleTelemetryPostListener
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Common.NimbleTelemetryPostListener();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = new global::System.Action<object>[1]
			{
				delegate(object t)
				{
					((global::Kampai.Common.NimbleTelemetryPostListener)t).PostConstruct();
				}
			};
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[2]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Common.NimbleTelemetryPostListener)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.NimbleTelemetryEventsPostedSignal), delegate(object target, object val)
				{
					((global::Kampai.Common.NimbleTelemetryPostListener)target).postSignal = (global::Kampai.Common.NimbleTelemetryEventsPostedSignal)val;
				})
			};
			SetterNames = new object[2];
		}
	}
}
