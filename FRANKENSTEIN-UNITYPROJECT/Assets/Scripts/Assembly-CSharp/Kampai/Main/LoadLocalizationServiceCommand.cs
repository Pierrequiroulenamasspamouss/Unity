namespace Kampai.Main
{
	public class LoadLocalizationServiceCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

        public override void Execute()
        {
            logger.Debug("Start loading localization Service");
            logger.EventStart("LoadLocalizationServiceCommand.Execute");

            if (!localService.IsInitialized())
            {
                // Service wasn't initialized earlier — do it now with the device language.
                string lang = global::Kampai.Util.Native.GetDeviceLanguage();
                logger.Info("Localization not yet initialized. Initializing with language: {0}", lang);
                localService.Initialize(lang);
            }

            localService.Update();

            logger.EventStop("LoadLocalizationServiceCommand.Execute");
        }
    }
}
