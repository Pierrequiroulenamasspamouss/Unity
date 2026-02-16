namespace Kampai.Game.Mtx
{
	public class ReceiptValidationRequest
	{
		public string sku;

		public string mtxTransactionId;

		public global::Kampai.Game.Mtx.IMtxReceipt receipt;

		public ReceiptValidationRequest(string sku, string mtxTransactionId, global::Kampai.Game.Mtx.IMtxReceipt receipt)
		{
			this.sku = sku;
			this.mtxTransactionId = mtxTransactionId;
			this.receipt = receipt;
		}
	}
}
