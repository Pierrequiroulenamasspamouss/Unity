namespace Kampai.Game
{
	public class DebrisDefinition : global::Kampai.Game.Definition, global::Kampai.Game.Locatable
	{
		public int BuildingDefinitionID { get; set; }

		public global::Kampai.Game.Location Location { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "LOCATION":
				reader.Read();
				Location = global::Kampai.Util.ReaderUtil.ReadLocation(reader, converters);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			case "BUILDINGDEFINITIONID":
				reader.Read();
				BuildingDefinitionID = global::System.Convert.ToInt32(reader.Value);
				break;
			}
			return true;
		}
	}
}
