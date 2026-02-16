namespace Kampai.Game.View
{
	public class LevelUpCommand : global::strange.extensions.command.impl.Command
	{
		[Inject(global::Kampai.UI.View.UIElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable uiContext { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerDurationService playerDurationService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal playSoundFX { get; set; }

		[Inject]
		public global::Kampai.Game.AwardLevelSignal awardLevelSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService characterService { get; set; }

		[Inject]
		public global::Kampai.Game.GetNewQuestSignal getNewQuestSignal { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.Game.UpdateForSaleSignsSignal updateForSaleSignsSignal { get; set; }

		[Inject]
		public global::Kampai.Game.UpdateMarketplaceRepairStateSignal updateMarketplaceSignal { get; set; }

		[Inject]
		public global::Kampai.Game.UnlockCharacterModel model { get; set; }

		[Inject]
		public global::Kampai.Splash.ILoadInService loadInService { get; set; }

		public override void Execute()
		{
			playerService.AlterQuantity(global::Kampai.Game.StaticItem.LEVEL_ID, 1);
			loadInService.SaveTipsForNextLaunch((int)playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID));
			if (model.characterUnlocks.Count == 0)
			{
				uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.DisplayLevelUpRewardSignal>().Dispatch();
			}
			global::Kampai.Game.Transaction.TransactionDefinition rewardTransaction = RewardUtil.GetRewardTransaction(definitionService, playerService);
			awardLevelSignal.Dispatch(rewardTransaction);
			characterService.UpdateEligiblePrestigeList();
			getNewQuestSignal.Dispatch();
			telemetryService.Send_Telemetry_EVT_GP_LEVEL_PROMOTION_DAYS_TOTAL_EALL();
			playerDurationService.MarkLevelUpUTC();
			updateMarketplaceSignal.Dispatch();
			updateForSaleSignsSignal.Dispatch();
		}
	}
}
