namespace Kampai.Game
{
	public class LandExpansionDefinition : global::Kampai.Game.Definition, global::Kampai.Game.Locatable
	{
		public int BuildingDefinitionID { get; set; }

		public int ExpansionID { get; set; }

		public global::Kampai.Game.Location Location { get; set; }

		public bool Grass { get; set; }

		public int MinimumLevel { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "BUILDINGDEFINITIONID":
				reader.Read();
				BuildingDefinitionID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "EXPANSIONID":
				reader.Read();
				ExpansionID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "LOCATION":
				reader.Read();
				Location = global::Kampai.Util.ReaderUtil.ReadLocation(reader, converters);
				break;
			case "GRASS":
				reader.Read();
				Grass = global::System.Convert.ToBoolean(reader.Value);
				break;
			case "MINIMUMLEVEL":
				reader.Read();
				MinimumLevel = global::System.Convert.ToInt32(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
