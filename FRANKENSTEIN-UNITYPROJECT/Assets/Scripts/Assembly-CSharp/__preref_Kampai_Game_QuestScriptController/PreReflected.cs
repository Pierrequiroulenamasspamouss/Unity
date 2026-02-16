namespace __preref_Kampai_Game_QuestScriptController
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.QuestScriptController();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[32]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.QuestScriptKernel), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).kernel = (global::Kampai.Game.QuestScriptKernel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).localService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeEventService), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).timeEventService = (global::Kampai.Game.ITimeEventService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeService), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).timeService = (global::Kampai.Game.ITimeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.IVideoService), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).videoService = (global::Kampai.Common.IVideoService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ITelemetryService), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).telemetryService = (global::Kampai.Common.ITelemetryService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITikiBarService), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).tikiBarService = (global::Kampai.Game.ITikiBarService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).gameContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).uiContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IOrderBoardService), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).orderBoardService = (global::Kampai.Game.IOrderBoardService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.DeselectAllMinionsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).deselectAllMinionsSignal = (global::Kampai.Common.DeselectAllMinionsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).globalSoundSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MoveBuildMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).moveBuildMenuSignal = (global::Kampai.UI.View.MoveBuildMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetPremiumCurrencySignal), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).setPremiumCurrencySignal = (global::Kampai.UI.View.SetPremiumCurrencySignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.PopupMessageSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).popupMessageSignal = (global::Kampai.UI.View.PopupMessageSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.OpenBuildingMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).openBuildingMenuSignal = (global::Kampai.Game.OpenBuildingMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowTipsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).showTipsSignal = (global::Kampai.UI.View.ShowTipsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.EnableCameraBehaviourSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).enableCameraBehaviourSignal = (global::Kampai.Game.EnableCameraBehaviourSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.DisableCameraBehaviourSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).disableCameraBehaviourSignal = (global::Kampai.Game.DisableCameraBehaviourSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.SelectMinionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).selectMinionSignal = (global::Kampai.Common.SelectMinionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CreateWayFinderSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).createWayFinderSignal = (global::Kampai.UI.View.CreateWayFinderSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.RemoveWayFinderSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).removeWayFinderSignal = (global::Kampai.UI.View.RemoveWayFinderSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetLimitTikiBarWayFindersSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).setLimitTikiBarWayFindersSignal = (global::Kampai.UI.View.SetLimitTikiBarWayFindersSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.IGUIService), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).guiService = (global::Kampai.UI.View.IGUIService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.IPickService), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).pickService = (global::Kampai.Common.IPickService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(ILocalPersistanceService), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).persistanceService = (ILocalPersistanceService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ICoppaService), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).coppaService = (global::Kampai.Common.ICoppaService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IZoomCameraModel), delegate(object target, object val)
				{
					((global::Kampai.Game.QuestScriptController)target).zoomCameraModel = (global::Kampai.Game.IZoomCameraModel)val;
				})
			};
			object[] array = new object[32];
			array[11] = global::Kampai.Game.GameElement.CONTEXT;
			array[12] = global::Kampai.UI.View.UIElement.CONTEXT;
			SetterNames = array;
		}
	}
}
