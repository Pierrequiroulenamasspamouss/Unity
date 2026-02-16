namespace Kampai.Game
{
	public class LocalizedTextDefinition : global::Kampai.Game.Definition
	{
		public string Language { get; set; }

		public string Translation { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			default:
			{
				int num;
                        num = 0; //added this line to remove use of unassigned variable
                        if (num == 1)
				{
					reader.Read();
					Translation = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
					break;
				}
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			case "LANGUAGE":
				reader.Read();
				Language = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			}
			return true;
		}
	}
}
