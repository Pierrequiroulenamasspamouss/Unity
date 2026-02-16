namespace Kampai.UI.View
{
	public class DLCIndicatorMediator : global::strange.extensions.mediation.impl.Mediator
	{
		private float DLCSize;

		private bool open;

		[Inject]
		public global::Kampai.UI.View.DLCIndicatorView view { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localiztion { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Download.DownloadInitializeSignal sizeSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void OnRegister()
		{
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.DLCProgressSignal>().AddListener(UpdateProgress);
			view.button.ClickedSignal.AddListener(ToggleView);
			sizeSignal.AddListener(SetSize);
			view.Init();
		}

		public override void OnRemove()
		{
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.DLCProgressSignal>().RemoveListener(UpdateProgress);
			view.button.ClickedSignal.RemoveListener(ToggleView);
			sizeSignal.RemoveListener(SetSize);
		}

		private void UpdateProgress(int currentProgress)
		{
			view.downloadSize.text = localiztion.GetString("DLCIndicatorProgress", currentProgress, DLCSize);
			float x = (float)currentProgress / DLCSize;
			view.progressBar.rectTransform.anchorMax = new global::UnityEngine.Vector2(x, 1f);
			if ((float)currentProgress >= DLCSize)
			{
				guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "DLC_DownloadProgressDialog");
			}
		}

		private void SetSize(float size)
		{
			DLCSize = size;
		}

		private void ToggleView()
		{
			if (open)
			{
				view.Close();
				open = false;
			}
			else
			{
				view.Open();
				open = true;
			}
		}
	}
}
