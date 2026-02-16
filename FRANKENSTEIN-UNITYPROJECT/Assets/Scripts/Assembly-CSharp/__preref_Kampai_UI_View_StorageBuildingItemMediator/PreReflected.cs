namespace __preref_Kampai_UI_View_StorageBuildingItemMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.StorageBuildingItemMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[18]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.StorageBuildingItemView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingItemMediator)target).view = (global::Kampai.UI.View.StorageBuildingItemView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IMarketplaceService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingItemMediator)target).marketplaceService = (global::Kampai.Game.IMarketplaceService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetNewSellItemSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingItemMediator)target).sellItemSignal = (global::Kampai.UI.View.SetNewSellItemSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Camera), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingItemMediator)target).uiCamera = (global::UnityEngine.Camera)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingItemMediator)target).soundFXSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.AppPauseSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingItemMediator)target).pauseSignal = (global::Kampai.Common.AppPauseSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingItemMediator)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.IGUIService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingItemMediator)target).guiService = (global::Kampai.UI.View.IGUIService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.RemoveStorageBuildingItemDescriptionSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingItemMediator)target).removeItemDescriptionSignal = (global::Kampai.UI.View.RemoveStorageBuildingItemDescriptionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SelectStorageBuildingItemSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingItemMediator)target).selectStorageBuildingItemSignal = (global::Kampai.UI.View.SelectStorageBuildingItemSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.EnableStorageBuildingItemDescriptionSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingItemMediator)target).enableItemDescriptionSignal = (global::Kampai.UI.View.EnableStorageBuildingItemDescriptionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OpenCreateNewSalePanelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingItemMediator)target).openCreateNewSalePanelSignal = (global::Kampai.UI.View.OpenCreateNewSalePanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseCreateNewSalePanelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingItemMediator)target).closeCreateNewSalePanelSignal = (global::Kampai.UI.View.CloseCreateNewSalePanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MarketplaceOpenSalePanelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingItemMediator)target).openSalePanelSignal = (global::Kampai.UI.View.MarketplaceOpenSalePanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MarketplaceCloseSalePanelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingItemMediator)target).closeSalePanelSignal = (global::Kampai.UI.View.MarketplaceCloseSalePanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MarketplaceOpenBuyPanelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingItemMediator)target).openBuyPanelSignal = (global::Kampai.UI.View.MarketplaceOpenBuyPanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MarketplaceCloseBuyPanelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingItemMediator)target).closeBuyPanelSignal = (global::Kampai.UI.View.MarketplaceCloseBuyPanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.StorageBuildingItemMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[18]
			{
				null,
				null,
				null,
				global::Kampai.UI.View.UIElement.CAMERA,
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
