namespace Kampai.Fatal
{
	public class FatalView : global::strange.extensions.mediation.impl.View
	{
		[Inject]
		public global::Kampai.Main.LoadNewGameLevelSignal loadNewGameLevelSignal { get; set; }

		private void OnApplicationPause(bool isPausing)
		{
			if (!isPausing && loadNewGameLevelSignal != null)
			{
				loadNewGameLevelSignal.Dispatch("Initialize");
			}
		}

		private void Update()
		{
			if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Escape))
			{
				loadNewGameLevelSignal.Dispatch("Initialize");
			}
		}
	}
}
