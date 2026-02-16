namespace Kampai.Game
{
	public class DebugCurrencyService : global::Kampai.Game.CurrencyService
	{
		[Inject]
		public global::Kampai.UI.View.ShowMockStoreDialogSignal showMockStoreDialogSignal { get; set; }

		public override void RequestPurchase(global::Kampai.Game.KampaiPendingTransaction item)
		{
			showMockStoreDialogSignal.Dispatch(item);
		}

		public override string GetPriceWithCurrencyAndFormat(string SKU)
		{
			switch (SKU)
			{
			case "SKU_FEW_DIAMONDS":
				return "$1.99";
			case "SKU_PILE_DIAMONDS":
				return "$4.99";
			case "SKU_SACK_DIAMONDS":
				return "$9.99";
			case "SKU_BAGS_DIAMONDS":
				return "$19.99";
			case "SKU_CHEST_DIAMONDS":
				return "$39.99";
			case "SKU_BIG_CHEST_DIAMONDS":
				return "$49.00";
			case "SKU_PILE_SAND_DOLLARS":
				return "$0.99";
			case "SKU_BAG_SAND_DOLLARS":
				return "$2.99";
			case "SKU_SACK_SAND_DOLLARS":
				return "$7.99";
			case "SKU_BOX_SAND_DOLLARS":
				return "$14.99";
			case "SKU_CHEST_SAND_DOLLARS":
				return "$29.99";
			case "SKU_TRUNK_SAND_DOLLARS":
				return "$79.00";
			default:
				base.logger.Fatal(global::Kampai.Util.FatalCode.TE_NO_SUCH_SKU);
				return string.Empty;
			}
		}

		public override void ReceiptValidationCallback(global::Kampai.Game.Mtx.ReceiptValidationResult result)
		{
		}

		public override void PauseTransactionsHandling()
		{
		}

		public override void ResumeTransactionsHandling()
		{
		}
	}
}
