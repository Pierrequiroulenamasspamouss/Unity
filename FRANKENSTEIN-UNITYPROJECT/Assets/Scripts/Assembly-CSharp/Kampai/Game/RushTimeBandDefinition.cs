namespace Kampai.Game
{
	public class RushTimeBandDefinition : global::Kampai.Game.Definition
	{
		public int TimeRemainingInSeconds { get; set; }

		public int PremiumCostConstruction { get; set; }

		public int PremiumCostBaseResource { get; set; }

		public int PremiumCostCraftable { get; set; }

		public int PremiumCostCooldowns { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "TIMEREMAININGINSECONDS":
				reader.Read();
				TimeRemainingInSeconds = global::System.Convert.ToInt32(reader.Value);
				break;
			case "PREMIUMCOSTCONSTRUCTION":
				reader.Read();
				PremiumCostConstruction = global::System.Convert.ToInt32(reader.Value);
				break;
			case "PREMIUMCOSTBASERESOURCE":
				reader.Read();
				PremiumCostBaseResource = global::System.Convert.ToInt32(reader.Value);
				break;
			case "PREMIUMCOSTCRAFTABLE":
				reader.Read();
				PremiumCostCraftable = global::System.Convert.ToInt32(reader.Value);
				break;
			case "PREMIUMCOSTCOOLDOWNS":
				reader.Read();
				PremiumCostCooldowns = global::System.Convert.ToInt32(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}

		public int GetCostForRushActionType(global::Kampai.Game.RushActionType rushActionType)
		{
			switch (rushActionType)
			{
			case global::Kampai.Game.RushActionType.CONSTRUCTION:
				return PremiumCostConstruction;
			case global::Kampai.Game.RushActionType.COOLDOWN:
				return PremiumCostCooldowns;
			case global::Kampai.Game.RushActionType.CRAFTING:
				return PremiumCostCraftable;
			case global::Kampai.Game.RushActionType.HARVESTING:
				return PremiumCostBaseResource;
			default:
				return -1;
			}
		}
	}
}
