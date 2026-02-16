namespace Kampai.Game
{
	public class TaxonomyDefinition : global::Kampai.Game.DisplayableDefinition
	{
		public string TaxonomyHighLevel { get; set; }

		public string TaxonomySpecific { get; set; }

		public string TaxonomyType { get; set; }

		public string TaxonomyOther { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "TAXONOMYHIGHLEVEL":
				reader.Read();
				TaxonomyHighLevel = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "TAXONOMYSPECIFIC":
				reader.Read();
				TaxonomySpecific = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "TAXONOMYTYPE":
				reader.Read();
				TaxonomyType = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "TAXONOMYOTHER":
				reader.Read();
				TaxonomyOther = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
