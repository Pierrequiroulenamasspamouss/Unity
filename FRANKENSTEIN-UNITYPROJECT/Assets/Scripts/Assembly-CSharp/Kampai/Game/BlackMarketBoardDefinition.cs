namespace Kampai.Game
{
	public class BlackMarketBoardDefinition : global::Kampai.Game.AnimatingBuildingDefinition, global::Kampai.Game.ZoomableBuildingDefinition
	{
		public global::UnityEngine.Vector3 zoomOffset { get; set; }

		public global::UnityEngine.Vector3 zoomEulers { get; set; }

		public float zoomFOV { get; set; }

		public float TicketRepopTime { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.BlackMarketBoardLevelBandDefinition> LevelBands { get; set; }

		public global::System.Collections.Generic.IList<string> OrderNames { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.BlackMarketBoardUnlockedOrderSlotDefinition> UnlockedIngredientsToOrderSlotsTable { get; set; }

		public int CharacterOrderChance { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "ZOOMOFFSET":
				reader.Read();
				zoomOffset = global::Kampai.Util.ReaderUtil.ReadVector3(reader, converters);
				break;
			case "ZOOMEULERS":
				reader.Read();
				zoomEulers = global::Kampai.Util.ReaderUtil.ReadVector3(reader, converters);
				break;
			case "ZOOMFOV":
				reader.Read();
				zoomFOV = global::System.Convert.ToSingle(reader.Value);
				break;
			case "TICKETREPOPTIME":
				reader.Read();
				TicketRepopTime = global::System.Convert.ToSingle(reader.Value);
				break;
			case "LEVELBANDS":
				reader.Read();
				LevelBands = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.BlackMarketBoardLevelBandDefinition>(reader, converters);
				break;
			case "ORDERNAMES":
				reader.Read();
                    //OrderNames = global::Kampai.Util.ReaderUtil.PopulateList(reader, converters, global::Kampai.Util.ReaderUtil.ReadString);
                    OrderNames = global::Kampai.Util.ReaderUtil.PopulateList<string>(reader, converters, global::Kampai.Util.ReaderUtil.ReadString);
                    break;
			case "UNLOCKEDINGREDIENTSTOORDERSLOTSTABLE":
				reader.Read();
				UnlockedIngredientsToOrderSlotsTable = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.BlackMarketBoardUnlockedOrderSlotDefinition>(reader, converters);
				break;
			case "CHARACTERORDERCHANCE":
				reader.Read();
				CharacterOrderChance = global::System.Convert.ToInt32(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}

		public override global::Kampai.Game.Building BuildBuilding()
		{
			return new global::Kampai.Game.OrderBoard(this);
		}
	}
}
