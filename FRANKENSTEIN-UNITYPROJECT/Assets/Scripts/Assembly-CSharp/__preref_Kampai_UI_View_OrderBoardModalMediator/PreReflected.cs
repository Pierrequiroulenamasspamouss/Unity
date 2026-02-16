namespace __preref_Kampai_UI_View_OrderBoardModalMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.OrderBoardModalMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[34]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.IGUIService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).guiService = (global::Kampai.UI.View.IGUIService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OrderBoardTicketClickedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).ticketClicked = (global::Kampai.UI.View.OrderBoardTicketClickedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OrderBoardTicketDeletedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).ticketDeletedSignal = (global::Kampai.UI.View.OrderBoardTicketDeletedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OrderBoardPrestigeSlotFullSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).prestigeSlotFullSignal = (global::Kampai.UI.View.OrderBoardPrestigeSlotFullSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.OrderBoardFillOrderSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).fillOrderSignal = (global::Kampai.Game.OrderBoardFillOrderSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.OrderBoardDeleteOrderSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).deleteOrderSignal = (global::Kampai.Game.OrderBoardDeleteOrderSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.OrderBoardRefillTicketSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).refillTicketSignal = (global::Kampai.Game.OrderBoardRefillTicketSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.RushDialogConfirmationSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).dialogConfirmedSignal = (global::Kampai.UI.View.RushDialogConfirmationSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.OrderBoardTransactionFailedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).transactionFailedSignal = (global::Kampai.Game.OrderBoardTransactionFailedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.LoadBuddyBarSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).loadBuddyBarSignal = (global::Kampai.UI.View.LoadBuddyBarSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideSkrimSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).hideSkrim = (global::Kampai.UI.View.HideSkrimSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeEventService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).timeEventService = (global::Kampai.Game.ITimeEventService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).localService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPrestigeService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).characterService = (global::Kampai.Game.IPrestigeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).soundFXSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OrderBoardStartFillingPrestigeBarSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).startFillingPrestigeBarSignal = (global::Kampai.UI.View.OrderBoardStartFillingPrestigeBarSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.OrderBoardFillOrderCompleteSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).fillOrderCompleteSignal = (global::Kampai.Game.OrderBoardFillOrderCompleteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetPremiumCurrencySignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).setPremiumCurrencySignal = (global::Kampai.UI.View.SetPremiumCurrencySignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ResetDoubleTapSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).resetDoubleTapSignal = (global::Kampai.UI.View.ResetDoubleTapSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetFTUETextSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).setFTUETextSignal = (global::Kampai.UI.View.SetFTUETextSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).gameContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.IPositionService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).positionService = (global::Kampai.UI.IPositionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.IFancyUIService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).fancyUIService = (global::Kampai.UI.IFancyUIService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.DoobersFlownSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).doobersFlownSignal = (global::Kampai.UI.View.DoobersFlownSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.MoveAudioListenerSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).moveAudioListener = (global::Kampai.Main.MoveAudioListenerSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OrderBoardModalView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).view = (global::Kampai.UI.View.OrderBoardModalView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIAddedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).uiAddedSignal = (global::Kampai.UI.View.UIAddedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIRemovedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).uiRemovedSignal = (global::Kampai.UI.View.UIRemovedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseAllOtherMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).closeAllOtherMenuSignal = (global::Kampai.UI.View.CloseAllOtherMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardModalMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			object[] array = new object[34];
			array[24] = global::Kampai.Game.GameElement.CONTEXT;
			array[33] = global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW;
			SetterNames = array;
		}
	}
}
