namespace Kampai.Game
{
	public class CurrencyItemConverter : global::Newtonsoft.Json.Converters.CustomCreationConverter<global::Kampai.Game.CurrencyItemDefinition>
	{
		private bool platformStoreSkuPropertyExists;

		public override object ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object existingValue, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			global::Newtonsoft.Json.Linq.JObject jObject = global::Newtonsoft.Json.Linq.JObject.Load(reader);
			platformStoreSkuPropertyExists = jObject.Property("platformStoreSku") != null;
			reader = jObject.CreateReader();
			return base.ReadJson(reader, objectType, existingValue, serializer);
		}

		public override global::Kampai.Game.CurrencyItemDefinition Create(global::System.Type objectType)
		{
			if (platformStoreSkuPropertyExists)
			{
				return new global::Kampai.Game.PremiumCurrencyItemDefinition();
			}
			return new global::Kampai.Game.CurrencyItemDefinition();
		}
	}
}
