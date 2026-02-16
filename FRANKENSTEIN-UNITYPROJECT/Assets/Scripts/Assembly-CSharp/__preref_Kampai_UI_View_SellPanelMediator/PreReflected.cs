namespace __preref_Kampai_UI_View_SellPanelMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.SellPanelMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[22]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).localService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ISocialService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).facebookService = (global::Kampai.Game.ISocialService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OpenCreateNewSalePanelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).openSalePanelSignal = (global::Kampai.UI.View.OpenCreateNewSalePanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseCreateNewSalePanelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).closeCreateNewSaleSignal = (global::Kampai.UI.View.CloseCreateNewSalePanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.RefreshSlotsSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).refreshSlotsSignal = (global::Kampai.UI.View.RefreshSlotsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MarketplaceOpenSalePanelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).openSalePanel = (global::Kampai.UI.View.MarketplaceOpenSalePanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MarketplaceCloseSalePanelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).closeSalePanelSignal = (global::Kampai.UI.View.MarketplaceCloseSalePanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MarketplaceCloseAllSalePanels), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).closeAllSalePanels = (global::Kampai.UI.View.MarketplaceCloseAllSalePanels)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.UpdateMarketplaceSaleOrderSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).updateSaleOrderSignal = (global::Kampai.Game.UpdateMarketplaceSaleOrderSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.EnableStorageBuildingItemDescriptionSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).enableItemDescriptionSignal = (global::Kampai.UI.View.EnableStorageBuildingItemDescriptionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SelectStorageBuildingItemSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).selectStorageBuildingItemSignal = (global::Kampai.UI.View.SelectStorageBuildingItemSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).soundFXSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SocialLoginSuccessSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).loginSuccess = (global::Kampai.Game.SocialLoginSuccessSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SellPanelView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).view = (global::Kampai.UI.View.SellPanelView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIAddedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).uiAddedSignal = (global::Kampai.UI.View.UIAddedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIRemovedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).uiRemovedSignal = (global::Kampai.UI.View.UIRemovedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseAllOtherMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).closeAllOtherMenuSignal = (global::Kampai.UI.View.CloseAllOtherMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SellPanelMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[22]
			{
				null,
				null,
				null,
				null,
				global::Kampai.Game.SocialServices.FACEBOOK,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
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
