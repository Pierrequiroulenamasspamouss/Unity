namespace Kampai.Game
{
	public class NamedCharacterAnimationDefinition : global::Kampai.Game.AnimationDefinition
	{
		public string StateMachine { get; set; }

		public float SpreadMin { get; set; }

		public float SpreadMax { get; set; }

		public int IdleCount { get; set; }

		public float AttentionDuration { get; set; }

		public global::Kampai.Game.CharacterUIAnimationDefinition characterUIAnimationDefinition { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "STATEMACHINE":
				reader.Read();
				StateMachine = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "SPREADMIN":
				reader.Read();
				SpreadMin = global::System.Convert.ToSingle(reader.Value);
				break;
			case "SPREADMAX":
				reader.Read();
				SpreadMax = global::System.Convert.ToSingle(reader.Value);
				break;
			case "IDLECOUNT":
				reader.Read();
				IdleCount = global::System.Convert.ToInt32(reader.Value);
				break;
			case "ATTENTIONDURATION":
				reader.Read();
				AttentionDuration = global::System.Convert.ToSingle(reader.Value);
				break;
			case "CHARACTERUIANIMATIONDEFINITION":
				reader.Read();
				characterUIAnimationDefinition = global::Kampai.Util.ReaderUtil.ReadCharacterUIAnimationDefinition(reader, converters);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
