namespace Kampai.Game
{
	public class MarketplaceRefreshTimerDefinition : global::Kampai.Game.Definition
	{
		public int RefreshTimeSeconds { get; set; }

		public int RushCost { get; set; }

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
					RushCost = global::System.Convert.ToInt32(reader.Value);
					break;
				}
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			case "REFRESHTIMESECONDS":
				reader.Read();
				RefreshTimeSeconds = global::System.Convert.ToInt32(reader.Value);
				break;
			}
			return true;
		}
	}
}
