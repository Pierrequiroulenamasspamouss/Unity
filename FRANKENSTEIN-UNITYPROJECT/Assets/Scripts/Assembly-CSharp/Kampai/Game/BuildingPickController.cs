namespace Kampai.Game
{
	public class BuildingPickController : global::strange.extensions.command.impl.Command
	{
		private global::Kampai.UI.Model.LevelUpModel levelUpModel;

		[Inject]
		public int pickEvent { get; set; }

		[Inject]
		public global::UnityEngine.Vector3 inputPosition { get; set; }

		[Inject(global::Kampai.Main.MainElement.CAMERA)]
		public global::UnityEngine.Camera camera { get; set; }

		[Inject(global::Kampai.Game.GameElement.GROUND_PLANE)]
		public global::Kampai.Util.Boxed<global::UnityEngine.Plane> groundPlane { get; set; }

		[Inject(global::Kampai.Game.GameElement.MINION_MANAGER)]
		public global::UnityEngine.GameObject minionManager { get; set; }

		[Inject(global::Kampai.UI.View.UIElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable uiContext { get; set; }

		[Inject]
		public global::Kampai.Common.PickControllerModel model { get; set; }

		[Inject]
		public global::Kampai.Game.SelectBuildingSignal selectBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.DeselectBuildingSignal deselectBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RevealBuildingSignal revealBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Common.DeselectTaskedMinionsSignal deselectTaskedMinionsSignal { get; set; }

		[Inject]
		public global::Kampai.Common.DragAndDropPickSignal dragAndDropSignal { get; set; }

		[Inject]
		public global::Kampai.Game.StartMinionTaskSignal startMinionTaskSignal { get; set; }

		[Inject]
		public global::Kampai.Common.TryHarvestBuildingSignal tryHarvestSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal globalSFXSignal { get; set; }

		[Inject]
		public global::Kampai.Common.DeselectAllMinionsSignal deselectMinionsSignal { get; set; }

		[Inject]
		public global::Kampai.Common.SelectMinionSignal selectMinionSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowNeedXMinionsSignal showNeedXMinionsSignal { get; set; }

		[Inject]
		public global::Kampai.Common.RepairBuildingSignal repairBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowQuestPanelSignal showQuestPanel { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowQuestRewardSignal showQuestRewardSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.OpenBuildingMenuSignal openBuildingMenuSignal { get; set; }

		[Inject]
		public global::Kampai.Game.DisplayInaccessibleMessageSignal displayInaccessibleMessageSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SendMinionToLeisureSignal sendMinionToLeisureSignal { get; set; }

		[Inject]
		public global::Kampai.Game.KillFunSignal killFunSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.UI.View.PopupMessageSignal popupMessageSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localeService { get; set; }

		[PostConstruct]
		public void PostConstruct()
		{
			levelUpModel = uiContext.injectionBinder.GetInstance<global::Kampai.UI.Model.LevelUpModel>();
		}

		public override void Execute()
		{
			switch (pickEvent)
			{
			case 2:
			{
				if (model.CurrentMode != global::Kampai.Common.PickControllerModel.Mode.Building || !(model.StartHitObject != null))
				{
					break;
				}
				global::Kampai.Game.View.BuildingObject component = model.StartHitObject.GetComponent<global::Kampai.Game.View.BuildingObject>();
				if (component == null)
				{
					break;
				}
				global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(component.ID);
				if (byInstanceId != null)
				{
					global::Kampai.Game.BuildingState state = byInstanceId.State;
					if (state == global::Kampai.Game.BuildingState.Construction || state == global::Kampai.Game.BuildingState.Inactive || state == global::Kampai.Game.BuildingState.Complete || model.IsInstanceIgnored(byInstanceId.ID))
					{
						break;
					}
				}
				TrySelectBuilding(component, component.ID);
				break;
			}
			case 3:
				PickEnd();
				break;
			}
		}

		private void PickEnd()
		{
			if (levelUpModel.IsRewardUiOpened || !(model.EndHitObject != null) || !(model.StartHitObject == model.EndHitObject) || model.DetectedMovement)
			{
				return;
			}
			global::Kampai.Game.View.BuildingObject component = model.EndHitObject.GetComponent<global::Kampai.Game.View.BuildingObject>();
			if (component != null)
			{
				global::Kampai.Game.View.IScaffoldingPart scaffoldingPart = component as global::Kampai.Game.View.IScaffoldingPart;
				if (scaffoldingPart != null)
				{
					global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(component.ID);
					revealBuildingSignal.Dispatch(byInstanceId);
				}
				else
				{
					PickEndBuilding(component);
				}
			}
		}

		private void PickEndBuilding(global::Kampai.Game.View.BuildingObject endHitObject)
		{
			global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(endHitObject.ID);
			if (byInstanceId != null)
			{
				if (!model.IsInstanceIgnored(byInstanceId.ID))
				{
					global::Kampai.Game.CabanaBuilding cabanaBuilding = byInstanceId as global::Kampai.Game.CabanaBuilding;
					if (cabanaBuilding != null && cabanaBuilding.Quest != null)
					{
						OpenCabanaQuest(cabanaBuilding.Quest);
					}
					if (byInstanceId.State == global::Kampai.Game.BuildingState.Broken)
					{
						repairBuildingSignal.Dispatch(byInstanceId);
						return;
					}
					if (byInstanceId.State == global::Kampai.Game.BuildingState.Inaccessible)
					{
						displayInaccessibleMessageSignal.Dispatch(byInstanceId);
						return;
					}
					openBuildingMenuSignal.Dispatch(endHitObject, byInstanceId);
					TrySendMinions(endHitObject, byInstanceId);
					tryHarvestSignal.Dispatch(endHitObject, delegate
					{
					});
					tryFun(byInstanceId);
				}
			}
			else
			{
				global::Kampai.Game.View.MignetteBuildingObject mignetteBuildingObject = endHitObject as global::Kampai.Game.View.MignetteBuildingObject;
				if (mignetteBuildingObject != null)
				{
					int id = mignetteBuildingObject.ID * -1;
					global::Kampai.Game.AspirationalBuildingDefinition aspirationalBuildingDefinition = definitionService.Get<global::Kampai.Game.AspirationalBuildingDefinition>(id);
					int buildingDefinitionID = aspirationalBuildingDefinition.BuildingDefinitionID;
					global::Kampai.Game.MignetteBuildingDefinition mignetteBuildingDefinition = definitionService.Get<global::Kampai.Game.MignetteBuildingDefinition>(buildingDefinitionID);
					string aspirationalMessage = mignetteBuildingDefinition.AspirationalMessage;
					globalSFXSignal.Dispatch("Play_action_locked_01");
					popupMessageSignal.Dispatch(localeService.GetString(aspirationalMessage));
				}
			}
		}

		private void OpenCabanaQuest(global::Kampai.Game.Quest q)
		{
			switch (q.state)
			{
			case global::Kampai.Game.QuestState.Notstarted:
			case global::Kampai.Game.QuestState.RunningStartScript:
			case global::Kampai.Game.QuestState.RunningTasks:
			case global::Kampai.Game.QuestState.RunningCompleteScript:
				showQuestPanel.Dispatch(q.ID);
				break;
			case global::Kampai.Game.QuestState.Harvestable:
				showQuestRewardSignal.Dispatch(q.ID);
				break;
			}
		}

		private void tryFun(global::Kampai.Game.Building endHitBuilding)
		{
			if (model.SelectedBuilding.HasValue || model.HeldTimer >= 0.5f)
			{
				return;
			}
			global::Kampai.Game.LeisureBuilding leisureBuilding = endHitBuilding as global::Kampai.Game.LeisureBuilding;
			if (leisureBuilding == null)
			{
				return;
			}
			if (leisureBuilding.State == global::Kampai.Game.BuildingState.Working)
			{
				killFunSignal.Dispatch(leisureBuilding.ID);
			}
			else
			{
				if (leisureBuilding.State != global::Kampai.Game.BuildingState.Idle)
				{
					return;
				}
				global::Kampai.Game.View.MinionManagerView component = minionManager.GetComponent<global::Kampai.Game.View.MinionManagerView>();
				global::System.Collections.Generic.Queue<int> minionListSortedByDistanceAndState = component.GetMinionListSortedByDistanceAndState(inputPosition);
				if (minionListSortedByDistanceAndState.Count < leisureBuilding.Definition.WorkStations)
				{
					showNeedXMinionsSignal.Dispatch(leisureBuilding.Definition.WorkStations - minionListSortedByDistanceAndState.Count);
					return;
				}
				globalSFXSignal.Dispatch("Play_minion_counter_down_01");
				globalSFXSignal.Dispatch("Play_minion_confirm_pathToBldg_01");
				int num = 0;
				while (minionListSortedByDistanceAndState.Count > 0 && num != leisureBuilding.Definition.WorkStations && num != leisureBuilding.Definition.WorkStations)
				{
					int second = minionListSortedByDistanceAndState.Dequeue();
					sendMinionToLeisureSignal.Dispatch(new global::Kampai.Util.Tuple<int, int, int>(leisureBuilding.ID, second, timeService.GameTimeSeconds()));
					num++;
				}
				deselectTaskedMinionsSignal.Dispatch();
			}
		}

		private void TrySelectBuilding(global::Kampai.Game.View.BuildingObject bo, int id)
		{
			if (model.SelectedBuilding.HasValue || model.DetectedMovement || !(model.HeldTimer >= 0.5f) || !(bo != null))
			{
				return;
			}
			global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(bo.ID);
			if (byInstanceId == null || !byInstanceId.Definition.Movable)
			{
				return;
			}
			global::Kampai.Game.StorageBuilding storageBuilding = byInstanceId as global::Kampai.Game.StorageBuilding;
			if (storageBuilding != null && !storageBuilding.IsBuildingRepaired())
			{
				return;
			}
			global::UnityEngine.Handheld.Vibrate();
			model.SelectedBuilding = id;
			model.CurrentMode = global::Kampai.Common.PickControllerModel.Mode.DragAndDrop;
			deselectMinionsSignal.Dispatch();
			dragAndDropSignal.Dispatch(1, inputPosition);
			global::Kampai.Game.BuildingDefinition definition = byInstanceId.Definition;
			if (byInstanceId != null && definition.Movable)
			{
				selectBuildingSignal.Dispatch(id, definitionService.GetBuildingFootprint(definition.FootprintID));
				deselectBuildingSignal.AddListener(DeselectBuilding);
				global::Kampai.Game.BuildingState state = byInstanceId.State;
				if (state == global::Kampai.Game.BuildingState.Working || state == global::Kampai.Game.BuildingState.Harvestable || state == global::Kampai.Game.BuildingState.HarvestableAndWorking)
				{
					uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.DisableMoveToInventorySignal>().Dispatch();
				}
			}
		}

		private void DeselectBuilding(int id)
		{
			if (model.SelectedBuilding == id)
			{
				model.SelectedBuilding = null;
				deselectBuildingSignal.RemoveListener(DeselectBuilding);
			}
		}

		private void TrySendMinions(global::Kampai.Game.View.BuildingObject buildingObj, global::Kampai.Game.Building building)
		{
			global::Kampai.Game.TaskableBuilding taskableBuilding = building as global::Kampai.Game.TaskableBuilding;
			if (model.SelectedMinions.Count == 0 || model.SelectedBuilding.HasValue || model.HeldTimer >= 0.5f || taskableBuilding == null || taskableBuilding.State == global::Kampai.Game.BuildingState.Harvestable || taskableBuilding.State == global::Kampai.Game.BuildingState.Cooldown || taskableBuilding is global::Kampai.Game.TikiBarBuilding)
			{
				return;
			}
			global::Kampai.Game.DebrisBuilding debrisBuilding = taskableBuilding as global::Kampai.Game.DebrisBuilding;
			if (debrisBuilding != null && !debrisBuilding.PaidInputCostToClear)
			{
				return;
			}
			global::Kampai.Game.MignetteBuilding mignetteBuilding = taskableBuilding as global::Kampai.Game.MignetteBuilding;
			if (mignetteBuilding != null && !TrySelectToFillTaskableBuilding(buildingObj, taskableBuilding))
			{
				showNeedXMinionsSignal.Dispatch(taskableBuilding.GetMinionSlotsOwned());
				return;
			}
			if (taskableBuilding.Definition.WorkStations > taskableBuilding.GetMinionsInBuilding())
			{
				globalSFXSignal.Dispatch("Play_minion_counter_down_01");
			}
			bool flag = true;
			foreach (int key in model.SelectedMinions.Keys)
			{
				if (flag)
				{
					globalSFXSignal.Dispatch("Play_minion_confirm_pathToBldg_01");
					flag = false;
				}
				startMinionTaskSignal.Dispatch(new global::Kampai.Util.Tuple<int, int, int>(taskableBuilding.ID, key, timeService.GameTimeSeconds()));
			}
			deselectTaskedMinionsSignal.Dispatch();
		}

		private bool TrySelectToFillTaskableBuilding(global::Kampai.Game.View.BuildingObject buildingObj, global::Kampai.Game.TaskableBuilding building)
		{
			int num = building.GetMinionSlotsOwned() - building.GetMinionsInBuilding();
			int count = model.SelectedMinions.Count;
			int num2 = num - count;
			if (num2 <= 0)
			{
				return true;
			}
			int num3 = playerService.GetMinionCount() - count;
			if (num2 > num3)
			{
				return false;
			}
			global::UnityEngine.Vector3 center = buildingObj.Center;
			global::System.Collections.Generic.Queue<int> minionListSortedByDistanceAndState = minionManager.GetComponent<global::Kampai.Game.View.MinionManagerView>().GetMinionListSortedByDistanceAndState(inputPosition);
			global::System.Collections.Generic.Queue<int> queue = new global::System.Collections.Generic.Queue<int>();
			while (minionListSortedByDistanceAndState.Count > 0 && num2 > 0)
			{
				int num4 = minionListSortedByDistanceAndState.Dequeue();
				global::Kampai.Game.Minion byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Minion>(num4);
				if (!model.SelectedMinions.ContainsKey(num4) && (byInstanceId.State == global::Kampai.Game.MinionState.Idle || byInstanceId.State == global::Kampai.Game.MinionState.Selectable || byInstanceId.State == global::Kampai.Game.MinionState.Leisure || byInstanceId.State == global::Kampai.Game.MinionState.Uninitialized))
				{
					queue.Enqueue(num4);
					num2--;
				}
			}
			global::Kampai.Util.Boxed<global::UnityEngine.Vector3> param = new global::Kampai.Util.Boxed<global::UnityEngine.Vector3>(center);
			bool flag = num2 == 0;
			if (flag)
			{
				while (queue.Count > 0)
				{
					selectMinionSignal.Dispatch(queue.Dequeue(), param, true);
				}
			}
			return flag;
		}
	}
}
