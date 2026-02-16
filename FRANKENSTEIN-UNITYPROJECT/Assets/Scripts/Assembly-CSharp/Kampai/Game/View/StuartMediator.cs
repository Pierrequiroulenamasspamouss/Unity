namespace Kampai.Game.View
{
	public class StuartMediator : global::strange.extensions.mediation.impl.Mediator
	{
		[Inject]
		public global::Kampai.Game.View.StuartView view { get; set; }

		[Inject]
		public global::Kampai.Game.StuartAddToStageSignal addToStageSignal { get; set; }

		[Inject]
		public global::Kampai.Game.StuartStartPerformingSignal startPerformingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.StuartGetOnStageSignal getOnStageSignal { get; set; }

		[Inject]
		public global::Kampai.Game.StuartGetOnStageImmediateSignal getOnStageImmediateSignal { get; set; }

		[Inject]
		public global::Kampai.Game.StuartCelebrateSignal celebrateSignal { get; set; }

		[Inject]
		public global::Kampai.Game.StuartGetAttentionSignal getAttentionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.AnimateStuartSignal animateStuartSignal { get; set; }

		[Inject]
		public global::Kampai.Game.StuartShowCompleteSignal stuartShowCompleteSignal { get; set; }

		[Inject]
		public global::Kampai.Game.GenerateTemporaryMinionsStageSignal generateTemporaryMinionsStageSignal { get; set; }

		public override void OnRegister()
		{
			addToStageSignal.AddListener(AddToStage);
			startPerformingSignal.AddListener(StartPerforming);
			getOnStageSignal.AddListener(GetOnStage);
			getOnStageImmediateSignal.AddListener(GetOnStageImmediate);
			celebrateSignal.AddListener(Celebrate);
			getAttentionSignal.AddListener(GetAttention);
			animateStuartSignal.AddListener(AnimateStuart);
		}

		public override void OnRemove()
		{
			addToStageSignal.RemoveListener(AddToStage);
			startPerformingSignal.RemoveListener(StartPerforming);
			getOnStageSignal.RemoveListener(GetOnStage);
			getOnStageImmediateSignal.RemoveListener(GetOnStageImmediate);
			celebrateSignal.RemoveListener(Celebrate);
			getAttentionSignal.RemoveListener(GetAttention);
			animateStuartSignal.RemoveListener(AnimateStuart);
		}

		private void AddToStage(global::UnityEngine.Vector3 position, global::UnityEngine.Quaternion rotation, global::Kampai.Game.StuartStageAnimationType animType)
		{
			global::System.Action startParty = null;
			if (animType == global::Kampai.Game.StuartStageAnimationType.FIRSTUNLOCK)
			{
				startParty = delegate
				{
					generateTemporaryMinionsStageSignal.Dispatch();
					StartPerforming(new global::Kampai.Util.SignalCallback<global::strange.extensions.signal.impl.Signal>(stuartShowCompleteSignal));
				};
			}
			view.AddToStage(position, rotation, animType, startParty);
		}

		private void StartPerforming(global::Kampai.Util.SignalCallback<global::strange.extensions.signal.impl.Signal> finishedCallback)
		{
			view.GetOnStage(true);
			view.Perform(finishedCallback);
		}

		private void GetOnStage(bool enable)
		{
			view.GetOnStage(enable);
		}

		private void GetOnStageImmediate(bool enable)
		{
			view.GetOnStageImmediate(enable);
		}

		private void Celebrate(bool enable)
		{
			view.Celebrate(enable);
		}

		private void GetAttention(bool enable)
		{
			view.GetAttention(enable);
		}

		private void AnimateStuart(string animation)
		{
			switch (animation)
			{
			case "walk":
				view.Walk(true);
				break;
			case "idle":
				view.Walk(false);
				break;
			}
		}
	}
}
