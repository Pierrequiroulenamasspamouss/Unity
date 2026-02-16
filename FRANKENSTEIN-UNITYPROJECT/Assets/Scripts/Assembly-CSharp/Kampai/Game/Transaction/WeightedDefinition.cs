namespace Kampai.Game.Transaction
{
	public class WeightedDefinition : global::Kampai.Game.Definition
	{
		public global::System.Collections.Generic.IList<global::Kampai.Game.Transaction.WeightedQuantityItem> Entities { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "ENTITIES":
				reader.Read();
				Entities = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.Transaction.WeightedQuantityItem>(reader, converters);
				return true;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
		}
	}
}
