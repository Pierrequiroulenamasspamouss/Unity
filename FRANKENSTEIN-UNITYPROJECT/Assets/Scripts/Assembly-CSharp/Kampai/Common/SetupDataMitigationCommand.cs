namespace Kampai.Common
{
	public class SetupDataMitigationCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Common.SetupDataMitigationParameters parameters { get; set; }

		[Inject]
		public global::Kampai.Game.IConfigurationsService configurationService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Common.DeltaDNADataMitigationSettings dataMitigationModel { get; set; }

		[Inject]
		public global::Kampai.Common.DeltaDNATelemetryService deltaDnaTelemetryService { get; set; }

		[Inject]
		public global::Kampai.Common.NimbleTelemetryEventsPostedSignal postNimbleEvents { get; set; }

		[Inject]
		public global::Kampai.Common.INimbleTelemetryPostListener postListener { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		public override void Execute()
		{
			string userID = parameters.UserID;
			bool flag = global::Kampai.Util.FeatureAccessUtil.isAccessible(global::Kampai.Game.AccessControlledFeature.DELTADNA, configurationService.GetConfigurations(), userID, logger);
			if (flag)
			{
				logger.Info("SetupDataMitigationCommand: User allowed to use DeltaNDA {0}", userID);
				deltaDnaTelemetryService.Initialize();
				deltaDnaTelemetryService.ClientIp = parameters.ClientIP;
				telemetryService.AddIapTelemetryService(deltaDnaTelemetryService);
			}
			else
			{
				logger.Info("SetupDataMitigationCommand: User not allowed to use DeltaNDA {0}", userID);
			}
			dataMitigationModel.SendRawNimbleEventsToDeltaDNA = flag;
			dataMitigationModel.SendRemappedNimbleEventsToDeltaDNA = flag;
			postNimbleEvents.Dispatch();
			postListener.Setup();
		}
	}
}
