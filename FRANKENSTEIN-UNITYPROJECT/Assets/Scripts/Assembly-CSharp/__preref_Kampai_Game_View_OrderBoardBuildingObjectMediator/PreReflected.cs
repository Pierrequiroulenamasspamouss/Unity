namespace __preref_Kampai_Game_View_OrderBoardBuildingObjectMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.OrderBoardBuildingObjectMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[17]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.OrderBoardBuildingObjectView), delegate(object target, object val)
				{
					((global::Kampai.Game.View.OrderBoardBuildingObjectMediator)target).view = (global::Kampai.Game.View.OrderBoardBuildingObjectView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.OrderBoardRefillTicketSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.OrderBoardBuildingObjectMediator)target).refillTicketSignal = (global::Kampai.Game.OrderBoardRefillTicketSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.OrderBoardStartRefillTicketSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.OrderBoardBuildingObjectMediator)target).startRefillTicketSignal = (global::Kampai.Game.OrderBoardStartRefillTicketSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.OrderBoardSetNewTicketSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.OrderBoardBuildingObjectMediator)target).setNewTicketSignal = (global::Kampai.Game.OrderBoardSetNewTicketSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SetupOrderBoardServiceSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.OrderBoardBuildingObjectMediator)target).setupOrderBoardServiceSignal = (global::Kampai.Game.SetupOrderBoardServiceSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PostTransactionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.OrderBoardBuildingObjectMediator)target).postTransactionSignal = (global::Kampai.Game.PostTransactionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.AwardLevelSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.OrderBoardBuildingObjectMediator)target).awardLevelSignal = (global::Kampai.Game.AwardLevelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.OrderBoardBuildingObjectMediator)target).timeService = (global::Kampai.Game.ITimeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.OrderBoardBuildingObjectMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Game.View.OrderBoardBuildingObjectMediator)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IOrderBoardService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.OrderBoardBuildingObjectMediator)target).orderBoardService = (global::Kampai.Game.IOrderBoardService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPrestigeService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.OrderBoardBuildingObjectMediator)target).prestigeService = (global::Kampai.Game.IPrestigeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.OrderBoardUpdateTicketOnBoardSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.OrderBoardBuildingObjectMediator)target).updateTicketOnBoardSignal = (global::Kampai.Game.OrderBoardUpdateTicketOnBoardSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ToggleHitboxSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.OrderBoardBuildingObjectMediator)target).toggleHitboxSignal = (global::Kampai.Game.ToggleHitboxSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.View.OrderBoardBuildingObjectMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher), delegate(object target, object val)
				{
					((global::Kampai.Game.View.OrderBoardBuildingObjectMediator)target).dispatcher = (global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.OrderBoardBuildingObjectMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[17]
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
				global::strange.extensions.context.api.ContextKeys.CONTEXT_DISPATCHER,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW
			};
		}
	}
}
