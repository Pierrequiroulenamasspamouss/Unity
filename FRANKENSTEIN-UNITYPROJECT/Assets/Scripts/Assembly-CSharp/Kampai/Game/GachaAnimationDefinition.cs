namespace Kampai.Game
{
	public class GachaAnimationDefinition : global::Kampai.Game.AnimationDefinition
	{
		public const int INFINITE_MINIONS = 4;

		public int AnimationID { get; set; }

		public int Minions { get; set; }

		public string Prefab { get; set; }

		public global::Kampai.Game.KnuckleheadednessInfo knuckleheadednessInfo { get; set; }

		public global::Kampai.Game.AnimationAlternate AnimationAlternate { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "ANIMATIONID":
				reader.Read();
				AnimationID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "MINIONS":
				reader.Read();
				Minions = global::System.Convert.ToInt32(reader.Value);
				break;
			case "PREFAB":
				reader.Read();
				Prefab = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "KNUCKLEHEADEDNESSINFO":
				reader.Read();
				knuckleheadednessInfo = global::Kampai.Util.ReaderUtil.ReadKnuckleheadednessInfo(reader, converters);
				break;
			case "ANIMATIONALTERNATE":
				reader.Read();
				AnimationAlternate = global::Kampai.Util.ReaderUtil.ReadAnimationAlternate(reader, converters);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
