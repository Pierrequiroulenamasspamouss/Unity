namespace Kampai.Game
{
	public class DynamicQuestDefinition : global::Kampai.Game.QuestDefinition
	{
		public global::Kampai.Game.Transaction.TransactionInstance RewardTransactionInstance { get; set; }

		public int DropStep { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			default:
					{
						int num;
                        num = 1; //added this line to remove use of unassigned variable
                        if (num == 1)
				{
					reader.Read();
					DropStep = global::System.Convert.ToInt32(reader.Value);
					break;
				}
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			case "REWARDTRANSACTIONINSTANCE":
				reader.Read();
				RewardTransactionInstance = global::Kampai.Util.ReaderUtil.ReadTransactionInstance(reader, converters);
				break;
			}
			return true;
		}

		public override global::Kampai.Game.Transaction.TransactionDefinition GetReward(global::Kampai.Game.IDefinitionService definitionService)
		{
			if (RewardTransactionInstance != null)
			{
				return RewardTransactionInstance.ToDefinition();
			}
			return base.GetReward(definitionService);
		}
	}
}
