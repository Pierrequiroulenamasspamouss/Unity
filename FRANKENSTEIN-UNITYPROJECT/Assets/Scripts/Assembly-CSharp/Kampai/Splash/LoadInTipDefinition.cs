namespace Kampai.Splash
{
	public class LoadInTipDefinition : global::Kampai.Game.Definition
	{
		public string Text { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Splash.BucketAssignment> Buckets { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "BUCKETS":
				reader.Read();
				Buckets = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Splash.BucketAssignment>(reader, converters, global::Kampai.Util.ReaderUtil.ReadBucketAssignment);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			case "TEXT":
				reader.Read();
				Text = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			}
			return true;
		}
	}
}
