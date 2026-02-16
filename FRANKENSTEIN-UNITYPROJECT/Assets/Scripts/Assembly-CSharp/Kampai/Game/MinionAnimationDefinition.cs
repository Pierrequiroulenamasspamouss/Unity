namespace Kampai.Game
{
	public class MinionAnimationDefinition : global::Kampai.Game.AnimationDefinition
	{
		public global::System.Collections.Generic.Dictionary<string, object> arguments;

		public string StateMachine { get; set; }

		public bool FaceCamera { get; set; }

		public float AnimationSeconds { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "STATEMACHINE":
				reader.Read();
				StateMachine = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "FACECAMERA":
				reader.Read();
				FaceCamera = global::System.Convert.ToBoolean(reader.Value);
				break;
			case "ANIMATIONSECONDS":
				reader.Read();
				AnimationSeconds = global::System.Convert.ToSingle(reader.Value);
				break;
			case "ARGUMENTS":
				reader.Read();
				arguments = global::Kampai.Util.ReaderUtil.ReadDictionary(reader);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
