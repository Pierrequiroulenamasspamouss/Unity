namespace __preref_Kampai_UI_View_CreditsPanelMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.CreditsPanelMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[8]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreditsPanelMediator)target).soundFXSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreditsPanelMediator)target).locService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ClientVersion), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreditsPanelMediator)target).clientVersion = (global::Kampai.Util.ClientVersion)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CreditsPanelView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreditsPanelMediator)target).view = (global::Kampai.UI.View.CreditsPanelView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIAddedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreditsPanelMediator)target).uiAddedSignal = (global::Kampai.UI.View.UIAddedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIRemovedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreditsPanelMediator)target).uiRemovedSignal = (global::Kampai.UI.View.UIRemovedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseAllOtherMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreditsPanelMediator)target).closeAllOtherMenuSignal = (global::Kampai.UI.View.CloseAllOtherMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreditsPanelMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[8]
			{
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
