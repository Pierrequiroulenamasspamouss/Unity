namespace Kampai.Game
{
	public class QuestCompleteCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.UI.View.UpdateQuestBookBadgeSignal updateBadgeSignal { get; set; }

		[Inject]
		public global::Kampai.Game.GetNewQuestSignal getNewQuestSignal { get; set; }

		[Inject]
		public global::Kampai.Game.UpdateQuestWorldIconsSignal updateQuestWorldIconsSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService prestigeService { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService characterService { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.Game.Quest quest { get; set; }

		public override void Execute()
		{
			if (quest.Definition.SurfaceType == global::Kampai.Game.QuestSurfaceType.Character || quest.Definition.SurfaceType == global::Kampai.Game.QuestSurfaceType.Automatic)
			{
				CheckIfQuestLineIsCompleted();
			}
			updateBadgeSignal.Dispatch();
			updateQuestWorldIconsSignal.Dispatch(quest);
			getNewQuestSignal.Dispatch();
			prestigeService.UpdateEligiblePrestigeList();
			string questGiver = string.Empty;
			if (quest.GetActiveDefinition().SurfaceType == global::Kampai.Game.QuestSurfaceType.Character)
			{
				global::Kampai.Game.Prestige prestige = characterService.GetPrestige(quest.GetActiveDefinition().SurfaceID, false);
				if (prestige != null)
				{
					questGiver = prestige.Definition.LocalizedKey;
				}
			}
			telemetryService.Send_Telemetry_EVT_GP_ACHIEVEMENTS_CHECKPOINTS_EAL(questService.GetEventName(quest.GetActiveDefinition().LocalizedKey), global::Kampai.Common.Service.Telemetry.AchievementType.Quest, questGiver);
		}

		private void CheckIfQuestLineIsCompleted()
		{
			global::Kampai.Game.QuestDefinition activeDefinition = quest.GetActiveDefinition();
			bool flag = false;
			global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.QuestLine> questLines = questService.GetQuestLines();
			global::Kampai.Game.QuestLine questLine = questLines[activeDefinition.QuestLineID];
			global::System.Collections.Generic.IList<global::Kampai.Game.QuestDefinition> quests = questLine.Quests;
			if (quests[0].ID == activeDefinition.ID)
			{
				flag = true;
				questService.SetQuestLineState(activeDefinition.QuestLineID, global::Kampai.Game.QuestLineState.Finished);
			}
			global::Kampai.Game.Prestige prestige = prestigeService.GetPrestige(activeDefinition.SurfaceID);
			if (prestige == null)
			{
				prestige = prestigeService.GetPrestige(questLine.GivenByCharacterID);
				if (prestige == null)
				{
					return;
				}
			}
			if (prestige.Definition.ID != 40000 && flag && (prestige.state == global::Kampai.Game.PrestigeState.Questing || prestige.state == global::Kampai.Game.PrestigeState.TaskableWhileQuesting))
			{
				prestigeService.ChangeToPrestigeState(prestige, global::Kampai.Game.PrestigeState.Taskable);
			}
		}
	}
}
