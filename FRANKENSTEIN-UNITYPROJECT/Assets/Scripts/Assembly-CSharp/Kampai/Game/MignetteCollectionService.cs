namespace Kampai.Game
{
	public class MignetteCollectionService
	{
		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.Mignette.MignetteGameModel mignetteGameModel { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public global::Kampai.Game.Transaction.TransactionDefinition pendingRewardTransaction { get; set; }

		public global::Kampai.Game.RewardCollection GetCollectionForActiveMignette()
		{
			return GetActiveCollectionForMignette(mignetteGameModel.BuildingId);
		}

		public global::Kampai.Game.RewardCollection GetActiveCollectionForMignette(int mignetteBuildingId, bool persistCreatedCollection = true)
		{
			global::Kampai.Game.MignetteBuilding byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.MignetteBuilding>(mignetteBuildingId);
			return GetActiveCollectionForMignette(byInstanceId, persistCreatedCollection);
		}

		public global::Kampai.Game.RewardCollection GetActiveCollectionForMignette(global::Kampai.Game.MignetteBuilding building, bool persistCreatedCollection = true)
		{
			global::Kampai.Game.RewardCollection orCreateActiveCollection = getOrCreateActiveCollection(building.StartedMainCollectionIDs, building.Definition.MainCollectionDefinitionIDs, persistCreatedCollection);
			if (orCreateActiveCollection != null)
			{
				return orCreateActiveCollection;
			}
			orCreateActiveCollection = getOrCreateActiveCollection(building.StartedRepeatableCollectionIDs, building.Definition.RepeatableCollectionDefinitionIDs, persistCreatedCollection);
			if (orCreateActiveCollection != null)
			{
				return orCreateActiveCollection;
			}
			global::Kampai.Game.RewardCollection rewardCollection = null;
			foreach (int startedRepeatableCollectionID in building.StartedRepeatableCollectionIDs)
			{
				orCreateActiveCollection = playerService.GetByInstanceId<global::Kampai.Game.RewardCollection>(startedRepeatableCollectionID);
				orCreateActiveCollection.ResetProgress();
				if (rewardCollection == null)
				{
					rewardCollection = orCreateActiveCollection;
				}
			}
			return rewardCollection;
		}

		private global::Kampai.Game.RewardCollection getOrCreateActiveCollection(global::System.Collections.Generic.IList<int> startedCollectionIDs, global::System.Collections.Generic.IList<int> collectionDefinitionIDs, bool persistCreatedCollection)
		{
			global::System.Collections.Generic.HashSet<int> hashSet = new global::System.Collections.Generic.HashSet<int>();
			foreach (int startedCollectionID in startedCollectionIDs)
			{
				global::Kampai.Game.RewardCollection byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.RewardCollection>(startedCollectionID);
				if (!byInstanceId.IsCompleted())
				{
					return byInstanceId;
				}
				hashSet.Add(byInstanceId.Definition.ID);
			}
			if (startedCollectionIDs.Count < collectionDefinitionIDs.Count)
			{
				foreach (int collectionDefinitionID in collectionDefinitionIDs)
				{
					if (!hashSet.Contains(collectionDefinitionID))
					{
						global::Kampai.Game.RewardCollectionDefinition definition = definitionService.Get<global::Kampai.Game.RewardCollectionDefinition>(collectionDefinitionID);
						global::Kampai.Game.RewardCollection rewardCollection = new global::Kampai.Game.RewardCollection(definition);
						if (persistCreatedCollection)
						{
							playerService.Add(rewardCollection);
							startedCollectionIDs.Add(rewardCollection.ID);
						}
						return rewardCollection;
					}
				}
			}
			return null;
		}

		public void IncreaseScoreForMignetteCollection(int mignetteBuildingId, int scoreIncrease)
		{
			global::Kampai.Game.RewardCollection activeCollectionForMignette = GetActiveCollectionForMignette(mignetteBuildingId);
			activeCollectionForMignette.IncreaseScore(scoreIncrease);
		}

		public global::Kampai.Game.Transaction.TransactionDefinition CreditRewardForActiveMignette()
		{
			global::Kampai.Game.RewardCollection collectionForActiveMignette = GetCollectionForActiveMignette();
			if (!collectionForActiveMignette.HasRewardReadyForCollection())
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "CreditRewardForActiveMignette called, but no reward is available! collectionID: " + collectionForActiveMignette.ID);
			}
			int transactionIDReadyForCollection = collectionForActiveMignette.GetTransactionIDReadyForCollection();
			global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition = definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(transactionIDReadyForCollection);
			playerService.RunEntireTransaction(transactionDefinition, global::Kampai.Game.TransactionTarget.NO_VISUAL, null);
			collectionForActiveMignette.NumRewardsCollected++;
			if (collectionForActiveMignette.IsCompleted())
			{
				int num = collectionForActiveMignette.CollectionScoreProgress - collectionForActiveMignette.GetMaxScore();
				if (num > 0)
				{
					GetCollectionForActiveMignette().CollectionScoreProgress += num;
				}
			}
			return transactionDefinition;
		}

		public void ResetMignetteProgress()
		{
			global::System.Collections.Generic.List<global::Kampai.Game.MignetteBuilding> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.MignetteBuilding>();
			foreach (global::Kampai.Game.MignetteBuilding item in instancesByType)
			{
				global::Kampai.Game.RewardCollection activeCollectionForMignette = GetActiveCollectionForMignette(item.ID);
				activeCollectionForMignette.ResetProgress();
			}
		}
	}
}
