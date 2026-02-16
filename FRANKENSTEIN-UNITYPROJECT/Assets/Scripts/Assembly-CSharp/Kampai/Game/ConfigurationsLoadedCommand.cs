namespace Kampai.Game
{
	public class ConfigurationsLoadedCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public bool init { get; set; }

		[Inject]
		public global::Kampai.Util.IInvokerService invoker { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IDLCService dlcService { get; set; }

		[Inject]
		public global::Kampai.Common.DownloadManifestSignal downloadManifestSignal { get; set; }

		[Inject]
		public ILocalPersistanceService persistService { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Main.SetupUpsightSignal setupUpsightSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CheckUpgradeSignal checkUpgradeSignal { get; set; }

		[Inject]
		public global::Kampai.Util.DeviceInformation deviceInformation { get; set; }

		[Inject]
		public global::Kampai.Common.ABTestResourcesUpdatedSignal abTestResourcesUpdatedSignal { get; set; }

		public override void Execute()
		{
			logger.EventStart("ConfigurationsLoadedCommand.Execute");
			invoker.Add(delegate
			{
				if (init)
				{
					telemetryService.Send_Telemetry_EVT_USER_GAME_LOAD_FUNNEL("20 - Loaded Config", playerService.SWRVEGroup);
					global::Kampai.Util.TargetPerformance targetPerformance = new global::Kampai.Util.DeviceCapabilities().GetTargetPerformance(logger, global::UnityEngine.Application.platform, deviceInformation);
					string text = dlcService.GetDisplayQualityLevel();
					if (text == null || text.Equals(string.Empty))
					{
						text = global::Kampai.Util.QualityHelper.getStartingQuality(targetPerformance);
						dlcService.SetDisplayQualityLevel(text);
					}
					global::Kampai.Util.TargetPerformance targetPerformance2 = global::Kampai.Util.QualityHelper.getCurrentTarget(targetPerformance, text);
					string value = persistService.GetData("FORCE_LOD");
					if (!string.IsNullOrEmpty(value))
					{
						targetPerformance2 = (global::Kampai.Util.TargetPerformance)(int)global::System.Enum.Parse(typeof(global::Kampai.Util.TargetPerformance), value);
					}
					logger.Debug("DLC quality to download = {0}", targetPerformance2.ToString());
					dlcService.SetDownloadQualityLevel(targetPerformance2);
					downloadManifestSignal.Dispatch();
					abTestResourcesUpdatedSignal.Dispatch(true);
					setupUpsightSignal.Dispatch();
					logger.EventStop("ConfigurationsLoadedCommand.Execute");
				}
				checkUpgradeSignal.Dispatch();
			});
		}
	}
}
