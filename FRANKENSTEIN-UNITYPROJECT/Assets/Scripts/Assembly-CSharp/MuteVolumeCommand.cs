public class MuteVolumeCommand : global::strange.extensions.command.impl.Command
{
	[Inject]
	public global::Kampai.Util.ILogger logger { get; set; }

	[Inject]
	public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

	public override void Execute()
	{
		CheckForOtherAudio();
	}

	private void CheckForOtherAudio()
	{
		if (global::Kampai.Audio.AudioSettingsModel.NeedMute)
		{
			bool mute = (global::Kampai.Audio.AudioSettingsModel.MusicMute = global::Kampai.Util.Native.IsUserMusicPlaying());
			MuteMusicBus(mute);
		}
	}

	private void MuteMusicBus(bool mute)
	{
		global::FMOD.Studio.Bus bus;
		global::FMODUnity.RuntimeManager.StudioSystem.getBus("bus:/Non-Diegetic/u_Music", out bus);
		if (bus != null)
		{
			bus.setMute(mute);
		}
		else
		{
			logger.Log(global::Kampai.Util.Logger.Level.Error, "Could not find Music bus.");
		}
	}
}
