namespace Kampai.Game.Transaction
{
	public class WeightedQuantityItem : global::Kampai.Util.QuantityItem
	{
		public uint Weight { get; set; }

		public WeightedQuantityItem()
		{
			base.Quantity = 1u;
		}

		public WeightedQuantityItem(int id, uint quantity, uint weight)
			: base(id, quantity)
		{
			Weight = weight;
		}

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "WEIGHT":
				reader.Read();
				Weight = global::System.Convert.ToUInt32(reader.Value);
				return true;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
		}
	}
}
