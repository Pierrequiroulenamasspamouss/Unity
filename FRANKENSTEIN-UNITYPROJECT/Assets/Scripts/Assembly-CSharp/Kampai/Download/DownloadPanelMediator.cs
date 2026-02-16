namespace Kampai.Download
{
	public class DownloadPanelMediator : global::strange.extensions.mediation.impl.Mediator
	{
		[Inject]
		public global::Kampai.Download.DownloadPanelView view { get; set; }

		[Inject]
		public global::Kampai.Download.DLCDownloadFinishedSignal downloadFinishedSignal { get; set; }

		[Inject]
		public global::Kampai.Download.ShowNoWiFiPanelSignal showNoWiFiPanelSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Download.ShowDLCPanelSignal showPanelSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ReloadGameSignal reloadSignal { get; set; }

		public override void OnRegister()
		{
			global::UnityEngine.Screen.sleepTimeout = -1;
			downloadFinishedSignal.AddListener(HandleDownloadFinished);
			showNoWiFiPanelSignal.AddListener(view.ShowNoWiFi);
		}

		public override void OnRemove()
		{
			downloadFinishedSignal.RemoveListener(HandleDownloadFinished);
			showNoWiFiPanelSignal.RemoveListener(view.ShowNoWiFi);
			global::UnityEngine.Screen.sleepTimeout = -2;
		}

		private void HandleDownloadFinished()
		{
			showPanelSignal.Dispatch(false);
			reloadSignal.Dispatch();
		}
	}
}
