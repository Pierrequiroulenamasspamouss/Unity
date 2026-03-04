namespace Kampai.Game
{
	public class PlayGlobalSoundFXOneShotCommand : global::strange.extensions.command.impl.Command
	{
		[Inject(global::Kampai.Main.MainElement.AUDIO_LISTENER)]
		public global::UnityEngine.GameObject audioListener { get; set; }

		[Inject]
		public string audioClip { get; set; }

		public override void Execute()
		{
			global::FMODUnity.RuntimeManager.PlayOneShot(audioClip, audioListener.transform.position);
		}
	}
}
