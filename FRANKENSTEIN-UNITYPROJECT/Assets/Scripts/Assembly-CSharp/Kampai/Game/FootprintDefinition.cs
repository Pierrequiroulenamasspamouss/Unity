namespace Kampai.Game
{
	public class FootprintDefinition : global::Kampai.Game.Definition
	{
		public string Footprint { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "FOOTPRINT":
				reader.Read();
				Footprint = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				return true;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
		}
	}
}
