namespace Kampai.Game
{
	public class DefinitionGroup : global::Kampai.Game.Definition
	{
		public global::System.Collections.Generic.IList<int> Group { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "GROUP":
				reader.Read();
				Group = global::Kampai.Util.ReaderUtil.PopulateListInt32(reader);
				return true;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
		}
	}
}
