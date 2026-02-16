namespace Kampai.Main
{
	public class LoadSharedBundlesCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Common.IManifestService manifestService { get; set; }

		[Inject]
		public global::Kampai.Main.IAssetBundlesService assetBundlesService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			logger.Debug("Start Loading Shared Bundles");
			global::Kampai.Util.TimeProfiler.StartSection("loading shared bundles");
			foreach (string shaderBundle in manifestService.GetShaderBundles())
			{
				assetBundlesService.LoadSharedBundle(shaderBundle);
			}
			foreach (string sharedBundle in manifestService.GetSharedBundles())
			{
				assetBundlesService.LoadSharedBundle(sharedBundle);
			}
			global::Kampai.Util.TimeProfiler.EndSection("loading shared bundles");
		}
	}
}
