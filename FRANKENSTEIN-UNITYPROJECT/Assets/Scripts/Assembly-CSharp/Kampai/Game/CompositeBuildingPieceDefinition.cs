namespace Kampai.Game
{
	public class CompositeBuildingPieceDefinition : global::Kampai.Game.DisplayableDefinition
	{
		public string PrefabName { get; set; }

		public int BuildingDefinitionID { get; set; }

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
					BuildingDefinitionID = global::System.Convert.ToInt32(reader.Value);
					break;
				}
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			case "PREFABNAME":
				reader.Read();
				PrefabName = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			}
			return true;
		}
	}
}
