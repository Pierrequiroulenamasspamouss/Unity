namespace Kampai.Game
{
	public class UnlockDefinition : global::Kampai.Game.ItemDefinition
	{
		public int ReferencedDefinitionID;

		public int UnlockedQuantity;

		public bool Delta;

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "REFERENCEDDEFINITIONID":
				reader.Read();
				ReferencedDefinitionID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "UNLOCKEDQUANTITY":
				reader.Read();
				UnlockedQuantity = global::System.Convert.ToInt32(reader.Value);
				break;
			case "DELTA":
				reader.Read();
				Delta = global::System.Convert.ToBoolean(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
