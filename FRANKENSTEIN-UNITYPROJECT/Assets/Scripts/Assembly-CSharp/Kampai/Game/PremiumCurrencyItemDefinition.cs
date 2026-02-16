namespace Kampai.Game
{
	public class PremiumCurrencyItemDefinition : global::Kampai.Game.CurrencyItemDefinition, global::Kampai.Game.MTXItem
	{
		private string sku;

		private global::Kampai.Game.PlatformStoreSkuDefinition platformDef;

		public string SKU
		{
			get
			{
				return sku;
			}
			set
			{
				sku = value;
			}
		}

		public global::Kampai.Game.PlatformStoreSkuDefinition PlatformStoreSku
		{
			get
			{
				return platformDef;
			}
			set
			{
				platformDef = value;
				sku = GetPlatformSKU();
			}
		}

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			default:
					{
						int num;
                        num = 1; //added this line to remove use of unassigned variable
                        if (num == 1)
				{
					reader.Read();
					PlatformStoreSku = global::Kampai.Util.ReaderUtil.ReadPlatformStoreSkuDefinition(reader, converters);
					break;
				}
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			case "SKU":
				reader.Read();
				SKU = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			}
			return true;
		}

		private string GetPlatformSKU()
		{
			return platformDef.googlePlay;
		}
	}
}
