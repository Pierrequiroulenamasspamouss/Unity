namespace Kampai.Game.Mtx
{
	public interface IMtxReceiptValidationService
	{
		void AddPendingReceipt(string sku, string mtxTransactionId, global::Kampai.Game.Mtx.IMtxReceipt receipt);

		void ValidatePendingReceipt();

		void ValidationResultCallback(global::Kampai.Game.Mtx.ReceiptValidationResult result);

		void RemovePendingReceipt(string mtxTransactionId);

		bool HasPendingReceipts();
	}
}
