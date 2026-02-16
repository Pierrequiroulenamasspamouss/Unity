namespace __preref_Kampai_UI_View_WorldProgressPanelMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.WorldProgressPanelMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[9]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.WorldProgressPanelView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.WorldProgressPanelMediator)target).view = (global::Kampai.UI.View.WorldProgressPanelView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.UI.View.WorldProgressPanelMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.DisplayWorldProgressSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.WorldProgressPanelMediator)target).displayWorldProgressSignal = (global::Kampai.UI.View.DisplayWorldProgressSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.RemoveWorldProgressSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.WorldProgressPanelMediator)target).removeWorldProgressSignal = (global::Kampai.UI.View.RemoveWorldProgressSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowMoveBuildingMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.WorldProgressPanelMediator)target).showMoveBuildingMenuSignal = (global::Kampai.UI.View.ShowMoveBuildingMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideAllWayFindersSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.WorldProgressPanelMediator)target).hideAllWayFindersSignal = (global::Kampai.UI.View.HideAllWayFindersSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowAllWayFindersSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.WorldProgressPanelMediator)target).showAllWayFindersSignal = (global::Kampai.UI.View.ShowAllWayFindersSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseAllOtherMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.WorldProgressPanelMediator)target).closeAllMenuSignal = (global::Kampai.UI.View.CloseAllOtherMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.WorldProgressPanelMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[9]
			{
				null,
				null,
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
