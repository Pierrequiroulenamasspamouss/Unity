public static class RewardUtil
{
	public static global::Kampai.Game.Transaction.TransactionDefinition GetRewardTransaction(global::Kampai.Game.IDefinitionService definitionService, global::Kampai.Game.IPlayerService playerService)
	{
		global::Kampai.Game.LevelUpDefinition levelUpDefinition = definitionService.Get<global::Kampai.Game.LevelUpDefinition>(88888);
		global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition;
		if ((int)(playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID) - 1) < levelUpDefinition.transactionList.Count)
		{
			transactionDefinition = definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(levelUpDefinition.transactionList[(int)(playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID) - 1)]);
		}
		else
		{
			transactionDefinition = new global::Kampai.Game.Transaction.TransactionDefinition();
			transactionDefinition.Outputs = new global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem>();
			transactionDefinition.Outputs.Add(new global::Kampai.Util.QuantityItem(1, 2u));
		}
		return transactionDefinition;
	}

	public static global::System.Collections.Generic.List<global::Kampai.Game.View.RewardQuantity> GetRewardQuantityFromTransaction(global::Kampai.Game.Transaction.TransactionDefinition transaction, global::Kampai.Game.IDefinitionService definitionService, global::Kampai.Game.IPlayerService playerService)
	{
		global::System.Collections.Generic.List<global::Kampai.Game.View.RewardQuantity> list = new global::System.Collections.Generic.List<global::Kampai.Game.View.RewardQuantity>();
		foreach (global::Kampai.Util.QuantityItem output in transaction.Outputs)
		{
			if (output.ID == 0 || output.ID == 1 || output.ID == 5)
			{
				list.Add(new global::Kampai.Game.View.RewardQuantity(output.ID, (int)output.Quantity, false, true));
			}
			else
			{
				if (output.ID == 6 || output.ID == 9)
				{
					continue;
				}
				bool isNew = false;
				global::Kampai.Game.UnlockDefinition definition;
				if (definitionService.TryGet<global::Kampai.Game.UnlockDefinition>(output.ID, out definition))
				{
					int unlockedQuantityOfID = playerService.GetUnlockedQuantityOfID(definition.ReferencedDefinitionID);
					if (!definition.Delta && unlockedQuantityOfID >= definition.UnlockedQuantity * (int)output.Quantity)
					{
						continue;
					}
					if (unlockedQuantityOfID == 0)
					{
						isNew = true;
					}
				}
				list.Add(new global::Kampai.Game.View.RewardQuantity(output.ID, (int)output.Quantity, isNew, false));
			}
		}
		return list;
	}
}
