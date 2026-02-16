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
			default:
					{
						int num;
                        num = 1; //added this line to remove use of unassigned variable
                        if (num == 1)
				{
					reader.Read();
					Location = global::Kampai.Util.ReaderUtil.ReadLocation(reader, converters);
					break;
				}
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			case "BUILDINGDEFINITIONID":
				reader.Read();
				BuildingDefinitionID = global::System.Convert.ToInt32(reader.Value);
				break;
			}
			return true;
		}
	}
}
