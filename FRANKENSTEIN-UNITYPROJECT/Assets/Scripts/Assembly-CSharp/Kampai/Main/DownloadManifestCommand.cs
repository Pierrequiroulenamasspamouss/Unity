namespace Kampai.Main
{
	public class DownloadManifestCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.IConfigurationsService configService { get; set; }

		[Inject]
		public ILocalPersistanceService localPersistance { get; set; }

		[Inject]
		public global::Kampai.Download.IDownloadService downloadService { get; set; }

		[Inject]
		public global::Kampai.Game.IDLCService dlcService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Util.IInvokerService invoker { get; set; }

		[Inject]
		public global::Kampai.Common.PostDownloadManifestSignal postSignal { get; set; }

		public override void Execute()
		{
			global::Kampai.Util.TimeProfiler.StartSection("retrieve manifest");
			logger.EventStart("DownloadManifestCommand.Execute");

			try
			{
				string manifestPath = global::Kampai.Util.GameConstants.RESOURCE_MANIFEST_PATH;
				string dir = global::System.IO.Path.GetDirectoryName(manifestPath);
				if (!string.IsNullOrEmpty(dir) && !global::System.IO.Directory.Exists(dir))
				{
					global::System.IO.Directory.CreateDirectory(dir);
				}
				string quality = dlcService.GetDownloadQualityLevel();
				if (string.IsNullOrEmpty(quality))
				{
					quality = global::Kampai.Util.TargetPerformance.LOW.ToString().ToLower();
				}
				string manifestUrl = "";
				try
				{
					manifestUrl = configService.GetConfigurations().dlcManifests[quality];
				}
				catch (global::System.Exception) { }

				string manifestId = "prod_v1";
				if (!string.IsNullOrEmpty(manifestUrl))
				{
					int lastSlash = manifestUrl.LastIndexOf('/');
					if (lastSlash >= 0 && lastSlash < manifestUrl.Length - 1)
					{
						manifestId = manifestUrl.Substring(lastSlash + 1);
					}
				}

				string minimalManifest = "{\"id\":\"" + manifestId
					+ "\",\"baseURL\":\"http://localhost:44733/assets/\""
					+ ",\"assets\":{},\"bundles\":[],\"bundledAssets\":[]}";
				global::System.IO.File.WriteAllText(manifestPath, minimalManifest);
				logger.Info("Wrote manifest to " + manifestPath);
			}
			catch (global::System.Exception ex)
			{
				logger.Warning("MOCK: Failed to write manifest file: " + ex.Message);
			}

			
			//logger.EventStop("DownloadManifestCommand.Execute");
		}
	}
}