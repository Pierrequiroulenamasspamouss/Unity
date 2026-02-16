namespace __preref_Kampai_Game_View_Audio_PositionalAudioListenerMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.Audio.PositionalAudioListenerMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[6]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.Audio.PositionalAudioListenerView), delegate(object target, object val)
				{
					((global::Kampai.Game.View.Audio.PositionalAudioListenerMediator)target).view = (global::Kampai.Game.View.Audio.PositionalAudioListenerView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Camera), delegate(object target, object val)
				{
					((global::Kampai.Game.View.Audio.PositionalAudioListenerMediator)target).mainCamera = (global::UnityEngine.Camera)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Camera), delegate(object target, object val)
				{
					((global::Kampai.Game.View.Audio.PositionalAudioListenerMediator)target).uiCamera = (global::UnityEngine.Camera)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.CameraUtils), delegate(object target, object val)
				{
					((global::Kampai.Game.View.Audio.PositionalAudioListenerMediator)target).cameraUtils = (global::Kampai.Game.View.CameraUtils)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.MoveAudioListenerSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.Audio.PositionalAudioListenerMediator)target).moveCharacterAudio = (global::Kampai.Main.MoveAudioListenerSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.Audio.PositionalAudioListenerMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[6]
			{
				null,
				global::Kampai.Main.MainElement.CAMERA,
				global::Kampai.UI.View.UIElement.CAMERA,
				null,
				null,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW
			};
		}
	}
}
