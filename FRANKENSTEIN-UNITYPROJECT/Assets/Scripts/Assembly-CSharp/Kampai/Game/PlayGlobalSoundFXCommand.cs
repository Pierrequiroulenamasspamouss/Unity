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
			FMOD_StudioSystem instance = FMOD_StudioSystem.instance;
			string guid = fmodService.GetGuid(audioSource);
			global::FMOD.Studio.EventInstance eventInstance = instance.GetEvent(guid);
			if (eventInstance == null)
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "Failed to Load Audio Source: " + audioSource);
				return;
			}
			eventInstance.set3DAttributes(global::FMOD.Studio.UnityUtil.to3DAttributes(audioListener));
			global::FMOD.RESULT rESULT = eventInstance.start();
			if (!global::FMOD.Studio.UnityUtil.ERRCHECK(rESULT))
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "Failed to Play (Error Code: " + rESULT.ToString() + ") Audio Source: " + audioSource);
			}
		}
	}
}
