namespace __preref_Kampai_UI_View_CreateNewSellMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.CreateNewSellMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[18]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CreateNewSellView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreateNewSellMediator)target).view = (global::Kampai.UI.View.CreateNewSellView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IMarketplaceService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreateNewSellMediator)target).marketplaceService = (global::Kampai.Game.IMarketplaceService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreateNewSellMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreateNewSellMediator)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetNewSellItemSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreateNewSellMediator)target).setNewSellItemSignal = (global::Kampai.UI.View.SetNewSellItemSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MarketplaceCloseAllSalePanels), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreateNewSellMediator)target).closeSalePanelSignal = (global::Kampai.UI.View.MarketplaceCloseAllSalePanels)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreateNewSellMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OpenCreateNewSalePanelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreateNewSellMediator)target).openSalePanelSignal = (global::Kampai.UI.View.OpenCreateNewSalePanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseCreateNewSalePanelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreateNewSellMediator)target).CloseCreateNewSalePanelSignal = (global::Kampai.UI.View.CloseCreateNewSalePanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MarketplaceOpenSalePanelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreateNewSellMediator)target).openSaleSignal = (global::Kampai.UI.View.MarketplaceOpenSalePanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MarketplaceCloseSalePanelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreateNewSellMediator)target).closeSaleSignal = (global::Kampai.UI.View.MarketplaceCloseSalePanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreateNewSellMediator)target).gameContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreateNewSellMediator)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ICoppaService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreateNewSellMediator)target).coppaService = (global::Kampai.Common.ICoppaService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SelectStorageBuildingItemSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreateNewSellMediator)target).selectStorageBuildingItemSignal = (global::Kampai.UI.View.SelectStorageBuildingItemSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreateNewSellMediator)target).soundFXSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetStorageCapacitySignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreateNewSellMediator)target).setStorageSignal = (global::Kampai.UI.View.SetStorageCapacitySignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CreateNewSellMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[18]
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
