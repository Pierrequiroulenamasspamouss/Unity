namespace Kampai.Game.Transaction
{
	public static class TransactionUtil
	{
		public static int GetPremiumCostForTransaction(global::Kampai.Game.Transaction.TransactionDefinition transaction)
		{
			return SumOutputsForStaticItem(transaction, global::Kampai.Game.StaticItem.PREMIUM_CURRENCY_ID, true);
		}

		public static int GetPremiumOutputForTransaction(global::Kampai.Game.Transaction.TransactionDefinition transaction)
		{
			return SumOutputsForStaticItem(transaction, global::Kampai.Game.StaticItem.PREMIUM_CURRENCY_ID);
		}

		public static int GetGrindOutputForTransaction(global::Kampai.Game.Transaction.TransactionDefinition transaction)
		{
			return SumOutputsForStaticItem(transaction, global::Kampai.Game.StaticItem.GRIND_CURRENCY_ID);
		}

		public static string GetTransactionItemName(global::Kampai.Game.Transaction.TransactionDefinition transaction, global::Kampai.Game.IDefinitionService definitionService)
		{
			if (transaction.Outputs != null)
			{
				foreach (global::Kampai.Util.QuantityItem output in transaction.Outputs)
				{
					global::Kampai.Game.IngredientsItemDefinition definition = null;
					definitionService.TryGet<global::Kampai.Game.IngredientsItemDefinition>(output.ID, out definition);
					if (definition != null)
					{
						return definition.LocalizedKey;
					}
				}
			}
			else if (transaction.Inputs != null)
			{
				global::Kampai.Game.IngredientsItemDefinition definition2 = null;
				definitionService.TryGet<global::Kampai.Game.IngredientsItemDefinition>(transaction.Inputs[0].ID, out definition2);
				if (definition2 != null)
				{
					return definition2.LocalizedKey;
				}
			}
			return string.Empty;
		}

		public static int SumOutputsForStaticItem(global::Kampai.Game.Transaction.TransactionDefinition transaction, global::Kampai.Game.StaticItem staticItem, bool inputs = false)
		{
			int num = 0;
			global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> list;
			if (inputs)
			{
				global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> inputs2 = transaction.Inputs;
				list = inputs2;
			}
			else
			{
				list = transaction.Outputs;
			}
			global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> list2 = list;
			if (list2 != null)
			{
				foreach (global::Kampai.Util.QuantityItem item in list2)
				{
					if (item.ID == (int)staticItem)
					{
						num += (int)item.Quantity;
					}
				}
			}
			return num;
		}

		public static int SumInputsForStaticItem(global::Kampai.Game.Transaction.TransactionDefinition transaction, global::Kampai.Game.StaticItem staticItem)
		{
			return SumOutputsForStaticItem(transaction, staticItem, true);
		}

		public static bool IsOnlyPremiumInputs(global::Kampai.Game.Transaction.TransactionDefinition def)
		{
			return IsOnlyIDInputs(def, 1);
		}

		public static bool IsOnlyGrindInputs(global::Kampai.Game.Transaction.TransactionDefinition def)
		{
			return IsOnlyIDInputs(def, 0);
		}

		public static bool IsOnlyIDInputs(global::Kampai.Game.Transaction.TransactionDefinition def, int id)
		{
			bool result = false;
			global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> inputs = def.Inputs;
			if (inputs != null && inputs.Count > 0)
			{
				result = inputs[0].ID == id;
				foreach (global::Kampai.Util.QuantityItem item in inputs)
				{
					if (item.ID != id)
					{
						result = false;
					}
				}
			}
			return result;
		}

		public static int ExtractQuantityFromTransaction(global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition, int definitionID)
		{
			return ExtractQuantityFromQuantityItemList(transactionDefinition.Outputs, definitionID);
		}

		public static int ExtractQuantityFromTransaction(global::Kampai.Game.Transaction.TransactionInstance transactionInstance, int definitionID)
		{
			return ExtractQuantityFromQuantityItemList(transactionInstance.Outputs, definitionID);
		}

		private static int ExtractQuantityFromQuantityItemList(global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> itemList, int definitionID)
		{
			int result = 0;
			if (itemList != null)
			{
				foreach (global::Kampai.Util.QuantityItem item in itemList)
				{
					if (item.ID == definitionID)
					{
						result = (int)item.Quantity;
						break;
					}
				}
			}
			return result;
		}
	}
}
