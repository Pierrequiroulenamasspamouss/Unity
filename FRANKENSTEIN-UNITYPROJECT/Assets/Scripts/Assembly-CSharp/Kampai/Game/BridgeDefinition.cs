namespace Kampai.Game
{
	public class BridgeDefinition : global::Kampai.Game.ItemDefinition
	{
		public global::Kampai.Game.Location location { get; set; }

		public int TransactionId { get; set; }

		public int BuildingId { get; set; }

		public int RepairedBuildingID { get; set; }

		public int LandExpansionID { get; set; }

		public global::Kampai.Game.BridgeCameraOffset cameraPan { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "LOCATION":
				reader.Read();
				location = global::Kampai.Util.ReaderUtil.ReadLocation(reader, converters);
				break;
			case "TRANSACTIONID":
				reader.Read();
				TransactionId = global::System.Convert.ToInt32(reader.Value);
				break;
			case "BUILDINGID":
				reader.Read();
				BuildingId = global::System.Convert.ToInt32(reader.Value);
				break;
			case "REPAIREDBUILDINGID":
				reader.Read();
				RepairedBuildingID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "LANDEXPANSIONID":
				reader.Read();
				LandExpansionID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "CAMERAPAN":
				reader.Read();
				cameraPan = global::Kampai.Util.ReaderUtil.ReadBridgeCameraOffset(reader, converters);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
