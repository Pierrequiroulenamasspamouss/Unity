namespace Kampai.UI.Controller
{
	public class CloseLevelUpRewardCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.UI.Model.LevelUpModel levelUpModel { get; set; }

		public override void Execute()
		{
			if (levelUpModel.IsRewardUiOpened)
			{
				guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "popup_LevelUp");
				levelUpModel.IsRewardUiOpened = false;
			}
		}
	}
}
