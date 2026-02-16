namespace Kampai.Game
{
	public class CommonLandExpansionDefinition : global::Kampai.Game.Definition
	{
		public string MinionPrefab { get; set; }

		public string VFXGrassClearing { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			default:
					{
						int num;
                        num = 1; //added this line to remove use of unassigned variable
                        if (num == 1)
				{
					reader.Read();
					VFXGrassClearing = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
					break;
				}
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			case "MINIONPREFAB":
				reader.Read();
				MinionPrefab = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			}
			return true;
		}
	}
}
