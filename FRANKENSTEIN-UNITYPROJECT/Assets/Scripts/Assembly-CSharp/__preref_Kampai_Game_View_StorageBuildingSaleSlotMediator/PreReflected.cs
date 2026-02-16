namespace __preref_Kampai_Game_View_StorageBuildingSaleSlotMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.StorageBuildingSaleSlotMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[23]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.StorageBuildingSaleSlotView), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).view = (global::Kampai.UI.View.StorageBuildingSaleSlotView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IMarketplaceService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).marketplaceService = (global::Kampai.Game.IMarketplaceService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).soundFXSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ISocialService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).facebookService = (global::Kampai.Game.ISocialService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowSocialPartyFBConnectSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).showFacebookPopupSignal = (global::Kampai.UI.View.ShowSocialPartyFBConnectSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SocialLoginSuccessSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).loginSuccess = (global::Kampai.Game.SocialLoginSuccessSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Camera), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).uiCamera = (global::UnityEngine.Camera)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OpenCreateNewSalePanelSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).createNewSalePanelSignal = (global::Kampai.UI.View.OpenCreateNewSalePanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UpdateSaleSlotSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).updateSaleSlot = (global::Kampai.UI.View.UpdateSaleSlotSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UpdateSaleSlotsStateSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).updateSaleSlotState = (global::Kampai.UI.View.UpdateSaleSlotsStateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CollectMarketplaceSaleSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).collectSaleSignal = (global::Kampai.Game.CollectMarketplaceSaleSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PurchaseMarketplaceSlotSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).purchaseSlotSignal = (global::Kampai.Game.PurchaseMarketplaceSlotSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).gameContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).timeService = (global::Kampai.Game.ITimeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).localizationService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.RefreshSlotsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).refreshSlotsSignal = (global::Kampai.UI.View.RefreshSlotsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.UpdateMarketplaceSlotStateSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).updateSlotStateSignal = (global::Kampai.Game.UpdateMarketplaceSlotStateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseCreateNewSalePanelSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).closeCreateNewSaleSignal = (global::Kampai.UI.View.CloseCreateNewSalePanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StorageBuildingSaleSlotMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[23]
			{
				null,
				null,
				null,
				null,
				null,
				global::Kampai.Game.SocialServices.FACEBOOK,
				null,
				null,
				global::Kampai.UI.View.UIElement.CAMERA,
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
				null,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW
			};
		}
	}
}
