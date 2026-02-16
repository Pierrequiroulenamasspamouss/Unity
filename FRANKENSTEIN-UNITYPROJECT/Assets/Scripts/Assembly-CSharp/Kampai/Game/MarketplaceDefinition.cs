namespace Kampai.Game
{
	public class MarketplaceDefinition : global::Kampai.Game.Definition
	{
		public global::System.Collections.Generic.IList<global::Kampai.Game.MarketplaceItemDefinition> itemDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.MarketplaceSaleSlotDefinition> slotDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::UnityEngine.Vector3> buyTimeSpline { get; set; }

		public global::Kampai.Game.MarketplaceRefreshTimerDefinition refreshTimerDefinition { get; set; }

		public int MaxSellQuantity { get; set; }

		public int MaxDropQuantity { get; set; }

		public float VariabilityBuyTimePercent { get; set; }

		public int CraftableWeight { get; set; }

		public int BaseResourceWeight { get; set; }

		public int DropWeight { get; set; }

		public int TotalSaleAds { get; set; }

		public int TotalBuyAds { get; set; }

		public int StartingBuyAds { get; set; }

		public int StandardSlots { get; set; }

		public int FacebookSlots { get; set; }

		public int MaxPremiumSlots { get; set; }

		public int PremiumInitialCost { get; set; }

		public int PremiumIncrementCost { get; set; }

		public int DeleteSaleCost { get; set; }

		public int LevelGate { get; set; }

		public bool KillSwitchTrigger { get; set; }

		public int SaleCancellationCost { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "ITEMDEFINITIONS":
				reader.Read();
				itemDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.MarketplaceItemDefinition>(reader, converters);
				break;
			case "SLOTDEFINITIONS":
				reader.Read();
				slotDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.MarketplaceSaleSlotDefinition>(reader, converters);
				break;
			case "BUYTIMESPLINE":
				reader.Read();
				buyTimeSpline = global::Kampai.Util.ReaderUtil.PopulateList<global::UnityEngine.Vector3>(reader, converters, global::Kampai.Util.ReaderUtil.ReadVector3);
				break;
			case "REFRESHTIMERDEFINITION":
				reader.Read();
				refreshTimerDefinition = global::Kampai.Util.FastJSONDeserializer.Deserialize<global::Kampai.Game.MarketplaceRefreshTimerDefinition>(reader, converters);
				break;
			case "MAXSELLQUANTITY":
				reader.Read();
				MaxSellQuantity = global::System.Convert.ToInt32(reader.Value);
				break;
			case "MAXDROPQUANTITY":
				reader.Read();
				MaxDropQuantity = global::System.Convert.ToInt32(reader.Value);
				break;
			case "VARIABILITYBUYTIMEPERCENT":
				reader.Read();
				VariabilityBuyTimePercent = global::System.Convert.ToSingle(reader.Value);
				break;
			case "CRAFTABLEWEIGHT":
				reader.Read();
				CraftableWeight = global::System.Convert.ToInt32(reader.Value);
				break;
			case "BASERESOURCEWEIGHT":
				reader.Read();
				BaseResourceWeight = global::System.Convert.ToInt32(reader.Value);
				break;
			case "DROPWEIGHT":
				reader.Read();
				DropWeight = global::System.Convert.ToInt32(reader.Value);
				break;
			case "TOTALSALEADS":
				reader.Read();
				TotalSaleAds = global::System.Convert.ToInt32(reader.Value);
				break;
			case "TOTALBUYADS":
				reader.Read();
				TotalBuyAds = global::System.Convert.ToInt32(reader.Value);
				break;
			case "STARTINGBUYADS":
				reader.Read();
				StartingBuyAds = global::System.Convert.ToInt32(reader.Value);
				break;
			case "STANDARDSLOTS":
				reader.Read();
				StandardSlots = global::System.Convert.ToInt32(reader.Value);
				break;
			case "FACEBOOKSLOTS":
				reader.Read();
				FacebookSlots = global::System.Convert.ToInt32(reader.Value);
				break;
			case "MAXPREMIUMSLOTS":
				reader.Read();
				MaxPremiumSlots = global::System.Convert.ToInt32(reader.Value);
				break;
			case "PREMIUMINITIALCOST":
				reader.Read();
				PremiumInitialCost = global::System.Convert.ToInt32(reader.Value);
				break;
			case "PREMIUMINCREMENTCOST":
				reader.Read();
				PremiumIncrementCost = global::System.Convert.ToInt32(reader.Value);
				break;
			case "DELETESALECOST":
				reader.Read();
				DeleteSaleCost = global::System.Convert.ToInt32(reader.Value);
				break;
			case "LEVELGATE":
				reader.Read();
				LevelGate = global::System.Convert.ToInt32(reader.Value);
				break;
			case "KILLSWITCHTRIGGER":
				reader.Read();
				KillSwitchTrigger = global::System.Convert.ToBoolean(reader.Value);
				break;
			case "SALECANCELLATIONCOST":
				reader.Read();
				SaleCancellationCost = global::System.Convert.ToInt32(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
