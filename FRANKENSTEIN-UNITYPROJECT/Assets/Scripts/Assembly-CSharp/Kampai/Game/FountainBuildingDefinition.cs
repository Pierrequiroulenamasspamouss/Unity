namespace Kampai.Game
{
	public class FountainBuildingDefinition : global::Kampai.Game.AnimatingBuildingDefinition
	{
		public string AspirationalMessage { get; set; }

		public string AspirationalMessageLevelReached { get; set; }

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
					AspirationalMessageLevelReached = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
					break;
				}
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			case "ASPIRATIONALMESSAGE":
				reader.Read();
				AspirationalMessage = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			}
			return true;
		}

		public override global::Kampai.Game.Building BuildBuilding()
		{
			return new global::Kampai.Game.FountainBuilding(this);
		}
	}
}
