namespace __preref_Kampai_Game_UnlockMinionsCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.UnlockMinionsCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[15]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlockMinionsCommand)target).uiContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlockMinionsCommand)target).camera = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlockMinionsCommand)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlockMinionsCommand)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.IRandomService), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlockMinionsCommand)target).randomService = (global::Kampai.Common.IRandomService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PhilBeginIntroLoopSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlockMinionsCommand)target).beginIntroLoopSignal = (global::Kampai.Game.PhilBeginIntroLoopSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.UnleashCharacterAtShoreSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlockMinionsCommand)target).unleashCharacterAtShoreSignal = (global::Kampai.Game.UnleashCharacterAtShoreSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseLevelUpRewardSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlockMinionsCommand)target).levelCompleteSignal = (global::Kampai.UI.View.CloseLevelUpRewardSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.PromptReceivedSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlockMinionsCommand)target).promptReceivedSignal = (global::Kampai.UI.View.PromptReceivedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraZoomBeachSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlockMinionsCommand)target).cameraZoomBeachSignal = (global::Kampai.Game.CameraZoomBeachSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.UnlockCharacterModel), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlockMinionsCommand)target).characterModel = (global::Kampai.Game.UnlockCharacterModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CreateNamedCharacterViewSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlockMinionsCommand)target).createCharacterSignal = (global::Kampai.Game.CreateNamedCharacterViewSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CreateMinionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlockMinionsCommand)target).createMinionSignal = (global::Kampai.Game.CreateMinionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlockMinionsCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlockMinionsCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[15]
			{
				global::Kampai.UI.View.UIElement.CONTEXT,
				global::Kampai.Main.MainElement.CAMERA,
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
				null
			};
		}
	}
}
