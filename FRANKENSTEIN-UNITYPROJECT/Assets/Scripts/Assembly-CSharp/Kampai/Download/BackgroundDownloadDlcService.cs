namespace Kampai.Download
{
	public class BackgroundDownloadDlcService : global::Kampai.Download.IBackgroundDownloadDlcService
	{
		private sealed class Invoker : global::Kampai.Util.IInvokerService
		{
			private global::System.Collections.Generic.Queue<global::System.Action> work = new global::System.Collections.Generic.Queue<global::System.Action>();

			private global::System.Threading.Mutex mutex = new global::System.Threading.Mutex(false);

			public void Add(global::System.Action a)
			{
				try
				{
					mutex.WaitOne();
					work.Enqueue(a);
				}
				finally
				{
					mutex.ReleaseMutex();
				}
			}

			public void Update()
			{
				if (work.Count <= 0)
				{
					return;
				}
				try
				{
					mutex.WaitOne();
					while (work.Count > 0)
					{
						global::System.Action action = work.Dequeue();
						action();
					}
				}
				finally
				{
					mutex.ReleaseMutex();
				}
			}
		}

		private const int MAX_CONCURRENT_REQUESTS = 5;

		private global::System.Collections.Generic.Queue<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> pendingRequests;

		private global::System.Collections.Generic.List<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> runningRequests = new global::System.Collections.Generic.List<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest>();

		private bool stopped = true;

		private bool isRunning;

		private global::Kampai.Download.BackgroundDownloadDlcService.Invoker invoker = new global::Kampai.Download.BackgroundDownloadDlcService.Invoker();

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Download.DLCModel dlcModel { get; set; }

		[Inject]
		public global::Kampai.Common.IManifestService manifestService { get; set; }

		[Inject]
		public global::Kampai.Game.IConfigurationsService configurationsService { get; set; }

		public bool Stopped
		{
			get
			{
				return stopped;
			}
		}

		public void Stop()
		{
			isRunning = false;
		}

		public void Init()
		{
			pendingRequests = CreateNetworkRequests(dlcModel.NeededBundles, manifestService.GetDLCURL());
			isRunning = pendingRequests.Count != 0;
			stopped = !isRunning;
			logger.Debug("BackgroundDownloadDlcService.Init(): pendingRequests.Count = {0}", pendingRequests.Count);
		}

		public void Run()
		{
			while (isRunning)
			{
				if (!ProcessQueue())
				{
					global::System.Threading.Thread.Sleep(100);
				}
				invoker.Update();
				if (pendingRequests.Count == 0 && runningRequests.Count == 0)
				{
					logger.Info("BackgroundDownloadDlcService.Run(): download finished, exiting");
					break;
				}
			}
			foreach (global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest runningRequest in runningRequests)
			{
				runningRequest.Abort();
			}
			while (runningRequests.Count > 0)
			{
				invoker.Update();
				global::System.Threading.Thread.Sleep(100);
			}
			stopped = true;
		}

		private bool ProcessQueue()
		{
			if (isRunning && pendingRequests.Count > 0 && runningRequests.Count < 5 && global::UnityEngine.Application.internetReachability == global::UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork)
			{
				global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = pendingRequests.Dequeue();
				runningRequests.Add(request);
				logger.Info("[BDLC] request: " + request.Uri);
				request.Execute(HandleResponseProxy);
				return true;
			}
			return false;
		}

		private void HandleResponseProxy(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
		{
			invoker.Add(delegate
			{
				HandleResponse(response);
			});
		}

		private void HandleResponse(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
		{
			global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = response.Request;
			runningRequests.Remove(request);
			string uri = request.Uri;
			if (request.IsAborted())
			{
				logger.Info("BackgroundDownloadDlcService.HandleResponse(): aborted, url = {0}", uri);
			}
			else if (!response.Success)
			{
				logger.Info("BackgroundDownloadDlcService.HandleResponse(): failure: code = {0}, url = {1}, enqueue request", response.Code, uri);
				pendingRequests.Enqueue(request);
			}
			else
			{
				logger.Info("BackgroundDownloadDlcService.HandleResponse(): success: url = {0}", uri);
			}
		}

		private global::System.Collections.Generic.Queue<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> CreateNetworkRequests(global::System.Collections.Generic.IList<global::Kampai.Util.BundleInfo> bundles, string baseDlcUrl)
		{
			global::System.Collections.Generic.Queue<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> queue = new global::System.Collections.Generic.Queue<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest>(bundles.Count);
			foreach (global::Kampai.Util.BundleInfo bundle in bundles)
			{
				global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest item = CreateRequest(bundle, baseDlcUrl);
				queue.Enqueue(item);
			}
			return queue;
		}

		private global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest CreateRequest(global::Kampai.Util.BundleInfo bundleInfo, string baseDlcUrl)
		{
			string name = bundleInfo.name;
			string uri = global::Kampai.Download.DownloadUtil.CreateBundleURL(baseDlcUrl, name);
			string filePath = global::Kampai.Download.DownloadUtil.CreateBundlePath(global::Kampai.Util.GameConstants.DLC_PATH, name);
			return new global::Ea.Sharkbite.HttpPlugin.Http.Impl.FileDownloadRequest(uri).WithOutputFile(filePath).WithMD5(bundleInfo.sum).WithGzip(true);
		}
	}
}
