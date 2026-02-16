namespace Kampai.Game
{
	public class WayFinderDefinition : global::Kampai.Game.Definition
	{
		public string NewQuestIcon { get; set; }

		public string QuestCompleteIcon { get; set; }

		public string TaskCompleteIcon { get; set; }

		public string TikibarDefaultIcon { get; set; }

		public float TikibarZoomViewEnabledAt { get; set; }

		public string CabanaDefaultIcon { get; set; }

		public string OrderBoardDefaultIcon { get; set; }

		public string TSMDefaultIcon { get; set; }

		public string StorageBuildingDefaultIcon { get; set; }

		public string BobPointsAtStuffLandExpansionIcon { get; set; }

		public string BobPointsAtStuffDefaultIcon { get; set; }

		public float BobPointsAtStuffYWorldOffset { get; set; }

		public string MarketplaceSoldIcon { get; set; }

		public string DefaultIcon { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "NEWQUESTICON":
				reader.Read();
				NewQuestIcon = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "QUESTCOMPLETEICON":
				reader.Read();
				QuestCompleteIcon = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "TASKCOMPLETEICON":
				reader.Read();
				TaskCompleteIcon = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "TIKIBARDEFAULTICON":
				reader.Read();
				TikibarDefaultIcon = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "TIKIBARZOOMVIEWENABLEDAT":
				reader.Read();
				TikibarZoomViewEnabledAt = global::System.Convert.ToSingle(reader.Value);
				break;
			case "CABANADEFAULTICON":
				reader.Read();
				CabanaDefaultIcon = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "ORDERBOARDDEFAULTICON":
				reader.Read();
				OrderBoardDefaultIcon = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "TSMDEFAULTICON":
				reader.Read();
				TSMDefaultIcon = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "STORAGEBUILDINGDEFAULTICON":
				reader.Read();
				StorageBuildingDefaultIcon = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "BOBPOINTSATSTUFFLANDEXPANSIONICON":
				reader.Read();
				BobPointsAtStuffLandExpansionIcon = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "BOBPOINTSATSTUFFDEFAULTICON":
				reader.Read();
				BobPointsAtStuffDefaultIcon = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "BOBPOINTSATSTUFFYWORLDOFFSET":
				reader.Read();
				BobPointsAtStuffYWorldOffset = global::System.Convert.ToSingle(reader.Value);
				break;
			case "MARKETPLACESOLDICON":
				reader.Read();
				MarketplaceSoldIcon = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "DEFAULTICON":
				reader.Read();
				DefaultIcon = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
