namespace Kampai.Game.View
{
	public class StorageBuildingMediator : global::strange.extensions.mediation.impl.Mediator
	{
		private global::Kampai.Game.StorageBuilding storageBuilding;

		private int trackedId;

		[Inject]
		public global::Kampai.Game.View.StorageBuildingView view { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IMarketplaceService marketplaceService { get; set; }

		[Inject]
		public global::Kampai.Game.UpdateMarketplaceRepairStateSignal updateRepairStateSignal { get; set; }

		[Inject]
		public global::Kampai.Game.MarketplaceUpdateSoldItemsSignal updateSoldItemsSignal { get; set; }

		[Inject]
		public global::Kampai.Game.StartMarketplaceOnboardingSignal startMarketplaceOnboardingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.AwardLevelSignal awardLevelSignal { get; set; }

		[Inject(global::Kampai.Game.GameElement.BUILDING_MANAGER)]
		public global::UnityEngine.GameObject buildingManager { get; set; }

		[Inject]
		public ILocalPersistanceService localPersistence { get; set; }

		[Inject]
		public global::Kampai.UI.View.CreateWayFinderSignal createWayFinderSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RemoveWayFinderSignal removeWayFinderSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.GetWayFinderSignal getWayFinderSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject(global::Kampai.UI.View.UIElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable UIContext { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner RoutineRunner { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			storageBuilding = GetStorageBuilding();
			trackedId = storageBuilding.ID;
			view.Init(storageBuilding);
			updateRepairStateSignal.AddListener(UpdateRepairState);
			updateSoldItemsSignal.AddListener(UpdateSoldSales);
			awardLevelSignal.AddListener(LeveledUp);
			view.SetMarketplaceEnabled(marketplaceService.IsUnlocked());
			UIContext.injectionBinder.GetInstance<global::Kampai.UI.View.SetStorageCapacitySignal>().AddListener(OnSetStorageCapacity);
			RoutineRunner.StartCoroutine(WaitAFrame());
		}

		public override void OnRemove()
		{
			base.OnRemove();
			updateRepairStateSignal.RemoveListener(UpdateRepairState);
			updateSoldItemsSignal.RemoveListener(UpdateSoldSales);
			awardLevelSignal.RemoveListener(LeveledUp);
			UIContext.injectionBinder.GetInstance<global::Kampai.UI.View.SetStorageCapacitySignal>().RemoveListener(OnSetStorageCapacity);
		}

		private global::System.Collections.IEnumerator WaitAFrame()
		{
			yield return null;
			OnSetStorageCapacity();
		}

		private void OnSetStorageCapacity()
		{
			bool flag = IsMissingItemToExpand();
			ToggleWayFinder(!flag, false);
		}

		private bool IsMissingItemToExpand()
		{
			int currentStorageBuildingLevel = storageBuilding.CurrentStorageBuildingLevel;
			global::System.Collections.Generic.IList<global::Kampai.Game.StorageUpgradeDefinition> storageUpgrades = storageBuilding.Definition.StorageUpgrades;
			if (currentStorageBuildingLevel >= storageUpgrades.Count)
			{
				return true;
			}
			global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition = definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(storageUpgrades[currentStorageBuildingLevel].TransactionId);
			if (transactionDefinition != null)
			{
				return playerService.IsMissingItemFromTransaction(transactionDefinition);
			}
			return true;
		}

		public void UpdateRepairState()
		{
			view.SetMarketplaceEnabled(marketplaceService.IsUnlocked());
		}

		public void UpdateSoldSales(bool showVFX)
		{
			bool flag = marketplaceService.AreThereSoldItems();
			bool flag2 = marketplaceService.AreTherePendingItems();
			bool flag3 = view.ToggleGrindReward(flag);
			bool flag4 = view.TogglePendingSale(flag2);
			ToggleWayFinder(flag, true);
			if (flag4 && !flag2 && showVFX)
			{
				global::Kampai.Game.View.BuildingManagerView component = buildingManager.GetComponent<global::Kampai.Game.View.BuildingManagerView>();
				global::Kampai.Game.View.BuildingObject buildingObject = component.GetBuildingObject(storageBuilding.ID);
				if (buildingObject != null && flag)
				{
					buildingObject.SetVFXState("PendingSale_Disappear");
				}
			}
			else if (flag3 && showVFX)
			{
				global::Kampai.Game.View.BuildingManagerView component2 = buildingManager.GetComponent<global::Kampai.Game.View.BuildingManagerView>();
				global::Kampai.Game.View.BuildingObject buildingObject2 = component2.GetBuildingObject(storageBuilding.ID);
				if (flag)
				{
					buildingObject2.SetVFXState("GrindReward_Appear");
				}
				else
				{
					buildingObject2.SetVFXState("GrindReward_Disappear");
				}
			}
		}

		private global::Kampai.Game.StorageBuilding GetStorageBuilding()
		{
			global::Kampai.Game.View.StorageBuildingObject component = GetComponent<global::Kampai.Game.View.StorageBuildingObject>();
			if (component != null)
			{
				return component.storageBuilding;
			}
			logger.Error("StorageBuildingMediator: could not find StorageBuilding for building " + base.gameObject.name);
			return null;
		}

		public void LeveledUp(global::Kampai.Game.Transaction.TransactionDefinition unlockTransaction)
		{
			if (global::Kampai.Game.Transaction.TransactionUtil.ExtractQuantityFromTransaction(unlockTransaction, 312) >= 1)
			{
				localPersistence.PutDataPlayer("MarketSurfacing", storageBuilding.ID.ToString());
				startMarketplaceOnboardingSignal.Dispatch(storageBuilding.ID);
			}
		}

		internal void ToggleWayFinder(bool enable, bool soldItemWayFinder)
		{
			getWayFinderSignal.Dispatch(trackedId, delegate(int wayFinderId, global::Kampai.UI.View.IWayFinderView wayFinderView)
			{
				if (wayFinderView == null)
				{
					if (enable)
					{
						createWayFinderSignal.Dispatch(new global::Kampai.UI.View.WayFinderSettings(trackedId));
					}
				}
				else
				{
					global::Kampai.UI.View.StorageBuildingWayFinderView storageBuildingWayFinderView = wayFinderView as global::Kampai.UI.View.StorageBuildingWayFinderView;
					if (!(storageBuildingWayFinderView == null))
					{
						if (enable)
						{
							if (soldItemWayFinder)
							{
								storageBuildingWayFinderView.SetIconToItemSold();
							}
						}
						else if (soldItemWayFinder)
						{
							if (IsMissingItemToExpand())
							{
								removeWayFinderSignal.Dispatch(wayFinderId);
							}
							else
							{
								storageBuildingWayFinderView.SetIconToDefault();
							}
						}
						else if (marketplaceService.AreThereSoldItems())
						{
							storageBuildingWayFinderView.SetIconToItemSold();
						}
						else
						{
							removeWayFinderSignal.Dispatch(wayFinderId);
						}
					}
				}
			});
		}
	}
}
