namespace Kampai.Game
{
	public class KevinCharacterDefinition : global::Kampai.Game.NamedCharacterDefinition
	{
		public global::Kampai.Game.Transaction.WeightedDefinition WanderWeightedDeck { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.LocationIncidentalAnimationDefinition> WanderAnimations { get; set; }

		public string WelcomeHutStateMachine { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "WANDERWEIGHTEDDECK":
				reader.Read();
				WanderWeightedDeck = global::Kampai.Util.FastJSONDeserializer.Deserialize<global::Kampai.Game.Transaction.WeightedDefinition>(reader, converters);
				break;
			case "WANDERANIMATIONS":
				reader.Read();
				WanderAnimations = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.LocationIncidentalAnimationDefinition>(reader, converters);
				break;
			case "WELCOMEHUTSTATEMACHINE":
				reader.Read();
				WelcomeHutStateMachine = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}

		public override global::Kampai.Game.Instance Build()
		{
			return new global::Kampai.Game.KevinCharacter(this);
		}
	}
}
