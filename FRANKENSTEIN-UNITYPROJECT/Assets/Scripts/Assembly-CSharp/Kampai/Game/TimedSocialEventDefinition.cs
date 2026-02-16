namespace Kampai.Game
{
	public class TimedSocialEventDefinition : global::Kampai.Game.Definition
	{
		public int StartTime { get; set; }

		public int FinishTime { get; set; }

		public int MaxTeamSize { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.SocialEventOrderDefinition> Orders { get; set; }

		public int RewardTransaction { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "STARTTIME":
				reader.Read();
				StartTime = global::System.Convert.ToInt32(reader.Value);
				break;
			case "FINISHTIME":
				reader.Read();
				FinishTime = global::System.Convert.ToInt32(reader.Value);
				break;
			case "MAXTEAMSIZE":
				reader.Read();
				MaxTeamSize = global::System.Convert.ToInt32(reader.Value);
				break;
			case "ORDERS":
				reader.Read();
				Orders = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.SocialEventOrderDefinition>(reader, converters, global::Kampai.Util.ReaderUtil.ReadSocialEventOrderDefinition);
				break;
			case "REWARDTRANSACTION":
				reader.Read();
				RewardTransaction = global::System.Convert.ToInt32(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}

		public virtual global::Kampai.Game.Transaction.TransactionDefinition GetReward(global::Kampai.Game.IDefinitionService definitionService)
		{
			return definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(RewardTransaction);
		}
	}
}
