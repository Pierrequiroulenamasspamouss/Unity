namespace Kampai.UI.View
{
	public class WayFinderPanelView : global::strange.extensions.mediation.impl.View
	{
		private delegate bool PriorityFunction(global::Kampai.UI.View.IWayFinderView wayFinderView);

		private global::System.Collections.Generic.Dictionary<int, global::Kampai.UI.View.IWayFinderView> trackedWayFinders;

		private global::System.Collections.Generic.Dictionary<int, global::Kampai.UI.View.IParentWayFinderView> trackedParentWayFinders;

		private global::Kampai.Util.ILogger logger;

		private global::Kampai.Game.ITikiBarService tikiBarService;

		private global::Kampai.Game.IPlayerService playerService;

		private global::Kampai.Game.IPrestigeService prestigeService;

		private global::Kampai.UI.View.WayFinderSettings tikiBarParentWayFinderSettings;

		private global::Kampai.UI.View.WayFinderSettings cabanaParentWayFinderSettings;

		private global::Kampai.UI.View.WayFinderSettings orderBoardWayFinderSettings;

		private global::Kampai.UI.View.WayFinderSettings storageBuildingWayFinderSettings;

		private int tsmWayFinderTrackedId;

		private bool limitTikiBarWayFinders;

		private bool isForceHideEnabled;

		private global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::Kampai.UI.View.WayFinderPanelView.PriorityFunction>> allPriorityFunctions;

		internal void Init(global::Kampai.Util.ILogger logger, global::Kampai.Game.ITikiBarService tikiBarService, global::Kampai.Game.IPlayerService playerService, global::Kampai.Game.IPrestigeService prestigeService, global::Kampai.UI.IPositionService positionService)
		{
			logger.Info("Init way finders...");
			this.logger = logger;
			this.tikiBarService = tikiBarService;
			this.playerService = playerService;
			this.prestigeService = prestigeService;
			tikiBarParentWayFinderSettings = new global::Kampai.UI.View.WayFinderSettings(313);
			cabanaParentWayFinderSettings = new global::Kampai.UI.View.WayFinderSettings(1000008087);
			orderBoardWayFinderSettings = new global::Kampai.UI.View.WayFinderSettings(309);
			storageBuildingWayFinderSettings = new global::Kampai.UI.View.WayFinderSettings(314);
			tsmWayFinderTrackedId = 301;
			trackedWayFinders = new global::System.Collections.Generic.Dictionary<int, global::Kampai.UI.View.IWayFinderView>();
			trackedParentWayFinders = new global::System.Collections.Generic.Dictionary<int, global::Kampai.UI.View.IParentWayFinderView>();
			allPriorityFunctions = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::Kampai.UI.View.WayFinderPanelView.PriorityFunction>>();
			allPriorityFunctions.Add(new global::System.Collections.Generic.List<global::Kampai.UI.View.WayFinderPanelView.PriorityFunction> { PrioritizeQuestComplete, PrioritizeAtTikiBar });
			allPriorityFunctions.Add(new global::System.Collections.Generic.List<global::Kampai.UI.View.WayFinderPanelView.PriorityFunction> { PrioritizeQuestComplete, PrioritizeAtCabana });
			allPriorityFunctions.Add(new global::System.Collections.Generic.List<global::Kampai.UI.View.WayFinderPanelView.PriorityFunction> { PrioritizeNewQuest, PrioritizeAtTikiBar });
			allPriorityFunctions.Add(new global::System.Collections.Generic.List<global::Kampai.UI.View.WayFinderPanelView.PriorityFunction> { PrioritizeNewQuest, PrioritizeAtCabana });
			allPriorityFunctions.Add(new global::System.Collections.Generic.List<global::Kampai.UI.View.WayFinderPanelView.PriorityFunction> { PrioritizeTaskComplete, PrioritizeAtTikiBar });
			allPriorityFunctions.Add(new global::System.Collections.Generic.List<global::Kampai.UI.View.WayFinderPanelView.PriorityFunction> { PrioritizeTaskComplete, PrioritizeAtCabana });
			allPriorityFunctions.Add(new global::System.Collections.Generic.List<global::Kampai.UI.View.WayFinderPanelView.PriorityFunction> { PrioritizeBob });
			allPriorityFunctions.Add(new global::System.Collections.Generic.List<global::Kampai.UI.View.WayFinderPanelView.PriorityFunction> { PrioritizeQuestAvailable, PrioritizeAtTikiBar });
			allPriorityFunctions.Add(new global::System.Collections.Generic.List<global::Kampai.UI.View.WayFinderPanelView.PriorityFunction> { PrioritizeQuestAvailable, PrioritizeAtCabana });
			positionService.HUDElementsToAvoid = GenerateListOfObjectsToSnapAround();
		}

		private global::System.Collections.Generic.List<global::UnityEngine.GameObject> GenerateListOfObjectsToSnapAround()
		{
			global::System.Collections.Generic.List<global::UnityEngine.GameObject> list = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();
			list.Add(global::UnityEngine.GameObject.Find("btn_OpenStore"));
			list.Add(global::UnityEngine.GameObject.Find("btn_Settings"));
			list.Add(global::UnityEngine.GameObject.Find("group_Storage"));
			list.Add(global::UnityEngine.GameObject.Find("group_Currency_Grind"));
			return list;
		}

		private bool PrioritizeBob(global::Kampai.UI.View.IWayFinderView wayFinderView)
		{
			return IsBobPointsAtStuffWayFinder(wayFinderView.Prestige);
		}

		private bool PrioritizeAtCabana(global::Kampai.UI.View.IWayFinderView wayFinderView)
		{
			return IsCabanaChildWayFinder(wayFinderView.Prestige);
		}

		private bool PrioritizeAtTikiBar(global::Kampai.UI.View.IWayFinderView wayFinderView)
		{
			return IsTikiBarChildWayFinder(wayFinderView.Prestige);
		}

		private bool PrioritizeQuestComplete(global::Kampai.UI.View.IWayFinderView wayFinderView)
		{
			global::Kampai.UI.View.AbstractQuestWayFinderView abstractQuestWayFinderView = wayFinderView as global::Kampai.UI.View.AbstractQuestWayFinderView;
			if (abstractQuestWayFinderView != null)
			{
				return abstractQuestWayFinderView.IsQuestComplete();
			}
			return false;
		}

		private bool PrioritizeQuestAvailable(global::Kampai.UI.View.IWayFinderView wayFinderView)
		{
			global::Kampai.UI.View.AbstractQuestWayFinderView abstractQuestWayFinderView = wayFinderView as global::Kampai.UI.View.AbstractQuestWayFinderView;
			if (abstractQuestWayFinderView != null)
			{
				return abstractQuestWayFinderView.IsQuestAvailable();
			}
			return false;
		}

		private bool PrioritizeNewQuest(global::Kampai.UI.View.IWayFinderView wayFinderView)
		{
			global::Kampai.UI.View.AbstractQuestWayFinderView abstractQuestWayFinderView = wayFinderView as global::Kampai.UI.View.AbstractQuestWayFinderView;
			if (abstractQuestWayFinderView != null)
			{
				return abstractQuestWayFinderView.IsNewQuestAvailable();
			}
			return false;
		}

		private bool PrioritizeTaskComplete(global::Kampai.UI.View.IWayFinderView wayFinderView)
		{
			global::Kampai.UI.View.AbstractQuestWayFinderView abstractQuestWayFinderView = wayFinderView as global::Kampai.UI.View.AbstractQuestWayFinderView;
			if (abstractQuestWayFinderView != null)
			{
				return abstractQuestWayFinderView.IsTaskReady();
			}
			return false;
		}

		internal void Cleanup()
		{
			logger.Info("Cleaning up way finders...");
			if (trackedWayFinders != null)
			{
				trackedWayFinders.Clear();
			}
			if (trackedParentWayFinders != null)
			{
				trackedParentWayFinders.Clear();
			}
			if (allPriorityFunctions != null)
			{
				allPriorityFunctions.Clear();
			}
		}

		private bool IsTikiBarParentWayFinder(int trackedId)
		{
			return trackedId == tikiBarParentWayFinderSettings.TrackedId;
		}

		private bool IsTikiBarChildWayFinder(global::Kampai.Game.Prestige prestige)
		{
			return prestige != null && tikiBarService.IsCharacterSitting(prestige);
		}

		private bool IsOrderBoardWayFinder(int trackedId)
		{
			return trackedId == orderBoardWayFinderSettings.TrackedId;
		}

		private bool IsCabanaParentWayFinder(int trackedId)
		{
			return trackedId == cabanaParentWayFinderSettings.TrackedId;
		}

		private bool IsStorageBuildingWayFinder(int trackedId)
		{
			return trackedId == storageBuildingWayFinderSettings.TrackedId;
		}

		private bool IsTSMWayFinder(int trackedId)
		{
			return trackedId == tsmWayFinderTrackedId;
		}

		private bool IsCabanaChildWayFinder(global::Kampai.Game.Prestige prestige)
		{
			if (prestige != null)
			{
				global::Kampai.Game.PrestigeDefinition definition = prestige.Definition;
				return definition.Type == global::Kampai.Game.PrestigeType.Villain || (definition.ID == 40004 && prestige.CurrentPrestigeLevel > 0);
			}
			return false;
		}

		private bool IsBobPointsAtStuffWayFinder(global::Kampai.Game.Prestige prestige)
		{
			if (prestige != null)
			{
				return prestige.Definition.ID == 40002;
			}
			return false;
		}

		internal global::Kampai.UI.View.IWayFinderView CreateWayFinder(global::Kampai.UI.View.WayFinderSettings settings, bool updatePriority = true)
		{
			int trackedId = settings.TrackedId;
			bool flag = settings.QuestDefId > 0;
			if (flag)
			{
				logger.Info("Creating way finder with tracking id: {0} and Quest Def id: {1}", trackedId, settings.QuestDefId);
			}
			else
			{
				logger.Info("Creating way finder with tracking id: {0}", trackedId);
			}
			global::Kampai.UI.View.IWayFinderView wayFinderView = null;
			if ((wayFinderView = GetWayFinder(trackedId)) != null)
			{
				logger.Warning("Way finder with id: {0} already exists, will not create another one", trackedId);
				return wayFinderView;
			}
			global::Kampai.Game.Prestige prestigeForWayFinder = GetPrestigeForWayFinder(trackedId);
			bool flag2 = IsCabanaChildWayFinder(prestigeForWayFinder);
			bool flag3 = IsTikiBarChildWayFinder(prestigeForWayFinder);
			global::Kampai.UI.View.IParentWayFinderView parentWayFinderView = null;
			if (flag2)
			{
				parentWayFinderView = CreateWayFinder(cabanaParentWayFinderSettings) as global::Kampai.UI.View.IParentWayFinderView;
			}
			else if (flag3)
			{
				parentWayFinderView = CreateWayFinder(tikiBarParentWayFinderSettings) as global::Kampai.UI.View.IParentWayFinderView;
			}
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(global::Kampai.Util.KampaiResources.Load("cmp_WayFinder")) as global::UnityEngine.GameObject;
			gameObject.transform.SetParent(base.transform, false);
			gameObject.SetActive(true);
			global::Kampai.UI.View.WayFinderModal component = gameObject.GetComponent<global::Kampai.UI.View.WayFinderModal>();
			component.Settings = settings;
			component.Prestige = prestigeForWayFinder;
			if (IsCabanaParentWayFinder(trackedId))
			{
				logger.Info("Way finder with id:{0} is cabana parent way finder", trackedId);
				wayFinderView = gameObject.AddComponent<global::Kampai.UI.View.CabanaParentWayFinderView>();
				trackedParentWayFinders.Add(trackedId, wayFinderView as global::Kampai.UI.View.CabanaParentWayFinderView);
			}
			else if (flag2)
			{
				logger.Info("Way finder with tracking id: {0} is a child of tikibar", trackedId);
				wayFinderView = gameObject.AddComponent<global::Kampai.UI.View.CabanaChildWayFinderView>();
				global::Kampai.UI.View.CabanaChildWayFinderView childWayFinderView = wayFinderView as global::Kampai.UI.View.CabanaChildWayFinderView;
				parentWayFinderView.AddChildWayFinder(childWayFinderView);
			}
			else if (IsTikiBarParentWayFinder(trackedId))
			{
				logger.Info("Way finder with id:{0} is tikibar parent way finder", trackedId);
				wayFinderView = gameObject.AddComponent<global::Kampai.UI.View.TikiBarParentWayFinderView>();
				trackedParentWayFinders.Add(trackedId, wayFinderView as global::Kampai.UI.View.TikiBarParentWayFinderView);
			}
			else if (flag3)
			{
				logger.Info("Way finder with tracking id: {0} is a child of tikibar", trackedId);
				wayFinderView = gameObject.AddComponent<global::Kampai.UI.View.TikiBarChildWayFinderView>();
				global::Kampai.UI.View.TikiBarChildWayFinderView childWayFinderView2 = wayFinderView as global::Kampai.UI.View.TikiBarChildWayFinderView;
				parentWayFinderView.AddChildWayFinder(childWayFinderView2);
			}
			else if (!IsOrderBoardWayFinder(trackedId))
			{
				wayFinderView = (flag ? ((!IsTSMWayFinder(trackedId)) ? ((global::Kampai.UI.View.AbstractQuestWayFinderView)gameObject.AddComponent<global::Kampai.UI.View.QuestWayFinderView>()) : ((global::Kampai.UI.View.AbstractQuestWayFinderView)gameObject.AddComponent<global::Kampai.UI.View.TSMWayFinderView>())) : (IsBobPointsAtStuffWayFinder(prestigeForWayFinder) ? gameObject.AddComponent<global::Kampai.UI.View.BobPointsAtStuffWayFinderView>() : ((!IsStorageBuildingWayFinder(trackedId)) ? ((global::Kampai.UI.View.AbstractWayFinderView)gameObject.AddComponent<global::Kampai.UI.View.WayFinderView>()) : ((global::Kampai.UI.View.AbstractWayFinderView)gameObject.AddComponent<global::Kampai.UI.View.StorageBuildingWayFinderView>()))));
			}
			else
			{
				logger.Info("Way finder with tracking id: {0} is on orderboard", trackedId);
				wayFinderView = gameObject.AddComponent<global::Kampai.UI.View.OrderBoardWayFinderView>();
			}
			trackedWayFinders[trackedId] = wayFinderView;
			PostCreateWayFinder(wayFinderView, updatePriority);
			return wayFinderView;
		}

		private void PostCreateWayFinder(global::Kampai.UI.View.IWayFinderView wayFinderView, bool updatePriority)
		{
			if (playerService.GetHighestFtueCompleted() < 7 && trackedWayFinders.Count > 2)
			{
				limitTikiBarWayFinders = true;
			}
			wayFinderView.SetForceHide(isForceHideEnabled);
			if (updatePriority)
			{
				UpdateWayFinderPriority();
			}
		}

		private global::Kampai.Game.Prestige GetPrestigeForWayFinder(int trackedId)
		{
			global::Kampai.Game.Character byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Character>(trackedId);
			if (byInstanceId != null)
			{
				return tikiBarService.GetPrestigeForSeatableCharacter(byInstanceId);
			}
			global::Kampai.Game.CabanaBuilding byInstanceId2 = playerService.GetByInstanceId<global::Kampai.Game.CabanaBuilding>(trackedId);
			if (byInstanceId2 != null)
			{
				global::System.Collections.Generic.List<global::Kampai.Game.Villain> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.Villain>();
				foreach (global::Kampai.Game.Villain item in instancesByType)
				{
					if (item.CabanaBuildingId == byInstanceId2.ID)
					{
						return prestigeService.GetPrestigeFromMinionInstance(item);
					}
				}
			}
			return null;
		}

		internal void RemoveWayFinder(int trackedId, bool updatePriority = true)
		{
			logger.Info("Removing way finder with tracking id: {0}", trackedId);
			global::Kampai.UI.View.IWayFinderView wayFinderView = null;
			if ((wayFinderView = GetWayFinder(trackedId)) != null)
			{
				trackedWayFinders.Remove(trackedId);
				global::Kampai.UI.View.IChildWayFinderView childWayFinderView = wayFinderView as global::Kampai.UI.View.IChildWayFinderView;
				if (childWayFinderView != null && childWayFinderView.ParentWayFinderTrackedId > 0)
				{
					global::Kampai.UI.View.IParentWayFinderView parentWayFinderView = GetWayFinder(childWayFinderView.ParentWayFinderTrackedId) as global::Kampai.UI.View.IParentWayFinderView;
					if (parentWayFinderView != null)
					{
						parentWayFinderView.RemoveChildWayFinder(trackedId);
						global::System.Collections.Generic.Dictionary<int, global::Kampai.UI.View.IChildWayFinderView> childrenWayFinders = parentWayFinderView.ChildrenWayFinders;
						if (childrenWayFinders != null && childrenWayFinders.Count == 0)
						{
							RemoveWayFinder(parentWayFinderView.TrackedId, false);
						}
					}
				}
				else
				{
					global::Kampai.UI.View.IParentWayFinderView parentWayFinderView2 = wayFinderView as global::Kampai.UI.View.IParentWayFinderView;
					if (parentWayFinderView2 != null)
					{
						global::System.Collections.Generic.Dictionary<int, global::Kampai.UI.View.IChildWayFinderView> childrenWayFinders2 = parentWayFinderView2.ChildrenWayFinders;
						if (childrenWayFinders2 != null && childrenWayFinders2.Count > 0)
						{
							foreach (global::Kampai.UI.View.IChildWayFinderView value in childrenWayFinders2.Values)
							{
								RemoveWayFinder(value.TrackedId, false);
							}
							return;
						}
						trackedParentWayFinders.Remove(trackedId);
					}
				}
				global::UnityEngine.Object.Destroy(wayFinderView.GameObject);
				if (updatePriority)
				{
					UpdateWayFinderPriority();
				}
			}
			else
			{
				logger.Warning("Way finder with id: {0} does not exist, ignoring remove", trackedId);
			}
		}

		internal global::Kampai.UI.View.IWayFinderView GetWayFinder(int trackedId)
		{
			if (trackedWayFinders != null && trackedWayFinders.ContainsKey(trackedId))
			{
				return trackedWayFinders[trackedId];
			}
			return null;
		}

		internal void AddQuestToExistingWayFinder(int questDefId, int trackedId)
		{
			global::Kampai.UI.View.IWayFinderView wayFinder = GetWayFinder(trackedId);
			if (wayFinder != null)
			{
				global::Kampai.UI.View.IQuestWayFinderView questWayFinderView = wayFinder as global::Kampai.UI.View.IQuestWayFinderView;
				if (questWayFinderView != null)
				{
					questWayFinderView.AddQuest(questDefId);
					return;
				}
				logger.Warning("Failed to add quest def id: {0} to way finder with tracked id: {1} since the way finder is not a quest way finder", questDefId, trackedId);
			}
			else
			{
				logger.Warning("Failed to add quest def id: {0} to way finder with tracked id: {1} since the way finder does not exist", questDefId, trackedId);
			}
		}

		internal void RemoveQuestFromExistingWayFinder(int questDefId, int trackedId)
		{
			global::Kampai.UI.View.IWayFinderView wayFinder = GetWayFinder(trackedId);
			if (wayFinder != null)
			{
				global::Kampai.UI.View.IQuestWayFinderView questWayFinderView = wayFinder as global::Kampai.UI.View.IQuestWayFinderView;
				if (questWayFinderView != null)
				{
					questWayFinderView.RemoveQuest(questDefId);
					return;
				}
				logger.Warning("Failed to remove quest def id: {0} from way finder with tracked id: {1} since the way finder is not a quest way finder", questDefId, trackedId);
			}
			else
			{
				logger.Warning("Failed to remove quest def id: {0} from way finder with tracked id: {1} since the way finder does not exist", questDefId, trackedId);
			}
		}

		private void SetForceHideForAllWayFinders()
		{
			foreach (global::Kampai.UI.View.IWayFinderView value in trackedWayFinders.Values)
			{
				value.SetForceHide(isForceHideEnabled);
			}
		}

		internal void HideAllWayFinders()
		{
			logger.Info("Hiding all way finders");
			isForceHideEnabled = true;
			SetForceHideForAllWayFinders();
		}

		internal void ShowAllWayFinders()
		{
			logger.Info("Showing all way finders");
			isForceHideEnabled = false;
			SetForceHideForAllWayFinders();
		}

		internal void SetLimitTikiBarWayFinders(bool limitTikiBarWayFinders)
		{
			this.limitTikiBarWayFinders = limitTikiBarWayFinders;
		}

		internal void UpdateWayFinderPriority()
		{
			logger.Info("Updating way finder priorities");
			foreach (global::Kampai.UI.View.IParentWayFinderView value in trackedParentWayFinders.Values)
			{
				value.UpdateWayFinderIcon();
			}
			if (playerService.GetHighestFtueCompleted() < 7)
			{
				logger.Info("Ignoring way finder priorities since we are in ftue.");
				return;
			}
			global::Kampai.UI.View.IWayFinderView prioritizedWayFinder = GetPrioritizedWayFinder();
			if (prioritizedWayFinder != null)
			{
				SetWayFinderSnappable(prioritizedWayFinder);
				if (prioritizedWayFinder.TrackedId != orderBoardWayFinderSettings.TrackedId && GetWayFinder(orderBoardWayFinderSettings.TrackedId) != null)
				{
					RemoveWayFinder(orderBoardWayFinderSettings.TrackedId, false);
				}
				return;
			}
			global::Kampai.UI.View.IWayFinderView wayFinder = GetWayFinder(orderBoardWayFinderSettings.TrackedId);
			if (wayFinder == null)
			{
				wayFinder = CreateWayFinder(orderBoardWayFinderSettings, false);
				SetWayFinderSnappable(wayFinder);
			}
		}

		private global::Kampai.UI.View.IWayFinderView GetPrioritizedWayFinder()
		{
			foreach (global::System.Collections.Generic.List<global::Kampai.UI.View.WayFinderPanelView.PriorityFunction> allPriorityFunction in allPriorityFunctions)
			{
				foreach (global::Kampai.UI.View.IWayFinderView value in trackedWayFinders.Values)
				{
					if (value.TrackedId == 301 || !PassesPriorityFunctions(value, allPriorityFunction))
					{
						continue;
					}
					return value;
				}
			}
			return null;
		}

		private bool PassesPriorityFunctions(global::Kampai.UI.View.IWayFinderView wayFinderView, global::System.Collections.Generic.List<global::Kampai.UI.View.WayFinderPanelView.PriorityFunction> priorityFunctions)
		{
			foreach (global::Kampai.UI.View.WayFinderPanelView.PriorityFunction priorityFunction in priorityFunctions)
			{
				if (!priorityFunction(wayFinderView))
				{
					return false;
				}
			}
			return true;
		}

		private void SetWayFinderSnappable(global::Kampai.UI.View.IWayFinderView snappableWayFinderView)
		{
			logger.Info("Setting way finder with tracked id: {0} to be dynamic (snappable)", snappableWayFinderView.TrackedId);
			foreach (global::Kampai.UI.View.IWayFinderView value in trackedWayFinders.Values)
			{
				value.Snappable = false;
			}
			global::Kampai.UI.View.IChildWayFinderView childWayFinderView = snappableWayFinderView as global::Kampai.UI.View.IChildWayFinderView;
			if (childWayFinderView != null)
			{
				int parentWayFinderTrackedId = childWayFinderView.ParentWayFinderTrackedId;
				logger.Info("Since way finder with id: {0} is a child of {1}(it's parent way finder) - we will make the parent snappable", childWayFinderView.TrackedId, parentWayFinderTrackedId);
				global::Kampai.UI.View.IParentWayFinderView parentWayFinderView = GetWayFinder(parentWayFinderTrackedId) as global::Kampai.UI.View.IParentWayFinderView;
				if (parentWayFinderView != null)
				{
					parentWayFinderView.Snappable = true;
					return;
				}
				logger.Warning("Parent way finder with tracked id: {0} does not exist, child with id:{1} has no parent!", parentWayFinderTrackedId, childWayFinderView.TrackedId);
			}
			else
			{
				snappableWayFinderView.Snappable = true;
			}
		}

		private void LateUpdate()
		{
			if (playerService.GetHighestFtueCompleted() < 7)
			{
				global::Kampai.UI.View.IWayFinderView wayFinder = GetWayFinder(78);
				global::Kampai.UI.View.IWayFinderView wayFinder2 = GetWayFinder(313);
				if (wayFinder != null && wayFinder2 != null && limitTikiBarWayFinders)
				{
					wayFinder.SetForceHide(true);
					wayFinder2.SetForceHide(true);
				}
			}
		}
	}
}
