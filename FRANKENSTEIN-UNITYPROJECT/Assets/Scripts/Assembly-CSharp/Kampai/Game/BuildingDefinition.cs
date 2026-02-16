namespace Kampai.Game
{
	[global::Kampai.Util.RequiresJsonConverter]
	public abstract class BuildingDefinition : global::Kampai.Game.TaxonomyDefinition, global::Kampai.Util.IBuilder<global::Kampai.Game.Instance>
	{
		public BuildingType.BuildingTypeIdentifier Type { get; set; }

		public int FootprintID { get; set; }

		public global::Kampai.Game.CameraOffset ModalOffset { get; set; }

		public global::Kampai.Game.CameraOffset CenterCameraOffset { get; set; }

		public int ConstructionTime { get; set; }

		public bool Movable { get; set; }

		public int RewardTransactionId { get; set; }

		public virtual string Prefab { get; set; }

		public string ScaffoldingPrefab { get; set; }

		public string RibbonPrefab { get; set; }

		public string PlatformPrefab { get; set; }

		public bool Storable { get; set; }

		public global::UnityEngine.Vector3 QuestIconOffset { get; set; }

		public string MenuPrefab { get; set; }

		public int IncrementalCost { get; set; }

		public bool RouteToSlot { get; set; }

		public int WorkStations { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "TYPE":
				reader.Read();
				Type = global::Kampai.Util.ReaderUtil.ReadEnum<BuildingType.BuildingTypeIdentifier>(reader);
				break;
			case "FOOTPRINTID":
				reader.Read();
				FootprintID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "MODALOFFSET":
				reader.Read();
				ModalOffset = global::Kampai.Util.ReaderUtil.ReadCameraOffset(reader, converters);
				break;
			case "CENTERCAMERAOFFSET":
				reader.Read();
				CenterCameraOffset = global::Kampai.Util.ReaderUtil.ReadCameraOffset(reader, converters);
				break;
			case "CONSTRUCTIONTIME":
				reader.Read();
				ConstructionTime = global::System.Convert.ToInt32(reader.Value);
				break;
			case "MOVABLE":
				reader.Read();
				Movable = global::System.Convert.ToBoolean(reader.Value);
				break;
			case "REWARDTRANSACTIONID":
				reader.Read();
				RewardTransactionId = global::System.Convert.ToInt32(reader.Value);
				break;
			case "PREFAB":
				reader.Read();
				Prefab = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "SCAFFOLDINGPREFAB":
				reader.Read();
				ScaffoldingPrefab = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "RIBBONPREFAB":
				reader.Read();
				RibbonPrefab = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "PLATFORMPREFAB":
				reader.Read();
				PlatformPrefab = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "STORABLE":
				reader.Read();
				Storable = global::System.Convert.ToBoolean(reader.Value);
				break;
			case "QUESTICONOFFSET":
				reader.Read();
				QuestIconOffset = global::Kampai.Util.ReaderUtil.ReadVector3(reader, converters);
				break;
			case "MENUPREFAB":
				reader.Read();
				MenuPrefab = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "INCREMENTALCOST":
				reader.Read();
				IncrementalCost = global::System.Convert.ToInt32(reader.Value);
				break;
			case "ROUTETOSLOT":
				reader.Read();
				RouteToSlot = global::System.Convert.ToBoolean(reader.Value);
				break;
			case "WORKSTATIONS":
				reader.Read();
				WorkStations = global::System.Convert.ToInt32(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}

		public abstract global::Kampai.Game.Building BuildBuilding();

		public global::Kampai.Game.Instance Build()
		{
			return BuildBuilding();
		}
	}
}
