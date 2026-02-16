namespace Kampai.Game
{
	public class LimitedQuestDefinition : global::Kampai.Game.QuestDefinition
	{
		public int ServerStartTimeUTC { get; set; }

		public int ServerStopTimeUTC { get; set; }

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
					ServerStopTimeUTC = global::System.Convert.ToInt32(reader.Value);
					break;
				}
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			case "SERVERSTARTTIMEUTC":
				reader.Read();
				ServerStartTimeUTC = global::System.Convert.ToInt32(reader.Value);
				break;
			}
			return true;
		}
	}
}
