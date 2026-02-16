namespace Kampai.Game
{
	public class BridgeBuildingDefinition : global::Kampai.Game.AnimatingBuildingDefinition
	{
		public string AspirationalMessage { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "ASPIRATIONALMESSAGE":
				reader.Read();
				AspirationalMessage = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				return true;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
		}

		public override global::Kampai.Game.Building BuildBuilding()
		{
			return new global::Kampai.Game.BridgeBuilding(this);
		}
	}
}
