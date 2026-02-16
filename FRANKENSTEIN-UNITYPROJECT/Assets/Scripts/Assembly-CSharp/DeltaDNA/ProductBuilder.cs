namespace DeltaDNA
{
	public class ProductBuilder
	{
		private global::System.Collections.Generic.Dictionary<string, object> realCurrency;

		private global::System.Collections.Generic.List<global::System.Collections.Generic.Dictionary<string, object>> virtualCurrencies;

		private global::System.Collections.Generic.List<global::System.Collections.Generic.Dictionary<string, object>> items;

		public global::DeltaDNA.ProductBuilder AddRealCurrency(string currencyType, int currencyAmount)
		{
			if (realCurrency != null)
			{
				throw new global::System.InvalidOperationException("A Product may only have one real currency");
			}
			realCurrency = new global::System.Collections.Generic.Dictionary<string, object>
			{
				{ "realCurrencyType", currencyType },
				{ "realCurrencyAmount", currencyAmount }
			};
			return this;
		}

		public global::DeltaDNA.ProductBuilder AddVirtualCurrency(string currencyName, string currencyType, int currencyAmount)
		{
			if (virtualCurrencies == null)
			{
				virtualCurrencies = new global::System.Collections.Generic.List<global::System.Collections.Generic.Dictionary<string, object>>();
			}
			virtualCurrencies.Add(new global::System.Collections.Generic.Dictionary<string, object> { 
			{
				"virtualCurrency",
				new global::System.Collections.Generic.Dictionary<string, object>
				{
					{ "virtualCurrencyName", currencyName },
					{ "virtualCurrencyType", currencyType },
					{ "virtualCurrencyAmount", currencyAmount }
				}
			} });
			return this;
		}

		public global::DeltaDNA.ProductBuilder AddItem(string itemName, string itemType, int itemAmount)
		{
			if (items == null)
			{
				items = new global::System.Collections.Generic.List<global::System.Collections.Generic.Dictionary<string, object>>();
			}
			items.Add(new global::System.Collections.Generic.Dictionary<string, object> { 
			{
				"item",
				new global::System.Collections.Generic.Dictionary<string, object>
				{
					{ "itemName", itemName },
					{ "itemType", itemType },
					{ "itemAmount", itemAmount }
				}
			} });
			return this;
		}

		public global::System.Collections.Generic.Dictionary<string, object> ToDictionary()
		{
			global::System.Collections.Generic.Dictionary<string, object> dictionary = new global::System.Collections.Generic.Dictionary<string, object>();
			if (realCurrency != null)
			{
				dictionary.Add("realCurrency", realCurrency);
			}
			if (virtualCurrencies != null)
			{
				dictionary.Add("virtualCurrencies", virtualCurrencies);
			}
			if (items != null)
			{
				dictionary.Add("items", items);
			}
			return dictionary;
		}
	}
}
