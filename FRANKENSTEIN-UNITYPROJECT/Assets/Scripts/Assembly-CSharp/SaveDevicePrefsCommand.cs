public class SaveDevicePrefsCommand : global::strange.extensions.command.impl.Command
{
	[Inject]
	public global::Kampai.Game.IDevicePrefsService prefsService { get; set; }

	[Inject]
	public ILocalPersistanceService persistService { get; set; }

	[Inject]
	public global::Kampai.Util.ILogger logger { get; set; }

	public override void Execute()
	{
		string value = prefsService.Serialize();
		if (string.IsNullOrEmpty(value))
		{
			logger.Log(global::Kampai.Util.Logger.Level.Debug, "Problem serializing device prefs");
		}
		else
		{
			persistService.PutData("DevicePrefs", value);
		}
	}
}
