namespace Kampai.Main
{
	public class AssetBundlesService : global::Kampai.Main.IAssetBundlesService
	{
		private global::System.Collections.Generic.Dictionary<string, global::UnityEngine.AssetBundle> loadedSharedBundles = new global::System.Collections.Generic.Dictionary<string, global::UnityEngine.AssetBundle>();

		private global::System.Collections.Generic.Dictionary<string, global::UnityEngine.AssetBundle> loadedDLCBundles = new global::System.Collections.Generic.Dictionary<string, global::UnityEngine.AssetBundle>();

		[Inject]
		public global::Kampai.Common.IManifestService manifestService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		private string GetBundlePath(string bundleName)
		{
			string bundleLocation = manifestService.GetBundleLocation(bundleName);
			if (bundleLocation.Length == 0)
			{
				logger.Error("Unable to find bundle: {0}", bundleName);
			}
			return global::System.IO.Path.Combine(bundleLocation, bundleName + ".unity3d");
		}

		public bool IsSharedBundle(string bundleName)
		{
			return loadedSharedBundles.ContainsKey(bundleName);
		}

		public void LoadSharedBundle(string bundleName)
		{
			logger.Info("Loading shared bundle: {0}", bundleName);
			string bundlePath = GetBundlePath(bundleName);
			global::UnityEngine.AssetBundle assetBundle = global::UnityEngine.AssetBundle.CreateFromFile(bundlePath);
			if (null == assetBundle)
			{
				logger.Error("Shared bundle {0} was not found.", bundlePath);
			}
			else
			{
				loadedSharedBundles.Add(bundleName, assetBundle);
			}
		}

		public global::UnityEngine.AssetBundle GetSharedBundle(string bundleName)
		{
			return loadedSharedBundles[bundleName];
		}

		public global::UnityEngine.AssetBundle GetDLCBundle(string bundleName)
		{
			if (loadedDLCBundles.ContainsKey(bundleName))
			{
				return loadedDLCBundles[bundleName];
			}
			logger.Info("Loading bundle: {0}", bundleName);
			string bundlePath = GetBundlePath(bundleName);
			global::UnityEngine.AssetBundle assetBundle = global::UnityEngine.AssetBundle.CreateFromFile(bundlePath);
			if (null == assetBundle)
			{
				logger.Error("Content bundle {0} was not found.", bundlePath);
				return null;
			}
			loadedDLCBundles.Add(bundleName, assetBundle);
			return assetBundle;
		}

		public void UnloadSharedBundles()
		{
			global::Kampai.Util.TimeProfiler.StartSection("unloading shared bundles");
			foreach (global::UnityEngine.AssetBundle value in loadedSharedBundles.Values)
			{
				value.Unload(false);
			}
			loadedSharedBundles.Clear();
			global::Kampai.Util.TimeProfiler.EndSection("unloading shared bundles");
		}

		public void UnloadDLCBundles()
		{
			global::Kampai.Util.TimeProfiler.StartSection("unload dlc bundles");
			foreach (global::UnityEngine.AssetBundle value in loadedDLCBundles.Values)
			{
				value.Unload(false);
			}
			loadedDLCBundles.Clear();
			global::Kampai.Util.TimeProfiler.EndSection("unload dlc bundles");
		}
	}
}
