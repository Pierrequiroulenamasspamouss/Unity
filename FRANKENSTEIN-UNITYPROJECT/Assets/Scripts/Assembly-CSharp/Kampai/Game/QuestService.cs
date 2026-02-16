namespace Kampai.Game
{
	public class QuestService : global::Kampai.Game.IQuestService
	{
		private global::System.Collections.Generic.Dictionary<string, global::Kampai.Game.IQuestScriptRunner> runners = new global::System.Collections.Generic.Dictionary<string, global::Kampai.Game.IQuestScriptRunner>();

		private global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.QuestLine> questLines = new global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.QuestLine>();

		private global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<int>> questUnlockTree = new global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<int>>();

		private global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Quest> quests = new global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Quest>();

		private bool isInitialized;

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService prestigeService { get; set; }

		[Inject]
		public global::Kampai.Game.QuestTimeoutSignal timeoutSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.QuestTaskReadySignal questTaskReadySignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateProceduralQuestPanelSignal updateSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateQuestPanelSignal updateQuestPanelSignal { get; set; }

		[Inject]
		public global::Kampai.Game.UpdateQuestWorldIconsSignal updateWorldIconSignal { get; set; }

		[Inject]
		public global::Kampai.Game.TimedQuestNotificationSignal questNoteSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService characterService { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowQuestPanelSignal showQuestPanelSignal { get; set; }

		[Inject(global::Kampai.Main.LocalizationServices.EVENT)]
		public global::Kampai.Main.ILocalizationService eventsLocalService { get; set; }

		public void Initialize()
		{
			LoadQuestLines();
			CreateQuestMap();
			CreateQuestUnlockTree();
			UpdateQuestLineStateBasedOnDependency();
			global::System.Collections.Generic.ICollection<global::Kampai.Game.Quest> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.Quest>();
			foreach (global::Kampai.Game.Quest item in instancesByType)
			{
				updateWorldIconSignal.Dispatch(item);
				CheckAndStartQuestTimers(item);
				if (item.state == global::Kampai.Game.QuestState.RunningStartScript)
				{
					StartQuestScript(item, true);
				}
				else if (item.state == global::Kampai.Game.QuestState.RunningCompleteScript)
				{
					StartQuestScript(item, false);
				}
				else
				{
					if (item.state != global::Kampai.Game.QuestState.RunningTasks)
					{
						continue;
					}
					for (int i = 0; i < item.Steps.Count; i++)
					{
						global::Kampai.Game.QuestStep questStep = item.Steps[i];
						if (questStep.state == global::Kampai.Game.QuestStepState.RunningStartScript || questStep.state == global::Kampai.Game.QuestStepState.RunningCompleteScript)
						{
							StartQuestScript(item, questStep.state == global::Kampai.Game.QuestStepState.RunningStartScript, i, true);
						}
					}
				}
			}
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.GetNewQuestSignal>().Dispatch();
			isInitialized = true;
		}

		public global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.QuestLine> GetQuestLines()
		{
			return questLines;
		}

		public global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Quest> GetQuestMap()
		{
			return quests;
		}

		public global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<int>> GetQuestUnlockTree()
		{
			return questUnlockTree;
		}

		public void AddQuest(global::Kampai.Game.Quest quest)
		{
			int iD = quest.GetActiveDefinition().ID;
			if (quests.ContainsKey(iD))
			{
				logger.Error("QuestService: Quest {0} already added.", iD);
				return;
			}
			quests.Add(iD, quest);
			playerService.Add(quest);
			SetQuestLineState(quest.GetActiveDefinition().QuestLineID, global::Kampai.Game.QuestLineState.Started);
			CheckAndStartQuestTimers(quest);
		}

		public void RemoveQuest(global::Kampai.Game.Quest quest)
		{
			global::Kampai.Game.QuestDefinition activeDefinition = quest.GetActiveDefinition();
			int iD = activeDefinition.ID;
			if (!quests.ContainsKey(iD))
			{
				logger.Info("QuestService: Quest {0} has already been removed.", iD);
				return;
			}
			if (activeDefinition.SurfaceType == global::Kampai.Game.QuestSurfaceType.LimitedEvent || activeDefinition.SurfaceType == global::Kampai.Game.QuestSurfaceType.TimedEvent)
			{
				timeEventService.RemoveEvent(quest.ID);
			}
			quests.Remove(iD);
			playerService.Remove(quest);
		}

		public void SetQuestLineState(int questLineId, global::Kampai.Game.QuestLineState targetState)
		{
			if (questLines.ContainsKey(questLineId))
			{
				global::Kampai.Game.QuestLine questLine = questLines[questLineId];
				if (questLine.state == global::Kampai.Game.QuestLineState.NotStarted)
				{
					questLine.state = targetState;
				}
				else if (questLine.state == global::Kampai.Game.QuestLineState.Started && targetState == global::Kampai.Game.QuestLineState.Finished)
				{
					questLine.state = targetState;
				}
			}
		}

		public bool IsOneOffCraftableDisplayable(int questDefinitionId, int trackedItemDefinitionID)
		{
			if (questDefinitionId == 0)
			{
				return true;
			}
			if (!quests.ContainsKey(questDefinitionId))
			{
				return false;
			}
			int index = 0;
			for (int i = 0; i < quests[questDefinitionId].GetActiveDefinition().QuestSteps.Count; i++)
			{
				if (quests[questDefinitionId].GetActiveDefinition().QuestSteps[i].ItemDefinitionID == trackedItemDefinitionID)
				{
					index = i;
				}
			}
			if (quests[questDefinitionId].Steps[index].state == global::Kampai.Game.QuestStepState.Inprogress)
			{
				return true;
			}
			return false;
		}

		public bool IsQuestCompleted(int questDefinitionID)
		{
			if (!isInitialized)
			{
				return false;
			}
			if (questDefinitionID == 0)
			{
				return true;
			}
			if (quests.ContainsKey(questDefinitionID))
			{
				return quests[questDefinitionID].state == global::Kampai.Game.QuestState.Complete;
			}
			global::Kampai.Game.QuestDefinition questDefinition = definitionService.Get<global::Kampai.Game.QuestDefinition>(questDefinitionID);
			int unlockQuestId = questDefinition.UnlockQuestId;
			uint quantity = playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID);
			if (quantity < questDefinition.UnlockLevel || (unlockQuestId != 0 && quests.ContainsKey(questDefinition.UnlockQuestId) && quests[questDefinition.UnlockQuestId].state != global::Kampai.Game.QuestState.Complete))
			{
				return false;
			}
			return IsQuestAlreadyFinished(questDefinitionID);
		}

		private bool IsQuestAlreadyFinished(int questDefinitionId)
		{
			global::Kampai.Game.QuestDefinition questDefinition = definitionService.Get<global::Kampai.Game.QuestDefinition>(questDefinitionId);
			if (questLines.ContainsKey(questDefinition.QuestLineID))
			{
				global::Kampai.Game.QuestLine questLine = questLines[questDefinition.QuestLineID];
				if (questLine.state == global::Kampai.Game.QuestLineState.NotStarted)
				{
					return false;
				}
				if (questLine.state == global::Kampai.Game.QuestLineState.Finished)
				{
					return true;
				}
				if (questLine.state == global::Kampai.Game.QuestLineState.Started)
				{
					foreach (global::Kampai.Game.QuestDefinition quest in questLine.Quests)
					{
						if (quests.ContainsKey(quest.ID))
						{
							if (quest.NarrativeOrder >= questDefinition.NarrativeOrder)
							{
								return true;
							}
							return false;
						}
					}
					return false;
				}
			}
			if (!questUnlockTree.ContainsKey(questDefinitionId))
			{
				return true;
			}
			foreach (int item in questUnlockTree[questDefinitionId])
			{
				if (quests.ContainsKey(item))
				{
					return true;
				}
			}
			foreach (int item2 in questUnlockTree[questDefinitionId])
			{
				if (IsQuestAlreadyFinished(item2))
				{
					return true;
				}
			}
			return false;
		}

		public global::Kampai.Game.QuestState GoToNextQuestState(global::Kampai.Game.Quest quest)
		{
			switch (quest.state)
			{
			case global::Kampai.Game.QuestState.Notstarted:
			{
				string questGiver = string.Empty;
				if (quest.GetActiveDefinition().SurfaceType == global::Kampai.Game.QuestSurfaceType.Character)
				{
					global::Kampai.Game.Prestige prestige = characterService.GetPrestige(quest.GetActiveDefinition().SurfaceID, false);
					if (prestige != null)
					{
						questGiver = prestige.Definition.LocalizedKey;
					}
				}
				telemetryService.Send_Telemetry_EVT_GP_ACHIEVEMENTS_STARTED_EAL(GetEventName(quest.GetActiveDefinition().LocalizedKey), global::Kampai.Common.Service.Telemetry.AchievementType.Quest, questGiver);
				if (quest.GetActiveDefinition().SurfaceType == global::Kampai.Game.QuestSurfaceType.TimedEvent)
				{
					StartTimedQuest(quest);
				}
				if (!HasScript(quest, true))
				{
					RunningTasksStateValidation(quest);
				}
				else
				{
					GoToQuestState(quest, global::Kampai.Game.QuestState.RunningStartScript);
				}
				break;
			}
			case global::Kampai.Game.QuestState.RunningStartScript:
				RunningTasksStateValidation(quest);
				break;
			case global::Kampai.Game.QuestState.RunningTasks:
				RunningCompleteScriptStateValidation(quest);
				break;
			case global::Kampai.Game.QuestState.RunningCompleteScript:
				RunningHarvestableStateValidation(quest);
				break;
			case global::Kampai.Game.QuestState.Harvestable:
				GoToQuestState(quest, global::Kampai.Game.QuestState.Complete);
				break;
			}
			return quest.state;
		}

		private void RunningTasksStateValidation(global::Kampai.Game.Quest quest)
		{
			if (quest.Steps.Count == 0)
			{
				RunningCompleteScriptStateValidation(quest);
			}
			else
			{
				GoToQuestState(quest, global::Kampai.Game.QuestState.RunningTasks);
			}
		}

		private void RunningCompleteScriptStateValidation(global::Kampai.Game.Quest quest)
		{
			if (!HasScript(quest, false))
			{
				RunningHarvestableStateValidation(quest);
			}
			else
			{
				GoToQuestState(quest, global::Kampai.Game.QuestState.RunningCompleteScript);
			}
		}

		private void RunningHarvestableStateValidation(global::Kampai.Game.Quest quest)
		{
			if (quest.state < global::Kampai.Game.QuestState.RunningCompleteScript)
			{
				GoToQuestState(quest, global::Kampai.Game.QuestState.RunningCompleteScript);
			}
			else if (quest.GetActiveDefinition().GetReward(definitionService) != null)
			{
				GoToQuestState(quest, global::Kampai.Game.QuestState.Harvestable);
			}
			else
			{
				GoToQuestState(quest, global::Kampai.Game.QuestState.Complete);
			}
		}

		public void GoToQuestState(global::Kampai.Game.Quest quest, global::Kampai.Game.QuestState targetState)
		{
			quest.state = targetState;
			switch (targetState)
			{
			case global::Kampai.Game.QuestState.Notstarted:
				break;
			case global::Kampai.Game.QuestState.RunningStartScript:
				StartQuestScript(quest, true);
				updateWorldIconSignal.Dispatch(quest);
				break;
			case global::Kampai.Game.QuestState.RunningTasks:
				UpdateConstructionTask();
				UpdateHarvestTask();
				updateWorldIconSignal.Dispatch(quest);
				if (quest.GetActiveDefinition().SurfaceType != global::Kampai.Game.QuestSurfaceType.ProcedurallyGenerated)
				{
					showQuestPanelSignal.Dispatch(quest.ID);
				}
				break;
			case global::Kampai.Game.QuestState.RunningCompleteScript:
				StartQuestScript(quest, false);
				break;
			case global::Kampai.Game.QuestState.Harvestable:
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.QuestHarvestableSignal>().Dispatch(quest);
				break;
			case global::Kampai.Game.QuestState.Complete:
				if (quest.GetActiveDefinition().GetReward(definitionService) != null)
				{
					gameContext.injectionBinder.GetInstance<global::Kampai.Game.RemoveQuestWorldIconSignal>().Dispatch(quest);
				}
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.QuestCompleteSignal>().Dispatch(quest);
				break;
			}
		}

		public global::Kampai.Game.QuestStepState GoToNextTaskState(global::Kampai.Game.Quest quest, int stepIndex, bool isTaskComplete = false)
		{
			if (isTaskComplete)
			{
				quest.Steps[stepIndex].state = global::Kampai.Game.QuestStepState.Ready;
			}
			switch (quest.Steps[stepIndex].state)
			{
			case global::Kampai.Game.QuestStepState.Notstarted:
				telemetryService.Send_Telemetry_EVT_GP_ACHIEVEMENTS_STARTED_EAL(GetEventName(quest.GetActiveDefinition().LocalizedKey), global::Kampai.Common.Service.Telemetry.AchievementType.QuestStep, string.Empty);
				if (!HasScript(quest, true, stepIndex, true))
				{
					InprogressStateCheck(quest, stepIndex);
				}
				else
				{
					GoToTaskState(quest, stepIndex, global::Kampai.Game.QuestStepState.RunningStartScript);
				}
				break;
			case global::Kampai.Game.QuestStepState.RunningStartScript:
				InprogressStateCheck(quest, stepIndex);
				break;
			case global::Kampai.Game.QuestStepState.Inprogress:
				GoToTaskState(quest, stepIndex, global::Kampai.Game.QuestStepState.Ready);
				questTaskReadySignal.Dispatch(quest, stepIndex);
				break;
			case global::Kampai.Game.QuestStepState.Ready:
			case global::Kampai.Game.QuestStepState.WaitComplete:
				if (!HasScript(quest, false, stepIndex, true))
				{
					GoToTaskState(quest, stepIndex, global::Kampai.Game.QuestStepState.Complete);
				}
				else
				{
					GoToTaskState(quest, stepIndex, global::Kampai.Game.QuestStepState.RunningCompleteScript);
				}
				break;
			case global::Kampai.Game.QuestStepState.RunningCompleteScript:
				GoToTaskState(quest, stepIndex, global::Kampai.Game.QuestStepState.Complete);
				break;
			}
			return quest.Steps[stepIndex].state;
		}

		private void InprogressStateCheck(global::Kampai.Game.Quest quest, int stepIndex)
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.QuestStepDefinition> questSteps = quest.GetActiveDefinition().QuestSteps;
			global::Kampai.Game.QuestStep questStep = quest.Steps[stepIndex];
			global::Kampai.Game.QuestStepDefinition questStepDefinition = questSteps[stepIndex];
			questStep.state = global::Kampai.Game.QuestStepState.Inprogress;
			if (questStepDefinition.Type == global::Kampai.Game.QuestStepType.Construction && questStep.AmountCompleted >= questStepDefinition.ItemAmount)
			{
				GoToNextTaskState(quest, stepIndex, true);
			}
		}

		public void GoToTaskState(global::Kampai.Game.Quest quest, int stepIndex, global::Kampai.Game.QuestStepState targetState)
		{
			global::Kampai.Game.QuestStep questStep = quest.Steps[stepIndex];
			global::Kampai.Game.QuestStepState state = questStep.state;
			questStep.state = targetState;
			if (state == global::Kampai.Game.QuestStepState.Ready && targetState == global::Kampai.Game.QuestStepState.Inprogress)
			{
				updateWorldIconSignal.Dispatch(quest);
			}
			switch (targetState)
			{
			case global::Kampai.Game.QuestStepState.RunningStartScript:
				StartQuestScript(quest, true, stepIndex, true);
				break;
			case global::Kampai.Game.QuestStepState.Ready:
			{
				global::System.Collections.Generic.IList<global::Kampai.Game.QuestStepDefinition> questSteps = quest.GetActiveDefinition().QuestSteps;
				if (questSteps != null && questSteps.Count > 0 && questSteps[0].Type != global::Kampai.Game.QuestStepType.OrderBoard)
				{
					updateSignal.Dispatch(quest.ID);
				}
				updateWorldIconSignal.Dispatch(quest);
				break;
			}
			case global::Kampai.Game.QuestStepState.RunningCompleteScript:
				StartQuestScript(quest, false, stepIndex, true);
				break;
			case global::Kampai.Game.QuestStepState.Complete:
				CheckAndUpdateQuestCompleteState(quest);
				telemetryService.Send_Telemetry_EVT_GP_ACHIEVEMENTS_CHECKPOINTS_EAL(GetEventName(quest.GetActiveDefinition().LocalizedKey), global::Kampai.Common.Service.Telemetry.AchievementType.QuestStep, string.Empty);
				break;
			case global::Kampai.Game.QuestStepState.Inprogress:
				break;
			}
		}

		public bool HasScript(global::Kampai.Game.Quest quest, bool pre)
		{
			return HasScript(quest, pre, -1);
		}

		public bool HasScript(global::Kampai.Game.Quest quest, bool pre, int stepID, bool isQuestStep = false)
		{
			string scriptName = string.Empty;
			return HasScript(quest, pre, stepID, out scriptName, isQuestStep);
		}

		public bool HasScript(global::Kampai.Game.Quest quest, bool pre, int stepID, out string scriptName, bool isQuestStep)
		{
			scriptName = "quest_" + quest.GetActiveDefinition().ID;
			if (stepID > -1)
			{
				scriptName = scriptName + "_step_" + (stepID + 1);
			}
			scriptName += ((!pre) ? "_post" : "_pre");
			if (global::Kampai.Util.KampaiResources.FileExists(scriptName))
			{
				return true;
			}
			scriptName = string.Empty;
			if (isQuestStep)
			{
				return false;
			}
			return HasIntroVoiceOrOutro(quest, pre);
		}

		private bool HasIntroVoiceOrOutro(global::Kampai.Game.Quest quest, bool pre)
		{
			bool flag = quest.GetActiveDefinition().QuestIntro != null && quest.GetActiveDefinition().QuestIntro.Length > 0;
			bool flag2 = quest.GetActiveDefinition().QuestVoice != null && quest.GetActiveDefinition().QuestVoice.Length > 0;
			bool flag3 = quest.GetActiveDefinition().QuestOutro != null && quest.GetActiveDefinition().QuestOutro.Length > 0;
			return (pre && (flag || flag2)) || (!pre && flag3);
		}

		public void StartQuestScript(global::Kampai.Game.Quest quest, bool pre)
		{
			StartQuestScript(quest, pre, -1);
		}

		public void StartQuestScript(global::Kampai.Game.Quest quest, bool pre, int stepID, bool isStepQuest = false)
		{
			string scriptName = string.Empty;
			if (!HasScript(quest, pre, stepID, out scriptName, isStepQuest))
			{
				GoToNextQuestState(quest);
				return;
			}
			string text = null;
			if (HasIntroVoiceOrOutro(quest, pre))
			{
				text = CreateDialogText(quest, pre);
			}
			if (scriptName.Length > 0)
			{
				global::UnityEngine.TextAsset textAsset = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.TextAsset>(scriptName);
				if (textAsset == null)
				{
					logger.Error("Failed to load script {0}", scriptName);
					return;
				}
				text = ((!string.IsNullOrEmpty(text)) ? (text + textAsset.text) : textAsset.text);
			}
			if (!string.IsNullOrEmpty(text))
			{
				if (string.IsNullOrEmpty(scriptName))
				{
					global::Kampai.Game.QuestDefinition activeDefinition = quest.GetActiveDefinition();
					scriptName = activeDefinition.ID + "_" + ((!pre) ? "Outro" : "Intro");
				}
				CreateRunningQuest(quest, scriptName, stepID, text);
			}
		}

		private string CreateDialogText(global::Kampai.Game.Quest quest, bool pre)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			int num = 0;
			int num2 = 0;
			global::Kampai.Game.QuestDefinition activeDefinition = quest.GetActiveDefinition();
			if (pre)
			{
				string questIntro = quest.GetActiveDefinition().QuestIntro;
				string questVoice = quest.GetActiveDefinition().QuestVoice;
				bool flag = questIntro != null && questIntro.Length > 0;
				if (flag)
				{
					text = questIntro;
					num = activeDefinition.SurfaceID;
				}
				if (questVoice != null && questVoice.Length > 0)
				{
					if (flag)
					{
						text2 = questVoice;
						num2 = activeDefinition.SurfaceID;
					}
					else
					{
						text = questVoice;
						num = activeDefinition.SurfaceID;
					}
				}
			}
			else
			{
				string questOutro = quest.GetActiveDefinition().QuestOutro;
				if (questOutro != null && questOutro.Length > 0)
				{
					text = questOutro;
					num = activeDefinition.SurfaceID;
				}
			}
			if (text.Length > 0)
			{
				return "qsutil.showIntroVoiceOrOutroDialogs('" + text + "', " + num + ", '" + text2 + "', " + num2 + ")" + global::System.Environment.NewLine;
			}
			return null;
		}

		private void CreateRunningQuest(global::Kampai.Game.Quest quest, string scriptName, int stepID, string data)
		{
			global::Kampai.Game.IQuestScriptRunner orCreateRunner = GetOrCreateRunner(scriptName, global::Kampai.Game.QuestRunnerLanguage.Lua);
			orCreateRunner.OnQuestScriptComplete = OnQuestScriptComplete;
			if (quest.questScriptInstances.ContainsKey(scriptName))
			{
				quest.questScriptInstances[scriptName].QuestID = quest.GetActiveDefinition().ID;
				quest.questScriptInstances[scriptName].QuestLocalizedKey = quest.GetActiveDefinition().LocalizedKey;
				quest.questScriptInstances[scriptName].QuestStepID = stepID;
				quest.questScriptInstances[scriptName].Key = scriptName;
			}
			else
			{
				global::Kampai.Game.QuestScriptInstance questScriptInstance = new global::Kampai.Game.QuestScriptInstance();
				questScriptInstance.QuestID = quest.GetActiveDefinition().ID;
				questScriptInstance.QuestLocalizedKey = quest.GetActiveDefinition().LocalizedKey;
				questScriptInstance.QuestStepID = stepID;
				questScriptInstance.Key = scriptName;
				quest.questScriptInstances.Add(scriptName, questScriptInstance);
			}
			orCreateRunner.Start(filename: (!scriptName.EndsWith("_Intro") && !scriptName.EndsWith("_Outro")) ? ("./" + scriptName + ".txt") : null, questScriptInstance: quest.questScriptInstances[scriptName], scriptText: data, startMethodName: null);
		}

		public void PauseQuestScripts()
		{
			global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>();
			foreach (string key in runners.Keys)
			{
				list.Add(key);
			}
			foreach (string item in list)
			{
				runners[item].Pause();
			}
		}

		public void ResumeQuestScripts()
		{
			global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>();
			foreach (string key in runners.Keys)
			{
				list.Add(key);
			}
			foreach (string item in list)
			{
				runners[item].Resume();
			}
		}

		public void OnQuestScriptComplete(global::Kampai.Game.QuestScriptInstance questScriptInstance)
		{
			global::Kampai.Game.Quest quest = quests[questScriptInstance.QuestID];
			if (quest.state == global::Kampai.Game.QuestState.RunningTasks)
			{
				int questStepID = questScriptInstance.QuestStepID;
				if (questStepID < 0 || questStepID > quest.Steps.Count)
				{
					logger.Error("QuestService:OnQuestScriptComplete: QuestStepId {0} is out of range! Can't mark as complete!", questStepID);
				}
				else if (quest.Steps[questScriptInstance.QuestStepID].state == global::Kampai.Game.QuestStepState.RunningStartScript)
				{
					quest.Steps[questScriptInstance.QuestStepID].state = global::Kampai.Game.QuestStepState.Inprogress;
				}
				else
				{
					quest.Steps[questScriptInstance.QuestStepID].state = global::Kampai.Game.QuestStepState.Complete;
					CheckAndUpdateQuestCompleteState(quest);
				}
			}
			else if (quest.state != global::Kampai.Game.QuestState.Harvestable)
			{
				GoToNextQuestState(quest);
			}
			runners.Remove(questScriptInstance.Key);
		}

		public void RushQuestStep(int questId, int step)
		{
			global::Kampai.Game.Quest quest = quests[questId];
			GoToNextTaskState(quest, step, true);
		}

		public bool IsBridgeQuestComplete(int bridgeDefId)
		{
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::Kampai.Game.Quest> quest in quests)
			{
				global::Kampai.Game.Quest value = quest.Value;
				if (value.state != global::Kampai.Game.QuestState.Complete || value.GetActiveDefinition().SurfaceType != global::Kampai.Game.QuestSurfaceType.Bridge)
				{
					continue;
				}
				global::System.Collections.Generic.IList<global::Kampai.Game.QuestStepDefinition> questSteps = value.GetActiveDefinition().QuestSteps;
				for (int i = 0; i < questSteps.Count; i++)
				{
					global::Kampai.Game.QuestStepDefinition questStepDefinition = questSteps[i];
					if (questStepDefinition.Type == global::Kampai.Game.QuestStepType.BridgeRepair && questStepDefinition.ItemDefinitionID == bridgeDefId)
					{
						return IsQuestCompleted(value.GetActiveDefinition().ID);
					}
				}
			}
			return false;
		}

		private void UpdateTask(global::Kampai.Game.QuestStepType type, global::Kampai.Game.Building building = null, global::Kampai.Game.QuestTaskTransition questTaskTransition = global::Kampai.Game.QuestTaskTransition.Start, int buildingDefId = 0, int item = 0)
		{
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::Kampai.Game.Quest> quest in quests)
			{
				global::Kampai.Game.Quest value = quest.Value;
				if (value.state != global::Kampai.Game.QuestState.RunningTasks)
				{
					continue;
				}
				global::System.Collections.Generic.IList<global::Kampai.Game.QuestStepDefinition> questSteps = value.GetActiveDefinition().QuestSteps;
				for (int i = 0; i < questSteps.Count; i++)
				{
					global::Kampai.Game.QuestStepDefinition questStepDefinition = questSteps[i];
					global::Kampai.Game.QuestStep questStep = value.Steps[i];
					if (questStepDefinition.Type != type || questStep.state == global::Kampai.Game.QuestStepState.Complete)
					{
						continue;
					}
					switch (type)
					{
					case global::Kampai.Game.QuestStepType.Construction:
						UpdateConstructionType(questStepDefinition, questStep, value, i);
						break;
					case global::Kampai.Game.QuestStepType.Mignette:
						UpdateMignetteType(questStepDefinition, questTaskTransition, questStep, value, i, building);
						break;
					case global::Kampai.Game.QuestStepType.Delivery:
						UpdateDeliveryType(questStepDefinition, questStep, value, i);
						break;
					case global::Kampai.Game.QuestStepType.BridgeRepair:
						if (questStep.TrackedID == building.ID)
						{
							UpdateBridgeType(questTaskTransition, questStep, value, i);
						}
						break;
					case global::Kampai.Game.QuestStepType.MinionTask:
						if (questStep.TrackedID == buildingDefId)
						{
							UpdateMinionTaskType(questStepDefinition, questTaskTransition, questStep, value, i);
						}
						break;
					case global::Kampai.Game.QuestStepType.OrderBoard:
					{
						int num = UpdateOrderBoardType(questStepDefinition, questTaskTransition, questStep, value, i);
						if (num != 1)
						{
						}
						break;
					}
					case global::Kampai.Game.QuestStepType.Harvest:
						if (item == 0 || questStepDefinition.ItemDefinitionID == item)
						{
							UpdateHarvestType(questStepDefinition, questTaskTransition, questStep, value, i);
						}
						break;
					case global::Kampai.Game.QuestStepType.StageRepair:
					case global::Kampai.Game.QuestStepType.WelcomeHutRepair:
					case global::Kampai.Game.QuestStepType.FountainRepair:
						if (questTaskTransition == global::Kampai.Game.QuestTaskTransition.Complete)
						{
							GoToTaskState(value, i, global::Kampai.Game.QuestStepState.Complete);
						}
						break;
					case global::Kampai.Game.QuestStepType.CabanaRepair:
						if (questStep.TrackedID == building.ID && questTaskTransition == global::Kampai.Game.QuestTaskTransition.Complete)
						{
							GoToTaskState(value, i, global::Kampai.Game.QuestStepState.Complete);
						}
						break;
					default:
						logger.Fatal(global::Kampai.Util.FatalCode.EX_INVALID_ENUM);
						return;
					}
				}
			}
		}

		private void UpdateMignetteType(global::Kampai.Game.QuestStepDefinition questStepDefinition, global::Kampai.Game.QuestTaskTransition questTaskTransition, global::Kampai.Game.QuestStep questStep, global::Kampai.Game.Quest quest, int step, global::Kampai.Game.Building building)
		{
			global::Kampai.Game.QuestStepState state = questStep.state;
			int iD = building.Definition.ID;
			if (questStepDefinition.ItemDefinitionID != iD)
			{
				return;
			}
			if (questTaskTransition == global::Kampai.Game.QuestTaskTransition.Start && state == global::Kampai.Game.QuestStepState.Notstarted)
			{
				GoToNextTaskState(quest, step);
			}
			else if (questTaskTransition == global::Kampai.Game.QuestTaskTransition.Complete && state == global::Kampai.Game.QuestStepState.Inprogress)
			{
				questStep.AmountCompleted++;
				if (questStep.AmountCompleted >= questStepDefinition.ItemAmount)
				{
					GoToTaskState(quest, step, global::Kampai.Game.QuestStepState.Complete);
				}
			}
		}

		private void UpdateConstructionType(global::Kampai.Game.QuestStepDefinition questStepDefinition, global::Kampai.Game.QuestStep questStep, global::Kampai.Game.Quest quest, int step)
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			global::System.Collections.Generic.ICollection<global::Kampai.Game.Building> byDefinitionId = playerService.GetByDefinitionId<global::Kampai.Game.Building>(questStepDefinition.ItemDefinitionID);
			foreach (global::Kampai.Game.Building item in byDefinitionId)
			{
				global::Kampai.Game.BuildingState state = item.State;
				global::Kampai.Game.QuestStepState state2 = questStep.state;
				int iD = item.Definition.ID;
				if (questStepDefinition.ItemDefinitionID == iD)
				{
					global::Kampai.Game.Building unreadyBuilding = GetUnreadyBuilding(iD);
					questStep.TrackedID = ((unreadyBuilding != null) ? unreadyBuilding.ID : 0);
					if (state2 == global::Kampai.Game.QuestStepState.Notstarted)
					{
						GoToNextTaskState(quest, step);
						flag = true;
					}
					switch (state)
					{
					case global::Kampai.Game.BuildingState.Complete:
						num++;
						break;
					case global::Kampai.Game.BuildingState.Idle:
					case global::Kampai.Game.BuildingState.Working:
					case global::Kampai.Game.BuildingState.Harvestable:
					case global::Kampai.Game.BuildingState.Inventory:
					case global::Kampai.Game.BuildingState.Cooldown:
					case global::Kampai.Game.BuildingState.HarvestableAndWorking:
						num2++;
						break;
					}
				}
			}
			questStep.AmountReady = num;
			questStep.AmountCompleted = num2;
			if (questStep.state != global::Kampai.Game.QuestStepState.Ready && questStep.AmountReady + questStep.AmountCompleted >= questStepDefinition.ItemAmount)
			{
				GoToTaskState(quest, step, global::Kampai.Game.QuestStepState.Ready);
			}
			if (questStep.AmountCompleted >= questStepDefinition.ItemAmount)
			{
				if (flag)
				{
					GoToTaskState(quest, step, global::Kampai.Game.QuestStepState.WaitComplete);
				}
				else
				{
					GoToNextTaskState(quest, step, true);
				}
			}
		}

		private global::Kampai.Game.Building GetUnreadyBuilding(int buildingDefId)
		{
			global::System.Collections.Generic.ICollection<global::Kampai.Game.Building> byDefinitionId = playerService.GetByDefinitionId<global::Kampai.Game.Building>(buildingDefId);
			foreach (global::Kampai.Game.Building item in byDefinitionId)
			{
				global::Kampai.Game.BuildingState state = item.State;
				if (state == global::Kampai.Game.BuildingState.Inactive || state == global::Kampai.Game.BuildingState.Construction || state == global::Kampai.Game.BuildingState.Complete)
				{
					return item;
				}
			}
			return null;
		}

		private void UpdateDeliveryType(global::Kampai.Game.QuestStepDefinition questStepDefinition, global::Kampai.Game.QuestStep questStep, global::Kampai.Game.Quest quest, int step)
		{
			questStep.AmountCompleted = (int)playerService.GetQuantityByDefinitionId(questStepDefinition.ItemDefinitionID);
			if (questStep.state == global::Kampai.Game.QuestStepState.Notstarted)
			{
				GoToNextTaskState(quest, step);
			}
			else if (questStep.AmountCompleted < questStepDefinition.ItemAmount)
			{
				GoToTaskState(quest, step, global::Kampai.Game.QuestStepState.Inprogress);
			}
			else if (questStep.state != global::Kampai.Game.QuestStepState.Ready)
			{
				GoToTaskState(quest, step, global::Kampai.Game.QuestStepState.Ready);
			}
		}

		private void UpdateBridgeType(global::Kampai.Game.QuestTaskTransition taskTransition, global::Kampai.Game.QuestStep questStep, global::Kampai.Game.Quest quest, int step)
		{
			if (taskTransition == global::Kampai.Game.QuestTaskTransition.Start && questStep.state == global::Kampai.Game.QuestStepState.Notstarted)
			{
				GoToNextTaskState(quest, step);
			}
			if (taskTransition == global::Kampai.Game.QuestTaskTransition.Complete)
			{
				GoToTaskState(quest, step, global::Kampai.Game.QuestStepState.Complete);
			}
		}

		private void UpdateMinionTaskType(global::Kampai.Game.QuestStepDefinition questStepDefinition, global::Kampai.Game.QuestTaskTransition taskTransition, global::Kampai.Game.QuestStep questStep, global::Kampai.Game.Quest quest, int step)
		{
			if (taskTransition == global::Kampai.Game.QuestTaskTransition.Start && questStep.state == global::Kampai.Game.QuestStepState.Notstarted)
			{
				GoToNextTaskState(quest, step);
			}
			else if (taskTransition == global::Kampai.Game.QuestTaskTransition.Harvestable && questStep.state == global::Kampai.Game.QuestStepState.Inprogress)
			{
				questStep.AmountReady++;
				if (questStep.AmountReady + questStep.AmountCompleted >= questStepDefinition.ItemAmount)
				{
					GoToNextTaskState(quest, step);
				}
			}
			else if (taskTransition == global::Kampai.Game.QuestTaskTransition.Complete && (questStep.state == global::Kampai.Game.QuestStepState.Inprogress || questStep.state == global::Kampai.Game.QuestStepState.Ready))
			{
				questStep.AmountCompleted++;
				questStep.AmountReady--;
				if (questStep.AmountCompleted >= questStepDefinition.ItemAmount)
				{
					GoToNextTaskState(quest, step, true);
				}
			}
		}

		private void UpdateHarvestType(global::Kampai.Game.QuestStepDefinition questStepDefinition, global::Kampai.Game.QuestTaskTransition taskTransition, global::Kampai.Game.QuestStep questStep, global::Kampai.Game.Quest quest, int step)
		{
			questStep.AmountCompleted = (int)playerService.GetQuantityByDefinitionId(questStepDefinition.ItemDefinitionID);
			questStep.AmountReady = GetHarvestableCount(questStepDefinition.ItemDefinitionID);
			if (taskTransition == global::Kampai.Game.QuestTaskTransition.Start && questStep.state == global::Kampai.Game.QuestStepState.Notstarted)
			{
				GoToNextTaskState(quest, step);
				if (questStep.AmountCompleted >= questStepDefinition.ItemAmount)
				{
					GoToTaskState(quest, step, global::Kampai.Game.QuestStepState.WaitComplete);
				}
			}
			else if (taskTransition == global::Kampai.Game.QuestTaskTransition.Harvestable && questStep.state == global::Kampai.Game.QuestStepState.Inprogress)
			{
				if (questStep.AmountReady + questStep.AmountCompleted >= questStepDefinition.ItemAmount)
				{
					GoToNextTaskState(quest, step);
				}
			}
			else if (taskTransition == global::Kampai.Game.QuestTaskTransition.Complete && (questStep.state == global::Kampai.Game.QuestStepState.Inprogress || questStep.state == global::Kampai.Game.QuestStepState.Ready))
			{
				if (questStep.AmountCompleted >= questStepDefinition.ItemAmount)
				{
					GoToTaskState(quest, step, global::Kampai.Game.QuestStepState.WaitComplete);
				}
			}
			else if (taskTransition == global::Kampai.Game.QuestTaskTransition.Start && questStep.AmountCompleted >= questStepDefinition.ItemAmount && questStep.state != global::Kampai.Game.QuestStepState.WaitComplete)
			{
				GoToNextTaskState(quest, step, true);
			}
		}

		private int GetHarvestableCount(int itemDefinitionId)
		{
			int num = 0;
			global::System.Collections.Generic.List<global::Kampai.Game.ResourceBuilding> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.ResourceBuilding>();
			global::System.Collections.Generic.List<global::Kampai.Game.CraftingBuilding> instancesByType2 = playerService.GetInstancesByType<global::Kampai.Game.CraftingBuilding>();
			int i = 0;
			for (int count = instancesByType.Count; i < count; i++)
			{
				global::Kampai.Game.ResourceBuilding resourceBuilding = instancesByType[i];
				if (resourceBuilding.Definition.ItemId == itemDefinitionId)
				{
					num += resourceBuilding.AvailableHarvest;
				}
			}
			int j = 0;
			for (int count2 = instancesByType2.Count; j < count2; j++)
			{
				global::Kampai.Game.CraftingBuilding craftingBuilding = instancesByType2[j];
				global::System.Collections.Generic.IList<int> completedCrafts = craftingBuilding.CompletedCrafts;
				int k = 0;
				for (int count3 = completedCrafts.Count; k < count3; k++)
				{
					if (completedCrafts[k] == itemDefinitionId)
					{
						num++;
					}
				}
			}
			return num;
		}

		private int UpdateOrderBoardType(global::Kampai.Game.QuestStepDefinition questStepDefinition, global::Kampai.Game.QuestTaskTransition taskTransition, global::Kampai.Game.QuestStep questStep, global::Kampai.Game.Quest quest, int step)
		{
			if (questStep.state == global::Kampai.Game.QuestStepState.Notstarted)
			{
				GoToNextTaskState(quest, step);
			}
			if (taskTransition == global::Kampai.Game.QuestTaskTransition.Harvestable)
			{
				if (questStep.state == global::Kampai.Game.QuestStepState.Inprogress)
				{
					questStep.AmountReady++;
					if (questStep.AmountReady + questStep.AmountCompleted >= questStepDefinition.ItemAmount)
					{
						GoToNextTaskState(quest, step);
					}
				}
			}
			else
			{
				if (questStep.state != global::Kampai.Game.QuestStepState.Inprogress && questStep.state != global::Kampai.Game.QuestStepState.Ready)
				{
					return 1;
				}
				questStep.AmountCompleted++;
				questStep.AmountReady--;
				if (questStep.AmountCompleted >= questStepDefinition.ItemAmount)
				{
					GoToNextTaskState(quest, step, true);
					if (quest.GetActiveDefinition().SurfaceType == global::Kampai.Game.QuestSurfaceType.ProcedurallyGenerated)
					{
						updateSignal.Dispatch(quest.ID);
					}
				}
			}
			return 0;
		}

		public void UpdateBlackMarketTask()
		{
			UpdateTask(global::Kampai.Game.QuestStepType.OrderBoard);
		}

		public void UpdateDeliveryTask()
		{
			UpdateTask(global::Kampai.Game.QuestStepType.Delivery);
		}

		public void UpdateConstructionTask()
		{
			UpdateTask(global::Kampai.Game.QuestStepType.Construction);
		}

		public void UpdateMignetteTask(global::Kampai.Game.Building building, global::Kampai.Game.QuestTaskTransition taskState)
		{
			UpdateTask(global::Kampai.Game.QuestStepType.Mignette, building, taskState);
		}

		public void UpdateBridgeRepairTask(global::Kampai.Game.Building building, global::Kampai.Game.QuestTaskTransition taskTransition)
		{
			UpdateTask(global::Kampai.Game.QuestStepType.BridgeRepair, building, taskTransition);
		}

		public void UpdateCabanaRepairTask(global::Kampai.Game.Building building, global::Kampai.Game.QuestTaskTransition taskTransition)
		{
			UpdateTask(global::Kampai.Game.QuestStepType.CabanaRepair, building, taskTransition);
		}

		public void UpdateHarvestTask()
		{
			UpdateTask(global::Kampai.Game.QuestStepType.Harvest);
		}

		public void UpdateHarvestTask(int item, global::Kampai.Game.QuestTaskTransition taskTransition)
		{
			UpdateTask(global::Kampai.Game.QuestStepType.Harvest, null, taskTransition, 0, item);
		}

		public void StartMinionTask(int buildingDefId)
		{
			UpdateTask(global::Kampai.Game.QuestStepType.MinionTask, null, global::Kampai.Game.QuestTaskTransition.Start, buildingDefId);
		}

		public void HarvestReady(int buildingDefId)
		{
			global::Kampai.Game.Definition definition = definitionService.Get(buildingDefId);
			global::Kampai.Game.BlackMarketBoardDefinition blackMarketBoardDefinition = definition as global::Kampai.Game.BlackMarketBoardDefinition;
			if (blackMarketBoardDefinition != null)
			{
				UpdateTask(global::Kampai.Game.QuestStepType.OrderBoard, null, global::Kampai.Game.QuestTaskTransition.Harvestable);
			}
			else
			{
				UpdateTask(global::Kampai.Game.QuestStepType.MinionTask, null, global::Kampai.Game.QuestTaskTransition.Harvestable, buildingDefId);
			}
		}

		public void HarvestTaskableComplete(int buildingDefId)
		{
			UpdateTask(global::Kampai.Game.QuestStepType.MinionTask, null, global::Kampai.Game.QuestTaskTransition.Complete, buildingDefId);
		}

		public void RepairStage()
		{
			UpdateTask(global::Kampai.Game.QuestStepType.StageRepair, null, global::Kampai.Game.QuestTaskTransition.Complete);
		}

		public void RepairWelcomeHut()
		{
			UpdateTask(global::Kampai.Game.QuestStepType.WelcomeHutRepair, null, global::Kampai.Game.QuestTaskTransition.Complete);
		}

		public void RepairFountain()
		{
			UpdateTask(global::Kampai.Game.QuestStepType.FountainRepair, null, global::Kampai.Game.QuestTaskTransition.Complete);
		}

		public void stop()
		{
			foreach (global::Kampai.Game.IQuestScriptRunner value in runners.Values)
			{
				value.Stop();
			}
			runners.Clear();
		}

		private void ERROR(string message)
		{
			logger.Log(global::Kampai.Util.Logger.Level.Error, true, string.Format("QuestService ERROR {1}", message));
		}

		private global::Kampai.Game.Quest loadQuest(int questDefinitionId)
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.Quest> list = playerService.GetByDefinitionId<global::Kampai.Game.Quest>(questDefinitionId) as global::System.Collections.Generic.IList<global::Kampai.Game.Quest>;
			if (list == null)
			{
				return null;
			}
			global::Kampai.Game.Quest quest;
			if (list.Count == 0)
			{
				quest = new global::Kampai.Game.Quest(definitionService.Get<global::Kampai.Game.QuestDefinition>(questDefinitionId));
				quest.Initialize();
				playerService.Add(quest);
			}
			else
			{
				quest = list[0];
			}
			return quest;
		}

		private void StartTimedQuest(global::Kampai.Game.Quest quest)
		{
			global::Kampai.Game.TimedQuestDefinition timedQuestDefinition = quest.GetActiveDefinition() as global::Kampai.Game.TimedQuestDefinition;
			if (timedQuestDefinition != null)
			{
				quest.UTCQuestStartTime = timeService.GameTimeSeconds();
				questNoteSignal.Dispatch(quest.ID);
				timeEventService.AddEvent(quest.ID, quest.UTCQuestStartTime, timedQuestDefinition.Duration, timeoutSignal);
			}
		}

		private void CheckAndStartQuestTimers(global::Kampai.Game.Quest quest)
		{
			if (quest.GetActiveDefinition().SurfaceType == global::Kampai.Game.QuestSurfaceType.LimitedEvent)
			{
				global::Kampai.Game.LimitedQuestDefinition limitedQuestDefinition = quest.GetActiveDefinition() as global::Kampai.Game.LimitedQuestDefinition;
				if (limitedQuestDefinition != null)
				{
					if (limitedQuestDefinition.ServerStopTimeUTC <= timeService.GameTimeSeconds())
					{
						RemoveQuest(quest);
						return;
					}
					int eventTime = limitedQuestDefinition.ServerStopTimeUTC - limitedQuestDefinition.ServerStartTimeUTC;
					timeEventService.AddEvent(quest.ID, limitedQuestDefinition.ServerStartTimeUTC, eventTime, timeoutSignal);
				}
			}
			else
			{
				if (quest.GetActiveDefinition().SurfaceType != global::Kampai.Game.QuestSurfaceType.TimedEvent || quest.state == global::Kampai.Game.QuestState.Notstarted || quest.state == global::Kampai.Game.QuestState.Complete)
				{
					return;
				}
				global::Kampai.Game.TimedQuestDefinition timedQuestDefinition = quest.GetActiveDefinition() as global::Kampai.Game.TimedQuestDefinition;
				if (timedQuestDefinition != null)
				{
					if (quest.UTCQuestStartTime == 0)
					{
						logger.Log(global::Kampai.Util.Logger.Level.Error, "The UTCQuestStartTime is not set for the timed quest!");
						return;
					}
					if (timeService.GameTimeSeconds() > timedQuestDefinition.Duration + quest.UTCQuestStartTime)
					{
						RemoveQuest(quest);
						return;
					}
					questNoteSignal.Dispatch(quest.ID);
					timeEventService.AddEvent(quest.ID, quest.UTCQuestStartTime, timedQuestDefinition.Duration, timeoutSignal);
				}
			}
		}

		private void CheckAndUpdateQuestCompleteState(global::Kampai.Game.Quest quest)
		{
			foreach (global::Kampai.Game.QuestStep step in quest.Steps)
			{
				if (step.state != global::Kampai.Game.QuestStepState.Complete)
				{
					quest.AutoGrantReward = false;
					updateWorldIconSignal.Dispatch(quest);
					return;
				}
			}
			RunningHarvestableStateValidation(quest);
		}

		private global::Kampai.Game.IQuestScriptRunner GetOrCreateRunner(string key, global::Kampai.Game.QuestRunnerLanguage lang)
		{
			global::Kampai.Game.IQuestScriptRunner value;
			runners.TryGetValue(key, out value);
			if (value == null || value.Lang != lang)
			{
				value = gameContext.injectionBinder.GetInstance<global::Kampai.Game.IQuestScriptRunner>(lang);
				runners[key] = value;
			}
			return value;
		}

		private void CreateQuestMap()
		{
			global::System.Collections.Generic.ICollection<global::Kampai.Game.Quest> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.Quest>();
			foreach (global::Kampai.Game.Quest item in instancesByType)
			{
				global::Kampai.Game.QuestDefinition activeDefinition = item.GetActiveDefinition();
				int iD = activeDefinition.ID;
				if (quests.ContainsKey(iD))
				{
					continue;
				}
				logger.Info("Restoring quest def id:{0} from player data", iD);
				quests.Add(iD, item);
				if (!item.IsDynamic())
				{
					global::Kampai.Game.QuestLine questLine = questLines[activeDefinition.QuestLineID];
					if (questLine.state == global::Kampai.Game.QuestLineState.NotStarted)
					{
						questLine.state = global::Kampai.Game.QuestLineState.Started;
					}
					if (questLine.Quests.Count == 1 && item.state == global::Kampai.Game.QuestState.Complete)
					{
						questLine.state = global::Kampai.Game.QuestLineState.Finished;
					}
				}
			}
		}

		private void LoadQuestLines()
		{
			global::System.Collections.Generic.Dictionary<int, int> dictionary = new global::System.Collections.Generic.Dictionary<int, int>();
			global::System.Collections.Generic.IList<global::Kampai.Game.QuestDefinition> all = definitionService.GetAll<global::Kampai.Game.QuestDefinition>();
			foreach (global::Kampai.Game.QuestDefinition item in all)
			{
				if (item.SurfaceID < 0 || item.SurfaceType < global::Kampai.Game.QuestSurfaceType.Building)
				{
					continue;
				}
				dictionary.Add(item.ID, item.QuestLineID);
				if (questLines.ContainsKey(item.QuestLineID))
				{
					int num = 0;
					bool flag = false;
					global::System.Collections.Generic.IList<global::Kampai.Game.QuestDefinition> list = questLines[item.QuestLineID].Quests;
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i].NarrativeOrder < item.NarrativeOrder)
						{
							list.Insert(i, item);
							flag = true;
							break;
						}
						if (list[i].NarrativeOrder == item.NarrativeOrder)
						{
							if (item.ID >= list[i].ID)
							{
								list.Insert(i, item);
								flag = true;
								break;
							}
							num = i;
						}
					}
					if (!flag)
					{
						if (num == 0)
						{
							list.Add(item);
						}
						else
						{
							list.Insert(num, item);
						}
					}
				}
				else
				{
					global::System.Collections.Generic.List<global::Kampai.Game.QuestDefinition> list2 = new global::System.Collections.Generic.List<global::Kampai.Game.QuestDefinition>();
					list2.Add(item);
					global::Kampai.Game.QuestLine questLine = new global::Kampai.Game.QuestLine();
					questLine.state = global::Kampai.Game.QuestLineState.NotStarted;
					questLine.Quests = list2;
					questLines.Add(item.QuestLineID, questLine);
				}
			}
			SetupQuestLineCharacterInfo(dictionary);
		}

		private void SetupQuestLineCharacterInfo(global::System.Collections.Generic.Dictionary<int, int> questToQuestLine)
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.PrestigeDefinition> all = definitionService.GetAll<global::Kampai.Game.PrestigeDefinition>();
			foreach (global::Kampai.Game.PrestigeDefinition item in all)
			{
				int iD = item.ID;
				if (iD == 40000 || iD == 40014)
				{
					continue;
				}
				for (int i = 0; i < item.PrestigeLevelSettings.Count; i++)
				{
					global::Kampai.Game.CharacterPrestigeLevelDefinition characterPrestigeLevelDefinition = item.PrestigeLevelSettings[i];
					global::Kampai.Game.Prestige prestige = prestigeService.GetPrestige(iD, false);
					if (characterPrestigeLevelDefinition.UnlockQuestID != 0 && questToQuestLine.ContainsKey(characterPrestigeLevelDefinition.UnlockQuestID))
					{
						int num = questToQuestLine[characterPrestigeLevelDefinition.UnlockQuestID];
						global::Kampai.Game.QuestLine questLine = questLines[num];
						questLine.UnlockCharacterPrestigeLevel = i;
						if (prestige != null && prestige.CurrentPrestigeLevel >= i && questLine.state == global::Kampai.Game.QuestLineState.NotStarted)
						{
							SetQuestLineState(num, global::Kampai.Game.QuestLineState.Started);
						}
					}
					if (characterPrestigeLevelDefinition.AttachedQuestID == 0 || !questToQuestLine.ContainsKey(characterPrestigeLevelDefinition.AttachedQuestID))
					{
						continue;
					}
					int num2 = questToQuestLine[characterPrestigeLevelDefinition.AttachedQuestID];
					global::Kampai.Game.QuestLine questLine2 = questLines[num2];
					questLine2.GivenByCharacterID = iD;
					questLine2.GivenByCharacterPrestigeLevel = i;
					if (prestige != null)
					{
						if (prestige.CurrentPrestigeLevel > i || (prestige.CurrentPrestigeLevel == i && prestige.state == global::Kampai.Game.PrestigeState.Taskable))
						{
							questLine2.state = global::Kampai.Game.QuestLineState.Finished;
						}
						else if (prestige.CurrentPrestigeLevel == i && (prestige.state == global::Kampai.Game.PrestigeState.Questing || prestige.state == global::Kampai.Game.PrestigeState.TaskableWhileQuesting) && questLine2.state == global::Kampai.Game.QuestLineState.NotStarted)
						{
							SetQuestLineState(num2, global::Kampai.Game.QuestLineState.Started);
						}
					}
				}
			}
		}

		private void CreateQuestUnlockTree()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::Kampai.Game.QuestLine> questLine in questLines)
			{
				global::System.Collections.Generic.IList<global::Kampai.Game.QuestDefinition> list = questLine.Value.Quests;
				int count = list.Count;
				for (int i = 0; i < count; i++)
				{
					global::Kampai.Game.QuestDefinition questDefinition = list[i];
					int iD = questDefinition.ID;
					if (questDefinition.SurfaceID < 0)
					{
						continue;
					}
					if (i < count - 1)
					{
						global::Kampai.Game.QuestDefinition questDefinition2 = list[i + 1];
						int iD2 = questDefinition2.ID;
						if (!questUnlockTree.ContainsKey(questDefinition2.ID))
						{
							global::System.Collections.Generic.List<int> list2 = new global::System.Collections.Generic.List<int>();
							list2.Add(iD);
							questUnlockTree.Add(iD2, list2);
						}
						else if (!questUnlockTree[iD2].Contains(iD))
						{
							questUnlockTree[iD2].Add(iD);
						}
					}
					if (questDefinition.UnlockQuestId != 0)
					{
						if (!questUnlockTree.ContainsKey(questDefinition.UnlockQuestId))
						{
							global::System.Collections.Generic.List<int> list3 = new global::System.Collections.Generic.List<int>();
							list3.Add(iD);
							questUnlockTree.Add(questDefinition.UnlockQuestId, list3);
						}
						else if (!questUnlockTree[questDefinition.UnlockQuestId].Contains(iD))
						{
							questUnlockTree[questDefinition.UnlockQuestId].Add(iD);
						}
						SetQuestDependency(questDefinition);
					}
				}
			}
		}

		private void SetQuestDependency(global::Kampai.Game.QuestDefinition questDefinition)
		{
			global::Kampai.Game.QuestDefinition questDefinition2 = definitionService.Get<global::Kampai.Game.QuestDefinition>(questDefinition.UnlockQuestId);
			if (questLines.ContainsKey(questDefinition.QuestLineID) && questLines.ContainsKey(questDefinition2.QuestLineID) && questLines[questDefinition2.QuestLineID].Quests[0].ID == questDefinition2.ID)
			{
				questLines[questDefinition.QuestLineID].unlockByQuestLine = questLines[questDefinition2.QuestLineID].QuestLineID;
			}
		}

		private void UpdateQuestLineStateBasedOnDependency()
		{
			global::System.Collections.Generic.List<global::Kampai.Game.QuestLine> list = new global::System.Collections.Generic.List<global::Kampai.Game.QuestLine>();
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::Kampai.Game.QuestLine> questLine2 in questLines)
			{
				global::Kampai.Game.QuestLine value = questLine2.Value;
				if (value.state == global::Kampai.Game.QuestLineState.NotStarted || list.Contains(value))
				{
					continue;
				}
				int unlockByQuestLine = value.unlockByQuestLine;
				while (unlockByQuestLine != 0 && unlockByQuestLine != -1 && questLines.ContainsKey(unlockByQuestLine))
				{
					global::Kampai.Game.QuestLine questLine = questLines[unlockByQuestLine];
					if (list.Contains(questLine))
					{
						break;
					}
					questLine.state = global::Kampai.Game.QuestLineState.Finished;
					list.Add(questLine);
					unlockByQuestLine = questLine.unlockByQuestLine;
				}
			}
		}

		public string GetEventName(string key, params object[] args)
		{
			if (key == null)
			{
				return key;
			}
			string text = key;
			string[] array = key.Split('_');
			string text2 = array[0];
			if (array.Length > 1)
			{
				text2 = text2 + "_" + array[1];
			}
			if (eventsLocalService.Contains(text2))
			{
				text = eventsLocalService.GetString(text2);
				if (array.Length > 1)
				{
					text = text + " " + array[1];
				}
			}
			return text;
		}
	}
}
