namespace __preref_Kampai_UI_View_StorageBuildingModalMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.StorageBuildingModalMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[37]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.IGUIService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).guiService = (global::Kampai.UI.View.IGUIService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).localizationService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UpdateStorageItemsSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).updateStorageItemsSignal = (global::Kampai.UI.View.UpdateStorageItemsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseAllOtherMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).closeAllMenuSignal = (global::Kampai.UI.View.CloseAllOtherMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).soundFXSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).localService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ICurrencyService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).currencyService = (global::Kampai.Game.ICurrencyService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(RushDialogPurchaseHelper), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).rushDialogPurchaseHelper = (RushDialogPurchaseHelper)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideSkrimSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).hideSignal = (global::Kampai.UI.View.HideSkrimSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BuildingChangeStateSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).stateChangeSignal = (global::Kampai.Game.BuildingChangeStateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetStorageCapacitySignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).storageCapacitySignal = (global::Kampai.UI.View.SetStorageCapacitySignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).gameContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IMarketplaceService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).marketplaceService = (global::Kampai.Game.IMarketplaceService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.EnableStorageBuildingItemDescriptionSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).enableItemDescriptionSignal = (global::Kampai.UI.View.EnableStorageBuildingItemDescriptionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UpdateSaleSlotsStateSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).updateSaleSlotsStateSignal = (global::Kampai.UI.View.UpdateSaleSlotsStateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MarketplaceOpenSalePanelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).openSalePanelSignal = (global::Kampai.UI.View.MarketplaceOpenSalePanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MarketplaceOpenBuyPanelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).openBuyPanelSignal = (global::Kampai.UI.View.MarketplaceOpenBuyPanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MarketplaceCloseSalePanelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).closeSalePanelSignal = (global::Kampai.UI.View.MarketplaceCloseSalePanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MarketplaceCloseBuyPanelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).closeBuyPanel = (global::Kampai.UI.View.MarketplaceCloseBuyPanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MarketplaceCloseAllSalePanels), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).closeAllSalePanels = (global::Kampai.UI.View.MarketplaceCloseAllSalePanels)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetNewSellItemSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).sellItemSignal = (global::Kampai.UI.View.SetNewSellItemSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.RemoveFloatingTextSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).removeFloatingTextSignal = (global::Kampai.UI.View.RemoveFloatingTextSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(ILocalPersistanceService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).localPersistance = (ILocalPersistanceService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ISocialService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).facebookService = (global::Kampai.Game.ISocialService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.PopupMessageSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).popupMessageSignal = (global::Kampai.UI.View.PopupMessageSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.GenerateBuyItemsSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).generateBuyItemsSignal = (global::Kampai.Game.GenerateBuyItemsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.RemoveStorageBuildingItemDescriptionSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).removeItemDescriptionSignal = (global::Kampai.UI.View.RemoveStorageBuildingItemDescriptionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SelectStorageBuildingItemSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).selectStorageBuildingItemSignal = (global::Kampai.UI.View.SelectStorageBuildingItemSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.StorageBuildingModalView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).view = (global::Kampai.UI.View.StorageBuildingModalView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIAddedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).uiAddedSignal = (global::Kampai.UI.View.UIAddedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIRemovedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).uiRemovedSignal = (global::Kampai.UI.View.UIRemovedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseAllOtherMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).closeAllOtherMenuSignal = (global::Kampai.UI.View.CloseAllOtherMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingModalMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			object[] array = new object[37];
			array[13] = global::Kampai.Game.GameElement.CONTEXT;
			array[25] = global::Kampai.Game.SocialServices.FACEBOOK;
			array[36] = global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW;
			SetterNames = array;
		}
	}
}
