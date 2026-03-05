namespace Kampai.Main
{
    public class MainCompleteCommand : global::strange.extensions.command.impl.Command
    {
        private global::UnityEngine.AsyncOperation async;

        [Inject]
        public global::Kampai.Util.ILogger logger { get; set; }

        [Inject]
        public global::Kampai.Common.Service.Audio.IFMODService fmodService { get; set; }

        [Inject]
        public global::Kampai.Game.AutoSavePlayerStateSignal autoSavePlayerSignal { get; set; }

        [Inject]
        public global::Kampai.Game.ReloadConfigurationsPeriodicSignal reloadConfigs { get; set; }

        [Inject]
        public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

        [Inject]
        public global::Kampai.Main.LoadDevicePrefsSignal loadDevicePrefsSignal { get; set; }

        [Inject]
        public global::Kampai.Main.LoadSharedBundlesSignal bundleSignal { get; set; }

        [Inject]
        public global::Kampai.Common.Service.HealthMetrics.IClientHealthService clientHealthService { get; set; }

        [Inject]
        public global::Kampai.Common.LogTapEventMetricsSignal logTapEventMetricsSignal { get; set; }

        [Inject]
        public global::Kampai.Main.LoadLocalizationServiceSignal localServiceSignal { get; set; }

        [Inject]
        public global::Kampai.Game.SetupPushNotificationsSignal setupPushNotificationsSignal { get; set; }

        [Inject]
        public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

        [Inject]
        public global::Kampai.Main.LoadExternalScenesSignal loadExternalScenesSignal { get; set; }

        [Inject]
        public global::Kampai.Main.IAssetBundlesService assetBundlesService { get; set; }

        [Inject]
        public global::Kampai.Game.ITimedSocialEventService socialEventService { get; set; }

        [Inject]
        public global::Kampai.Game.IPlayerService playerService { get; set; }

        [Inject]
        public global::Kampai.Game.IDLCService dlcService { get; set; }

        [Inject]
        public global::Kampai.Download.DLCModel dlcModel { get; set; }

        [Inject]
        public global::Kampai.Game.NimbleOTSignal nimbleOTSignal { get; set; }

        [Inject]
        public global::Kampai.Util.ICoroutineProgressMonitor coroutineProgressMonitor { get; set; }

        [Inject]
        public global::Kampai.Main.LoadAudioSignal loadAudioSignal { get; set; }

        [Inject]
        public global::Kampai.Download.ShowDLCPanelSignal showDLCPanelSignal { get; set; }

        [Inject]
        public global::Kampai.Download.LaunchDownloadSignal launchDownloadSignal { get; set; }

        [Inject]
        public global::Kampai.Splash.SplashProgressUpdateSignal splashProgressDoneSignal { get; set; }

        [Inject]
        public global::Kampai.Splash.ILoadInService loadInService { get; set; }

        [Inject]
        public global::Kampai.Util.FastCommandPool fastCommandPool { get; set; }

        public override void Execute()
        {
            logger.EventStart("MainCompleteCommand.Execute");
            int quantity = (int)playerService.GetQuantity(global::Kampai.Game.StaticItem.TIER_ID);
            int quantity2 = (int)playerService.GetQuantity(global::Kampai.Game.StaticItem.TIER_GATE_ID);
            dlcService.SetPlayerDLCTier(quantity);
            if (dlcModel.HighestTierDownloaded < quantity2)
            {
                logger.Debug("DLC Highest Tier Downloaded is less than the tier Gate, launching DLC Download");
                showDLCPanelSignal.Dispatch(true);
                launchDownloadSignal.Dispatch(false);
            }
            else
            {
                global::Kampai.Util.TimeProfiler.StartSection("loading scenes");
                loadDevicePrefsSignal.Dispatch();
                loadExternalScenesSignal.Dispatch();
                loadAudioSignal.Dispatch();

                routineRunner.StartCoroutine(LevelLoadSequence());
            }
            logger.EventStop("MainCompleteCommand.Execute");
        }

        private global::System.Collections.IEnumerator LevelLoadSequence()
        {

            yield return null;

            logger.Debug("Starting Load Post External Scene");
            bundleSignal.Dispatch();
            localServiceSignal.Dispatch();
            global::Kampai.Util.DeviceCapabilities.Initialize();
            global::Kampai.Util.TimeProfiler.StartSection("loading game scene");
            logger.EventStart("MainCompleteCommand.LoadGame");
            
            global::UnityEngine.Application.LoadLevelAdditive("Game");
            global::UnityEngine.Application.LoadLevelAdditive("UI");
            
            splashProgressDoneSignal.Dispatch(100, 5f);

            yield return null;
            yield return null;

            
            logger.EventStop("MainCompleteCommand.LoadGame");
            global::Kampai.Util.TimeProfiler.EndSection("loading game scene");
            logger.EventStart("MainCompleteCommand.LoadUI");
            
            autoSavePlayerSignal.Dispatch();
            reloadConfigs.Dispatch();
            nimbleOTSignal.Dispatch();
            clientHealthService.MarkMeterEvent("AppFlow.AppStart");
            telemetryService.Send_Telemetry_EVT_USER_GAME_LOAD_FUNNEL("100 - Load Complete", playerService.SWRVEGroup);
            loadDevicePrefsSignal.Dispatch();
            logTapEventMetricsSignal.Dispatch();
            setupPushNotificationsSignal.Dispatch();

            DoLoadUI();
        }

        private void DoLoadUI()
        {
            try
            {
                socialEventService.GetPastEventsWithUnclaimedReward();
            }
            catch(global::System.Exception)
            {
                
            }

            try
            {
                if (loadInService != null && playerService != null)
                {
                    int levelId = (int)playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID);
                    loadInService.SaveTipsForNextLaunch(levelId);
                }
            }
            catch (global::System.Exception)
            {
            }

            
            // Critical Cleanup
            try
            {
                fastCommandPool.Warmup();
            }
            catch(global::System.Exception ex)
            {
                logger.Warning("FastCommandPool Warmup failed: " + ex);
            }
            
            global::strange.extensions.context.api.ICrossContextCapable splashContext = null;
            try
            {
                splashContext = injectionBinder.GetInstance<global::strange.extensions.context.api.ICrossContextCapable>(global::Kampai.Splash.SplashElement.CONTEXT);
            }
            catch (global::System.Exception ex) { logger.Warning(ex.ToString()); }

            // Warmup Shaders - wrapped in try catch as it can be unstable
            try 
            {
                global::UnityEngine.Shader.WarmupAllShaders();
            }
            catch(global::System.Exception ex)
            {
                logger.Error("Shader Warmup failed: " + ex);
            }

            if (splashContext != null)
            {
                try
                {
                    splashContext.injectionBinder.GetInstance<global::Kampai.Splash.HideSplashSignal>().Dispatch();
                }
                catch(global::System.Exception ex)
                {
                    logger.Error("HideSplashSignal dispatch failed: " + ex);
                }
            }
            
            logger.EventStop("MainCompleteCommand.LoadUI");
        }
    }
}
