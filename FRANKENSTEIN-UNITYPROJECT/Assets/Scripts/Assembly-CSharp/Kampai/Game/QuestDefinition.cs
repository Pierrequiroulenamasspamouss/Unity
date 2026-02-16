namespace Kampai.Game
{
	[global::Kampai.Util.RequiresJsonConverter]
	public class QuestDefinition : global::Kampai.Game.Definition, global::Kampai.Util.IBuilder<global::Kampai.Game.Instance>
	{
		public int QuestLineID { get; set; }

		public virtual global::Kampai.Game.QuestType type { get; set; }

		public int NarrativeOrder { get; set; }

		public global::Kampai.Game.QuestSurfaceType SurfaceType { get; set; }

		public int SurfaceID { get; set; }

		public int UnlockLevel { get; set; }

		public int UnlockQuestId { get; set; }

		public string QuestBookIcon { get; set; }

		public string QuestBookMask { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.QuestStepDefinition> QuestSteps { get; set; }

		public int RewardTransaction { get; set; }

		public string WayFinderIcon { get; set; }

		public string QuestIntro { get; set; }

		public string QuestVoice { get; set; }

		public string QuestOutro { get; set; }

		public string QuestIntroMood { get; set; }

		public string QuestVoiceMood { get; set; }

		public string QuestOutroMood { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "QUESTLINEID":
				reader.Read();
				QuestLineID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "TYPE":
				reader.Read();
				type = global::Kampai.Util.ReaderUtil.ReadEnum<global::Kampai.Game.QuestType>(reader);
				break;
			case "NARRATIVEORDER":
				reader.Read();
				NarrativeOrder = global::System.Convert.ToInt32(reader.Value);
				break;
			case "SURFACETYPE":
				reader.Read();
				SurfaceType = global::Kampai.Util.ReaderUtil.ReadEnum<global::Kampai.Game.QuestSurfaceType>(reader);
				break;
			case "SURFACEID":
				reader.Read();
				SurfaceID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "UNLOCKLEVEL":
				reader.Read();
				UnlockLevel = global::System.Convert.ToInt32(reader.Value);
				break;
			case "UNLOCKQUESTID":
				reader.Read();
				UnlockQuestId = global::System.Convert.ToInt32(reader.Value);
				break;
			case "QUESTBOOKICON":
				reader.Read();
				QuestBookIcon = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "QUESTBOOKMASK":
				reader.Read();
				QuestBookMask = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "QUESTSTEPS":
				reader.Read();
				//QuestSteps = global::Kampai.Util.ReaderUtil.PopulateList(reader, converters, global::Kampai.Util.ReaderUtil.ReadQuestStepDefinition);
				QuestSteps = global::Kampai.Util.ReaderUtil.PopulateList < global::Kampai.Game.QuestStepDefinition > (reader, converters, global::Kampai.Util.ReaderUtil.ReadQuestStepDefinition);

                    break;
			case "REWARDTRANSACTION":
				reader.Read();
				RewardTransaction = global::System.Convert.ToInt32(reader.Value);
				break;
			case "WAYFINDERICON":
				reader.Read();
				WayFinderIcon = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "QUESTINTRO":
				reader.Read();
				QuestIntro = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "QUESTVOICE":
				reader.Read();
				QuestVoice = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "QUESTOUTRO":
				reader.Read();
				QuestOutro = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "QUESTINTROMOOD":
				reader.Read();
				QuestIntroMood = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "QUESTVOICEMOOD":
				reader.Read();
				QuestVoiceMood = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "QUESTOUTROMOOD":
				reader.Read();
				QuestOutroMood = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}

		public virtual global::Kampai.Game.Transaction.TransactionDefinition GetReward(global::Kampai.Game.IDefinitionService definitionService)
		{
			if (RewardTransaction == 0 || definitionService == null)
			{
				return null;
			}
			return definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(RewardTransaction);
		}

		public static string GetProceduralQuestIcon(global::Kampai.Game.QuestStepType type)
		{
			switch (type)
			{
			case global::Kampai.Game.QuestStepType.Delivery:
				return "tempCharQuestIcon";
			case global::Kampai.Game.QuestStepType.OrderBoard:
				return "tempCharQuestIcon";
			case global::Kampai.Game.QuestStepType.MinionTask:
				return "tempCharQuestIcon";
			case global::Kampai.Game.QuestStepType.Mignette:
				return "tempCharQuestIcon";
			default:
				return string.Empty;
			}
		}

		public static string GetProceduralQuestDescription(global::Kampai.Game.QuestStepType type)
		{
			switch (type)
			{
			case global::Kampai.Game.QuestStepType.Delivery:
				return "deliveryTaskName";
			case global::Kampai.Game.QuestStepType.OrderBoard:
				return "orderBoardTaskName";
			case global::Kampai.Game.QuestStepType.MinionTask:
				return "minionTaskName";
			case global::Kampai.Game.QuestStepType.Mignette:
				return "mignetteTaskName";
			default:
				return string.Empty;
			}
		}

		public virtual global::Kampai.Game.Instance Build()
		{
			return new global::Kampai.Game.Quest(this);
		}
	}
}
