namespace Kampai.Main
{
	public class MainStartCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Main.InitLocalizationServiceSignal initLocalizationServiceSignal { get; set; }

		[Inject]
		public global::Kampai.Common.CheckAvailableStorageSignal checkAvailableStorageSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Util.IInvokerService invokerService { get; set; }

		[Inject]
		public global::Kampai.Common.NetworkModel networkModel { get; set; }

		[Inject]
		public global::Kampai.Common.NetworkConnectionLostSignal networkConnectionLostSignal { get; set; }

		[Inject]
		public global::Kampai.Main.SetupHockeyAppSignal setupHockeyAppSignal { get; set; }

		[Inject]
		public global::Kampai.Main.SetupEventSystemSignal loadEventSystemSignal { get; set; }

		[Inject]
		public global::Kampai.Game.LoadConfigurationSignal loadConfigurationSignal { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.Common.NimbleTelemetrySender nimbleTelemetryService { get; set; }

		[Inject]
		public global::Kampai.Main.SetupNativeAlertManagerSignal setupNativeAlertManagerSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Common.SetupLogglyServiceSignal setupLogglyServiceSignal { get; set; }

		public override void Execute()
		{
			logger.EventStart("MainStartCommand.Execute");
			initLocalizationServiceSignal.Dispatch();
			checkAvailableStorageSignal.Dispatch(string.Empty, 2097152uL, ContinueExecution);
			logger.EventStop("MainStartCommand.Execute");
		}

		private void ContinueExecution()
		{
			telemetryService.AddTelemetrySender(nimbleTelemetryService);
			telemetryService.SharingUsageCompliance();
			telemetryService.COPPACompliance();
			telemetryService.Send_Telemetry_EVT_USER_GAME_LOAD_FUNNEL("10 - Load Start", playerService.SWRVEGroup);
			global::UnityEngine.Application.targetFrameRate = 30;
			bool autorotateToLandscapeRight = (global::UnityEngine.Screen.autorotateToLandscapeLeft = global::Kampai.Util.Native.AutorotationIsOSAllowed());
			global::UnityEngine.Screen.autorotateToLandscapeRight = autorotateToLandscapeRight;
			SetupBindings();
			routineRunner.StartCoroutine(VerifyNetworkRoutine());
			setupHockeyAppSignal.Dispatch();
			setupLogglyServiceSignal.Dispatch();
			loadEventSystemSignal.Dispatch();
			loadConfigurationSignal.Dispatch(true);
			setupNativeAlertManagerSignal.Dispatch();
		}

		private void SetupBindings()
		{
			global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject("Managers");
			base.injectionBinder.Bind<global::UnityEngine.GameObject>().ToValue(gameObject).ToName(global::Kampai.Main.MainElement.MANAGER_PARENT)
				.CrossContext();
			global::UnityEngine.GameObject gameObject2 = new global::UnityEngine.GameObject("Invoker");
			gameObject2.transform.parent = gameObject.transform;
			global::Kampai.Util.Invoker invoker = gameObject2.AddComponent<global::Kampai.Util.Invoker>();
			global::Kampai.Util.InvokerService invokerService = this.invokerService as global::Kampai.Util.InvokerService;
			if (invokerService != null)
			{
				invoker.Initialize(invokerService);
			}
			else
			{
				logger.Error("Unexpected binding to IInvokerService. InvokerService is expected.");
			}
			global::UnityEngine.GameObject gameObject3 = new global::UnityEngine.GameObject("AppTracker");
			gameObject3.transform.parent = gameObject.transform;
			gameObject3.AddComponent<AppTrackerView>();
		}

		private global::System.Collections.IEnumerator VerifyNetworkRoutine()
		{
			while (true)
			{
				if (!networkModel.isConnectionLost && !global::Kampai.Util.NetworkUtil.IsConnected())
				{
					networkConnectionLostSignal.Dispatch();
				}
				yield return new global::UnityEngine.WaitForSeconds(15f);
			}
		}
	}
}
