namespace Kampai.Game
{
	public class LevelXPTable : global::Kampai.Game.Definition
	{
		public global::System.Collections.Generic.IList<int> xpNeededList { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "XPNEEDEDLIST":
				reader.Read();
				xpNeededList = global::Kampai.Util.ReaderUtil.PopulateListInt32(reader);
				return true;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
		}
	}
}
