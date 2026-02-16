namespace Kampai.Game
{
	public class PrestigeService : global::Kampai.Game.IPrestigeService
	{
		private global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Prestige> prestigeMap = new global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Prestige>();

		private global::System.Collections.Generic.Dictionary<global::Kampai.Game.PrestigeType, global::Kampai.Game.UpdatePrestigeSignal> updateTable = new global::System.Collections.Generic.Dictionary<global::Kampai.Game.PrestigeType, global::Kampai.Game.UpdatePrestigeSignal>();

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		public global::Kampai.Game.IOrderBoardService orderBoardService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject(global::Kampai.UI.View.UIElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable uiContext { get; set; }

		[Inject(global::Kampai.Game.GameElement.NAMED_CHARACTER_MANAGER)]
		public global::UnityEngine.GameObject namedCharacterManager { get; set; }

		[Inject(global::Kampai.Game.GameElement.MINION_MANAGER)]
		public global::UnityEngine.GameObject minionManager { get; set; }

		[Inject(global::Kampai.Game.GameElement.BUILDING_MANAGER)]
		public global::UnityEngine.GameObject buildingManager { get; set; }

		[Inject]
		public global::Kampai.Util.PathFinder pathFinder { get; set; }

		[Inject]
		public global::Kampai.Game.UpdateNamedCharacterPrestigeSignal updateNamedCharacterSignal { get; set; }

		[Inject]
		public global::Kampai.Game.UpdateVillainPrestigeSignal updateVillainSignal { get; set; }

		[PostConstruct]
		public void PostConstruct()
		{
			questService = gameContext.injectionBinder.GetInstance<global::Kampai.Game.IQuestService>();
			orderBoardService = gameContext.injectionBinder.GetInstance<global::Kampai.Game.IOrderBoardService>();
			updateTable[global::Kampai.Game.PrestigeType.Minion] = updateNamedCharacterSignal;
			updateTable[global::Kampai.Game.PrestigeType.Villain] = updateVillainSignal;
		}

		public void Initialize()
		{
			GeneratePrestigeMap();
		}

		public global::Kampai.Game.Prestige GetPrestige(int prestigeDefinitionId, bool logIfNonexistant = true)
		{
			if (prestigeMap.ContainsKey(prestigeDefinitionId))
			{
				return prestigeMap[prestigeDefinitionId];
			}
			if (logIfNonexistant)
			{
				logger.Log(global::Kampai.Util.Logger.Level.Warning, "Prestige doesn't exist for the prestige definition Id: {0}", prestigeDefinitionId);
			}
			return null;
		}

		public global::System.Collections.Generic.IList<global::Kampai.Game.Prestige> GetBuddyPrestiges()
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.Prestige> list = new global::System.Collections.Generic.List<global::Kampai.Game.Prestige>();
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::Kampai.Game.Prestige> item in prestigeMap)
			{
				global::Kampai.Game.Prestige value = item.Value;
				if (value.CurrentPrestigePoints > 0 && (value.state == global::Kampai.Game.PrestigeState.PreUnlocked || value.state == global::Kampai.Game.PrestigeState.Prestige))
				{
					list.Add(value);
				}
			}
			return list;
		}

		public global::System.Collections.Generic.Dictionary<int, bool> GetPrestigedCharacterStates()
		{
			global::System.Collections.Generic.Dictionary<int, bool> dictionary = new global::System.Collections.Generic.Dictionary<int, bool>();
			global::System.Collections.Generic.IList<global::Kampai.Game.PrestigeDefinition> all = definitionService.GetAll<global::Kampai.Game.PrestigeDefinition>();
			foreach (global::Kampai.Game.PrestigeDefinition item in all)
			{
				int iD = item.ID;
				if (iD != 40014 && iD != 40002)
				{
					global::Kampai.Game.Prestige firstInstanceByDefinitionId = playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.Prestige>(iD);
					if (firstInstanceByDefinitionId != null && (firstInstanceByDefinitionId.CurrentPrestigeLevel > 0 || firstInstanceByDefinitionId.state == global::Kampai.Game.PrestigeState.Questing || firstInstanceByDefinitionId.state == global::Kampai.Game.PrestigeState.Taskable || firstInstanceByDefinitionId.state == global::Kampai.Game.PrestigeState.Locked || firstInstanceByDefinitionId.state == global::Kampai.Game.PrestigeState.TaskableWhileQuesting))
					{
						dictionary.Add(iD, true);
					}
					else
					{
						dictionary.Add(iD, false);
					}
				}
			}
			return dictionary;
		}

		public void GetCharacterImageBasedOnMood(int prestigeDefinitionId, global::Kampai.Game.CharacterImageType type, out global::UnityEngine.Sprite characterImage, out global::UnityEngine.Sprite characterMask)
		{
			global::Kampai.Game.PrestigeDefinition prestigeDefinition = definitionService.Get<global::Kampai.Game.PrestigeDefinition>(prestigeDefinitionId);
			GetCharacterImageBasedOnMood(prestigeDefinition, type, out characterImage, out characterMask);
		}

		public void GetCharacterImageBasedOnMood(global::Kampai.Game.PrestigeDefinition prestigeDefinition, global::Kampai.Game.CharacterImageType type, out global::UnityEngine.Sprite characterImage, out global::UnityEngine.Sprite characterMask)
		{
			global::Kampai.Game.QuestResourceDefinition questResourceDefinition = null;
			characterImage = null;
			characterMask = null;
			questResourceDefinition = DetermineQuestResourceDefinition(prestigeDefinition, type);
			if (questResourceDefinition != null)
			{
				characterImage = UIUtils.LoadSpriteFromPath(questResourceDefinition.resourcePath);
				characterMask = UIUtils.LoadSpriteFromPath(questResourceDefinition.maskPath);
			}
		}

		public void GetCharacterImagePathBasedOnMood(int prestigeDefinitionId, global::Kampai.Game.CharacterImageType type, out string characterImagePath, out string characterMaskPath)
		{
			global::Kampai.Game.PrestigeDefinition prestigeDefinition = definitionService.Get<global::Kampai.Game.PrestigeDefinition>(prestigeDefinitionId);
			GetCharacterImagePathBasedOnMood(prestigeDefinition, type, out characterImagePath, out characterMaskPath);
		}

		public void GetCharacterImagePathBasedOnMood(global::Kampai.Game.PrestigeDefinition prestigeDefinition, global::Kampai.Game.CharacterImageType type, out string characterImagePath, out string characterMaskPath)
		{
			global::Kampai.Game.QuestResourceDefinition questResourceDefinition = null;
			characterImagePath = string.Empty;
			characterMaskPath = string.Empty;
			questResourceDefinition = DetermineQuestResourceDefinition(prestigeDefinition, type);
			if (questResourceDefinition != null)
			{
				characterImagePath = questResourceDefinition.resourcePath;
				characterMaskPath = questResourceDefinition.maskPath;
			}
		}

		public global::Kampai.Game.QuestResourceDefinition DetermineQuestResourceDefinition(int prestigeDefinitionId, global::Kampai.Game.CharacterImageType type)
		{
			global::Kampai.Game.PrestigeDefinition prestigeDefinition = definitionService.Get<global::Kampai.Game.PrestigeDefinition>(prestigeDefinitionId);
			return DetermineQuestResourceDefinition(prestigeDefinition, type);
		}

		private global::Kampai.Game.QuestResourceDefinition DetermineQuestResourceDefinition(global::Kampai.Game.PrestigeDefinition prestigeDefinition, global::Kampai.Game.CharacterImageType type)
		{
			global::Kampai.Game.QuestResourceDefinition result = null;
			switch (type)
			{
			case global::Kampai.Game.CharacterImageType.SmallAvatarIcon:
				result = definitionService.Get<global::Kampai.Game.QuestResourceDefinition>(prestigeDefinition.SmallAvatarResouceId);
				break;
			case global::Kampai.Game.CharacterImageType.WayfinderIcon:
				result = definitionService.Get<global::Kampai.Game.QuestResourceDefinition>(prestigeDefinition.WayFinderIconResourceId);
				break;
			case global::Kampai.Game.CharacterImageType.BigAvatarIcon:
				result = definitionService.Get<global::Kampai.Game.QuestResourceDefinition>(prestigeDefinition.BigAvatarResourceId);
				break;
			}
			return result;
		}

		public void AddMinionToTikiBarSlot(global::Kampai.Game.Character targetMinion, int slotIndex, global::Kampai.Game.TikiBarBuilding tikiBar, bool enablePathing = false)
		{
			global::Kampai.Game.View.CharacterObject characterObject = null;
			int iD = targetMinion.ID;
			global::Kampai.Game.Minion minion = targetMinion as global::Kampai.Game.Minion;
			if (minion != null)
			{
				global::Kampai.Game.View.MinionManagerView component = minionManager.GetComponent<global::Kampai.Game.View.MinionManagerView>();
				characterObject = component.Get(iD);
			}
			else if (targetMinion is global::Kampai.Game.NamedCharacter)
			{
				global::Kampai.Game.View.NamedCharacterManagerView component2 = namedCharacterManager.GetComponent<global::Kampai.Game.View.NamedCharacterManagerView>();
				characterObject = component2.Get(iD);
			}
			if (characterObject == null)
			{
				logger.Error("AddMinionToTikiBarSlot: ao as MinionObject and NamedCharacterObject == null");
				return;
			}
			global::Kampai.Game.View.BuildingManagerView component3 = buildingManager.GetComponent<global::Kampai.Game.View.BuildingManagerView>();
			global::Kampai.Game.View.BuildingObject buildingObject = component3.GetBuildingObject(tikiBar.ID);
			global::Kampai.Game.View.TikiBarBuildingObjectView tikiBarBuildingObjectView = buildingObject as global::Kampai.Game.View.TikiBarBuildingObjectView;
			global::UnityEngine.Vector3 position = characterObject.transform.position;
			global::UnityEngine.Vector3 routePosition = tikiBarBuildingObjectView.GetRoutePosition(slotIndex, tikiBar, position);
			global::UnityEngine.Vector3 routeRotation = tikiBarBuildingObjectView.GetRouteRotation(slotIndex);
			if (enablePathing)
			{
				global::System.Collections.Generic.IList<global::UnityEngine.Vector3> list = pathFinder.FindPath(position, routePosition, 4, true);
				if (list == null)
				{
					global::System.Collections.Generic.List<global::UnityEngine.Vector3> list2 = new global::System.Collections.Generic.List<global::UnityEngine.Vector3>();
					list2.Add(routePosition);
					list = list2;
				}
				global::Kampai.Game.View.RouteInstructions type = new global::Kampai.Game.View.RouteInstructions
				{
					MinionId = iD,
					Path = list,
					Rotation = routeRotation.y,
					TargetBuilding = tikiBar
				};
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.PathCharacterToTikiBarSignal>().Dispatch(characterObject, type, slotIndex);
			}
			else
			{
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.TeleportCharacterToTikiBarSignal>().Dispatch(characterObject, slotIndex);
			}
			global::Kampai.Game.Prestige prestigeFromMinionInstance = GetPrestigeFromMinionInstance(targetMinion);
			if (prestigeFromMinionInstance == null)
			{
				logger.Error("AddMinionToTikiBarSlot: Prestige == null for minion ID: {0}", iD);
				return;
			}
			ChangeToPrestigeState(prestigeFromMinionInstance, global::Kampai.Game.PrestigeState.Questing);
			if (minion != null)
			{
				minion.PrestigeCharacter = prestigeFromMinionInstance;
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.MinionStateChangeSignal>().Dispatch(iD, global::Kampai.Game.MinionState.Questing);
			}
		}

		public global::Kampai.Game.Prestige GetPrestigeFromMinionInstance(global::Kampai.Game.Character minionCharacter)
		{
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::Kampai.Game.Prestige> item in prestigeMap)
			{
				global::Kampai.Game.Prestige value = item.Value;
				if (value.trackedInstanceId == minionCharacter.ID)
				{
					return value;
				}
			}
			logger.Log(global::Kampai.Util.Logger.Level.Warning, "Prestige doesn't exist for the character: {0}", minionCharacter.ID);
			return null;
		}

		public bool IsTaskableWhileQuesting(global::Kampai.Game.Prestige prestige)
		{
			int iD = prestige.Definition.ID;
			if (prestige.CurrentPrestigeLevel >= 1 && prestige.state == global::Kampai.Game.PrestigeState.Questing && prestige.Definition.Type == global::Kampai.Game.PrestigeType.Minion && iD != 40004 && iD != 40003 && iD != 40000 && iD != 40014)
			{
				return true;
			}
			return false;
		}

		public global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Prestige> GetAllUnlockedPrestiges()
		{
			return prestigeMap;
		}

		public void AddPrestige(global::Kampai.Game.Prestige prestige)
		{
			if (!prestigeMap.ContainsKey(prestige.Definition.ID))
			{
				prestigeMap.Add(prestige.Definition.ID, prestige);
				playerService.Add(prestige);
			}
		}

		public void RemovePrestige(global::Kampai.Game.Prestige prestige)
		{
			if (prestigeMap.ContainsKey(prestige.Definition.ID))
			{
				prestigeMap.Remove(prestige.Definition.ID);
				playerService.Remove(prestige);
			}
		}

		public void ChangeToPrestigeState(global::Kampai.Game.Prestige prestige, global::Kampai.Game.PrestigeState targetState, int targetPrestigeLevel = 0, bool triggerNewQuest = true)
		{
			int iD = prestige.Definition.ID;
			if (prestige.state == targetState)
			{
				if (targetState == global::Kampai.Game.PrestigeState.Questing && triggerNewQuest)
				{
					gameContext.injectionBinder.GetInstance<global::Kampai.Game.GetNewQuestSignal>().Dispatch();
				}
				return;
			}
			global::Kampai.Game.PrestigeState state = prestige.state;
			prestige.state = targetState;
			switch (targetState)
			{
			case global::Kampai.Game.PrestigeState.PreUnlocked:
			case global::Kampai.Game.PrestigeState.Prestige:
				if (targetPrestigeLevel <= 0 || prestige.CurrentPrestigeLevel + 1 == targetPrestigeLevel)
				{
					prestige.CurrentPrestigeLevel = targetPrestigeLevel;
				}
				else if (targetPrestigeLevel >= 1 && prestige.CurrentPrestigeLevel < 0)
				{
					prestige.CurrentPrestigeLevel = 0;
				}
				else
				{
					prestige.CurrentPrestigeLevel++;
				}
				break;
			case global::Kampai.Game.PrestigeState.InQueue:
				prestige.CurrentPrestigePoints = 0;
				orderBoardService.ReplaceCharacterTickets(iD);
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.GetNewQuestSignal>().Dispatch();
				break;
			case global::Kampai.Game.PrestigeState.Questing:
				if (iD == 40000)
				{
					return;
				}
				if (prestige.UTCTimeUnlocked == 0)
				{
					prestige.UTCTimeUnlocked = timeService.GameTimeSeconds();
				}
				prestige.CurrentPrestigePoints = 0;
				if (triggerNewQuest)
				{
					gameContext.injectionBinder.GetInstance<global::Kampai.Game.GetNewQuestSignal>().Dispatch();
				}
				break;
			}
			global::Kampai.Game.UpdatePrestigeSignal value;
			if (!updateTable.TryGetValue(prestige.Definition.Type, out value))
			{
				logger.Error("PrestigeService doesn't know how to update a presetige with type {0}!", prestige.Definition.Type);
			}
			else
			{
				global::Kampai.Util.Tuple<global::Kampai.Game.PrestigeState, global::Kampai.Game.PrestigeState> type = new global::Kampai.Util.Tuple<global::Kampai.Game.PrestigeState, global::Kampai.Game.PrestigeState>(state, targetState);
				value.Dispatch(prestige, type);
			}
		}

		public void UpdateEligiblePrestigeList()
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.PrestigeDefinition> all = definitionService.GetAll<global::Kampai.Game.PrestigeDefinition>();
			foreach (global::Kampai.Game.PrestigeDefinition item in all)
			{
				int iD = item.ID;
				if (iD == 40000 || iD == 40014)
				{
					continue;
				}
				if (prestigeMap.ContainsKey(iD))
				{
					global::Kampai.Game.Prestige prestige = prestigeMap[iD];
					if (prestige.state != global::Kampai.Game.PrestigeState.Locked && prestige.state != global::Kampai.Game.PrestigeState.Taskable && prestige.state != global::Kampai.Game.PrestigeState.PreUnlocked)
					{
						continue;
					}
					int prestigeUnlockedPrestigeLevel = GetPrestigeUnlockedPrestigeLevel(item);
					if (prestige.CurrentPrestigeLevel < prestigeUnlockedPrestigeLevel)
					{
						if (prestige.CurrentPrestigeLevel > -1)
						{
							prestige.CurrentPrestigePoints = 0;
						}
						ChangeToPrestigeState(prestige, global::Kampai.Game.PrestigeState.Prestige, prestigeUnlockedPrestigeLevel);
						orderBoardService.AddPriorityPrestigeCharacter(iD);
					}
					continue;
				}
				int prestigeUnlockedPrestigeLevel2 = GetPrestigeUnlockedPrestigeLevel(item);
				if (prestigeUnlockedPrestigeLevel2 == -1)
				{
					global::Kampai.Game.Prestige prestige2 = new global::Kampai.Game.Prestige(item);
					ChangeToPrestigeState(prestige2, global::Kampai.Game.PrestigeState.PreUnlocked, prestigeUnlockedPrestigeLevel2);
					AddPrestige(prestige2);
					orderBoardService.AddPriorityPrestigeCharacter(iD);
				}
				else if (prestigeUnlockedPrestigeLevel2 >= 0)
				{
					global::Kampai.Game.Prestige prestige3 = new global::Kampai.Game.Prestige(item);
					ChangeToPrestigeState(prestige3, global::Kampai.Game.PrestigeState.Prestige, prestigeUnlockedPrestigeLevel2);
					AddPrestige(prestige3);
					orderBoardService.AddPriorityPrestigeCharacter(iD);
				}
			}
		}

		public void PostOrderCompletion(global::Kampai.Game.Prestige prestige)
		{
			if (prestige.CurrentPrestigeLevel >= 0 && prestige.CurrentPrestigePoints >= prestige.Definition.PrestigeLevelSettings[prestige.CurrentPrestigeLevel].PointsNeeded)
			{
				ChangeToPrestigeState(prestige, global::Kampai.Game.PrestigeState.InQueue);
			}
		}

		public int GetPrestigeUnlockedPrestigeLevel(global::Kampai.Game.PrestigeDefinition prestigeDefinition)
		{
			uint quantity = playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID);
			int result = -2;
			if (quantity < prestigeDefinition.PreUnlockLevel)
			{
				return result;
			}
			if (questService.IsQuestCompleted(prestigeDefinition.PrestigeLevelSettings[0].UnlockQuestID))
			{
				result = -1;
				for (int i = 0; i < prestigeDefinition.PrestigeLevelSettings.Count; i++)
				{
					if (prestigeDefinition.PrestigeLevelSettings[i].UnlockLevel <= quantity && questService.IsQuestCompleted(prestigeDefinition.PrestigeLevelSettings[i].UnlockQuestID))
					{
						result = i;
					}
				}
				return result;
			}
			return result;
		}

		public int ResolveTrackedId(int questTrackedInstanceId)
		{
			global::Kampai.Game.Character byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Character>(questTrackedInstanceId);
			if ((byInstanceId is global::Kampai.Game.NamedCharacter && byInstanceId.Definition.ID == 70002) || byInstanceId is global::Kampai.Game.Minion)
			{
				global::Kampai.Game.Prestige prestigeFromMinionInstance = GetPrestigeFromMinionInstance(byInstanceId);
				if (prestigeFromMinionInstance.state == global::Kampai.Game.PrestigeState.TaskableWhileQuesting)
				{
					return 78;
				}
			}
			global::Kampai.Game.Villain villain = byInstanceId as global::Kampai.Game.Villain;
			if (villain == null || villain.CabanaBuildingId < 0)
			{
				return questTrackedInstanceId;
			}
			return villain.CabanaBuildingId;
		}

		public bool IsTikiBarFull()
		{
			global::Kampai.Game.TikiBarBuilding byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.TikiBarBuilding>(313);
			if (byInstanceId.GetOpenSlot() == -1)
			{
				return true;
			}
			return false;
		}

		public global::Kampai.Game.CabanaBuilding GetEmptyCabana()
		{
			foreach (global::Kampai.Game.Instance item in playerService.GetInstancesByDefinition<global::Kampai.Game.CabanaBuildingDefinition>())
			{
				global::Kampai.Game.CabanaBuilding cabanaBuilding = item as global::Kampai.Game.CabanaBuilding;
				if (cabanaBuilding != null && !cabanaBuilding.Occupied)
				{
					return cabanaBuilding;
				}
			}
			return null;
		}

		private void GeneratePrestigeMap()
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.Prestige> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.Prestige>();
			foreach (global::Kampai.Game.Prestige item in instancesByType)
			{
				prestigeMap.Add(item.Definition.ID, item);
			}
			if (!prestigeMap.ContainsKey(40000))
			{
				global::Kampai.Game.PrestigeDefinition def = definitionService.Get<global::Kampai.Game.PrestigeDefinition>(40000);
				global::Kampai.Game.Prestige prestige = new global::Kampai.Game.Prestige(def);
				prestige.state = global::Kampai.Game.PrestigeState.Taskable;
				prestige.trackedInstanceId = 78;
				AddPrestige(prestige);
			}
			if (!prestigeMap.ContainsKey(40014))
			{
				global::Kampai.Game.PrestigeDefinition def2 = definitionService.Get<global::Kampai.Game.PrestigeDefinition>(40014);
				global::Kampai.Game.Prestige prestige2 = new global::Kampai.Game.Prestige(def2);
				prestige2.state = global::Kampai.Game.PrestigeState.Taskable;
				prestige2.trackedInstanceId = 301;
				AddPrestige(prestige2);
			}
		}
	}
}
