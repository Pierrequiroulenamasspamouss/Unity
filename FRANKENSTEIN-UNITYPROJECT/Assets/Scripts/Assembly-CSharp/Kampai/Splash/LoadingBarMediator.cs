namespace Kampai.Splash
{
	public class LoadingBarMediator : global::strange.extensions.mediation.impl.Mediator
	{
		private float start;

		private float target;

		private float current;

		private float timeTarget;

		private float timeRemaining;

		[Inject]
		public global::Kampai.Splash.LoadingBarView view { get; set; }

		[Inject]
		public global::Kampai.Splash.SplashProgressUpdateSignal splashProgressUpdateSignal { get; set; }

		[Inject]
		public global::Kampai.Splash.SplashProgressResetSignal splashProgressResetSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void OnRegister()
		{
			splashProgressUpdateSignal.AddListener(OnSplashProgressUpdate);
			splashProgressResetSignal.AddListener(OnSplashProgressReset);
			view.Init();
		}

		public override void OnRemove()
		{
			splashProgressUpdateSignal.RemoveListener(OnSplashProgressUpdate);
			splashProgressResetSignal.RemoveListener(OnSplashProgressReset);
		}

		private void OnSplashProgressUpdate(int target, float time)
		{
			logger.Debug("Loading Progress {0}:{1}", target, time);
			this.target += target;
			start = current;
			if (this.target > 100f)
			{
				this.target = 100f;
			}
			timeRemaining = time;
			timeTarget = time;
		}

		private void OnSplashProgressReset()
		{
			logger.Debug("Loading Progress Reset");
			start = 0f;
			target = 0f;
			current = 0f;
			timeTarget = 0f;
			timeRemaining = 0f;
			UpdateView();
		}

		private void Update()
		{
			if (timeRemaining > 0f && timeTarget > 0f)
			{
				float deltaTime = global::UnityEngine.Time.deltaTime;
				timeRemaining -= deltaTime;
				current += (target - start) * (deltaTime / timeTarget);
				if (current > 100f)
				{
					current = 100f;
				}
				UpdateView();
			}
		}

		private void UpdateView()
		{
			view.SetProgressCounter((int)current);
			view.SetMeterFill((int)current);
		}
	}
}
