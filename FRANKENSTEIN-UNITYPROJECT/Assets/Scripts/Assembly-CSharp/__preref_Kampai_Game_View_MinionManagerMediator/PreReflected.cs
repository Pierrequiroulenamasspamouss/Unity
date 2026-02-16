namespace __preref_Kampai_Game_View_MinionManagerMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.MinionManagerMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[40]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.MinionManagerView), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).view = (global::Kampai.Game.View.MinionManagerView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MinionMoveToSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).minionMoveToSignal = (global::Kampai.Game.MinionMoveToSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.AddMinionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).addMinionSignal = (global::Kampai.Game.AddMinionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MinionWalkPathSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).minionWalkPathSignal = (global::Kampai.Game.MinionWalkPathSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MinionRunPathSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).minionRunPathSignal = (global::Kampai.Game.MinionRunPathSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MinionAppearSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).minionAppearSignal = (global::Kampai.Game.MinionAppearSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.AnimateSelectedMinionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).animateSelectedMinionSignal = (global::Kampai.Game.AnimateSelectedMinionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MinionStateChangeSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).stateChangeSignal = (global::Kampai.Game.MinionStateChangeSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StartMinionRouteSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).startMinionRouteSignal = (global::Kampai.Game.StartMinionRouteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StartTeleportTaskSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).startTeleportTaskSignal = (global::Kampai.Game.StartTeleportTaskSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StartTaskSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).startTaskSignal = (global::Kampai.Game.StartTaskSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SignalActionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).stopTaskSignal = (global::Kampai.Game.SignalActionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RelocateCharacterSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).relocateSignal = (global::Kampai.Game.RelocateCharacterSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.IRandomService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).randomService = (global::Kampai.Common.IRandomService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPrestigeService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).characterService = (global::Kampai.Game.IPrestigeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StartGroupGachaSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).startGroupGachaSignal = (global::Kampai.Game.StartGroupGachaSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.DeselectAllMinionsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).deselectAllMinionsSignal = (global::Kampai.Common.DeselectAllMinionsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StartIncidentalAnimationSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).startIncidentalAnimationSignal = (global::Kampai.Game.StartIncidentalAnimationSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MinionAcknowledgeSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).minionAcknowledgeSignal = (global::Kampai.Game.MinionAcknowledgeSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.PathFinder), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).pathFinder = (global::Kampai.Util.PathFinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.PickControllerModel), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).model = (global::Kampai.Common.PickControllerModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).buildingManager = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.UpdateTaskedMinionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).updateTaskedMinionSignal = (global::Kampai.Game.UpdateTaskedMinionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RestoreMinionStateSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).restoreMinionSignal = (global::Kampai.Game.RestoreMinionStateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ReadyAnimationSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).readyAnimationSignal = (global::Kampai.Game.ReadyAnimationSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MinionReactSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).reactSignal = (global::Kampai.Game.MinionReactSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.EnableMinionRendererSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).enableRendererSignal = (global::Kampai.Game.EnableMinionRendererSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MoveMinionFinishedSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).moveMinionFinishedSignal = (global::Kampai.Game.MoveMinionFinishedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PlayMinionNoAnimAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).playMinionNoAnimAudioSignal = (global::Kampai.Game.PlayMinionNoAnimAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.AddMinionToTikiBarSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).addMinionTikiBarSignal = (global::Kampai.Game.AddMinionToTikiBarSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MinionSeekPositionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).minionSeekPositionSignal = (global::Kampai.Game.MinionSeekPositionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SetPartyStatesSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).setPartyStateSignal = (global::Kampai.Game.SetPartyStatesSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.TapMinionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).tapMinionSignal = (global::Kampai.Game.TapMinionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ICoroutineProgressMonitor), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).coroutineProgressMonitor = (global::Kampai.Util.ICoroutineProgressMonitor)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).dispatcher = (global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.MinionManagerMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			object[] array = new object[40];
			array[24] = global::Kampai.Game.GameElement.BUILDING_MANAGER;
			array[38] = global::strange.extensions.context.api.ContextKeys.CONTEXT_DISPATCHER;
			array[39] = global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW;
			SetterNames = array;
		}
	}
}
