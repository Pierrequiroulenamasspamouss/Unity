namespace Kampai.Game
{
	public class GetNewQuestCommand : global::strange.extensions.command.impl.Command
	{
		private global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.QuestLine> questLines;

		private global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Quest> questMap;

		private global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<int>> questUnlockTree;

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Game.UpdateQuestWorldIconsSignal UpdateQuestWorldIconsSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Common.IRandomService randomService { get; set; }

		[Inject]
		public global::Kampai.Game.Environment environment { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService characterService { get; set; }

		[Inject(global::Kampai.Game.GameElement.BUILDING_MANAGER)]
		public global::UnityEngine.GameObject buildingManager { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			logger.Debug("Get New Quest Command");
			questLines = questService.GetQuestLines();
			questMap = questService.GetQuestMap();
			questUnlockTree = questService.GetQuestUnlockTree();
			foreach (global::Kampai.Game.QuestLine value in questLines.Values)
			{
				if (value.Quests.Count != 0 && value.state != global::Kampai.Game.QuestLineState.Finished)
				{
					ExamineQuestLine(value);
				}
			}
		}

		private void ExamineQuestLine(global::Kampai.Game.QuestLine questLine)
		{
			for (int i = 0; i < questLine.Quests.Count; i++)
			{
				global::Kampai.Game.QuestDefinition questDefinition = questLine.Quests[i];
				int iD = questDefinition.ID;
				if (UnlockValidation(questLine, questDefinition, i))
				{
					logger.Info("Unlocked Quest {0}", iD);
					global::Kampai.Game.Quest quest = new global::Kampai.Game.Quest(questDefinition);
					quest.Initialize();
					if (QuestValidation(quest))
					{
						questService.AddQuest(quest);
						logger.Debug("Unlocking New Quests... Quest Def ID: {0} Quest Surface Type: {1}", quest.GetActiveDefinition().ID, quest.GetActiveDefinition().SurfaceType.ToString());
						if (questDefinition.SurfaceType == global::Kampai.Game.QuestSurfaceType.Automatic || questDefinition.SurfaceType == global::Kampai.Game.QuestSurfaceType.Bridge)
						{
							ProcessAutomaticQuest(quest);
						}
						else
						{
							questService.GoToQuestState(quest, global::Kampai.Game.QuestState.Notstarted);
						}
						TryDeleteOldQuest(questLine, questDefinition, i);
						if (quest.state != global::Kampai.Game.QuestState.Complete)
						{
							routineRunner.StartCoroutine(WaitAFrame(quest));
						}
						break;
					}
				}
				else
				{
					global::Kampai.Game.Quest value;
					questMap.TryGetValue(iD, out value);
					if (value != null && NeedQuestTracking(value))
					{
						SetupQuestTracking(value);
					}
					if (questMap.ContainsKey(iD))
					{
						break;
					}
				}
			}
		}

		private void TryDeleteOldQuest(global::Kampai.Game.QuestLine questLine, global::Kampai.Game.QuestDefinition qd, int index)
		{
			if (index < questLine.Quests.Count - 1)
			{
				int iD = questLine.Quests[index + 1].ID;
				if (questMap.ContainsKey(iD) && QuestDeleteValidation(iD))
				{
					logger.Debug("Removing Quests... Quest Def ID: {0}", iD);
					questService.RemoveQuest(questMap[iD]);
				}
			}
			int unlockQuestId = qd.UnlockQuestId;
			if (unlockQuestId != 0 && questMap.ContainsKey(unlockQuestId) && QuestDeleteValidation(unlockQuestId))
			{
				logger.Debug("Removing Dependency Quests... Quest Def ID: {0}", unlockQuestId);
				questService.RemoveQuest(questMap[unlockQuestId]);
			}
		}

		private bool UnlockValidation(global::Kampai.Game.QuestLine questLine, global::Kampai.Game.QuestDefinition questDefinition, int indexInQuestLine)
		{
			int iD = questDefinition.ID;
			if (questMap.ContainsKey(iD))
			{
				return false;
			}
			if (playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID) < questDefinition.UnlockLevel)
			{
				return false;
			}
			if (questDefinition.UnlockQuestId != 0)
			{
				if (!questMap.ContainsKey(questDefinition.UnlockQuestId))
				{
					return false;
				}
				if (questMap.ContainsKey(questDefinition.UnlockQuestId) && questMap[questDefinition.UnlockQuestId].state != global::Kampai.Game.QuestState.Complete)
				{
					return false;
				}
			}
			if (indexInQuestLine < questLine.Quests.Count - 1)
			{
				int iD2 = questLine.Quests[indexInQuestLine + 1].ID;
				if (questMap.ContainsKey(iD2) && questMap[iD2].state != global::Kampai.Game.QuestState.Complete)
				{
					return false;
				}
				if (!questMap.ContainsKey(iD2))
				{
					return false;
				}
			}
			if (questUnlockTree.ContainsKey(iD))
			{
				foreach (int item in questUnlockTree[iD])
				{
					if (questMap.ContainsKey(item))
					{
						return false;
					}
				}
			}
			if (questDefinition.SurfaceType == global::Kampai.Game.QuestSurfaceType.Character || (questDefinition.SurfaceType == global::Kampai.Game.QuestSurfaceType.Automatic && questDefinition.SurfaceID > 0))
			{
				global::Kampai.Game.Prestige prestige = characterService.GetPrestige(questDefinition.SurfaceID);
				if (prestige == null || (prestige.state != global::Kampai.Game.PrestigeState.Questing && prestige.state != global::Kampai.Game.PrestigeState.TaskableWhileQuesting))
				{
					return false;
				}
				if (questLine.GivenByCharacterID != 0)
				{
					global::Kampai.Game.Prestige prestige2 = characterService.GetPrestige(questLine.GivenByCharacterID);
					if (prestige2 == null || (prestige2.state != global::Kampai.Game.PrestigeState.Questing && prestige2.state != global::Kampai.Game.PrestigeState.TaskableWhileQuesting) || prestige2.CurrentPrestigeLevel < questLine.GivenByCharacterPrestigeLevel)
					{
						return false;
					}
					if (prestige2.state == global::Kampai.Game.PrestigeState.Questing)
					{
						bool flag = true;
						if (prestige2.Definition.Type == global::Kampai.Game.PrestigeType.Villain || (prestige2.CurrentPrestigeLevel > 0 && (prestige2.Definition.ID == 40003 || prestige2.Definition.ID == 40004)))
						{
							flag = false;
						}
						global::Kampai.Game.View.BuildingManagerView component = buildingManager.GetComponent<global::Kampai.Game.View.BuildingManagerView>();
						global::Kampai.Game.View.TikiBarBuildingObjectView tikiBarBuildingObjectView = component.GetBuildingObject(313) as global::Kampai.Game.View.TikiBarBuildingObjectView;
						if (tikiBarBuildingObjectView != null && !tikiBarBuildingObjectView.ContainsCharacter(prestige2.trackedInstanceId) && flag)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		private bool QuestDeleteValidation(int targetQuestDeleteDefID)
		{
			if (questUnlockTree.ContainsKey(targetQuestDeleteDefID))
			{
				foreach (int item in questUnlockTree[targetQuestDeleteDefID])
				{
					if (!questMap.ContainsKey(item) && !IsQuestDeleted(targetQuestDeleteDefID))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		private bool IsQuestDeleted(int questDefID)
		{
			if (questUnlockTree.ContainsKey(questDefID))
			{
				foreach (int item in questUnlockTree[questDefID])
				{
					if (!questMap.ContainsKey(item) && !IsQuestDeleted(item))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		private bool QuestValidation(global::Kampai.Game.Quest quest)
		{
			if (quest.GetActiveDefinition().SurfaceType == global::Kampai.Game.QuestSurfaceType.LimitedEvent)
			{
				global::Kampai.Game.LimitedQuestDefinition limitedQuestDefinition = quest.GetActiveDefinition() as global::Kampai.Game.LimitedQuestDefinition;
				if (limitedQuestDefinition == null)
				{
					return false;
				}
				if (limitedQuestDefinition.ServerStartTimeUTC > timeService.GameTimeSeconds() || limitedQuestDefinition.ServerStopTimeUTC < timeService.GameTimeSeconds())
				{
					return false;
				}
			}
			return true;
		}

		private void ProcessAutomaticQuest(global::Kampai.Game.Quest quest)
		{
			global::Kampai.Game.QuestDefinition activeDefinition = quest.GetActiveDefinition();
			if (!questService.HasScript(quest, true))
			{
				if (activeDefinition.QuestSteps == null || activeDefinition.QuestSteps.Count == 0)
				{
					if (!questService.HasScript(quest, false))
					{
						quest.state = global::Kampai.Game.QuestState.Complete;
					}
					else
					{
						questService.GoToQuestState(quest, global::Kampai.Game.QuestState.RunningCompleteScript);
					}
				}
				else
				{
					questService.GoToQuestState(quest, global::Kampai.Game.QuestState.RunningTasks);
				}
			}
			else
			{
				questService.GoToQuestState(quest, global::Kampai.Game.QuestState.RunningStartScript);
			}
		}

		private bool NeedQuestTracking(global::Kampai.Game.Quest quest)
		{
			if (quest.Steps == null || quest.Steps.Count == 0)
			{
				if (quest.GetActiveDefinition().SurfaceID > 0 && quest.GetActiveDefinition().SurfaceType == global::Kampai.Game.QuestSurfaceType.Automatic)
				{
					return true;
				}
				return false;
			}
			return true;
		}

		private void SetupQuestTracking(global::Kampai.Game.Quest quest)
		{
			AssignBuildingTrackIdToAllQuestStep(quest);
			switch (quest.GetActiveDefinition().SurfaceType)
			{
			case global::Kampai.Game.QuestSurfaceType.Building:
				if (!AssignQuestIconTrackedBuildingInstanceID(quest))
				{
					logger.Log(global::Kampai.Util.Logger.Level.Error, "Quest tracking instance is not set! This quest: {0} won't have any icons in the game world", quest.GetActiveDefinition().ID.ToString());
				}
				break;
			case global::Kampai.Game.QuestSurfaceType.Character:
				if (!AssignQuestIconTrackedCharacterInstanceID(quest))
				{
					logger.Log(global::Kampai.Util.Logger.Level.Error, "Quest tracking instance is not set! This quest: {0} won't have any icons in the game world", quest.GetActiveDefinition().ID.ToString());
				}
				break;
			case global::Kampai.Game.QuestSurfaceType.Automatic:
			case global::Kampai.Game.QuestSurfaceType.LimitedEvent:
			case global::Kampai.Game.QuestSurfaceType.TimedEvent:
			case global::Kampai.Game.QuestSurfaceType.Bridge:
				AssignQuestIconTrackedInstanceID(quest);
				break;
			case global::Kampai.Game.QuestSurfaceType.ProcedurallyGenerated:
				break;
			}
		}

		private void AssignQuestIconTrackedInstanceID(global::Kampai.Game.Quest quest)
		{
			if (!AssignQuestIconTrackedBuildingInstanceID(quest) && !AssignQuestIconTrackedCharacterInstanceID(quest))
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "Quest tracking instance is not set! This quest: {0} won't have any icons in the game world", quest.GetActiveDefinition().ID.ToString());
			}
		}

		private bool AssignQuestIconTrackedCharacterInstanceID(global::Kampai.Game.Quest quest)
		{
			global::Kampai.Game.Prestige prestige = characterService.GetPrestige(quest.GetActiveDefinition().SurfaceID);
			if (prestige != null)
			{
				quest.QuestIconTrackedInstanceId = prestige.trackedInstanceId;
				return true;
			}
			logger.Log(global::Kampai.Util.Logger.Level.Error, "Character doesn't exist for the quest surface id: {0}. This quest: {1} won't have any icons in the game world", quest.GetActiveDefinition().SurfaceID, quest.GetActiveDefinition().ID.ToString());
			return false;
		}

		private bool AssignQuestIconTrackedBuildingInstanceID(global::Kampai.Game.Quest quest)
		{
			global::System.Collections.Generic.ICollection<global::Kampai.Game.Building> byDefinitionId = playerService.GetByDefinitionId<global::Kampai.Game.Building>(quest.GetActiveDefinition().SurfaceID);
			foreach (global::Kampai.Game.Building item in byDefinitionId)
			{
				global::Kampai.Game.BuildingState state = item.State;
				if (state != global::Kampai.Game.BuildingState.Complete && state != global::Kampai.Game.BuildingState.Construction && state != global::Kampai.Game.BuildingState.Inventory)
				{
					quest.QuestIconTrackedInstanceId = item.ID;
					return true;
				}
			}
			return false;
		}

		private void AssignBuildingTrackIdToAllQuestStep(global::Kampai.Game.Quest quest)
		{
			for (int i = 0; i < quest.Steps.Count; i++)
			{
				AssignBuildingTrackIdToQuestStep(quest, i);
			}
		}

		private void AssignBuildingTrackIdToQuestStep(global::Kampai.Game.Quest quest, int i)
		{
			switch (quest.GetActiveDefinition().QuestSteps[i].Type)
			{
			case global::Kampai.Game.QuestStepType.Construction:
				AssignBuildingTrackIdToQuestStepConstruction(quest, i);
				break;
			case global::Kampai.Game.QuestStepType.MinionTask:
			case global::Kampai.Game.QuestStepType.Mignette:
				AssignBuildingTrackIdToQuestStepMinionTaskAndMignette(quest, i);
				break;
			case global::Kampai.Game.QuestStepType.Delivery:
			case global::Kampai.Game.QuestStepType.Harvest:
				AssignBuildingTrackIdToQuestStepDelivery(quest, i);
				break;
			case global::Kampai.Game.QuestStepType.CabanaRepair:
				AssignBuildingTrackIdToQuestStepCabanaRepair(quest, i);
				break;
			case global::Kampai.Game.QuestStepType.BridgeRepair:
				AssignBuildingTrackIdToQuestStepRepair(quest, i);
				break;
			case global::Kampai.Game.QuestStepType.StageRepair:
				AssignBuildingTrackIdToQuestStepStageRepair(quest, i);
				break;
			case global::Kampai.Game.QuestStepType.WelcomeHutRepair:
				AssignBuildingTrackIdToQuestStepWelcomeHutRepair(quest, i);
				break;
			case global::Kampai.Game.QuestStepType.FountainRepair:
				AssignBuildingTrackIdToQuestStepFountainRepair(quest, i);
				break;
			case global::Kampai.Game.QuestStepType.OrderBoard:
				break;
			}
		}

		private void AssignBuildingTrackIdToQuestStepWelcomeHutRepair(global::Kampai.Game.Quest quest, int i)
		{
			global::Kampai.Game.WelcomeHutBuilding firstInstanceByDefinitionId = playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.WelcomeHutBuilding>(quest.GetActiveDefinition().QuestSteps[i].ItemDefinitionID);
			if (firstInstanceByDefinitionId == null)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.PS_MISSING_WELCOME_HUT, "Welcome hut instance not found");
			}
			else
			{
				quest.Steps[i].TrackedID = firstInstanceByDefinitionId.ID;
			}
		}

		private void AssignBuildingTrackIdToQuestStepFountainRepair(global::Kampai.Game.Quest quest, int i)
		{
			global::Kampai.Game.FountainBuilding firstInstanceByDefinitionId = playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.FountainBuilding>(quest.GetActiveDefinition().QuestSteps[i].ItemDefinitionID);
			if (firstInstanceByDefinitionId == null)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.PS_MISSING_FOUNTAIN, "Fountain instance not found");
			}
			else
			{
				quest.Steps[i].TrackedID = firstInstanceByDefinitionId.ID;
			}
		}

		private void AssignBuildingTrackIdToQuestStepStageRepair(global::Kampai.Game.Quest quest, int i)
		{
			global::Kampai.Game.StageBuilding firstInstanceByDefinitionId = playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.StageBuilding>(quest.GetActiveDefinition().QuestSteps[i].ItemDefinitionID);
			if (firstInstanceByDefinitionId == null)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.PS_MISSING_STAGE, "Stage instance not found");
			}
			else
			{
				quest.Steps[i].TrackedID = firstInstanceByDefinitionId.ID;
			}
		}

		private void AssignBuildingTrackIdToQuestStepCabanaRepair(global::Kampai.Game.Quest quest, int i)
		{
			global::Kampai.Game.CabanaBuilding firstInstanceByDefinitionId = playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.CabanaBuilding>(quest.GetActiveDefinition().QuestSteps[i].ItemDefinitionID);
			if (firstInstanceByDefinitionId == null)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.PS_MISSING_CABANA, "Cabana instance not found");
			}
			else
			{
				quest.Steps[i].TrackedID = firstInstanceByDefinitionId.ID;
			}
		}

		private void AssignBuildingTrackIdToQuestStepRepair(global::Kampai.Game.Quest quest, int i)
		{
			global::Kampai.Game.Definition definition = definitionService.Get(quest.GetActiveDefinition().QuestSteps[i].ItemDefinitionID);
			if (definition == null)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.DS_NO_BRIDGE_DEF, "Bridge definition not found");
				return;
			}
			global::Kampai.Game.BridgeDefinition bridgeDefinition = definition as global::Kampai.Game.BridgeDefinition;
			if (bridgeDefinition != null)
			{
				global::Kampai.Game.Building building = environment.GetBuilding(bridgeDefinition.location.x, bridgeDefinition.location.y);
				if (building == null)
				{
					logger.Fatal(global::Kampai.Util.FatalCode.QS_BUILDING_MISSING, "Building not found in environment");
				}
				quest.Steps[i].TrackedID = building.ID;
			}
		}

		private void AssignBuildingTrackIdToQuestStepDelivery(global::Kampai.Game.Quest quest, int i)
		{
			quest.Steps[i].TrackedID = definitionService.GetBuildingDefintionIDFromItemDefintionID(quest.GetActiveDefinition().QuestSteps[i].ItemDefinitionID);
			if (quest.Steps[i].TrackedID == 0)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.DS_NO_ITEM_DEF, "Item definition id not found for Delivery Type quests");
			}
		}

		private void AssignBuildingTrackIdToQuestStepMinionTaskAndMignette(global::Kampai.Game.Quest quest, int i)
		{
			quest.Steps[i].TrackedID = quest.GetActiveDefinition().QuestSteps[i].ItemDefinitionID;
			if (quest.Steps[i].TrackedID == 0)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.DS_NO_ITEM_DEF, "Item definition id not found for {0} Type quests", quest.GetActiveDefinition().QuestSteps[i].Type);
			}
		}

		private void AssignBuildingTrackIdToQuestStepConstruction(global::Kampai.Game.Quest quest, int i)
		{
			int itemDefinitionID = quest.GetActiveDefinition().QuestSteps[i].ItemDefinitionID;
			global::Kampai.Game.Building firstInstanceByDefinitionId = playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.Building>(itemDefinitionID);
			if (firstInstanceByDefinitionId != null)
			{
				quest.Steps[i].TrackedID = firstInstanceByDefinitionId.ID;
			}
		}

		private global::System.Collections.IEnumerator WaitAFrame(global::Kampai.Game.Quest quest)
		{
			yield return null;
			if (NeedQuestTracking(quest))
			{
				SetupQuestTracking(quest);
			}
			UpdateQuestWorldIconsSignal.Dispatch(quest);
		}
	}
}
