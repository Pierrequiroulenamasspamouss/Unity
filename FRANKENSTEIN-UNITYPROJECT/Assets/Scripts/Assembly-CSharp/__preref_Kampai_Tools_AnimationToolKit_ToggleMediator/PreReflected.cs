namespace __preref_Kampai_Tools_AnimationToolKit_ToggleMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Tools.AnimationToolKit.ToggleMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[4]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.ToggleMediator)target).ToggleGroupGameObject = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.ToggleView), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.ToggleMediator)target).view = (global::Kampai.Tools.AnimationToolKit.ToggleView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.AnimationToolKit), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.ToggleMediator)target).AnimationToolKit = (global::Kampai.Tools.AnimationToolKit.AnimationToolKit)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.ToggleMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[4]
			{
				global::Kampai.Tools.AnimationToolKit.AnimationToolKitElement.TOGGLE_GROUP,
				null,
				null,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW
			};
		}
	}
}
