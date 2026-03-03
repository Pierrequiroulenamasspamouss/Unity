namespace Kampai.Game
{
	public class QuestResourceDefinition : global::Kampai.Game.Definition
	{
		public string resourcePath { get; set; }

		public string maskPath { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "MASKPATH":
				reader.Read();
				maskPath = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			case "RESOURCEPATH":
				reader.Read();
				resourcePath = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			}
			return true;
		}
	}
}
