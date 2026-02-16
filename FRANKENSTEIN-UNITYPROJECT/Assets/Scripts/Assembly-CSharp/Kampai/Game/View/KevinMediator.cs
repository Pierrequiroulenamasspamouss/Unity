namespace Kampai.Game.View
{
	public class KevinMediator : global::strange.extensions.mediation.impl.Mediator
	{
		private global::strange.extensions.signal.impl.Signal animationCallback = new global::strange.extensions.signal.impl.Signal();

		[Inject]
		public global::Kampai.Game.View.KevinView view { get; set; }

		[Inject]
		public global::Kampai.Game.KevinGreetVillainSignal greetVillainSignal { get; set; }

		[Inject]
		public global::Kampai.Game.KevinWaveFarewellSignal waveFarewellSignal { get; set; }

		[Inject]
		public global::Kampai.Game.AnimateKevinSignal animateKevinSignal { get; set; }

		[Inject]
		public global::strange.extensions.injector.api.IInjectionBinder injectionBinder { get; set; }

		[Inject]
		public global::Kampai.Game.KevinGoToWelcomeHutSignal gotoWelcomeHutSignal { get; set; }

		[Inject]
		public global::Kampai.Game.KevinWanderSignal kevinWanderSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CharacterArrivedAtDestinationSignal arrivedAtDestinationSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.KevinFrolicsSignal kevinFrolicsSignal { get; set; }

		public override void OnRegister()
		{
			view.Init();
			view.SetAnimationCallback(animationCallback);
			greetVillainSignal.AddListener(HandleGreeting);
			waveFarewellSignal.AddListener(WaveFarewell);
			animateKevinSignal.AddListener(AnimateKevin);
			gotoWelcomeHutSignal.AddListener(GotoWelcomeHut);
			kevinWanderSignal.AddListener(Wander);
			arrivedAtDestinationSignal.AddListener(ArrivedAtWayPoint);
			animationCallback.AddListener(AnimationCallback);
		}

		public override void OnRemove()
		{
			greetVillainSignal.RemoveListener(HandleGreeting);
			waveFarewellSignal.RemoveListener(WaveFarewell);
			animateKevinSignal.RemoveListener(AnimateKevin);
			gotoWelcomeHutSignal.RemoveListener(GotoWelcomeHut);
			kevinWanderSignal.RemoveListener(Wander);
			arrivedAtDestinationSignal.RemoveListener(ArrivedAtWayPoint);
			animationCallback.RemoveListener(AnimationCallback);
		}

		private void AnimateKevin(string animation)
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

		private void HandleGreeting(bool shouldGreet)
		{
			view.GreetVillain(shouldGreet);
		}

		private void WaveFarewell(bool shouldWave)
		{
			view.WaveFarewell(shouldWave);
		}

		private void GotoWelcomeHut(bool pop)
		{
			view.GotoWelcomeHut(injectionBinder.GetInstance<global::UnityEngine.GameObject>(global::Kampai.Game.StaticItem.WELCOME_BOOTH_BUILDING_ID_DEF), pop);
		}

		private void Wander(global::Kampai.Game.LocationIncidentalAnimationDefinition animationDefinition)
		{
			global::Kampai.Game.KevinCharacter byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.KevinCharacter>(view.ID);
			byInstanceId.CurrentFrolicLocation = animationDefinition.Location;
			global::Kampai.Game.MinionAnimationDefinition mad = definitionService.Get<global::Kampai.Game.MinionAnimationDefinition>(animationDefinition.AnimationId);
			view.Wander(animationDefinition, mad);
		}

		private void ArrivedAtWayPoint(int minionID)
		{
			if (view.ID == minionID)
			{
				view.ArrivedAtWayPoint();
			}
		}

		private void AnimationCallback()
		{
			kevinFrolicsSignal.Dispatch();
		}
	}
}
