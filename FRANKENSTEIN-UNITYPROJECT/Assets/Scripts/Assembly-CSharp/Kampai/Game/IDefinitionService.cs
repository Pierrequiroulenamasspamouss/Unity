namespace Kampai.Game
{
	public interface IDefinitionService
	{
		bool Has(int id);

		bool Has<T>(int id) where T : global::Kampai.Game.Definition;

		T Get<T>(int id) where T : global::Kampai.Game.Definition;

		T Get<T>() where T : global::Kampai.Game.Definition;

		global::Kampai.Game.Definition Get(int id);

		bool TryGet<T>(int id, out T definition) where T : global::Kampai.Game.Definition;

		global::System.Collections.Generic.IList<string> GetEnvironemtDefinition();

		global::Kampai.Game.PartyDefinition GetPartyDefinition();

		void ReclaimEnfironmentDefinitions();

		global::System.Collections.Generic.List<T> GetAll<T>() where T : global::Kampai.Game.Definition;

		global::Kampai.Game.Transaction.WeightedDefinition GetGachaWeightsForNumMinions(int numMinions, bool party);

		global::System.Collections.Generic.List<global::Kampai.Game.Transaction.WeightedDefinition> GetAllGachaDefinitions();

		global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Definition> GetAllDefinitions();

		void Deserialize(string json);

		int GetHarvestItemDefinitionIdFromTransactionId(int transactionId);

		string GetHarvestIconFromTransactionID(int transactionId);

		bool HasUnlockItemInTransactionOutput(int transactionID);

		int GetBuildingDefintionIDFromItemDefintionID(int itemDefinitionID);

		global::Kampai.Game.BridgeDefinition GetBridgeDefinition(int itemDefinitionID);

		int ExtractQuantityFromTransaction(int transactionID, int definitionID);

		int GetLevelItemUnlocksAt(int definitionID);

		global::Kampai.Game.TaskLevelBandDefinition GetTaskLevelBandForLevel(int level);

		global::Kampai.Game.RushTimeBandDefinition GetRushTimeBandForTime(int timeRemainingInSeconds);

		string GetInitialPlayer();

		string GetBuildingFootprint(int ID);

		int GetIncrementalCost(global::Kampai.Game.Definition definition);
		
		bool IsReady { get; }
	}
}
