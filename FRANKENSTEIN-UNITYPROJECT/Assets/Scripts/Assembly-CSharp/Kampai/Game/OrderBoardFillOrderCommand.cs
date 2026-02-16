namespace Kampai.Game
{
	public class OrderBoardFillOrderCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.OrderBoard building { get; set; }

		[Inject]
		public int TicketIndex { get; set; }

		[Inject]
		public global::Kampai.Game.Transaction.TransactionDefinition def { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.OrderBoardSetNewTicketSignal setNewTicketSignal { get; set; }

		[Inject]
		public global::Kampai.Game.OrderBoardTransactionFailedSignal transactionFailedSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService prestigeService { get; set; }

		[Inject]
		public global::Kampai.Game.HarvestReadySignal harvestReadySignal { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetStorageCapacitySignal setStorageSignal { get; set; }

		public override void Execute()
		{
			int startTime = timeService.GameTimeSeconds();
			playerService.StartTransaction(def, global::Kampai.Game.TransactionTarget.BLACKMARKETBOARD, TransactionCallback, new global::Kampai.Game.TransactionArg(building.ID), startTime, TicketIndex);
		}

		private void TransactionCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			if (pct.Success)
			{
				soundFXSignal.Dispatch("Play_fill_order_01");
				int characterDefinitionId = building.tickets[TicketIndex].CharacterDefinitionId;
				setNewTicketSignal.Dispatch(-TicketIndex, true);
				telemetryService.Send_Telemetry_EVT_GP_ACHIEVEMENTS_CHECKPOINTS_EAL(TicketIndex.ToString(), global::Kampai.Common.Service.Telemetry.AchievementType.Order, string.Empty);
				GetReward(characterDefinitionId);
			}
			else
			{
				building.HarvestableCharacterDefinitionId = 0;
				transactionFailedSignal.Dispatch(def);
			}
		}

		private void GetReward(int prestigeDefID)
		{
			global::Kampai.Game.TransactionArg transactionArg = new global::Kampai.Game.TransactionArg(building.ID);
			if (prestigeDefID != 0)
			{
				building.HarvestableCharacterDefinitionId = prestigeDefID;
				global::Kampai.Game.Prestige prestige = prestigeService.GetPrestige(prestigeDefID);
				if (prestige != null)
				{
					prestige.CurrentOrdersCompleted++;
					transactionArg.AddAccumulator(prestige);
				}
			}
			if (playerService.FinishTransaction(def, global::Kampai.Game.TransactionTarget.BLACKMARKETBOARD, transactionArg))
			{
				questService.UpdateBlackMarketTask();
			}
			setStorageSignal.Dispatch();
		}
	}
}
