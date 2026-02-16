using Kampai.Game.View;
using UnityEngine.EventSystems;

namespace Kampai.Game
{
    public class GameContext : global::Kampai.Util.BaseContext
    {
        private global::Kampai.Game.TimeEventService timeEventServiceInstance;

        public GameContext()
        {
            UnityEngine.Debug.Log("[GameContext] Default constructor called");
        }

        public GameContext(global::UnityEngine.MonoBehaviour view, bool autoStartup)
            : base(view, autoStartup)
        {
            UnityEngine.Debug.Log("[GameContext] Constructor called with view: " + (view != null ? view.name : "NULL") + ", autoStartup: " + autoStartup);
            UnityEngine.Debug.Log("[GameContext] Base constructor completed. Context should initialize now...");
        }

        protected override void mapBindings()
        {
            UnityEngine.Debug.Log("[GameContext.mapBindings] ========== LOWERCASE METHOD CALLED ==========");
            base.mapBindings();
            UnityEngine.Debug.Log("[GameContext.mapBindings] base.mapBindings() completed");
        }

        protected override void MapBindings()
        {
            UnityEngine.Debug.Log("[GameContext.MapBindings] ========== STARTING ==========");
            
            // [CRITICAL FIX] Bind fundamental signals locally to ensure availability
            // regardless of MainContext/UIContext initialization timing due to circular dependencies.
            // These are normally CrossContext but GameContext runs too early.
            injectionBinder.Bind<global::Kampai.Game.QuestScriptKernel>().ToSingleton();
            // signals now handled by waiting for MainContext in GameContextView

            // DIAGNOSTIC DELETED - Issue identified as timing/sharing problem.
            
            // [CRITICAL FIX] TimeEventService MUST be bound FIRST, before ANY other code runs.
            // UI Views are created DURING MapBindings (not after), and they inject QuestService,
            // which requires ITimeEventService. This MUST exist before anything else.
            // HOWEVER: We cannot inject its dependencies yet because BaseContext bindings aren't ready.
            // So we bind it now, but inject it in PostBindings().
            
            
            global::UnityEngine.GameObject timeEventGo = new global::UnityEngine.GameObject("TimeEventService");
            timeEventServiceInstance = timeEventGo.AddComponent<global::Kampai.Game.TimeEventService>();
            injectionBinder.Bind<global::Kampai.Game.ITimeEventService>().ToValue(timeEventServiceInstance).CrossContext();


            // Bind GameContext as ICrossContextCapable with name GameElement.CONTEXT.
            // This coexists with MainContext's BaseElement.CONTEXT binding because
            // BaseElement and GameElement are different enum types — their boxed values
            // are different dictionary keys even though both have integer value 0.
            injectionBinder.Bind<global::strange.extensions.context.api.ICrossContextCapable>()
                .ToValue(this).ToName(global::Kampai.Game.GameElement.CONTEXT).CrossContext();

            // [CRITICAL FIX] Bind MISSING named ILocalizationService instances
            // QuestService and other services need these named bindings
            // BaseContext only binds the default (unnamed) instance
            injectionBinder.Bind<global::Kampai.Main.ILocalizationService>()
                .To<global::Kampai.Main.MockHALService>()
                .ToSingleton()
                .ToName(global::Kampai.Main.LocalizationServices.EVENT)
                .CrossContext();
            
            injectionBinder.Bind<global::Kampai.Main.ILocalizationService>()
                .To<global::Kampai.Main.MockHALService>()
                .ToSingleton()
                .ToName(global::Kampai.Main.LocalizationServices.PLAYER)
                .CrossContext();

            // ========================================================================
            // RESTORE CRITICAL BINDINGS
            // MainContext binds some of these CrossContext, but GameContext cannot resolve 
            // some CrossContext bindings. We must rebind locally to ensure proper injection.
            // ========================================================================

            // ITimeService is in MainContext but with .Weak() - must rebind locally
            injectionBinder.Unbind<global::Kampai.Game.ITimeService>();
            injectionBinder.Bind<global::Kampai.Game.ITimeService>().To<global::Kampai.Game.TimeService>().ToSingleton();

            // PickControllerModel IS in MainContext (L70) CrossContext - but unbind for local
            injectionBinder.Unbind<global::Kampai.Common.PickControllerModel>();
            injectionBinder.Bind<global::Kampai.Common.PickControllerModel>().ToSingleton();

            // IPlayerService IS in MainContext (L152) as PurchaseAwarePlayerService
            // GameContext needs PlayerService (different implementation)
            injectionBinder.Unbind<global::Kampai.Game.IPlayerService>();
            injectionBinder.Bind<global::Kampai.Game.IPlayerService>().To<global::Kampai.Game.PlayerService>().ToSingleton();

            // These services ARE in MainContext CrossContext, but GameContext needs local control
            injectionBinder.Unbind<global::Kampai.Game.ILandExpansionService>();
            injectionBinder.Bind<global::Kampai.Game.ILandExpansionService>().To<global::Kampai.Game.LandExpansionService>().ToSingleton();


            injectionBinder.Unbind<global::Kampai.Game.IConfigurationsService>();
            injectionBinder.Bind<global::Kampai.Game.IConfigurationsService>().To<global::Kampai.Game.ConfigurationsService>().ToSingleton();

            // IFMODService - commands need local access even though MainContext has it CrossContext
            injectionBinder.Unbind<global::Kampai.Common.Service.Audio.IFMODService>();
            injectionBinder.Bind<global::Kampai.Common.Service.Audio.IFMODService>().To<global::Kampai.Game.MockFMODService>().ToSingleton();

            // ISwrveService - commands need local access even though MainContext has it CrossContext
            injectionBinder.Unbind<global::Kampai.Common.ISwrveService>();
            injectionBinder.Bind<global::Kampai.Common.ISwrveService>().To<global::Kampai.Game.MockSwrveService>().ToSingleton();

            // ILandExpansionConfigService - needed by SetupDebrisCommand
            injectionBinder.Bind<global::Kampai.Game.ILandExpansionConfigService>().To<global::Kampai.Game.LandExpansionConfigService>().ToSingleton();

            injectionBinder.Unbind<global::Kampai.Game.ICameraControlsService>();
            injectionBinder.Bind<global::Kampai.Game.ICameraControlsService>().To<global::Kampai.Game.CameraControlsService>().ToSingleton();

            injectionBinder.Unbind<global::Kampai.Common.IPickService>();
            injectionBinder.Bind<global::Kampai.Common.IPickService>().To<global::Kampai.Common.PickService>().ToSingleton();

            // [CRITICAL] IMignetteService - TouchInput (local) needs this
            injectionBinder.Unbind<global::Kampai.Game.Mignette.IMignetteService>();
            injectionBinder.Bind<global::Kampai.Game.Mignette.IMignetteService>().To<global::Kampai.Game.Mignette.MignetteService>().ToSingleton();

            // [CRITICAL] QuestService is CrossContext in BaseContext but depends on IPlayerService
            // Since we override IPlayerService locally, we must also override IQuestService locally
            injectionBinder.Unbind<global::Kampai.Game.IQuestService>();
            injectionBinder.Bind<global::Kampai.Game.IQuestService>().To<global::Kampai.Game.QuestService>().ToSingleton();

            // [CRITICAL] OrderBoardService also depends on IPlayerService (and is in BaseContext CrossContext)
            injectionBinder.Unbind<global::Kampai.Game.IOrderBoardService>();
            injectionBinder.Bind<global::Kampai.Game.IOrderBoardService>().To<global::Kampai.Game.OrderBoardService>().ToSingleton();

            // [CRITICAL] PrestigeService depends on QuestService and OrderBoardService (circular dependency)
            injectionBinder.Unbind<global::Kampai.Game.IPrestigeService>();
            injectionBinder.Bind<global::Kampai.Game.IPrestigeService>().To<global::Kampai.Game.PrestigeService>().ToSingleton();

            // [CRITICAL] SynergyService depends on IPlayerService (already bound locally - L76)
            injectionBinder.Unbind<global::Kampai.Game.ISynergyService>();
            injectionBinder.Bind<global::Kampai.Game.ISynergyService>().To<global::Kampai.Game.SynergyService>().ToSingleton();

            // [CRITICAL] TimedSocialEventService depends on IPlayerService and IUserSessionService
            injectionBinder.Unbind<global::Kampai.Game.ITimedSocialEventService>();
            injectionBinder.Bind<global::Kampai.Game.ITimedSocialEventService>().To<global::Kampai.Game.TimedSocialEventService>().ToSingleton();

            // [CRITICAL] UserSessionService depends on IPlayerService - must bind locally
            injectionBinder.Unbind<global::Kampai.Game.IUserSessionService>();
            injectionBinder.Bind<global::Kampai.Game.IUserSessionService>().To<global::Kampai.Game.UserSessionService>().ToSingleton();

            // [CRITICAL] PlayerDurationService depends on IPlayerService
            injectionBinder.Unbind<global::Kampai.Game.IPlayerDurationService>();
            injectionBinder.Bind<global::Kampai.Game.IPlayerDurationService>().To<global::Kampai.Game.PlayerDurationService>().ToSingleton();


            // KEEP LOCAL BINDINGS (Specific to Game Scene)
            // ========================================================================

            // [REMOVED] Audio Signals duplicates (bound in MainContext)

            // [REMOVED] CONTEXT binding moved to top of MapBindings (after TimeEventService)

            // Signals...
            injectionBinder.Bind<global::Kampai.Game.UserRegisteredSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Main.SetupHockeyAppUserSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Main.SetupSwrveSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Main.SetupDataMitigationSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.UpdateUserSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.UserSessionGrantedSignal>().ToSingleton();
            


            // --- FIX: LOCALIZED EVENT SYSTEM ---
            // Removed .CrossContext() to prevent "Conflicts: UnityEngine.GameObject"
            UnityEngine.GameObject eventSystem = new UnityEngine.GameObject("EventSystem");
            eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
            eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
            injectionBinder.Bind<UnityEngine.GameObject>()
                .ToValue(eventSystem).ToName(global::Kampai.Main.MainElement.UI_EVENTSYSTEM);

            // IDLCService stub (normally in DownloadContext, bind local stub)
            injectionBinder.Bind<global::Kampai.Game.IDLCService>().To<global::Kampai.Game.DLCService>().ToSingleton();
            
            // GameStartCommand.Execute() creates and binds these dynamically:
            // - CameraUtils (L148-149)
            // - FLOWER_PARENT GameObject (L150-152)
            // - FOR_SALE_SIGN_PARENT GameObject (L153-155)
            // - LAND_EXPANSION_PARENT GameObject (L156-158)
            // DO NOT bind them here to avoid conflicts

            // [FIX] Bind placeholder UI_GLASSCANVAS - will be overridden by UIContext when it creates the real canvas
            global::UnityEngine.GameObject placeholderCanvas = new global::UnityEngine.GameObject("PlaceholderGlassCanvas");
            injectionBinder.Bind<global::UnityEngine.GameObject>().ToValue(placeholderCanvas).ToName(global::Kampai.Main.MainElement.UI_GLASSCANVAS).CrossContext().Weak();

            // [RESTORED] Audio Signals - MainContext has them as Weak(), which might fail here.
            injectionBinder.Bind<global::Kampai.Main.StartLoopingAudioSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Main.PlayLocalAudioSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Main.StopLocalAudioSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Main.PlayMinionStateAudioSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Main.PlayLocalAudioOneShotSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Main.QueueLocalAudioCommandSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Main.PlayGlobalSoundFXSignal>().ToSingleton();
            
            injectionBinder.Bind<global::Kampai.Game.EnableCameraBehaviourSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.DisableCameraBehaviourSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.SocialInitAllServicesSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.ABTestSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Common.ABTestResourcesUpdatedSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.KillSwitchChangedSignal>().ToSingleton();

            // Local Game Scope Services
            injectionBinder.Bind<global::Kampai.Game.ICurrencyService>().To<global::Kampai.Game.NimbleCurrencyService>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.ShowDialogSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.StartPremiumPurchaseSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.OrderBoardUpdateTicketOnBoardSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.IInput>().To<global::Kampai.Game.TouchInput>().ToSingleton();

            injectionBinder.Bind<global::Kampai.Game.TemporaryMinionsService>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.EnvironmentBuilder>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.INotificationService>().To<global::Kampai.Game.NotificationService>().ToSingleton();

            // [FIX] The Game scene has "Game Camera" tagged "MainCamera". Find it.
            global::UnityEngine.GameObject mainCameraGo = global::UnityEngine.GameObject.FindWithTag("MainCamera");
            global::UnityEngine.Camera main = (mainCameraGo != null) ? mainCameraGo.GetComponent<global::UnityEngine.Camera>() : null;
            
            // Also try Camera.main as a secondary lookup
            if (main == null) {
                main = global::UnityEngine.Camera.main;
                if (main != null) mainCameraGo = main.gameObject;
            }
            
            // Try finding by name "Game Camera" as last resort
            if (main == null) {
                global::UnityEngine.GameObject gameCamGo = global::UnityEngine.GameObject.Find("Game Camera");
                if (gameCamGo != null) {
                    main = gameCamGo.GetComponent<global::UnityEngine.Camera>();
                    if (main != null) {
                        mainCameraGo = gameCamGo;
                        // Ensure it's tagged properly
                        mainCameraGo.tag = "MainCamera";
                    }
                }
            }

            if (main == null) {
                // As absolute last resort, create a camera, but log clearly
                global::UnityEngine.Debug.LogWarning("[GameContext] MainCamera not found in scene! Creating minimal fallback.");
                mainCameraGo = new global::UnityEngine.GameObject("MainCamera_Fallback");
                main = mainCameraGo.AddComponent<global::UnityEngine.Camera>();
                mainCameraGo.tag = "MainCamera";
            } else {
                global::UnityEngine.Debug.Log("[GameContext] Found MainCamera: " + mainCameraGo.name);
            }

            // Bind Camera (Local) - ToValue implies singleton, do not add .ToSingleton()
            injectionBinder.Bind<global::UnityEngine.Camera>()
                .ToValue(main)
                .ToName(global::Kampai.Main.MainElement.CAMERA); 

            // Bind Camera GameObject - ToValue implies singleton
            injectionBinder.Bind<global::UnityEngine.GameObject>()
                .ToValue(mainCameraGo)
                .ToName(global::Kampai.Main.MainElement.CAMERA)
                .CrossContext();

            // [FIX] Bind CameraUtils early
            global::Kampai.Game.View.CameraUtils cameraUtils = mainCameraGo.GetComponent<global::Kampai.Game.View.CameraUtils>();
            if (cameraUtils == null) cameraUtils = mainCameraGo.AddComponent<global::Kampai.Game.View.CameraUtils>();
            
            if (cameraUtils == null) throw new global::System.Exception("[GameContext] Failed to create CameraUtils");
            
            injectionBinder.Bind<global::Kampai.Game.View.CameraUtils>().ToValue(cameraUtils);

            global::UnityEngine.Vector3 inNormal = new global::UnityEngine.Vector3(0f, 1f, 0f);
            global::UnityEngine.Vector3 inPoint = new global::UnityEngine.Vector3(0f, 0f, 0f);
            global::UnityEngine.Plane value = new global::UnityEngine.Plane(inNormal, inPoint);
            global::Kampai.Util.Boxed<global::UnityEngine.Plane> o = new global::Kampai.Util.Boxed<global::UnityEngine.Plane>(value);
            injectionBinder.Bind<global::Kampai.Util.Boxed<global::UnityEngine.Plane>>().ToValue(o).ToName(global::Kampai.Game.GameElement.GROUND_PLANE);

            injectionBinder.Bind<global::Kampai.Game.IDCNService>().To<global::Kampai.Game.DCNService>().ToSingleton();

            injectionBinder.Bind<global::Kampai.Game.IdleMinionSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.AddMinionSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.AddNamedCharacterSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.MinionWalkPathSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.MinionRunPathSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.MinionAppearSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.AnimateSelectedMinionSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.DebugUpdateGridSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.DebugKeyHitSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.StartGroupGachaSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.StartMinionRouteSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.StartTaskSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.StartTeleportTaskSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.EnableMinionRendererSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.RevealBuildingSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.StartIncidentalAnimationSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.MinionAcknowledgeSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.ShowBuildingFootprintSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.DisplayBuildingSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.BRBCelebrationAnimationSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.CreateDummyBuildingSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.ReadyAnimationSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.PlayMinionNoAnimAudioSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.RouteMinionToLeisureSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.KillFunSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.KillFunWithMinionStateSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.TeleportMinionToLeisureSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.SetPartyStatesSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.TapMinionSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.HideTSMCharacterSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.TikiBarSetAnimParamSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.PhilCelebrateSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.PhilGetAttentionSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.PhilBeginIntroLoopSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.PhilPlayIntroSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.PhilSitAtBarSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.PhilActivateSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.PhilEnableTikiBarControllerSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.PhilGoToTikiBarSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.PhilGoToStartLocationSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.StuartAddToStageSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.StuartStartPerformingSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.StuartGetOnStageSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.StuartGetOnStageImmediateSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.StuartCelebrateSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.StuartGetAttentionSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.KevinGreetVillainSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.KevinWaveFarewellSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.KevinGoToWelcomeHutSignal>().ToSingleton();
            base.commandBinder.Bind<global::Kampai.Game.KevinFrolicsSignal>().To<global::Kampai.Game.KevinFrolicsCommand>();
            mediationBinder.Bind<MouseDragPanView>()
    .To<MouseDragPanMediator>();

            injectionBinder.Bind<global::Kampai.Game.CreateVillainViewSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.VillainGotoCarpetSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.VillainGotoCabanaSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.VillainPlayWelcomeSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.VillainPlayFarewellSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.VillainGotoBoatSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.VillainAttachToShipSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.CruiseShipModel>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.CruiseShipService>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.SignalActionSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.InventoryBuildingMovementSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.RestoreScaffoldingViewSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.RestoreRibbonViewSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.RestorePlatformViewSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.RestoreBuildingViewSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.UpdateTaskedMinionSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.UnitializeCameraSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.CameraResetPanVelocitySignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.CameraAutoZoomSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.CameraAutoZoomCompleteSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.CameraAutoPanSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.CameraAutoPanCompleteSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.CameraCinematicZoomSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.CameraCinematicPanSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.ToggleVignetteSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.MinionReactSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.Scaffolding>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.UnlockCharacterModel>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.Mignette.MignetteGameModel>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.MinionSeekPositionSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.CloseConfirmationSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.PurchaseNewBuildingSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.CancelPurchaseSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.SendBuildingToInventorySignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.CreateInventoryBuildingSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.SetBuildingPositionSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.MoveScaffoldingSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.EjectAllMinionsFromBuildingSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.ITikiBarService>().To<global::Kampai.Game.TikiBarService>().ToSingleton()
                .CrossContext();
            injectionBinder.Bind<global::Kampai.Game.RestoreMinionAtTikiBarSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.ReleaseMinionFromTikiBarSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.PathCharacterToTikiBarSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.TeleportCharacterToTikiBarSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.UnveilCharacterObjectSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.BeginCharacterLoopAnimationSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.PopUnleashedCharacterToTikiBarSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.UnleashCharacterAnimationCompleteSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.CharacterDrinkingCompleteSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.CharacterIntroCompleteSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.ToggleStickerbookGlowSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.LeisureBuildingCooldownSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.LeisureProximityRadiusSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.LeisureProximityRadiusCommand>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.OrderBoardUpdateTicketOnBoardSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.OpenOrderBoardSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.CameraAutoMoveToInstanceSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.CameraAutoMoveToPositionSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.CameraCinematicMoveToBuildingSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.CameraAutoMoveToMignetteSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.SwitchCameraSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.RandomFlyOverSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.InitCharacterObjectSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.HighlightBuildingSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.BobPointsAtSignSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.BobIdleInTownHallSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.BobCelebrateLandExpansionSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.BobCelebrateLandExpansionCompleteSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.BobReturnToTownSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.AnimatePhilSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.AnimateStuartSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.AnimateKevinSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.KevinWanderSignal>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.CancelAllNotificationSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.CancelNotificationSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.PlayLocalAudioCommand>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.StartLoopingAudioCommand>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.StopLocalAudioCommand>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.Controller.Audio.PlayMinionStateAudioCommand>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.PlayLocalAudioOneShotCommand>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.QueueLocalAudioCommand>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.PlayGlobalSoundFXCommand>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.UpdatePlayerDLCTierSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Game.UpdateMarketplaceRepairStateSignal>().ToSingleton();
            base.commandBinder.Bind<global::Kampai.Common.StartSignal>().To<global::Kampai.Game.GameStartCommand>();
            base.commandBinder.Bind<global::Kampai.Common.AppPauseSignal>().To<global::Kampai.Game.GamePauseCommand>();
            base.commandBinder.Bind<global::Kampai.Common.AppResumeSignal>().To<global::Kampai.Game.GameResumeCommand>();
            base.commandBinder.Bind<global::Kampai.Common.AppQuitSignal>().To<global::Kampai.Game.Controller.AppQuitCommand>();
            			base.commandBinder.Bind<global::Kampai.Common.NimbleTelemetryEventsPostedSignal>().To<global::Kampai.Common.Controller.HandleNimblePostedEventsCommand>();
            
            
            base.commandBinder.Bind<global::Kampai.Game.AllMinionLoadedSignal>().To<global::Kampai.Game.AllMinionLoadedCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CreateForSaleSignSignal>().To<global::Kampai.Game.CreateForSaleSignCommand>();
            base.commandBinder.Bind<global::Kampai.Game.UpdateForSaleSignsSignal>().To<global::Kampai.Game.UpdateForSaleSignsCommand>();
            base.commandBinder.Bind<global::Kampai.Game.DisplayMinionSelectedIconSignal>().To<global::Kampai.Game.View.DisplayMinionSelectedIconCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CreateMarketplaceSlotSignal>().To<global::Kampai.Game.CreateMarketplaceSlotCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CreateLockedPremiumSlotSignal>().To<global::Kampai.Game.CreateLockedPremiumSlotCommand>();
            base.commandBinder.Bind<global::Kampai.Game.PurchaseMarketplaceSlotSignal>().To<global::Kampai.Game.PurchaseMarketplaceSlotCommand>();
            base.commandBinder.Bind<global::Kampai.Game.UpdateMarketplaceSaleOrderSignal>().To<global::Kampai.Game.UpdateMarketplaceSaleOrderCommand>();
            base.commandBinder.Bind<global::Kampai.Game.UpdateMarketplaceSlotStateSignal>().To<global::Kampai.Game.UpdateMarketplaceSlotStateCommand>();
            base.commandBinder.Bind<global::Kampai.Game.MarketplaceItemSoldSignal>().To<global::Kampai.Game.MarketplaceItemSoldCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SellToAISignal>().To<global::Kampai.Game.SellToAICommand>();
            base.commandBinder.Bind<global::Kampai.Game.GenerateBuyItemsSignal>().To<global::Kampai.Game.GenerateBuyItemsCommand>();
            base.commandBinder.Bind<global::Kampai.Game.BuyMarketplaceItemSignal>().To<global::Kampai.Game.BuyMarketplaceItemCommand>();
            base.commandBinder.Bind<global::Kampai.Game.InitializeMarketplaceSlotsSignal>().To<global::Kampai.Game.InitializeMarketplaceSlotsCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CollectMarketplaceSaleSignal>().To<global::Kampai.Game.CollectMarketplaceSaleCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RestorePlayersSalesSignal>().To<RestorePlayersSalesCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CancelMarketPlaceSaleSignal>().To<global::Kampai.Game.CancelMarketplaceSaleCommand>();
            base.commandBinder.Bind<global::Kampai.Game.StartMarketplaceRefreshTimerSignal>().To<StartMarketplaceRefreshTimerCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RefreshSaleItemsSignal>().To<RefreshSaleItemsCommand>();
            base.commandBinder.Bind<global::Kampai.Game.InterpolateSaleTimeSignal>().To<global::Kampai.Game.InterpolateSaleTimeCommand>();
            base.commandBinder.Bind<global::Kampai.Game.ReportMarketplaceTransactionSignal>().To<global::Kampai.Game.ReportMarketplaceTransactionCommand>();
            base.commandBinder.Bind<global::Kampai.Game.StartMarketplaceOnboardingSignal>().To<StartMarketplaceOnboardingCommand>();
            base.commandBinder.Bind<global::Kampai.Game.MailboxSelectedSignal>().To<global::Kampai.Game.MailboxSelectedCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SetupMailboxPromoSignal>().To<global::Kampai.Game.SetupMailboxPromoCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SetupBuildingManagerSignal>().To<global::Kampai.Game.SetupBuildingManagerCommand>();
            base.commandBinder.Bind<global::Kampai.Game.PlaceBuildingSignal>().To<global::Kampai.Game.PlaceBuildingCommand>();
            base.commandBinder.Bind<global::Kampai.Game.AddFootprintSignal>().To<global::Kampai.Game.AddFootprintCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RemoveBuildingSignal>().To<global::Kampai.Game.RemoveBuildingCommand>();
            base.commandBinder.Bind<global::Kampai.Game.BuildingCooldownCompleteSignal>().To<global::Kampai.Game.BuildingCooldownCompleteCommand>();
            base.commandBinder.Bind<global::Kampai.Game.BuildingChangeStateSignal>().To<global::Kampai.Game.BuildingChangeStateCommand>();
            base.commandBinder.Bind<global::Kampai.Game.ConstructionCompleteSignal>().To<global::Kampai.Game.ConstructionCompleteCommand>();
            base.commandBinder.Bind<global::Kampai.Game.StartConstructionSignal>().To<StartConstructionCommand>();
            base.commandBinder.Bind<global::Kampai.Common.ScheduleCooldownSignal>().To<ScheduleCooldownCommand>();
            base.commandBinder.Bind<global::Kampai.Game.DisplayInaccessibleMessageSignal>().To<global::Kampai.Game.DisplayInaccessibleMessageCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RestoreBuildingSignal>().To<global::Kampai.Game.RestoreBuildingCommand>();
            base.commandBinder.Bind<global::Kampai.Game.AdjustBuildingTimerSignal>().To<global::Kampai.Game.AdjustBuildingTimerCommand>();
            base.commandBinder.Bind<global::Kampai.Common.TryHarvestBuildingSignal>().To<global::Kampai.Game.View.TryHarvestBuildingCommand>();
            base.commandBinder.Bind<global::Kampai.Game.QuestStepRushSignal>().To<global::Kampai.Game.QuestStepRushCommand>();
            base.commandBinder.Bind<global::Kampai.Game.DeliverTaskItemSignal>().To<global::Kampai.Game.DeliverTaskItemCommand>();
            base.commandBinder.Bind<global::Kampai.Game.QuestTimeoutSignal>().To<global::Kampai.Game.QuestTimeoutCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CleanupDebrisSignal>().To<global::Kampai.Game.View.CleanupDebrisCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RepairBridgeSignal>().To<global::Kampai.Game.View.RepairBridgeCommand>();
            base.commandBinder.Bind<global::Kampai.Game.PanAndOpenModalSignal>().To<global::Kampai.Game.PanAndOpenModalCommand>();
            base.commandBinder.Bind<global::Kampai.Game.PanAndOpenQuestSignal>().To<global::Kampai.Game.PanAndOpenQuestCommand>();
            base.commandBinder.Bind<global::Kampai.Game.TSMCharacterSelectedSignal>().To<global::Kampai.Game.TSMCharacterSelectedCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CancelTSMQuestTaskSignal>().To<global::Kampai.Game.CancelTSMQuestTaskCommand>();
            base.commandBinder.Bind<global::Kampai.Game.UpdateTSMQuestTaskSignal>().To<global::Kampai.Game.UpdateTSMQuestTaskCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CreateTSMQuestTaskSignal>().To<global::Kampai.Game.CreateTSMQuestTaskCommand>();
            base.commandBinder.Bind<global::Kampai.Game.StartQuestTaskSignal>().To<global::Kampai.Game.StartTSMQuestTaskCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CollectTSMQuestTaskRewardSignal>().To<global::Kampai.Game.CollectTSMQuestTaskRewardCommand>();
            base.commandBinder.Bind<global::Kampai.Game.DisplaySaleSignal>().To<global::Kampai.Game.DisplaySaleCommand>();
            base.commandBinder.Bind<global::Kampai.Game.DisplayConfirmationSignal>().To<global::Kampai.Game.DisplayConfirmationCommand>();
            base.commandBinder.Bind<global::Kampai.Game.QueueConfirmationSignal>().To<global::Kampai.Game.QueueConfirmationCommand>();
            base.commandBinder.Bind<global::Kampai.Game.DisplayStickerbookSignal>().To<global::Kampai.Game.View.DisplayStickerbookCommand>();
            base.commandBinder.Bind<global::Kampai.Game.ShowDialogSignal>().To<global::Kampai.Game.ShowDialogCommand>();
            base.commandBinder.Bind<global::Kampai.Game.UpdatePrestigeListSignal>().To<global::Kampai.Game.UpdatePrestigeListCommand>();
            base.commandBinder.Bind<global::Kampai.Game.InitBuildingObjectSignal>().To<global::Kampai.Game.InitBuildingObjectCommand>();
            base.commandBinder.Bind<global::Kampai.Game.ToggleMinionRendererSignal>().To<global::Kampai.Game.ToggleMinionRendererCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RelocateTaskedMinionsSignal>().To<global::Kampai.Game.RelocateTaskedMinionsCommand>();
            base.commandBinder.Bind<global::Kampai.Game.GetNewQuestSignal>().To<global::Kampai.Game.GetNewQuestCommand>();
            base.commandBinder.Bind<global::Kampai.Game.QuestCompleteSignal>().To<global::Kampai.Game.QuestCompleteCommand>();
            base.commandBinder.Bind<global::Kampai.UI.View.QuestTaskReadySignal>().To<global::Kampai.Game.QuestTaskReadyCommand>();
            base.commandBinder.Bind<global::Kampai.Game.QuestHarvestableSignal>().To<global::Kampai.Game.QuestHarvestableCommand>();
            base.commandBinder.Bind<global::Kampai.Game.UpdateNamedCharacterPrestigeSignal>().To<global::Kampai.Game.UpdateNamedCharacterPrestigeCommand>();
            base.commandBinder.Bind<global::Kampai.Game.UpdateVillainPrestigeSignal>().To<global::Kampai.Game.UpdateVillainPrestigeCommand>();
            base.commandBinder.Bind<global::Kampai.Game.QueueCabanaSignal>().To<global::Kampai.Game.QueueCabanaCommand>();
            base.commandBinder.Bind<global::Kampai.Game.MoveInCabanaSignal>().To<global::Kampai.Game.MoveInCabanaCommand>();
            base.commandBinder.Bind<global::Kampai.Game.MoveOutCabanaSignal>().To<global::Kampai.Game.MoveOutCabanaCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CheckResourceBuildingSlotsSignal>().To<global::Kampai.Game.CheckResourceBuildingSlotsCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SetupTSMCharacterSignal>().To<global::Kampai.Game.SetupTSMCharacterCommand>();
            base.commandBinder.Bind<global::Kampai.Game.MinionPrestigeCompleteSignal>().To<global::Kampai.Game.MinionPrestigeCompleteCommand>();
            base.commandBinder.Bind<global::Kampai.Game.AddMinionToTikiBarSignal>().To<global::Kampai.Game.AddMinionToTikiBarCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RemoveMinionFromTikiBarSignal>().To<global::Kampai.Game.RemoveMinionFromTikiBarCommand>();
            base.commandBinder.Bind<global::Kampai.Game.UnveilCharacterSignal>().To<global::Kampai.Game.UnveilCharacterCommand>();
            base.commandBinder.Bind<global::Kampai.Game.BeginCharacterIntroLoopSignal>().To<global::Kampai.Game.BeginCharacterIntroLoopCommand>();
            base.commandBinder.Bind<global::Kampai.Game.UnleashCharacterAtShoreSignal>().To<global::Kampai.Game.UnleashCharacterAtShoreCommand>();
            base.commandBinder.Bind<global::Kampai.Game.PopulateEnvironmentSignal>().To<global::Kampai.Game.PopulateEnvironmentCommand>();
            base.commandBinder.Bind<global::Kampai.Game.PopulateBuildingSignal>().To<global::Kampai.Game.PopulateBuildingCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SetupBrokenBridgesSignal>().To<global::Kampai.Game.SetupBrokenBridgesCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SetupLandExpansionsSignal>().To<global::Kampai.Game.SetupLandExpansionsCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SetupForSaleSignsSignal>().To<global::Kampai.Game.SetupForSaleSignsCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SetupFlowersSignal>().To<global::Kampai.Game.SetupFlowersCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SetupDebrisSignal>().To<global::Kampai.Game.SetupDebrisCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SetupAspirationalBuildingsSignal>().To<global::Kampai.Game.SetupAspirationalBuildingsCommand>();
            base.commandBinder.Bind<global::Kampai.Game.MapAnimationEventSignal>().To<global::Kampai.Game.MapAnimationEventCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RelocateCharacterSignal>().To<global::Kampai.Game.RelocateCharacterCommand>();
            
            
            // [DUPLICATE REMOVED] IFMODService was already bound at top of file (L53)
            
            injectionBinder.Bind<global::System.Collections.Generic.List<global::Kampai.Util.Point>>().ToValue(new global::System.Collections.Generic.List<global::Kampai.Util.Point>(10)).ToName(global::Kampai.Game.GameElement.RELOCATION_POINTS);
            
            // [FIX] Moved GetInstance calls to PostBindings() to avoid circular dependency during binding phase

            global::Kampai.Main.PlayGlobalSoundFXSignal instance7 = injectionBinder.GetInstance<global::Kampai.Main.PlayGlobalSoundFXSignal>();
            global::Kampai.Game.PlayGlobalSoundFXCommand playGlobalSoundFXCommand = null;
            instance7.AddListener(delegate (string audioClip)
            {
                if (playGlobalSoundFXCommand == null)
                {
                    playGlobalSoundFXCommand = injectionBinder.GetInstance<global::Kampai.Game.PlayGlobalSoundFXCommand>();
                }
                playGlobalSoundFXCommand.Execute(audioClip);
            });
            base.commandBinder.Bind<global::Kampai.Main.PlayGlobalMusicSignal>().To<global::Kampai.Game.PlayGlobalMusicCommand>();
            base.commandBinder.Bind<global::Kampai.Main.PlayMignetteMusicSignal>().To<global::Kampai.Game.PlayMignetteMusicCommand>();
            base.commandBinder.Bind<global::Kampai.Main.FadeBackgroundAudioSignal>().To<global::Kampai.Game.FadeBackgroundAudioCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SetupObjectManagersSignal>().To<global::Kampai.Game.SetupObjectManagersCommand>();
            base.commandBinder.Bind<global::Kampai.Game.LoadMinionDataSignal>().To<global::Kampai.Game.LoadMinionDataCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CreateMinionSignal>().To<global::Kampai.Game.CreateMinionCommand>();
            base.commandBinder.Bind<global::Kampai.Game.InitCharacterObjectSignal>().To<global::Kampai.Game.InitCharacterObjectCommand>();
            base.commandBinder.Bind<global::Kampai.Game.MinionMoveToSignal>().To<global::Kampai.Game.MoveMinionCommand>();
            base.commandBinder.Bind<global::Kampai.Game.MoveMinionFinishedSignal>().To<global::Kampai.Game.MoveMinionFinishedCommand>();
            base.commandBinder.Bind<global::Kampai.Game.MinionRunToSignal>().To<global::Kampai.Game.RunMinionCommand>();
            base.commandBinder.Bind<global::Kampai.Common.MinionReactInRadiusSignal>().To<global::Kampai.Game.MinionReactInRadiusCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CharacterArrivedAtDestinationSignal>().To<global::Kampai.Game.CharacterArrivedAtDestinationCommand>();
            base.commandBinder.Bind<global::Kampai.Game.UnlockMinionsSignal>().To<global::Kampai.Game.UnlockMinionsCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SendMinionToLeisureSignal>().To<SendMinionToLeisureCommand>();
            global::Kampai.Game.LeisureProximityRadiusSignal instance8 = injectionBinder.GetInstance<global::Kampai.Game.LeisureProximityRadiusSignal>();
            global::Kampai.Game.LeisureProximityRadiusCommand leisureProximityRadiusCommand = null;
            instance8.AddListener(delegate
            {
                if (leisureProximityRadiusCommand == null)
                {
                    leisureProximityRadiusCommand = injectionBinder.GetInstance<global::Kampai.Game.LeisureProximityRadiusCommand>();
                }
                leisureProximityRadiusCommand.Execute();
            });
            base.commandBinder.Bind<global::Kampai.Game.GenerateTemporaryMinionSignal>().To<global::Kampai.Game.GenerateTemporaryMinionCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SetNamedCharacterCollidersSignal>().To<global::Kampai.Game.SetNamedCharacterCollidersCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SetupNamedCharactersSignal>().To<global::Kampai.Game.SetupNamedCharactersCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CreateNamedCharacterViewSignal>().To<global::Kampai.Game.CreateNamedCharacterViewCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RestoreNamedCharacterSignal>().To<RestoreNamedCharacterCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RestoreVillainSignal>().To<global::Kampai.Game.RestoreVillainCommand>();
            base.commandBinder.Bind<global::Kampai.Game.NamedCharacterRemovedFromTikiBarSignal>().To<global::Kampai.Game.NamedCharacterRemovedFromTikiBarCommand>();
            base.commandBinder.Bind<global::Kampai.Game.StartCOPPAFlowSignal>().To<global::Kampai.Game.StartCOPPAFlowCommand>();
            base.commandBinder.Bind<global::Kampai.Game.MinionStateChangeSignal>().To<global::Kampai.Game.MinionStateChangeCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SelectionCompleteSignal>().To<global::Kampai.Game.SelectionCompleteCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RerouteMinionPathsSignal>().To<global::Kampai.Game.RerouteMinionPathsCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RestoreMinionStateSignal>().To<RestoreMinionStateCommand>();
            base.commandBinder.Bind<global::Kampai.Game.StartMinionTaskSignal>().To<StartMinionTaskCommand>();
            base.commandBinder.Bind<global::Kampai.Game.AwardLevelSignal>().To<global::Kampai.Game.View.AwardLevelCommand>();
            base.commandBinder.Bind<global::Kampai.Game.LevelUpSignal>().To<global::Kampai.Game.View.LevelUpCommand>();
            base.commandBinder.Bind<global::Kampai.Game.ReconcileLevelUnlocksSignal>().To<global::Kampai.Game.ReconcileLevelUnlocksCommand>();
            base.commandBinder.Bind<global::Kampai.Game.PointBobLandExpansionSignal>().To<global::Kampai.Game.PointBobLandExpansionCommand>();
            base.commandBinder.Bind<global::Kampai.Game.BobFrolicsSignal>().To<global::Kampai.Game.BobFrolicsCommand>();
            base.commandBinder.Bind<global::Kampai.Game.AddStuartToStageSignal>().To<global::Kampai.Game.AddStuartToStageCommand>();
            base.commandBinder.Bind<global::Kampai.Game.StartMignetteSignal>().To<global::Kampai.Game.StartMignetteCommand>();
            base.commandBinder.Bind<global::Kampai.Game.StopMignetteSignal>().To<global::Kampai.Game.StopMignetteCommand>();
            base.commandBinder.Bind<global::Kampai.Game.ShowAndIncreaseMignetteScoreSignal>().To<global::Kampai.Game.ShowAndIncreaseMignetteScoreCommand>();
            base.commandBinder.Bind<global::Kampai.Game.DCNTokenSignal>().To<global::Kampai.Game.DCNTokenCommand>();
            base.commandBinder.Bind<global::Kampai.Game.DCNFeaturedSignal>().To<global::Kampai.Game.DCNFeaturedCommand>();
            base.commandBinder.Bind<global::Kampai.Game.DCNEventSignal>().To<global::Kampai.Game.DCNEventCommand>();
            base.commandBinder.Bind<global::Kampai.Game.UpdatePlayerDLCTierSignal>().To<global::Kampai.Game.UpdatePlayerDLCTierCommand>();
            base.commandBinder.Bind<global::Kampai.Game.ScheduleNotificationSignal>().To<global::Kampai.Game.ScheduleNotificationCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CancelNotificationSignal>().To<global::Kampai.Game.CancelNotificationCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CancelAllNotificationSignal>().To<global::Kampai.Game.CancelAllNotificationCommand>();
            base.commandBinder.Bind<global::Kampai.Game.ReengageNotificationSignal>().To<global::Kampai.Game.ReengageNotificationCommand>();
            base.commandBinder.Bind<global::Kampai.Game.TimedQuestNotificationSignal>().To<global::Kampai.Game.TimedQuestNotificationCommand>();
            base.commandBinder.Bind<global::Kampai.Game.ScheduleJobsCompleteNotificationsSignal>().To<global::Kampai.Game.ScheduleJobCompleteNotificationsCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SetupAudioSignal>().To<global::Kampai.Game.SetupAudioCommand>();
            base.commandBinder.Bind<global::Kampai.Game.UpdateVolumeSignal>().To<UpdateVolumeCommand>();
            base.commandBinder.Bind<global::Kampai.Game.MuteVolumeSignal>().To<MuteVolumeCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SavePlayerSignal>().To<global::Kampai.Game.SavePlayerCommand>();
            base.commandBinder.Bind<global::Kampai.UI.View.DisablePickControllerSignal>().To<global::Kampai.Game.DisablePickControllerCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SetupTimeEventServiceSignal>().To<global::Kampai.Game.SetupTimeEventServiceCommand>();
            base.commandBinder.Bind<global::Kampai.Game.HarvestReadySignal>().To<global::Kampai.Game.View.HarvestReadyCommand>();
            base.commandBinder.Bind<global::Kampai.Game.UpdateQuestWorldIconsSignal>().To<global::Kampai.Game.View.UpdateQuestWorldIconsCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RemoveQuestWorldIconSignal>().To<global::Kampai.Game.View.RemoveQuestWorldIconCommand>();
            base.commandBinder.Bind<global::Kampai.Game.InsufficientInputsSignal>().To<global::Kampai.Game.InsufficientInputsCommand>();
            base.commandBinder.Bind<global::Kampai.Game.PostTransactionSignal>().To<global::Kampai.Game.PostTransactionCommand>();
            base.commandBinder.Bind<global::Kampai.Game.BuildingReactionSignal>().To<global::Kampai.Game.BuildingReactionCommand>();
            base.commandBinder.Bind<global::Kampai.Game.IncidentalAnimationSignal>().To<global::Kampai.Game.IncidentalAnimationCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RemoveGagFromPlayerSignal>().To<global::Kampai.Game.RemoveGagFromPlayerCommand>();
            base.commandBinder.Bind<global::Kampai.Game.BRBCelebrationAnimationSignal>().To<global::Kampai.Game.BRBCelebrationAnimationCommand>();
            base.commandBinder.Bind<global::Kampai.Game.EnableBuildingAnimatorsSignal>().To<global::Kampai.Game.EnableBuildingAnimatorsCommand>();
            base.commandBinder.Bind<global::Kampai.Game.EnableAllMinionRenderersSignal>().To<global::Kampai.Game.EnableAllMinionRenderersCommand>();
            base.commandBinder.Bind<global::Kampai.Game.PartySignal>().To<global::Kampai.Game.PartyCommand>();
            base.commandBinder.Bind<global::Kampai.Game.ShuffleCompositeBuildingPiecesSignal>().To<global::Kampai.Game.ShuffleCompositeBuildingPiecesCommand>();
            base.commandBinder.Bind<global::Kampai.Game.ConfirmBuildingMovementSignal>().To<global::Kampai.Game.ConfirmBuildingMovementCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CancelBuildingMovementSignal>().To<global::Kampai.Game.CancelBuildingMovementCommand>();
            base.commandBinder.Bind<global::Kampai.Game.InventoryBuildingMovementSignal>().To<global::Kampai.Game.InventoryBuildingMovementCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CreateBuildingInInventorySignal>().To<global::Kampai.Game.CreateBuildingInInventoryCommand>();
            base.commandBinder.Bind<global::Kampai.Common.SelectMinionSignal>().To<global::Kampai.Game.SelectMinionCommand>();
            base.commandBinder.Bind<global::Kampai.Common.DeselectMinionSignal>().To<global::Kampai.Game.DeselectMinionCommand>();
            base.commandBinder.Bind<global::Kampai.Common.DeselectAllMinionsSignal>().To<global::Kampai.Game.DeselectAllMinionsCommand>();
            base.commandBinder.Bind<global::Kampai.Common.DeselectTaskedMinionsSignal>().To<global::Kampai.Game.DeselectTaskedMinionsCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RandomizeMinionPositionsSignal>().To<global::Kampai.Game.RandomizeMinionPositionsCommand>();
            base.commandBinder.Bind<global::Kampai.Common.DeselectPickSignal>().To<global::Kampai.Game.DeselectPickController>();
            base.commandBinder.Bind<global::Kampai.Common.MinionPickSignal>().To<global::Kampai.Game.MinionPickController>();
            base.commandBinder.Bind<global::Kampai.Common.BuildingPickSignal>().To<global::Kampai.Game.BuildingPickController>();
            base.commandBinder.Bind<global::Kampai.Common.DragAndDropPickSignal>().To<global::Kampai.Game.DragAndDropPickController>();
            base.commandBinder.Bind<global::Kampai.Common.MagnetFingerPickSignal>().To<global::Kampai.Game.MagnetFingerPickController>();
            base.commandBinder.Bind<global::Kampai.Common.DoubleClickPickSignal>().To<global::Kampai.Game.DoubleClickPickController>();
            base.commandBinder.Bind<global::Kampai.Common.LandExpansionPickSignal>().To<global::Kampai.Game.LandExpansionPickController>();
            base.commandBinder.Bind<global::Kampai.Common.VillainIslandMessageSignal>().To<global::Kampai.Main.VillainIslandMessageController>();
            base.commandBinder.Bind<global::Kampai.Common.TikiBarViewPickSignal>().To<global::Kampai.Game.TikiBarViewPickController>();
            base.commandBinder.Bind<global::Kampai.Game.CallMinionSignal>().To<global::Kampai.Game.CallMinionCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RushTaskSignal>().To<global::Kampai.Game.RushMinionTaskCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RushOneMinionInBuildingSignal>().To<global::Kampai.Game.RushOneMinionInBuildingCommand>();
            base.commandBinder.Bind<global::Kampai.Common.AddMinionToEventServiceSignal>().To<global::Kampai.Game.View.AddMinionToEventServiceCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CameraAutoMoveSignal>().To<global::Kampai.Game.CameraAutoMoveCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CameraAutoMoveToBuildingSignal>().To<global::Kampai.Game.View.CameraAutoMoveToBuildingCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CameraAutoMoveToBuildingDefSignal>().To<global::Kampai.Game.View.CameraAutoMoveToBuildingDefCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CameraAutoMoveToMignetteSignal>().To<global::Kampai.Game.View.CameraAutoMoveToMignetteCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CameraAutoMoveToInstanceSignal>().To<global::Kampai.Game.CameraAutoMoveToInstanceCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CameraAutoMoveToPositionSignal>().To<global::Kampai.Game.CameraAutoMoveToPositionCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CameraCinematicMoveToBuildingSignal>().To<global::Kampai.Game.View.CameraCinematicMoveToBuildingCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CameraInstanceFocusSignal>().To<global::Kampai.Game.CameraInstanceFocusCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RandomFlyOverSignal>().To<global::Kampai.Game.RandomFlyOverCommand>();
            base.commandBinder.Bind<global::Kampai.Game.ShowHiddenBuildingsSignal>().To<global::Kampai.Game.ShowHiddenBuildingCommand>();
            base.commandBinder.Bind<global::Kampai.Game.OpenOrderBoardSignal>().To<global::Kampai.Game.View.OpenOrderBoardCommand>();
            base.commandBinder.Bind<global::Kampai.Game.OrderBoardFillOrderSignal>().To<global::Kampai.Game.OrderBoardFillOrderCommand>();
            base.commandBinder.Bind<global::Kampai.Game.OrderBoardDeleteOrderSignal>().To<global::Kampai.Game.OrderBoardDeleteOrderCommand>();
            base.commandBinder.Bind<global::Kampai.Game.OrderBoardSetNewTicketSignal>().To<global::Kampai.Game.OrderBoardSetNewTicketCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SetupOrderBoardServiceSignal>().To<global::Kampai.Game.SetupOrderBoardServiceCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RestoreLeisureBuildingSignal>().To<global::Kampai.Game.View.RestoreLeisureBuildingCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RestoreCraftingBuildingsSignal>().To<global::Kampai.Game.View.RestoreCraftingBuildingsCommand>();
            base.commandBinder.Bind<global::Kampai.Game.RestoreTaskableBuildingSignal>().To<global::Kampai.Game.View.RestoreTaskableBuildingCommand>();
            base.commandBinder.Bind<global::Kampai.Game.TeleportMinionToBuildingSignal>().To<global::Kampai.Game.TeleportMinionToBuildingCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CameraZoomBeachSignal>().To<global::Kampai.Game.CameraZoomBeachCommand>();
            base.commandBinder.Bind<global::Kampai.Game.StuartShowCompleteSignal>().To<global::Kampai.Game.StuartShowCompleteCommand>();
            base.commandBinder.Bind<global::Kampai.Game.GenerateTemporaryMinionsStageSignal>().To<global::Kampai.Game.GenerateTemporaryMinionsStageCommand>();
            base.commandBinder.Bind<global::Kampai.Game.LoadCruiseShipSignal>().To<global::Kampai.Game.LoadCruiseShipCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SwitchCameraSignal>().To<global::Kampai.Game.SwitchCameraCommand>();
            // [REMOVED] BuildingZoomSignal - BaseContext already binds it at line 382, duplicate causes conflict
            base.commandBinder.Bind<global::Kampai.Game.SelectLandExpansionSignal>().To<global::Kampai.Game.SelectLandExpansionCommand>();
            base.commandBinder.Bind<global::Kampai.Game.PurchaseLandExpansionSignal>().To<global::Kampai.Game.PurchaseLandExpansionCommand>();
            base.commandBinder.Bind<global::Kampai.Game.HighlightLandExpansionSignal>().To<global::Kampai.Game.HighlightLandExpansionCommand>();
            base.commandBinder.Bind<global::Kampai.Game.OpenBuildingMenuSignal>().To<global::Kampai.Game.OpenBuildingMenuCommand>();
            base.commandBinder.Bind<global::Kampai.Game.LoadRushDialogSignal>().To<global::Kampai.Game.View.LoadRushDialogCommand>();
            base.commandBinder.Bind<global::Kampai.Game.LoadCurrencyWarningSignal>().To<global::Kampai.Game.View.LoadCurrencyWarningDialogCommand>();
            base.commandBinder.Bind<global::Kampai.Game.OpenStorageBuildingSignal>().To<global::Kampai.Game.View.OpenStorageBuildingCommand>();
            base.commandBinder.Bind<global::Kampai.Game.OpenStageBuildingSignal>().To<global::Kampai.Game.View.OpenStageBuildingCommand>();
            base.commandBinder.Bind<global::Kampai.Game.SaveDevicePrefsSignal>().To<SaveDevicePrefsCommand>();
            base.commandBinder.Bind<global::Kampai.Game.LoadAnimatorStateInfoSignal>().To<global::Kampai.Util.AnimatorStateInfo.LoadAnimatorStateInfoCommand>();
            base.commandBinder.Bind<global::Kampai.Game.UnloadAnimatorStateInfoSignal>().To<global::Kampai.Util.AnimatorStateInfo.UnloadAnimatorStateInfoCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CloneUserSignal>().To<global::Kampai.Game.CloneUserCommand>();
            base.commandBinder.Bind<global::Kampai.Main.OpenHelpSignal>().To<global::Kampai.Main.OpenHelpCommand>();
            base.commandBinder.Bind<global::Kampai.UI.View.ShowInAppMessageSignal>().To<global::Kampai.UI.View.ShowInAppMessageCommand>();
            base.commandBinder.Bind<global::Kampai.Game.CreditCollectionRewardSignal>().To<global::Kampai.Game.CreditCollectionRewardCommand>();
            base.commandBinder.Bind<global::Kampai.Game.GotoSignal>().To<global::Kampai.Game.GotoCommand>();
            base.commandBinder.Bind<global::Kampai.Game.FreezeTimeSignal>().To<global::Kampai.Game.View.FreezeTimeCommand>();
            injectionBinder.Bind<global::Kampai.Game.SocialOrderBoardCompleteSignal>().ToSingleton().CrossContext();
            base.commandBinder.Bind<global::Kampai.Game.SocialOrderBoardCompleteSignal>().To<global::Kampai.Game.SocialOrderBoardCompleteCommand>();
            base.mediationBinder.Bind<global::Kampai.Game.View.NamedCharacterManagerView>().To<global::Kampai.Game.View.NamedCharacterManagerMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.View.VillainManagerView>().To<global::Kampai.Game.View.VillainManagerMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.View.BuildingManagerView>().To<global::Kampai.Game.View.BuildingManagerMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.View.OrderBoardBuildingObjectView>().To<global::Kampai.Game.View.OrderBoardBuildingObjectMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.View.TikiBarBuildingObjectView>().To<global::Kampai.Game.View.TikiBarBuildingObjectMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.View.LeisureBuildingObjectView>().To<global::Kampai.Game.View.LeisureBuildingObjectMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.View.EnvironmentalMignetteView>().To<global::Kampai.Game.View.EnvironmentalMignetteMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.Mignette.View.MignetteBuildingCooldownView>().To<global::Kampai.Game.Mignette.View.MignetteBuildingCooldownMediator>();
            base.mediationBinder.Bind<EnvironmentAudioManagerView>().To<EnvironmentAudioManagerMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.View.EnvironmentAudioEmitterView>().To<global::Kampai.Game.View.EnvironmentAudioEmitterMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.View.FootprintView>().To<global::Kampai.Game.View.FootprintMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.View.Audio.PositionalAudioListenerView>().To<global::Kampai.Game.View.Audio.PositionalAudioListenerMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.View.CompositeBuildingView>().To<global::Kampai.Game.View.CompositeBuildingMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.View.StorageBuildingView>().To<global::Kampai.Game.View.StorageBuildingMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.View.BobView>().To<global::Kampai.Game.View.BobMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.View.KevinView>().To<global::Kampai.Game.View.KevinMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.View.PhilView>().To<global::Kampai.Game.View.PhilMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.View.StuartView>().To<global::Kampai.Game.View.StuartMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.CruiseShipView>().To<global::Kampai.Game.CruiseShipMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.View.VignetteView>().To<global::Kampai.Game.View.VignetteMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.View.ParticleSystemAudioHandlerView>().To<global::Kampai.Game.View.ParticleSystemAudioHandlerMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.View.TouchPanView>().To<global::Kampai.Game.View.TouchPanMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.View.TouchZoomView>().To<global::Kampai.Game.View.TouchZoomMediator>();
            base.mediationBinder.Bind<global::Kampai.Game.View.TouchDragPanView>().To<global::Kampai.Game.View.TouchDragPanMediator>();

            BindManagers();


            // [CRASH FIX] Bind Environment LAST - forces immediate resolution of QuestService etc.
            // By now, TimeEventService and ProcessNextPendingTransactionSignal are ready.
            injectionBinder.Bind<global::Kampai.Game.Environment>().ToSingleton();
            injectionBinder.Bind<global::Kampai.Game.IEnvironmentNavigation>().ToValue(injectionBinder.GetInstance<global::Kampai.Game.Environment>());
            
            UnityEngine.Debug.Log("[GameContext.MapBindings] ========== COMPLETED! Returning to StrangeIoC ==========");
        }

