namespace Kampai.UI
{
	public interface IBuildMenuService
	{
		void SetStoreUnlockCheckedState(bool unlockChecked);

		void AddNewUnlockedItem(global::Kampai.Game.StoreItemType type, int buildingDefinitionID);

		void RemoveNewUnlockedItem(global::Kampai.Game.StoreItemType type, int buildingDefinitionID);

		void IncreaseInventoryItemCountOnTab(global::Kampai.Game.StoreItemType type);

		void DecreaseInventoryItemCountOnTab(global::Kampai.Game.StoreItemType type);

		void ClearTab(global::Kampai.Game.StoreItemType type);

		void RetoreBuidMenuState(global::System.Collections.Generic.Dictionary<global::Kampai.Game.StoreItemType, global::System.Collections.Generic.List<global::Kampai.UI.View.StoreButtonView>> buttonViews);

		void ClearAllNewUnlockItems();

		void UpdateNewUnlockList(global::System.Collections.Generic.Dictionary<global::Kampai.Game.StoreItemType, global::System.Collections.Generic.List<global::Kampai.UI.View.StoreButtonView>> buttonViews, bool updateBuildMenuButton = true);
	}
}
