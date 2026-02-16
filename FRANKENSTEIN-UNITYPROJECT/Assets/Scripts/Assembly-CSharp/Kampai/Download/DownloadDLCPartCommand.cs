namespace Kampai.Download
{
	public class DownloadDLCPartCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Download.IDownloadService downloadService { get; set; }

		[Inject]
		public global::Kampai.Download.DLCModel dlcModel { get; set; }

		[Inject]
		public global::Kampai.Common.IManifestService manifestService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Common.NetworkModel networkModel { get; set; }

		[Inject]
		public global::Kampai.Download.ShowNoWiFiPanelSignal showNoWiFiPanelSignal { get; set; }

		public override void Execute()
		{
			if (global::Kampai.Util.NetworkUtil.IsNetworkWiFi())
			{
				dlcModel.AllowDownloadVia3G = false;
			}
			if (dlcModel.AllowDownloadVia3G || global::Kampai.Util.NetworkUtil.IsNetworkWiFi() || !global::Kampai.Util.NetworkUtil.IsConnected())
			{
				while (dlcModel.PendingRequests.Count > 0 && dlcModel.RunningRequests.Count < 5)
				{
					global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = dlcModel.PendingRequests.Dequeue();
					dlcModel.RunningRequests.Add(request);
					downloadService.Perform(request);
				}
			}
			else if (!networkModel.isConnectionLost)
			{
				showNoWiFiPanelSignal.Dispatch(true);
			}
		}
	}
}
