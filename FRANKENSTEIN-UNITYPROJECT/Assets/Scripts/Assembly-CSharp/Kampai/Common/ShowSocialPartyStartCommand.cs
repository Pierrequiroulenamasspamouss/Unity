namespace Kampai.Common
{
	public class ShowSocialPartyStartCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal globalSFX { get; set; }

		public override void Execute()
		{
			globalSFX.Dispatch("Play_menu_popUp_01");
			global::Kampai.UI.View.IGUICommand iGUICommand = guiService.BuildCommand(global::Kampai.UI.View.GUIOperation.Load, "cmp_FlyOut_SocialParty");
			iGUICommand.skrimScreen = "StageSkrim";
			iGUICommand.darkSkrim = false;
			guiService.Execute(iGUICommand);
		}
	}
}
