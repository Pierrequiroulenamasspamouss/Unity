namespace Kampai.Common
{
	public class ShowSocialPartyRewardCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public int eventIndex { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal globalSFX { get; set; }

		public override void Execute()
		{
			global::Kampai.UI.View.IGUICommand iGUICommand = null;
			iGUICommand = guiService.BuildCommand(global::Kampai.UI.View.GUIOperation.Load, "popup_SocialPartyReward");
			global::Kampai.UI.View.GUIArguments args = iGUICommand.Args;
			iGUICommand.skrimScreen = "SocialReward";
			iGUICommand.darkSkrim = true;
			args.Add(typeof(int), eventIndex);
			guiService.Execute(iGUICommand);
			globalSFX.Dispatch("Play_menu_popUp_01");
		}
	}
}
