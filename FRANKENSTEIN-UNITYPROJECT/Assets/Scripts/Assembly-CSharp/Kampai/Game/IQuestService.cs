namespace Kampai.Game
{
	public interface IQuestService
	{
		void Initialize();

		bool HasScript(global::Kampai.Game.Quest quest, bool pre);

		bool HasScript(global::Kampai.Game.Quest quest, bool pre, int stepID, bool isQuestStep = false);

		void StartQuestScript(global::Kampai.Game.Quest quest, bool pre);

		void StartQuestScript(global::Kampai.Game.Quest quest, bool pre, int stepID, bool isStepQuest = false);

		void RushQuestStep(int questId, int step);

		void UpdateBlackMarketTask();

		void UpdateDeliveryTask();

		void UpdateConstructionTask();

		void UpdateMignetteTask(global::Kampai.Game.Building building, global::Kampai.Game.QuestTaskTransition taskState);

		void UpdateBridgeRepairTask(global::Kampai.Game.Building building, global::Kampai.Game.QuestTaskTransition taskTransition);

		void UpdateCabanaRepairTask(global::Kampai.Game.Building building, global::Kampai.Game.QuestTaskTransition taskTransition);

		void UpdateHarvestTask();

		void UpdateHarvestTask(int item, global::Kampai.Game.QuestTaskTransition taskTransition);

		void StartMinionTask(int buildingDefId);

		void HarvestReady(int buildingDefId);

		void HarvestTaskableComplete(int buildingDefId);

		void RepairStage();

		void RepairWelcomeHut();

		void RepairFountain();

		void stop();

		bool IsOneOffCraftableDisplayable(int questDefinitionId, int trackedItemDefinitionID);

		bool IsQuestCompleted(int questDefinitionID);

		void SetQuestLineState(int questLineId, global::Kampai.Game.QuestLineState targetState);

		bool IsBridgeQuestComplete(int bridgeDefId);

		global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.QuestLine> GetQuestLines();

		global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Quest> GetQuestMap();

		global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<int>> GetQuestUnlockTree();

		void AddQuest(global::Kampai.Game.Quest quest);

		void RemoveQuest(global::Kampai.Game.Quest quest);

		global::Kampai.Game.QuestState GoToNextQuestState(global::Kampai.Game.Quest quest);

		void GoToQuestState(global::Kampai.Game.Quest quest, global::Kampai.Game.QuestState targetState);

		global::Kampai.Game.QuestStepState GoToNextTaskState(global::Kampai.Game.Quest quest, int stepIndex, bool isTaskComplete = false);

		void GoToTaskState(global::Kampai.Game.Quest quest, int stepIndex, global::Kampai.Game.QuestStepState targetState);

		string GetEventName(string key, params object[] args);

		void PauseQuestScripts();

		void ResumeQuestScripts();
	}
}
