namespace __preref_Kampai_UI_View_QuestPanelMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.QuestPanelMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[29]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.QuestDetailIDSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).idSignal = (global::Kampai.UI.View.QuestDetailIDSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseAllOtherMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).closeSignal = (global::Kampai.UI.View.CloseAllOtherMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseQuestBookSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).closeQuestSignal = (global::Kampai.UI.View.CloseQuestBookSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.IGUIService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).guiService = (global::Kampai.UI.View.IGUIService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).localService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UpdateQuestPanelWithNewQuestSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).updateQuestPanelSignal = (global::Kampai.UI.View.UpdateQuestPanelWithNewQuestSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UpdateQuestLineProgressSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).updateQuestLineProgressSignal = (global::Kampai.UI.View.UpdateQuestLineProgressSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).playSoundFXSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideSkrimSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).hideSkrimSignal = (global::Kampai.UI.View.HideSkrimSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IQuestService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).questService = (global::Kampai.Game.IQuestService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowQuestRewardSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).questRewardSignal = (global::Kampai.UI.View.ShowQuestRewardSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.FTUEProgressSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).FTUEsignal = (global::Kampai.UI.View.FTUEProgressSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.FTUEQuestPanelCloseSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).FTUECloseMenuSignal = (global::Kampai.UI.View.FTUEQuestPanelCloseSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).timeService = (global::Kampai.Game.ITimeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.QuestUIModel), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).questUIModel = (global::Kampai.UI.View.QuestUIModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowHUDSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).showHUDSignal = (global::Kampai.UI.View.ShowHUDSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IZoomCameraModel), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).zoomCameraModel = (global::Kampai.Game.IZoomCameraModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).soundFXSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPrestigeService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).prestigeService = (global::Kampai.Game.IPrestigeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.IFancyUIService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).fancyUIService = (global::Kampai.UI.IFancyUIService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.MoveAudioListenerSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).toggleCharacterAudioSignal = (global::Kampai.Main.MoveAudioListenerSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowSettingsButtonSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).showSettingsButtonSignal = (global::Kampai.UI.View.ShowSettingsButtonSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.QuestPanelView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).view = (global::Kampai.UI.View.QuestPanelView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIAddedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).uiAddedSignal = (global::Kampai.UI.View.UIAddedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIRemovedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).uiRemovedSignal = (global::Kampai.UI.View.UIRemovedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseAllOtherMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).closeAllOtherMenuSignal = (global::Kampai.UI.View.CloseAllOtherMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestPanelMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			object[] array = new object[29];
			array[28] = global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW;
			SetterNames = array;
		}
	}
}
