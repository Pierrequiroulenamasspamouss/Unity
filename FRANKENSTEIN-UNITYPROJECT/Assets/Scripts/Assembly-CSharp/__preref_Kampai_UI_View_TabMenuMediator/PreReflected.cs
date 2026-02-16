namespace __preref_Kampai_UI_View_TabMenuMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.TabMenuMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[11]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.TabMenuView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.TabMenuMediator)target).view = (global::Kampai.UI.View.TabMenuView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.AddStoreTabSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.TabMenuMediator)target).addTabSignal = (global::Kampai.UI.View.AddStoreTabSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OnTabClickedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.TabMenuMediator)target).tabClickSignal = (global::Kampai.UI.View.OnTabClickedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MoveTabMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.TabMenuMediator)target).moveTabSignal = (global::Kampai.UI.View.MoveTabMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetBadgeForStoreTabSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.TabMenuMediator)target).setBadgeForTabSignal = (global::Kampai.UI.View.SetBadgeForStoreTabSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetNewUnlockForStoreTabSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.TabMenuMediator)target).setNewUnlockForTabSignal = (global::Kampai.UI.View.SetNewUnlockForStoreTabSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.IBuildMenuService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.TabMenuMediator)target).buildMenuService = (global::Kampai.UI.IBuildMenuService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.IGUIService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.TabMenuMediator)target).guiService = (global::Kampai.UI.View.IGUIService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.UI.View.TabMenuMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher), delegate(object target, object val)
				{
					((global::Kampai.UI.View.TabMenuMediator)target).dispatcher = (global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.TabMenuMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[11]
			{
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_DISPATCHER,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW
			};
		}
	}
}
