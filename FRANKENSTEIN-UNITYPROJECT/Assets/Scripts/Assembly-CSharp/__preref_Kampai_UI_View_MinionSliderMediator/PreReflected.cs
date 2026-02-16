namespace __preref_Kampai_UI_View_MinionSliderMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.MinionSliderMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[19]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MinionSliderView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MinionSliderMediator)target).view = (global::Kampai.UI.View.MinionSliderView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UpdateSliderSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MinionSliderMediator)target).updateSliderSignal = (global::Kampai.UI.View.UpdateSliderSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ResetDoubleTapSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MinionSliderMediator)target).resetDoubleTapSignal = (global::Kampai.UI.View.ResetDoubleTapSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MinionSliderMediator)target).globalSFXSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MinionSliderMediator)target).gameContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MinionSliderMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MinionSliderMediator)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MinionSliderMediator)target).timeService = (global::Kampai.Game.ITimeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeEventService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MinionSliderMediator)target).timeEventService = (global::Kampai.Game.ITimeEventService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CallMinionSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MinionSliderMediator)target).callMinionSignal = (global::Kampai.Game.CallMinionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.FinishCallMinionSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MinionSliderMediator)target).finishCallMinionSignal = (global::Kampai.Game.FinishCallMinionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MinionSliderMediator)target).localService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UITryHarvestSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MinionSliderMediator)target).tryHarvestSignal = (global::Kampai.UI.View.UITryHarvestSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetPremiumCurrencySignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MinionSliderMediator)target).setPremiumCurrencySignal = (global::Kampai.UI.View.SetPremiumCurrencySignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UpdateIdleMinionCountSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MinionSliderMediator)target).updateMinionCountSignal = (global::Kampai.UI.View.UpdateIdleMinionCountSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UpdateSlotPurchaseButtonSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MinionSliderMediator)target).updatePurchaseSlotSignal = (global::Kampai.UI.View.UpdateSlotPurchaseButtonSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BuildingHarvestSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MinionSliderMediator)target).buildingHarvestSignal = (global::Kampai.Game.BuildingHarvestSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MinionSliderMediator)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MinionSliderMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[19]
			{
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
