using UnityEngine;

namespace Kampai.Game
{
	public class GameStartCommand : global::strange.extensions.command.impl.Command
	{
		private const int maxSocialInitTries = 3;

		[Inject]
		public global::Kampai.Game.IInput input { get; set; }

		[Inject]
		public global::Kampai.Game.DCNTokenSignal dcnTokenSignal { get; set; }

		[Inject]
		public global::Kampai.Game.PopulateEnvironmentSignal environmentSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CancelAllNotificationSignal cancelAllNotificationSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SetupObjectManagersSignal setupMinionsSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SetupBuildingManagerSignal setupBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.LoadCruiseShipSignal cruiseShipSignal { get; set; }

		[Inject]
		public global::Kampai.Game.UpdateVolumeSignal updateVolumeSignal { get; set; }

		[Inject]
		public global::Kampai.Game.MuteVolumeSignal muteVolumeSignal { get; set; }

		[Inject]
		public global::Kampai.Game.EnableCameraBehaviourSignal enableCameraSignal { get; set; }

		[Inject]
		public global::Kampai.Game.DisableCameraBehaviourSignal disableCameraSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SetupTimeEventServiceSignal timeEventServiceSignal { get; set; }

		[Inject]
		public global::Kampai.Game.AwardLevelSignal awardLevelSignal { get; set; }

		[Inject(global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW)]
		public global::UnityEngine.GameObject contextView { get; set; }

