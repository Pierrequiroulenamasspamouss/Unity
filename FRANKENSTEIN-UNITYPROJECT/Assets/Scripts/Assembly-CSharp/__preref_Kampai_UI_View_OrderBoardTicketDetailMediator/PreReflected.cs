namespace __preref_Kampai_UI_View_OrderBoardTicketDetailMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.OrderBoardTicketDetailMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[18]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OrderBoardTicketDetailView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardTicketDetailMediator)target).view = (global::Kampai.UI.View.OrderBoardTicketDetailView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OrderBoardTicketClickedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardTicketDetailMediator)target).ticketClickedSignal = (global::Kampai.UI.View.OrderBoardTicketClickedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OrderBoardTicketDeletedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardTicketDetailMediator)target).ticketDeletedSignal = (global::Kampai.UI.View.OrderBoardTicketDeletedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OrderBoardPrestigeSlotFullSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardTicketDetailMediator)target).slotFullSignal = (global::Kampai.UI.View.OrderBoardPrestigeSlotFullSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OrderBoardStartFillingPrestigeBarSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardTicketDetailMediator)target).startFillingPrestigeBarSignal = (global::Kampai.UI.View.OrderBoardStartFillingPrestigeBarSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardTicketDetailMediator)target).localService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardTicketDetailMediator)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPrestigeService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardTicketDetailMediator)target).characterService = (global::Kampai.Game.IPrestigeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardTicketDetailMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetFTUETextSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardTicketDetailMediator)target).setFTUETextSignal = (global::Kampai.UI.View.SetFTUETextSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideItemPopupSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardTicketDetailMediator)target).hideItemPopupSignal = (global::Kampai.UI.View.HideItemPopupSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.DisplayItemPopupSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardTicketDetailMediator)target).displayItemPopupSignal = (global::Kampai.UI.View.DisplayItemPopupSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.IFancyUIService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardTicketDetailMediator)target).fancyUIService = (global::Kampai.UI.IFancyUIService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.AppPauseSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardTicketDetailMediator)target).pauseSignal = (global::Kampai.Common.AppPauseSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardTicketDetailMediator)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardTicketDetailMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.MoveAudioListenerSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardTicketDetailMediator)target).moveAudioListener = (global::Kampai.Main.MoveAudioListenerSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.OrderBoardTicketDetailMediator)target).contextView = (global::UnityEngine.GameObject)val;
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
