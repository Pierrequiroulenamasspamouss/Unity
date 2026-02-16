namespace Kampai.Game
{
	public class QueueLocalAudioCommand
	{
		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public void Execute(CustomFMOD_StudioEventEmitter emitter, string audioSource)
		{
			emitter.QueueClip(audioSource);
		}
	}
}
