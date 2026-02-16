namespace Kampai.Game.View
{
	public class BobMediator : global::strange.extensions.mediation.impl.Mediator
	{
		private global::strange.extensions.signal.impl.Signal animationCallback = new global::strange.extensions.signal.impl.Signal();

		[Inject]
		public global::Kampai.Game.View.BobView view { get; set; }

		[Inject]
		public global::Kampai.Game.BobPointsAtSignSignal pointAtSignSignal { get; set; }

		[Inject]
		public global::Kampai.Game.BobIdleInTownHallSignal bobIdleInTownHallSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CharacterArrivedAtDestinationSignal arrivedAtDestinationSignal { get; set; }

		[Inject]
		public global::Kampai.Game.BobCelebrateLandExpansionSignal celebrateLandExpansionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.BobCelebrateLandExpansionCompleteSignal celebrateLandExpansionCompleteSignal { get; set; }

		[Inject]
		public global::Kampai.Game.BobReturnToTownSignal bobReturnToTown { get; set; }

		[Inject]
		public global::Kampai.Game.PointBobLandExpansionSignal pointBobLandExpansionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.BobFrolicsSignal bobFrolicsSignal { get; set; }

		public override void OnRegister()
		{
			view.Init();
			view.SetAnimationCallback(animationCallback);
			pointAtSignSignal.AddListener(PointAtSign);
			bobIdleInTownHallSignal.AddListener(IdleInTownHall);
			arrivedAtDestinationSignal.AddListener(ArrivedAtWayPoint);
			celebrateLandExpansionSignal.AddListener(CelebrateLandExpansion);
			animationCallback.AddListener(AnimationCallback);
			bobReturnToTown.AddListener(ReturnToTown);
			pointBobLandExpansionSignal.Dispatch();
		}

		public override void OnRemove()
		{
			pointAtSignSignal.RemoveListener(PointAtSign);
			bobIdleInTownHallSignal.RemoveListener(IdleInTownHall);
			arrivedAtDestinationSignal.RemoveListener(ArrivedAtWayPoint);
			celebrateLandExpansionSignal.RemoveListener(CelebrateLandExpansion);
			animationCallback.RemoveListener(AnimationCallback);
			bobReturnToTown.RemoveListener(ReturnToTown);
		}

		private void PointAtSign(global::UnityEngine.Vector3 position)
		{
			view.PointAtSign(position);
		}

		private void IdleInTownHall(global::Kampai.Game.LocationIncidentalAnimationDefinition animationDefinition)
		{
			global::Kampai.Game.BobCharacter byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.BobCharacter>(view.ID);
			byInstanceId.CurrentFrolicLocation = animationDefinition.Location;
			global::Kampai.Game.MinionAnimationDefinition mad = definitionService.Get<global::Kampai.Game.MinionAnimationDefinition>(animationDefinition.AnimationId);
			view.IdleInTownHall(animationDefinition, mad);
		}

		private void ArrivedAtWayPoint(int minionID)
		{
			if (view.ID == minionID)
			{
				view.ArrivedAtWayPoint();
			}
		}

		private void CelebrateLandExpansion()
		{
			view.CelebrateLandExpansion(celebrateLandExpansionCompleteSignal);
		}

		private void AnimationCallback()
		{
			bobFrolicsSignal.Dispatch();
		}

		private void ReturnToTown()
		{
			view.ReturnToTown();
		}
	}
}
