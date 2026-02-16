namespace Kampai.Common.Controller
{
    public class HandleNimblePostedEventsCommand
        : global::strange.extensions.command.impl.Command
    {
        // On garde les injections pour éviter les erreurs Strange
        [Inject]
        public global::Kampai.Common.DeltaDNATelemetryService deltaDnaTelemetryService { get; set; }

        [Inject]
        public global::Kampai.Common.DeltaDNADataMitigationSettings dataMitigationModel { get; set; }

        [Inject]
        public global::Kampai.Util.ILogger logger { get; set; }

        public override void Execute()
        {
            // MOCK : Nimble est désactivé / absent
            // On ne tente PAS d'accéder à la DLL native
            // On ne poste PAS d'événements DeltaDNA

            logger.Info(
                "[MOCK NIMBLE] HandleNimblePostedEventsCommand skipped. " +
                "Nimble operational telemetry is disabled."
            );

            // No-op volontaire
        }
    }
}
