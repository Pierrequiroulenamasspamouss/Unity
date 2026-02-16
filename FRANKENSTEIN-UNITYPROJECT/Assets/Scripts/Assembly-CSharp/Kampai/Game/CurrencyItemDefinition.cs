namespace Kampai.Game
{
	[global::Kampai.Util.RequiresJsonConverter]
	public class CurrencyItemDefinition : global::Kampai.Game.Definition
	{
		public string Image { get; set; }

		public string Mask { get; set; }

		public string VFX { get; set; }

		public string Audio { get; set; }

		public bool COPPAGated { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "IMAGE":
				reader.Read();
				Image = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "MASK":
				reader.Read();
				Mask = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "VFX":
				reader.Read();
				VFX = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "AUDIO":
				reader.Read();
				Audio = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "COPPAGATED":
				reader.Read();
				COPPAGated = global::System.Convert.ToBoolean(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
