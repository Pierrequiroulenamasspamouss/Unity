public class UpdateVolumeCommand : global::strange.extensions.command.impl.Command
{
	[Inject]
	public global::Kampai.Game.IDevicePrefsService prefs { get; set; }

	[Inject]
	public global::Kampai.Util.ILogger logger { get; set; }

	public override void Execute()
	{
		global::Kampai.Game.DevicePrefs devicePrefs = prefs.GetDevicePrefs();
		FMOD_StudioSystem instance = FMOD_StudioSystem.instance;
		global::FMOD.Studio.Bus bus;
		instance.System.getBus("bus:/Non-Diegetic/u_Music", out bus);
		if (bus != null)
		{
			if (!global::Kampai.Audio.AudioSettingsModel.MusicMute)
			{
				bool mute = false;
				bus.getMute(out mute);
				if (mute)
				{
					bus.setMute(false);
				}
			}
			bus.setFaderLevel(devicePrefs.MusicVolume);
		}
		else
		{
			logger.Log(global::Kampai.Util.Logger.Level.Info, "Could not find Music bus.");
		}
		global::FMOD.Studio.Bus bus2;
		instance.System.getBus("bus:/Diagetic/u_SFX", out bus2);
		if (bus2 != null)
		{
			bus2.setFaderLevel(devicePrefs.SFXVolume);
		}
		else
		{
			logger.Log(global::Kampai.Util.Logger.Level.Info, "Could not find SFX bus.");
		}
		global::FMOD.Studio.Bus bus3;
		instance.System.getBus("bus:/Non-Diegetic/u_UI", out bus3);
		if (bus3 != null)
		{
			bus3.setFaderLevel(devicePrefs.SFXVolume);
		}
		else
		{
			logger.Log(global::Kampai.Util.Logger.Level.Info, "Could not find UI bus.");
		}
	}
}
