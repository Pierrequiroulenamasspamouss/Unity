namespace Kampai.UI.View
{
	public abstract class AbstractQuestWayFinderView : global::Kampai.UI.View.AbstractWayFinderView, global::Kampai.UI.View.IQuestWayFinderView, global::Kampai.UI.View.IWayFinderView, global::Kampai.UI.View.IWorldToGlassView
	{
		private global::System.Collections.Generic.List<int> allQuests;

		private bool ignoreFirstPriorityUpdate = true;

		private int currentActiveQuestIndex = -1;

		public global::Kampai.Game.Quest CurrentActiveQuest { get; private set; }

		protected override string WayFinderDefaultIcon
		{
			get
			{
				return wayFinderDefinition.NewQuestIcon;
			}
		}

		protected override void InitSubView()
		{
			allQuests = new global::System.Collections.Generic.List<int>();
			global::Kampai.UI.View.WayFinderSettings wayFinderSettings = m_Settings as global::Kampai.UI.View.WayFinderSettings;
			AddQuest(wayFinderSettings.QuestDefId);
			ignoreFirstPriorityUpdate = false;
		}

		internal override void Clear()
		{
			logger.Info("Clearing quests way finder with tracked id: {0}", TrackedId);
			if (allQuests != null)
			{
				allQuests.Clear();
			}
		}

		protected override bool OnCanUpdate()
		{
			if (zoomCameraModel.ZoomedIn)
			{
				return false;
			}
			if (m_Prestige != null && m_Prestige.Definition.ID != 40003)
			{
				return false;
			}
			return true;
		}

		public void AddQuest(int questDefId)
		{
			int num = allQuests.IndexOf(questDefId);
			if (num != -1)
			{
				logger.Warning("Quest def id:{0} is already added to way finder tracked id: {1}", questDefId, TrackedId);
				UpdateQuestIcon();
			}
			else
			{
				logger.Info("Adding quest def id:{0} to way finder tracked id: {1}", questDefId, TrackedId);
				allQuests.Add(questDefId);
				SetNextQuest(allQuests.Count - 1);
			}
		}

		public void RemoveQuest(int questDefId)
		{
			int num = allQuests.IndexOf(questDefId);
			if (num == -1)
			{
				logger.Warning("Quest def id: {0} was already removed previously, ignoring remove action.", questDefId);
				return;
			}
			logger.Info("Removing quest def id:{0} from way finder tracked id: {1}", questDefId, TrackedId);
			allQuests.Remove(questDefId);
			if (allQuests.Count == 0)
			{
				CurrentActiveQuest = null;
				RemoveWayFinderSignal.Dispatch();
			}
			else
			{
				SetNextQuest(num);
			}
		}

		public void SetNextQuest(int indexToSet = -1)
		{
			if (allQuests.Count != 0)
			{
				if (indexToSet == -1)
				{
					currentActiveQuestIndex++;
				}
				else
				{
					currentActiveQuestIndex = indexToSet;
				}
				currentActiveQuestIndex %= allQuests.Count;
				CurrentActiveQuest = GetQuestByDefId(allQuests[currentActiveQuestIndex]);
				UpdateQuestIcon();
			}
		}

		public global::Kampai.Game.Quest GetQuestByDefId(int questDefId)
		{
			return playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.Quest>(questDefId);
		}

		protected virtual bool CanUpdateQuestIcon()
		{
			return true;
		}

		private void UpdateQuestIcon()
		{
			if (CanUpdateQuestIcon())
			{
				if (IsQuestComplete())
				{
					SetQuestIcon(wayFinderDefinition.QuestCompleteIcon);
				}
				else if (IsNewQuestAvailable())
				{
					SetQuestIcon(wayFinderDefinition.NewQuestIcon);
				}
				else if (IsTaskReady())
				{
					SetQuestIcon(wayFinderDefinition.TaskCompleteIcon);
				}
				else if (IsQuestAvailable())
				{
					SetQuestIcon(wayFinderDefinition.NewQuestIcon);
				}
			}
		}

		private void SetQuestIcon(string icon)
		{
			UpdateIcon(icon);
			if (!ignoreFirstPriorityUpdate)
			{
				UpdateWayFinderPrioritySignal.Dispatch();
			}
		}

		public bool IsNewQuestAvailable()
		{
			if (CurrentActiveQuest != null)
			{
				global::Kampai.Game.QuestState state = CurrentActiveQuest.state;
				if (state == global::Kampai.Game.QuestState.Notstarted || state == global::Kampai.Game.QuestState.RunningStartScript)
				{
					return true;
				}
			}
			return false;
		}

		public bool IsQuestAvailable()
		{
			return CurrentActiveQuest != null && CurrentActiveQuest.state == global::Kampai.Game.QuestState.RunningTasks && !IsTaskReady();
		}

		public bool IsTaskReady()
		{
			if (CurrentActiveQuest != null && CurrentActiveQuest.state == global::Kampai.Game.QuestState.RunningTasks)
			{
				foreach (global::Kampai.Game.QuestStep step in CurrentActiveQuest.Steps)
				{
					if (step.state == global::Kampai.Game.QuestStepState.Ready)
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool IsQuestComplete()
		{
			if (CurrentActiveQuest != null)
			{
				switch (CurrentActiveQuest.state)
				{
				case global::Kampai.Game.QuestState.RunningCompleteScript:
				case global::Kampai.Game.QuestState.Harvestable:
				case global::Kampai.Game.QuestState.Complete:
					return true;
				}
			}
			return false;
		}
	}
}
