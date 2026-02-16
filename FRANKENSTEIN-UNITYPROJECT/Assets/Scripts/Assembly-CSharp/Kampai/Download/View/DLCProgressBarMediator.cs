namespace Kampai.Download.View
{
	public class DLCProgressBarMediator : global::strange.extensions.mediation.impl.Mediator
	{
		[Inject]
		public global::Kampai.Download.View.DLCProgressBarView view { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localizationService { get; set; }

		[Inject]
		public global::Kampai.Download.DownloadInitializeSignal initializeSignal { get; set; }

		[Inject]
		public global::Kampai.Download.DownloadProgressSignal progressSignal { get; set; }

		[Inject]
		public global::Kampai.Download.DLCLoadScreenModel model { get; set; }

		[Inject]
		public global::Kampai.Common.IManifestService manifestService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Download.DLCModel dlcModel { get; set; }

		public override void OnRegister()
		{
			initializeSignal.AddListener(InitializeDLC);
			progressSignal.AddListener(UpdateProgress);
		}

		public override void OnRemove()
		{
			initializeSignal.RemoveListener(InitializeDLC);
			progressSignal.RemoveListener(UpdateProgress);
		}

		private void UpdateProgress(global::Ea.Sharkbite.HttpPlugin.Http.Api.DownloadProgress progress, global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request)
		{
			if (request != null && !request.IsAborted())
			{
				model.CurrentProgress += (float)progress.Delta / 1024f / 1024f;
				UpdateProgress((int)model.CurrentProgress);
				view.UpdateProgress(model);
			}
		}

		private void InitializeDLC(float size)
		{
			model.TotalSize = size / 1024f / 1024f;
			view.titleText.text = localizationService.GetString("DLCProgressTitle");
			UpdateProgress(0);
		}

		private void UpdateProgress(int progress)
		{
			view.updateText.text = localizationService.GetString("DLCIndicatorProgress", progress, (int)model.TotalSize);
		}
	}
}
