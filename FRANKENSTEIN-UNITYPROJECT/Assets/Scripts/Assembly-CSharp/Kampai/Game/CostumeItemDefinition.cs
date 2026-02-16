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
			default:
					{
						int num;
                        num = 1; //added this line to remove use of unassigned variable
                        if (num == 1)
				{
					reader.Read();
					characterUIAnimationDefinition = global::Kampai.Util.ReaderUtil.ReadCharacterUIAnimationDefinition(reader, converters);
					break;
				}
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			case "BASEFBX":
				reader.Read();
				baseFBX = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			}
			return true;
		}
	}
}
