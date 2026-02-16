namespace __preref_Kampai_Game_KeyboardInput
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.KeyboardInput();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = new global::System.Action<object>[1]
			{
				delegate(object t)
				{
					((global::Kampai.Game.KeyboardInput)t).PostConstruct();
				}
			};
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[5]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher), delegate(object target, object val)
				{
					((global::Kampai.Game.KeyboardInput)target).dispatcher = (global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Game.KeyboardInput)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.IPickService), delegate(object target, object val)
				{
					((global::Kampai.Game.KeyboardInput)target).pickService = (global::Kampai.Common.IPickService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.IMignetteService), delegate(object target, object val)
				{
					((global::Kampai.Game.KeyboardInput)target).mignetteService = (global::Kampai.Game.Mignette.IMignetteService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.DebugKeyHitSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.KeyboardInput)target).debugKeyHitSignal = (global::Kampai.Game.DebugKeyHitSignal)val;
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
