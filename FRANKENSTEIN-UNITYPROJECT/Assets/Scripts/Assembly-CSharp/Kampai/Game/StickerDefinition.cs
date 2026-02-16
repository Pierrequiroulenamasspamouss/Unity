namespace Kampai.Game
{
	public class StickerDefinition : global::Kampai.Game.DisplayableDefinition
	{
		public int CharacterID { get; set; }

		public bool IsLimitedTime { get; set; }

		public int EventDefinitionID { get; set; }

		public int UnlockLevel { get; set; }

		public int DynamicIngredientDefinitionID { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "CHARACTERID":
				reader.Read();
				CharacterID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "ISLIMITEDTIME":
				reader.Read();
				IsLimitedTime = global::System.Convert.ToBoolean(reader.Value);
				break;
			case "EVENTDEFINITIONID":
				reader.Read();
				EventDefinitionID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "UNLOCKLEVEL":
				reader.Read();
				UnlockLevel = global::System.Convert.ToInt32(reader.Value);
				break;
			case "DYNAMICINGREDIENTDEFINITIONID":
				reader.Read();
				DynamicIngredientDefinitionID = global::System.Convert.ToInt32(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
