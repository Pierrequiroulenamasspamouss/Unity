namespace Kampai.Game
{
	public class StageBuildingDefinition : global::Kampai.Game.AnimatingBuildingDefinition, global::Kampai.Game.ZoomableBuildingDefinition
	{
		public global::UnityEngine.Vector3 zoomOffset { get; set; }

		public global::UnityEngine.Vector3 zoomEulers { get; set; }

		public float zoomFOV { get; set; }

		public string backdropPrefabName { get; set; }

		public int temporaryMinionNum { get; set; }

		public float temporaryMinionsOffset { get; set; }

		public string temporaryMinionASM { get; set; }

		public string AspirationalMessage { get; set; }

		public string AspirationalMessageSocialEvent { get; set; }

		public int SocialEventMinimumLevel { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "ZOOMOFFSET":
				reader.Read();
				zoomOffset = global::Kampai.Util.ReaderUtil.ReadVector3(reader, converters);
				break;
			case "ZOOMEULERS":
				reader.Read();
				zoomEulers = global::Kampai.Util.ReaderUtil.ReadVector3(reader, converters);
				break;
			case "ZOOMFOV":
				reader.Read();
				zoomFOV = global::System.Convert.ToSingle(reader.Value);
				break;
			case "BACKDROPPREFABNAME":
				reader.Read();
				backdropPrefabName = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "TEMPORARYMINIONNUM":
				reader.Read();
				temporaryMinionNum = global::System.Convert.ToInt32(reader.Value);
				break;
			case "TEMPORARYMINIONSOFFSET":
				reader.Read();
				temporaryMinionsOffset = global::System.Convert.ToSingle(reader.Value);
				break;
			case "TEMPORARYMINIONASM":
				reader.Read();
				temporaryMinionASM = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "ASPIRATIONALMESSAGE":
				reader.Read();
				AspirationalMessage = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "ASPIRATIONALMESSAGESOCIALEVENT":
				reader.Read();
				AspirationalMessageSocialEvent = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "SOCIALEVENTMINIMUMLEVEL":
				reader.Read();
				SocialEventMinimumLevel = global::System.Convert.ToInt32(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}

		public override global::Kampai.Game.Building BuildBuilding()
		{
			return new global::Kampai.Game.StageBuilding(this);
		}
	}
}
