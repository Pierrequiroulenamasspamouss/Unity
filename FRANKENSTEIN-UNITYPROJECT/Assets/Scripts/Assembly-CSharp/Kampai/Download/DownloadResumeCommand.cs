namespace Kampai.Download
{
	public class DownloadResumeCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Download.DLCLoadScreenModel model { get; set; }

		[Inject]
		public global::Kampai.Download.IDownloadService downloadService { get; set; }

		[Inject]
		public global::Kampai.Download.DLCModel dlcModel { get; set; }

		[Inject]
		public global::Kampai.Download.LaunchDownloadSignal launchSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			global::System.Collections.Generic.Queue<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> pendingRequests = dlcModel.PendingRequests;
			global::System.Collections.Generic.List<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> runningRequests = dlcModel.RunningRequests;
			if ((pendingRequests == null || pendingRequests.Count == 0) && (runningRequests == null || runningRequests.Count == 0))
			{
				logger.Debug("DownloadResumeCommand.Execute(): do not restart DLC downloading, it was not started yet.");
				return;
			}
			logger.Debug("DownloadResumeCommand.Execute: abort download service, restart downloading.");
			downloadService.Abort();
			if (pendingRequests != null)
			{
				pendingRequests.Clear();
			}
			if (runningRequests != null)
			{
				runningRequests.Clear();
			}
			model.CurrentProgress = 0f;
			launchSignal.Dispatch(dlcModel.ShouldLoadAudio);
		}
	}
}
