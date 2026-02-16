namespace __preref_Kampai_Game_TouchInput
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.TouchInput();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = new global::System.Action<object>[1]
			{
				delegate(object t)
				{
					((global::Kampai.Game.TouchInput)t).PostConstruct();
				}
			};
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[5]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher), delegate(object target, object val)
				{
					((global::Kampai.Game.TouchInput)target).dispatcher = (global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Game.TouchInput)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.IPickService), delegate(object target, object val)
				{
					((global::Kampai.Game.TouchInput)target).pickService = (global::Kampai.Common.IPickService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.IMignetteService), delegate(object target, object val)
				{
					((global::Kampai.Game.TouchInput)target).mignetteService = (global::Kampai.Game.Mignette.IMignetteService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.DeviceInformation), delegate(object target, object val)
				{
					((global::Kampai.Game.TouchInput)target).deviceInformation = (global::Kampai.Util.DeviceInformation)val;
				})
			};
			SetterNames = new object[5]
			{
				global::strange.extensions.context.api.ContextKeys.CONTEXT_DISPATCHER,
				null,
				null,
				null,
				null
			};
		}
	}
}
