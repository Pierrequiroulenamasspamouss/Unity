namespace __preref_Kampai_Game_View_PhilMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.PhilMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[17]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.PhilView), delegate(object target, object val)
				{
					((global::Kampai.Game.View.PhilMediator)target).view = (global::Kampai.Game.View.PhilView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PhilCelebrateSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.PhilMediator)target).celebrateSignal = (global::Kampai.Game.PhilCelebrateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PhilGetAttentionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.PhilMediator)target).getAttentionSignal = (global::Kampai.Game.PhilGetAttentionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PhilBeginIntroLoopSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.PhilMediator)target).beginIntroLoopSignal = (global::Kampai.Game.PhilBeginIntroLoopSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PhilPlayIntroSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.PhilMediator)target).playIntroSignal = (global::Kampai.Game.PhilPlayIntroSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PhilSitAtBarSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.PhilMediator)target).sitAtBarSignal = (global::Kampai.Game.PhilSitAtBarSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PhilActivateSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.PhilMediator)target).activateSignal = (global::Kampai.Game.PhilActivateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.AnimatePhilSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.PhilMediator)target).animatePhilSignal = (global::Kampai.Game.AnimatePhilSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PhilGoToTikiBarSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.PhilMediator)target).philGoToTikiBarSignal = (global::Kampai.Game.PhilGoToTikiBarSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PhilEnableTikiBarControllerSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.PhilMediator)target).enableTikiBarControllerSignal = (global::Kampai.Game.PhilEnableTikiBarControllerSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.TeleportCharacterToTikiBarSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.PhilMediator)target).teleportCharacterToTikiBarSignal = (global::Kampai.Game.TeleportCharacterToTikiBarSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.PhilMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.View.PhilMediator)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.TikiBarSetAnimParamSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.PhilMediator)target).tikiBarSetAnimParamSignal = (global::Kampai.Game.TikiBarSetAnimParamSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PhilGoToStartLocationSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.PhilMediator)target).goToStartLocationSignal = (global::Kampai.Game.PhilGoToStartLocationSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Game.View.PhilMediator)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.PhilMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[17]
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
