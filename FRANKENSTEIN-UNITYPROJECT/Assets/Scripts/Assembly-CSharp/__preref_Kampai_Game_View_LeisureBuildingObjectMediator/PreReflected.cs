namespace __preref_Kampai_Game_View_LeisureBuildingObjectMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.LeisureBuildingObjectMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[15]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.LeisureBuildingObjectView), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LeisureBuildingObjectMediator)target).view = (global::Kampai.Game.View.LeisureBuildingObjectView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LeisureBuildingObjectMediator)target).minionManager = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RouteMinionToLeisureSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LeisureBuildingObjectMediator)target).routeMinionToLeisureSignal = (global::Kampai.Game.RouteMinionToLeisureSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.LeisureBuildingCooldownSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LeisureBuildingObjectMediator)target).cooldownSignal = (global::Kampai.Game.LeisureBuildingCooldownSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeEventService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LeisureBuildingObjectMediator)target).timeEventService = (global::Kampai.Game.ITimeEventService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BuildingChangeStateSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LeisureBuildingObjectMediator)target).changeStateSignal = (global::Kampai.Game.BuildingChangeStateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.TeleportMinionToLeisureSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LeisureBuildingObjectMediator)target).teleportMinionToLeisureSignal = (global::Kampai.Game.TeleportMinionToLeisureSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MinionStateChangeSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LeisureBuildingObjectMediator)target).minionStateChangeSignal = (global::Kampai.Game.MinionStateChangeSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.KillFunSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LeisureBuildingObjectMediator)target).killFunSignal = (global::Kampai.Game.KillFunSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.KillFunWithMinionStateSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LeisureBuildingObjectMediator)target).killFunWithMinionStateSignal = (global::Kampai.Game.KillFunWithMinionStateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LeisureBuildingObjectMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LeisureBuildingObjectMediator)target).timeService = (global::Kampai.Game.ITimeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LeisureBuildingObjectMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LeisureBuildingObjectMediator)target).dispatcher = (global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LeisureBuildingObjectMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[15]
			{
				null,
				global::Kampai.Game.GameElement.MINION_MANAGER,
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
