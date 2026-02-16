namespace __preref_Kampai_UI_View_CraftingPopupMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.CraftingPopupMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[8]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CraftingPopupView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CraftingPopupMediator)target).view = (global::Kampai.UI.View.CraftingPopupView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CraftingPopupMediator)target).localizationService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CraftingPopupMediator)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CraftingPopupMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.DisplayItemPopupSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CraftingPopupMediator)target).popupSignal = (global::Kampai.UI.View.DisplayItemPopupSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideItemPopupSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CraftingPopupMediator)target).closeSignal = (global::Kampai.UI.View.HideItemPopupSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CraftingPopupMediator)target).soundFXSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CraftingPopupMediator)target).contextView = (global::UnityEngine.GameObject)val;
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
