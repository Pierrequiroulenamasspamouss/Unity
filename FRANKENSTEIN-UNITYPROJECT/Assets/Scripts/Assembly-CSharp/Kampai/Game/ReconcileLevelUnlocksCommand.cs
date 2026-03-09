namespace Kampai.Game
{
	public class ReconcileLevelUnlocksCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.AwardLevelSignal awardLevelSignal { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.LevelUpDefinition levelUpDefinition = definitionService.Get<global::Kampai.Game.LevelUpDefinition>(88888);
			int levelIndex = (int)(playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID) - 1);

			if (playerService.GetUnlockedQuantityOfID(0) == -1)
			{
				if (levelIndex >= 0 && levelIndex < levelUpDefinition.transactionList.Count)
				{
					global::Kampai.Game.Transaction.TransactionDefinition type = definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(levelUpDefinition.transactionList[levelIndex]);
					awardLevelSignal.Dispatch(type);
				}
				else if (levelIndex != -1)
				{
					logger.Warning("ReconcileLevelUnlocksCommand: levelIndex {0} is out of bounds for transactionList (Count {1})", levelIndex, levelUpDefinition.transactionList.Count);
				}
				return;
			}
			global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition = new global::Kampai.Game.Transaction.TransactionDefinition();
			transactionDefinition.Inputs = new global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem>();
			transactionDefinition.Outputs = new global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem>();
			int quantity = (int)playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID);
			for (int i = 0; i < quantity; i++)
			{
				if (i >= levelUpDefinition.transactionList.Count)
				{
					continue;
				}
				global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition2 = definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(levelUpDefinition.transactionList[i]);
				foreach (global::Kampai.Util.QuantityItem output in transactionDefinition2.Outputs)
				{
					global::Kampai.Game.UnlockDefinition definition = null;
					if (definitionService.TryGet<global::Kampai.Game.UnlockDefinition>(output.ID, out definition) && playerService.GetUnlockedQuantityOfID(definition.ID) == 0)
					{
						transactionDefinition.Outputs.Add(output);
					}
				}
			}
			if (transactionDefinition.Outputs.Count > 0)
			{
				playerService.RunEntireTransaction(transactionDefinition, global::Kampai.Game.TransactionTarget.NO_VISUAL, null);
			}
		}
	}
}
