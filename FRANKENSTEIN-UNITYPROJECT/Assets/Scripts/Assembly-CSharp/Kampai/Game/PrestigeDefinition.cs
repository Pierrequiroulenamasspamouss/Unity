namespace Kampai.Game
{
	public class PrestigeDefinition : global::Kampai.Game.Definition
	{
		public global::Kampai.Game.PrestigeType Type { get; set; }

		public uint PreUnlockLevel { get; set; }

		public uint MaxedBadgedOrder { get; set; }

		public uint OrderBoardWeight { get; set; }

		public string CollectionTitle { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.CharacterPrestigeLevelDefinition> PrestigeLevelSettings { get; set; }

		public int SmallAvatarResouceId { get; set; }

		public int WayFinderIconResourceId { get; set; }

		public int BigAvatarResourceId { get; set; }

		public int TrackedDefinitionID { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "TYPE":
				reader.Read();
				Type = global::Kampai.Util.ReaderUtil.ReadEnum<global::Kampai.Game.PrestigeType>(reader);
				break;
			case "PREUNLOCKLEVEL":
				reader.Read();
				PreUnlockLevel = global::System.Convert.ToUInt32(reader.Value);
				break;
			case "MAXEDBADGEDORDER":
				reader.Read();
				MaxedBadgedOrder = global::System.Convert.ToUInt32(reader.Value);
				break;
			case "ORDERBOARDWEIGHT":
				reader.Read();
				OrderBoardWeight = global::System.Convert.ToUInt32(reader.Value);
				break;
			case "COLLECTIONTITLE":
				reader.Read();
				CollectionTitle = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "PRESTIGELEVELSETTINGS":
				reader.Read();
                    //PrestigeLevelSettings = global::Kampai.Util.ReaderUtil.PopulateList(reader, converters, global::Kampai.Util.ReaderUtil.ReadCharacterPrestigeLevelDefinition);
                    PrestigeLevelSettings =
        global::Kampai.Util.ReaderUtil.PopulateList<
            global::Kampai.Game.CharacterPrestigeLevelDefinition
        >(
            reader,
            converters,
            global::Kampai.Util.ReaderUtil.ReadCharacterPrestigeLevelDefinition
        );
                    break;
			case "SMALLAVATARRESOUCEID":
				reader.Read();
				SmallAvatarResouceId = global::System.Convert.ToInt32(reader.Value);
				break;
			case "WAYFINDERICONRESOURCEID":
				reader.Read();
				WayFinderIconResourceId = global::System.Convert.ToInt32(reader.Value);
				break;
			case "BIGAVATARRESOURCEID":
				reader.Read();
				BigAvatarResourceId = global::System.Convert.ToInt32(reader.Value);
				break;
			case "TRACKEDDEFINITIONID":
				reader.Read();
				TrackedDefinitionID = global::System.Convert.ToInt32(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
