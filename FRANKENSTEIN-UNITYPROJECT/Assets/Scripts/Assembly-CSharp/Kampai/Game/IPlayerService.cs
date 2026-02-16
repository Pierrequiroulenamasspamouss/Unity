namespace Kampai.Game
{
	public interface IPlayerService
	{
		long ID { get; set; }

		global::Kampai.Game.Player LastSave { get; set; }

		int LevelUpUTC { get; set; }

		int LastGameStartUTC { get; set; }

		string SWRVEGroup { get; set; }

		int GameplaySecondsSinceLevelUp { get; set; }

		uint GetQuantity(global::Kampai.Game.StaticItem def);

		int GetCountByDefinitionId(int defId);

		uint GetQuantityByDefinitionId(int defId);

		uint GetQuantityByInstanceId(int instanceId);

		global::System.Collections.Generic.ICollection<int> GetAnimatingBuildingIDs();

		void AlterQuantity(global::Kampai.Game.StaticItem def, int amount);

		global::System.Collections.Generic.ICollection<global::Kampai.Game.Item> GetItems();

		global::System.Collections.Generic.Dictionary<int, int> GetBuildingOnBoardCountMap();

		global::System.Collections.Generic.ICollection<global::Kampai.Game.Item> GetItems(out uint itemCount);

		global::System.Collections.Generic.IList<global::Kampai.Game.IngredientsItemDefinition> GetAllUnLockedIngredients();

		global::System.Collections.Generic.IList<global::Kampai.Game.IngredientsItemDefinition> FindAllAvailableIngredients();

		int GetUnlockedQuantityOfID(int defId);

		global::Kampai.Game.Player LoadPlayerData(string serialized);

		void Deserialize(string serialized, bool isRetry = false);

		byte[] SavePlayerData(global::Kampai.Game.Player playerData);

		byte[] Serialize();

		bool IsPlayerInitialized();

		void Add(global::Kampai.Game.Instance i);

		void AssignNextInstanceId(global::Kampai.Util.Identifiable i);

		void Remove(global::Kampai.Game.Instance i);

		T GetByInstanceId<T>(int id) where T : class, global::Kampai.Game.Instance;

		T GetFirstInstanceByDefinitionId<T>(int definitionId) where T : class, global::Kampai.Game.Instance;

		global::System.Collections.Generic.ICollection<T> GetByDefinitionId<T>(int id) where T : global::Kampai.Game.Instance;

		global::Kampai.Game.Transaction.WeightedInstance GetWeightedInstance(int defId, global::Kampai.Game.Transaction.WeightedDefinition wd = null);

		int CalculateRushCost(global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> items);

		void ProcessSlotPurchase(int slotCost, bool showStoreOnFail, int slotNumber, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, int instanceId);

		void ProcessSaleCancel(int cost, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback);

		void ProcessRefreshMarket(int cost, bool showStoreOnFail, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback);

		void ProcessItemPurchase(int itemCost, global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> items, bool showStoreOnFail, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, bool byPassStorageCheck = false);

		void ProcessItemPurchase(global::System.Collections.Generic.IList<int> itemCosts, global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> items, bool showStoreOnFail, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, bool byPassStorageCheck = false);

		void ProcessOrderFill(int slotCost, global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> items, bool showStoreOnFail, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback);

		void ProcessRush(int rushCost, bool showStoreOnFail, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, int instanceId);

		void ProcessRush(int rushCost, bool showStoreOnFail, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback);

		void ProcessRush(int rushCost, global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> items, bool showStoreOnFail, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, bool byPassStorageCheck = false);

		bool IsMissingItemFromTransaction(global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition);

		global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> GetMissingItemListFromTransaction(global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition);

		void StartTransaction(int transactionId, global::Kampai.Game.TransactionTarget target, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, global::Kampai.Game.TransactionArg arg = null, int startTime = 0, int index = 0);

		void StartTransaction(global::Kampai.Game.Transaction.TransactionDefinition td, global::Kampai.Game.TransactionTarget target, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, global::Kampai.Game.TransactionArg arg = null, int startTime = 0, int index = 0);

		bool FinishTransaction(int transactionId, global::Kampai.Game.TransactionTarget target, global::Kampai.Game.TransactionArg arg = null);

		bool FinishTransaction(global::Kampai.Game.Transaction.TransactionDefinition td, global::Kampai.Game.TransactionTarget target, global::Kampai.Game.TransactionArg arg = null);

		bool FinishTransaction(int transactionId, global::Kampai.Game.TransactionTarget target, out global::System.Collections.Generic.IList<global::Kampai.Game.Instance> outputs, global::Kampai.Game.TransactionArg arg = null);

		void RunEntireTransaction(int transactionId, global::Kampai.Game.TransactionTarget target, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, global::Kampai.Game.TransactionArg arg = null);

		void RunEntireTransaction(global::Kampai.Game.Transaction.TransactionDefinition def, global::Kampai.Game.TransactionTarget target, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback, global::Kampai.Game.TransactionArg arg = null);

		bool VerifyTransaction(global::Kampai.Game.Transaction.TransactionDefinition transactiondef);

		bool VerifyTransaction(int transactionId);

		void StopTask(int minionId);

		void BuyCraftingSlot(int buildingID);

		void UpdateCraftingQueue(int buildingID, int itemDefId);

		bool VerifyPlayerHasRequiredInputs(global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> inputs);

		void PurchaseSlotForBuilding(int buildingID, int level);

		int GetMinionCount();

		void CreateAndRunCustomTransaction(int defID, int quantity, global::Kampai.Game.TransactionTarget target);

		global::Kampai.Game.KampaiPendingTransaction GetPendingTransaction(string externalIdentifier);

		bool PlayerAlreadyHasPlatformStoreTransactionID(string identifier);

		void AddPlatformStoreTransactionID(string identifier);

		global::System.Collections.Generic.IList<global::Kampai.Game.KampaiPendingTransaction> GetPendingTransactions();

		void QueuePendingTransaction(global::Kampai.Game.KampaiPendingTransaction pendingTransaction);

		global::Kampai.Game.KampaiPendingTransaction ProcessPendingTransaction(string externalIdentifier, bool isFromPremium);

		global::Kampai.Game.KampaiPendingTransaction CancelPendingTransaction(string externalIdentifier);

		void ExchangePremiumForGrind(int grindNeeded, global::System.Action<global::Kampai.Game.PendingCurrencyTransaction> callback);

		int PremiumCostForGrind(int grindNeeded);

		bool CanAffordExchange(int grindNeeded);

		int GetInvestmentTimeForTransaction(int transactionID);

		global::System.Collections.Generic.IList<global::Kampai.Game.Instance> GetInstancesByDefinition<T>() where T : global::Kampai.Game.Definition;

		I GetFirstInstanceByDefintion<I, D>() where I : class, global::Kampai.Game.Instance where D : global::Kampai.Game.Definition;

		global::System.Collections.Generic.IList<global::Kampai.Game.Instance> GetInstancesByDefinitionID(int defId);

		global::System.Collections.Generic.List<T> GetInstancesByType<T>() where T : class, global::Kampai.Game.Instance;

		void GetInstancesByType<T>(ref global::System.Collections.Generic.List<T> list) where T : class, global::Kampai.Game.Instance;

		global::System.Collections.Generic.IList<global::Kampai.Game.Item> GetItemsByDefinition<T>() where T : global::Kampai.Game.Definition;

		global::System.Collections.Generic.IList<global::Kampai.Game.Building> GetBuildingsWithState(global::Kampai.Game.BuildingState state);

		void AddLandExpansion(global::Kampai.Game.LandExpansionConfig expansionConfig);

		bool IsExpansionPurchased(int expansionId);

		int GetPurchasedExpansionCount();

		void QueueVillain(global::Kampai.Game.Prestige villainPrestige);

		int PopVillain();

		void SetTargetExpansion(int id);

		int GetTargetExpansion();

		void ClearTargetExpansion();

		bool HasTargetExpansion();

		void SetFreezeTime(int freezeTime);

		int GetFreezeTime();

		bool HasStorageBuilding();

		bool isStorageFull();

		uint GetAvailableStorageCapacity();

		global::System.Collections.Generic.List<global::Kampai.Game.Minion> GetMinions(bool has, params global::Kampai.Game.MinionState[] states);

		global::System.Collections.Generic.List<global::Kampai.Game.Minion> GetIdleMinions();

		int GetHighestFtueCompleted();

		void SetHighestFtueCompleted(int newLevel);

		int GetInventoryCountByDefinitionID(int defId);

		global::Kampai.Game.SocialClaimRewardItem.ClaimState GetSocialClaimReward(int eventID);

		void AddSocialClaimReward(int eventID, global::Kampai.Game.SocialClaimRewardItem.ClaimState claimState);

		uint GetStorageCount();

		global::Kampai.Game.Player.SanityCheckFailureReason DeepScan(global::Kampai.Game.Player prevSave);
	}
}
