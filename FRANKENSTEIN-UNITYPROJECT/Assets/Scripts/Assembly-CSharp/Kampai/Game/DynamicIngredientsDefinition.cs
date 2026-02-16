namespace Kampai.Game
{
	public class DynamicIngredientsDefinition : global::Kampai.Game.IngredientsItemDefinition
	{
		public int QuestDefinitionUnlockId { get; set; }

		public int MaxCraftable { get; set; }

		public int CraftingBuildingId { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "QUESTDEFINITIONUNLOCKID":
				reader.Read();
				QuestDefinitionUnlockId = global::System.Convert.ToInt32(reader.Value);
				break;
			case "MAXCRAFTABLE":
				reader.Read();
				MaxCraftable = global::System.Convert.ToInt32(reader.Value);
				break;
			case "CRAFTINGBUILDINGID":
				reader.Read();
				CraftingBuildingId = global::System.Convert.ToInt32(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
