namespace Kampai.Game
{
	public class CheckUpgradeCommand : global::strange.extensions.command.impl.Command
	{
        [Inject]
        public global::Kampai.Game.IConfigurationsService configService { get; set; }

        [Inject]
        public global::Kampai.Game.IUserSessionService userSessionService { get; set; }

        [Inject]
        public global::Kampai.UI.View.ShowClientUpgradeDialogSignal showClientUpgradeDialogSignal { get; set; }

        [Inject]
        public global::Kampai.UI.View.ShowForcedClientUpgradeScreenSignal showForcedClientUpgradeScreenSignal { get; set; }

        [Inject]
        public global::Kampai.Util.ClientVersion clientVersionService { get; set; }


        [Inject]
		public global::Kampai.Util.ILogger logger { get; set; }


        public override void Execute()
        {
            logger.Info("MOCK: CheckUpgradeCommand - Bypassing version verification. ");
            return;
        }
    }
}
