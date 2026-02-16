namespace Kampai.Game
{
	public class DisplayableDefinition : global::Kampai.Game.Definition
	{
		public string Image { get; set; }

		public string Mask { get; set; }

		public string Description { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "IMAGE":
				reader.Read();
				Image = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "MASK":
				reader.Read();
				Mask = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "DESCRIPTION":
				reader.Read();
				Description = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
