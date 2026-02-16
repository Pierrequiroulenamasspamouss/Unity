namespace Kampai.Download
{
	public class DLCModel
	{
		public global::System.Collections.Generic.IList<global::Kampai.Util.BundleInfo> NeededBundles { get; set; }

		public ulong TotalSize { get; set; }

		public global::System.Collections.Generic.Queue<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> PendingRequests { get; set; }

		public global::System.Collections.Generic.List<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> RunningRequests { get; set; }

		public float LastNetworkFailureTime { get; set; }

		public bool AllowDownloadVia3G { get; set; }

		public int HighestTierDownloaded { get; set; }

		public bool ShouldLaunchDownloadAgain { get; set; }

		public bool ShouldLoadAudio { get; set; }

		public bool NextDownloadShouldLoadAudio { get; set; }

		public global::System.Collections.Generic.Queue<string> DownloadedAudioBundles { get; set; }

		public DLCModel()
		{
			DownloadedAudioBundles = new global::System.Collections.Generic.Queue<string>(10);
		}
	}
}
