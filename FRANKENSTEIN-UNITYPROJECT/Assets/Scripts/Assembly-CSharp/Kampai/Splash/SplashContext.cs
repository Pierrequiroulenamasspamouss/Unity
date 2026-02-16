namespace Kampai.Splash
{
	public class SplashContext : global::Kampai.Util.BaseContext
	{
		public SplashContext()
		{
		}

		public SplashContext(global::UnityEngine.MonoBehaviour view, bool autoStartup)
			: base(view, autoStartup)
		{
		}

		protected override void MapBindings()
		{
			injectionBinder.Bind<global::strange.extensions.context.api.ICrossContextCapable>().ToValue(this).ToName(global::Kampai.Splash.SplashElement.CONTEXT)
				.CrossContext();
			base.mediationBinder.Bind<global::Kampai.Splash.LoadInTipView>().To<global::Kampai.Splash.LoadInTipMediator>();
			base.mediationBinder.Bind<global::Kampai.Splash.LoadingBarView>().To<global::Kampai.Splash.LoadingBarMediator>();
			base.commandBinder.Bind<global::Kampai.Common.StartSignal>().To<global::Kampai.Splash.SplashStartCommand>();
			base.commandBinder.Bind<global::Kampai.Splash.HideSplashSignal>().To<global::Kampai.Splash.HideSplashCommand>();
		}
	}
}
