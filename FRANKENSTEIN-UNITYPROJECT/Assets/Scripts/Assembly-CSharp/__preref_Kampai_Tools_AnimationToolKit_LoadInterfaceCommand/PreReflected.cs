namespace __preref_Kampai_Tools_AnimationToolKit_LoadInterfaceCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Tools.AnimationToolKit.LoadInterfaceCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[10]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadInterfaceCommand)target).DefinitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadInterfaceCommand)target).PlayerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.AnimationToolkitModel), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadInterfaceCommand)target).Model = (global::Kampai.Tools.AnimationToolKit.AnimationToolkitModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Camera), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadInterfaceCommand)target).Camera = (global::UnityEngine.Camera)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Canvas), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadInterfaceCommand)target).Canvas = (global::UnityEngine.Canvas)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.LoadToggleGroupSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadInterfaceCommand)target).LoadToggleGroupSignal = (global::Kampai.Tools.AnimationToolKit.LoadToggleGroupSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.LoadToggleSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadInterfaceCommand)target).LoadToggleSignal = (global::Kampai.Tools.AnimationToolKit.LoadToggleSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.InitToggleSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadInterfaceCommand)target).InitToggleSignal = (global::Kampai.Tools.AnimationToolKit.InitToggleSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadInterfaceCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadInterfaceCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[10];
		}
	}
}
