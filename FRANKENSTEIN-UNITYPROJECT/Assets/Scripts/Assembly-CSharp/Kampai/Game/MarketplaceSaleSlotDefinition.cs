namespace Kampai.Game
{
	public class MarketplaceSaleSlotDefinition : global::Kampai.Game.Definition
	{
		public enum SlotType
		{
			DEFAULT = 0,
			FACEBOOK_UNLOCKABLE = 1,
			PREMIUM_UNLOCKABLE = 2
		}

		public global::Kampai.Game.MarketplaceSaleSlotDefinition.SlotType type { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "TYPE":
				reader.Read();
				type = global::Kampai.Util.ReaderUtil.ReadEnum<global::Kampai.Game.MarketplaceSaleSlotDefinition.SlotType>(reader);
				return true;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
		}
	}
}
