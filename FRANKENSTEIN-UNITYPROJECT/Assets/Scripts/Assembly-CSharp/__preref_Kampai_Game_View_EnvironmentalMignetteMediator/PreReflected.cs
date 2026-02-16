namespace __preref_Kampai_Game_View_EnvironmentalMignetteMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.EnvironmentalMignetteMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[5]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.MinionReactInRadiusSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.EnvironmentalMignetteMediator)target).minionReactInRadiusSignal = (global::Kampai.Common.MinionReactInRadiusSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.EnvironmentalMignetteView), delegate(object target, object val)
				{
					((global::Kampai.Game.View.EnvironmentalMignetteMediator)target).view = (global::Kampai.Game.View.EnvironmentalMignetteView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.EnvironmentalMignetteTappedSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.EnvironmentalMignetteMediator)target).environmentalMignetteTappedSignal = (global::Kampai.Common.EnvironmentalMignetteTappedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher), delegate(object target, object val)
				{
					((global::Kampai.Game.View.EnvironmentalMignetteMediator)target).dispatcher = (global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.EnvironmentalMignetteMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[5]
			{
				null,
				null,
				null,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_DISPATCHER,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW
			};
		}
	}
}
