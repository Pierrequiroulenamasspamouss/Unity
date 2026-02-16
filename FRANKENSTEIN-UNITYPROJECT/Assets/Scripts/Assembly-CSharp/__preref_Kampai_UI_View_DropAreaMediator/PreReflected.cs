namespace __preref_Kampai_UI_View_DropAreaMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.DropAreaMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[6]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.DropAreaView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DropAreaMediator)target).DropAreaView = (global::Kampai.UI.View.DropAreaView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OnDragItemOverDropAreaSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DropAreaMediator)target).OnDragItemOverDropAreaSignal = (global::Kampai.UI.View.OnDragItemOverDropAreaSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OnDropItemOverDropAreaSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DropAreaMediator)target).OnDropItemOverDropAreaSignal = (global::Kampai.UI.View.OnDropItemOverDropAreaSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OnDragItemSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DropAreaMediator)target).OnDragItemSignal = (global::Kampai.UI.View.OnDragItemSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OnDropItemSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DropAreaMediator)target).OnDropItemSignal = (global::Kampai.UI.View.OnDropItemSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DropAreaMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[6]
			{
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
