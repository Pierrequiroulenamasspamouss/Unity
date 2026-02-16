namespace Kampai.Splash
{
	public class SplashStartCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Common.IVideoService videoService { get; set; }

		[Inject]
		public global::Kampai.Splash.ILoadInService loadInService { get; set; }

		[Inject]
		public global::Kampai.Splash.SplashProgressUpdateSignal splashProgressUpdateSignal { get; set; }

		public override void Execute()
		{
			logger.Debug("Loggly test: This log should have a user ID attached in Loggly!");
			global::UnityEngine.GameObject gameObject = global::UnityEngine.GameObject.Find("SplashRoot");
			if (gameObject != null)
			{
				gameObject.GetComponent<global::Kampai.Util.SplashLogoView>().EnableLogo();
			}
			global::UnityEngine.GameObject gameObject2 = gameObject.FindChild("ToolTip");
			gameObject2.AddComponent("LoadInTipView");
			global::UnityEngine.GameObject gameObject3 = gameObject.FindChild("meter_bar");
			gameObject3.AddComponent("LoadingBarView");
			splashProgressUpdateSignal.Dispatch(10, 10f);
			global::UnityEngine.Application.LoadLevelAdditive("Main");
			global::UnityEngine.Application.LoadLevelAdditive("Download");
		}
	}
}
