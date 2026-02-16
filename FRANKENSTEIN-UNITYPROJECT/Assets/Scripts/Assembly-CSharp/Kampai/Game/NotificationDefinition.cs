namespace Kampai.Game
{
	public class NotificationDefinition : global::Kampai.Game.Definition
	{
		public string Type { get; set; }

		public int Seconds { get; set; }

		public string Title { get; set; }

		public string Text { get; set; }

		public int Slot { get; set; }

		public string Sound { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "TYPE":
				reader.Read();
				Type = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "SECONDS":
				reader.Read();
				Seconds = global::System.Convert.ToInt32(reader.Value);
				break;
			case "TITLE":
				reader.Read();
				Title = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "TEXT":
				reader.Read();
				Text = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "SLOT":
				reader.Read();
				Slot = global::System.Convert.ToInt32(reader.Value);
				break;
			case "SOUND":
				reader.Read();
				Sound = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
