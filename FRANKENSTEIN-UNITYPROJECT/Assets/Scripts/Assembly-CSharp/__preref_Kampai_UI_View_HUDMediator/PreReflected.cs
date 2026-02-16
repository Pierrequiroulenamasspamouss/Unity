namespace __preref_Kampai_UI_View_HUDMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.HUDMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[41]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HUDView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).view = (global::Kampai.UI.View.HUDView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseHUDSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).closeSignal = (global::Kampai.UI.View.CloseHUDSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseAllOtherMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).closeAllOtherMenusSignal = (global::Kampai.UI.View.CloseAllOtherMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetLevelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).setLevelSignal = (global::Kampai.UI.View.SetLevelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetXPSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).setXPSignal = (global::Kampai.UI.View.SetXPSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetGrindCurrencySignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).setGrindCurrencySignal = (global::Kampai.UI.View.SetGrindCurrencySignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetPremiumCurrencySignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).setPremiumCurrencySignal = (global::Kampai.UI.View.SetPremiumCurrencySignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetStorageCapacitySignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).setStorageSignal = (global::Kampai.UI.View.SetStorageCapacitySignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).playSFXSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowPremiumStoreSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).showPremiumStoreSignal = (global::Kampai.UI.View.ShowPremiumStoreSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowGrindStoreSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).showGrindStoreSignal = (global::Kampai.UI.View.ShowGrindStoreSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowHUDSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).showHUDSignal = (global::Kampai.UI.View.ShowHUDSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowSettingsButtonSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).showSettingsButtonSignal = (global::Kampai.UI.View.ShowSettingsButtonSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.PeekHUDSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).peekHUDSignal = (global::Kampai.UI.View.PeekHUDSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.TogglePopupForHUDSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).togglePopupSignal = (global::Kampai.UI.View.TogglePopupForHUDSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetHUDButtonsVisibleSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).setHUDButtonsVisibleSignal = (global::Kampai.UI.View.SetHUDButtonsVisibleSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ICurrencyService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).currencyService = (global::Kampai.Game.ICurrencyService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIAddedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).uiAddedSignal = (global::Kampai.UI.View.UIAddedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIRemovedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).uiRemovedSignal = (global::Kampai.UI.View.UIRemovedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.XPFTUEHighlightSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).ftueHighlightSignal = (global::Kampai.UI.View.XPFTUEHighlightSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowAllWayFindersSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).showAllWayFindersSignal = (global::Kampai.UI.View.ShowAllWayFindersSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideAllWayFindersSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).hideAllWayFindersSignal = (global::Kampai.UI.View.HideAllWayFindersSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowAllResourceIconsSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).showAllResourceIconsSignal = (global::Kampai.UI.View.ShowAllResourceIconsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideAllResourceIconsSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).hideAllResourceIconsSignal = (global::Kampai.UI.View.HideAllResourceIconsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HUDChangedSiblingIndexSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).hudChangingSiblingIndexSignal = (global::Kampai.UI.View.HUDChangedSiblingIndexSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.FireXPVFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).fireXpSignal = (global::Kampai.UI.View.FireXPVFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.FirePremiumVFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).firePremiumSignal = (global::Kampai.UI.View.FirePremiumVFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.FireGrindVFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).fireGrindSignal = (global::Kampai.UI.View.FireGrindVFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.IGUIService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).guiService = (global::Kampai.UI.View.IGUIService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideDelayHUDSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).hideDelayHUDSignal = (global::Kampai.UI.View.HideDelayHUDSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CheckForLevelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).levelSignal = (global::Kampai.UI.View.CheckForLevelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.StopAutopanSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).stopAutopanSignal = (global::Kampai.UI.View.StopAutopanSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IQuestService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).questService = (global::Kampai.Game.IQuestService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).localService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CurrencyDialogClosedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).currencyDialogClosedSignal = (global::Kampai.UI.View.CurrencyDialogClosedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.OpenStorageBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).OpenStorageBuildingSignal = (global::Kampai.Game.OpenStorageBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ICoppaService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).coppaService = (global::Kampai.Common.ICoppaService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).dispatcher = (global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HUDMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			object[] array = new object[41];
			array[39] = global::strange.extensions.context.api.ContextKeys.CONTEXT_DISPATCHER;
			array[40] = global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW;
			SetterNames = array;
		}
	}
}
