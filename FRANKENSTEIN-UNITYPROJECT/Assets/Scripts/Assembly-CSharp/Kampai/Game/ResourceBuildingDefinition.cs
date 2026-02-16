namespace Kampai.Game
{
	public class ResourceBuildingDefinition : global::Kampai.Game.TaskableBuildingDefinition
	{
		public int ItemId { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "ITEMID":
				reader.Read();
				ItemId = global::System.Convert.ToInt32(reader.Value);
				return true;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
		}

		public override global::Kampai.Game.Building BuildBuilding()
		{
			return new global::Kampai.Game.ResourceBuilding(this);
		}
	}
}
