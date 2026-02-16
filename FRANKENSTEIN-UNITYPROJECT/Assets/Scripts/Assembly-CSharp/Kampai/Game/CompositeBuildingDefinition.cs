namespace Kampai.Game
{
	public class CompositeBuildingDefinition : global::Kampai.Game.BuildingDefinition
	{
		public int MaxPieces { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "MAXPIECES":
				reader.Read();
				MaxPieces = global::System.Convert.ToInt32(reader.Value);
				return true;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
		}

		public override global::Kampai.Game.Building BuildBuilding()
		{
			return new global::Kampai.Game.CompositeBuilding(this);
		}
	}
}
