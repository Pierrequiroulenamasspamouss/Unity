namespace Kampai.UI.View
{
	public class QuestStepMediator : global::strange.extensions.mediation.impl.Mediator
	{
		public const float PLACEMENT_ZOOM_LEVEL = 0.4f;

		private global::Kampai.Game.Quest quest;

		[Inject]
		public global::Kampai.UI.View.QuestStepView view { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.UI.View.CloseQuestBookSignal closeSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSkrim { get; set; }

		[Inject]
		public global::Kampai.Game.QuestStepRushSignal stepRushSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal globalSFXSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoMoveToBuildingSignal moveToBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoMoveToMignetteSignal moveToMignetteSignal { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.UI.View.OpenStoreHighlightItemSignal openStoreSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateQuestPanelSignal updateQuestPanelSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.FTUEProgressSignal ftueSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.FTUEQuestGoToSignal ftueGoToSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.FTUEQuestFinished ftueQuestFinished { get; set; }

		[Inject]
		public global::Kampai.Game.DeliverTaskItemSignal deliverTaskItemSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IZoomCameraModel zoomCameraModel { get; set; }

		[Inject]
		public global::Kampai.UI.View.CraftingModalParams craftingModalParams { get; set; }

		[Inject]
		public global::Kampai.UI.View.RushRevealBuildingSignal rushRevealBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ConstructionCompleteSignal constructionCompleteSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoZoomSignal autoZoomSignal { get; set; }

		[Inject(global::Kampai.Main.MainElement.CAMERA)]
		public global::UnityEngine.Camera myCamera { get; set; }

		public override void OnRegister()
		{
			view.Init(playerService, timeEventService, definitionService, localService);
			view.taskPurchaseButton.ClickedSignal.AddListener(TaskButtonClicked);
			view.taskActionButton.ClickedSignal.AddListener(TaskButtonClicked);
			view.taskStateButton.ClickedSignal.AddListener(GoToClicked);
			view.taskIconImage.ClickedSignal.AddListener(GoToClicked);
			updateQuestPanelSignal.AddListener(UpdateQuestState);
			constructionCompleteSignal.AddListener(ConstructionComplete);
			Init();
		}

		public override void OnRemove()
		{
			view.taskActionButton.ClickedSignal.RemoveListener(TaskButtonClicked);
			view.taskPurchaseButton.ClickedSignal.RemoveListener(TaskButtonClicked);
			view.taskStateButton.ClickedSignal.RemoveListener(GoToClicked);
			view.taskIconImage.ClickedSignal.RemoveListener(GoToClicked);
			updateQuestPanelSignal.RemoveListener(UpdateQuestState);
			constructionCompleteSignal.RemoveListener(ConstructionComplete);
		}

		private void Init()
		{
			quest = playerService.GetByInstanceId<global::Kampai.Game.Quest>(view.questInstanceID);
			UpdateQuestState(quest, view.stepNumber);
			view.SetupStepAction();
		}

		private void ConstructionComplete(int instanceId)
		{
			if (view.step != null && view.stepDefinition != null && view.step.TrackedID == instanceId && view.stepDefinition.Type == global::Kampai.Game.QuestStepType.Construction)
			{
				view.SetStateReady(view.stepDefinition.Type);
			}
		}

		private void UpdateQuestState(global::Kampai.Game.Quest quest, int step)
		{
			if (view.step == quest.Steps[step])
			{
				switch (quest.Steps[step].state)
				{
				case global::Kampai.Game.QuestStepState.Notstarted:
					view.SetStateNotStarted();
					break;
				case global::Kampai.Game.QuestStepState.Inprogress:
					view.SetStateInProgress();
					break;
				case global::Kampai.Game.QuestStepState.Ready:
					view.SetStateReady(view.stepDefinition.Type);
					break;
				case global::Kampai.Game.QuestStepState.WaitComplete:
					view.SetStateWaitComplete();
					break;
				case global::Kampai.Game.QuestStepState.Complete:
					CheckPlayAudio(step);
					view.SetStateComplete();
					break;
				case global::Kampai.Game.QuestStepState.RunningStartScript:
				case global::Kampai.Game.QuestStepState.RunningCompleteScript:
					break;
				}
			}
		}

		private void CheckPlayAudio(int currentStep)
		{
			bool flag = true;
			foreach (global::Kampai.Game.QuestStep step in quest.Steps)
			{
				if (view.step != quest.Steps[currentStep] && step.state != global::Kampai.Game.QuestStepState.Complete)
				{
					flag = false;
					break;
				}
			}
			if (!flag)
			{
				globalSFXSignal.Dispatch("Play_completePartQuest_01");
			}
		}

		private void GoToClicked()
		{
			ftueGoToSignal.Dispatch();
			switch (view.stepDefinition.Type)
			{
			case global::Kampai.Game.QuestStepType.Mignette:
				HandleMignette();
				break;
			case global::Kampai.Game.QuestStepType.Construction:
			case global::Kampai.Game.QuestStepType.BridgeRepair:
			case global::Kampai.Game.QuestStepType.CabanaRepair:
			case global::Kampai.Game.QuestStepType.WelcomeHutRepair:
			case global::Kampai.Game.QuestStepType.FountainRepair:
				HandleBridgeAndConstruction();
				break;
			case global::Kampai.Game.QuestStepType.Delivery:
			case global::Kampai.Game.QuestStepType.MinionTask:
			case global::Kampai.Game.QuestStepType.Harvest:
				HandleDeliveryAndMinionTask();
				break;
			case global::Kampai.Game.QuestStepType.OrderBoard:
				HandleOrderBoard();
				break;
			case global::Kampai.Game.QuestStepType.StageRepair:
				HandleStage();
				break;
			}
			closeSignal.Dispatch();
		}

		private void PanOutTikiBar(global::System.Action onComplete = null)
		{
			if (zoomCameraModel.ZoomedIn)
			{
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.BuildingZoomSignal>().Dispatch(new global::Kampai.Game.BuildingZoomSettings(global::Kampai.Game.ZoomType.OUT, zoomCameraModel.LastZoomBuildingType, onComplete));
			}
			else if (onComplete != null)
			{
				onComplete();
			}
		}

		private void GotoBuilding(int questId, global::Kampai.Game.Building building, bool openModal = false)
		{
			if (questId == 32910 || questId == 2000000001)
			{
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.CameraAutoMoveToBuildingSignal>().Dispatch(building, new global::Kampai.Game.PanInstructions(building));
			}
			else if (!openModal)
			{
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.CameraAutoMoveToBuildingSignal>().Dispatch(building, new global::Kampai.Game.PanInstructions(building));
			}
			else
			{
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.PanAndOpenModalSignal>().Dispatch(building.ID);
			}
		}

		private void ZoomOutToPlacementLevel()
		{
			float currentPercentage = myCamera.GetComponent<global::Kampai.Game.View.ZoomView>().GetCurrentPercentage();
			if (currentPercentage > 0.4f)
			{
				autoZoomSignal.Dispatch(0.4f);
			}
		}

		private void HandleMignette()
		{
			PanOutTikiBar(delegate
			{
				moveToMignetteSignal.Dispatch(view.step.TrackedID, new global::Kampai.Game.PanInstructions(null));
			});
		}

		private void HandleBridgeAndConstruction()
		{
			PanOutTikiBar(delegate
			{
				if (view.step.TrackedID == 0)
				{
					openStoreSignal.Dispatch(view.stepDefinition.ItemDefinitionID, true);
					ZoomOutToPlacementLevel();
				}
				else
				{
					global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(view.step.TrackedID);
					moveToBuildingSignal.Dispatch(byInstanceId, new global::Kampai.Game.PanInstructions(byInstanceId));
				}
			});
		}

		private void HandleStage()
		{
			PanOutTikiBar(delegate
			{
				global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(view.step.TrackedID);
				moveToBuildingSignal.Dispatch(byInstanceId, new global::Kampai.Game.PanInstructions(byInstanceId));
			});
		}

		private void HandleDeliveryAndMinionTask()
		{
			global::System.Collections.Generic.List<global::Kampai.Game.Building> list = new global::System.Collections.Generic.List<global::Kampai.Game.Building>();
			foreach (global::Kampai.Game.Building item in playerService.GetByDefinitionId<global::Kampai.Game.Building>(view.step.TrackedID))
			{
				if (item.State != global::Kampai.Game.BuildingState.Inventory)
				{
					list.Add(item);
				}
			}
			if (list.Count > 0)
			{
				global::Kampai.Game.Building building = list[global::UnityEngine.Random.Range(0, list.Count - 1)];
				if (building.State == global::Kampai.Game.BuildingState.Inactive || building.State == global::Kampai.Game.BuildingState.Complete)
				{
					PanOutTikiBar(delegate
					{
						GotoBuilding(quest.Definition.ID, building);
					});
				}
				else
				{
					PanOutTikiBar(delegate
					{
						GotoBuilding(quest.Definition.ID, building, true);
					});
				}
				if (building.Definition.Type == BuildingType.BuildingTypeIdentifier.CRAFTING)
				{
					craftingModalParams.itemId = view.stepDefinition.ItemDefinitionID;
				}
			}
			else
			{
				PanOutTikiBar();
				openStoreSignal.Dispatch(view.step.TrackedID, true);
				ZoomOutToPlacementLevel();
			}
		}

		private void HandleOrderBoard()
		{
			global::Kampai.Game.OrderBoard orderBoard = playerService.GetByInstanceId<global::Kampai.Game.OrderBoard>(309);
			PanOutTikiBar(delegate
			{
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.OpenOrderBoardSignal>().Dispatch(orderBoard);
			});
		}

		private void ConstructionTransactionCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			if (pct.Success)
			{
				globalSFXSignal.Dispatch("Play_button_premium_01");
				timeEventService.RushEvent(view.step.TrackedID);
				stepRushSignal.Dispatch(quest, view.stepNumber);
				UpdateQuestState(quest, view.stepNumber);
				rushRevealBuildingSignal.Dispatch(view.step.TrackedID);
			}
		}

		private void TaskButtonClicked()
		{
			if (!view.taskPurchaseButton.gameObject.activeSelf || view.taskPurchaseButton.isDoubleConfirmed())
			{
				if (view.stepDefinition.Type == global::Kampai.Game.QuestStepType.Construction && view.step.state == global::Kampai.Game.QuestStepState.Inprogress)
				{
					playerService.ProcessRush(view.constructionRushCost, true, ConstructionTransactionCallback, view.stepDefinition.ItemDefinitionID);
					return;
				}
				deliverTaskItemSignal.Dispatch(quest, view.stepNumber);
				UpdateQuestState(quest, view.stepNumber);
				ftueQuestFinished.Dispatch();
			}
		}
	}
}
