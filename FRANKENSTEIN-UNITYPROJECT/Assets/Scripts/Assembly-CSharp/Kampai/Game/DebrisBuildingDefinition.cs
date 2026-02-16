namespace Kampai.Game
{
	public class DebrisBuildingDefinition : global::Kampai.Game.TaskableBuildingDefinition
	{
		public float ClearTime { get; set; }

		public int TransactionID { get; set; }

		public global::System.Collections.Generic.IList<string> VFXPrefabs { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "CLEARTIME":
				reader.Read();
				ClearTime = global::System.Convert.ToSingle(reader.Value);
				break;
			case "TRANSACTIONID":
				reader.Read();
				TransactionID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "VFXPREFABS":
				reader.Read();
				VFXPrefabs = global::Kampai.Util.ReaderUtil.PopulateList<string>(reader, converters, global::Kampai.Util.ReaderUtil.ReadString);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}

		public override global::Kampai.Game.Building BuildBuilding()
		{
			return new global::Kampai.Game.DebrisBuilding(this);
		}
	}
}
