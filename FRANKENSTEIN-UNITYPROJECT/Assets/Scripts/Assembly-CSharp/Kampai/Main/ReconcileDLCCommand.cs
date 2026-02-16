namespace Kampai.Main
{
	public class ReconcileDLCCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public bool purge { get; set; }

		[Inject]
		public global::Kampai.Common.IManifestService manifestService { get; set; }

		[Inject]
		public global::Kampai.Download.DLCModel model { get; set; }

		[Inject]
		public global::Kampai.Game.IDLCService dlcService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			global::Kampai.Util.TimeProfiler.StartSection("reconcile dlc");
			global::System.Collections.Generic.HashSet<global::Kampai.Util.BundleInfo> bundles = manifestService.GetBundles();
			global::System.Collections.Generic.List<global::Kampai.Util.BundleInfo> list = new global::System.Collections.Generic.List<global::Kampai.Util.BundleInfo>();
			ulong num = 0uL;
			int playerDLCTier = dlcService.GetPlayerDLCTier();
			int num2 = playerDLCTier;
			foreach (global::Kampai.Util.BundleInfo item in bundles)
			{
				string name = item.name;
				if (IsValidBundle(item))
				{
					continue;
				}
				if (playerDLCTier >= item.tier)
				{
					list.Add(item);
					num += item.size;
					if (num2 >= item.tier)
					{
						num2 = item.tier - 1;
					}
					if (purge && BundleExists(name))
					{
						global::System.IO.File.Delete(GetBundlePath(name));
					}
				}
				else
				{
					logger.Debug("Unable to download: " + name + "    Tier is too high for this user");
				}
			}
			if (purge && global::System.IO.Directory.Exists(global::Kampai.Util.GameConstants.DLC_PATH))
			{
				string[] files = global::System.IO.Directory.GetFiles(global::Kampai.Util.GameConstants.DLC_PATH);
				foreach (string text in files)
				{
					string fileNameWithoutExtension = global::System.IO.Path.GetFileNameWithoutExtension(text);
					if (!text.Equals(".DS_Store") && !manifestService.ContainsBundle(fileNameWithoutExtension))
					{
						global::System.IO.File.Delete(text);
					}
				}
			}
			model.NeededBundles = list;
			model.HighestTierDownloaded = num2;
			model.TotalSize = num;
			logger.Debug("ReconcileDLCCommand BundlesNeeded: {0}", list.Count);
			global::Kampai.Util.TimeProfiler.EndSection("reconcile dlc");
		}

		private string GetBundlePath(string name)
		{
			return global::System.IO.Path.Combine(global::Kampai.Util.GameConstants.DLC_PATH, name + ".unity3d");
		}

		private bool BundleExists(string name)
		{
			return global::System.IO.File.Exists(GetBundlePath(name));
		}

		private bool IsValidBundle(global::Kampai.Util.BundleInfo info)
		{
			if (!BundleExists(info.name))
			{
				return false;
			}
			string bundlePath = GetBundlePath(info.name);
			ulong length = (ulong)new global::System.IO.FileInfo(bundlePath).Length;
			if (length != info.size)
			{
				logger.Error("SIZE CHECK FAILED: {0} {1}!={2} failed", info.name, length, info.size);
				return false;
			}
			return true;
		}
	}
}
