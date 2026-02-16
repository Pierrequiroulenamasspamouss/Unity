namespace __preref_Kampai_UI_View_DragDropItemMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.DragDropItemMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[5]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.DragDropItemView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DragDropItemMediator)target).View = (global::Kampai.UI.View.DragDropItemView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OnDragItemSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DragDropItemMediator)target).OnDragItemSignal = (global::Kampai.UI.View.OnDragItemSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OnDropItemSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DragDropItemMediator)target).OnDropItemSignal = (global::Kampai.UI.View.OnDropItemSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DragDropItemMediator)target).playSFXSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DragDropItemMediator)target).contextView = (global::UnityEngine.GameObject)val;
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
