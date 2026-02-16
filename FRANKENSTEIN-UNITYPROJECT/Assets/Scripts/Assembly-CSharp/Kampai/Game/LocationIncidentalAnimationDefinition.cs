namespace Kampai.Game
{
	public class LocationIncidentalAnimationDefinition : global::Kampai.Game.Definition
	{
		public int AnimationId { get; set; }

		public global::Kampai.Game.FloatLocation Location { get; set; }

		public global::Kampai.Game.Angle Rotation { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "ANIMATIONID":
				reader.Read();
				AnimationId = global::System.Convert.ToInt32(reader.Value);
				break;
			case "LOCATION":
				reader.Read();
				Location = global::Kampai.Util.ReaderUtil.ReadFloatLocation(reader, converters);
				break;
			case "ROTATION":
				reader.Read();
				Rotation = global::Kampai.Util.ReaderUtil.ReadAngle(reader, converters);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
