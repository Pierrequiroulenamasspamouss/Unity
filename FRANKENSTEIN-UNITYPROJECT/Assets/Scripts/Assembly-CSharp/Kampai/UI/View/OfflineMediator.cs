namespace Kampai.UI.View
{
	public class OfflineMediator : global::strange.extensions.mediation.impl.Mediator
	{
		private global::UnityEngine.UI.Button button;

		[Inject]
		public global::Kampai.UI.View.OfflineView view { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService locService { get; set; }

		[Inject]
		public global::Kampai.Common.NetworkModel networkModel { get; set; }

		[Inject]
		public global::Kampai.Download.IDownloadService downloadService { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowOfflinePopupSignal showOfflinePopupSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		public override void OnRegister()
		{
			view.retryButton.ClickedSignal.AddListener(OnRetry);
			view.title.text = locService.GetString("OfflineTitle");
			view.description.text = locService.GetString("OfflineDescription");
			view.retryButtonText.text = locService.GetString("OfflineRetry");
			view.OnMenuClose.AddListener(OnMenuClose);
			view.Init();
			view.Open();
			button = view.retryButton.GetComponent<global::UnityEngine.UI.Button>();
			if (button != null)
			{
				button.onClick.RemoveListener(view.retryButton.OnClickEvent);
				button.onClick.AddListener(view.retryButton.OnClickEvent);
			}
		}

		public override void OnRemove()
		{
			view.retryButton.ClickedSignal.RemoveListener(OnRetry);
			view.OnMenuClose.RemoveListener(OnMenuClose);
		}

		private void OnRetry()
		{
			button.interactable = false;
			routineRunner.DelayAction(new global::UnityEngine.WaitForSeconds(2f), delegate
			{
				if (view.retryButton != null)
				{
					button.interactable = true;
				}
			});
			networkModel.isConnectionLost = !global::Kampai.Util.NetworkUtil.IsConnected();
			if (!networkModel.isConnectionLost)
			{
				Close();
				downloadService.Retry();
			}
		}

		private void OnMenuClose()
		{
			if (!networkModel.isConnectionLost)
			{
				showOfflinePopupSignal.Dispatch(false);
			}
			else
			{
				view.Open();
			}
		}

		private void Close()
		{
			view.Close();
		}
	}
}
