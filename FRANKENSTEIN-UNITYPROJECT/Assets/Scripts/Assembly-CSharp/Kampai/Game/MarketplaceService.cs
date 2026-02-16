namespace Kampai.Game
{
	public class MarketplaceService : global::Kampai.Game.IMarketplaceService
	{
		[Inject]
		public global::Kampai.Game.IConfigurationsService configurationsService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		public bool IsDebugMode { get; set; }

		public bool isServerKillSwitchEnabled
		{
			get
			{
				return configurationsService.isKillSwitchOn(global::Kampai.Game.KillSwitch.MARKETPLACESERVER);
			}
		}

		public bool GetItemDefinitionByItemID(int itemID, out global::Kampai.Game.MarketplaceItemDefinition itemDefinition)
		{
			global::Kampai.Game.MarketplaceDefinition marketplaceDefinition = definitionService.Get<global::Kampai.Game.MarketplaceDefinition>();
			bool result = false;
			itemDefinition = null;
			if (marketplaceDefinition != null && marketplaceDefinition.itemDefinitions != null)
			{
				foreach (global::Kampai.Game.MarketplaceItemDefinition itemDefinition2 in marketplaceDefinition.itemDefinitions)
				{
					if (itemDefinition2.ItemID == itemID)
					{
						itemDefinition = itemDefinition2;
						result = true;
						break;
					}
				}
			}
			return result;
		}

		public global::Kampai.Game.MarketplaceSaleSlot GetSlotByItem(global::Kampai.Game.MarketplaceSaleItem item)
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.MarketplaceSaleSlot> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.MarketplaceSaleSlot>();
			foreach (global::Kampai.Game.MarketplaceSaleSlot item2 in instancesByType)
			{
				global::Kampai.Game.MarketplaceSaleItem byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.MarketplaceSaleItem>(item2.itemId);
				if (byInstanceId == item)
				{
					return item2;
				}
			}
			return null;
		}

		public global::Kampai.Game.MarketplaceSaleSlot GetNextAvailableSlot()
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.MarketplaceSaleSlot> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.MarketplaceSaleSlot>();
			foreach (global::Kampai.Game.MarketplaceSaleSlot item in instancesByType)
			{
				global::Kampai.Game.MarketplaceSaleItem byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.MarketplaceSaleItem>(item.itemId);
				if (byInstanceId == null && item.state == global::Kampai.Game.MarketplaceSaleSlot.State.UNLOCKED)
				{
					return item;
				}
			}
			return null;
		}

		public int GetSlotIndex(global::Kampai.Game.MarketplaceSaleSlot slot)
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.MarketplaceSaleSlot> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.MarketplaceSaleSlot>();
			if (instancesByType.Contains(slot))
			{
				return instancesByType.IndexOf(slot);
			}
			return -1;
		}

		public bool AreThereValidItemsInStorage()
		{
			foreach (global::Kampai.Game.Item item in playerService.GetItems())
			{
				global::Kampai.Game.DynamicIngredientsDefinition dynamicIngredientsDefinition = item.Definition as global::Kampai.Game.DynamicIngredientsDefinition;
				if (dynamicIngredientsDefinition == null)
				{
					global::Kampai.Game.MarketplaceItemDefinition itemDefinition;
					GetItemDefinitionByItemID(item.Definition.ID, out itemDefinition);
					if (itemDefinition != null)
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool IsUnlocked()
		{
			global::Kampai.Game.MarketplaceDefinition marketplaceDefinition = definitionService.Get<global::Kampai.Game.MarketplaceDefinition>();
			return playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID) >= marketplaceDefinition.LevelGate;
		}

		public bool AreThereSoldItems()
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.MarketplaceSaleSlot> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.MarketplaceSaleSlot>();
			foreach (global::Kampai.Game.MarketplaceSaleSlot item in instancesByType)
			{
				global::Kampai.Game.MarketplaceSaleItem byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.MarketplaceSaleItem>(item.itemId);
				if (byInstanceId != null && byInstanceId.state == global::Kampai.Game.MarketplaceSaleItem.State.SOLD)
				{
					return true;
				}
			}
			return false;
		}

		public bool AreTherePendingItems()
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.MarketplaceSaleSlot> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.MarketplaceSaleSlot>();
			foreach (global::Kampai.Game.MarketplaceSaleSlot item in instancesByType)
			{
				global::Kampai.Game.MarketplaceSaleItem byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.MarketplaceSaleItem>(item.itemId);
				if (byInstanceId != null && byInstanceId.state == global::Kampai.Game.MarketplaceSaleItem.State.PENDING)
				{
					return true;
				}
			}
			return false;
		}
	}
}
