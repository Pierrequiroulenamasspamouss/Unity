namespace Kampai.ForcedUpgrade
{
	public class ForcedUpgradeStartCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Main.InitLocalizationServiceSignal initLocalizationServiceSignal { get; set; }

		public override void Execute()
		{
			logger.Info("ForcedUpgrade scene starting...");
			initLocalizationServiceSignal.Dispatch();
		}
	}
}
