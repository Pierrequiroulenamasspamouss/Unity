namespace Kampai.Game
{
	public class StoreItemDefinition : global::Kampai.Game.Definition
	{
		public bool OnlyShowIfInInventory;

		public global::Kampai.Game.StoreItemType Type { get; set; }

		public int ReferencedDefID { get; set; }

		public int TransactionID { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "TYPE":
				reader.Read();
				Type = global::Kampai.Util.ReaderUtil.ReadEnum<global::Kampai.Game.StoreItemType>(reader);
				break;
			case "REFERENCEDDEFID":
				reader.Read();
				ReferencedDefID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "TRANSACTIONID":
				reader.Read();
				TransactionID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "ONLYSHOWIFININVENTORY":
				reader.Read();
				OnlyShowIfInInventory = global::System.Convert.ToBoolean(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
