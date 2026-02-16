namespace DeltaDNA
{
	public class TransactionBuilder
	{
		private global::DeltaDNA.SDK sdk;

		internal TransactionBuilder(global::DeltaDNA.SDK sdk)
		{
			this.sdk = sdk;
		}

		public void BuyVirtualCurrency(string transactionName, string realCurrencyType, int realCurrencyAmount, string virtualCurrencyName, string virtualCurrencyType, int virtualCurrencyAmount, string transactionReceipt = null)
		{
			global::DeltaDNA.EventBuilder eventParams = new global::DeltaDNA.EventBuilder().AddParam("transactionType", "PURCHASE").AddParam("transactionName", transactionName).AddParam("productsSpent", new global::DeltaDNA.ProductBuilder().AddRealCurrency(realCurrencyType, realCurrencyAmount))
				.AddParam("productsReceived", new global::DeltaDNA.ProductBuilder().AddVirtualCurrency(virtualCurrencyName, virtualCurrencyType, virtualCurrencyAmount))
				.AddParam("transactionReceipt", transactionReceipt);
			sdk.RecordEvent("transaction", eventParams);
		}
	}
}