        private void BindManagers()
        {
            // Buildings
            BindManager<global::Kampai.Game.View.BuildingManagerView>("Buildings", global::Kampai.Game.GameElement.BUILDING_MANAGER);
            
            // Named Characters
            BindManager<global::Kampai.Game.View.NamedCharacterManagerView>("NamedCharacters", global::Kampai.Game.GameElement.NAMED_CHARACTER_MANAGER);
            
            // Villains
            BindManager<global::Kampai.Game.View.VillainManagerView>("Villains", global::Kampai.Game.GameElement.VILLAIN_MANAGER);
            
            // Minions
            global::Kampai.Game.View.MinionManagerView minionManager = BindManager<global::Kampai.Game.View.MinionManagerView>("Minions", global::Kampai.Game.GameElement.MINION_MANAGER);
            injectionBinder.Bind<global::Kampai.Game.View.MinionIdleNotifier>().ToValue(minionManager);
        }



        private T BindManager<T>(string goName, global::Kampai.Game.GameElement bindingName) where T : global::UnityEngine.MonoBehaviour
        {
            global::UnityEngine.GameObject gameObject = null;
            T result = null;

            global::UnityEngine.MonoBehaviour contextMB = contextView as global::UnityEngine.MonoBehaviour;
            if (contextMB != null)
            {
                global::UnityEngine.Transform existing = contextMB.transform.Find(goName);
                if (existing != null)
                {
                    gameObject = existing.gameObject;
                    result = gameObject.GetComponent<T>();
                    if (result == null) result = gameObject.AddComponent<T>();
                }
            }

            if (gameObject == null)
            {
                gameObject = new global::UnityEngine.GameObject(goName);
                if (contextMB != null) gameObject.transform.parent = contextMB.transform;
                result = gameObject.AddComponent<T>();
            }

            injectionBinder.Bind<global::UnityEngine.GameObject>().ToValue(gameObject).ToName(bindingName);
            return result;
        }

