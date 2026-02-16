namespace Kampai.Game
{
	public abstract class RepairableBuildingDefinition : global::Kampai.Game.BuildingDefinition
	{
		public string brokenPrefab { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "BROKENPREFAB":
				reader.Read();
				brokenPrefab = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				return true;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
		}
	}
}
