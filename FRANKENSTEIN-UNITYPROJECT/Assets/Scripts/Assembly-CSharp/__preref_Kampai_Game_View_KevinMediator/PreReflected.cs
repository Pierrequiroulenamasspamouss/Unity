namespace __preref_Kampai_Game_View_KevinMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.KevinMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[12]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.KevinView), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KevinMediator)target).view = (global::Kampai.Game.View.KevinView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.KevinGreetVillainSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KevinMediator)target).greetVillainSignal = (global::Kampai.Game.KevinGreetVillainSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.KevinWaveFarewellSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KevinMediator)target).waveFarewellSignal = (global::Kampai.Game.KevinWaveFarewellSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.AnimateKevinSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KevinMediator)target).animateKevinSignal = (global::Kampai.Game.AnimateKevinSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KevinMediator)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.KevinGoToWelcomeHutSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KevinMediator)target).gotoWelcomeHutSignal = (global::Kampai.Game.KevinGoToWelcomeHutSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.KevinWanderSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KevinMediator)target).kevinWanderSignal = (global::Kampai.Game.KevinWanderSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CharacterArrivedAtDestinationSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KevinMediator)target).arrivedAtDestinationSignal = (global::Kampai.Game.CharacterArrivedAtDestinationSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KevinMediator)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KevinMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.KevinFrolicsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KevinMediator)target).kevinFrolicsSignal = (global::Kampai.Game.KevinFrolicsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KevinMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[12]
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
				null,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW
			};
		}
	}
}
