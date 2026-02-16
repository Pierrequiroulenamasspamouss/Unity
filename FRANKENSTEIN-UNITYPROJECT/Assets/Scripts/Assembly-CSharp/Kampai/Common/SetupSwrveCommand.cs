    namespace Kampai.Common
    {
        /// <summary>
        /// MOCK version of SetupSwrveCommand.
        /// Same signature, same injections, no Swrve SDK usage.
        /// </summary>
        public class SetupSwrveCommand : global::strange.extensions.command.impl.Command
        {
            private static bool swrveVerboseLogEnabled = false;

            // --------------------------------------------------------------------
            // Injections (KEPT IDENTICAL ON PURPOSE)
            // --------------------------------------------------------------------

            [Inject]
            public string kampaiUserID { get; set; }

            [Inject]
            public global::Kampai.Game.IConfigurationsService configurationsService { get; set; }

            [Inject(global::Kampai.Main.MainElement.MANAGER_PARENT)]
            public global::UnityEngine.GameObject managers { get; set; }

            [Inject]
            public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

            [Inject]
            public global::Kampai.Game.IPlayerService playerService { get; set; }

            [Inject]
            public global::Kampai.Common.ISwrveService swrveService { get; set; }

            [Inject]
            public global::Kampai.Game.ABTestSignal abTestSignal { get; set; }

            [Inject]
            public global::Kampai.Util.ILogger logger { get; set; }

            [Inject]
            public global::Kampai.Common.ABTestResourcesUpdatedSignal abTestResourcesUpdatedSignal { get; set; }

            // --------------------------------------------------------------------
            // Execute (NO-OP LOGIC)
            // --------------------------------------------------------------------

            public override void Execute()
            {
                // Respect kill switch logic so game flow stays consistent
                if (configurationsService != null &&
                    configurationsService.isKillSwitchOn(global::Kampai.Game.KillSwitch.SWRVE))
                {
                    if (logger != null)
                        logger.Info("MOCK: Swrve disabled by kill switch.");
                    MoveForwardWithoutSwrve();
                    return;
                }

                if (telemetryService != null && !telemetryService.SharingUsageEnabled())
                {
                    if (logger != null)
                        logger.Info("MOCK: Swrve disabled because sharing usage is disabled.");
                    MoveForwardWithoutSwrve();
                    return;
                }

                // Simulate AB test hookup (no SDK)
                if (abTestSignal != null)
                    abTestSignal.AddOnce(SetSWRVEData);

                if (playerService != null)
                    playerService.SWRVEGroup = global::Kampai.Util.ABTestModel.definitionVariants;

                if (logger != null)
                    logger.Debug("MOCK: SetupSwrveCommand executed. Swrve initialization skipped.");

                // Register mock service so telemetry flow does not break
                if (telemetryService != null && swrveService != null)
                {
                    telemetryService.AddTelemetrySender(swrveService);
                    telemetryService.AddIapTelemetryService(swrveService);
                }

                if (swrveService != null && telemetryService != null)
                {
                    swrveService.SharingUsage(telemetryService.SharingUsageEnabled());
                    swrveService.COPPACompliance();
                }
            }

            // --------------------------------------------------------------------
            // SAME PRIVATE METHODS (EMPTY IMPLEMENTATION)
            // --------------------------------------------------------------------

            private void SetSWRVEData(global::Kampai.Util.ABTestCommand.GameMetaData dataArgs)
            {
                if (playerService != null)
                    playerService.SWRVEGroup = dataArgs.definitionVariants;
            }

            private void MoveForwardWithoutSwrve()
            {
                if (abTestResourcesUpdatedSignal != null)
                    abTestResourcesUpdatedSignal.Dispatch(false);
            }

            private void Init(global::UnityEngine.GameObject swrveObject)
            {
                // MOCK: do nothing
            }

            private void InitSwrveLog()
            {
                // MOCK: do nothing
            }

            private void OnSwrveLog(SwrveLog.SwrveLogType type, object message, string tag)
            {
                // MOCK: silence
            }

            private static global::Kampai.Util.Logger.Level Convert(SwrveLog.SwrveLogType type)
            {
                return global::Kampai.Util.Logger.Level.Debug;
            }
        }
    }
