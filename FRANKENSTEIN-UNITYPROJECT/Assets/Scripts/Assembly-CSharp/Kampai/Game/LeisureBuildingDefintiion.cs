namespace Kampai.Game
{
	public class LeisureBuildingDefintiion : global::Kampai.Game.AnimatingBuildingDefinition
	{
		public int LeisureTimeDuration { get; set; }

		public float ProximityRadius { get; set; }

		public int ProximityPriority { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "LEISURETIMEDURATION":
				reader.Read();
				LeisureTimeDuration = global::System.Convert.ToInt32(reader.Value);
				break;
			case "PROXIMITYRADIUS":
				reader.Read();
				ProximityRadius = global::System.Convert.ToSingle(reader.Value);
				break;
			case "PROXIMITYPRIORITY":
				reader.Read();
				ProximityPriority = global::System.Convert.ToInt32(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}

		public override global::Kampai.Game.Building BuildBuilding()
		{
			return new global::Kampai.Game.LeisureBuilding(this);
		}
	}
}
