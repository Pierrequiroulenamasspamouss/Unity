namespace Kampai.Game
{
	public class StuartCharacterDefinition : global::Kampai.Game.NamedCharacterDefinition
	{
		public string StageStateMachine { get; set; }

		public global::Kampai.Game.MinionAnimationDefinition OnStageAnimation { get; set; }

		public global::Kampai.Game.MinionAnimationDefinition CelebrateAnimation { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "STAGESTATEMACHINE":
				reader.Read();
				StageStateMachine = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "ONSTAGEANIMATION":
				reader.Read();
				OnStageAnimation = global::Kampai.Util.FastJSONDeserializer.Deserialize<global::Kampai.Game.MinionAnimationDefinition>(reader, converters);
				break;
			case "CELEBRATEANIMATION":
				reader.Read();
				CelebrateAnimation = global::Kampai.Util.FastJSONDeserializer.Deserialize<global::Kampai.Game.MinionAnimationDefinition>(reader, converters);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}

		public override global::Kampai.Game.Instance Build()
		{
			return new global::Kampai.Game.StuartCharacter(this);
		}
	}
}
