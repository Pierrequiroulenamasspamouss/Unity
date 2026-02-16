namespace Kampai.Game.View
{
	public class PhilMediator : global::strange.extensions.mediation.impl.Mediator
	{
		[Inject]
		public global::Kampai.Game.View.PhilView view { get; set; }

		[Inject]
		public global::Kampai.Game.PhilCelebrateSignal celebrateSignal { get; set; }

		[Inject]
		public global::Kampai.Game.PhilGetAttentionSignal getAttentionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.PhilBeginIntroLoopSignal beginIntroLoopSignal { get; set; }

		[Inject]
		public global::Kampai.Game.PhilPlayIntroSignal playIntroSignal { get; set; }

		[Inject]
		public global::Kampai.Game.PhilSitAtBarSignal sitAtBarSignal { get; set; }

		[Inject]
		public global::Kampai.Game.PhilActivateSignal activateSignal { get; set; }

		[Inject]
		public global::Kampai.Game.AnimatePhilSignal animatePhilSignal { get; set; }

		[Inject]
		public global::Kampai.Game.PhilGoToTikiBarSignal philGoToTikiBarSignal { get; set; }

		[Inject]
		public global::Kampai.Game.PhilEnableTikiBarControllerSignal enableTikiBarControllerSignal { get; set; }

		[Inject]
		public global::Kampai.Game.TeleportCharacterToTikiBarSignal teleportCharacterToTikiBarSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::strange.extensions.injector.api.IInjectionBinder injectionBinder { get; set; }

		[Inject]
		public global::Kampai.Game.TikiBarSetAnimParamSignal tikiBarSetAnimParamSignal { get; set; }

		[Inject]
		public global::Kampai.Game.PhilGoToStartLocationSignal goToStartLocationSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		public override void OnRegister()
		{
			celebrateSignal.AddListener(Celebrate);
			getAttentionSignal.AddListener(GetAttention);
			beginIntroLoopSignal.AddListener(BeginIntroLoop);
			playIntroSignal.AddListener(PlayIntro);
			sitAtBarSignal.AddListener(SitAtBar);
			activateSignal.AddListener(Activate);
			animatePhilSignal.AddListener(AnimatePhil);
			enableTikiBarControllerSignal.AddListener(EnableTikiBarController);
			philGoToTikiBarSignal.AddListener(GotoTikiBar);
			view.AnimSignal.AddListener(SendTikiBarSignal);
			goToStartLocationSignal.AddListener(GoToStartLocation);
		}

		public override void OnRemove()
		{
			celebrateSignal.RemoveListener(Celebrate);
			getAttentionSignal.RemoveListener(GetAttention);
			beginIntroLoopSignal.RemoveListener(BeginIntroLoop);
			playIntroSignal.RemoveListener(PlayIntro);
			sitAtBarSignal.RemoveListener(SitAtBar);
			activateSignal.RemoveListener(Activate);
			animatePhilSignal.RemoveListener(AnimatePhil);
			enableTikiBarControllerSignal.RemoveListener(EnableTikiBarController);
			philGoToTikiBarSignal.RemoveListener(GotoTikiBar);
			goToStartLocationSignal.RemoveListener(GoToStartLocation);
		}

		private void GoToStartLocation()
		{
			view.GoToStartLocation();
		}

		private void Activate(bool activate)
		{
			view.Activate(activate);
		}

		private void SitAtBar(bool sit)
		{
			view.SitAtBar(sit, teleportCharacterToTikiBarSignal);
		}

		private void Celebrate()
		{
			view.Celebrate();
		}

		private void GetAttention(bool enable)
		{
			view.GetAttention(enable);
		}

		private void BeginIntroLoop()
		{
			view.BeginIntroLoop();
		}

		private void PlayIntro()
		{
			view.PlayIntro();
		}

		private void AnimatePhil(string animation)
		{
			if (animation.Equals("idle"))
			{
				view.StopWalking();
			}
			view.Animate(animation);
		}

		private void GotoTikiBar(bool pop)
		{
			view.GotoTikiBar(injectionBinder.GetInstance<global::UnityEngine.GameObject>(global::Kampai.Game.StaticItem.TIKI_BAR_BUILDING_ID_DEF), teleportCharacterToTikiBarSignal, routineRunner);
		}

		private void EnableTikiBarController()
		{
			view.EnableTikiBarController();
		}

		private void SendTikiBarSignal(string animation, global::System.Type type, object obj)
		{
			tikiBarSetAnimParamSignal.Dispatch(animation, type, obj);
		}
	}
}
