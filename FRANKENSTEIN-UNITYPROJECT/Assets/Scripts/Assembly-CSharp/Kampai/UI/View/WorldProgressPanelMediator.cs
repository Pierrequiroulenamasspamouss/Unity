namespace Kampai.UI.View
{
	public class WorldProgressPanelMediator : global::strange.extensions.mediation.impl.Mediator
	{
		[Inject]
		public global::Kampai.UI.View.WorldProgressPanelView view { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.UI.View.DisplayWorldProgressSignal displayWorldProgressSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RemoveWorldProgressSignal removeWorldProgressSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowMoveBuildingMenuSignal showMoveBuildingMenuSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideAllWayFindersSignal hideAllWayFindersSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowAllWayFindersSignal showAllWayFindersSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CloseAllOtherMenuSignal closeAllMenuSignal { get; set; }

		public override void OnRegister()
		{
			view.Init(logger);
			displayWorldProgressSignal.AddListener(DisplayWorldProgress);
			removeWorldProgressSignal.AddListener(RemoveWorldProgress);
			showMoveBuildingMenuSignal.AddListener(ShowMoveBuildingMenu);
		}

		public override void OnRemove()
		{
			view.Cleanup();
			displayWorldProgressSignal.RemoveListener(DisplayWorldProgress);
			removeWorldProgressSignal.RemoveListener(RemoveWorldProgress);
			showMoveBuildingMenuSignal.RemoveListener(ShowMoveBuildingMenu);
		}

		private void ShowMoveBuildingMenu(bool show, int trackedID, global::Kampai.UI.View.MoveBuildingMenuMediator.Buttons mask)
		{
			if (show)
			{
				global::UnityEngine.GameObject type = view.CreateOrUpdateMoveBuildingMenu(new global::Kampai.UI.View.MoveBuildingSetting(trackedID, (int)mask));
				closeAllMenuSignal.Dispatch(type);
				hideAllWayFindersSignal.Dispatch();
			}
			else
			{
				showAllWayFindersSignal.Dispatch();
				view.RemoveMoveBuildingMenu();
			}
		}

		private void DisplayWorldProgress(global::Kampai.UI.View.ProgressBarSettings settings)
		{
			view.CreateProgressBar(settings);
		}

		private void RemoveWorldProgress(int trackedId)
		{
			view.RemoveProgressBar(trackedId);
		}
	}
}
