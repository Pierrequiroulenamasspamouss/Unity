namespace __preref_Kampai_Tools_AnimationToolKit_AnimationToolKitButtonMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Tools.AnimationToolKit.AnimationToolKitButtonMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[7]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.AnimationToolKitButtonView), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKitButtonMediator)target).view = (global::Kampai.Tools.AnimationToolKit.AnimationToolKitButtonView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.AnimationToolkitModel), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKitButtonMediator)target).Model = (global::Kampai.Tools.AnimationToolKit.AnimationToolkitModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.AnimationToolKit), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKitButtonMediator)target).AnimationToolKit = (global::Kampai.Tools.AnimationToolKit.AnimationToolKit)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.LoadInterfaceSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKitButtonMediator)target).loadInterfaceSignal = (global::Kampai.Tools.AnimationToolKit.LoadInterfaceSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.ToggleInterfaceSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKitButtonMediator)target).toggleInterfaceSignal = (global::Kampai.Tools.AnimationToolKit.ToggleInterfaceSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.ToggleMeshSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKitButtonMediator)target).toggleMeshSignal = (global::Kampai.Tools.AnimationToolKit.ToggleMeshSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKitButtonMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[7]
			{
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
