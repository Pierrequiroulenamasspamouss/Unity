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
			default:
					{
						int num;
                        num = 1; //added this line to remove use of unassigned variable
                        if (num == 1)
				{
					reader.Read();
					maskPath = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
					break;
				}
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			case "RESOURCEPATH":
				reader.Read();
				resourcePath = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			}
			return true;
		}
	}
}
