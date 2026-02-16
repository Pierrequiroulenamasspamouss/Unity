namespace Kampai.UI.View
{
	public class DisplayLevelUpRewardCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.UnlockCharacterModel unlockCharacterModel { get; set; }

		[Inject]
		public global::Kampai.UI.Model.LevelUpModel levelUpModel { get; set; }

		public override void Execute()
		{
			levelUpModel.IsRewardUiOpened = true;
			global::Kampai.Game.Transaction.TransactionDefinition rewardTransaction = RewardUtil.GetRewardTransaction(definitionService, playerService);
			global::System.Collections.Generic.List<global::Kampai.Game.View.RewardQuantity> rewardQuantityFromTransaction = RewardUtil.GetRewardQuantityFromTransaction(rewardTransaction, definitionService, playerService);
			global::Kampai.UI.View.IGUICommand iGUICommand = guiService.BuildCommand(global::Kampai.UI.View.GUIOperation.Queue, "popup_LevelUp");
			iGUICommand.Args.Add(rewardQuantityFromTransaction);
			iGUICommand.ShouldShowPredicate = () => unlockCharacterModel.characterUnlocks.Count == 0;
			guiService.Execute(iGUICommand);
		}
	}
}
