namespace Kampai.Game
{
	public class SaleDefinition : global::Kampai.Game.Definition
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public int TransactionDefinitionID { get; set; }

		public int UTCStartDate { get; set; }

		public int UTCEndDate { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "TITLE":
				reader.Read();
				Title = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "DESCRIPTION":
				reader.Read();
				Description = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "TRANSACTIONDEFINITIONID":
				reader.Read();
				TransactionDefinitionID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "UTCSTARTDATE":
				reader.Read();
				UTCStartDate = global::System.Convert.ToInt32(reader.Value);
				break;
			case "UTCENDDATE":
				reader.Read();
				UTCEndDate = global::System.Convert.ToInt32(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
