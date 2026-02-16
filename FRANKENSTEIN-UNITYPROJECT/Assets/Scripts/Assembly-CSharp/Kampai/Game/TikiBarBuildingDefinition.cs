namespace Kampai.Game
{
	public class TikiBarBuildingDefinition : global::Kampai.Game.TaskableBuildingDefinition, global::Kampai.Game.ZoomableBuildingDefinition
	{
		public global::UnityEngine.Vector3 zoomOffset { get; set; }

		public global::UnityEngine.Vector3 zoomEulers { get; set; }

		public float zoomFOV { get; set; }

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
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}

		public override global::Kampai.Game.Building BuildBuilding()
		{
			return new global::Kampai.Game.TikiBarBuilding(this);
		}
	}
}
