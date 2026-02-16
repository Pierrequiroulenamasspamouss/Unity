namespace Kampai.Game
{
	public class InitializeMarketplaceSlotsCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.CreateMarketplaceSlotSignal createMarketplaceSlotSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CreateLockedPremiumSlotSignal createLockedPremiumSlotSignal { get; set; }

		[Inject]
		public global::Kampai.Game.UpdateMarketplaceSlotStateSignal updateSlotStatesSignal { get; set; }

		[Inject]
		public global::Kampai.Common.ICoppaService coppaService { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.MarketplaceDefinition marketplaceDefinition = definitionService.Get<global::Kampai.Game.MarketplaceDefinition>();
			int num = marketplaceDefinition.StandardSlots;
			if (marketplaceDefinition == null)
			{
				logger.Warning("MarketplaceDefinition is null");
				return;
			}
			if (coppaService.Restricted())
			{
				num += marketplaceDefinition.FacebookSlots;
			}
			CreateSlots(global::Kampai.Game.MarketplaceSaleSlotDefinition.SlotType.DEFAULT, 1000008094, num);
			if (!coppaService.Restricted())
			{
				CreateSlots(global::Kampai.Game.MarketplaceSaleSlotDefinition.SlotType.FACEBOOK_UNLOCKABLE, 1000008095, marketplaceDefinition.FacebookSlots);
			}
			if (!coppaService.Restricted())
			{
				createLockedPremiumSlotSignal.Dispatch();
			}
			updateSlotStatesSignal.Dispatch();
			logger.Debug("InitializeMarketplaceSlotsCommand: Marketplace slots created.");
		}

		private void CreateSlots(global::Kampai.Game.MarketplaceSaleSlotDefinition.SlotType slotType, int slotDefinitionId, int slotCount)
		{
			global::System.Collections.Generic.ICollection<global::Kampai.Game.MarketplaceSaleSlot> byDefinitionId = playerService.GetByDefinitionId<global::Kampai.Game.MarketplaceSaleSlot>(slotDefinitionId);
			while (byDefinitionId.Count < slotCount)
			{
				createMarketplaceSlotSignal.Dispatch(slotType);
				byDefinitionId = playerService.GetByDefinitionId<global::Kampai.Game.MarketplaceSaleSlot>(slotDefinitionId);
			}
		}
	}
}
