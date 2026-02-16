namespace Kampai.Common
{
	public class SetupLogglyServiceCommand : global::strange.extensions.command.impl.Command
	{
		private const float DEFAULT_SHIPMENT_INTERVAL = 60f;

		private const float DEFAULT_PROCESS_INTERVAL = 1f;

		[Inject]
		public global::Kampai.Util.Logging.Hosted.ILogglyService logglyService { get; set; }

		[Inject]
		public global::Kampai.Game.IConfigurationsService configurationsService { get; set; }

		[Inject]
		public global::Kampai.Game.IUserSessionService userSessionService { get; set; }

		[Inject]
		public ILocalPersistanceService localPersistService { get; set; }

		[Inject]
		public global::Kampai.Game.ConfigurationsLoadedSignal configurationsLoadedSignal { get; set; }

		[Inject]
		public global::Kampai.Game.UserSessionGrantedSignal userSessionGrantedSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Util.Logging.Hosted.LogglyDtoCache logglyDtoCache { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			//logger.EventStart("SetupLogglyServiceCommand.Execute");
			//logger.Info("Setting up Loggly Service!!");
			logglyService.Initialize(configurationsService, userSessionService, logglyDtoCache, localPersistService);
			configurationsLoadedSignal.AddListener(logglyService.OnConfigurationsLoaded);
			userSessionGrantedSignal.AddListener(logglyService.OnUserSessionGranted);
			userSessionGrantedSignal.AddOnce(StartProcessingLogs);
			//logger.EventStop("SetupLogglyServiceCommand.Execute");
		}

		private void StartProcessingLogs()
		{
			logger.Debug("Starting to process Loggly logs. This should only happen once!");
			routineRunner.StartCoroutine(logglyService.ShipAllOnInterval(60f));
			routineRunner.StartCoroutine(logglyService.Process(1f));
		}
	}
}
