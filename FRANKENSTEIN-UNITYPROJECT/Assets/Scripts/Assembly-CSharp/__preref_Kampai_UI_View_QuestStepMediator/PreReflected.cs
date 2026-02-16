namespace __preref_Kampai_UI_View_QuestStepMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.QuestStepMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[26]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.QuestStepView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).view = (global::Kampai.UI.View.QuestStepView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeEventService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).timeEventService = (global::Kampai.Game.ITimeEventService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).localService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseQuestBookSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).closeSignal = (global::Kampai.UI.View.CloseQuestBookSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideSkrimSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).hideSkrim = (global::Kampai.UI.View.HideSkrimSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.QuestStepRushSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).stepRushSignal = (global::Kampai.Game.QuestStepRushSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).globalSFXSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoMoveToBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).moveToBuildingSignal = (global::Kampai.Game.CameraAutoMoveToBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoMoveToMignetteSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).moveToMignetteSignal = (global::Kampai.Game.CameraAutoMoveToMignetteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).gameContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OpenStoreHighlightItemSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).openStoreSignal = (global::Kampai.UI.View.OpenStoreHighlightItemSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UpdateQuestPanelSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).updateQuestPanelSignal = (global::Kampai.UI.View.UpdateQuestPanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.FTUEProgressSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).ftueSignal = (global::Kampai.UI.View.FTUEProgressSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.FTUEQuestGoToSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).ftueGoToSignal = (global::Kampai.UI.View.FTUEQuestGoToSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.FTUEQuestFinished), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).ftueQuestFinished = (global::Kampai.UI.View.FTUEQuestFinished)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.DeliverTaskItemSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).deliverTaskItemSignal = (global::Kampai.Game.DeliverTaskItemSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IZoomCameraModel), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).zoomCameraModel = (global::Kampai.Game.IZoomCameraModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CraftingModalParams), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).craftingModalParams = (global::Kampai.UI.View.CraftingModalParams)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.RushRevealBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).rushRevealBuildingSignal = (global::Kampai.UI.View.RushRevealBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ConstructionCompleteSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).constructionCompleteSignal = (global::Kampai.Game.ConstructionCompleteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoZoomSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).autoZoomSignal = (global::Kampai.Game.CameraAutoZoomSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Camera), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).myCamera = (global::UnityEngine.Camera)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.QuestStepMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[26]
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
				global::Kampai.Main.MainElement.CAMERA,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW
			};
		}
	}
}
