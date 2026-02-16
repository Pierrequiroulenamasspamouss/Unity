namespace Kampai.Game
{
	public class DefinitionService : global::Kampai.Game.IDefinitionService
	{
		protected global::Kampai.Util.ILogger logger;

		protected global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Definition> AllDefinitions;

		protected global::System.Collections.Generic.IList<string> environmentDefinition;

		protected global::Kampai.Game.PartyDefinition partyDefinition;

		protected global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Transaction.WeightedDefinition> gachaDefinitionsByNumMinions;

		protected global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Transaction.WeightedDefinition> partyDefinitionsByNumMinions;

		protected global::System.Collections.Generic.Dictionary<int, int> levelUnlockLookUpTable;

		protected global::Kampai.Game.TaskDefinition taskDefinition;

		protected global::System.Collections.Generic.IList<global::Kampai.Game.RushTimeBandDefinition> rushDefinitions;

		protected global::System.Collections.Generic.IList<global::Kampai.Game.LandExpansionConfig> expansionConfigs;

		protected global::Newtonsoft.Json.Linq.JObject initialPlayer;

		public DefinitionService(global::Kampai.Util.ILogger logger)
		{
			this.logger = logger;
			AllDefinitions = new global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Definition>();
		}

		public void Deserialize(string json)
		{
			if (!string.IsNullOrEmpty(json))
			{
				global::Kampai.Game.Definitions definitions = null;
				try
				{
					JsonConverters jsonConverters = new JsonConverters();
					jsonConverters.buildingDefinitionConverter = new global::Kampai.Game.BuildingDefinitionFastConverter();
					jsonConverters.questDefinitionConverter = new global::Kampai.Game.QuestDefinitionFastConverter();
					jsonConverters.itemDefinitionConverter = new global::Kampai.Game.ItemDefinitionFastConverter();
					jsonConverters.currencyItemDefinitionConverter = new global::Kampai.Game.CurrencyItemFastConverter();
					jsonConverters.playerVersionConverter = new global::Kampai.Game.PlayerDefinitionFastConverter(this);
					jsonConverters.plotDefinitionConverter = new global::Kampai.Game.PlotDefinitionFastConverter(logger);
					jsonConverters.namedCharacterDefinitionConverter = new global::Kampai.Game.NamedCharacterDefinitionFastConverter();
					definitions = global::Kampai.Util.FastJSONDeserializer.Deserialize<global::Kampai.Game.Definitions>(json, jsonConverters);
				}
				catch (global::Newtonsoft.Json.JsonSerializationException ex)
				{
					logger.Error(ex.StackTrace);
					throw new global::Kampai.Util.FatalException(global::Kampai.Util.FatalCode.DS_PARSE_ERROR, ex, "Def json error: {0}", ex);
				}
				catch (global::Newtonsoft.Json.JsonReaderException ex2)
				{
					logger.Error(ex2.StackTrace);
					throw new global::Kampai.Util.FatalException(global::Kampai.Util.FatalCode.DS_PARSE_ERROR, ex2, "Def json error: {0}", ex2);
				}
				if (definitions == null)
				{
					throw new global::Kampai.Util.FatalException(global::Kampai.Util.FatalCode.DS_NULL_ERROR, "DefinitionService.Deserialize(): definitions are null");
				}
				AllDefinitions = new global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Definition>();
				MarkDefinitions(definitions);
				MarkMoreDefinitions(definitions);
				MarkMarketplaceDefinitions(definitions);
				MarkNamedDefinitions(definitions.namedCharacterDefinitions);
				AssembleGachaAnimationDefinitions();
				environmentDefinition = definitions.environmentDefinition;
				partyDefinition = definitions.party;
				AddLevelUpDefinition(definitions);
				AddLevelXPTable(definitions);
				AddDropLevelBandDefinition(definitions);
				taskDefinition = GetTaskDefintion(definitions);
				rushDefinitions = GetRushDefinitions(definitions);
				return;
			}
			throw new global::Kampai.Util.FatalException(global::Kampai.Util.FatalCode.DS_EMPTY_JSON, "DefinitionService.Deserialize(): empty json");
		}

		private void MarkDefinitions(global::Kampai.Game.Definitions definitions)
		{
			MarkDefinitionsAsUsed(definitions.weightedDefinitions);
			MarkDefinitionsAsUsed(definitions.transactions);
			MarkDefinitionsAsUsed(definitions.buildingDefinitions);
			MarkDefinitionsAsUsed(definitions.plotDefinitions);
			MarkDefinitionsAsUsed(definitions.itemDefinitions);
			MarkDefinitionsAsUsed(definitions.minionDefinitions);
			MarkDefinitionsAsUsed(definitions.currencyItemDefinitions);
			MarkDefinitionsAsUsed(definitions.storeItemDefinitions);
			MarkDefinitionsAsUsed(definitions.purchasedExpansionDefinitions);
			MarkDefinitionsAsUsed(definitions.commonExpansionDefinitions);
			MarkDefinitionsAsUsed(definitions.expansionDefinitions);
			MarkDefinitionsAsUsed(definitions.debrisDefinitions);
			MarkDefinitionsAsUsed(definitions.aspirationalBuildingDefinitions);
			MarkDefinitionsAsUsed(definitions.footprintDefinitions);
		}

		private void MarkMoreDefinitions(global::Kampai.Game.Definitions definitions)
		{
			MarkDefinitionsAsUsed(definitions.gachaConfig.GatchaAnimationDefinitions);
			MarkDefinitionsAsUsed(definitions.gachaConfig.DistributionTables);
			MarkDefinitionsAsUsed(definitions.expansionConfigs);
			MarkDefinitionsAsUsed(definitions.MinionAnimationDefinitions);
			MarkDefinitionsAsUsed(definitions.quests);
			MarkDefinitionsAsUsed(definitions.questResources);
			MarkDefinitionsAsUsed(definitions.notificationDefinitions);
			MarkDefinitionsAsUsed(definitions.collectionDefinitions);
			MarkDefinitionsAsUsed(definitions.prestigeDefinitions);
			MarkDefinitionsAsUsed(definitions.definitionGroups);
			MarkDefinitionsAsUsed(definitions.timedSocialEventDefinitions);
			MarkDefinitionsAsUsed(definitions.compositeBuildingPieceDefinitions);
			MarkDefinitionsAsUsed(definitions.questChains);
			MarkDefinitionsAsUsed(definitions.saleDefinitions);
			MarkDefinitionsAsUsed(definitions.stickerDefinitions);
			MarkDefinitionsAsUsed(definitions.limitedTimeEventDefinitions);
			MarkDefinitionAsUsed(definitions.wayFinderDefinition);
			MarkDefinitionsAsUsed(definitions.flyOverDefinitions);
			MarkDefinitionsAsUsed(definitions.loadInTipBucketDefinitions);
			MarkDefinitionsAsUsed(definitions.loadInTipDefinitions);
			MarkDefinitionAsUsed(definitions.cameraSettingsDefinition);
			MarkDefinitionAsUsed(definitions.socialSettingsDefinition);
		}

		private void MarkMarketplaceDefinitions(global::Kampai.Game.Definitions definitions)
		{
			MarkDefinitionAsUsed(definitions.marketplaceDefinition);
			MarkDefinitionsAsUsed(definitions.marketplaceDefinition.itemDefinitions);
			MarkDefinitionsAsUsed(definitions.marketplaceDefinition.slotDefinitions);
			MarkDefinitionAsUsed(definitions.marketplaceDefinition.refreshTimerDefinition);
		}

		private void MarkNamedDefinitions(global::System.Collections.Generic.IList<global::Kampai.Game.NamedCharacterDefinition> namedCharacterDefinitions)
		{
			if (namedCharacterDefinitions == null)
			{
				return;
			}
			MarkDefinitionsAsUsed(namedCharacterDefinitions);
			foreach (global::Kampai.Game.NamedCharacterDefinition namedCharacterDefinition in namedCharacterDefinitions)
			{
				global::Kampai.Game.BobCharacterDefinition bobCharacterDefinition = namedCharacterDefinition as global::Kampai.Game.BobCharacterDefinition;
				if (bobCharacterDefinition != null)
				{
					MarkDefinitionAsUsed(bobCharacterDefinition.WanderWeightedDeck);
				}
			}
		}

		public bool Has(int id)
		{
			return AllDefinitions.ContainsKey(id);
		}

		public bool Has<T>(int id) where T : global::Kampai.Game.Definition
		{
			global::Kampai.Game.Definition definition = null;
			if (Has(id))
			{
				definition = AllDefinitions[id] as T;
			}
			return definition != null;
		}

		public global::Kampai.Game.Definition Get(int id)
		{
			if (Has(id))
			{
				return AllDefinitions[id];
			}
			logger.Fatal(global::Kampai.Util.FatalCode.DS_NO_ITEM_DEF, id);
			return null;
		}

		public T Get<T>(int id) where T : global::Kampai.Game.Definition
		{
			T val = (T)null;
			if (Has(id))
			{
				val = AllDefinitions[id] as T;
			}
			if (val == null)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.DS_NO_ITEM_TYPE_DEF, id);
			}
			return val;
		}

		public bool TryGet<T>(int id, out T definition) where T : global::Kampai.Game.Definition
		{
			definition = (T)null;
			if (Has(id))
			{
				definition = AllDefinitions[id] as T;
			}
			return definition != null;
		}

		public global::System.Collections.Generic.List<T> GetAll<T>() where T : global::Kampai.Game.Definition
		{
			global::System.Collections.Generic.List<T> list = new global::System.Collections.Generic.List<T>();
			foreach (global::Kampai.Game.Definition value in AllDefinitions.Values)
			{
				T val = value as T;
				if (val != null)
				{
					list.Add(val);
				}
			}
			return list;
		}

		public T Get<T>() where T : global::Kampai.Game.Definition
		{
			foreach (global::Kampai.Game.Definition value in AllDefinitions.Values)
			{
				T val = value as T;
				if (val != null)
				{
					return val;
				}
			}
			return (T)null;
		}

		public global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Definition> GetAllDefinitions()
		{
			return AllDefinitions;
		}

		public global::System.Collections.Generic.IList<string> GetEnvironemtDefinition()
		{
			return environmentDefinition;
		}

		public global::Kampai.Game.PartyDefinition GetPartyDefinition()
		{
			return partyDefinition;
		}

		public void ReclaimEnfironmentDefinitions()
		{
			environmentDefinition = null;
		}

		public global::Kampai.Game.Transaction.WeightedDefinition GetGachaWeightsForNumMinions(int numMinions, bool party)
		{
			global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Transaction.WeightedDefinition> dictionary = ((!party) ? gachaDefinitionsByNumMinions : partyDefinitionsByNumMinions);
			if (dictionary.ContainsKey(numMinions))
			{
				return dictionary[numMinions];
			}
			return dictionary[4];
		}

		public global::System.Collections.Generic.List<global::Kampai.Game.Transaction.WeightedDefinition> GetAllGachaDefinitions()
		{
			global::System.Collections.Generic.List<global::Kampai.Game.Transaction.WeightedDefinition> list = new global::System.Collections.Generic.List<global::Kampai.Game.Transaction.WeightedDefinition>();
			list.AddRange(gachaDefinitionsByNumMinions.Values);
			list.AddRange(partyDefinitionsByNumMinions.Values);
			return list;
		}

		public int GetHarvestItemDefinitionIdFromTransactionId(int transactionId)
		{
			global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition = Get<global::Kampai.Game.Transaction.TransactionDefinition>(transactionId);
			global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> outputs = transactionDefinition.Outputs;
			if (outputs[0] != null)
			{
				return outputs[0].ID;
			}
			logger.Fatal(global::Kampai.Util.FatalCode.PS_NO_TRANSACTION, transactionId);
			return -1;
		}

		public string GetHarvestIconFromTransactionID(int transactionId)
		{
			global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition = Get<global::Kampai.Game.Transaction.TransactionDefinition>(transactionId);
			global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> outputs = transactionDefinition.Outputs;
			if (outputs[0] != null)
			{
				global::Kampai.Game.ItemDefinition itemDefinition = Get<global::Kampai.Game.ItemDefinition>(outputs[0].ID);
				return itemDefinition.Image;
			}
			logger.Fatal(global::Kampai.Util.FatalCode.PS_NO_TRANSACTION, transactionId);
			return string.Empty;
		}

		public int ExtractQuantityFromTransaction(int transactionID, int definitionID)
		{
			int result = 0;
			global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition = Get<global::Kampai.Game.Transaction.TransactionDefinition>(transactionID);
			if (transactionDefinition.Outputs != null)
			{
				result = global::Kampai.Game.Transaction.TransactionUtil.ExtractQuantityFromTransaction(transactionDefinition, definitionID);
			}
			return result;
		}

		public int GetBuildingDefintionIDFromItemDefintionID(int itemDefinitionID)
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.AnimatingBuildingDefinition> all = GetAll<global::Kampai.Game.AnimatingBuildingDefinition>();
			foreach (global::Kampai.Game.AnimatingBuildingDefinition item in all)
			{
				global::Kampai.Game.ResourceBuildingDefinition resourceBuildingDefinition = item as global::Kampai.Game.ResourceBuildingDefinition;
				if (resourceBuildingDefinition != null && resourceBuildingDefinition.ItemId == itemDefinitionID)
				{
					return resourceBuildingDefinition.ID;
				}
				global::Kampai.Game.CraftingBuildingDefinition craftingBuildingDefinition = item as global::Kampai.Game.CraftingBuildingDefinition;
				if (craftingBuildingDefinition == null)
				{
					continue;
				}
				foreach (global::Kampai.Game.RecipeDefinition recipeDefinition in craftingBuildingDefinition.RecipeDefinitions)
				{
					if (recipeDefinition.ItemID == itemDefinitionID)
					{
						return craftingBuildingDefinition.ID;
					}
				}
			}
			return 0;
		}

		public string GetBuildingFootprint(int ID)
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.FootprintDefinition> all = GetAll<global::Kampai.Game.FootprintDefinition>();
			foreach (global::Kampai.Game.FootprintDefinition item in all)
			{
				if (item.ID == ID)
				{
					return item.Footprint;
				}
			}
			return string.Empty;
		}

		public int GetIncrementalCost(global::Kampai.Game.Definition definition)
		{
			global::Kampai.Game.BuildingDefinition buildingDefinition = definition as global::Kampai.Game.BuildingDefinition;
			return (buildingDefinition != null) ? buildingDefinition.IncrementalCost : 0;
		}

		public global::Kampai.Game.BridgeDefinition GetBridgeDefinition(int itemDefinitionID)
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.BridgeDefinition> all = GetAll<global::Kampai.Game.BridgeDefinition>();
			foreach (global::Kampai.Game.BridgeDefinition item in all)
			{
				if (item.ID == itemDefinitionID)
				{
					return item;
				}
			}
			return null;
		}

		public bool HasUnlockItemInTransactionOutput(int transactionID)
		{
			global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition = Get<global::Kampai.Game.Transaction.TransactionDefinition>(transactionID);
			if (transactionDefinition.Outputs != null)
			{
				foreach (global::Kampai.Util.QuantityItem output in transactionDefinition.Outputs)
				{
					global::Kampai.Game.Definition definition = Get<global::Kampai.Game.Definition>(output.ID);
					if (definition is global::Kampai.Game.UnlockDefinition)
					{
						return true;
					}
				}
			}
			return false;
		}

		public int GetLevelItemUnlocksAt(int definitionID)
		{
			if (levelUnlockLookUpTable == null)
			{
				levelUnlockLookUpTable = new global::System.Collections.Generic.Dictionary<int, int>();
				global::Kampai.Game.LevelUpDefinition levelUpDefinition = Get<global::Kampai.Game.LevelUpDefinition>(88888);
				for (int i = 0; i < levelUpDefinition.transactionList.Count; i++)
				{
					if (levelUpDefinition.transactionList[i] == 0)
					{
						continue;
					}
					global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition = Get<global::Kampai.Game.Transaction.TransactionDefinition>(levelUpDefinition.transactionList[i]);
					if (transactionDefinition == null || transactionDefinition.Outputs == null)
					{
						continue;
					}
					foreach (global::Kampai.Util.QuantityItem output in transactionDefinition.Outputs)
					{
						global::Kampai.Game.UnlockDefinition definition = null;
						if (TryGet<global::Kampai.Game.UnlockDefinition>(output.ID, out definition) && !levelUnlockLookUpTable.ContainsKey(definition.ReferencedDefinitionID))
						{
							levelUnlockLookUpTable.Add(definition.ReferencedDefinitionID, i + 1);
						}
					}
				}
			}
			if (!levelUnlockLookUpTable.ContainsKey(definitionID))
			{
				logger.Fatal(global::Kampai.Util.FatalCode.TE_MISSING_UNLOCK, definitionID);
				return 0;
			}
			return levelUnlockLookUpTable[definitionID];
		}

		public global::Kampai.Game.TaskLevelBandDefinition GetTaskLevelBandForLevel(int level)
		{
			if (taskDefinition != null)
			{
				global::System.Collections.Generic.IList<global::Kampai.Game.TaskLevelBandDefinition> levelBands = taskDefinition.levelBands;
				if (levelBands != null)
				{
					global::Kampai.Game.TaskLevelBandDefinition result = levelBands[0];
					{
						foreach (global::Kampai.Game.TaskLevelBandDefinition item in levelBands)
						{
							if (item.MinLevel <= level)
							{
								result = item;
							}
						}
						return result;
					}
				}
			}
			return null;
		}

		public global::Kampai.Game.RushTimeBandDefinition GetRushTimeBandForTime(int timeRemainingInSeconds)
		{
			if (rushDefinitions != null)
			{
				global::System.Collections.Generic.IList<global::Kampai.Game.RushTimeBandDefinition> list = rushDefinitions;
				if (list != null)
				{
					foreach (global::Kampai.Game.RushTimeBandDefinition item in list)
					{
						if (timeRemainingInSeconds <= item.TimeRemainingInSeconds)
						{
							return item;
						}
					}
					return list[list.Count - 1];
				}
			}
			return null;
		}

		private void AssembleGachaAnimationDefinitions()
		{
			gachaDefinitionsByNumMinions = new global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Transaction.WeightedDefinition>();
			partyDefinitionsByNumMinions = new global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Transaction.WeightedDefinition>();
			foreach (global::Kampai.Game.GachaWeightedDefinition item in GetAll<global::Kampai.Game.GachaWeightedDefinition>())
			{
				global::Kampai.Game.Transaction.WeightedDefinition weightedDefinition = item.WeightedDefinition;
				MarkDefinitionAsUsed(weightedDefinition);
				int minions = item.Minions;
				bool partyOnly = item.PartyOnly;
				if ((!partyOnly && gachaDefinitionsByNumMinions.ContainsKey(minions)) || (partyOnly && partyDefinitionsByNumMinions.ContainsKey(minions)))
				{
					throw new global::Kampai.Util.FatalException(global::Kampai.Util.FatalCode.DS_DUPLICATE_GACHA, minions);
				}
				global::System.Collections.Generic.IList<global::Kampai.Game.Transaction.WeightedQuantityItem> entities = weightedDefinition.Entities;
				foreach (global::Kampai.Game.Transaction.WeightedQuantityItem item2 in entities)
				{
					int iD = item2.ID;
					if (iD > 0)
					{
						global::Kampai.Game.GachaAnimationDefinition gachaAnimationDefinition = Get<global::Kampai.Game.GachaAnimationDefinition>(iD);
						if (gachaAnimationDefinition == null)
						{
							throw new global::Kampai.Util.FatalException(global::Kampai.Util.FatalCode.DS_RELATION_DOES_NOT_EXIST, iD);
						}
						if (gachaAnimationDefinition.Minions > 0 && gachaAnimationDefinition.Minions != minions)
						{
							throw new global::Kampai.Util.FatalException(global::Kampai.Util.FatalCode.DS_NUM_MINION_GACHA_MISMATCH, item2.ID);
						}
					}
				}
				if (partyOnly)
				{
					partyDefinitionsByNumMinions.Add(minions, weightedDefinition);
				}
				else
				{
					gachaDefinitionsByNumMinions.Add(minions, weightedDefinition);
				}
			}
		}

		private void AddLevelUpDefinition(global::Kampai.Game.Definitions definitions)
		{
			if (AllDefinitions.ContainsKey(88888))
			{
				throw new global::Kampai.Util.FatalException(global::Kampai.Util.FatalCode.DS_DUPLICATE_LEVELUP, 88888);
			}
			AllDefinitions[88888] = definitions.levelUpDefinition;
		}

		private void AddDropLevelBandDefinition(global::Kampai.Game.Definitions definitions)
		{
			if (AllDefinitions.ContainsKey(88889))
			{
				throw new global::Kampai.Util.FatalException(global::Kampai.Util.FatalCode.DS_DUPLICATE_RANDOM_BANDS, 88889);
			}
			AllDefinitions[88889] = definitions.randomDropLevelBandDefinition;
		}

		private void AddLevelXPTable(global::Kampai.Game.Definitions definitions)
		{
			if (AllDefinitions.ContainsKey(99999))
			{
				throw new global::Kampai.Util.FatalException(global::Kampai.Util.FatalCode.DS_DUPLICATE_XP, 99999);
			}
			AllDefinitions[99999] = definitions.levelXPTable;
		}

		private void MarkDefinitionAsUsed(global::Kampai.Game.Definition d)
		{
			int iD = d.ID;
			if (AllDefinitions.ContainsKey(iD))
			{
				throw new global::Kampai.Util.FatalException(global::Kampai.Util.FatalCode.DS_DUPLICATE_ID, iD, "DefinitionService.MarkDefinitionAsUsed(): defId = {0}", iD);
			}
			AllDefinitions[d.ID] = d;
		}

		private global::System.Collections.Generic.IEnumerable<T> MarkDefinitionsAsUsed<T>(global::System.Collections.Generic.IEnumerable<T> used) where T : global::Kampai.Game.Definition
		{
			if (used != null)
			{
				foreach (T item in used)
				{
					if (!item.Disabled)
					{
						MarkDefinitionAsUsed(item);
					}
				}
			}
			return used;
		}

		private global::Kampai.Game.TaskDefinition GetTaskDefintion(global::Kampai.Game.Definitions defs)
		{
			global::Kampai.Game.TaskDefinition tasks = defs.tasks;
			if (tasks != null)
			{
				global::System.Collections.Generic.List<global::Kampai.Game.TaskLevelBandDefinition> list = new global::System.Collections.Generic.List<global::Kampai.Game.TaskLevelBandDefinition>(tasks.levelBands);
				list.Sort((global::Kampai.Game.TaskLevelBandDefinition p1, global::Kampai.Game.TaskLevelBandDefinition p2) => p1.MinLevel - p2.MinLevel);
				tasks.levelBands = list;
			}
			return tasks;
		}

		private global::System.Collections.Generic.IList<global::Kampai.Game.RushTimeBandDefinition> GetRushDefinitions(global::Kampai.Game.Definitions defs)
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.RushTimeBandDefinition> list = defs.rushDefinitions;
			if (list != null)
			{
				global::System.Collections.Generic.List<global::Kampai.Game.RushTimeBandDefinition> list2 = new global::System.Collections.Generic.List<global::Kampai.Game.RushTimeBandDefinition>(list);
				global::Kampai.Util.RushUtil.SortByTime(list2);
				list = list2;
			}
			return list;
		}

		public void SetInitialPlayer(global::Newtonsoft.Json.Linq.JObject token)
		{
			initialPlayer = token;
		}

	public string GetInitialPlayer()
		{
			return initialPlayer.ToString();
		}

		public bool IsReady
		{
			get
			{
				// Check if AllDefinitions is populated
				// We can also check specific critical definitions if needed
				return AllDefinitions != null && AllDefinitions.Count > 0 && partyDefinition != null;
			}
		}
	}
}
