namespace __preref_Kampai_Game_View_TikiBarBuildingObjectMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.TikiBarBuildingObjectMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[30]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.TikiBarBuildingObjectView), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).view = (global::Kampai.Game.View.TikiBarBuildingObjectView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPrestigeService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).prestigeService = (global::Kampai.Game.IPrestigeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IQuestService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).questService = (global::Kampai.Game.IQuestService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RestoreMinionAtTikiBarSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).restoreMinionSignal = (global::Kampai.Game.RestoreMinionAtTikiBarSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MinionStateChangeSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).minionChangeStateSignal = (global::Kampai.Game.MinionStateChangeSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowQuestPanelSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).showPanelSignal = (global::Kampai.UI.View.ShowQuestPanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowQuestRewardSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).showQuestRewardSignal = (global::Kampai.UI.View.ShowQuestRewardSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PathCharacterToTikiBarSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).pathCharacterToTikibarSignal = (global::Kampai.Game.PathCharacterToTikiBarSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.TeleportCharacterToTikiBarSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).teleportCharacterToTikibarSignal = (global::Kampai.Game.TeleportCharacterToTikiBarSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.UnveilCharacterObjectSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).unveilCharacterSignal = (global::Kampai.Game.UnveilCharacterObjectSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BeginCharacterLoopAnimationSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).characterLoopAnimationSignal = (global::Kampai.Game.BeginCharacterLoopAnimationSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PopUnleashedCharacterToTikiBarSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).popUnleashedCharacterToTikibarSignal = (global::Kampai.Game.PopUnleashedCharacterToTikiBarSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ReleaseMinionFromTikiBarSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).releaseMinionFromTikiBarSignal = (global::Kampai.Game.ReleaseMinionFromTikiBarSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.NamedCharacterRemovedFromTikiBarSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).removedFromTikibarSignal = (global::Kampai.Game.NamedCharacterRemovedFromTikiBarSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CharacterIntroCompleteSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).introCompleteSignal = (global::Kampai.Game.CharacterIntroCompleteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CharacterDrinkingCompleteSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).drinkingCompleteSignal = (global::Kampai.Game.CharacterDrinkingCompleteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.DisplayLevelUpRewardSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).displayLevelUpRewardSignal = (global::Kampai.UI.View.DisplayLevelUpRewardSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ToggleStickerbookGlowSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).glowSignal = (global::Kampai.Game.ToggleStickerbookGlowSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.GetNewQuestSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).getNewQuestSignal = (global::Kampai.Game.GetNewQuestSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).worldCanvas = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ToggleHitboxSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).toggleHitboxSignal = (global::Kampai.Game.ToggleHitboxSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.TikiBarSetAnimParamSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).setAnimParamSignal = (global::Kampai.Game.TikiBarSetAnimParamSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.UnlockCharacterModel), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).characterModel = (global::Kampai.Game.UnlockCharacterModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RelocateCharacterSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).relocateCharacterSignal = (global::Kampai.Game.RelocateCharacterSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Environment), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).environment = (global::Kampai.Game.Environment)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).dispatcher = (global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TikiBarBuildingObjectMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			object[] array = new object[30];
			array[22] = global::Kampai.Main.MainElement.UI_WORLDCANVAS;
			array[28] = global::strange.extensions.context.api.ContextKeys.CONTEXT_DISPATCHER;
			array[29] = global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW;
			SetterNames = array;
		}
	}
}
