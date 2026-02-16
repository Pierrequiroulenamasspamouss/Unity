namespace Kampai.UI
{
	public class BuildMenuService : global::Kampai.UI.IBuildMenuService
	{
		private global::Kampai.UI.BuildMenuLocalState localState;

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public ILocalPersistanceService localPersistanceService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetBadgeForStoreTabSignal setBadgeForTabSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetNewUnlockForStoreTabSignal setNewUnlockForTabSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetNewUnlockForBuildMenuSignal setNewUnlockForBuildMenuSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetInventoryCountForBuildMenuSignal setInventoryCountForBuildMenuSignal { get; set; }

		[PostConstruct]
		public void PostConstruct()
		{
			LoadPersist();
		}

		public void RetoreBuidMenuState(global::System.Collections.Generic.Dictionary<global::Kampai.Game.StoreItemType, global::System.Collections.Generic.List<global::Kampai.UI.View.StoreButtonView>> buttonViews)
		{
			bool unlockChecked = localState.UnlockChecked;
			UpdateNewUnlockList(buttonViews);
			if (localState.UncheckedInventoryItemOnTabs.Count <= 0)
			{
				return;
			}
			int num = 0;
			foreach (global::System.Collections.Generic.KeyValuePair<global::Kampai.Game.StoreItemType, int> uncheckedInventoryItemOnTab in localState.UncheckedInventoryItemOnTabs)
			{
				if (uncheckedInventoryItemOnTab.Value != 0)
				{
					setBadgeForTabSignal.Dispatch(uncheckedInventoryItemOnTab.Key, uncheckedInventoryItemOnTab.Value);
					num += uncheckedInventoryItemOnTab.Value;
				}
			}
			if (num > 0 && !unlockChecked)
			{
				setInventoryCountForBuildMenuSignal.Dispatch(num);
			}
		}

		public void SetStoreUnlockCheckedState(bool unlockChecked)
		{
			if (localState.UnlockChecked != unlockChecked)
			{
				localState.UnlockChecked = unlockChecked;
				PersistLocalState();
			}
		}

		public void AddNewUnlockedItem(global::Kampai.Game.StoreItemType type, int buildingDefinitionID)
		{
			if (localState.NewUnlockedItemOnTabs.ContainsKey(type))
			{
				if (!localState.NewUnlockedItemOnTabs[type].Contains(buildingDefinitionID))
				{
					localState.NewUnlockedItemOnTabs[type].Add(buildingDefinitionID);
				}
				else
				{
					logger.Warning("New unlock list already contains this item {0}", buildingDefinitionID);
				}
			}
			else
			{
				global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>();
				list.Add(buildingDefinitionID);
				localState.NewUnlockedItemOnTabs.Add(type, list);
			}
			PersistLocalState();
		}

		public void RemoveNewUnlockedItem(global::Kampai.Game.StoreItemType type, int buildingDefinitionID)
		{
			if (!localState.NewUnlockedItemOnTabs.ContainsKey(type) || !localState.NewUnlockedItemOnTabs[type].Contains(buildingDefinitionID))
			{
				return;
			}
			localState.NewUnlockedItemOnTabs[type].Remove(buildingDefinitionID);
			if (localState.NewUnlockedItemOnTabs[type].Count == 0)
			{
				localState.NewUnlockedItemOnTabs.Remove(type);
				if (localState.UncheckedTabs.Contains(type))
				{
					setNewUnlockForTabSignal.Dispatch(type, 0);
					localState.UncheckedTabs.Remove(type);
				}
			}
			PersistLocalState();
		}

		public void ClearAllNewUnlockItems()
		{
			localState.UncheckedTabs.Clear();
			localState.NewUnlockedItemOnTabs.Clear();
		}

		public void UpdateNewUnlockList(global::System.Collections.Generic.Dictionary<global::Kampai.Game.StoreItemType, global::System.Collections.Generic.List<global::Kampai.UI.View.StoreButtonView>> buttonViews, bool updateBuildMenuButton = true)
		{
			global::System.Collections.Generic.Dictionary<int, int> buildingOnBoardCountMap = playerService.GetBuildingOnBoardCountMap();
			int num = 0;
			foreach (global::System.Collections.Generic.KeyValuePair<global::Kampai.Game.StoreItemType, global::System.Collections.Generic.List<global::Kampai.UI.View.StoreButtonView>> buttonView in buttonViews)
			{
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				global::Kampai.Game.StoreItemType key = buttonView.Key;
				foreach (global::Kampai.UI.View.StoreButtonView item in buttonView.Value)
				{
					num3++;
					if (global::Kampai.UI.View.StoreButtonBuilder.DetermineUnlock(item, playerService, buildingOnBoardCountMap, definitionService, logger))
					{
						if (!localState.UncheckedTabs.Contains(key))
						{
							localState.UncheckedTabs.Add(key);
						}
						AddNewUnlockedItem(key, item.definition.ID);
						num4++;
						num++;
					}
					else if (localState.NewUnlockedItemOnTabs.ContainsKey(key) && localState.NewUnlockedItemOnTabs[key].Contains(item.definition.ID))
					{
						item.SetNewUnlockState(true);
						num4++;
						num++;
					}
					item.SetShouldBerendered(true);
					if (item.IsUnlocked())
					{
						num2++;
					}
					if (num3 - num2 > 2)
					{
						item.SetButtonTeased(true);
					}
					if (num3 - num2 > 4)
					{
						item.SetShouldBerendered(false);
					}
				}
				setNewUnlockForTabSignal.Dispatch(buttonView.Key, num4);
			}
			if (updateBuildMenuButton && num > 0)
			{
				setNewUnlockForBuildMenuSignal.Dispatch(num);
			}
		}

		public void IncreaseInventoryItemCountOnTab(global::Kampai.Game.StoreItemType type)
		{
			if (localState.UncheckedInventoryItemOnTabs.ContainsKey(type))
			{
				global::System.Collections.Generic.IDictionary<global::Kampai.Game.StoreItemType, int> uncheckedInventoryItemOnTabs;
				global::System.Collections.Generic.IDictionary<global::Kampai.Game.StoreItemType, int> dictionary = (uncheckedInventoryItemOnTabs = localState.UncheckedInventoryItemOnTabs);
				global::Kampai.Game.StoreItemType key2;
				global::Kampai.Game.StoreItemType key = (key2 = type);
				int num = uncheckedInventoryItemOnTabs[key2];
				dictionary[key] = num + 1;
			}
			else
			{
				localState.UncheckedInventoryItemOnTabs.Add(type, 1);
			}
			setBadgeForTabSignal.Dispatch(type, localState.UncheckedInventoryItemOnTabs[type]);
		}

		public void DecreaseInventoryItemCountOnTab(global::Kampai.Game.StoreItemType type)
		{
			if (localState.UncheckedInventoryItemOnTabs.ContainsKey(type))
			{
				global::System.Collections.Generic.IDictionary<global::Kampai.Game.StoreItemType, int> uncheckedInventoryItemOnTabs;
				global::System.Collections.Generic.IDictionary<global::Kampai.Game.StoreItemType, int> dictionary = (uncheckedInventoryItemOnTabs = localState.UncheckedInventoryItemOnTabs);
				global::Kampai.Game.StoreItemType key2;
				global::Kampai.Game.StoreItemType key = (key2 = type);
				int num = uncheckedInventoryItemOnTabs[key2];
				dictionary[key] = num - 1;
				if (localState.UncheckedInventoryItemOnTabs[type] <= 0)
				{
					localState.UncheckedInventoryItemOnTabs.Remove(type);
				}
			}
		}

		public void ClearTab(global::Kampai.Game.StoreItemType type)
		{
			if (localState.UncheckedInventoryItemOnTabs.ContainsKey(type))
			{
				localState.UncheckedInventoryItemOnTabs.Remove(type);
			}
			if (localState.UncheckedTabs.Contains(type))
			{
				localState.UncheckedTabs.Remove(type);
			}
		}

		private void PersistLocalState()
		{
			if (localState != null)
			{
				try
				{
					string data = global::Newtonsoft.Json.JsonConvert.SerializeObject(localState);
					localPersistanceService.PutDataPlayer("BuildMenuLocalSave", data);
					return;
				}
				catch (global::Newtonsoft.Json.JsonSerializationException ex)
				{
					logger.Error("PersistLocalState(): Json Parse Err: {0}", ex);
					return;
				}
			}
			localPersistanceService.DeleteKeyPlayer("BuildMenuLocalSave");
		}

		private void LoadPersist()
		{
			if (localPersistanceService.HasKeyPlayer("BuildMenuLocalSave"))
			{
				string dataPlayer = localPersistanceService.GetDataPlayer("BuildMenuLocalSave");
				if (dataPlayer != null)
				{
					try
					{
						localState = global::Kampai.Util.FastJsonParser.Deserialize<global::Kampai.UI.BuildMenuLocalState>(dataPlayer);
					}
					catch (global::System.Exception e)
					{
						HandleJsonException(e);
					}
				}
			}
			if (localState == null)
			{
				localState = new global::Kampai.UI.BuildMenuLocalState();
			}
		}

		private void HandleJsonException(global::System.Exception e)
		{
			logger.Error("BuildMenuService.LoadFromPersistence(): Json Parse Err: {0}", e);
		}
	}
}
