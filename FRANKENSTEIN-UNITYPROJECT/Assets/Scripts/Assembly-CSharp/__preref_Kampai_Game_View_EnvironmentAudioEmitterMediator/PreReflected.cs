namespace __preref_Kampai_Game_View_EnvironmentAudioEmitterMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.EnvironmentAudioEmitterMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[5]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.EnvironmentAudioEmitterView), delegate(object target, object val)
				{
					((global::Kampai.Game.View.EnvironmentAudioEmitterMediator)target).View = (global::Kampai.Game.View.EnvironmentAudioEmitterView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Camera), delegate(object target, object val)
				{
					((global::Kampai.Game.View.EnvironmentAudioEmitterMediator)target).MainCamera = (global::UnityEngine.Camera)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.Service.Audio.IFMODService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.EnvironmentAudioEmitterMediator)target).FMODService = (global::Kampai.Common.Service.Audio.IFMODService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Game.View.EnvironmentAudioEmitterMediator)target).RoutineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.EnvironmentAudioEmitterMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[5]
			{
				null,
				global::Kampai.Main.MainElement.CAMERA,
				null,
				null,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW
			};
		}
	}
}
