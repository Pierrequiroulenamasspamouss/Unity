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

            if (localService.IsInitialized())
            {
                localService.Update();
            }
            else
            {
                // --- FIX: CHANGED FATAL TO WARNING ---
                // Do NOT stop the game. Just log it. 
                // The UI might show string keys (e.g. "IDS_WELCOME"), but the game will run.
                logger.Warning("Localization service hasn't been initialized yet. Skipping update to prevent FATAL crash.");

                // Optional: Try to force a default if your ILocalizationService interface supports it.
                // But simply removing the Fatal call is enough to unblock the flow.
            }

            logger.EventStop("LoadLocalizationServiceCommand.Execute");
        }
    }
}
