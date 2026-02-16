namespace __preref_Kampai_UI_View_MignetteCallMinionsMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.MignetteCallMinionsMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[15]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideSkrimSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteCallMinionsMediator)target).hideSkrim = (global::Kampai.UI.View.HideSkrimSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.IGUIService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteCallMinionsMediator)target).guiService = (global::Kampai.UI.View.IGUIService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteCallMinionsMediator)target).localService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteCallMinionsMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CallMinionSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteCallMinionsMediator)target).callMinionSignal = (global::Kampai.Game.CallMinionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideAllWayFindersSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteCallMinionsMediator)target).hideAllWayFindersSignal = (global::Kampai.UI.View.HideAllWayFindersSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowHUDSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteCallMinionsMediator)target).showHUDSignal = (global::Kampai.UI.View.ShowHUDSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowStoreSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteCallMinionsMediator)target).showStoreSignal = (global::Kampai.UI.View.ShowStoreSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteCallMinionsMediator)target).gameContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteCallMinionsMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MignetteCallMinionsView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteCallMinionsMediator)target).view = (global::Kampai.UI.View.MignetteCallMinionsView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIAddedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteCallMinionsMediator)target).uiAddedSignal = (global::Kampai.UI.View.UIAddedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIRemovedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteCallMinionsMediator)target).uiRemovedSignal = (global::Kampai.UI.View.UIRemovedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseAllOtherMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteCallMinionsMediator)target).closeAllOtherMenuSignal = (global::Kampai.UI.View.CloseAllOtherMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteCallMinionsMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[15]
			{
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				global::Kampai.Game.GameElement.CONTEXT,
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
