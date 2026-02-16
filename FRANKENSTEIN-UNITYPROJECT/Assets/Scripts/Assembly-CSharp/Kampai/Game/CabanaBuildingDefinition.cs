namespace Kampai.Game
{
	public class CabanaBuildingDefinition : global::Kampai.Game.AnimatingBuildingDefinition
	{
		public int CharacterID { get; set; }

		public string InactivePrefab { get; set; }

		public string DisheveledScene { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "CHARACTERID":
				reader.Read();
				CharacterID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "INACTIVEPREFAB":
				reader.Read();
				InactivePrefab = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "DISHEVELEDSCENE":
				reader.Read();
				DisheveledScene = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}

		public override global::Kampai.Game.Building BuildBuilding()
		{
			return new global::Kampai.Game.CabanaBuilding(this);
		}
	}
}
