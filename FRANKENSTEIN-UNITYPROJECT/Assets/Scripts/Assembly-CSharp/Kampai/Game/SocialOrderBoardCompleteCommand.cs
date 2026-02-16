namespace Kampai.Game
{
	public class SocialOrderBoardCompleteCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Common.PickControllerModel pickControllerModel { get; set; }

		[Inject]
		public global::Kampai.Game.StuartStartPerformingSignal startPerformingSignal { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.Game.StuartShowCompleteSignal showCompleteSignal { get; set; }

		[Inject]
		public global::Kampai.Game.GenerateTemporaryMinionsStageSignal generateTemporaryMinionsStageSignal { get; set; }

		[Inject]
		public global::Kampai.Game.StageService stageService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimedSocialEventService timedSocialEventService { get; set; }

		[Inject]
		public global::Kampai.Game.CloseConfirmationSignal closeConfirmationSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowHUDSignal showHUDSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowStoreSignal showStoreSignal { get; set; }

		public override void Execute()
		{
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.BuildingZoomSignal>().Dispatch(new global::Kampai.Game.BuildingZoomSettings(global::Kampai.Game.ZoomType.IN, global::Kampai.Game.BuildingZoomType.STAGE, ZoomCompleted));
			global::strange.extensions.signal.impl.Signal signal = new global::strange.extensions.signal.impl.Signal();
			global::Kampai.Util.SignalCallback<global::strange.extensions.signal.impl.Signal> signalCallback = new global::Kampai.Util.SignalCallback<global::strange.extensions.signal.impl.Signal>(signal);
			signal.AddListener(HandleShowFinished);
			startPerformingSignal.Dispatch(signalCallback);
			if (!signalCallback.WillDispatch)
			{
				HandleShowFinished();
			}
			else
			{
				ToggleUI(false);
			}
			timedSocialEventService.setRewardCutscene(true);
		}

		private void HandleShowFinished()
		{
			closeConfirmationSignal.Dispatch();
			timedSocialEventService.setRewardCutscene(false);
			showCompleteSignal.Dispatch();
		}

		public void ZoomCompleted()
		{
			generateTemporaryMinionsStageSignal.Dispatch();
			stageService.ShowStageBackdrop();
			pickControllerModel.CurrentMode = global::Kampai.Common.PickControllerModel.Mode.StageView;
		}

		private void ToggleUI(bool enable)
		{
			showHUDSignal.Dispatch(enable);
			showStoreSignal.Dispatch(enable);
		}
	}
}
