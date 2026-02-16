namespace __preref_Kampai_Tools_AnimationToolKit_LoadAnimationToolKitCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Tools.AnimationToolKit.LoadAnimationToolKitCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[11]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadAnimationToolKitCommand)target).ContextView = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadAnimationToolKitCommand)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.Service.Audio.IFMODService), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadAnimationToolKitCommand)target).fmodService = (global::Kampai.Common.Service.Audio.IFMODService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadAnimationToolKitCommand)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.LoadDefinitionsSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadAnimationToolKitCommand)target).LoadDefinitionsSignal = (global::Kampai.Game.LoadDefinitionsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.LoadCameraSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadAnimationToolKitCommand)target).LoadCameraSignal = (global::Kampai.Tools.AnimationToolKit.LoadCameraSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.LoadCanvasSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadAnimationToolKitCommand)target).LoadCanvasSignal = (global::Kampai.Tools.AnimationToolKit.LoadCanvasSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.LoadEventSystemSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadAnimationToolKitCommand)target).LoadEventSystemSignal = (global::Kampai.Tools.AnimationToolKit.LoadEventSystemSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.SetupManifestSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadAnimationToolKitCommand)target).setupManifestSignal = (global::Kampai.Common.SetupManifestSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadAnimationToolKitCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadAnimationToolKitCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[11]
			{
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW,
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
