namespace Kampai.Game
{
	public class CurrencyItemFastConverter : global::Kampai.Util.FastJsonCreationConverter<global::Kampai.Game.CurrencyItemDefinition>
	{
		private bool platformStoreSkuPropertyExists;

		public override global::Kampai.Game.CurrencyItemDefinition ReadJson(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JObject jObject = global::Newtonsoft.Json.Linq.JObject.Load(reader);
			platformStoreSkuPropertyExists = jObject.Property("platformStoreSku") != null;
			reader = jObject.CreateReader();
			return base.ReadJson(reader, converters);
		}

		public override global::Kampai.Game.CurrencyItemDefinition Create()
		{
			if (platformStoreSkuPropertyExists)
			{
				return new global::Kampai.Game.PremiumCurrencyItemDefinition();
			}
			return new global::Kampai.Game.CurrencyItemDefinition();
		}
	}
}
