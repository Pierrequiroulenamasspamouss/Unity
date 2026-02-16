namespace Kampai.Game
{
	public class LandExpansionConfig : global::Kampai.Game.Definition
	{
		public int expansionId { get; set; }

		public global::System.Collections.Generic.IList<int> adjacentExpansionIds { get; set; }

		public global::System.Collections.Generic.IList<int> containedDebris { get; set; }

		public global::System.Collections.Generic.IList<int> containedAspirationalBuildings { get; set; }

		public int transactionId { get; set; }

		public global::Kampai.Game.Location routingSlot { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "EXPANSIONID":
				reader.Read();
				expansionId = global::System.Convert.ToInt32(reader.Value);
				break;
			case "ADJACENTEXPANSIONIDS":
				reader.Read();
				adjacentExpansionIds = global::Kampai.Util.ReaderUtil.PopulateListInt32(reader);
				break;
			case "CONTAINEDDEBRIS":
				reader.Read();
				containedDebris = global::Kampai.Util.ReaderUtil.PopulateListInt32(reader);
				break;
			case "CONTAINEDASPIRATIONALBUILDINGS":
				reader.Read();
				containedAspirationalBuildings = global::Kampai.Util.ReaderUtil.PopulateListInt32(reader);
				break;
			case "TRANSACTIONID":
				reader.Read();
				transactionId = global::System.Convert.ToInt32(reader.Value);
				break;
			case "ROUTINGSLOT":
				reader.Read();
				routingSlot = global::Kampai.Util.ReaderUtil.ReadLocation(reader, converters);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
