namespace Kampai.Common
{
	public class SetupUpsightCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.IConfigurationsService configurationsService { get; set; }

		[Inject]
		public global::Kampai.Main.IUpsightService upsightService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public ILocalPersistanceService localPersistanceService { get; set; }

		[Inject]
		public global::Kampai.Common.ICoppaService coppaService { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		public override void Execute()
		{
			if (configurationsService.isKillSwitchOn(global::Kampai.Game.KillSwitch.UPSIGHT))
			{
				logger.Info("Upsight is disabled by kill switch.");
				return;
			}
			bool optOutStatus = coppaService.Restricted() || !telemetryService.SharingUsageEnabled();
			upsightService.Init(optOutStatus);
			telemetryService.AddTelemetrySender(upsightService);
			telemetryService.AddIapTelemetryService(upsightService);
			upsightService.SharingUsage(telemetryService.SharingUsageEnabled());
			upsightService.COPPACompliance();
		}
	}
}
