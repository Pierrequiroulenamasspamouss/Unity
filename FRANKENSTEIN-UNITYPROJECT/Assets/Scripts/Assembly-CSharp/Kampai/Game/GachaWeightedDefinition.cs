namespace Kampai.Game
{
	public class GachaWeightedDefinition : global::Kampai.Game.Definition
	{
		public int Minions { get; set; }

		public global::Kampai.Game.Transaction.WeightedDefinition WeightedDefinition { get; set; }

		public bool PartyOnly { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "MINIONS":
				reader.Read();
				Minions = global::System.Convert.ToInt32(reader.Value);
				break;
			case "WEIGHTEDDEFINITION":
				reader.Read();
				WeightedDefinition = global::Kampai.Util.FastJSONDeserializer.Deserialize<global::Kampai.Game.Transaction.WeightedDefinition>(reader, converters);
				break;
			case "PARTYONLY":
				reader.Read();
				PartyOnly = global::System.Convert.ToBoolean(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