		[Inject]
		public global::Kampai.Game.IUserSessionService userSessionService { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Game.ILandExpansionService landExpansionService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerDurationService playerDurationService { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowInAppMessageSignal showInAppMessageSignal { get; set; }

		[Inject]
		public global::Kampai.Game.FreezeTimeSignal freezeTimeSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SetupMailboxPromoSignal mailboxSetupSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Game.SocialInitAllServicesSignal socialInitSignal { get; set; }

		[Inject]
		public global::Kampai.Common.ISwrveService swrveService { get; set; }

		[Inject]
		public global::Kampai.Game.INotificationService notificationService { get; set; }

		[Inject]
		public global::Kampai.Util.ICoroutineProgressMonitor couroutineProgressMonitor { get; set; }

		[Inject]
		public global::Kampai.Common.ICoppaService coppaService { get; set; }

		[Inject]
		public global::Kampai.Game.SetupAudioSignal setupAudioSignal { get; set; }

		[Inject]
		public global::Kampai.Game.LoadMinionDataSignal loadMinionDataSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SetupNamedCharactersSignal setupNamedCharactersSignal { get; set; }

		[Inject]
		public global::Kampai.Common.VillainIslandMessageSignal villainIslandMessageSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RandomizeMinionPositionsSignal randomizeMinionPositionsSignal { get; set; }

		[Inject]
		public ILocalPersistanceService localPersistence { get; set; }

		[Inject]
		public global::Kampai.Game.ToggleStickerbookGlowSignal stickerbookGlow { get; set; }

		[Inject]
		public global::Kampai.Game.RandomFlyOverSignal randomFlyOverSignal { get; set; }

		[Inject]
		public global::Kampai.Game.InitializeMarketplaceSlotsSignal initializeSlotsSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RestorePlayersSalesSignal restoreSalesSignal { get; set; }

		[Inject]
		public global::Kampai.Game.StartMarketplaceOnboardingSignal startMarketplaceOnboardingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.MarketplaceUpdateSoldItemsSignal updateSoldItemsSignal { get; set; }

		public override void Execute()
		{
			Debug.Log("[GameStartCommand] ========== EXECUTE STARTED ==========");
			logger.EventStart("GameStartCommand.Execute");
			global::Kampai.Util.TimeProfiler.StartSection("game start");
			global::UnityEngine.Camera main = global::UnityEngine.Camera.main;
			Debug.Log("[GameStartCommand] Found main camera: " + (main != null ? main.name : "NULL"));
			global::UnityEngine.GameObject gameObject = main.gameObject;
            
            // [FIX] Removed duplicate Camera/CameraUtils bindings. 
            // These are now handled in GameContext.MapBindings to ensure early availability for Views.
            
			main.cullingMask = -1025;
            // CameraUtils is already attached in GameContext
			SetupCamera(gameObject);
			global::UnityEngine.GameObject gameObject2 = new global::UnityEngine.GameObject("Flowers");
			gameObject2.transform.parent = contextView.transform;
			base.injectionBinder.Bind<global::UnityEngine.GameObject>().ToValue(gameObject2).ToName(global::Kampai.Game.GameElement.FLOWER_PARENT);
			global::UnityEngine.GameObject gameObject3 = new global::UnityEngine.GameObject("ForSaleSigns");
			gameObject3.transform.parent = contextView.transform;
			base.injectionBinder.Bind<global::UnityEngine.GameObject>().ToValue(gameObject3).ToName(global::Kampai.Game.GameElement.FOR_SALE_SIGN_PARENT);
			// [FIX] LandExpansions is now bound in GameContext.PostBindings to ensure it exists for BuildingManagerView
			// global::UnityEngine.GameObject gameObject4 = new global::UnityEngine.GameObject("LandExpansions");
			// gameObject4.transform.parent = contextView.transform;
			// base.injectionBinder.Bind<global::UnityEngine.GameObject>().ToValue(gameObject4).ToName(global::Kampai.Game.GameElement.LAND_EXPANSION_PARENT);
			SetupCamera(gameObject);
			routineRunner.StartCoroutine(StartGame());
		}

		private global::System.Collections.IEnumerator PostLoadRoutine()
		{
			while (couroutineProgressMonitor.HasRunningTasks())
			{
				yield return null;
			}
			InitSocialIfNeeded();
			routineRunner.StartCoroutine(WaitAFrame());
		}

		private void InitMarketplaceSlotsIfNeeded()
		{
			if (coppaService.IsBirthdateKnown())
			{
				initializeSlotsSignal.Dispatch();
			}
		}

		private void InitSocialIfNeeded()
		{
			if (coppaService.IsBirthdateKnown())
			{
				RetrySocialInit(0);
			}
		}

		private void RetrySocialInit(int tries)
		{
			float time = 1f;
			routineRunner.StartCoroutine(WaitSomeTime(time, delegate
			{
				if (userSessionService.UserSession != null && !string.IsNullOrEmpty(userSessionService.UserSession.SessionID))
				{
					socialInitSignal.Dispatch();
				}
				else if (tries < 3)
				{
					tries++;
					logger.Log(global::Kampai.Util.Logger.Level.Info, "User Session was not available, will retry to initialize social networks in " + time + " second");
					RetrySocialInit(tries);
				}
				else
				{
					logger.Log(global::Kampai.Util.Logger.Level.Error, "User Session was never initilized so social services will not be initialized");
				}
			}));
		}

		private void SetupCamera(global::UnityEngine.GameObject camera)
		{
			camera.AddComponent<global::Kampai.Game.View.TouchPanView>();
			camera.AddComponent<global::Kampai.Game.View.TouchZoomView>();
			camera.AddComponent<global::Kampai.Game.View.TouchDragPanView>();
		}

		private global::System.Collections.IEnumerator WaitSomeTime(float time, global::System.Action a)
		{
			yield return new global::UnityEngine.WaitForSeconds(time);
			a();
		}

		private global::System.Collections.IEnumerator WaitAFrame()
		{
			yield return null;
			showInAppMessageSignal.Dispatch();
			RestoreFreezeState();
			string language = localService.GetLanguage();
			int minions = playerService.GetMinionCount();
			int seconds = playerService.LastGameStartUTC;
			playerDurationService.SetLastGameStartUTC();
			if (seconds != 0)
			{
				seconds = playerService.LastGameStartUTC - seconds;
			}
			int expansionCount = playerService.GetPurchasedExpansionCount();
			int expansionsLeft = landExpansionService.GetLandExpansionCount();
			string expansions = string.Format("{0}/{1}", expansionCount, expansionsLeft + expansionCount);
			if (localPersistence.HasKeyPlayer("StickerbookGlow"))
			{
				stickerbookGlow.Dispatch(true);
			}
			if (localPersistence.HasKeyPlayer("MarketSurfacing"))
			{
				int buildingID = global::System.Convert.ToInt32(localPersistence.GetDataPlayer("MarketSurfacing"));
				startMarketplaceOnboardingSignal.Dispatch(buildingID);
			}
			string swrveGroup = playerService.SWRVEGroup;
			telemetryService.Send_Telemetry_EVT_USER_DATA_AT_APP_START(seconds, language, minions, (!string.IsNullOrEmpty(swrveGroup)) ? swrveGroup : "anyVariant", expansions);
			if (localPersistence.HasKeyPlayer("StickerbookGlow"))
			{
				stickerbookGlow.Dispatch(true);
			}
			if (playerService.GetHighestFtueCompleted() == 999999)
			{
				randomFlyOverSignal.Dispatch(-1);
			}
			global::Kampai.Util.TimeProfiler.EndSection("game start");
		}

		private void RestoreFreezeState()
		{
			int freezeTime = playerService.GetFreezeTime();
			if (freezeTime > 0)
			{
				freezeTimeSignal.Dispatch(freezeTime);
			}
		}

		private global::System.Collections.IEnumerator StartGame()
		{
			// [FIX] Wait for critical services to be ready to prevent NullReferenceException
			float timeout = 30f;
			float timer = 0f;
			
			while (timer < timeout)
			{
				bool sessionReady = userSessionService.UserSession != null && !string.IsNullOrEmpty(userSessionService.UserSession.UserID);
				bool definitionsReady = definitionService.IsReady;

				if (sessionReady && definitionsReady)
				{
					break;
				}

				if (timer % 5f < 0.1f) // Log every 5 seconds
				{
					logger.Info("[GameStartCommand] Waiting for services... Session: {0}, Definitions: {1}", sessionReady, definitionsReady);
				}

				timer += 0.1f;
				yield return new global::UnityEngine.WaitForSeconds(0.1f);
			}

			if (timer >= timeout)
			{
				logger.Error("[GameStartCommand] Critical services failed to initialize in time! Game behavior may be unstable.");
			}
			else
			{
				logger.Info("[GameStartCommand] All services ready. Proceeding with game start.");
			}

			int id = couroutineProgressMonitor.StartTask("start game");
            
            // [FIX] Prevent NRE if UserSession is missing due to timeout/mocking
            if (userSessionService.UserSession != null && !string.IsNullOrEmpty(userSessionService.UserSession.UserID))
            {
			    playerService.ID = global::System.Convert.ToInt64(userSessionService.UserSession.UserID);
            }
            else
            {
                logger.Warning("[FIX] UserSession is null in GameStartCommand. Using fallback ID 1001.");
                playerService.ID = 1001;
                
                // Attempt to patch the session so other services don't crash
                try {
                    if (userSessionService.UserSession == null) {
                        userSessionService.UserSession = new global::Kampai.Game.UserSession();
                        userSessionService.UserSession.UserID = "1001";
                    }
                } catch {}
            }
			global::UnityEngine.GameObject prefab = global::Kampai.Util.KampaiResources.Load("Footprint") as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject footprint;
			if (prefab != null)
			{
				footprint = global::UnityEngine.Object.Instantiate(prefab) as global::UnityEngine.GameObject;
			}
			else
			{
				logger.Warning("[FIX] Footprint resource missing. Creating dummy.");
				footprint = new global::UnityEngine.GameObject("Footprint_Dummy");
			}
			footprint.transform.parent = contextView.transform;
			footprint.AddComponent<global::Kampai.Game.View.FootprintView>();
			enableCameraSignal.Dispatch(7);
			disableCameraSignal.Dispatch(8);
			notificationService.Initialize();
			logger.Debug("GameStartCommand: Notification Service initialized.");
			cancelAllNotificationSignal.Dispatch();
			timeEventServiceSignal.Dispatch(contextView);
			setupAudioSignal.Dispatch();
			environmentSignal.Dispatch(false);
			Debug.Log("[GameStartCommand] Dispatching setupBuildingSignal...");
			setupBuildingSignal.Dispatch();
			Debug.Log("[GameStartCommand] setupBuildingSignal dispatched");
			cruiseShipSignal.Dispatch();
			updateVolumeSignal.Dispatch();
			muteVolumeSignal.Dispatch();
			setupMinionsSignal.Dispatch();
			yield return null;
			loadMinionDataSignal.Dispatch();
			while (couroutineProgressMonitor.GetRunningTasksCount() > 1)
			{
				yield return null;
			}
			setupNamedCharactersSignal.Dispatch();
			playerDurationService.InitLevelUpUTC();
			while (couroutineProgressMonitor.GetRunningTasksCount() > 1)
			{
				yield return null;
			}
			randomizeMinionPositionsSignal.Dispatch();
			couroutineProgressMonitor.FinishTask(id);
			InitMarketplaceSlotsIfNeeded();
			villainIslandMessageSignal.Dispatch(false);
			global::Kampai.Game.MarketplaceDefinition definition = definitionService.Get<global::Kampai.Game.MarketplaceDefinition>();
			if (definition != null)
			{
				if (playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID) >= definition.LevelGate)
				{
					restoreSalesSignal.Dispatch();
				}
				else
				{
					updateSoldItemsSignal.Dispatch(false);
				}
			}
			else
			{
				logger.Warning("Marketplace Definition is null.");
			}
			routineRunner.StartCoroutine(PostLoadRoutine());
			logger.EventStop("GameStartCommand.Execute");
		}
	}
}
