namespace Kampai.Game
{
	public class MinionDefinition : global::Kampai.Game.Definition
	{
		public uint Eyes { get; set; }

		public global::Kampai.Game.MinionBody Body { get; set; }

		public global::Kampai.Game.MinionHair Hair { get; set; }

		public int LeisureCooldownTime { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "EYES":
				reader.Read();
				Eyes = global::System.Convert.ToUInt32(reader.Value);
				break;
			case "BODY":
				reader.Read();
				Body = global::Kampai.Util.ReaderUtil.ReadEnum<global::Kampai.Game.MinionBody>(reader);
				break;
			case "HAIR":
				reader.Read();
				Hair = global::Kampai.Util.ReaderUtil.ReadEnum<global::Kampai.Game.MinionHair>(reader);
				break;
			case "LEISURECOOLDOWNTIME":
				reader.Read();
				LeisureCooldownTime = global::System.Convert.ToInt32(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
