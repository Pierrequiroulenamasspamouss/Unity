namespace Kampai.Game
{
	public class BuildingAnimationDefinition : global::Kampai.Game.AnimationDefinition
	{
		public int CostumeId { get; set; }

		public float GagWeight { get; set; }

		public string BuildingController { get; set; }

		public string MinionController { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "COSTUMEID":
				reader.Read();
				CostumeId = global::System.Convert.ToInt32(reader.Value);
				break;
			case "GAGWEIGHT":
				reader.Read();
				GagWeight = global::System.Convert.ToSingle(reader.Value);
				break;
			case "BUILDINGCONTROLLER":
				reader.Read();
				BuildingController = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "MINIONCONTROLLER":
				reader.Read();
				MinionController = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
