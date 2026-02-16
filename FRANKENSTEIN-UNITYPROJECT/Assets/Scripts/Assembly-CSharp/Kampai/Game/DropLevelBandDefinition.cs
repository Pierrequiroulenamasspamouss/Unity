namespace Kampai.Game
{
	public class DropLevelBandDefinition : global::Kampai.Game.Definition
	{
		public global::System.Collections.Generic.List<int> HarvestsPerDrop { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "HARVESTSPERDROP":
				reader.Read();
				HarvestsPerDrop = global::Kampai.Util.ReaderUtil.PopulateListInt32(reader);
				return true;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
		}
	}
}
