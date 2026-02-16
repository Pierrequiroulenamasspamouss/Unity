namespace __preref_Kampai_UI_View_FloatingTextPanelMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.FloatingTextPanelMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[5]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.FloatingTextPanelView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.FloatingTextPanelMediator)target).view = (global::Kampai.UI.View.FloatingTextPanelView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.UI.View.FloatingTextPanelMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.DisplayFloatingTextSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.FloatingTextPanelMediator)target).displayFloatingTextSignal = (global::Kampai.UI.View.DisplayFloatingTextSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.RemoveFloatingTextSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.FloatingTextPanelMediator)target).removeFloatingTextSignal = (global::Kampai.UI.View.RemoveFloatingTextSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.FloatingTextPanelMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[5]
			{
				null,
				null,
				null,
				null,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW
			};
		}
	}
}
