namespace Kampai.Game.View
{
	internal sealed class BuildingManagerMediator : global::strange.extensions.mediation.impl.EventMediator
	{
		private const float LEISURESIGNAL_INTERVAL = 0.25f;

		private global::Kampai.Game.View.DummyBuildingObject currentDummyBuilding;

		private global::strange.extensions.signal.impl.Signal<int> triggerGagAnimation = new global::strange.extensions.signal.impl.Signal<int>();

		private global::strange.extensions.signal.impl.Signal<int> minionTaskingAnimationDone = new global::strange.extensions.signal.impl.Signal<int>();

		private float currentLeisureInterval;

		private bool allowStorable = true;

		private int buildingID = -1;

		[Inject(global::Kampai.UI.View.UIElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable uiContext { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

		[Inject(global::Kampai.Game.GameElement.MINION_MANAGER)]
		public global::UnityEngine.GameObject minionManager { get; set; }

		[Inject(global::Kampai.Main.MainElement.UI_GLASSCANVAS)]
		public global::UnityEngine.GameObject glassCanvas { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.ILandExpansionService landExpansionService { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.View.BuildingManagerView view { get; set; }

		[Inject]
		public global::Kampai.Game.SelectBuildingSignal selectBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.DeselectBuildingSignal deselectBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.MoveBuildingSignal moveBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.MoveScaffoldingSignal moveScaffoldingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.StartTaskSignal startTaskSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SignalActionSignal stopTaskSignal { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingUtilities buildingUtil { get; set; }

		[Inject]
		public global::Kampai.Main.PlayLocalAudioSignal playLocalAudioSignal { get; set; }

		[Inject]
		public global::Kampai.Main.StopLocalAudioSignal stopLocalAudioSignal { get; set; }

		[Inject]
		public global::Kampai.Game.DebugUpdateGridSignal gridSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RevealBuildingSignal revealBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RemoveWayFinderSignal removeWayFinderSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CreateWayFinderSignal createWayFinderSignal { get; set; }

		[Inject]
		public global::Kampai.Game.DisableCameraBehaviourSignal disableCameraSignal { get; set; }

		[Inject]
		public global::Kampai.Game.EnableCameraBehaviourSignal enableCameraSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CreateDummyBuildingSignal createDummyBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ShowBuildingFootprintSignal showBuildingFootprintSignal { get; set; }

		[Inject]
		public global::Kampai.Common.PickControllerModel model { get; set; }

		[Inject]
		public global::Kampai.Game.CreateBuildingInInventorySignal createBuildingInInventorySignal { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingChangeStateSignal buildingChangeStateSignal { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingHarvestSignal buildingHarvestSignal { get; set; }

		[Inject]
		public global::Kampai.Game.HarvestReadySignal harvestReadySignal { get; set; }

		[Inject]
		public global::Kampai.Game.UpdateTaskedMinionSignal updateTaskedMinionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.StartConstructionSignal startConstructionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RestoreBuildingSignal restoreBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RestoreScaffoldingViewSignal restoreScaffoldingViewSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RestoreRibbonViewSignal restoreRibbonViewSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RestorePlatformViewSignal restorePlatformViewSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RestoreBuildingViewSignal restoreBuildingViewSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoMoveSignal autoMoveSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CameraInstanceFocusSignal buildingFocusSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.UITryHarvestSignal tryHarvestSignal { get; set; }

		[Inject]
		public global::Kampai.Common.TryHarvestBuildingSignal harvestBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Common.RepairBuildingSignal repairBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Common.RecreateBuildingSignal recreateBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingReactionSignal buildingReactionSignal { get; set; }

		[Inject]
		public global::Kampai.Common.MinionTaskCompleteSignal taskCompleteSignal { get; set; }

		[Inject]
		public global::Kampai.Game.MinionStateChangeSignal minionStateChange { get; set; }

		[Inject]
		public global::Kampai.Game.EjectAllMinionsFromBuildingSignal ejectAllMinionsFromBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.PopupMessageSignal popupMessageSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Game.PurchaseNewBuildingSignal purchaseNewBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SendBuildingToInventorySignal sendBuildingToInventorySignal { get; set; }

		[Inject]
		public global::Kampai.Game.CreateInventoryBuildingSignal createInventoryBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CancelPurchaseSignal cancelPurchaseSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SetBuildingPositionSignal setBuildingPositionSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal globalAudioSignal { get; set; }

		[Inject]
		public global::Kampai.Game.AddFootprintSignal addFootprintSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SetupBrokenBridgesSignal setupBrokenBridgesSignal { get; set; }

		[Inject]
		public global::Kampai.Game.BurnLandExpansionSignal burnLandExpansionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SetupLandExpansionsSignal setupLandExpansionsSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SetupDebrisSignal setupDebrisSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SetupAspirationalBuildingsSignal setupAspirationalBuildingsSignal { get; set; }

		[Inject]
		public global::Kampai.Game.DisplayBuildingSignal displayBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.StartIncidentalAnimationSignal animationSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService characterService { get; set; }

		[Inject]
		public global::Kampai.Game.BRBCelebrationAnimationSignal brbExitAnimimationSignal { get; set; }

		[Inject]
		public global::Kampai.Game.LeisureProximityRadiusSignal leisureProximityRadiusSignal { get; set; }

		[Inject]
		public global::strange.extensions.injector.api.IInjectionBinder injectionBinder { get; set; }

		[Inject]
		public global::Kampai.Game.InitBuildingObjectSignal initBuildingObjectSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetBuildMenuEnabledSignal setBuildMenuEnabledSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CleanupDebrisSignal cleanupDebrisSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ToggleMinionRendererSignal toggleMinionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RelocateTaskedMinionsSignal relocateMinionsSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ICoroutineProgressMonitor coroutineProgressMonitor { get; set; }

		[Inject]
		public global::Kampai.Game.MarketplaceUpdateSoldItemsSignal updateSoldItemsSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.OpenStoreHighlightItemSignal openStoreSignal { get; set; }

		[Inject]
		public global::Kampai.Game.HighlightBuildingSignal highlightBuildingSignal { get; set; }

		public float HarvestTimer { get; set; }

		public int LastHarvestedBuildingID { get; set; }

		public override void OnRegister()
		{
			view.Init(logger, definitionService);
			view.updateMinionSignal.AddListener(UpdateTaskedMinions);
			view.addFootprintSignal.AddListener(AddFootprint);
			ManageRegisterStackSize();
			restoreBuildingViewSignal.AddListener(RestoreBuildingView);
			buildingChangeStateSignal.AddListener(UpdateViewFromBuildingState);
			triggerGagAnimation.AddListener(TriggerGagAnimation);
			repairBuildingSignal.AddListener(RepairBuilding);
			recreateBuildingSignal.AddListener(RecreateBuilding);
			displayBuildingSignal.AddListener(DisplayBuilding);
			tryHarvestSignal.AddListener(TryHarvest);
			deselectBuildingSignal.AddListener(DeselectBuilding);
			purchaseNewBuildingSignal.AddListener(PurchaseNewBuilding);
			sendBuildingToInventorySignal.AddListener(SendToInventory);
			createInventoryBuildingSignal.AddListener(CreateInventoryBuilding);
			cancelPurchaseSignal.AddListener(CancelPurchaseStart);
			setBuildingPositionSignal.AddListener(SetBuildingPosition);
			ejectAllMinionsFromBuildingSignal.AddListener(EjectAllMinionsFromBuilding);
			minionTaskingAnimationDone.AddListener(OnMinionTaskingAnimationDone);
			burnLandExpansionSignal.AddListener(OnBurnedLandExpansion);
			routineRunner.StartCoroutine(Init());
		}

		private void ManageRegisterStackSize()
		{
			view.updateResourceBuildingSignal.AddListener(VerifyResourceBuildingSlots);
			setBuildMenuEnabledSignal.AddListener(AllowStorable);
			view.initBuildingObject.AddListener(InitBuildingObject);
			selectBuildingSignal.AddListener(SelectBuilding);
			moveBuildingSignal.AddListener(MoveBuilding);
			moveScaffoldingSignal.AddListener(MoveDummyBuildingObject);
			startTaskSignal.AddListener(StartMinionTask);
			taskCompleteSignal.AddListener(MinionTaskComplete);
			revealBuildingSignal.AddListener(OnRevealBuilding);
			createDummyBuildingSignal.AddListener(CreateDummyBuilding);
			buildingHarvestSignal.AddListener(HarvestComplete);
			restoreScaffoldingViewSignal.AddListener(RestoreScaffoldingView);
			restoreRibbonViewSignal.AddListener(RestoreRibbonView);
			restorePlatformViewSignal.AddListener(RestorePlatformView);
			highlightBuildingSignal.AddListener(HighlightBuilding);
			uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.SetBuildingRushedSignal>().AddListener(SetBuildingRushed);
			uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.RushRevealBuildingSignal>().AddListener(RushRevealBuilding);
		}

		public override void OnRemove()
		{
			view.addFootprintSignal.RemoveListener(AddFootprint);
			view.updateResourceBuildingSignal.RemoveListener(VerifyResourceBuildingSlots);
			ManageRemoveStackSize();
			buildingHarvestSignal.RemoveListener(HarvestComplete);
			restoreScaffoldingViewSignal.RemoveListener(RestoreScaffoldingView);
			restorePlatformViewSignal.RemoveListener(RestorePlatformView);
			restoreRibbonViewSignal.RemoveListener(RestoreRibbonView);
			restoreBuildingViewSignal.RemoveListener(RestoreBuildingView);
			buildingChangeStateSignal.RemoveListener(UpdateViewFromBuildingState);
			triggerGagAnimation.RemoveListener(TriggerGagAnimation);
			repairBuildingSignal.RemoveListener(RepairBuilding);
			recreateBuildingSignal.RemoveListener(RecreateBuilding);
			displayBuildingSignal.RemoveListener(DisplayBuilding);
			setBuildMenuEnabledSignal.RemoveListener(AllowStorable);
			tryHarvestSignal.RemoveListener(TryHarvest);
			deselectBuildingSignal.RemoveListener(DeselectBuilding);
			purchaseNewBuildingSignal.RemoveListener(PurchaseNewBuilding);
			sendBuildingToInventorySignal.RemoveListener(SendToInventory);
			createInventoryBuildingSignal.RemoveListener(CreateInventoryBuilding);
			cancelPurchaseSignal.RemoveListener(CancelPurchaseStart);
			setBuildingPositionSignal.RemoveListener(SetBuildingPosition);
			ejectAllMinionsFromBuildingSignal.RemoveListener(EjectAllMinionsFromBuilding);
			minionTaskingAnimationDone.RemoveListener(OnMinionTaskingAnimationDone);
			burnLandExpansionSignal.RemoveListener(OnBurnedLandExpansion);
			view.initBuildingObject.RemoveListener(InitBuildingObject);
		}

		private void ManageRemoveStackSize()
		{
			selectBuildingSignal.RemoveListener(SelectBuilding);
			moveBuildingSignal.RemoveListener(MoveBuilding);
			moveScaffoldingSignal.RemoveListener(MoveDummyBuildingObject);
			startTaskSignal.RemoveListener(StartMinionTask);
			taskCompleteSignal.RemoveListener(MinionTaskComplete);
			revealBuildingSignal.RemoveListener(OnRevealBuilding);
			createDummyBuildingSignal.RemoveListener(CreateDummyBuilding);
			highlightBuildingSignal.RemoveListener(HighlightBuilding);
			uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.SetBuildingRushedSignal>().RemoveListener(SetBuildingRushed);
			uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.RushRevealBuildingSignal>().RemoveListener(RushRevealBuilding);
		}

		private global::System.Collections.IEnumerator Init()
		{
			int id = coroutineProgressMonitor.StartTask("init buildings");
			yield return null;
			global::Kampai.Util.TimeProfiler.StartSection("buildings");
			global::System.Collections.Generic.ICollection<global::Kampai.Game.Building> buildingList = playerService.GetInstancesByType<global::Kampai.Game.Building>();
			global::Kampai.Util.TimeProfiler.StartSection("restoring");
			global::System.Diagnostics.Stopwatch sw = global::System.Diagnostics.Stopwatch.StartNew();
			foreach (global::Kampai.Game.Building building in buildingList)
			{
				restoreBuildingSignal.Dispatch(building);
				if (sw.ElapsedMilliseconds > 1500)
				{
					sw.Reset();
					sw.Start();
					yield return null;
				}
			}
			sw.Stop();
			global::Kampai.Util.TimeProfiler.EndSection("restoring");
			yield return null;
			global::Kampai.Util.TimeProfiler.StartSection("expansions");
			setupBrokenBridgesSignal.Dispatch();
			setupLandExpansionsSignal.Dispatch();
			setupDebrisSignal.Dispatch();
			global::Kampai.Util.TimeProfiler.EndSection("expansions");
			setupAspirationalBuildingsSignal.Dispatch();
			global::Kampai.Util.TimeProfiler.EndSection("buildings");
			coroutineProgressMonitor.FinishTask(id);
		}

		private void AllowStorable(bool storable)
		{
			allowStorable = storable;
		}

		private void InitBuildingObject(global::Kampai.Game.View.BuildingObject buildingObject, global::System.Collections.Generic.Dictionary<string, global::UnityEngine.RuntimeAnimatorController> controllers, global::Kampai.Game.Building building)
		{
			initBuildingObjectSignal.Dispatch(buildingObject, controllers, building);
		}

		public void AddFootprint(global::Kampai.Game.Building building, global::Kampai.Game.Location location)
		{
			addFootprintSignal.Dispatch(building, location);
		}

		public void Update()
		{
			if (HarvestTimer > 0f)
			{
				HarvestTimer -= global::UnityEngine.Time.deltaTime;
			}
			currentLeisureInterval += global::UnityEngine.Time.deltaTime;
			if (currentLeisureInterval >= 0.25f)
			{
				leisureProximityRadiusSignal.Dispatch();
				currentLeisureInterval = 0f;
			}
		}

		private void InjectBuildingObject(global::UnityEngine.GameObject go, int id)
		{
			switch (id)
			{
			case 3041:
				SetBuildingBinding(go, global::Kampai.Game.StaticItem.TIKI_BAR_BUILDING_ID_DEF);
				break;
			case 3070:
				SetBuildingBinding(go, global::Kampai.Game.StaticItem.WELCOME_BOOTH_BUILDING_ID_DEF);
				break;
			}
		}

		private void SetBuildingBinding(global::UnityEngine.GameObject go, object name)
		{
			global::strange.extensions.injector.api.IInjectionBinding binding = injectionBinder.GetBinding<global::UnityEngine.GameObject>(name);
			if (binding == null)
			{
				injectionBinder.Bind<global::UnityEngine.GameObject>().ToValue(go).ToName(name);
			}
			else
			{
				binding.SetValue(go);
			}
		}

		private void RepairBuilding(global::Kampai.Game.Building building)
		{
			int iD = building.ID;
			global::Kampai.Game.BuildingState state = building.State;
			if (state != global::Kampai.Game.BuildingState.Broken)
			{
				return;
			}
			globalAudioSignal.Dispatch("Play_building_repair_01");
			removeWayFinderSignal.Dispatch(iD);
			buildingChangeStateSignal.Dispatch(iD, global::Kampai.Game.BuildingState.Idle);
			RecreateBuilding(building);
			global::Kampai.Game.View.BuildingObject buildingObject = view.GetBuildingObject(iD);
			buildingObject.SetVFXState("RepairBuilding");
			global::Kampai.Game.StageBuilding stageBuilding = building as global::Kampai.Game.StageBuilding;
			global::Kampai.Game.WelcomeHutBuilding welcomeHutBuilding = building as global::Kampai.Game.WelcomeHutBuilding;
			global::Kampai.Game.CabanaBuilding cabanaBuilding = building as global::Kampai.Game.CabanaBuilding;
			global::Kampai.Game.FountainBuilding fountainBuilding = building as global::Kampai.Game.FountainBuilding;
			global::Kampai.Game.StorageBuilding storageBuilding = building as global::Kampai.Game.StorageBuilding;
			if (stageBuilding != null)
			{
				questService.RepairStage();
			}
			else if (welcomeHutBuilding != null)
			{
				questService.RepairWelcomeHut();
			}
			else if (fountainBuilding != null)
			{
				questService.RepairFountain();
			}
			else if (storageBuilding != null)
			{
				updateSoldItemsSignal.Dispatch(false);
			}
			else
			{
				if (cabanaBuilding == null)
				{
					return;
				}
				global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Quest> questMap = questService.GetQuestMap();
				foreach (global::Kampai.Game.Quest value in questMap.Values)
				{
					if (value.state != global::Kampai.Game.QuestState.RunningTasks)
					{
						continue;
					}
					foreach (global::Kampai.Game.QuestStep step in value.Steps)
					{
						if (step.state == global::Kampai.Game.QuestStepState.Notstarted && step.TrackedID == iD && value.Definition.QuestSteps[value.Steps.IndexOf(step)].Type == global::Kampai.Game.QuestStepType.CabanaRepair)
						{
							questService.UpdateCabanaRepairTask(building, global::Kampai.Game.QuestTaskTransition.Complete);
							return;
						}
					}
				}
			}
		}

		private void HighlightBuilding(int buildingId, bool highlight)
		{
			view.HighlightBuilding(buildingId, highlight);
		}

		private void RecreateBuilding(global::Kampai.Game.Building building)
		{
			view.RemoveBuilding(building.ID);
			InjectBuildingObject(view.CreateBuilding(building), building.Definition.ID);
		}

		private void RestoreBuildingView(global::Kampai.Game.Building building)
		{
			logger.Debug("Restoring building with id: {0}, type: {1}, state: {2}", building.ID, building.GetType(), building.State);
			InjectBuildingObject(view.CreateBuilding(building), building.Definition.ID);
			global::Kampai.Game.BuildingState state = building.State;
			if ((state == global::Kampai.Game.BuildingState.Construction || state == global::Kampai.Game.BuildingState.Complete || state == global::Kampai.Game.BuildingState.Inactive) && building.Definition.ConstructionTime > 0)
			{
				DisplayBuilding(false, building.ID);
			}
		}

		private void PurchaseNewBuilding(global::Kampai.Game.Building building)
		{
			if (currentDummyBuilding != null)
			{
				global::UnityEngine.Object.Destroy(currentDummyBuilding.gameObject);
			}
			global::Kampai.Game.BuildingDefinition definition = building.Definition;
			global::Kampai.Game.Location location = building.Location;
			global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(location.x, 0f, location.y);
			int iD = building.ID;
			global::UnityEngine.GameObject gameObject = view.CreateBuilding(building);
			global::Kampai.Game.View.BuildingObject component = gameObject.GetComponent<global::Kampai.Game.View.BuildingObject>();
			component.ID = iD;
			InjectBuildingObject(gameObject, definition.ID);
			if (definition.ConstructionTime > 0)
			{
				DisplayBuilding(false, iD);
				view.CreatePlatformBuildingObject(building, position);
				view.CreateScaffoldingBuildingObject(building, position);
				GrowScaffolding(component, true);
				TriggerVFX(position, "FX_Drop_Prefab", definition);
			}
			else
			{
				GrowScaffolding(component, false);
			}
		}

		private void GrowScaffolding(global::Kampai.Game.View.BuildingObject building, bool isScaffoldingPrefab)
		{
			if (building != null)
			{
				if (isScaffoldingPrefab)
				{
					PlaySFX(building.ID, "Play_scaffold_construction_01", true);
				}
				else
				{
					PlaySFX(building.ID, "Play_prop_land_01", true);
				}
				global::UnityEngine.Vector3 center = building.Center;
				global::Kampai.Util.Boxed<global::UnityEngine.Vector3> type = new global::Kampai.Util.Boxed<global::UnityEngine.Vector3>(new global::UnityEngine.Vector3(center.x, 0f, center.z));
				buildingReactionSignal.Dispatch(type);
				startConstructionSignal.Dispatch(building.gameObject, building.ID, false);
			}
		}

		private void SetBuildingRushed(int buildingId)
		{
			view.SetBuildingRushed(buildingId);
		}

		private void RushRevealBuilding(int buildingId)
		{
			global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(buildingId);
			if (byInstanceId != null)
			{
				RevealBuilding(byInstanceId);
			}
		}

		private void OnRevealBuilding(global::Kampai.Game.Building building)
		{
			RevealBuilding(building);
		}

		private void PlaySFX(int buildingId, string sfxName, bool enable)
		{
			global::Kampai.Game.View.BuildingObject buildingObject = view.GetBuildingObject(buildingId);
			if (buildingObject != null)
			{
				if (enable)
				{
					stopLocalAudioSignal.Dispatch(buildingObject.localAudioEmitter);
					playLocalAudioSignal.Dispatch(buildingObject.localAudioEmitter, sfxName, null);
				}
				else
				{
					stopLocalAudioSignal.Dispatch(buildingObject.localAudioEmitter);
				}
			}
		}

		private void RestoreScaffoldingView(global::Kampai.Game.Building building, bool restoreTimer)
		{
			if (building.Definition.ConstructionTime <= 0)
			{
				return;
			}
			logger.Debug("Restoring scaffolding for building id: {0} type: {1}", building.ID, building.GetType());
			global::Kampai.Game.View.ScaffoldingBuildingObject scaffoldingBuildingObject = view.CreateScaffoldingBuildingObject(building, new global::UnityEngine.Vector3(building.Location.x, 0f, building.Location.y));
			if (restoreTimer)
			{
				routineRunner.StartCoroutine(WaitAFrame(delegate
				{
					startConstructionSignal.Dispatch(scaffoldingBuildingObject.gameObject, building.ID, true);
				}));
			}
		}

		private void RestorePlatformView(global::Kampai.Game.Building building)
		{
			if (building.Definition.ConstructionTime > 0)
			{
				logger.Debug("Restoring platform for building id: {0} type: {1}", building.ID, building.GetType());
				view.CreatePlatformBuildingObject(building, new global::UnityEngine.Vector3(building.Location.x, 0f, building.Location.y));
			}
		}

		private void RestoreRibbonView(global::Kampai.Game.Building building)
		{
			if (building.Definition.ConstructionTime > 0)
			{
				logger.Debug("Restoring ribbon for building id: {0} type: {1}", building.ID, building.GetType());
				view.CreateRibbonBuildingObject(building, new global::UnityEngine.Vector3(building.Location.x, 0f, building.Location.y));
				CheckIfItIsTheFirstSwampGasForFTUE(building);
			}
		}

		private void CreateInventoryBuilding(global::Kampai.Game.Building building, global::Kampai.Game.Location location)
		{
			if (currentDummyBuilding != null)
			{
				global::UnityEngine.Object.Destroy(currentDummyBuilding.gameObject);
			}
			global::UnityEngine.GameObject gameObject = view.CreateBuilding(building);
			InjectBuildingObject(gameObject, building.Definition.ID);
			gameObject.transform.position = new global::UnityEngine.Vector3(location.x, 0f, location.y);
			global::Kampai.Game.View.BuildingObject component = gameObject.GetComponent<global::Kampai.Game.View.BuildingObject>();
			if (component != null)
			{
				global::UnityEngine.Vector3 center = component.Center;
				global::Kampai.Util.Boxed<global::UnityEngine.Vector3> type = new global::Kampai.Util.Boxed<global::UnityEngine.Vector3>(new global::UnityEngine.Vector3(center.x, 0f, center.z));
				buildingReactionSignal.Dispatch(type);
			}
			if (model.CurrentMode != global::Kampai.Common.PickControllerModel.Mode.DragAndDrop)
			{
				model.SelectedBuilding = null;
			}
		}

		private void RevealBuilding(global::Kampai.Game.Building building)
		{
			global::Kampai.Game.BuildingState state = building.State;
			int iD = building.ID;
			if (state != global::Kampai.Game.BuildingState.Complete)
			{
				logger.Info("Can't reveal building id:{0} when construction is not complete!", iD);
				return;
			}
			global::Kampai.Game.Location location = building.Location;
			TriggerVFX(new global::UnityEngine.Vector3(location.x, 0f, location.y), "FX_Reveal_Prefab", building.Definition);
			buildingChangeStateSignal.Dispatch(iD, global::Kampai.Game.BuildingState.Idle);
			RemoveSwampGasWayFinderForFTUE(building);
			view.RemoveAllScaffoldingParts(iD);
			global::Kampai.Game.BuildingDefinition definition = building.Definition;
			global::Kampai.Game.View.BuildingObject buildingObject = view.GetBuildingObject(iD);
			global::UnityEngine.Vector3 center = buildingObject.Center;
			AnimateRevealBuilding(buildingObject);
			if (definition.RewardTransactionId != 0)
			{
				playerService.RunEntireTransaction(definition.RewardTransactionId, global::Kampai.Game.TransactionTarget.REWARD_BUILDING, null);
				uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.SpawnDooberSignal>().Dispatch(center, global::Kampai.UI.View.DestinationType.XP, -1, true);
			}
			global::Kampai.Util.Boxed<global::UnityEngine.Vector3> type = new global::Kampai.Util.Boxed<global::UnityEngine.Vector3>(new global::UnityEngine.Vector3(center.x, 0f, center.z));
			buildingReactionSignal.Dispatch(type);
			PlaySFX(iD, "Play_scaffold_disappear_01", true);
			globalAudioSignal.Dispatch("Play_building_repair_01");
			questService.UpdateConstructionTask();
		}

		private void AnimateRevealBuilding(global::Kampai.Game.View.BuildingObject buildingObject)
		{
			buildingObject.transform.localScale = new global::UnityEngine.Vector3(0.01f, 0.01f, 0.01f);
			DisplayBuilding(true, buildingObject.ID);
			Go.to(buildingObject.transform, 0.5f, new GoTweenConfig().scale(new global::UnityEngine.Vector3(1f, 1f, 1f)).setEaseType(GoEaseType.BackOut).onComplete(delegate(AbstractGoTween tween)
			{
				tween.destroy();
			})).play();
		}

		private void RemoveSwampGasWayFinderForFTUE(global::Kampai.Game.Building building)
		{
			if (building.Definition.ID == 3015 && playerService.GetCountByDefinitionId(building.Definition.ID) == 1)
			{
				removeWayFinderSignal.Dispatch(building.ID);
			}
		}

		private void SelectBuilding(int buildingId, string footprint)
		{
			view.SelectBuilding(buildingId);
			ToggleTaskableMinions(buildingId, false);
			globalAudioSignal.Dispatch("Play_building select_01");
			disableCameraSignal.Dispatch(1);
			enableCameraSignal.Dispatch(8);
			global::Kampai.Game.View.BuildingObject buildingObject = view.GetBuildingObject(buildingId);
			if (!(buildingObject == null))
			{
				showBuildingFootprintSignal.Dispatch(buildingObject, buildingObject.transform, global::Kampai.Util.Tuple.Create(buildingObject.Width, buildingObject.Depth), true);
				global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(buildingId);
				if (!byInstanceId.Definition.Storable || !allowStorable)
				{
					GetUISignal<global::Kampai.UI.View.ShowMoveBuildingMenuSignal>().Dispatch(true, byInstanceId.ID, global::Kampai.UI.View.MoveBuildingMenuMediator.Buttons.Inventory);
				}
				else
				{
					GetUISignal<global::Kampai.UI.View.ShowMoveBuildingMenuSignal>().Dispatch(true, byInstanceId.ID, global::Kampai.UI.View.MoveBuildingMenuMediator.Buttons.All);
				}
				GetUISignal<global::Kampai.UI.View.ShowHUDSignal>().Dispatch(false);
				GetUISignal<global::Kampai.UI.View.ShowStoreSignal>().Dispatch(false);
				GetUISignal<global::Kampai.UI.View.ShowWorldCanvasSignal>().Dispatch(false);
			}
		}

		private void DeselectBuilding(int buildingId)
		{
			view.DeselectBuilding(buildingId);
			ToggleTaskableMinions(buildingId, true);
			disableCameraSignal.Dispatch(8);
			enableCameraSignal.Dispatch(1);
			HideFootprint();
			HideBuildingPlacementMenu();
		}

		private void ToggleTaskableMinions(int buildingID, bool enableRenderers)
		{
			global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(buildingID);
			global::Kampai.Game.TaskableBuilding taskableBuilding = byInstanceId as global::Kampai.Game.TaskableBuilding;
			global::Kampai.Game.LeisureBuilding leisureBuilding = byInstanceId as global::Kampai.Game.LeisureBuilding;
			global::System.Collections.Generic.IList<int> list3;
			if (taskableBuilding == null)
			{
				global::System.Collections.Generic.IList<int> list2;
				global::System.Collections.Generic.IList<int> list;
				if (leisureBuilding == null)
				{
					list = null;
					list2 = list;
				}
				else
				{
					list2 = leisureBuilding.MinionList;
				}
				list = list2;
				list3 = list;
			}
			else
			{
				list3 = taskableBuilding.MinionList;
			}
			global::System.Collections.Generic.IList<int> list4 = list3;
			if (list4 == null)
			{
				return;
			}
			foreach (int item in list4)
			{
				toggleMinionSignal.Dispatch(item, enableRenderers);
			}
			if (enableRenderers)
			{
				relocateMinionsSignal.Dispatch(byInstanceId);
			}
		}

		private void DisplayBuilding(bool isVisible, int buildingId)
		{
			global::Kampai.Game.View.BuildingObject buildingObject = view.GetBuildingObject(buildingId);
			if (buildingObject != null)
			{
				buildingObject.gameObject.SetActive(isVisible);
			}
		}

		private void MoveBuilding(int buildingId, global::UnityEngine.Vector3 position, bool isValidPosition)
		{
			global::Kampai.Game.View.BuildingObject buildingObject = view.GetBuildingObject(buildingId);
			if (buildingObject != null)
			{
				PlayAudioOnMove(buildingObject.transform.position, position, isValidPosition);
			}
			view.MoveBuilding(buildingId, position, isValidPosition);
		}

		private void MoveDummyBuildingObject(global::UnityEngine.Vector3 position, bool isValidPosition)
		{
			if (currentDummyBuilding != null)
			{
				position = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Round(position.x), currentDummyBuilding.transform.position.y, global::UnityEngine.Mathf.Round(position.z));
				PlayAudioOnMove(currentDummyBuilding.transform.position, position, isValidPosition);
				currentDummyBuilding.transform.position = position;
				currentDummyBuilding.SetBlendedColor((!isValidPosition) ? global::Kampai.Util.GameConstants.Building.INVALID_PLACEMENT_COLOR : global::Kampai.Util.GameConstants.Building.VALID_PLACEMENT_COLOR);
			}
		}

		private void PlayAudioOnMove(global::UnityEngine.Vector3 oldPosition, global::UnityEngine.Vector3 newPosition, bool isValidPosition)
		{
			global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Round(oldPosition.x), oldPosition.y, global::UnityEngine.Mathf.Round(oldPosition.z));
			global::UnityEngine.Vector3 vector2 = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Round(newPosition.x), newPosition.y, global::UnityEngine.Mathf.Round(newPosition.z));
			if (vector2 != vector)
			{
				if (isValidPosition)
				{
					globalAudioSignal.Dispatch("Play_click_snap_01");
				}
				else
				{
					globalAudioSignal.Dispatch("Play_error_button_01");
				}
			}
		}

		private void StartMinionTask(global::Kampai.Game.Minion minion, global::Kampai.Game.Building building)
		{
			int iD = building.ID;
			global::Kampai.Game.TaskableBuilding taskableBuilding = building as global::Kampai.Game.TaskableBuilding;
			if (taskableBuilding != null)
			{
				global::UnityEngine.GameObject gameObject = minionManager.GetComponent<global::Kampai.Game.View.MinionManagerView>().GetGameObject(minion.ID);
				bool alreadyRushed = minion.AlreadyRushed;
				buildingChangeStateSignal.Dispatch(iD, global::Kampai.Game.BuildingState.Working);
				view.StartMinionTask(taskableBuilding, gameObject.GetComponent<global::Kampai.Game.View.MinionObject>(), alreadyRushed);
				if (taskableBuilding is global::Kampai.Game.TikiBarBuilding)
				{
					global::Kampai.Game.Prestige prestigeFromMinionInstance = characterService.GetPrestigeFromMinionInstance(minion);
					characterService.ChangeToPrestigeState(prestigeFromMinionInstance, global::Kampai.Game.PrestigeState.Questing);
					return;
				}
				PlaceMinionInBuilding(taskableBuilding, minion);
				if (alreadyRushed)
				{
					return;
				}
				global::Kampai.Game.MignetteBuilding mignetteBuilding = taskableBuilding as global::Kampai.Game.MignetteBuilding;
				if (mignetteBuilding != null)
				{
					if (!mignetteBuilding.AreAllMinionSlotsFilled())
					{
						return;
					}
					global::Kampai.Game.View.BuildingObject buildingObject = view.GetBuildingObject(iD);
					if (buildingObject != null)
					{
						global::Kampai.Game.View.MignetteBuildingObject mignetteBuildingObject = buildingObject as global::Kampai.Game.View.MignetteBuildingObject;
						if (mignetteBuildingObject != null && mignetteBuildingObject.GetMignetteMinionCount() == mignetteBuilding.GetMinionSlotsOwned())
						{
							MoveCameraAndFocusOnBuilding(mignetteBuilding, true);
						}
					}
				}
				else if (taskableBuilding is global::Kampai.Game.DebrisBuilding)
				{
					cleanupDebrisSignal.Dispatch(iD, true);
					view.AppendMinionTaskAnimationCompleteCallback(gameObject.GetComponent<global::Kampai.Game.View.MinionObject>(), minionTaskingAnimationDone);
				}
				else
				{
					if (!taskableBuilding.IsEligibleForGag())
					{
						return;
					}
					int num = iD;
					int nextGagPlayTime = taskableBuilding.GetNextGagPlayTime(timeService.GameTimeSeconds());
					if (timeEventService.GetEventDuration(num) == 0)
					{
						if (nextGagPlayTime > 0)
						{
							timeEventService.AddEvent(num, timeService.GameTimeSeconds(), nextGagPlayTime, triggerGagAnimation);
						}
						else
						{
							TriggerGagAnimation(num);
						}
					}
				}
			}
			else
			{
				logger.Fatal(global::Kampai.Util.FatalCode.BV_NOT_A_TASKING_BUILDING);
			}
		}

		private void PlaceMinionInBuilding(global::Kampai.Game.TaskableBuilding taskableBuilding, global::Kampai.Game.Minion minion)
		{
			global::System.Collections.Generic.IList<int> minionList = taskableBuilding.MinionList;
			if (minionList.Count <= 1)
			{
				return;
			}
			minionList.Remove(minion.ID);
			bool flag = false;
			for (int i = 0; i < minionList.Count; i++)
			{
				int id = minionList[i];
				global::Kampai.Game.Minion byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Minion>(id);
				if (byInstanceId.UTCTaskStartTime > minion.UTCTaskStartTime)
				{
					minionList.Insert(i, minion.ID);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				minionList.Add(minion.ID);
			}
		}

		private void TriggerGagAnimation(int buildingId)
		{
			global::Kampai.Game.TaskableBuilding byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.TaskableBuilding>(buildingId);
			int utcTime = timeService.GameTimeSeconds();
			if (byInstanceId.IsEligibleForGag() && byInstanceId.GetNextGagPlayTime(utcTime) == 0 && view.TriggerGagAnimation(buildingId))
			{
				byInstanceId.GagPlayed(utcTime);
				if (byInstanceId.IsEligibleForGag())
				{
					MoveCameraAndFocusOnBuilding(byInstanceId);
					timeEventService.AddEvent(buildingId, timeService.GameTimeSeconds(), byInstanceId.GetNextGagPlayTime(utcTime), triggerGagAnimation);
				}
			}
		}

		private void MoveCameraAndFocusOnBuilding(global::Kampai.Game.TaskableBuilding building, bool showModalOnArrive = false)
		{
			global::UnityEngine.Vector3 buildingPosition = view.GetBuildingPosition(building.ID);
			global::Kampai.Game.CameraOffset centerCameraOffset = building.Definition.CenterCameraOffset;
			if (centerCameraOffset == null || !(buildingPosition != global::UnityEngine.Vector3.zero))
			{
				return;
			}
			global::UnityEngine.Vector3 type = new global::UnityEngine.Vector3(buildingPosition.x + centerCameraOffset.x, buildingPosition.y, buildingPosition.z + centerCameraOffset.z);
			int num = building.Definition.GagID;
			if (num == 0)
			{
				num = -1;
			}
			if (num != -1)
			{
				if (playerService.GetByDefinitionId<global::Kampai.Game.Item>(num).Count > 0)
				{
					return;
				}
				global::Kampai.Game.Item i = new global::Kampai.Game.Item(definitionService.Get<global::Kampai.Game.ItemDefinition>(num));
				playerService.Add(i);
			}
			global::Kampai.Game.CameraMovementSettings.Settings settings = (showModalOnArrive ? global::Kampai.Game.CameraMovementSettings.Settings.ShowMenu : global::Kampai.Game.CameraMovementSettings.Settings.None);
			autoMoveSignal.Dispatch(type, centerCameraOffset.zoom, new global::Kampai.Game.CameraMovementSettings(settings, building, null));
			buildingFocusSignal.Dispatch(building.ID, buildingPosition);
		}

		private void MinionTaskComplete(int minionId)
		{
			global::Kampai.Game.Minion byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Minion>(minionId);
			if (byInstanceId == null)
			{
				return;
			}
			byInstanceId.TaskDuration = 0;
			int num = byInstanceId.BuildingID;
			global::Kampai.Game.TaskableBuilding byInstanceId2 = playerService.GetByInstanceId<global::Kampai.Game.TaskableBuilding>(num);
			if (byInstanceId2 == null)
			{
				return;
			}
			int utcTime = timeService.GameTimeSeconds();
			if (byInstanceId2.GetNumCompleteMinions() == byInstanceId2.GetMinionSlotsOwned() && !playerService.HasStorageBuilding())
			{
				uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.CloseAllOtherMenuSignal>().Dispatch(null);
			}
			global::Kampai.Game.ResourceBuilding resourceBuilding = byInstanceId2 as global::Kampai.Game.ResourceBuilding;
			global::Kampai.Game.DebrisBuilding debrisBuilding = byInstanceId2 as global::Kampai.Game.DebrisBuilding;
			if (resourceBuilding != null)
			{
				resourceBuilding.PrepareForHarvest(utcTime);
				byInstanceId.AlreadyRushed = false;
				EjectMinionFromBuilding(byInstanceId2, minionId);
				minionStateChange.Dispatch(minionId, global::Kampai.Game.MinionState.Idle);
				toggleMinionSignal.Dispatch(minionId, true);
				if (view.IsGagAnimationPlaying(num))
				{
					view.StopGagAnimation(num);
				}
			}
			else
			{
				if (debrisBuilding != null)
				{
					EjectMinionFromBuilding(byInstanceId2, minionId);
					minionStateChange.Dispatch(minionId, global::Kampai.Game.MinionState.Idle);
					timeEventService.RemoveEvent(num);
					return;
				}
				byInstanceId2.AddToCompletedMinions(minionId, utcTime);
				view.HarvestReady(byInstanceId2.ID, minionId);
			}
			buildingChangeStateSignal.Dispatch(byInstanceId2.ID, global::Kampai.Game.BuildingState.Harvestable);
			harvestReadySignal.Dispatch(num);
			timeEventService.RemoveEvent(num);
		}

		private void EjectAllMinionsFromBuilding(int buildingID)
		{
			global::Kampai.Game.TaskableBuilding byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.TaskableBuilding>(buildingID);
			if (byInstanceId != null)
			{
				int minionsInBuilding = byInstanceId.GetMinionsInBuilding();
				for (int i = 0; i < minionsInBuilding; i++)
				{
					int minionByIndex = byInstanceId.GetMinionByIndex(0);
					EjectMinionFromBuilding(byInstanceId, minionByIndex);
				}
			}
		}

		private void HarvestComplete(int buildingID)
		{
			global::Kampai.Game.TaskableBuilding byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.TaskableBuilding>(buildingID);
			if (byInstanceId != null)
			{
				global::Kampai.Game.ResourceBuilding resourceBuilding = byInstanceId as global::Kampai.Game.ResourceBuilding;
				if (resourceBuilding != null)
				{
					resourceBuilding.AvailableHarvest--;
					return;
				}
				int minionID = byInstanceId.HarvestFromCompleteMinions();
				EjectMinionFromBuilding(byInstanceId, minionID);
			}
		}

		private void EjectMinionFromBuilding(global::Kampai.Game.TaskableBuilding taskableBuilding, int minionID)
		{
			int iD = taskableBuilding.ID;
			view.UntrackMinion(iD, minionID, taskableBuilding);
			playerService.StopTask(minionID);
			stopTaskSignal.Dispatch(minionID);
			if (taskableBuilding is global::Kampai.Game.ResourceBuilding)
			{
				brbExitAnimimationSignal.Dispatch(minionID);
			}
			global::Kampai.Game.View.MinionManagerView component = minionManager.GetComponent<global::Kampai.Game.View.MinionManagerView>();
			global::Kampai.Game.View.MinionObject minionObject = component.Get(minionID);
			if (minionObject.currentAction is global::Kampai.Game.View.ConstantSpeedPathAction || minionObject.currentAction is global::Kampai.Game.View.RotateAction || minionObject.currentAction is global::Kampai.Game.View.SetAnimatorAction)
			{
				minionObject.ClearActionQueue();
				minionStateChange.Dispatch(minionID, global::Kampai.Game.MinionState.Idle);
			}
			if (taskableBuilding.GetNumCompleteMinions() != 0 || taskableBuilding.Definition.Type == BuildingType.BuildingTypeIdentifier.RESOURCE)
			{
				return;
			}
			if (taskableBuilding.GetMinionsInBuilding() == 0)
			{
				if (taskableBuilding.Definition is global::Kampai.Game.MignetteBuildingDefinition)
				{
					buildingChangeStateSignal.Dispatch(iD, global::Kampai.Game.BuildingState.Cooldown);
				}
				else
				{
					buildingChangeStateSignal.Dispatch(iD, global::Kampai.Game.BuildingState.Idle);
				}
			}
			else
			{
				buildingChangeStateSignal.Dispatch(iD, global::Kampai.Game.BuildingState.Working);
			}
		}

		private void UpdateViewFromBuildingState(int buildingId, global::Kampai.Game.BuildingState buildingState)
		{
			view.UpdateBuildingState(buildingId, buildingState);
			if (buildingState != global::Kampai.Game.BuildingState.Complete)
			{
				return;
			}
			global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(buildingId);
			if (byInstanceId.Definition.ConstructionTime > 0)
			{
				global::Kampai.Game.Location location = byInstanceId.Location;
				global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(location.x, 0f, location.y);
				if (!view.IsBuildingRushed(buildingId))
				{
					PlaySFX(buildingId, null, false);
					view.CreateRibbonBuildingObject(byInstanceId, position);
					TriggerVFX(position, "FX_Bow_Prefab", byInstanceId.Definition);
					CheckIfItIsTheFirstSwampGasForFTUE(byInstanceId);
				}
			}
		}

		private void AdjustVFXPosition(global::Kampai.Game.BuildingDefinition buildingDef, global::UnityEngine.Transform transform)
		{
			if (view.Is8x8Building(buildingDef))
			{
				transform.localPosition += new global::UnityEngine.Vector3(1f, 0f, -1f);
			}
		}

		private void TriggerVFX(global::UnityEngine.Vector3 position, string prefabName, global::Kampai.Game.BuildingDefinition buildingDef)
		{
			global::UnityEngine.GameObject gameObject = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.GameObject>(prefabName);
			if (!(gameObject != null))
			{
				return;
			}
			global::UnityEngine.GameObject vfxInstance = global::UnityEngine.Object.Instantiate(gameObject) as global::UnityEngine.GameObject;
			global::UnityEngine.Transform transform = vfxInstance.transform;
			transform.position = position;
			AdjustVFXPosition(buildingDef, transform);
			float num = 0f;
			foreach (global::UnityEngine.Transform item in transform)
			{
				global::UnityEngine.ParticleSystem component = item.GetComponent<global::UnityEngine.ParticleSystem>();
				if (component != null && component.duration > num)
				{
					num = component.duration;
				}
				component.Play();
			}
			routineRunner.StartCoroutine(WaitSomeTime(num, delegate
			{
				global::UnityEngine.Object.Destroy(vfxInstance);
			}));
		}

		private void CheckIfItIsTheFirstSwampGasForFTUE(global::Kampai.Game.Building building)
		{
			if (building.Definition.ID == 3015 && playerService.GetCountByDefinitionId(building.Definition.ID) == 1)
			{
				global::Kampai.UI.View.WayFinderSettings type = new global::Kampai.UI.View.WayFinderSettings(building.ID);
				createWayFinderSignal.Dispatch(type);
			}
		}

		private void UpdateTaskedMinions(int minionID, global::Kampai.Game.View.MinionTaskInfo taskInfo)
		{
			updateTaskedMinionSignal.Dispatch(minionID, taskInfo);
		}

		private void CreateDummyBuilding(global::Kampai.Game.BuildingDefinition buildingDefinition, global::UnityEngine.Vector3 position, bool isInInventory)
		{
			buildingID = buildingDefinition.ID;
			currentDummyBuilding = view.CreateDummyBuilding(buildingDefinition, position);
			showBuildingFootprintSignal.Dispatch(currentDummyBuilding, currentDummyBuilding.transform, global::Kampai.Util.Tuple.Create(currentDummyBuilding.Width, currentDummyBuilding.Depth), true);
		}

		private void SetBuildingPosition(int buildingId, global::UnityEngine.Vector3 position)
		{
			view.SetBuildingPosition(buildingId, position);
		}

		private void CancelPurchaseStart(bool invalidLocation)
		{
			if (currentDummyBuilding != null)
			{
				HideFootprint();
				openStoreSignal.Dispatch(buildingID, true);
				global::UnityEngine.Vector3 destination = BuildingUtil.UIToWorldCoords(global::UnityEngine.Camera.main, model.LastBuildingStorePosition);
				view.TweenBuildingToMenu(currentDummyBuilding.gameObject, destination, CancelPurchaseEnd);
				if (invalidLocation)
				{
					string type = localService.GetString("InvalidLocation");
					popupMessageSignal.Dispatch(type);
				}
			}
		}

		private void CancelPurchaseEnd(global::UnityEngine.GameObject lastScaffolding)
		{
			global::UnityEngine.Object.Destroy(lastScaffolding);
			lastScaffolding = null;
		}

		private void SendToInventory(int buildingId)
		{
			HideFootprint();
			view.ToInventory(buildingId);
			HideBuildingPlacementMenu();
		}

		private void HideBuildingPlacementMenu()
		{
			GetUISignal<global::Kampai.UI.View.ShowMoveBuildingMenuSignal>().Dispatch(false, 0, global::Kampai.UI.View.MoveBuildingMenuMediator.Buttons.None);
			GetUISignal<global::Kampai.UI.View.ShowHUDSignal>().Dispatch(true);
			GetUISignal<global::Kampai.UI.View.ShowStoreSignal>().Dispatch(true);
			GetUISignal<global::Kampai.UI.View.ShowWorldCanvasSignal>().Dispatch(true);
		}

		private void HideFootprint()
		{
			showBuildingFootprintSignal.Dispatch(null, null, global::Kampai.Util.Tuple.Create(1, 1), false);
		}

		private T GetUISignal<T>()
		{
			return uiContext.injectionBinder.GetInstance<T>();
		}

		private global::System.Collections.IEnumerator WaitAFrame(global::System.Action a)
		{
			yield return null;
			a();
		}

		private global::System.Collections.IEnumerator WaitSomeTime(float delayTime, global::System.Action a)
		{
			yield return new global::UnityEngine.WaitForSeconds(delayTime);
			a();
		}

		private void OnMinionTaskingAnimationDone(int minionId)
		{
			global::Kampai.Game.Minion byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Minion>(minionId);
			if (byInstanceId != null)
			{
				int id = byInstanceId.BuildingID;
				global::Kampai.Game.TaskableBuilding byInstanceId2 = playerService.GetByInstanceId<global::Kampai.Game.TaskableBuilding>(id);
				if (byInstanceId2 is global::Kampai.Game.DebrisBuilding)
				{
					MinionTaskComplete(minionId);
				}
			}
		}

		private void OnBurnedLandExpansion(int buildingId)
		{
			global::Kampai.Game.LandExpansionBuilding buildingByInstanceID = landExpansionService.GetBuildingByInstanceID(buildingId);
			view.RemoveBuilding(buildingByInstanceID.ID);
		}

		private void VerifyResourceBuildingSlots(global::Kampai.Game.Building building)
		{
			global::Kampai.Game.ResourceBuilding resourceBuilding = building as global::Kampai.Game.ResourceBuilding;
			int num = 0;
			int quantity = (int)playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID);
			if (resourceBuilding.BuildingNumber == 0)
			{
				resourceBuilding.BuildingNumber = playerService.GetInstancesByDefinitionID(resourceBuilding.Definition.ID).Count;
			}
			int index = resourceBuilding.BuildingNumber - 1;
			foreach (int slotUnlockLevel in resourceBuilding.Definition.SlotUnlocks[index].SlotUnlockLevels)
			{
				if (slotUnlockLevel <= quantity)
				{
					num++;
				}
			}
			if (num > resourceBuilding.MinionSlotsOwned)
			{
				resourceBuilding.MinionSlotsOwned = num;
			}
		}

		private void TryHarvest(int buildingID, global::System.Action callback)
		{
			harvestBuildingSignal.Dispatch(view.GetBuildingObject(buildingID), callback);
		}
	}
}
