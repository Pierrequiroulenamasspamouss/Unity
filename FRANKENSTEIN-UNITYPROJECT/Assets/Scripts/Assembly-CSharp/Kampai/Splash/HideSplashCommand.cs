namespace Kampai.Splash
{
    public class HideSplashCommand : global::strange.extensions.command.impl.Command
    {
        [Inject(global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW)]
        public global::UnityEngine.GameObject ContextView { get; set; }

        [Inject]
        public global::Kampai.Util.ILogger Logger { get; set; }

        [Inject]
        public global::Kampai.Util.IRoutineRunner RoutineRunner { get; set; }

        [Inject]
        public global::Kampai.Main.AppStartCompleteSignal AppStartCompleteSignal { get; set; }

        public override void Execute()
        {
            Logger.EventStart("HideSplashCommand.Execute");

            // Dispatch signal FIRST before hiding splash
            AppStartCompleteSignal.Dispatch();

            // Then hide the splash by deactivating the ContextView
            // This is safer than destroying it or searching for specific GameObjects
            if (ContextView != null)
            {
                ContextView.SetActive(false);
                Logger.Info("HideSplashCommand: Deactivated splash ContextView");
            }

            Logger.EventStop("HideSplashCommand.Execute");
        }
    }
}
