namespace Kampai.Game
{
	public class BlackMarketBoardUnlockedOrderSlotDefinition : global::Kampai.Game.Definition
	{
		public int UnlockItems { get; set; }

		public int OrderSlots { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "ORDERSLOTS":
				reader.Read();
				OrderSlots = global::System.Convert.ToInt32(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			case "UNLOCKITEMS":
				reader.Read();
				UnlockItems = global::System.Convert.ToInt32(reader.Value);
				break;
			}
			return true;
		}
	}
}
