namespace Kampai.Download
{
	public class DownloadService : global::Kampai.Download.IDownloadService
	{
		public const int MAX_CONCURRENT_REQUESTS = 5;

		private global::System.Collections.Generic.Queue<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> requestQueue;

		private global::System.Collections.Generic.LinkedList<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> runningRequests;

		private bool logInfo;

		private global::System.Collections.Generic.LinkedList<global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>> globalResponseSignals;

		private string deviceTypeUrlEscaped;

		[Inject]
		public global::Kampai.Util.IInvokerService invoker { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Common.NetworkModel networkModel { get; set; }

		[Inject]
		public global::Kampai.Common.NetworkConnectionLostSignal networkConnectionLostSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ClientVersion clientVersion { get; set; }

		public bool IsRunning { get; set; }

		public DownloadService()
		{
			global::System.Net.ServicePointManager.DefaultConnectionLimit = 5;
			IsRunning = true;
			requestQueue = new global::System.Collections.Generic.Queue<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest>();
			runningRequests = new global::System.Collections.Generic.LinkedList<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest>();
			globalResponseSignals = new global::System.Collections.Generic.LinkedList<global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>>();
			global::Ea.Sharkbite.HttpPlugin.Http.Api.ConnectionSettings.ConnectionLimit = 5;
		}

		[PostConstruct]
		public void PostConstruct()
		{
			deviceTypeUrlEscaped = global::UnityEngine.WWW.EscapeURL(clientVersion.GetClientDeviceType());
		}

		public void Perform(global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request, bool forceRequest = false)
		{
			if (forceRequest)
			{
				DoPerform(request);
				return;
			}
			invoker.Add(delegate
			{
				DoPerform(request);
			});
		}

		private void DoPerform(global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request)
		{
			logInfo = logger.IsAllowedLevel(global::Kampai.Util.Logger.Level.Info);
			if (request.NotifyProgress != null)
			{
				global::Ea.Sharkbite.HttpPlugin.Http.Api.INotifiable notifiable = request as global::Ea.Sharkbite.HttpPlugin.Http.Api.INotifiable;
				if (notifiable != null)
				{
					notifiable.RegisterNotifiable(NotifyProgress);
				}
				else
				{
					logger.Warning("Unable to notify if request is not notifiable");
				}
			}
			request = request.WithHeaderParam("K-Platform", clientVersion.GetClientPlatform()).WithHeaderParam("K-Device", deviceTypeUrlEscaped).WithHeaderParam("K-Version", clientVersion.GetClientVersion());
			requestQueue.Enqueue(request);
			if (!networkModel.isConnectionLost)
			{
				if (global::Kampai.Util.NetworkUtil.IsConnected())
				{
					ProcessQueue();
				}
				else
				{
					networkConnectionLostSignal.Dispatch();
				}
			}
		}

		public void AddGlobalResponseListener(global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal)
		{
			globalResponseSignals.AddLast(signal);
		}

		private void ProcessQueue()
		{
			if (IsRunning && requestQueue.Count > 0 && runningRequests.Count < 5)
			{
				global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = null;
				request = requestQueue.Dequeue();
				if (request != null)
				{
					DoDownload(request);
				}
			}
		}

		public void Retry()
		{
			if (networkModel.isConnectionLost)
			{
				return;
			}
			if (global::Kampai.Util.NetworkUtil.IsConnected())
			{
				if (IsRunning)
				{
					while (requestQueue.Count > 0 && runningRequests.Count < 5)
					{
						ProcessQueue();
					}
				}
			}
			else
			{
				networkConnectionLostSignal.Dispatch();
			}
		}

		private void RetryDownload(global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request)
		{
			if (IsRunning)
			{
				logger.Warning("failed to download {0}, trying again...", request.Uri);
				request.RetryCount--;
				if (IsRunning && runningRequests.Count < 5)
				{
					DoDownload(request);
					return;
				}
				requestQueue.Enqueue(request);
				ProcessQueue();
			}
		}

		private void DoDownload(global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request)
		{
			runningRequests.AddLast(request);
			if (logInfo)
			{
				logger.Info("HTTP START [{0}] {1}: {2}", runningRequests.Count, request.Method, request.Uri);
			}
			request.Execute(RequestCallbackProxy);
		}

		private void RequestCallbackProxy(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
		{
			invoker.Add(delegate
			{
				RequestCallback(response);
			});
		}

		private void RequestCallback(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
		{
			if (!IsRunning)
			{
				global::Kampai.Util.Native.LogError("Ignoring http response (shutting down)");
				return;
			}
			string text = "unknown";
			if (response != null)
			{
				if (response.IsConnectionLost && !networkModel.isConnectionLost)
				{
					networkModel.isConnectionLost = true;
					networkConnectionLostSignal.Dispatch();
				}
				global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = response.Request;
				if (request != null)
				{
					runningRequests.Remove(request);
					text = request.Uri;
					if (!response.Success)
					{
						int code = response.Code;
						logger.Warning("Error downloading {0} HTTP RESPONSE => {1}", text, code);
						if (!networkModel.isConnectionLost)
						{
							networkModel.isConnectionLost = true;
							networkConnectionLostSignal.Dispatch();
						}
						if (networkModel.isConnectionLost)
						{
							requestQueue.Enqueue(request);
							return;
						}
						if (request.CanRetry && request.RetryCount > 0 && !request.IsAborted())
						{
							RetryDownload(request);
							return;
						}
					}
					else if (logInfo)
					{
						global::Kampai.Util.Native.LogInfo("Successfully downloaded " + text);
					}
				}
				else
				{
					global::Kampai.Util.Native.LogError("Null request on response");
				}
			}
			else
			{
				global::Kampai.Util.Native.LogError("Null response");
				response = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultResponse().WithCode(500).WithBody("Null response");
			}
			if (response.Success || !networkModel.isConnectionLost)
			{
				NotifyResponse(response);
			}
			if (logInfo)
			{
				global::Kampai.Util.Native.LogInfo("HTTP END [" + runningRequests.Count + "] " + text);
			}
			if (!networkModel.isConnectionLost)
			{
				ProcessQueue();
			}
		}

		private void NotifyProgress(global::Ea.Sharkbite.HttpPlugin.Http.Api.DownloadProgress progress, global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request)
		{
			global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.DownloadProgress, global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> notifySignal = progress.NotifySignal;
			if (notifySignal != null)
			{
				invoker.Add(delegate
				{
					notifySignal.Dispatch(progress, request);
				});
			}
		}

		private void NotifyResponse(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
		{
			if (!IsRunning)
			{
				return;
			}
			global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> responseSignal = response.Request.ResponseSignal;
			if (responseSignal != null)
			{
				responseSignal.Dispatch(response);
			}
			foreach (global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> globalResponseSignal in globalResponseSignals)
			{
				globalResponseSignal.Dispatch(response);
			}
		}

		public void Shutdown()
		{
			IsRunning = false;
			ClearQueueAndAbortRunning();
			runningRequests.Clear();
		}

		public void Abort()
		{
			foreach (global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest item in requestQueue)
			{
				item.Abort();
				NotifyResponse(new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultResponse().WithCode(500).WithRequest(item));
			}
			ClearQueueAndAbortRunning();
		}

		private void ClearQueueAndAbortRunning()
		{
			requestQueue.Clear();
			foreach (global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest runningRequest in runningRequests)
			{
				runningRequest.Abort();
			}
		}
	}
}
