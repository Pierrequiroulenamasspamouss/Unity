namespace Kampai.Main
{
	public class InitLocalizationServiceCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Main.LocalizationServiceInitializedSignal localizationServiceInitializedSignal { get; set; }

		[Inject(global::Kampai.Main.LocalizationServices.EVENT)]
		public global::Kampai.Main.ILocalizationService localEventService { get; set; }

		[Inject]
		public global::Kampai.Splash.SplashProgressUpdateSignal splashProgressDoneSignal { get; set; }

		public override void Execute()
		{
			if (!localService.IsInitialized())
			{
				string deviceLanguage = global::Kampai.Util.Native.GetDeviceLanguage();
				logger.Info("Got language code {0}", deviceLanguage);
				localService.Initialize(deviceLanguage);
				localEventService.Initialize("EN-US");
				localizationServiceInitializedSignal.Dispatch();
				splashProgressDoneSignal.Dispatch(20, 5f);
			}
		}
	}
}
