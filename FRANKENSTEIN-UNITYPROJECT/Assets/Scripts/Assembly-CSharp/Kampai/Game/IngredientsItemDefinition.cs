namespace Kampai.Game
{
	public class IngredientsItemDefinition : global::Kampai.Game.ItemDefinition
	{
		public uint TimeToHarvest { get; set; }

		public int TransactionId { get; set; }

		public int Tier { get; set; }

		public int BaseXP { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "TIMETOHARVEST":
				reader.Read();
				TimeToHarvest = global::System.Convert.ToUInt32(reader.Value);
				break;
			case "TRANSACTIONID":
				reader.Read();
				TransactionId = global::System.Convert.ToInt32(reader.Value);
				break;
			case "TIER":
				reader.Read();
				Tier = global::System.Convert.ToInt32(reader.Value);
				break;
			case "BASEXP":
				reader.Read();
				BaseXP = global::System.Convert.ToInt32(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