        // NOTE: Do NOT override postBindings() here.
        // BaseContext.postBindings() already calls PostBindings() then base.postBindings().
        // Overriding it here would cause PostBindings() to execute TWICE, creating duplicate bindings.

        protected override void PostBindings()
        {
            UnityEngine.Debug.Log("[GameContext.PostBindings] ========== STARTING ==========");

            // Bind LAND_EXPANSION_PARENT - BuildingManagerView needs this during Start()
            global::UnityEngine.GameObject landExpansionParent = new global::UnityEngine.GameObject("LandExpansionParent");
            if (contextView != null)
            {
                global::UnityEngine.MonoBehaviour contextMB = contextView as global::UnityEngine.MonoBehaviour;
                if (contextMB != null)
                {
                    landExpansionParent.transform.parent = contextMB.transform;
                }
            }
            injectionBinder.Bind<global::UnityEngine.GameObject>()
                .ToValue(landExpansionParent)
                .ToName(global::Kampai.Game.GameElement.LAND_EXPANSION_PARENT);

            // Inject TimeEventService dependencies NOW, after all BaseContext bindings are ready
            if (timeEventServiceInstance != null)
            {
                injectionBinder.injector.Inject(timeEventServiceInstance);
            }
            
            injectionBinder.Bind<global::Kampai.Util.PathFinder>().ToSingleton();
            
            // [FIX] Set up audio signal listeners AFTER all bindings are complete to avoid circular dependency
            global::Kampai.Game.StartLoopingAudioCommand startLoopingAudioCommand = injectionBinder.GetInstance<global::Kampai.Game.StartLoopingAudioCommand>();
            global::Kampai.Main.StartLoopingAudioSignal instance = injectionBinder.GetInstance<global::Kampai.Main.StartLoopingAudioSignal>();
            instance.AddListener(delegate (CustomFMOD_StudioEventEmitter emitter, string name, global::System.Collections.Generic.Dictionary<string, float> evtParams)
            {
                startLoopingAudioCommand.Execute(emitter, name, evtParams);
            });
            
            global::Kampai.Game.PlayLocalAudioCommand playLocalAudioCommand = injectionBinder.GetInstance<global::Kampai.Game.PlayLocalAudioCommand>();
            global::Kampai.Main.PlayLocalAudioSignal instance2 = injectionBinder.GetInstance<global::Kampai.Main.PlayLocalAudioSignal>();
            instance2.AddListener(delegate (CustomFMOD_StudioEventEmitter emitter, string name, global::System.Collections.Generic.Dictionary<string, float> evtParams)
            {
                playLocalAudioCommand.Execute(emitter, name, evtParams);
            });
            
            global::Kampai.Game.StopLocalAudioCommand stopLocalAudioCommand = injectionBinder.GetInstance<global::Kampai.Game.StopLocalAudioCommand>();
            global::Kampai.Main.StopLocalAudioSignal instance3 = injectionBinder.GetInstance<global::Kampai.Main.StopLocalAudioSignal>();
            instance3.AddListener(delegate (CustomFMOD_StudioEventEmitter emitter)
            {
                stopLocalAudioCommand.Execute(emitter);
            });
            
            global::Kampai.Game.Controller.Audio.PlayMinionStateAudioCommand playMinionStateAudioCommand = injectionBinder.GetInstance<global::Kampai.Game.Controller.Audio.PlayMinionStateAudioCommand>();
            global::Kampai.Main.PlayMinionStateAudioSignal instance4 = injectionBinder.GetInstance<global::Kampai.Main.PlayMinionStateAudioSignal>();
            instance4.AddListener(delegate (MinionStateAudioArgs args)
            {
                playMinionStateAudioCommand.Execute(args);
            });
            
            global::Kampai.Game.PlayLocalAudioOneShotCommand playLocalAudioOneShotCommand = injectionBinder.GetInstance<global::Kampai.Game.PlayLocalAudioOneShotCommand>();
            global::Kampai.Main.PlayLocalAudioOneShotSignal instance5 = injectionBinder.GetInstance<global::Kampai.Main.PlayLocalAudioOneShotSignal>();
            instance5.AddListener(delegate (CustomFMOD_StudioEventEmitter emitter, string audioClip)
            {
                playLocalAudioOneShotCommand.Execute(emitter, audioClip);
            });
            
            global::Kampai.Main.QueueLocalAudioCommandSignal instance6 = injectionBinder.GetInstance<global::Kampai.Main.QueueLocalAudioCommandSignal>();
            global::Kampai.Game.QueueLocalAudioCommand queueLocalAudioCommand = injectionBinder.GetInstance<global::Kampai.Game.QueueLocalAudioCommand>();
            instance6.AddListener(delegate (CustomFMOD_StudioEventEmitter emitter, string audioClip)
            {
                queueLocalAudioCommand.Execute(emitter, audioClip);
            });
            
            // DO NOT dispatch StartSignal here! Context.Start() calls Launch() AFTER
            // postBindings() completes (including MVCSContext mediation). Launch() calls
            // BaseContext.Launch() which dispatches StartSignal at the correct time.
            // Dispatching here would cause exceptions (missing deps) that leave ICommand
            // bound in CommandBinder.getCommand's transient pattern, poisoning the binder.
            
            UnityEngine.Debug.Log("[GameContext.PostBindings] ========== COMPLETED! ==========");
        }

    }
}