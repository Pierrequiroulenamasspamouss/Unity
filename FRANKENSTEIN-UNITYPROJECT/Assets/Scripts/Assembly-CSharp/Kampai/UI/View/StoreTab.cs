namespace Kampai.UI.View
{
	public class StoreTab
	{
		public string LocalizedName { get; set; }

		public global::Kampai.Game.StoreItemType Type { get; set; }

		public int AvailableInstance { get; set; }

		public bool HasNewUnlockedItem { get; set; }

		public StoreTab(string localizedName, global::Kampai.Game.StoreItemType type, int availAbleInstance, bool hasUnlockedItems)
		{
			LocalizedName = localizedName;
			Type = type;
			AvailableInstance = availAbleInstance;
			HasNewUnlockedItem = hasUnlockedItems;
		}
	}
}
