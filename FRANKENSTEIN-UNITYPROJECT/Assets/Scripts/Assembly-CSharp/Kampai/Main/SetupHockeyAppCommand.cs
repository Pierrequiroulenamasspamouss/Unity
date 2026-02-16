namespace Kampai.Main
{
	public class SetupHockeyAppCommand : global::strange.extensions.command.impl.Command
	{
		private string HockeyAppId = global::Kampai.Util.GameConstants.StaticConfig.HOCKEY_APP_ID;

		[Inject(global::Kampai.Main.MainElement.MANAGER_PARENT)]
		public global::UnityEngine.GameObject managers { get; set; }

		[Inject]
		public ILocalPersistanceService persistanceService { get; set; }

		[Inject]
		public global::Kampai.Common.Service.HealthMetrics.IClientHealthService clientHealthService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			//logger.EventStart("SetupHockeyAppCommand.Execute");
			global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject("HockeyApp");
			gameObject.transform.parent = managers.transform;
			gameObject.SetActive(false);
			string userId = persistanceService.GetData("UserID");
			gameObject.name = "HockeyAppUnityAndroid";
			HockeyAppAndroid hockeyAppAndroid = gameObject.AddComponent<HockeyAppAndroid>();
			hockeyAppAndroid.packageID = global::Kampai.Util.Native.BundleIdentifier;
			hockeyAppAndroid.exceptionLogging = true;
			hockeyAppAndroid.appID = HockeyAppId;
			hockeyAppAndroid.userId = userId;
			hockeyAppAndroid.crashReportCallback = delegate
			{
				clientHealthService.MarkMeterEvent("AppFlow.Crash");
			};
			hockeyAppAndroid.autoUpload = true;
			gameObject.SetActive(true);
			//logger.EventStop("SetupHockeyAppCommand.Execute");
		}
	}
}
