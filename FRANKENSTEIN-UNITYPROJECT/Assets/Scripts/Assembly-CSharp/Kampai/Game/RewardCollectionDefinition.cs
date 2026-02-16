namespace Kampai.Game
{
	public class RewardCollectionDefinition : global::Kampai.Game.Definition
	{
		public global::System.Collections.Generic.IList<global::Kampai.Game.CollectionReward> Rewards { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "REWARDS":
				reader.Read();
				Rewards = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.CollectionReward>(reader, converters, global::Kampai.Util.ReaderUtil.ReadCollectionReward);
				return true;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
		}
	}
}
