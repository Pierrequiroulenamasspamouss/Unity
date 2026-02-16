namespace Kampai.Game
{
	public class LimitedTimeEventDefinition : global::Kampai.Game.DisplayableDefinition
	{
		public int UTCStartTime { get; set; }

		public int UTCEndTime { get; set; }

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
					UTCEndTime = global::System.Convert.ToInt32(reader.Value);
					break;
				}
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			case "UTCSTARTTIME":
				reader.Read();
				UTCStartTime = global::System.Convert.ToInt32(reader.Value);
				break;
			}
			return true;
		}
	}
}
