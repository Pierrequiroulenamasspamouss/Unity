namespace Kampai.Game
{
	public class CostumeItemDefinition : global::Kampai.Game.ItemDefinition
	{
		public string baseFBX { get; set; }

		public global::Kampai.Game.CharacterUIAnimationDefinition characterUIAnimationDefinition { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "CHARACTERUIANIMATIONDEFINITION":
				reader.Read();
				characterUIAnimationDefinition = global::Kampai.Util.ReaderUtil.ReadCharacterUIAnimationDefinition(reader, converters);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			case "BASEFBX":
				reader.Read();
				baseFBX = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			}
			return true;
		}
	}
}
