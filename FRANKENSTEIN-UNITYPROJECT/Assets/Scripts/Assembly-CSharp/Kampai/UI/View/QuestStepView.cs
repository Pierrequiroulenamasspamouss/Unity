namespace Kampai.UI.View
{
	public class QuestStepView : global::strange.extensions.mediation.impl.View
	{
		public global::UnityEngine.UI.Text taskNameText;

		public global::UnityEngine.UI.Text taskActionText;

		public global::Kampai.UI.View.KampaiClickableImage taskIconImage;

		public ScrollableButtonView taskStateButton;

		public global::UnityEngine.GameObject deliverPanel;

		public global::Kampai.UI.View.KampaiImage purchaseDeliverIconImage;

		public global::UnityEngine.UI.Text purchaseDeliverLeftText;

		public global::UnityEngine.RectTransform rushProgressPanel;

		public global::UnityEngine.UI.Text rushProgressText;

		public global::UnityEngine.RectTransform progressBar;

		public global::UnityEngine.UI.Image progressFill;

		public global::UnityEngine.UI.Text progressText;

		public ScrollableButtonView taskPurchaseButton;

		public ScrollableButtonView taskActionButton;

		public global::UnityEngine.UI.Image taskButtonCurrencyImage;

		public global::UnityEngine.UI.Text taskButtonCostText;

		public global::UnityEngine.UI.Text taskButtonCompleteText;

		public global::UnityEngine.UI.Text completeText;

		internal int stepNumber;

		internal int questInstanceID;

		internal global::Kampai.Game.QuestStep step;

		internal global::Kampai.Game.QuestStepDefinition stepDefinition;

		internal int constructionRushCost;

		internal int collectionRushCost;

		internal int collectionAmountNeeded;

		private global::Kampai.Game.IPlayerService playerService;

		private global::Kampai.Game.ITimeEventService timeEventService;

		private global::Kampai.Game.IDefinitionService definitionService;

		private global::Kampai.Main.ILocalizationService localService;

		private global::UnityEngine.Vector3 originalGoToScale;

		private global::UnityEngine.Vector3 originalDeliverScale;

		private bool hideTaskButton;

		internal void Init(global::Kampai.Game.IPlayerService playerService, global::Kampai.Game.ITimeEventService timeEventService, global::Kampai.Game.IDefinitionService definitionService, global::Kampai.Main.ILocalizationService localService)
		{
			this.playerService = playerService;
			this.timeEventService = timeEventService;
			this.definitionService = definitionService;
			this.localService = localService;
			rushProgressPanel.gameObject.SetActive(false);
			progressBar.gameObject.SetActive(true);
			taskNameText.text = QuestUtils.SetupTaskDescText(definitionService, localService, stepDefinition);
			SetupTaskDescIcon();
			completeText.gameObject.SetActive(false);
			originalGoToScale = taskStateButton.transform.localScale;
			originalDeliverScale = taskActionButton.transform.localScale;
			taskPurchaseButton.EnableDoubleConfirm();
		}

		internal void SetTaskButtonState(bool hideTaskButton)
		{
			this.hideTaskButton = hideTaskButton;
		}

		private void SetupTaskDescIcon()
		{
			global::Kampai.Game.BuildingDefinition buildingDefinition = null;
			switch (stepDefinition.Type)
			{
			case global::Kampai.Game.QuestStepType.Mignette:
				buildingDefinition = definitionService.Get<global::Kampai.Game.BuildingDefinition>(stepDefinition.ItemDefinitionID);
				taskIconImage.sprite = UIUtils.LoadSpriteFromPath(buildingDefinition.Image);
				taskIconImage.maskSprite = UIUtils.LoadSpriteFromPath(buildingDefinition.Mask);
				break;
			case global::Kampai.Game.QuestStepType.OrderBoard:
				buildingDefinition = definitionService.Get<global::Kampai.Game.BuildingDefinition>(3022);
				taskIconImage.sprite = UIUtils.LoadSpriteFromPath(buildingDefinition.Image);
				taskIconImage.maskSprite = UIUtils.LoadSpriteFromPath(buildingDefinition.Mask);
				break;
			case global::Kampai.Game.QuestStepType.Construction:
			case global::Kampai.Game.QuestStepType.StageRepair:
			case global::Kampai.Game.QuestStepType.CabanaRepair:
			case global::Kampai.Game.QuestStepType.WelcomeHutRepair:
			case global::Kampai.Game.QuestStepType.FountainRepair:
				buildingDefinition = definitionService.Get<global::Kampai.Game.BuildingDefinition>(stepDefinition.ItemDefinitionID);
				taskIconImage.sprite = UIUtils.LoadSpriteFromPath(buildingDefinition.Image);
				taskIconImage.maskSprite = UIUtils.LoadSpriteFromPath(buildingDefinition.Mask);
				break;
			case global::Kampai.Game.QuestStepType.Delivery:
			case global::Kampai.Game.QuestStepType.Harvest:
			{
				int buildingDefintionIDFromItemDefintionID = definitionService.GetBuildingDefintionIDFromItemDefintionID(stepDefinition.ItemDefinitionID);
				buildingDefinition = definitionService.Get<global::Kampai.Game.BuildingDefinition>(buildingDefintionIDFromItemDefintionID);
				taskIconImage.sprite = UIUtils.LoadSpriteFromPath(buildingDefinition.Image);
				taskIconImage.maskSprite = UIUtils.LoadSpriteFromPath(buildingDefinition.Mask);
				break;
			}
			case global::Kampai.Game.QuestStepType.BridgeRepair:
			{
				global::Kampai.Game.ItemDefinition itemDefinition = definitionService.Get<global::Kampai.Game.ItemDefinition>(stepDefinition.ItemDefinitionID);
				taskIconImage.sprite = UIUtils.LoadSpriteFromPath(itemDefinition.Image);
				taskIconImage.maskSprite = UIUtils.LoadSpriteFromPath(itemDefinition.Mask);
				break;
			}
			case global::Kampai.Game.QuestStepType.MinionTask:
				break;
			}
		}

		public void Update()
		{
			if (step.TrackedID != 0 && step.state == global::Kampai.Game.QuestStepState.Inprogress && stepDefinition.Type == global::Kampai.Game.QuestStepType.Construction && stepDefinition.ItemAmount == 1)
			{
				int timeRemaining = timeEventService.GetTimeRemaining(step.TrackedID);
				if (UpdateRushProgressBar(timeRemaining))
				{
					int eventDuration = timeEventService.GetEventDuration(step.TrackedID);
					int num = eventDuration - timeRemaining;
					UpdateProgressBar(num, eventDuration, true);
				}
			}
		}

		internal void SetupStepAction()
		{
			taskActionText.text = QuestUtils.SetupStepAction(localService, stepDefinition);
		}

		internal void SetStateNotStarted()
		{
			UpdateTaskStateButton(global::Kampai.Game.QuestStepState.Notstarted);
			switch (stepDefinition.Type)
			{
			case global::Kampai.Game.QuestStepType.BridgeRepair:
				UpdateDeliverButton(false);
				UpdateProgressBar(step.AmountCompleted, stepDefinition.ItemAmount);
				break;
			case global::Kampai.Game.QuestStepType.Construction:
				UpdateDeliverButton(false);
				UpdateProgressBar(step.AmountCompleted, stepDefinition.ItemAmount);
				break;
			case global::Kampai.Game.QuestStepType.Mignette:
				UpdateProgressBar(step.AmountCompleted, stepDefinition.ItemAmount);
				UpdateDeliverButton(false);
				break;
			case global::Kampai.Game.QuestStepType.Delivery:
				UpdateDeliverButton(true);
				UpdateProgressBar(step.AmountCompleted, stepDefinition.ItemAmount);
				break;
			case global::Kampai.Game.QuestStepType.OrderBoard:
				UpdateDeliverButton(false);
				UpdateProgressBar(step.AmountCompleted, stepDefinition.ItemAmount);
				break;
			case global::Kampai.Game.QuestStepType.MinionTask:
				UpdateDeliverButton(false);
				UpdateProgressBar(step.AmountCompleted, stepDefinition.ItemAmount);
				break;
			case global::Kampai.Game.QuestStepType.Harvest:
				UpdateDeliverButton(false);
				UpdateProgressBar(step.AmountCompleted, stepDefinition.ItemAmount);
				break;
			case global::Kampai.Game.QuestStepType.StageRepair:
			case global::Kampai.Game.QuestStepType.CabanaRepair:
			case global::Kampai.Game.QuestStepType.WelcomeHutRepair:
			case global::Kampai.Game.QuestStepType.FountainRepair:
				UpdateDeliverButton(false);
				progressBar.gameObject.SetActive(false);
				rushProgressPanel.gameObject.SetActive(false);
				break;
			}
		}

		internal void SetStateInProgress()
		{
			UpdateTaskStateButton(global::Kampai.Game.QuestStepState.Inprogress);
			switch (stepDefinition.Type)
			{
			case global::Kampai.Game.QuestStepType.Construction:
				UpdateDeliverButton(false);
				UpdateProgressBar(step.AmountCompleted, stepDefinition.ItemAmount);
				break;
			case global::Kampai.Game.QuestStepType.Mignette:
				UpdateProgressBar(step.AmountCompleted, stepDefinition.ItemAmount);
				UpdateDeliverButton(false);
				break;
			case global::Kampai.Game.QuestStepType.MinionTask:
				UpdateProgressBar(step.AmountCompleted, stepDefinition.ItemAmount);
				UpdateDeliverButton(false);
				break;
			case global::Kampai.Game.QuestStepType.Delivery:
				UpdateDeliverButton(true);
				UpdateProgressBar(step.AmountCompleted, stepDefinition.ItemAmount);
				break;
			case global::Kampai.Game.QuestStepType.BridgeRepair:
			case global::Kampai.Game.QuestStepType.StageRepair:
			case global::Kampai.Game.QuestStepType.WelcomeHutRepair:
			case global::Kampai.Game.QuestStepType.FountainRepair:
				UpdateDeliverButton(false);
				break;
			case global::Kampai.Game.QuestStepType.OrderBoard:
				UpdateDeliverButton(false);
				UpdateProgressBar(step.AmountCompleted, stepDefinition.ItemAmount);
				break;
			case global::Kampai.Game.QuestStepType.Harvest:
				UpdateProgressBar(step.AmountCompleted, stepDefinition.ItemAmount);
				UpdateDeliverButton(true);
				break;
			case global::Kampai.Game.QuestStepType.CabanaRepair:
				break;
			}
		}

		internal void SetStateReady(global::Kampai.Game.QuestStepType stepType)
		{
			UpdateTaskStateButton(global::Kampai.Game.QuestStepState.Ready, stepType);
			switch (stepDefinition.Type)
			{
			case global::Kampai.Game.QuestStepType.Construction:
				UpdateDeliverButton(false);
				completeText.text = localService.GetString("Ready");
				completeText.gameObject.SetActive(true);
				rushProgressPanel.gameObject.SetActive(false);
				UpdateProgressBar(step.AmountCompleted, stepDefinition.ItemAmount);
				break;
			case global::Kampai.Game.QuestStepType.Mignette:
				UpdateDeliverButton(false);
				UpdateProgressBar(step.AmountCompleted, stepDefinition.ItemAmount);
				break;
			case global::Kampai.Game.QuestStepType.Delivery:
				UpdateDeliverButton(true);
				UpdateProgressBar(step.AmountCompleted, stepDefinition.ItemAmount);
				break;
			case global::Kampai.Game.QuestStepType.BridgeRepair:
			case global::Kampai.Game.QuestStepType.StageRepair:
			case global::Kampai.Game.QuestStepType.WelcomeHutRepair:
			case global::Kampai.Game.QuestStepType.FountainRepair:
				UpdateDeliverButton(false);
				break;
			case global::Kampai.Game.QuestStepType.OrderBoard:
				UpdateDeliverButton(false);
				break;
			case global::Kampai.Game.QuestStepType.MinionTask:
				UpdateDeliverButton(false);
				UpdateProgressBar(step.AmountCompleted, stepDefinition.ItemAmount);
				break;
			case global::Kampai.Game.QuestStepType.Harvest:
				UpdateDeliverButton(false);
				UpdateProgressBar(step.AmountCompleted, stepDefinition.ItemAmount);
				break;
			case global::Kampai.Game.QuestStepType.CabanaRepair:
				break;
			}
		}

		internal void SetStateWaitComplete()
		{
			UpdateTaskStateButton(global::Kampai.Game.QuestStepState.WaitComplete);
			UpdateDeliverButton(false);
			UpdateTaskButton(false, true, 0, "Done");
			switch (stepDefinition.Type)
			{
			case global::Kampai.Game.QuestStepType.Construction:
				rushProgressPanel.gameObject.SetActive(false);
				UpdateProgressBar(stepDefinition.ItemAmount, stepDefinition.ItemAmount);
				break;
			case global::Kampai.Game.QuestStepType.Harvest:
				UpdateProgressBar(stepDefinition.ItemAmount, stepDefinition.ItemAmount);
				break;
			}
		}

		internal void SetStateComplete()
		{
			UpdateTaskStateButton(global::Kampai.Game.QuestStepState.Complete);
			UpdateDeliverButton(false);
			UpdateTaskButton(false, false);
			switch (stepDefinition.Type)
			{
			case global::Kampai.Game.QuestStepType.OrderBoard:
			case global::Kampai.Game.QuestStepType.BridgeRepair:
			case global::Kampai.Game.QuestStepType.MinionTask:
			case global::Kampai.Game.QuestStepType.Mignette:
			case global::Kampai.Game.QuestStepType.CabanaRepair:
				break;
			case global::Kampai.Game.QuestStepType.Construction:
			case global::Kampai.Game.QuestStepType.Delivery:
				rushProgressPanel.gameObject.SetActive(false);
				UpdateProgressBar(stepDefinition.ItemAmount, stepDefinition.ItemAmount);
				break;
			case global::Kampai.Game.QuestStepType.Harvest:
				UpdateProgressBar(step.AmountCompleted, stepDefinition.ItemAmount);
				break;
			case global::Kampai.Game.QuestStepType.StageRepair:
				break;
			}
		}

		private void EnableStateButton(bool enable)
		{
			taskStateButton.gameObject.SetActive(enable);
			taskIconImage.EnableClick(enable);
		}

		private void UpdateTaskStateButton(global::Kampai.Game.QuestStepState state, global::Kampai.Game.QuestStepType stepType = global::Kampai.Game.QuestStepType.Delivery)
		{
			EnableStateButton(true);
			switch (state)
			{
			case global::Kampai.Game.QuestStepState.Notstarted:
				EnableStateButton(true);
				break;
			case global::Kampai.Game.QuestStepState.Inprogress:
				break;
			case global::Kampai.Game.QuestStepState.Ready:
				if (stepType == global::Kampai.Game.QuestStepType.Delivery)
				{
					EnableStateButton(false);
				}
				break;
			case global::Kampai.Game.QuestStepState.WaitComplete:
				taskStateButton.gameObject.SetActive(false);
				break;
			case global::Kampai.Game.QuestStepState.Complete:
				EnableStateButton(false);
				completeText.text = localService.GetString("Complete");
				completeText.gameObject.SetActive(true);
				break;
			case global::Kampai.Game.QuestStepState.RunningStartScript:
			case global::Kampai.Game.QuestStepState.RunningCompleteScript:
				break;
			}
		}

		private void UpdateTaskButton(bool isPremium, bool show = true, int cost = 0, string locKey = null)
		{
			taskActionButton.gameObject.SetActive(false);
			taskPurchaseButton.gameObject.SetActive(false);
			if (show && !hideTaskButton)
			{
				if (!isPremium)
				{
					taskActionButton.gameObject.SetActive(true);
					taskButtonCompleteText.text = localService.GetString(locKey);
					taskButtonCompleteText.gameObject.SetActive(true);
					taskButtonCostText.gameObject.SetActive(false);
					taskButtonCurrencyImage.gameObject.SetActive(false);
				}
				else
				{
					taskPurchaseButton.gameObject.SetActive(true);
					taskButtonCostText.text = cost.ToString();
					taskButtonCurrencyImage.gameObject.SetActive(true);
					taskButtonCompleteText.gameObject.SetActive(false);
					taskButtonCostText.gameObject.SetActive(true);
				}
			}
		}

		private void UpdateDeliverButton(bool isActive)
		{
			deliverPanel.gameObject.SetActive(isActive);
			if (!isActive)
			{
				UpdateTaskButton(false, false);
				return;
			}
			global::Kampai.Game.ItemDefinition itemDefinition = definitionService.Get<global::Kampai.Game.ItemDefinition>(stepDefinition.ItemDefinitionID);
			uint quantityByDefinitionId = playerService.GetQuantityByDefinitionId(stepDefinition.ItemDefinitionID);
			step.AmountCompleted = (int)quantityByDefinitionId;
			int num = stepDefinition.ItemAmount - (int)quantityByDefinitionId;
			if (num <= 0)
			{
				if (stepDefinition.Type == global::Kampai.Game.QuestStepType.Delivery)
				{
					num = (int)quantityByDefinitionId;
					UpdateTaskButton(false, true, 0, "Deliver");
					purchaseDeliverLeftText.gameObject.SetActive(false);
				}
				else
				{
					deliverPanel.gameObject.SetActive(false);
				}
			}
			else
			{
				int num2 = global::UnityEngine.Mathf.FloorToInt((float)num * itemDefinition.BasePremiumCost);
				int cost = num2;
				UpdateTaskButton(true, true, cost);
				purchaseDeliverLeftText.text = string.Format("+{0}", num);
			}
			purchaseDeliverIconImage.sprite = UIUtils.LoadSpriteFromPath(itemDefinition.Image);
			purchaseDeliverIconImage.maskSprite = UIUtils.LoadSpriteFromPath(itemDefinition.Mask);
			CheckIfItemIsOneOffCraftable(itemDefinition);
		}

		private void CheckIfItemIsOneOffCraftable(global::Kampai.Game.ItemDefinition itemDef)
		{
			global::Kampai.Game.DynamicIngredientsDefinition dynamicIngredientsDefinition = itemDef as global::Kampai.Game.DynamicIngredientsDefinition;
			if (dynamicIngredientsDefinition != null)
			{
				global::Kampai.Game.CraftingBuilding firstInstanceByDefinitionId = playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.CraftingBuilding>(dynamicIngredientsDefinition.CraftingBuildingId);
				if (firstInstanceByDefinitionId != null && (firstInstanceByDefinitionId.RecipeInQueue.Contains(itemDef.ID) || firstInstanceByDefinitionId.CompletedCrafts.Contains(itemDef.ID)))
				{
					taskActionButton.gameObject.SetActive(false);
				}
			}
		}

		private bool UpdateRushProgressBar(int timeRemaining)
		{
			rushProgressPanel.gameObject.SetActive(true);
			if (timeRemaining < 0)
			{
				UpdateTaskButton(false, false);
				rushProgressText.text = UIUtils.FormatTime(0);
				return false;
			}
			rushProgressText.text = UIUtils.FormatTime(timeRemaining);
			constructionRushCost = timeEventService.CalculateRushCostForTimer(timeRemaining, global::Kampai.Game.RushActionType.CONSTRUCTION);
			int cost = constructionRushCost;
			UpdateTaskButton(true, true, cost);
			return true;
		}

		private void UpdateProgressBar(float progress, float complete, bool isTimer = false)
		{
			float num = global::UnityEngine.Mathf.Min(progress, complete);
			float num2 = num / complete;
			if (isTimer)
			{
				progressText.text = string.Format("{0}%", (int)(num2 * 100f));
			}
			else
			{
				progressText.text = localService.GetString("OfComplete", num, complete);
			}
			progressFill.rectTransform.anchorMax = new global::UnityEngine.Vector2(num2, 1f);
		}

		internal void HighlightGoTo(bool isHighlighted)
		{
			global::UnityEngine.Animator[] componentsInChildren = taskStateButton.GetComponentsInChildren<global::UnityEngine.Animator>();
			if (isHighlighted)
			{
				global::UnityEngine.Animator[] array = componentsInChildren;
				foreach (global::UnityEngine.Animator animator in array)
				{
					animator.enabled = false;
				}
				global::Kampai.Util.TweenUtil.Throb(taskStateButton.transform, 0.85f, 0.5f, out originalGoToScale);
				return;
			}
			Go.killAllTweensWithTarget(taskStateButton.transform);
			taskStateButton.transform.localScale = originalGoToScale;
			global::UnityEngine.Animator[] array2 = componentsInChildren;
			foreach (global::UnityEngine.Animator animator2 in array2)
			{
				animator2.enabled = true;
			}
		}

		internal void HighlightDeliver(bool isHighlighted)
		{
			global::UnityEngine.Animator[] componentsInChildren = taskActionButton.GetComponentsInChildren<global::UnityEngine.Animator>();
			if (isHighlighted)
			{
				global::UnityEngine.Animator[] array = componentsInChildren;
				foreach (global::UnityEngine.Animator animator in array)
				{
					animator.enabled = false;
				}
				global::Kampai.Util.TweenUtil.Throb(taskActionButton.transform, 0.85f, 0.5f, out originalDeliverScale);
				return;
			}
			Go.killAllTweensWithTarget(taskActionButton.transform);
			taskActionButton.transform.localScale = originalDeliverScale;
			global::UnityEngine.Animator[] array2 = componentsInChildren;
			foreach (global::UnityEngine.Animator animator2 in array2)
			{
				animator2.enabled = true;
			}
		}
	}
}
