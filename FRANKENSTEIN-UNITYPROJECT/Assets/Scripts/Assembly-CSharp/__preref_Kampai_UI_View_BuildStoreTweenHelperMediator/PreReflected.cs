namespace __preref_Kampai_UI_View_BuildStoreTweenHelperMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.BuildStoreTweenHelperMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[2]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.BuildStoreTweenHelperView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.BuildStoreTweenHelperMediator)target).view = (global::Kampai.UI.View.BuildStoreTweenHelperView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.BuildStoreTweenHelperMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[2]
			{
				null,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW
			};
		}
	}
}
