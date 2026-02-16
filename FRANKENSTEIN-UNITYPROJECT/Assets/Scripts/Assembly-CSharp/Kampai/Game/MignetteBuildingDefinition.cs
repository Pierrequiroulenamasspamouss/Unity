namespace Kampai.Game
{
	public class MignetteBuildingDefinition : global::Kampai.Game.TaskableBuildingDefinition
	{
		public string CollectableImage;

		public string CollectableImageMask;

		public global::System.Collections.Generic.IList<MignetteRuleDefinition> MignetteRules;

		public global::System.Collections.Generic.IList<global::Kampai.Game.MignetteChildObjectDefinition> ChildObjects;

		public global::System.Collections.Generic.IList<global::Kampai.Game.MignetteChildObjectDefinition> CooldownObjects;

		public bool ShowPlayConfirmMenu { get; set; }

		public bool ShowMignetteHUD { get; set; }

		public string ContextRootName { get; set; }

		public int CooldownInSeconds { get; set; }

		public global::System.Collections.Generic.IList<int> MainCollectionDefinitionIDs { get; set; }

		public global::System.Collections.Generic.IList<int> RepeatableCollectionDefinitionIDs { get; set; }

		public string AspirationalMessage { get; set; }

		public MignetteBuildingDefinition()
		{
			ShowPlayConfirmMenu = false;
			ShowMignetteHUD = true;
		}

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "SHOWPLAYCONFIRMMENU":
				reader.Read();
				ShowPlayConfirmMenu = global::System.Convert.ToBoolean(reader.Value);
				break;
			case "SHOWMIGNETTEHUD":
				reader.Read();
				ShowMignetteHUD = global::System.Convert.ToBoolean(reader.Value);
				break;
			case "CONTEXTROOTNAME":
				reader.Read();
				ContextRootName = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "COOLDOWNINSECONDS":
				reader.Read();
				CooldownInSeconds = global::System.Convert.ToInt32(reader.Value);
				break;
			case "MAINCOLLECTIONDEFINITIONIDS":
				reader.Read();
				MainCollectionDefinitionIDs = global::Kampai.Util.ReaderUtil.PopulateListInt32(reader);
				break;
			case "REPEATABLECOLLECTIONDEFINITIONIDS":
				reader.Read();
				RepeatableCollectionDefinitionIDs = global::Kampai.Util.ReaderUtil.PopulateListInt32(reader);
				break;
			case "ASPIRATIONALMESSAGE":
				reader.Read();
				AspirationalMessage = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "COLLECTABLEIMAGE":
				reader.Read();
				CollectableImage = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "COLLECTABLEIMAGEMASK":
				reader.Read();
				CollectableImageMask = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "MIGNETTERULES":
				reader.Read();
				MignetteRules = global::Kampai.Util.ReaderUtil.PopulateList<MignetteRuleDefinition>(reader, converters, global::Kampai.Util.ReaderUtil.ReadMignetteRuleDefinition);
				break;
			case "CHILDOBJECTS":
				reader.Read();
				ChildObjects = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.MignetteChildObjectDefinition>(reader, converters, global::Kampai.Util.ReaderUtil.ReadMignetteChildObjectDefinition);
				break;
			case "COOLDOWNOBJECTS":
				reader.Read();
				CooldownObjects = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.MignetteChildObjectDefinition>(reader, converters, global::Kampai.Util.ReaderUtil.ReadMignetteChildObjectDefinition);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}

		public override global::Kampai.Game.Building BuildBuilding()
		{
			return new global::Kampai.Game.MignetteBuilding(this);
		}
	}
}
