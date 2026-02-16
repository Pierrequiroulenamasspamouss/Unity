namespace Kampai.Game
{
	public interface IMarketplaceService
	{
		bool IsDebugMode { get; set; }

		bool isServerKillSwitchEnabled { get; }

		bool GetItemDefinitionByItemID(int itemID, out global::Kampai.Game.MarketplaceItemDefinition itemDefinition);

		global::Kampai.Game.MarketplaceSaleSlot GetSlotByItem(global::Kampai.Game.MarketplaceSaleItem item);

		global::Kampai.Game.MarketplaceSaleSlot GetNextAvailableSlot();

		int GetSlotIndex(global::Kampai.Game.MarketplaceSaleSlot slot);

		bool AreThereValidItemsInStorage();

		bool IsUnlocked();

		bool AreThereSoldItems();

		bool AreTherePendingItems();
	}
}
