namespace __preref_Kampai_Game_View_BuildingManagerMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.BuildingManagerMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[82]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).uiContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeEventService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).timeEventService = (global::Kampai.Game.ITimeEventService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).minionManager = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).glassCanvas = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ILandExpansionService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).landExpansionService = (global::Kampai.Game.ILandExpansionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IQuestService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).questService = (global::Kampai.Game.IQuestService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).timeService = (global::Kampai.Game.ITimeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.BuildingManagerView), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).view = (global::Kampai.Game.View.BuildingManagerView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SelectBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).selectBuildingSignal = (global::Kampai.Game.SelectBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.DeselectBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).deselectBuildingSignal = (global::Kampai.Game.DeselectBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MoveBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).moveBuildingSignal = (global::Kampai.Game.MoveBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MoveScaffoldingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).moveScaffoldingSignal = (global::Kampai.Game.MoveScaffoldingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StartTaskSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).startTaskSignal = (global::Kampai.Game.StartTaskSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SignalActionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).stopTaskSignal = (global::Kampai.Game.SignalActionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BuildingUtilities), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).buildingUtil = (global::Kampai.Game.BuildingUtilities)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayLocalAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).playLocalAudioSignal = (global::Kampai.Main.PlayLocalAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.StopLocalAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).stopLocalAudioSignal = (global::Kampai.Main.StopLocalAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.DebugUpdateGridSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).gridSignal = (global::Kampai.Game.DebugUpdateGridSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RevealBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).revealBuildingSignal = (global::Kampai.Game.RevealBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.RemoveWayFinderSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).removeWayFinderSignal = (global::Kampai.UI.View.RemoveWayFinderSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CreateWayFinderSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).createWayFinderSignal = (global::Kampai.UI.View.CreateWayFinderSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.DisableCameraBehaviourSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).disableCameraSignal = (global::Kampai.Game.DisableCameraBehaviourSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.EnableCameraBehaviourSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).enableCameraSignal = (global::Kampai.Game.EnableCameraBehaviourSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CreateDummyBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).createDummyBuildingSignal = (global::Kampai.Game.CreateDummyBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ShowBuildingFootprintSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).showBuildingFootprintSignal = (global::Kampai.Game.ShowBuildingFootprintSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.PickControllerModel), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).model = (global::Kampai.Common.PickControllerModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CreateBuildingInInventorySignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).createBuildingInInventorySignal = (global::Kampai.Game.CreateBuildingInInventorySignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BuildingChangeStateSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).buildingChangeStateSignal = (global::Kampai.Game.BuildingChangeStateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BuildingHarvestSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).buildingHarvestSignal = (global::Kampai.Game.BuildingHarvestSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.HarvestReadySignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).harvestReadySignal = (global::Kampai.Game.HarvestReadySignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.UpdateTaskedMinionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).updateTaskedMinionSignal = (global::Kampai.Game.UpdateTaskedMinionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StartConstructionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).startConstructionSignal = (global::Kampai.Game.StartConstructionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RestoreBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).restoreBuildingSignal = (global::Kampai.Game.RestoreBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RestoreScaffoldingViewSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).restoreScaffoldingViewSignal = (global::Kampai.Game.RestoreScaffoldingViewSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RestoreRibbonViewSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).restoreRibbonViewSignal = (global::Kampai.Game.RestoreRibbonViewSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RestorePlatformViewSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).restorePlatformViewSignal = (global::Kampai.Game.RestorePlatformViewSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RestoreBuildingViewSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).restoreBuildingViewSignal = (global::Kampai.Game.RestoreBuildingViewSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoMoveSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).autoMoveSignal = (global::Kampai.Game.CameraAutoMoveSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraInstanceFocusSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).buildingFocusSignal = (global::Kampai.Game.CameraInstanceFocusSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UITryHarvestSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).tryHarvestSignal = (global::Kampai.UI.View.UITryHarvestSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.TryHarvestBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).harvestBuildingSignal = (global::Kampai.Common.TryHarvestBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.RepairBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).repairBuildingSignal = (global::Kampai.Common.RepairBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.RecreateBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).recreateBuildingSignal = (global::Kampai.Common.RecreateBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BuildingReactionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).buildingReactionSignal = (global::Kampai.Game.BuildingReactionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.MinionTaskCompleteSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).taskCompleteSignal = (global::Kampai.Common.MinionTaskCompleteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MinionStateChangeSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).minionStateChange = (global::Kampai.Game.MinionStateChangeSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.EjectAllMinionsFromBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).ejectAllMinionsFromBuildingSignal = (global::Kampai.Game.EjectAllMinionsFromBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.PopupMessageSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).popupMessageSignal = (global::Kampai.UI.View.PopupMessageSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).localService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PurchaseNewBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).purchaseNewBuildingSignal = (global::Kampai.Game.PurchaseNewBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SendBuildingToInventorySignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).sendBuildingToInventorySignal = (global::Kampai.Game.SendBuildingToInventorySignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CreateInventoryBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).createInventoryBuildingSignal = (global::Kampai.Game.CreateInventoryBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CancelPurchaseSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).cancelPurchaseSignal = (global::Kampai.Game.CancelPurchaseSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SetBuildingPositionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).setBuildingPositionSignal = (global::Kampai.Game.SetBuildingPositionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).globalAudioSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.AddFootprintSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).addFootprintSignal = (global::Kampai.Game.AddFootprintSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SetupBrokenBridgesSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).setupBrokenBridgesSignal = (global::Kampai.Game.SetupBrokenBridgesSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BurnLandExpansionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).burnLandExpansionSignal = (global::Kampai.Game.BurnLandExpansionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SetupLandExpansionsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).setupLandExpansionsSignal = (global::Kampai.Game.SetupLandExpansionsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SetupDebrisSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).setupDebrisSignal = (global::Kampai.Game.SetupDebrisSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SetupAspirationalBuildingsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).setupAspirationalBuildingsSignal = (global::Kampai.Game.SetupAspirationalBuildingsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.DisplayBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).displayBuildingSignal = (global::Kampai.Game.DisplayBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StartIncidentalAnimationSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).animationSignal = (global::Kampai.Game.StartIncidentalAnimationSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPrestigeService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).characterService = (global::Kampai.Game.IPrestigeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BRBCelebrationAnimationSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).brbExitAnimimationSignal = (global::Kampai.Game.BRBCelebrationAnimationSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.LeisureProximityRadiusSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).leisureProximityRadiusSignal = (global::Kampai.Game.LeisureProximityRadiusSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.InitBuildingObjectSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).initBuildingObjectSignal = (global::Kampai.Game.InitBuildingObjectSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetBuildMenuEnabledSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).setBuildMenuEnabledSignal = (global::Kampai.UI.View.SetBuildMenuEnabledSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CleanupDebrisSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).cleanupDebrisSignal = (global::Kampai.Game.CleanupDebrisSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ToggleMinionRendererSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).toggleMinionSignal = (global::Kampai.Game.ToggleMinionRendererSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RelocateTaskedMinionsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).relocateMinionsSignal = (global::Kampai.Game.RelocateTaskedMinionsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ICoroutineProgressMonitor), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).coroutineProgressMonitor = (global::Kampai.Util.ICoroutineProgressMonitor)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MarketplaceUpdateSoldItemsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).updateSoldItemsSignal = (global::Kampai.Game.MarketplaceUpdateSoldItemsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OpenStoreHighlightItemSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).openStoreSignal = (global::Kampai.UI.View.OpenStoreHighlightItemSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.HighlightBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).highlightBuildingSignal = (global::Kampai.Game.HighlightBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).dispatcher = (global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BuildingManagerMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			object[] array = new object[82];
			array[0] = global::Kampai.UI.View.UIElement.CONTEXT;
			array[2] = global::Kampai.Game.GameElement.MINION_MANAGER;
			array[3] = global::Kampai.Main.MainElement.UI_GLASSCANVAS;
			array[80] = global::strange.extensions.context.api.ContextKeys.CONTEXT_DISPATCHER;
			array[81] = global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW;
			SetterNames = array;
		}
	}
}
