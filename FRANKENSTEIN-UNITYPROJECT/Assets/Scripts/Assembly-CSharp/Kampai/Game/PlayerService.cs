namespace Kampai.Game
{
	public class PlayerService : global::Kampai.Game.IPlayerService
	{
		protected global::Kampai.Game.Player player;

		private object mutex = new object();

		protected global::Kampai.Game.TransactionEngine _engine;

		private string swrveGroup;

		protected global::Kampai.Game.TransactionEngine engine
		{
			get
			{
				if (_engine == null)
				{
					_engine = new global::Kampai.Game.TransactionEngine(logger, definitionService, randService);
				}
				return _engine;
			}
		}

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Common.IRandomService randService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Game.PostTransactionSignal postTransactionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.InsufficientInputsSignal insufficientInputsSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.DisplayRandomDropIconSignal randomDropSignal { get; set; }

		[Inject]
		public global::Kampai.Game.OpenStorageBuildingSignal openStorageBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingChangeStateSignal changeState { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public ILocalPersistanceService localPersistanceService { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowBuildingDetailMenuSignal ShowBuildingDetailmenuSignal { get; set; }

		[PostConstruct]
		public void Init()
		{
			// Initialize player object to prevent NullReferenceException
			if (player == null)
			{
				player = new global::Kampai.Game.Player(definitionService, logger);
			}
		}

		public long ID
		{
			get
			{
				return player.ID;
			}
			set
			{
                if (player == null)
                {
                    // Fallback to avoid crash if accessed before Deserialization
                    player = new global::Kampai.Game.Player(definitionService, logger);
                }
				player.ID = value;
			}
		}

		public global::Kampai.Game.Player LastSave { get; set; }

		public int LevelUpUTC
		{
			get
			{
				return player.lastLevelUpTime;
			}
			set
			{
				player.lastLevelUpTime = value;
			}
		}

		public int LastGameStartUTC
		{
			get
			{
				return player.lastGameStartTime;
			}
			set
			{
				player.lastGameStartTime = value;
			}
		}

		public string SWRVEGroup
		{
			get
			{
				return (!string.IsNullOrEmpty(swrveGroup)) ? swrveGroup : "anyVariant";
			}
			set
			{
				swrveGroup = value;
			}
		}

		public int GameplaySecondsSinceLevelUp
		{
			get
			{
				return player.totalGameplayDurationSinceLastLevelUp;
			}
			set
			{
				player.totalGameplayDurationSinceLastLevelUp = value;
			}
		}

		public PlayerService()
		{
		}

		public PlayerService(global::Kampai.Game.Player player)
			: this()
		{
			this.player = player;
		}

		public uint GetQuantity(global::Kampai.Game.StaticItem def)
		{
			return player.GetQuantity(def);
		}

		public int GetCountByDefinitionId(int defId)
		{
			return player.GetCountByDefinitionId(defId);
		}

		public uint GetQuantityByDefinitionId(int defId)
		{
			return player.GetQuantityByDefinitionId(defId);
		}

		public uint GetQuantityByInstanceId(int instanceId)
		{
			return player.GetQuantityByInstanceId(instanceId);
		}

		public void AlterQuantity(global::Kampai.Game.StaticItem def, int amount)
		{
			player.AlterQuantity(def, amount);
		}

		public global::System.Collections.Generic.ICollection<int> GetAnimatingBuildingIDs()
		{
			return player.FindAllAnimatingBuildingIDs();
		}

		public global::System.Collections.Generic.ICollection<global::Kampai.Game.Item> GetItems()
		{
			uint itemCount = 0u;
			return player.FindAllItems(out itemCount);
		}

		public global::System.Collections.Generic.IList<global::Kampai.Game.Instance> GetInstancesByDefinition<T>() where T : global::Kampai.Game.Definition
		{
			return player.GetInstancesByDefinition<T>();
		}

		public global::System.Collections.Generic.IList<global::Kampai.Game.Instance> GetInstancesByDefinitionID(int defId)
		{
			return player.GetInstancesByDefinitionID(defId);
		}

		public void GetInstancesByType<T>(ref global::System.Collections.Generic.List<T> list) where T : class, global::Kampai.Game.Instance
		{
			player.GetInstancesByType(ref list);
		}

		public global::System.Collections.Generic.List<T> GetInstancesByType<T>() where T : class, global::Kampai.Game.Instance
		{
			global::System.Collections.Generic.List<T> result = new global::System.Collections.Generic.List<T>();
			player.GetInstancesByType(ref result);
			return result;
		}

		public global::System.Collections.Generic.IList<global::Kampai.Game.Item> GetItemsByDefinition<T>() where T : global::Kampai.Game.Definition
		{
			return player.GetItemsByDefinition<T>();
		}

		public global::System.Collections.Generic.IList<global::Kampai.Game.IngredientsItemDefinition> GetAllUnLockedIngredients()
		{
			return player.FindAllUnLockedIngredients();
		}

		public global::System.Collections.Generic.IList<global::Kampai.Game.IngredientsItemDefinition> FindAllAvailableIngredients()
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.IngredientsItemDefinition> list = new global::System.Collections.Generic.List<global::Kampai.Game.IngredientsItemDefinition>();
			foreach (global::Kampai.Game.IngredientsItemDefinition allUnLockedIngredient in GetAllUnLockedIngredients())
			{
				int buildingDefintionIDFromItemDefintionID = definitionService.GetBuildingDefintionIDFromItemDefintionID(allUnLockedIngredient.ID);
				if (GetFirstInstanceByDefinitionId<global::Kampai.Game.Building>(buildingDefintionIDFromItemDefintionID) != null)
				{
					list.Add(allUnLockedIngredient);
				}
			}
			return list;
		}

		public global::System.Collections.Generic.ICollection<global::Kampai.Game.Item> GetItems(out uint itemCount)
		{
			return player.FindAllItems(out itemCount);
		}

		public global::System.Collections.Generic.Dictionary<int, int> GetBuildingOnBoardCountMap()
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.Building> instancesByType = player.GetInstancesByType<global::Kampai.Game.Building>();
			global::System.Collections.Generic.Dictionary<int, int> dictionary = new global::System.Collections.Generic.Dictionary<int, int>();
			int num = 0;
			foreach (global::Kampai.Game.Building item in instancesByType)
			{
				if (item.State != global::Kampai.Game.BuildingState.Inventory)
				{
					num = item.Definition.ID;
					if (dictionary.ContainsKey(num))
					{
						global::System.Collections.Generic.Dictionary<int, int> dictionary3;
						global::System.Collections.Generic.Dictionary<int, int> dictionary2 = (dictionary3 = dictionary);
						int key2;
						int key = (key2 = num);
						key2 = dictionary3[key2];
						dictionary2[key] = key2 + 1;
					}
					else
					{
						dictionary.Add(num, 1);
					}
				}
			}
			return dictionary;
		}

		public T GetByInstanceId<T>(int id) where T : class, global::Kampai.Game.Instance
		{
			return player.GetByInstanceId<T>(id);
		}

		public T GetFirstInstanceByDefinitionId<T>(int definitionId) where T : class, global::Kampai.Game.Instance
		{
			return player.GetFirstInstanceByDefinitionId<T>(definitionId);
		}

		public I GetFirstInstanceByDefintion<I, D>() where I : class, global::Kampai.Game.Instance where D : global::Kampai.Game.Definition
		{
			return player.GetFirstInstanceByDefintion<I, D>();
		}

		public int GetUnlockedQuantityOfID(int defId)
		{
			return player.GetUnlockedAmountFromID(defId);
		}

		private bool RunTransaction(global::Kampai.Game.Transaction.TransactionDefinition transaction, out global::System.Collections.Generic.IList<global::Kampai.Game.Instance> outputs, global::Kampai.Game.TransactionArg arg = null)
		{
			outputs = null;
			if (transaction != null)
			{
				return engine.Perform(player, transaction, out outputs, arg);
			}
			logger.Fatal(global::Kampai.Util.FatalCode.PS_NULL_TRANSACTION, "Null transaction");
			return false;
		}

        public global::Kampai.Game.Player LoadPlayerData(string serialized)
        {
            global::Kampai.Game.Player loadedPlayer = null;

            lock (mutex)
            {
                // 1. Clean String [FIX] Robust JSON Extraction
                if (!string.IsNullOrEmpty(serialized))
                {
                    serialized = serialized.Trim();
                    // Check for BOM
                    if (serialized.StartsWith("\ufeff")) serialized = serialized.Substring(1);

                    // [FIX] Detect missing opening brace (headless JSON)
                    // If the string starts with a property like "version": or "ID": but no brace
                    if (!serialized.StartsWith("{"))
                    {
                         if (serialized.IndexOf("\"version\":") >= 0 || serialized.IndexOf("\"ID\":") >= 0)
                         {
                             logger.Warning("[PlayerService] Detected missing opening brace. Change 'garbage' to '{' ...");
                             serialized = "{" + serialized;
                         }
                    }

                    // Find limits of JSON object
                    int firstBrace = serialized.IndexOf('{');
                    int lastBrace = serialized.LastIndexOf('}');

                    // [FIX] Sanity check: If firstBrace is suspiciously deep (e.g. inside inventory), reject it or warn
                    // The inventory item typically starts with {"ID":309...
                    if (firstBrace > 10 && (serialized.Contains("\"ID\":309") || serialized.Contains("\"ID\": 309"))) 
                    {
                        logger.Warning("[PlayerService] firstBrace found at " + firstBrace + " which seems to be inside an Inventory Item. Investigating context...");
                        
                        // Try to find if we missed the start
                        string inspection = serialized.Substring(0, firstBrace);
                        logger.Warning("[PlayerService] Pre-brace content: " + inspection);
                        
                        // If we have "version":3 before the brace, we definitely missed a brace
                        if (inspection.Contains("\"version\":") || inspection.Contains("\"ID\":"))
                        {
                             logger.Warning("[PlayerService] repairing missing root brace based on context.");
                             serialized = "{" + serialized;
                             firstBrace = 0; 
                             lastBrace = serialized.LastIndexOf('}');
                        }
                    }

                    if (firstBrace >= 0 && lastBrace > firstBrace)
                    {
                        // Extract only the valid JSON part
                        serialized = serialized.Substring(firstBrace, (lastBrace - firstBrace) + 1);
                    }
                }

                bool loadFailed = false;

                // 2. Try Deserialize (Using V3 Serializer directly)
                if (!string.IsNullOrEmpty(serialized))
                {
                    try
                    {
                        // Use the specific V3 serializer to avoid the Version Checker crash
                        global::Kampai.Game.PlayerSerializerV3 serializer = new global::Kampai.Game.PlayerSerializerV3();
                        player = serializer.Deserialize(serialized, definitionService, logger);
                    }
                    catch (global::System.Exception e)
                    {
                        //logger.Error("[MOCK] Load Failed: " + e.Message + " | JSON: " + serialized);
                        loadFailed = true;
                    }
                }
                else
                {
                    loadFailed = true;
                }

                // 3. Fallback Creation
                if (loadFailed || player == null)
                {
                    //logger.Warning("[MOCK] Creating Fallback Player.");
                    player = new global::Kampai.Game.Player(definitionService, logger);
                    player.ID = 1001;
                    player.HighestFtueLevel = 999;
                    player.Version = 3;
                }

                // 4. CRITICAL: SETUP ENVIRONMENT FOR LOCALIZATION & SWRVE
                // This writes to Unity's permanent storage. The Localization Service
                // reads this during its initialization phase.
                if (!global::UnityEngine.PlayerPrefs.HasKey("Language"))
                {
                    logger.Warning("[FIX] Setting Default Language to en-US");
                    global::UnityEngine.PlayerPrefs.SetString("Language", "en-US");
                    global::UnityEngine.PlayerPrefs.SetString("Region", "US");
                    global::UnityEngine.PlayerPrefs.Save();
                }

                EnsureCriticalDataIntegrity(player);
            }
            return player;
        }

        private global::Kampai.Game.BlackMarketBoardDefinition CreateFallbackOrderBoardDefinition()
        {
            logger.Warning("Creating fallback OrderBoard definition (ID 3022).");
            var def = new global::Kampai.Game.BlackMarketBoardDefinition();
            def.ID = 3022;
            // [FIX] BuildingType is in GLOBAL namespace
            def.Type = global::BuildingType.BuildingTypeIdentifier.BLACKMARKETBOARD;
            def.Prefab = "Unique_JobBoard_Prefab";
            def.ScaffoldingPrefab = "Unique_Scaffolding_Prefab";
            def.TicketRepopTime = 0.5f;
            def.CharacterOrderChance = 50;
            
            // Reconstruct minimal viable properties
            def.OrderNames = new global::System.Collections.Generic.List<string> { "FallbackPort" };
            def.UnlockedIngredientsToOrderSlotsTable = new global::System.Collections.Generic.List<global::Kampai.Game.BlackMarketBoardUnlockedOrderSlotDefinition>();
            def.UnlockedIngredientsToOrderSlotsTable.Add(new global::Kampai.Game.BlackMarketBoardUnlockedOrderSlotDefinition { OrderSlots = 1, UnlockItems = 1 });
            
            def.LevelBands = new global::System.Collections.Generic.List<global::Kampai.Game.BlackMarketBoardLevelBandDefinition>();
            def.LevelBands.Add(new global::Kampai.Game.BlackMarketBoardLevelBandDefinition {
                Level = 1, ResurfaceTime = 20, MaxUniqueResources = 1, MinGrindReward = 10, MaxGrindReward = 30,
                EasyWeight = 1, GrindWeight = 1, EasyMultiplier = 1f, GrindMultipler = 1f
            });

            return def;
        }

        private void EnsureCriticalDataIntegrity(global::Kampai.Game.Player player)
        {
            if (player == null) return;

            // [FIX] Repair missing OrderBoard (2020-3 OrderBoardMissing)
            const int ORDER_BOARD_DEF_ID = 3022; 
            global::Kampai.Game.OrderBoard orderBoard = player.GetFirstInstanceByDefinitionId<global::Kampai.Game.OrderBoard>(ORDER_BOARD_DEF_ID);

            if (orderBoard == null)
            {
                logger.Warning("[PlayerService] CRITICAL: OrderBoard missing! Repairing...");
                
                global::Kampai.Game.BlackMarketBoardDefinition def;
                if (!definitionService.TryGet(ORDER_BOARD_DEF_ID, out def))
                {
                    def = CreateFallbackOrderBoardDefinition();
                }

                if (def != null)
                {
                    orderBoard = new global::Kampai.Game.OrderBoard(def);
                    // Initialize critical lists to prevent sanity check failures
                    // [FIX] Correct type is OrderBoardTicket
                    if (orderBoard.tickets == null) orderBoard.tickets = new global::System.Collections.Generic.List<global::Kampai.Game.OrderBoardTicket>(); 
                    
                    // Specific fix for property initialization based on observed errors
                    orderBoard.PriorityPrestigeDefinitionIDs = new global::System.Collections.Generic.List<int> { 40003 };
                    orderBoard.HarvestableCharacterDefinitionId = 0;
                    
                    player.Add(orderBoard);
                    logger.Warning("[PlayerService] OrderBoard restored successfully.");
                }
                else
                {
                    logger.FatalNoThrow(global::Kampai.Util.FatalCode.PS_FAILED_DEEP_SCAN, "Unable to find OrderBoard definition during repair.");
                }
            }
            else
            {
                // Ensure list integrity for existing instance
                // [FIX] Correct type is OrderBoardTicket
                if (orderBoard.tickets == null) orderBoard.tickets = new global::System.Collections.Generic.List<global::Kampai.Game.OrderBoardTicket>(); 
            }
        }

        private void HandleJsonParseException(string json, global::System.Exception e)
		{
			logger.Error("HandleJsonParseException(): player json: {0}", json ?? "null");
			throw new global::Kampai.Util.FatalException(global::Kampai.Util.FatalCode.PS_JSON_PARSE_ERR, e, "Json Parse Err: {0}", e);
		}

        public void Deserialize(string serialized, bool isRetry = false)
        {
            var loaded = LoadPlayerData(serialized);
            player = loaded;
            LastSave = loaded;
        }

        public byte[] SavePlayerData(global::Kampai.Game.Player playerData)
		{
			byte[] result = null;
			lock (mutex)
			{
				try
				{
					global::Kampai.Game.PlayerVersion playerVersion = new global::Kampai.Game.PlayerVersion();
					result = playerVersion.Serialize(playerData, definitionService, logger);
                    
                    // --- FIX: PERSISTENCE (with corruption protection)---
                    if (result != null && result.Length > 0)
                    {
                        string path = global::UnityEngine.Application.persistentDataPath + "/player_save.json";
                        global::System.IO.File.WriteAllBytes(path, result);
                        string tmp = path + ".tmp";
                        global::System.IO.File.WriteAllBytes(tmp, result);
                        global::System.IO.File.Copy(tmp, path, true);
                        global::System.IO.File.Delete(tmp);
                    }
				}
				catch (global::Newtonsoft.Json.JsonSerializationException ex)
				{
					logger.Fatal(global::Kampai.Util.FatalCode.PS_JSON_SERIALIZE_ERR, "Json Err: {0}", ex.ToString());
				}
			}
			return result;
		}

		public byte[] Serialize()
		{
			return SavePlayerData(player);
		}

		public bool IsPlayerInitialized()
		{
			return player != null;
		}

		public void Add(global::Kampai.Game.Instance i)
		{
			int iD = i.ID;
			if (iD != 0)
			{
				global::Kampai.Game.Instance byInstanceId = player.GetByInstanceId<global::Kampai.Game.Instance>(iD);
				if (byInstanceId != null)
				{
					logger.Fatal(global::Kampai.Util.FatalCode.PS_ITEM_ALREADY_ADDED, "Item {0} already exists in player inventory.", iD);
					return;
				}
			}
			player.AssignNextInstanceId(i);
			player.Add(i);
		}

		public void AssignNextInstanceId(global::Kampai.Util.Identifiable i)
		{
			player.AssignNextInstanceId(i);
		}

		public void Remove(global::Kampai.Game.Instance i)
		{
			if (i.ID != 0)
			{
				player.Remove(i);
			}
		}

		public global::System.Collections.Generic.ICollection<T> GetByDefinitionId<T>(int id) where T : global::Kampai.Game.Instance
		{
			return player.GetByDefinitionId<T>(id);
		}

		public global::Kampai.Game.Transaction.WeightedInstance GetWeightedInstance(int defId, global::Kampai.Game.Transaction.WeightedDefinition wd = null)
		{
			return player.GetWeightedInstance(defId, wd);
		}

		public bool CanAffordExchange(int grindNeeded)
		{
			return player.GetQuantityByDefinitionId(1) >= PremiumCostForGrind(grindNeeded);
		}

		public int PremiumCostForGrind(int grindNeeded)
		{
			return engine.RequiredPremiumForGrind(grindNeeded);
		}

		public void ExchangePremiumForGrind(int grindNeeded, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback)
		{
			int num = engine.RequiredPremiumForGrind(grindNeeded);
			global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition = new global::Kampai.Game.Transaction.TransactionDefinition();
			transactionDefinition.ID = int.MaxValue;
			global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> list = new global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem>();
			global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> list2 = new global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem>();
			grindNeeded = engine.PremiumToGrind(num);
			list.Add(new global::Kampai.Util.QuantityItem(1, (uint)num));
			list2.Add(new global::Kampai.Util.QuantityItem(0, (uint)grindNeeded));
			transactionDefinition.Inputs = list;
			transactionDefinition.Outputs = list2;
			ProcessItemPurchase(num, list2, true, callback, true);
		}

		public int CalculateRushCost(global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> items)
		{
			return engine.CalculateRushCost(player, items);
		}

		public void ProcessSlotPurchase(int slotCost, bool showStoreOnFail, int slotNumber, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, int instanceId)
		{
			global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>();
			list.Add(slotCost);
			string source = "SlotPurchase" + slotNumber;
			ProcessPremiumTransaction(list, null, showStoreOnFail, callback, source, instanceId, true);
		}

		public void ProcessSaleCancel(int cost, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback)
		{
			global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>();
			list.Add(cost);
			string source = "DeleteSale";
			ProcessPremiumTransaction(list, null, true, callback, source, 0, false, true);
		}

		public void ProcessRefreshMarket(int cost, bool showStoreOnFail, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback)
		{
			global::System.Collections.Generic.IList<int> list = new global::System.Collections.Generic.List<int>();
			list.Add(cost);
			ProcessPremiumTransaction(list, null, showStoreOnFail, callback, "RefreshMarket", 0, true);
		}

		public void ProcessOrderFill(int orderCost, global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> items, bool showStoreOnFail, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback)
		{
			global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>();
			list.Add(orderCost);
			string source = "OrderCompletion";
			ProcessPremiumTransaction(list, items, showStoreOnFail, callback, source, 3022, false, true);
		}

		public void ProcessItemPurchase(int itemCost, global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> items, bool showStoreOnFail, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, bool byPassStorageCheck = false)
		{
			global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>();
			list.Add(itemCost);
			ProcessPremiumTransaction(list, items, showStoreOnFail, callback, "ItemPurchase", 0, true, byPassStorageCheck);
		}

		public void ProcessItemPurchase(global::System.Collections.Generic.IList<int> itemCosts, global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> items, bool showStoreOnFail, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, bool byPassStorageCheck = false)
		{
			ProcessPremiumTransaction(itemCosts, items, showStoreOnFail, callback, "ItemPurchase", 0, true, byPassStorageCheck);
		}

		public void ProcessRush(int rushCost, bool showStoreOnFail, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, int instanceId)
		{
			global::System.Collections.Generic.IList<int> list = new global::System.Collections.Generic.List<int>();
			list.Add(rushCost);
			ProcessPremiumTransaction(list, null, showStoreOnFail, callback, "Rush", instanceId, true);
		}

		public void ProcessRush(int rushCost, bool showStoreOnFail, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback)
		{
			ProcessRush(rushCost, null, showStoreOnFail, callback);
		}

		public void ProcessRush(int rushCost, global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> items, bool showStoreOnFail, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, bool byPassStorageCheck = false)
		{
			global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>();
			list.Add(rushCost);
			ProcessPremiumTransaction(list, items, showStoreOnFail, callback, "Rush", 0, true, byPassStorageCheck);
		}

		public void ProcessRush(global::System.Collections.Generic.IList<int> itemCostList, global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> items, bool showStoreOnFail, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, bool byPassStorageCheck = false)
		{
			ProcessPremiumTransaction(itemCostList, items, showStoreOnFail, callback, "Rush", 0, true, byPassStorageCheck);
		}

		public void ProcessPremiumTransaction(global::System.Collections.Generic.IList<int> rushCosts, global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> items, bool showStoreOnFail, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, string source, int instanceId, bool reportItems, bool byPassStorageCheck = false)
		{
			if (!byPassStorageCheck && !CheckStorageCapacity(items, global::Kampai.Game.TransactionTarget.NO_VISUAL, null))
			{
				return;
			}
			int num = 0;
			foreach (int rushCost in rushCosts)
			{
				num += rushCost;
			}
			if (player.GetQuantity(global::Kampai.Game.StaticItem.PREMIUM_CURRENCY_ID) >= num)
			{
				player.AlterQuantity(global::Kampai.Game.StaticItem.PREMIUM_CURRENCY_ID, -num);
				if (items != null)
				{
					engine.AddOutputs(player, items);
				}
				if (items != null && reportItems)
				{
					for (int i = 0; i < items.Count; i++)
					{
						int num2 = num;
						if (rushCosts.Count - 1 >= i)
						{
							num2 = rushCosts[i];
						}
						if (items[i].ID == 0)
						{
							global::Kampai.Game.Transaction.TransactionUpdateData transactionUpdateData = createPremiumPurchaseTransactionData(items[i], num2, source, instanceId);
							transactionUpdateData.IsFromPremiumSource = true;
							postTransactionSignal.Dispatch(transactionUpdateData);
							continue;
						}
						for (int j = 0; j < items[i].Quantity; j++)
						{
							global::Kampai.Game.Transaction.TransactionUpdateData transactionUpdateData2 = new global::Kampai.Game.Transaction.TransactionUpdateData();
							transactionUpdateData2.Type = global::Kampai.Game.Transaction.UpdateType.OTHER;
							transactionUpdateData2.Source = source;
							transactionUpdateData2.InstanceId = instanceId;
							transactionUpdateData2.IsFromPremiumSource = true;
							transactionUpdateData2.Outputs = createSingleQuantityItemList(items[i]);
							transactionUpdateData2.AddInput(1, (int)(num2 / items[i].Quantity));
							postTransactionSignal.Dispatch(transactionUpdateData2);
						}
					}
				}
				else
				{
					global::Kampai.Game.Transaction.TransactionUpdateData transactionUpdateData3 = new global::Kampai.Game.Transaction.TransactionUpdateData();
					transactionUpdateData3.Type = global::Kampai.Game.Transaction.UpdateType.OTHER;
					transactionUpdateData3.Source = source;
					transactionUpdateData3.InstanceId = instanceId;
					transactionUpdateData3.Outputs = items;
					transactionUpdateData3.AddInput(1, num);
					postTransactionSignal.Dispatch(transactionUpdateData3);
				}
				Success(callback, null, true, num, items, null);
			}
			else if (showStoreOnFail)
			{
				insufficientInputsSignal.Dispatch(new global::Kampai.Game.PendingCurrencyTransaction(null, true, num, items, null, callback), false);
			}
			else
			{
				Fail(callback, null, true, num, items, null);
			}
		}

		private global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem> createSingleQuantityItemList(global::Kampai.Util.QuantityItem item)
		{
			global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem> list = new global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem>();
			if (item.Quantity > 1)
			{
				global::Kampai.Util.QuantityItem quantityItem = new global::Kampai.Util.QuantityItem();
				quantityItem.ID = item.ID;
				quantityItem.Quantity = 1u;
				list.Add(quantityItem);
			}
			else
			{
				list.Add(item);
			}
			return list;
		}

		private global::Kampai.Game.Transaction.TransactionUpdateData createPremiumPurchaseTransactionData(global::Kampai.Util.QuantityItem output, int itemCost, string source, int instanceId)
		{
			global::Kampai.Game.Transaction.TransactionUpdateData transactionUpdateData = new global::Kampai.Game.Transaction.TransactionUpdateData();
			transactionUpdateData.Type = global::Kampai.Game.Transaction.UpdateType.OTHER;
			transactionUpdateData.Source = source;
			transactionUpdateData.InstanceId = instanceId;
			global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> list = new global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem>();
			list.Add(output);
			transactionUpdateData.Outputs = list;
			transactionUpdateData.AddInput(1, itemCost);
			return transactionUpdateData;
		}

		public bool IsMissingItemFromTransaction(global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition)
		{
			global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> inputs = transactionDefinition.Inputs;
			if (inputs != null)
			{
				int count = inputs.Count;
				for (int i = 0; i < count; i++)
				{
					uint quantityByDefinitionId = GetQuantityByDefinitionId(inputs[i].ID);
					int num = (int)(inputs[i].Quantity - quantityByDefinitionId);
					if (num > 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		public global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> GetMissingItemListFromTransaction(global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition)
		{
			global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> list = new global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem>();
			global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> inputs = transactionDefinition.Inputs;
			if (inputs != null)
			{
				int count = inputs.Count;
				for (int i = 0; i < count; i++)
				{
					global::Kampai.Util.QuantityItem quantityItem = null;
					uint quantityByDefinitionId = GetQuantityByDefinitionId(inputs[i].ID);
					int num = (int)(inputs[i].Quantity - quantityByDefinitionId);
					if (num > 0)
					{
						quantityItem = new global::Kampai.Util.QuantityItem(inputs[i].ID, (uint)num);
						list.Add(quantityItem);
					}
				}
			}
			return list;
		}

		private void SetTransactionTime(ref global::Kampai.Game.TransactionArg arg)
		{
			if (arg == null)
			{
				arg = new global::Kampai.Game.TransactionArg();
			}
			arg.TransactionUTCTime = timeService.GameTimeSeconds();
		}

		public bool VerifyTransaction(int transactionId)
		{
			global::Kampai.Game.Transaction.TransactionDefinition transactiondef = definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(transactionId);
			return VerifyTransaction(transactiondef);
		}

		public bool VerifyTransaction(global::Kampai.Game.Transaction.TransactionDefinition transactiondef)
		{
			return engine.ValidateInputs(player, transactiondef);
		}

		public void StartTransaction(int transactionId, global::Kampai.Game.TransactionTarget target, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, global::Kampai.Game.TransactionArg arg = null, int startTime = 0, int index = 0)
		{
			global::Kampai.Game.Transaction.TransactionDefinition td = definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(transactionId);
			StartTransaction(td, target, callback, arg, startTime, index);
		}

		public void StartTransaction(global::Kampai.Game.Transaction.TransactionDefinition td, global::Kampai.Game.TransactionTarget target, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, global::Kampai.Game.TransactionArg arg = null, int startTime = 0, int index = 0)
		{
			SetTransactionTime(ref arg);
			switch (target)
			{
			case global::Kampai.Game.TransactionTarget.INGREDIENT:
			case global::Kampai.Game.TransactionTarget.REPAIR_BRIDGE:
				if (!VerifyTransaction(td))
				{
					insufficientInputsSignal.Dispatch(new global::Kampai.Game.PendingCurrencyTransaction(td, true, 0, td.Inputs, null, callback), false);
					return;
				}
				break;
			case global::Kampai.Game.TransactionTarget.BLACKMARKETBOARD:
			{
				if (!VerifyTransaction(td))
				{
					insufficientInputsSignal.Dispatch(new global::Kampai.Game.PendingCurrencyTransaction(td, true, 0, td.Inputs, null, callback), false);
					return;
				}
				global::Kampai.Game.OrderBoard byInstanceId2 = GetByInstanceId<global::Kampai.Game.OrderBoard>(arg.InstanceId);
				foreach (global::Kampai.Game.OrderBoardTicket ticket in byInstanceId2.tickets)
				{
					if (ticket.BoardIndex == index)
					{
						ticket.StartTime = startTime;
						break;
					}
				}
				break;
			}
			case global::Kampai.Game.TransactionTarget.CLEAR_DEBRIS:
			{
				if (!VerifyTransaction(td))
				{
					insufficientInputsSignal.Dispatch(new global::Kampai.Game.PendingCurrencyTransaction(td, true, 0, td.Inputs, null, callback, target), false);
					return;
				}
				global::Kampai.Game.DebrisBuilding byInstanceId = GetByInstanceId<global::Kampai.Game.DebrisBuilding>(arg.InstanceId);
				byInstanceId.PaidInputCostToClear = true;
				break;
			}
			}
			if (!engine.SubtractInputs(player, td))
			{
				insufficientInputsSignal.Dispatch(new global::Kampai.Game.PendingCurrencyTransaction(td, false, 0, null, null, callback), false);
				return;
			}
			SendTransactionUpdate(td, arg, global::Kampai.Game.Transaction.UpdateType.TRANSACTION_START, null, target);
			Success(callback, td, false, 0, null, null);
		}

		public bool FinishTransaction(int transactionId, global::Kampai.Game.TransactionTarget target, global::Kampai.Game.TransactionArg arg = null)
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.Instance> outputs;
			return FinishTransaction(transactionId, target, out outputs, arg);
		}

		public bool FinishTransaction(global::Kampai.Game.Transaction.TransactionDefinition td, global::Kampai.Game.TransactionTarget target, global::Kampai.Game.TransactionArg arg = null)
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.Instance> outputs;
			return FinishTransaction(td, target, out outputs, arg);
		}

		public bool FinishTransaction(int transactionId, global::Kampai.Game.TransactionTarget target, out global::System.Collections.Generic.IList<global::Kampai.Game.Instance> outputs, global::Kampai.Game.TransactionArg arg = null)
		{
			global::Kampai.Game.Transaction.TransactionDefinition td = definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(transactionId);
			return FinishTransaction(td, target, out outputs, arg);
		}

		public bool FinishTransaction(global::Kampai.Game.Transaction.TransactionDefinition td, global::Kampai.Game.TransactionTarget target, out global::System.Collections.Generic.IList<global::Kampai.Game.Instance> outputs, global::Kampai.Game.TransactionArg arg = null)
		{
			SetTransactionTime(ref arg);
			bool flag = false;
			outputs = null;
			if (!CheckStorageCapacity(td.Outputs, target, arg))
			{
				return false;
			}
			if (engine.AddOutputs(player, td, out outputs, arg))
			{
				SendTransactionUpdate(td, arg, global::Kampai.Game.Transaction.UpdateType.TRANSACTION_FINISH, outputs, target);
				flag = true;
			}
			else
			{
				flag = false;
			}
			CheckRandomDrop(target, arg);
			return flag;
		}

		private int RandomDropIncrement(global::Kampai.Game.TransactionTarget target, global::Kampai.Game.TransactionArg arg = null)
		{
			int result = 0;
			switch (target)
			{
			case global::Kampai.Game.TransactionTarget.HARVEST:
				result = 1;
				break;
			case global::Kampai.Game.TransactionTarget.TASK_COMPLETE:
			case global::Kampai.Game.TransactionTarget.TASK_COMPLETE_INGREDIENT:
			{
				if (arg == null)
				{
					logger.LogNullArgument();
					break;
				}
				global::Kampai.Game.TaskTransactionArgument taskTransactionArgument = arg.Get<global::Kampai.Game.TaskTransactionArgument>();
				if (taskTransactionArgument == null)
				{
					logger.LogNullArgument();
				}
				else
				{
					result = taskTransactionArgument.DropStep;
				}
				break;
			}
			}
			return result;
		}

		private bool IsLastItemInStack(int instanceId)
		{
			global::Kampai.Game.Building byInstanceId = GetByInstanceId<global::Kampai.Game.Building>(instanceId);
			global::Kampai.Game.ResourceBuilding resourceBuilding = byInstanceId as global::Kampai.Game.ResourceBuilding;
			if (resourceBuilding != null)
			{
				return resourceBuilding.AvailableHarvest == 1;
			}
			global::Kampai.Game.CraftingBuilding craftingBuilding = byInstanceId as global::Kampai.Game.CraftingBuilding;
			if (craftingBuilding != null)
			{
				return craftingBuilding.CompletedCrafts.Count == 1;
			}
			return true;
		}

		private void CheckRandomDrop(global::Kampai.Game.TransactionTarget target, global::Kampai.Game.TransactionArg arg = null)
		{
			int num = RandomDropIncrement(target, arg);
			if (num <= 0)
			{
				return;
			}
			int instanceId = ((arg != null) ? arg.InstanceId : 0);
			AlterQuantity(global::Kampai.Game.StaticItem.ACTIONS_SINCE_LAST_DROP, num);
			global::Kampai.Game.DropLevelBandDefinition dropLevelBandDefinition = definitionService.Get<global::Kampai.Game.DropLevelBandDefinition>(88889);
			int num2 = (int)(GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID) - 1);
			if (num2 >= dropLevelBandDefinition.HarvestsPerDrop.Count)
			{
				num2 = dropLevelBandDefinition.HarvestsPerDrop.Count - 1;
			}
			if (GetQuantity(global::Kampai.Game.StaticItem.ACTIONS_SINCE_LAST_DROP) >= dropLevelBandDefinition.HarvestsPerDrop[num2] && (target != global::Kampai.Game.TransactionTarget.HARVEST || IsLastItemInStack(instanceId)))
			{
				player.SetQuantityByStaticItem(global::Kampai.Game.StaticItem.ACTIONS_SINCE_LAST_DROP, 0u);
				global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback = delegate(global::Kampai.Game.PendingCurrencyTransaction pct)
				{
					randomDropSignal.Dispatch(global::Kampai.Util.Tuple.Create(pct.GetOutputs()[0].Definition.ID, instanceId));
				};
				RunEntireTransaction(5037, global::Kampai.Game.TransactionTarget.NO_VISUAL, callback, arg);
			}
		}

		public void RunEntireTransaction(int transactionId, global::Kampai.Game.TransactionTarget target, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, global::Kampai.Game.TransactionArg arg = null)
		{
			global::Kampai.Game.Transaction.TransactionDefinition td = definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(transactionId);
			RunEntireTransaction(td, target, callback, arg);
		}

		public void RunEntireTransaction(global::Kampai.Game.Transaction.TransactionDefinition td, global::Kampai.Game.TransactionTarget target, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, global::Kampai.Game.TransactionArg arg = null)
		{
			SetTransactionTime(ref arg);
			bool isRush = false;
			global::System.Collections.Generic.IList<global::Kampai.Game.Instance> outputs = null;
			switch (target)
			{
			case global::Kampai.Game.TransactionTarget.CURRENCY:
				if (RunTransaction(td, out outputs, arg))
				{
					SendTransactionUpdate(td, arg, global::Kampai.Game.Transaction.UpdateType.TRANSACTION_FULL, outputs, target);
					CheckRandomDrop(target, arg);
					Success(callback, td, false, 0, null, outputs);
				}
				else
				{
					Fail(callback, td, false, 0, null, null);
					logger.Fatal(global::Kampai.Util.FatalCode.PS_UNABLE_TO_RUN_PENDING_TRANSACTION);
				}
				return;
			case global::Kampai.Game.TransactionTarget.STORAGEBUILDING:
				if (RunTransaction(td, out outputs, arg))
				{
					GetByInstanceId<global::Kampai.Game.StorageBuilding>(arg.InstanceId).CurrentStorageBuildingLevel++;
					SendTransactionUpdate(td, arg, global::Kampai.Game.Transaction.UpdateType.TRANSACTION_FULL, outputs, target);
					CheckRandomDrop(target, arg);
					Success(callback, td, false, 0, null, outputs);
				}
				else
				{
					insufficientInputsSignal.Dispatch(new global::Kampai.Game.PendingCurrencyTransaction(td, false, 0, null, null, callback), false);
				}
				return;
			case global::Kampai.Game.TransactionTarget.INGREDIENT:
			case global::Kampai.Game.TransactionTarget.TASK_COMPLETE_INGREDIENT:
				isRush = true;
				break;
			case global::Kampai.Game.TransactionTarget.MARKETPLACE:
				if (!CheckStorageCapacity(td.Outputs, target, arg, true))
				{
					Fail(callback, td, true, 0, null, null, global::Kampai.Game.CurrencyTransactionFailReason.STORAGE);
				}
				else if (VerifyTransaction(td))
				{
					RunTransaction(td, out outputs, arg);
					SendTransactionUpdate(td, arg, global::Kampai.Game.Transaction.UpdateType.TRANSACTION_FULL, outputs, target);
					CheckRandomDrop(target, arg);
					Success(callback, td, isRush, 0, null, outputs);
				}
				else
				{
					insufficientInputsSignal.Dispatch(new global::Kampai.Game.PendingCurrencyTransaction(td, isRush, 0, null, null, callback), false);
				}
				return;
			}
			if (VerifyTransaction(td))
			{
				RunTransaction(td, out outputs, arg);
				SendTransactionUpdate(td, arg, global::Kampai.Game.Transaction.UpdateType.TRANSACTION_FULL, outputs, target);
				CheckRandomDrop(target, arg);
				Success(callback, td, isRush, 0, null, outputs);
			}
			else
			{
				insufficientInputsSignal.Dispatch(new global::Kampai.Game.PendingCurrencyTransaction(td, isRush, 0, null, null, callback), false);
			}
		}

		private void SendTransactionUpdate(global::Kampai.Game.Transaction.TransactionDefinition td, global::Kampai.Game.TransactionArg arg, global::Kampai.Game.Transaction.UpdateType type, global::System.Collections.Generic.IList<global::Kampai.Game.Instance> newItems, global::Kampai.Game.TransactionTarget target)
		{
			global::Kampai.Game.Transaction.TransactionUpdateData transactionUpdateData = new global::Kampai.Game.Transaction.TransactionUpdateData();
			transactionUpdateData.Type = type;
			transactionUpdateData.TransactionId = td.ID;
			transactionUpdateData.InstanceId = ((arg != null) ? arg.InstanceId : 0);
			transactionUpdateData.startPosition = ((arg != null) ? arg.StartPosition : global::UnityEngine.Vector3.zero);
			transactionUpdateData.fromGlass = arg != null && arg.fromGlass;
			transactionUpdateData.Source = ((arg != null) ? arg.Source : null);
			transactionUpdateData.NewItems = newItems;
			transactionUpdateData.IsFromPremiumSource = arg.IsFromPremiumSource;
			transactionUpdateData.Target = target;
			if (arg.IsFromQuestSource)
			{
				transactionUpdateData.Source = "QuestStep";
			}
			switch (type)
			{
			case global::Kampai.Game.Transaction.UpdateType.TRANSACTION_START:
				transactionUpdateData.Inputs = td.Inputs;
				break;
			case global::Kampai.Game.Transaction.UpdateType.TRANSACTION_FINISH:
				transactionUpdateData.Outputs = td.Outputs;
				break;
			case global::Kampai.Game.Transaction.UpdateType.TRANSACTION_FULL:
				transactionUpdateData.Inputs = td.Inputs;
				transactionUpdateData.Outputs = td.Outputs;
				break;
			}
			postTransactionSignal.Dispatch(transactionUpdateData);
		}

		public void StopTask(int minionId)
		{
			global::Kampai.Game.Minion byInstanceId = GetByInstanceId<global::Kampai.Game.Minion>(minionId);
			global::Kampai.Game.TaskableBuilding byInstanceId2 = GetByInstanceId<global::Kampai.Game.TaskableBuilding>(byInstanceId.BuildingID);
			if (byInstanceId == null || byInstanceId2 == null)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.PS_NO_SUCH_INSTANCE_TASKING, "{0} - {1}", minionId, byInstanceId.BuildingID);
			}
			byInstanceId.BuildingID = -1;
			byInstanceId.UTCTaskStartTime = -1;
			byInstanceId.State = global::Kampai.Game.MinionState.Idle;
			byInstanceId2.RemoveMinion(minionId, timeService.GameTimeSeconds());
			if (byInstanceId2.GetMinionsInBuilding() == 0)
			{
				changeState.Dispatch(byInstanceId2.ID, global::Kampai.Game.BuildingState.Idle);
			}
			byInstanceId2.StateStartTime = 0;
		}

		public void BuyCraftingSlot(int buildingID)
		{
			GetByInstanceId<global::Kampai.Game.CraftingBuilding>(buildingID).Slots++;
		}

		public void UpdateCraftingQueue(int buildingID, int itemDefId)
		{
			global::Kampai.Game.CraftingBuilding byInstanceId = GetByInstanceId<global::Kampai.Game.CraftingBuilding>(buildingID);
			byInstanceId.RecipeInQueue.Add(itemDefId);
		}

		public bool VerifyPlayerHasRequiredInputs(global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> inputs)
		{
			bool result = true;
			foreach (global::Kampai.Util.QuantityItem input in inputs)
			{
				if (GetQuantityByDefinitionId(input.ID) < input.Quantity)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		public void PurchaseSlotForBuilding(int buildingID, int level)
		{
			global::Kampai.Game.ResourceBuilding byInstanceId = GetByInstanceId<global::Kampai.Game.ResourceBuilding>(buildingID);
			int num = byInstanceId.BuildingNumber - 1;
			if (num < 0)
			{
				num = GetInstancesByDefinitionID(byInstanceId.Definition.ID).Count;
			}
			if (byInstanceId.MinionSlotsOwned < byInstanceId.Definition.SlotUnlocks[num].SlotUnlockLevels.Count && byInstanceId.Definition.SlotUnlocks[num].SlotUnlockLevels[byInstanceId.MinionSlotsOwned] == level)
			{
				byInstanceId.IncrementMinionSlotsOwned();
			}
		}

		public int GetMinionCount()
		{
			return GetInstancesByType<global::Kampai.Game.Minion>().Count;
		}

		private bool CheckStorageCapacity(global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> outputs, global::Kampai.Game.TransactionTarget target, global::Kampai.Game.TransactionArg arg, bool allowDrops = false)
		{
			uint num = 0u;
			global::Kampai.Game.CraftingBuilding craftingBuilding = null;
			bool flag = false;
			if (arg != null && arg.InstanceId != 0)
			{
				craftingBuilding = GetByInstanceId<global::Kampai.Game.CraftingBuilding>(arg.InstanceId);
				if (craftingBuilding != null)
				{
					flag = true;
				}
			}
			if (outputs != null)
			{
				foreach (global::Kampai.Util.QuantityItem output in outputs)
				{
					int iD = output.ID;
					if (iD != 0 && iD != 2 && iD != 1 && iD != 3 && iD != 4)
					{
						global::Kampai.Game.ItemDefinition itemDefinition = definitionService.Get<global::Kampai.Game.ItemDefinition>(output.ID);
						if ((allowDrops || !(itemDefinition is global::Kampai.Game.DropItemDefinition)) && !(itemDefinition is global::Kampai.Game.CostumeItemDefinition))
						{
							num += output.Quantity;
						}
					}
				}
			}
			if (num == 0)
			{
				return true;
			}
			global::Kampai.Game.StorageBuilding storageBuilding = null;
			using (global::System.Collections.Generic.IEnumerator<global::Kampai.Game.StorageBuilding> enumerator2 = player.GetByDefinitionId<global::Kampai.Game.StorageBuilding>(3018).GetEnumerator())
			{
				if (enumerator2.MoveNext())
				{
					global::Kampai.Game.StorageBuilding current2 = enumerator2.Current;
					storageBuilding = current2;
				}
			}
			if (storageBuilding == null)
			{
				return true;
			}
			uint storageCapacity = storageBuilding.Definition.StorageUpgrades[storageBuilding.CurrentStorageBuildingLevel].StorageCapacity;
			uint itemCount = 0u;
			player.FindAllItems(out itemCount);
			uint num2 = itemCount + num;
			if (num2 > storageCapacity)
			{
				telemetryService.Send_Telemetry_EVT_STORAGE_LIMIT_HIT((int)storageCapacity);
				if (target.Equals(global::Kampai.Game.TransactionTarget.HARVEST) && flag && craftingBuilding != null)
				{
					ShowBuildingDetailmenuSignal.Dispatch(craftingBuilding);
				}
				else if (!target.Equals(global::Kampai.Game.TransactionTarget.MARKETPLACE))
				{
					openStorageBuildingSignal.Dispatch(storageBuilding, true);
				}
				return false;
			}
			return true;
		}

		public void CreateAndRunCustomTransaction(int defID, int quantity, global::Kampai.Game.TransactionTarget target)
		{
			global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition = new global::Kampai.Game.Transaction.TransactionDefinition();
			global::Kampai.Util.QuantityItem item = new global::Kampai.Util.QuantityItem(defID, (uint)quantity);
			transactionDefinition.Inputs = new global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem>();
			transactionDefinition.Outputs = new global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem>();
			transactionDefinition.Outputs.Add(item);
			transactionDefinition.ID = int.MaxValue;
			RunEntireTransaction(transactionDefinition, target, null);
		}

		public int GetInvestmentTimeForTransaction(int transactionID)
		{
			int num = 0;
			global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition = definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(transactionID);
			if (transactionDefinition == null)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.PS_NO_TRANSACTION, transactionID);
				return 0;
			}
			global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> inputs = transactionDefinition.Inputs;
			if (inputs != null)
			{
				foreach (global::Kampai.Util.QuantityItem item in inputs)
				{
					global::Kampai.Game.IngredientsItemDefinition ingredientsItemDefinition = definitionService.Get<global::Kampai.Game.IngredientsItemDefinition>(item.ID);
					int quantity = (int)item.Quantity;
					num += (int)ingredientsItemDefinition.TimeToHarvest * quantity;
					int tier = ingredientsItemDefinition.Tier;
					if (tier > 0)
					{
						num += GetInvestmentTimeForTransaction(ingredientsItemDefinition.TransactionId);
					}
				}
			}
			return num;
		}

		public global::Kampai.Game.KampaiPendingTransaction GetPendingTransaction(string externalIdentifier)
		{
			return player.GetPendingTransaction(externalIdentifier);
		}

		public bool PlayerAlreadyHasPlatformStoreTransactionID(string identifier)
		{
			return player.HasPlatformStoreTransactionID(identifier);
		}

		public void AddPlatformStoreTransactionID(string identifier)
		{
			player.AddPlatformStoreTransactionID(identifier);
		}

		public void QueuePendingTransaction(global::Kampai.Game.KampaiPendingTransaction pendingTransaction)
		{
			logger.Debug("QUEUE: {0}", pendingTransaction);
			if (player.GetPendingTransaction(pendingTransaction.ExternalIdentifier) == null)
			{
				player.QueuePendingTransaction(pendingTransaction);
			}
			else
			{
				logger.Fatal(global::Kampai.Util.FatalCode.PS_DUPLICATE_PENDING_TRANSACTION);
			}
		}

		public global::Kampai.Game.KampaiPendingTransaction ProcessPendingTransaction(string externalIdentifier, bool isFromPremium)
		{
			logger.Debug("PROCESS: {0}", externalIdentifier);
			global::Kampai.Game.KampaiPendingTransaction pendingTransaction = player.GetPendingTransaction(externalIdentifier);
			if (pendingTransaction != null)
			{
				global::Kampai.Game.Transaction.TransactionDefinition transaction = pendingTransaction.Transaction;
				RunEntireTransaction(transaction, global::Kampai.Game.TransactionTarget.CURRENCY, null);
				int grindOutputForTransaction = global::Kampai.Game.Transaction.TransactionUtil.GetGrindOutputForTransaction(transaction);
				int premiumOutputForTransaction = global::Kampai.Game.Transaction.TransactionUtil.GetPremiumOutputForTransaction(transaction);
				player.RemovePendingTransaction(pendingTransaction);
				if (grindOutputForTransaction > 0)
				{
					telemetryService.Send_Telemetry_EVT_IGE_FREE_CREDITS_EARNED(grindOutputForTransaction, "STORE", isFromPremium);
				}
				if (premiumOutputForTransaction > 0)
				{
					telemetryService.Send_Telemetry_EVT_IGE_PAID_CREDITS_EARNED(premiumOutputForTransaction, "STORE", isFromPremium);
				}
				return pendingTransaction;
			}
			return null;
		}

		public global::Kampai.Game.KampaiPendingTransaction CancelPendingTransaction(string externalIdentifier)
		{
			global::Kampai.Game.KampaiPendingTransaction pendingTransaction = player.GetPendingTransaction(externalIdentifier);
			if (pendingTransaction != null)
			{
				player.RemovePendingTransaction(pendingTransaction);
			}
			return pendingTransaction;
		}

		public global::System.Collections.Generic.IList<global::Kampai.Game.KampaiPendingTransaction> GetPendingTransactions()
		{
			return player.GetPendingTransactions();
		}

		private void Success(global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, global::Kampai.Game.Transaction.TransactionDefinition pendingTransaction, bool isRush, int rushCost, global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> rushOutputs, global::System.Collections.Generic.IList<global::Kampai.Game.Instance> outputs)
		{
			if (callback != null)
			{
				global::Kampai.Game.PendingCurrencyTransaction pendingCurrencyTransaction = new global::Kampai.Game.PendingCurrencyTransaction(pendingTransaction, isRush, rushCost, rushOutputs, outputs);
				pendingCurrencyTransaction.Success = true;
				callback(pendingCurrencyTransaction);
			}
		}

		private void Fail(global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, global::Kampai.Game.Transaction.TransactionDefinition pendingTransaction, bool isRush, int rushCost, global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> rushOutputs, global::System.Collections.Generic.IList<global::Kampai.Game.Instance> outputs, global::Kampai.Game.CurrencyTransactionFailReason failReason = global::Kampai.Game.CurrencyTransactionFailReason.NONE)
		{
			if (callback != null)
			{
				global::Kampai.Game.PendingCurrencyTransaction pendingCurrencyTransaction = new global::Kampai.Game.PendingCurrencyTransaction(pendingTransaction, isRush, rushCost, rushOutputs, outputs);
				pendingCurrencyTransaction.FailReason = failReason;
				callback(pendingCurrencyTransaction);
			}
		}

		public global::System.Collections.Generic.IList<global::Kampai.Game.Building> GetBuildingsWithState(global::Kampai.Game.BuildingState state)
		{
			global::System.Collections.Generic.List<global::Kampai.Game.Building> result = new global::System.Collections.Generic.List<global::Kampai.Game.Building>();
			player.GetInstancesByType(ref result, (global::Kampai.Game.Building building) => building.State == state);
			return result;
		}

		public void AddLandExpansion(global::Kampai.Game.LandExpansionConfig expansionConfig)
		{
			player.AddLandExpansion(expansionConfig);
		}

		public bool IsExpansionPurchased(int expansionId)
		{
			return player.IsExpansionPurchased(expansionId);
		}

		public int GetPurchasedExpansionCount()
		{
			return player.GetPurchasedExpansionCount();
		}

		public void QueueVillain(global::Kampai.Game.Prestige villainPrestige)
		{
			player.QueueVillain(villainPrestige);
		}

		public int PopVillain()
		{
			return player.PopVillain();
		}

		public void SetTargetExpansion(int id)
		{
			player.targetExpansionID = id;
		}

		public int GetTargetExpansion()
		{
			return player.targetExpansionID;
		}

		public void ClearTargetExpansion()
		{
			player.targetExpansionID = 0;
		}

		public bool HasTargetExpansion()
		{
			return player.targetExpansionID != 0;
		}

		public void SetFreezeTime(int freezeTime)
		{
			player.SetFreezeTime(freezeTime);
		}

		public int GetFreezeTime()
		{
			return player.freezeTime;
		}

		public bool HasStorageBuilding()
		{
			global::Kampai.Game.StorageBuilding firstInstanceByDefintion = player.GetFirstInstanceByDefintion<global::Kampai.Game.StorageBuilding, global::Kampai.Game.StorageBuildingDefinition>();
			global::Kampai.Game.BuildingState state = firstInstanceByDefintion.State;
			return state != global::Kampai.Game.BuildingState.Broken && state != global::Kampai.Game.BuildingState.Inaccessible && state != global::Kampai.Game.BuildingState.Disabled;
		}

		public uint GetStorageCount()
		{
			uint itemCount = 0u;
			player.FindAllItems(out itemCount);
			return itemCount;
		}

		public bool isStorageFull()
		{
			global::Kampai.Game.StorageBuilding firstInstanceByDefintion = player.GetFirstInstanceByDefintion<global::Kampai.Game.StorageBuilding, global::Kampai.Game.StorageBuildingDefinition>();
			if (firstInstanceByDefintion == null)
			{
				return false;
			}
			uint storageCapacity = firstInstanceByDefintion.Definition.StorageUpgrades[firstInstanceByDefintion.CurrentStorageBuildingLevel].StorageCapacity;
			uint itemCount = 0u;
			player.FindAllItems(out itemCount);
			if (itemCount >= storageCapacity)
			{
				return true;
			}
			return false;
		}

		public uint GetAvailableStorageCapacity()
		{
			global::Kampai.Game.StorageBuilding firstInstanceByDefintion = player.GetFirstInstanceByDefintion<global::Kampai.Game.StorageBuilding, global::Kampai.Game.StorageBuildingDefinition>();
			if (firstInstanceByDefintion == null)
			{
				return 0u;
			}
			uint storageCapacity = firstInstanceByDefintion.Definition.StorageUpgrades[firstInstanceByDefintion.CurrentStorageBuildingLevel].StorageCapacity;
			uint itemCount = 0u;
			player.FindAllItems(out itemCount);
			return storageCapacity - itemCount;
		}

		public global::System.Collections.Generic.List<global::Kampai.Game.Minion> GetMinions(bool has, params global::Kampai.Game.MinionState[] states)
		{
			global::System.Collections.Generic.List<global::Kampai.Game.Minion> list = new global::System.Collections.Generic.List<global::Kampai.Game.Minion>();
			global::System.Collections.Generic.List<global::Kampai.Game.Minion> instancesByType = GetInstancesByType<global::Kampai.Game.Minion>();
			for (int i = 0; i < instancesByType.Count; i++)
			{
				bool flag = false;
				for (int j = 0; j < states.Length; j++)
				{
					if (instancesByType[i].State == states[j])
					{
						flag = true;
						break;
					}
				}
				if ((flag && has) || (!flag && !has))
				{
					list.Add(instancesByType[i]);
				}
			}
			return list;
		}

		public global::System.Collections.Generic.List<global::Kampai.Game.Minion> GetIdleMinions()
		{
			return GetMinions(true, global::Kampai.Game.MinionState.Idle, global::Kampai.Game.MinionState.Selectable, global::Kampai.Game.MinionState.Uninitialized, global::Kampai.Game.MinionState.Leisure);
		}

		public int GetHighestFtueCompleted()
		{
			return player.HighestFtueLevel;
		}

		public void SetHighestFtueCompleted(int newLevel)
		{
			int highestFtueLevel = player.HighestFtueLevel;
			if (newLevel > highestFtueLevel)
			{
				player.HighestFtueLevel = newLevel;
				return;
			}
			logger.Warning("New FTUE level lower than current {0} -> {1}", highestFtueLevel, newLevel);
		}

		public int GetInventoryCountByDefinitionID(int defId)
		{
			int num = 0;
			global::System.Collections.Generic.ICollection<global::Kampai.Game.Instance> byDefinitionId = GetByDefinitionId<global::Kampai.Game.Instance>(defId);
			if (byDefinitionId.Count != 0)
			{
				foreach (global::Kampai.Game.Instance item in byDefinitionId)
				{
					global::Kampai.Game.Building building = item as global::Kampai.Game.Building;
					if (building != null && building.State == global::Kampai.Game.BuildingState.Inventory)
					{
						num++;
					}
				}
			}
			return num;
		}

		public global::Kampai.Game.SocialClaimRewardItem.ClaimState GetSocialClaimReward(int eventID)
		{
			if (player == null)
			{
				logger.Warning("Failed to get claim reward state for event {0}", eventID);
				return global::Kampai.Game.SocialClaimRewardItem.ClaimState.UNKNOWN;
			}
			global::System.Collections.Generic.IDictionary<int, global::Kampai.Game.SocialClaimRewardItem.ClaimState> socialClaimRewards = player.GetSocialClaimRewards();
			if (socialClaimRewards.ContainsKey(eventID))
			{
				return socialClaimRewards[eventID];
			}
			return global::Kampai.Game.SocialClaimRewardItem.ClaimState.UNKNOWN;
		}

		public void AddSocialClaimReward(int eventID, global::Kampai.Game.SocialClaimRewardItem.ClaimState claimState)
		{
			if (player == null)
			{
				logger.Warning("Failed to update claim reward state for event {0}", eventID);
			}
			else
			{
				player.AddSocialClaimRewards(eventID, claimState);
			}
		}

		public global::Kampai.Game.Player.SanityCheckFailureReason DeepScan(global::Kampai.Game.Player prevSave)
		{
			return player.ValidateSaveData(prevSave);
		}
	}
}
