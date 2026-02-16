namespace Kampai.Download
{
	public class LaunchDownloadCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public bool shouldLoadAudio { get; set; }

		[Inject]
		public global::Kampai.Common.ReconcileDLCSignal reconcileSignal { get; set; }

		[Inject]
		public global::Kampai.Download.DLCModel dlcModel { get; set; }

		[Inject]
		public global::Kampai.Common.IManifestService manifestService { get; set; }

		[Inject]
		public global::Kampai.Download.DownloadResponseSignal downloadResponseSignal { get; set; }

		[Inject]
		public global::Kampai.Download.DownloadInitializeSignal initSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Download.DownloadProgressSignal progressSignal { get; set; }

		[Inject]
		public global::Kampai.Download.DownloadDLCPartSignal downloadDLCPartSignal { get; set; }

		[Inject]
		public global::Kampai.Download.DLCDownloadFinishedSignal downloadFinishedSignal { get; set; }

		public override void Execute()
		{
			if ((dlcModel.PendingRequests != null && dlcModel.PendingRequests.Count > 0) || (dlcModel.RunningRequests != null && dlcModel.RunningRequests.Count > 0))
			{
				dlcModel.ShouldLaunchDownloadAgain = true;
				dlcModel.NextDownloadShouldLoadAudio = shouldLoadAudio;
				return;
			}
			dlcModel.ShouldLoadAudio = shouldLoadAudio;
			reconcileSignal.Dispatch(false);
			routineRunner.StartCoroutine(WaitAFrame());
			dlcModel.PendingRequests = CreateNetworkRequests();
			dlcModel.RunningRequests = new global::System.Collections.Generic.List<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest>();
			dlcModel.LastNetworkFailureTime = -1f;
		}

		private global::System.Collections.IEnumerator WaitAFrame()
		{
			yield return new global::UnityEngine.WaitForEndOfFrame();
			downloadDLCPartSignal.Dispatch();
			initSignal.Dispatch(dlcModel.TotalSize);
			if (dlcModel.NeededBundles != null && dlcModel.NeededBundles.Count == 0)
			{
				downloadFinishedSignal.Dispatch();
			}
		}

		private global::System.Collections.Generic.Queue<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> CreateNetworkRequests()
		{
			global::System.Collections.Generic.Queue<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> queue = new global::System.Collections.Generic.Queue<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest>(dlcModel.NeededBundles.Count);
			foreach (global::Kampai.Util.BundleInfo neededBundle in dlcModel.NeededBundles)
			{
				global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest item = CreateRequest(neededBundle);
				queue.Enqueue(item);
			}
			return queue;
		}

		private global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest CreateRequest(global::Kampai.Util.BundleInfo bundleInfo)
		{
			string name = bundleInfo.name;
			string uri = global::Kampai.Download.DownloadUtil.CreateBundleURL(manifestService.GetDLCURL(), name);
			string filePath = global::Kampai.Download.DownloadUtil.CreateBundlePath(global::Kampai.Util.GameConstants.DLC_PATH, name);
			return new global::Ea.Sharkbite.HttpPlugin.Http.Impl.FileDownloadRequest(uri).WithOutputFile(filePath).WithMD5(bundleInfo.sum).WithGzip(true)
				.WithRetry(true, 1)
				.WithResponseSignal(downloadResponseSignal)
				.WithNotifyProgress(progressSignal);
		}
	}
}
