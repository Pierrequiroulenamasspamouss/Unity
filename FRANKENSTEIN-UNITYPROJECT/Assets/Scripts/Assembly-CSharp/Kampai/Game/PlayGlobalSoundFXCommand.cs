namespace Kampai.Game
{
	public class PlayGlobalSoundFXCommand
	{
		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Common.Service.Audio.IFMODService fmodService { get; set; }

		[Inject(global::Kampai.Main.MainElement.AUDIO_LISTENER)]
		public global::UnityEngine.GameObject audioListener { get; set; }

		public void Execute(string audioSource)
		{
			string guid = fmodService.GetGuid(audioSource);
			if (string.IsNullOrEmpty(guid))
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "Failed to Load Audio Source: " + audioSource);
				return;
			}
			global::FMOD.Studio.EventInstance eventInstance = global::FMODUnity.RuntimeManager.CreateInstance(new global::System.Guid(guid));
			eventInstance.set3DAttributes(global::FMODUnity.RuntimeUtils.To3DAttributes(audioListener));
			global::FMOD.RESULT rESULT = eventInstance.start();
			if (rESULT != global::FMOD.RESULT.OK)
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "Failed to Play (Error Code: " + rESULT.ToString() + ") Audio Source: " + audioSource);
			}
		}
	}
}
