namespace Kampai.UI
{
	public class BuildMenuLocalState
	{
		public bool UnlockChecked;

		public global::System.Collections.Generic.IList<global::Kampai.Game.StoreItemType> UncheckedTabs;

		public global::System.Collections.Generic.IDictionary<global::Kampai.Game.StoreItemType, global::System.Collections.Generic.List<int>> NewUnlockedItemOnTabs;

		public global::System.Collections.Generic.IDictionary<global::Kampai.Game.StoreItemType, int> UncheckedInventoryItemOnTabs;

		public BuildMenuLocalState()
		{
			UncheckedTabs = new global::System.Collections.Generic.List<global::Kampai.Game.StoreItemType>();
			NewUnlockedItemOnTabs = new global::System.Collections.Generic.Dictionary<global::Kampai.Game.StoreItemType, global::System.Collections.Generic.List<int>>();
			UncheckedInventoryItemOnTabs = new global::System.Collections.Generic.Dictionary<global::Kampai.Game.StoreItemType, int>();
		}
	}
}
