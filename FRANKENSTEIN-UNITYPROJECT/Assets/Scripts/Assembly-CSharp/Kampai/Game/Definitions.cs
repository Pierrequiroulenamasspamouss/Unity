namespace Kampai.Game
{
	public class Definitions : global::Kampai.Util.IFastJSONDeserializable
	{
		public global::System.Collections.Generic.IList<global::Kampai.Game.CompositeBuildingPieceDefinition> compositeBuildingPieceDefinitions;

		public global::System.Collections.Generic.IList<global::Kampai.Game.DefinitionGroup> definitionGroups { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.Transaction.TransactionDefinition> transactions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.Transaction.WeightedDefinition> weightedDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.BuildingDefinition> buildingDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.PlotDefinition> plotDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.ItemDefinition> itemDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.MinionDefinition> minionDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.StoreItemDefinition> storeItemDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.CurrencyItemDefinition> currencyItemDefinitions { get; set; }

		public global::Kampai.Game.MarketplaceDefinition marketplaceDefinition { get; set; }

		public global::System.Collections.Generic.IList<string> environmentDefinition { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.PurchasedLandExpansionDefinition> purchasedExpansionDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.LandExpansionDefinition> expansionDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.DebrisDefinition> debrisDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.AspirationalBuildingDefinition> aspirationalBuildingDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.LandExpansionConfig> expansionConfigs { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.CommonLandExpansionDefinition> commonExpansionDefinitions { get; set; }

		public global::Kampai.Game.GachaConfig gachaConfig { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.MinionAnimationDefinition> MinionAnimationDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.QuestDefinition> quests { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.QuestResourceDefinition> questResources { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.NotificationDefinition> notificationDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.RewardCollectionDefinition> collectionDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.PrestigeDefinition> prestigeDefinitions { get; set; }

		public global::Kampai.Game.LevelUpDefinition levelUpDefinition { get; set; }

		public global::Kampai.Game.LevelXPTable levelXPTable { get; set; }

		public global::Kampai.Game.TaskDefinition tasks { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.TimedSocialEventDefinition> timedSocialEventDefinitions { get; set; }

		public global::Kampai.Game.PlayerVersion player { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.RushTimeBandDefinition> rushDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.FootprintDefinition> footprintDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.QuestChainDefinition> questChains { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.SaleDefinition> saleDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.NamedCharacterDefinition> namedCharacterDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.StickerDefinition> stickerDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.LimitedTimeEventDefinition> limitedTimeEventDefinitions { get; set; }

		public global::Kampai.Game.PartyDefinition party { get; set; }

		public global::Kampai.Game.DropLevelBandDefinition randomDropLevelBandDefinition { get; set; }

		public global::Kampai.Game.WayFinderDefinition wayFinderDefinition { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.FlyOverDefinition> flyOverDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Splash.LoadinTipBucketDefinition> loadInTipBucketDefinitions { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Splash.LoadInTipDefinition> loadInTipDefinitions { get; set; }

		public global::Kampai.Game.CameraDefinition cameraSettingsDefinition { get; set; }

		public global::Kampai.Game.SocialSettingsDefinition socialSettingsDefinition { get; set; }

		public virtual object Deserialize(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			global::Kampai.Util.ReaderUtil.EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
				{
					string propertyName = ((string)reader.Value).ToUpper();
					if (!DeserializeProperty(propertyName, reader, converters))
					{
						reader.Skip();
					}
					break;
				}
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return this;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, global::Kampai.Util.ReaderUtil.GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		protected virtual bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "DEFINITIONGROUPS":
				reader.Read();
				definitionGroups = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.DefinitionGroup>(reader, converters);
				break;
			case "TRANSACTIONS":
				reader.Read();
				transactions = ((converters.transactionDefinitionConverter == null) ? global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.Transaction.TransactionDefinition>(reader, converters) : global::Kampai.Util.ReaderUtil.PopulateList(reader, converters, converters.transactionDefinitionConverter));
				break;
			case "WEIGHTEDDEFINITIONS":
				reader.Read();
				weightedDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.Transaction.WeightedDefinition>(reader, converters);
				break;
			case "BUILDINGDEFINITIONS":
				reader.Read();
				buildingDefinitions = global::Kampai.Util.ReaderUtil.PopulateList(reader, converters, converters.buildingDefinitionConverter);
				break;
			case "PLOTDEFINITIONS":
				reader.Read();
				plotDefinitions = global::Kampai.Util.ReaderUtil.PopulateList(reader, converters, converters.plotDefinitionConverter);
				break;
			case "ITEMDEFINITIONS":
				reader.Read();
				itemDefinitions = ((converters.itemDefinitionConverter == null) ? global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.ItemDefinition>(reader, converters) : global::Kampai.Util.ReaderUtil.PopulateList(reader, converters, converters.itemDefinitionConverter));
				break;
			case "MINIONDEFINITIONS":
				reader.Read();
				minionDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.MinionDefinition>(reader, converters);
				break;
			case "STOREITEMDEFINITIONS":
				reader.Read();
				storeItemDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.StoreItemDefinition>(reader, converters);
				break;
			case "CURRENCYITEMDEFINITIONS":
				reader.Read();
				currencyItemDefinitions = ((converters.currencyItemDefinitionConverter == null) ? global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.CurrencyItemDefinition>(reader, converters) : global::Kampai.Util.ReaderUtil.PopulateList(reader, converters, converters.currencyItemDefinitionConverter));
				break;
			case "MARKETPLACEDEFINITION":
				reader.Read();
				marketplaceDefinition = global::Kampai.Util.FastJSONDeserializer.Deserialize<global::Kampai.Game.MarketplaceDefinition>(reader, converters);
				break;
			case "ENVIRONMENTDEFINITION":
				reader.Read();
				environmentDefinition = global::Kampai.Util.ReaderUtil.PopulateList<string>(reader, converters, global::Kampai.Util.ReaderUtil.ReadString);
				break;
			case "PURCHASEDEXPANSIONDEFINITIONS":
				reader.Read();
				purchasedExpansionDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.PurchasedLandExpansionDefinition>(reader, converters);
				break;
			case "EXPANSIONDEFINITIONS":
				reader.Read();
				expansionDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.LandExpansionDefinition>(reader, converters);
				break;
			case "DEBRISDEFINITIONS":
				reader.Read();
				debrisDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.DebrisDefinition>(reader, converters);
				break;
			case "ASPIRATIONALBUILDINGDEFINITIONS":
				reader.Read();
				aspirationalBuildingDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.AspirationalBuildingDefinition>(reader, converters);
				break;
			case "EXPANSIONCONFIGS":
				reader.Read();
				expansionConfigs = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.LandExpansionConfig>(reader, converters);
				break;
			case "COMMONEXPANSIONDEFINITIONS":
				reader.Read();
				commonExpansionDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.CommonLandExpansionDefinition>(reader, converters);
				break;
			case "GACHACONFIG":
				reader.Read();
				gachaConfig = global::Kampai.Util.ReaderUtil.ReadGachaConfig(reader, converters);
				break;
			case "MINIONANIMATIONDEFINITIONS":
				reader.Read();
				MinionAnimationDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.MinionAnimationDefinition>(reader, converters);
				break;
			case "QUESTS":
				reader.Read();
				quests = ((converters.questDefinitionConverter == null) ? global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.QuestDefinition>(reader, converters) : global::Kampai.Util.ReaderUtil.PopulateList(reader, converters, converters.questDefinitionConverter));
				break;
			case "QUESTRESOURCES":
				reader.Read();
				questResources = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.QuestResourceDefinition>(reader, converters);
				break;
			case "NOTIFICATIONDEFINITIONS":
				reader.Read();
				notificationDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.NotificationDefinition>(reader, converters);
				break;
			case "COLLECTIONDEFINITIONS":
				reader.Read();
				collectionDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.RewardCollectionDefinition>(reader, converters);
				break;
			case "PRESTIGEDEFINITIONS":
				reader.Read();
				prestigeDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.PrestigeDefinition>(reader, converters);
				break;
			case "LEVELUPDEFINITION":
				reader.Read();
				levelUpDefinition = global::Kampai.Util.FastJSONDeserializer.Deserialize<global::Kampai.Game.LevelUpDefinition>(reader, converters);
				break;
			case "LEVELXPTABLE":
				reader.Read();
				levelXPTable = global::Kampai.Util.FastJSONDeserializer.Deserialize<global::Kampai.Game.LevelXPTable>(reader, converters);
				break;
			case "TASKS":
				reader.Read();
				tasks = global::Kampai.Util.ReaderUtil.ReadTaskDefinition(reader, converters);
				break;
			case "TIMEDSOCIALEVENTDEFINITIONS":
				reader.Read();
				timedSocialEventDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.TimedSocialEventDefinition>(reader, converters);
				break;
			case "PLAYER":
				reader.Read();
				player = ((converters.playerVersionConverter == null) ? global::Kampai.Util.FastJSONDeserializer.Deserialize<global::Kampai.Game.PlayerVersion>(reader, converters) : converters.playerVersionConverter.ReadJson(reader, converters));
				break;
			case "RUSHDEFINITIONS":
				reader.Read();
				rushDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.RushTimeBandDefinition>(reader, converters);
				break;
			case "FOOTPRINTDEFINITIONS":
				reader.Read();
				footprintDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.FootprintDefinition>(reader, converters);
				break;
			case "QUESTCHAINS":
				reader.Read();
				questChains = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.QuestChainDefinition>(reader, converters);
				break;
			case "SALEDEFINITIONS":
				reader.Read();
				saleDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.SaleDefinition>(reader, converters);
				break;
			case "NAMEDCHARACTERDEFINITIONS":
				reader.Read();
				namedCharacterDefinitions = global::Kampai.Util.ReaderUtil.PopulateList(reader, converters, converters.namedCharacterDefinitionConverter);
				break;
			case "STICKERDEFINITIONS":
				reader.Read();
				stickerDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.StickerDefinition>(reader, converters);
				break;
			case "LIMITEDTIMEEVENTDEFINITIONS":
				reader.Read();
				limitedTimeEventDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.LimitedTimeEventDefinition>(reader, converters);
				break;
			case "PARTY":
				reader.Read();
				party = global::Kampai.Util.ReaderUtil.ReadPartyDefinition(reader, converters);
				break;
			case "RANDOMDROPLEVELBANDDEFINITION":
				reader.Read();
				randomDropLevelBandDefinition = global::Kampai.Util.FastJSONDeserializer.Deserialize<global::Kampai.Game.DropLevelBandDefinition>(reader, converters);
				break;
			case "WAYFINDERDEFINITION":
				reader.Read();
				wayFinderDefinition = global::Kampai.Util.FastJSONDeserializer.Deserialize<global::Kampai.Game.WayFinderDefinition>(reader, converters);
				break;
			case "FLYOVERDEFINITIONS":
				reader.Read();
				flyOverDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.FlyOverDefinition>(reader, converters);
				break;
			case "LOADINTIPBUCKETDEFINITIONS":
				reader.Read();
				loadInTipBucketDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Splash.LoadinTipBucketDefinition>(reader, converters);
				break;
			case "LOADINTIPDEFINITIONS":
				reader.Read();
				loadInTipDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Splash.LoadInTipDefinition>(reader, converters);
				break;
			case "CAMERASETTINGSDEFINITION":
				reader.Read();
				cameraSettingsDefinition = global::Kampai.Util.FastJSONDeserializer.Deserialize<global::Kampai.Game.CameraDefinition>(reader, converters);
				break;
			case "SOCIALSETTINGSDEFINITION":
				reader.Read();
				socialSettingsDefinition = global::Kampai.Util.FastJSONDeserializer.Deserialize<global::Kampai.Game.SocialSettingsDefinition>(reader, converters);
				break;
			case "COMPOSITEBUILDINGPIECEDEFINITIONS":
				reader.Read();
				compositeBuildingPieceDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.CompositeBuildingPieceDefinition>(reader, converters);
				break;
			default:
				return false;
			}
			return true;
		}
	}
}
