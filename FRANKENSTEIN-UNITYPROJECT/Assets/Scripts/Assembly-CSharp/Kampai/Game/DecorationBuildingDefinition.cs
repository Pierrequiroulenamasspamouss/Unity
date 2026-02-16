namespace Kampai.Game
{
	public class DecorationBuildingDefinition : global::Kampai.Game.BuildingDefinition
	{
		public int Cost { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "COST":
				reader.Read();
				Cost = global::System.Convert.ToInt32(reader.Value);
				return true;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
		}

		public override global::Kampai.Game.Building BuildBuilding()
		{
			return new global::Kampai.Game.DecorationBuilding(this);
		}
	}
}
