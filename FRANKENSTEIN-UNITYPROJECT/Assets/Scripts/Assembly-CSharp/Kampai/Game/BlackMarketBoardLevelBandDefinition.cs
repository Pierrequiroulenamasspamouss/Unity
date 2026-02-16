namespace Kampai.Game
{
	public class BlackMarketBoardLevelBandDefinition : global::Kampai.Game.Definition
	{
		public int Level { get; set; }

		public int ResurfaceTime { get; set; }

		public int MaxUniqueResources { get; set; }

		public int MinInvestmentTime { get; set; }

		public int MaxInvestmentTime { get; set; }

		public int MinGrindReward { get; set; }

		public int MaxGrindReward { get; set; }

		public int MinXPReward { get; set; }

		public int MaxXPReward { get; set; }

		public int MinIngredientQty { get; set; }

		public int MaxIngredientQty { get; set; }

		public float GrindMultipler { get; set; }

		public float XPMultipler { get; set; }

		public int InvestmentWeight { get; set; }

		public int XPWeight { get; set; }

		public int GrindWeight { get; set; }

		public int QtyWeight { get; set; }

		public int EasyWeight { get; set; }

		public int MediumWeight { get; set; }

		public int HardWeight { get; set; }

		public float EasyMultiplier { get; set; }

		public float MediumMultiplier { get; set; }

		public float HardMultiplier { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "LEVEL":
				reader.Read();
				Level = global::System.Convert.ToInt32(reader.Value);
				break;
			case "RESURFACETIME":
				reader.Read();
				ResurfaceTime = global::System.Convert.ToInt32(reader.Value);
				break;
			case "MAXUNIQUERESOURCES":
				reader.Read();
				MaxUniqueResources = global::System.Convert.ToInt32(reader.Value);
				break;
			case "MININVESTMENTTIME":
				reader.Read();
				MinInvestmentTime = global::System.Convert.ToInt32(reader.Value);
				break;
			case "MAXINVESTMENTTIME":
				reader.Read();
				MaxInvestmentTime = global::System.Convert.ToInt32(reader.Value);
				break;
			case "MINGRINDREWARD":
				reader.Read();
				MinGrindReward = global::System.Convert.ToInt32(reader.Value);
				break;
			case "MAXGRINDREWARD":
				reader.Read();
				MaxGrindReward = global::System.Convert.ToInt32(reader.Value);
				break;
			case "MINXPREWARD":
				reader.Read();
				MinXPReward = global::System.Convert.ToInt32(reader.Value);
				break;
			case "MAXXPREWARD":
				reader.Read();
				MaxXPReward = global::System.Convert.ToInt32(reader.Value);
				break;
			case "MININGREDIENTQTY":
				reader.Read();
				MinIngredientQty = global::System.Convert.ToInt32(reader.Value);
				break;
			case "MAXINGREDIENTQTY":
				reader.Read();
				MaxIngredientQty = global::System.Convert.ToInt32(reader.Value);
				break;
			case "GRINDMULTIPLER":
				reader.Read();
				GrindMultipler = global::System.Convert.ToSingle(reader.Value);
				break;
			case "XPMULTIPLER":
				reader.Read();
				XPMultipler = global::System.Convert.ToSingle(reader.Value);
				break;
			case "INVESTMENTWEIGHT":
				reader.Read();
				InvestmentWeight = global::System.Convert.ToInt32(reader.Value);
				break;
			case "XPWEIGHT":
				reader.Read();
				XPWeight = global::System.Convert.ToInt32(reader.Value);
				break;
			case "GRINDWEIGHT":
				reader.Read();
				GrindWeight = global::System.Convert.ToInt32(reader.Value);
				break;
			case "QTYWEIGHT":
				reader.Read();
				QtyWeight = global::System.Convert.ToInt32(reader.Value);
				break;
			case "EASYWEIGHT":
				reader.Read();
				EasyWeight = global::System.Convert.ToInt32(reader.Value);
				break;
			case "MEDIUMWEIGHT":
				reader.Read();
				MediumWeight = global::System.Convert.ToInt32(reader.Value);
				break;
			case "HARDWEIGHT":
				reader.Read();
				HardWeight = global::System.Convert.ToInt32(reader.Value);
				break;
			case "EASYMULTIPLIER":
				reader.Read();
				EasyMultiplier = global::System.Convert.ToSingle(reader.Value);
				break;
			case "MEDIUMMULTIPLIER":
				reader.Read();
				MediumMultiplier = global::System.Convert.ToSingle(reader.Value);
				break;
			case "HARDMULTIPLIER":
				reader.Read();
				HardMultiplier = global::System.Convert.ToSingle(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
