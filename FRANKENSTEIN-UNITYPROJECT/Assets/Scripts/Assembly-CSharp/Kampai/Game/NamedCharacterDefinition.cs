namespace Kampai.Game
{
	[global::Kampai.Util.RequiresJsonConverter]
	public abstract class NamedCharacterDefinition : global::Kampai.Game.Definition, global::Kampai.Util.IBuilder<global::Kampai.Game.Instance>, global::Kampai.Game.Locatable
	{
		public string Prefab { get; set; }

		public global::Kampai.Game.Location Location { get; set; }

		public global::Kampai.Game.NamedCharacterAnimationDefinition CharacterAnimations { get; set; }

		public global::Kampai.Game.NamedCharacterType Type { get; set; }

		public int VFXBuildingID { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "PREFAB":
				reader.Read();
				Prefab = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "LOCATION":
				reader.Read();
				Location = global::Kampai.Util.ReaderUtil.ReadLocation(reader, converters);
				break;
			case "CHARACTERANIMATIONS":
				reader.Read();
				CharacterAnimations = global::Kampai.Util.FastJSONDeserializer.Deserialize<global::Kampai.Game.NamedCharacterAnimationDefinition>(reader, converters);
				break;
			case "TYPE":
				reader.Read();
				Type = global::Kampai.Util.ReaderUtil.ReadEnum<global::Kampai.Game.NamedCharacterType>(reader);
				break;
			case "VFXBUILDINGID":
				reader.Read();
				VFXBuildingID = global::System.Convert.ToInt32(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}

		public abstract global::Kampai.Game.Instance Build();
	}
}
