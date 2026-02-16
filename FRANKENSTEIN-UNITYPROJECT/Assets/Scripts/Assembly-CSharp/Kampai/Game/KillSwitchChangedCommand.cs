namespace Kampai.Game
{
	public class KillSwitchChangedCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.IConfigurationsService configService { get; set; }

		[Inject(global::Kampai.Game.SocialServices.FACEBOOK)]
		public global::Kampai.Game.ISocialService facebookService { get; set; }

		[Inject(global::Kampai.Game.SocialServices.GAMECENTER)]
		public global::Kampai.Game.ISocialService gcService { get; set; }

		[Inject(global::Kampai.Game.SocialServices.GOOGLEPLAY)]
		public global::Kampai.Game.ISocialService gpService { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.Common.NimbleTelemetrySender nimbleTelemetryService { get; set; }

		[Inject]
		public global::Kampai.Util.Logging.Hosted.ILogglyService logglyService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			logglyService.UpdateKillSwitch();
			facebookService.updateKillSwitchFlag();
			gpService.updateKillSwitchFlag();
			if (configService.isKillSwitchOn(global::Kampai.Game.KillSwitch.SYNERGY))
			{
				logger.Info("=======================================================================================================================================================================================");
				logger.Info("=======================================================================================================================================================================================");
				logger.Info("#                                                                                                                                                                                     #");
				logger.Info("#                  SYNERGY KillSwitch ON                                                                                                                                              #");
				logger.Info("#                                                                                                                                                                                     #");
				logger.Info("=======================================================================================================================================================================================");
				telemetryService.SharingUsage(nimbleTelemetryService, false);
			}
			else
			{
				logger.Info("=======================================================================================================================================================================================");
				logger.Info("=======================================================================================================================================================================================");
				logger.Info("#                                                                                                                                                                                     #");
				logger.Info("#                  SYNERGY KillSwitch OFF                                                                                                                                             #");
				logger.Info("#                                                                                                                                                                                     #");
				logger.Info("=======================================================================================================================================================================================");
				telemetryService.SharingUsage(nimbleTelemetryService, true);
			}
		}
	}
}
