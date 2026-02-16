namespace Kampai.Game.Mtx
{
	public class ReceiptValidationResult
	{
		public enum Code
		{
			SUCCESS = 0,
			RECEIPT_INVALID = 1,
			RECEIPT_DUPLICATE = 2,
			VALIDATION_UNAVAILABLE = 3
		}

		public string sku;

		public string mtxTransactionId;

		public global::Kampai.Game.Mtx.ReceiptValidationResult.Code code;

		public ReceiptValidationResult(string sku, string mtxTransactionId, global::Kampai.Game.Mtx.ReceiptValidationResult.Code code)
		{
			this.sku = sku;
			this.mtxTransactionId = mtxTransactionId;
			this.code = code;
		}
	}
}
