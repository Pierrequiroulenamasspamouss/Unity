namespace Kampai.Download
{
	public class DownloadContext : global::Kampai.Util.BaseContext
	{
		public DownloadContext()
		{
		}

		public DownloadContext(global::UnityEngine.MonoBehaviour view, bool autoStartup)
			: base(view, autoStartup)
		{
		}

		protected override void MapBindings()
		{
			// [MOVED TO MAIN CONTEXT]
			// injectionBinder.Bind<global::Kampai.Download.ShowDLCPanelSignal>().ToSingleton().CrossContext();
			// injectionBinder.Bind<global::Kampai.Download.LaunchDownloadSignal>().ToSingleton().CrossContext();
			// injectionBinder.Bind<global::Kampai.Common.ReconcileDLCSignal>().ToSingleton().CrossContext();
			
			// [REMOVED] IDLCService is now bound in MainContext to support startup Configurations
			// injectionBinder.Bind<global::Kampai.Game.IDLCService>().To<global::Kampai.Game.DLCService>().ToSingleton()
			// 	.CrossContext();
			
			// [MOVED TO MAIN CONTEXT]
			// injectionBinder.Bind<global::Kampai.Download.DLCModel>().ToSingleton().CrossContext();
			
			injectionBinder.Bind<global::strange.extensions.context.api.ICrossContextCapable>().ToValue(this).ToName(global::Kampai.Download.DownloadElement.CONTEXT);
			injectionBinder.Bind<global::Kampai.Download.DownloadInitializeSignal>().ToSingleton();
			injectionBinder.Bind<global::Kampai.Download.DownloadProgressSignal>().ToSingleton();
			injectionBinder.Bind<global::Kampai.Download.DLCLoadScreenModel>().ToSingleton();
			injectionBinder.Bind<global::Kampai.Main.PlayGlobalSoundFXSignal>().ToSingleton();
			injectionBinder.Bind<global::Kampai.Common.AppPauseSignal>().ToSingleton();
			injectionBinder.Bind<global::Kampai.Common.AppQuitSignal>().ToSingleton();
			injectionBinder.Bind<global::Kampai.Common.AppFocusGainedSignal>().ToSingleton();
			injectionBinder.Bind<global::Kampai.Download.DLCDownloadFinishedSignal>().ToSingleton();
			injectionBinder.Bind<global::Kampai.Download.ShowNoWiFiPanelSignal>().ToSingleton();
			injectionBinder.Bind<global::Kampai.Download.Model.DownloadUIModel>().ToSingleton();
			base.commandBinder.Bind<global::Kampai.Common.AppResumeSignal>().To<global::Kampai.Download.DownloadResumeCommand>();
			base.commandBinder.Bind<global::Kampai.Common.StartSignal>().To<global::Kampai.Download.DownloadStartCommand>();
			base.commandBinder.Bind<global::Kampai.Download.LaunchDownloadSignal>().To<global::Kampai.Download.LaunchDownloadCommand>();
			base.commandBinder.Bind<global::Kampai.Download.DownloadDLCPartSignal>().To<global::Kampai.Download.DownloadDLCPartCommand>();
			base.commandBinder.Bind<global::Kampai.Download.DownloadResponseSignal>().To<global::Kampai.Download.DownloadResponseCommand>();
			base.commandBinder.Bind<global::Kampai.Download.ShowDLCPanelSignal>().To<global::Kampai.Download.ShowDLCPanelCommand>();
			base.commandBinder.Bind<global::Kampai.Common.ReconcileDLCSignal>().To<global::Kampai.Main.ReconcileDLCCommand>();
			base.mediationBinder.Bind<global::Kampai.Download.View.DLCProgressBarView>().To<global::Kampai.Download.View.DLCProgressBarMediator>();
			base.mediationBinder.Bind<global::Kampai.Download.View.NoWiFiView>().To<global::Kampai.Download.View.NoWiFiMediator>();
			base.mediationBinder.Bind<global::Kampai.Download.DownloadPanelView>().To<global::Kampai.Download.DownloadPanelMediator>();
		}
	}
}
