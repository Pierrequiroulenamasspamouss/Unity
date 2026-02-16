namespace Kampai.Game
{
	public class StorageBuildingDefinition : global::Kampai.Game.AnimatingBuildingDefinition
	{
		public int Capacity { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.StorageUpgradeDefinition> StorageUpgrades { get; set; }

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
					StorageUpgrades = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.StorageUpgradeDefinition>(reader, converters, global::Kampai.Util.ReaderUtil.ReadStorageUpgradeDefinition);
					break;
				}
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			case "CAPACITY":
				reader.Read();
				Capacity = global::System.Convert.ToInt32(reader.Value);
				break;
			}
			return true;
		}

		public override global::Kampai.Game.Building BuildBuilding()
		{
			return new global::Kampai.Game.StorageBuilding(this);
		}
	}
}
