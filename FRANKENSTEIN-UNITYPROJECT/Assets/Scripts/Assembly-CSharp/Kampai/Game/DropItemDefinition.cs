namespace Kampai.Game
{
	public class DropItemDefinition : global::Kampai.Game.ItemDefinition
	{
		public float Rarity { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "RARITY":
				reader.Read();
				Rarity = global::System.Convert.ToSingle(reader.Value);
				return true;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
		}
	}
}
