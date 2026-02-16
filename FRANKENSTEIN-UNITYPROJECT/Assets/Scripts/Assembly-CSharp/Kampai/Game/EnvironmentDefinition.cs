namespace Kampai.Game
{
	public class EnvironmentDefinition : global::Kampai.Game.Definition
	{
		[global::Kampai.Util.FastDeserializerIgnore]
		public global::Kampai.Game.EnvironmentGridSquareDefinition[,] DefinitionGrid;

		public global::Kampai.Game.PartyDefinition PartyDefinition;

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "PARTYDEFINITION":
				reader.Read();
				PartyDefinition = global::Kampai.Util.ReaderUtil.ReadPartyDefinition(reader, converters);
				return true;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
		}

		public bool IsUsable(int x, int z)
		{
			return DefinitionGrid[x, z].Usable;
		}

		public bool IsUsable(global::Kampai.Game.Location location)
		{
			return IsUsable(location.x, location.y);
		}

		public bool IsWater(int x, int z)
		{
			return DefinitionGrid[x, z].Water;
		}

		public bool IsWater(global::Kampai.Game.Location location)
		{
			return IsWater(location.x, location.y);
		}
	}
}
