namespace Kampai.Game
{
	public class CompositeBuildingPieceDefinition : global::Kampai.Game.DisplayableDefinition
	{
		public string PrefabName { get; set; }

		public int BuildingDefinitionID { get; set; }

        protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
        {
            switch (propertyName)
            {
                case "BUILDINGDEFINITIONID": // On lui donne son vrai nom
                    reader.Read();
                    BuildingDefinitionID = global::Kampai.Util.ReaderUtil.SafeParseInt(reader.Value);
                    break;

                case "PREFABNAME":
                    reader.Read();
                    PrefabName = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
                    break;

                default:
                    // Ici, on laisse la base (Definition.cs) gérer (ID, LocalizedKey, etc.)
                    // ou skipper si c'est inconnu (comme "LOCATION")
                    return base.DeserializeProperty(propertyName, reader, converters);
            }
            return true;
        }
    }
}
