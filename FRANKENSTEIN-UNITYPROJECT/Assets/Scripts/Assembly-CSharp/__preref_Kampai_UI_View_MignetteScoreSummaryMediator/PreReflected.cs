namespace __preref_Kampai_UI_View_MignetteScoreSummaryMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.MignetteScoreSummaryMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[23]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).localizationService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.MignetteGameModel), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).mignetteGameModel = (global::Kampai.Game.Mignette.MignetteGameModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.IGUIService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).guiService = (global::Kampai.UI.View.IGUIService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StopMignetteSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).stopMignettSignal = (global::Kampai.Game.StopMignetteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MignetteCollectionService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).collectionService = (global::Kampai.Game.MignetteCollectionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoMoveToInstanceSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).autoMoveToInstanceSignal = (global::Kampai.Game.CameraAutoMoveToInstanceSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetHUDButtonsVisibleSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).setHUDButtonsVisibleSignal = (global::Kampai.UI.View.SetHUDButtonsVisibleSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CreditCollectionRewardSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).creditCollectionRewardSignal = (global::Kampai.Game.CreditCollectionRewardSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SpawnDooberSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).spawnDooberSignal = (global::Kampai.UI.View.SpawnDooberSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ITelemetryService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).telemetryService = (global::Kampai.Common.ITelemetryService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Camera), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).uiCamera = (global::UnityEngine.Camera)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalMusicSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).musicSignal = (global::Kampai.Main.PlayGlobalMusicSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).globalAudioSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideSkrimSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).hideSkrim = (global::Kampai.UI.View.HideSkrimSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoMoveToMignetteSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).moveToMignetteSignal = (global::Kampai.Game.CameraAutoMoveToMignetteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IQuestService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).questService = (global::Kampai.Game.IQuestService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MignetteScoreSummaryView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).view = (global::Kampai.UI.View.MignetteScoreSummaryView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIAddedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).uiAddedSignal = (global::Kampai.UI.View.UIAddedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIRemovedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).uiRemovedSignal = (global::Kampai.UI.View.UIRemovedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseAllOtherMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).closeAllOtherMenuSignal = (global::Kampai.UI.View.CloseAllOtherMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MignetteScoreSummaryMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[23]
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
				global::Kampai.UI.View.UIElement.CAMERA,
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
