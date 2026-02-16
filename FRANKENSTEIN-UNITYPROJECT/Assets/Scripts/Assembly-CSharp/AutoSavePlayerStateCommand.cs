public class AutoSavePlayerStateCommand : global::strange.extensions.command.impl.Command
{
	private static int AUTO_SAVE_INTERVAL = 60;

	[Inject]
	public global::Kampai.Game.SavePlayerSignal savePlayerSignal { get; set; }

	[Inject]
	public global::Kampai.Common.LogClientMetricsSignal clientMetricsSignal { get; set; }

	[Inject]
	public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

	public override void Execute()
	{
		routineRunner.StartCoroutine(PeriodicSavePlayer());
	}

	private global::System.Collections.IEnumerator PeriodicSavePlayer()
	{
		while (true)
		{
			yield return new global::UnityEngine.WaitForSeconds(AUTO_SAVE_INTERVAL);
			while (global::Kampai.Game.Mignette.View.MignetteManagerView.GetIsPlaying())
			{
				yield return new global::UnityEngine.WaitForSeconds(1f);
			}
			savePlayerSignal.Dispatch(new global::Kampai.Util.Tuple<string, string, bool>("remote", string.Empty, false));
			clientMetricsSignal.Dispatch(false);
		}
	}
}
