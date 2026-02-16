namespace Kampai.Game
{
	public class AspirationalBuildingDefinition : global::Kampai.Game.Definition, global::Kampai.Game.Locatable
	{
		public int BuildingDefinitionID { get; set; }

		public global::Kampai.Game.Location Location { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
                			case "BUILDINGDEFINITIONID":
				reader.Read();
				BuildingDefinitionID = global::System.Convert.ToInt32(reader.Value);
				break;

                case "LOCATION":
                reader.Read(); // Move to StartObject or null
                if (reader.TokenType == global::Newtonsoft.Json.JsonToken.StartObject)
                {
                    Location = global::Kampai.Util.ReaderUtil.ReadLocation(reader, converters);
                }
                else
                {
                    global::UnityEngine.Debug.LogWarning("AspirationalBuildingDefinition: Expected StartObject for LOCATION, got " + reader.TokenType);
                }
                break;
            default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
