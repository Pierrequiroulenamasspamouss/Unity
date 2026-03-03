namespace Kampai.Game
{
	public class InventoryFastConverter : global::Kampai.Util.FastJsonCreationConverter<global::Kampai.Game.Instance>
	{
		private global::Kampai.Game.IDefinitionService definitionService;

		private global::Kampai.Util.ILogger logger;

		private global::Kampai.Game.Definition def;

		private bool isSaleItem;

		public InventoryFastConverter(global::Kampai.Game.IDefinitionService definitionService, global::Kampai.Util.ILogger logger)
		{
			this.definitionService = definitionService;
			this.logger = logger;
		}

		public override global::Kampai.Game.Instance ReadJson(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			def = null;
			global::Newtonsoft.Json.Linq.JObject jObject = global::Newtonsoft.Json.Linq.JObject.Load(reader);
			global::Newtonsoft.Json.Linq.JProperty jProperty = jObject.Property("def");
			global::Newtonsoft.Json.Linq.JProperty jProperty2 = ((jProperty != null) ? jProperty : jObject.Property("Definition"));
			isSaleItem = jObject.Property("BuyQuantity") == null;
			if (jProperty2 != null)
			{
				int num = global::Newtonsoft.Json.Linq.LinqExtensions.Value<int>(jProperty2.Value);
				if (num == 77777)
				{
					global::Newtonsoft.Json.Linq.JProperty jProperty3 = jObject.Property("dynamicDefinition");
					if (jProperty3 != null)
					{
						def = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Kampai.Game.DynamicQuestDefinition>(jProperty3.Value.ToString());
					}
				}
				else if (definitionService.Has(num))
				{
					def = definitionService.Get(num);
				}
				else
				{
					logger.Log(global::Kampai.Util.Logger.Level.Error,
						"[InventoryFastConverter] Unknown definition ID: " + num + ", skipping inventory item.");
					return null;
				}
			}
			if (def == null)
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error,
					"[InventoryFastConverter] No definition found for inventory item, skipping.");
				return null;
			}
			reader = jObject.CreateReader();
			return base.ReadJson(reader, converters);
		}

		public override global::Kampai.Game.Instance Create()
		{
			if (def != null)
			{
				global::Kampai.Util.IBuilder<global::Kampai.Game.Instance> builder = def as global::Kampai.Util.IBuilder<global::Kampai.Game.Instance>;
				if (builder != null)
				{
					return builder.Build();
				}
				global::System.Type type = def.GetType();
				if (type == typeof(global::Kampai.Game.MinionDefinition))
				{
					return new global::Kampai.Game.Minion((global::Kampai.Game.MinionDefinition)def);
				}
				if (type == typeof(global::Kampai.Game.Transaction.WeightedDefinition))
				{
					return new global::Kampai.Game.Transaction.WeightedInstance((global::Kampai.Game.Transaction.WeightedDefinition)def);
				}
				if (type == typeof(global::Kampai.Game.PrestigeDefinition))
				{
					return new global::Kampai.Game.Prestige((global::Kampai.Game.PrestigeDefinition)def);
				}
				if (type == typeof(global::Kampai.Game.RewardCollectionDefinition))
				{
					return new global::Kampai.Game.RewardCollection((global::Kampai.Game.RewardCollectionDefinition)def);
				}
				if (type == typeof(global::Kampai.Game.NoOpPlotDefinition))
				{
					return new global::Kampai.Game.NoOpPlot((global::Kampai.Game.NoOpPlotDefinition)def);
				}
				if (type == typeof(global::Kampai.Game.PurchasedLandExpansionDefinition))
				{
					return new global::Kampai.Game.PurchasedLandExpansion((global::Kampai.Game.PurchasedLandExpansionDefinition)def);
				}
				if (type == typeof(global::Kampai.Game.CompositeBuildingPieceDefinition))
				{
					return new global::Kampai.Game.CompositeBuildingPiece((global::Kampai.Game.CompositeBuildingPieceDefinition)def);
				}
				if (type == typeof(global::Kampai.Game.StickerDefinition))
				{
					return new global::Kampai.Game.Sticker((global::Kampai.Game.StickerDefinition)def);
				}
				if (type == typeof(global::Kampai.Game.MarketplaceSaleSlotDefinition))
				{
					return new global::Kampai.Game.MarketplaceSaleSlot((global::Kampai.Game.MarketplaceSaleSlotDefinition)def);
				}
				if (type == typeof(global::Kampai.Game.MarketplaceItemDefinition))
				{
					if (isSaleItem)
					{
						return new global::Kampai.Game.MarketplaceSaleItem((global::Kampai.Game.MarketplaceItemDefinition)def);
					}
					return new global::Kampai.Game.MarketplaceBuyItem((global::Kampai.Game.MarketplaceItemDefinition)def);
				}
				if (type == typeof(global::Kampai.Game.MarketplaceRefreshTimerDefinition))
				{
					return new global::Kampai.Game.MarketplaceRefreshTimer((global::Kampai.Game.MarketplaceRefreshTimerDefinition)def);
				}
                if (type == typeof(global::Kampai.Game.Transaction.TransactionDefinition))
                {
                    logger.Log(global::Kampai.Util.Logger.Level.Error, "TransactionDefinition in inventory, skipping: " + def.ID);
                    return null;
                }

                logger.Log(global::Kampai.Util.Logger.Level.Error, "Unable to map inventory type of " + type);
				throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("InventoryFastConverter.Create: Unable to map inventory type of {0}", type));
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("InventoryFastConverter.Create(): null definition.");
		}
	}
}
