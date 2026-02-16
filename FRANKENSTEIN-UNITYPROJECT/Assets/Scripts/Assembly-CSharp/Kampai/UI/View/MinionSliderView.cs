namespace Kampai.UI.View
{
	public class MinionSliderView : global::strange.extensions.mediation.impl.View
	{
		public global::UnityEngine.GameObject ClockPanel;

		public global::UnityEngine.GameObject AvailableMinionPanel;

		public global::UnityEngine.GameObject lockedPanel;

		public global::UnityEngine.GameObject lockedTranslucentOverlay;

		public global::UnityEngine.GameObject HarvestPanel;

		public global::UnityEngine.UI.Text durationText;

		public global::UnityEngine.UI.Text costText;

		public global::UnityEngine.UI.Text minionCount;

		public global::UnityEngine.UI.Text lockedText;

		public global::UnityEngine.UI.Text lockedCostText;

		public global::UnityEngine.UI.Text availableText;

		public global::Kampai.UI.View.KampaiImage buttonImage;

		public ScrollableButtonView rushButton;

		public ScrollableButtonView callButton;

		public UnlockableScrollableButtonView lockedButton;

		public ScrollableButtonView harvestButton;

		public global::Kampai.Game.ResourceBuilding building;

		public float PaddingInPixels;

		internal global::strange.extensions.signal.impl.Signal completeSignal = new global::strange.extensions.signal.impl.Signal();

		internal global::Kampai.Game.IPlayerService playerService;

		internal int identifier;

		internal int minionID;

		internal bool isLockedHighlighted;

		internal global::Kampai.UI.View.MinionSliderState state;

		public double startTime;

		private uint rushCost;

		private int harvestTime;

		private int count;

		private bool completed;

		private global::Kampai.Main.ILocalizationService localService;

		private global::Kampai.Game.IDefinitionService definitionService;

		private global::UnityEngine.Vector3 originalScale;

		private global::Kampai.UI.View.ModalSettings modalSettings;

		private global::Kampai.Util.IRoutineRunner routineRunner;

		[Inject]
		public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

		internal void Init(global::Kampai.Main.ILocalizationService localService, global::Kampai.Game.IDefinitionService definitionService, global::Kampai.Util.IRoutineRunner routineRunner)
		{
			this.localService = localService;
			this.definitionService = definitionService;
			this.routineRunner = routineRunner;
			UpdateHarvestTime();
			setMinionText();
			lockedButton.EnableDoubleConfirm();
		}

		internal void UpdateHarvestTime()
		{
			if (definitionService != null)
			{
				harvestTime = BuildingUtil.GetHarvestTimeForTaskableBuilding(building, definitionService);
			}
		}

		internal void UpdateLockedButton()
		{
			if ((identifier == 2 && building.MinionSlotsOwned < 2) || !modalSettings.enableLockedButtons)
			{
				lockedButton.gameObject.GetComponent<global::UnityEngine.UI.Button>().interactable = false;
				lockedTranslucentOverlay.SetActive(true);
			}
			else
			{
				lockedButton.gameObject.GetComponent<global::UnityEngine.UI.Button>().interactable = true;
				lockedTranslucentOverlay.SetActive(false);
			}
		}

		internal void SetIdleMinionCount()
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.Minion> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.Minion>();
			count = 0;
			foreach (global::Kampai.Game.Minion item in instancesByType)
			{
				if (item.State == global::Kampai.Game.MinionState.Idle || item.State == global::Kampai.Game.MinionState.Selectable || item.State == global::Kampai.Game.MinionState.Selected || item.State == global::Kampai.Game.MinionState.Leisure)
				{
					count++;
				}
			}
			minionCount.text = count.ToString();
			if (state == global::Kampai.UI.View.MinionSliderState.Harvestable)
			{
				harvestButton.GetComponent<global::UnityEngine.UI.Button>().interactable = modalSettings.enableHarvestButtons;
			}
			else
			{
				harvestButton.GetComponent<global::UnityEngine.UI.Button>().interactable = false;
			}
			if (state == global::Kampai.UI.View.MinionSliderState.Available)
			{
				if (count == 0)
				{
					callButton.GetComponent<global::UnityEngine.UI.Button>().interactable = false;
				}
				else
				{
					callButton.GetComponent<global::UnityEngine.UI.Button>().interactable = modalSettings.enableCallButtons;
				}
			}
		}

		public void Update()
		{
			if (minionID == -1)
			{
				return;
			}
			int timeRemaining = timeEventService.GetTimeRemaining(minionID);
			if (timeRemaining > harvestTime)
			{
				durationText.text = UIUtils.FormatTime(harvestTime);
				rushCost = (uint)timeEventService.CalculateRushCostForTimer(harvestTime, global::Kampai.Game.RushActionType.HARVESTING);
				costText.text = string.Format("{0}", rushCost);
				return;
			}
			if (timeRemaining == -1 && building.State != global::Kampai.Game.BuildingState.Working)
			{
				completed = true;
			}
			if (timeRemaining == 0 && !completed)
			{
				timeRemaining = harvestTime;
			}
			string text = UIUtils.FormatTime(timeRemaining);
			if (timeRemaining > -1 && !completed)
			{
				durationText.text = text;
				rushCost = (uint)timeEventService.CalculateRushCostForTimer(timeRemaining, global::Kampai.Game.RushActionType.HARVESTING);
				costText.text = string.Format("{0}", rushCost);
				if (timeRemaining <= 0)
				{
					completed = true;
				}
			}
			if (completed)
			{
				ClearSlot();
				completeSignal.Dispatch();
				completed = false;
			}
		}

		internal void ClearSlot()
		{
			costText.text = string.Empty;
			minionID = -1;
			SetMinionSliderState(global::Kampai.UI.View.MinionSliderState.Harvestable);
		}

		internal void CallMinion()
		{
			harvestTime = BuildingUtil.GetHarvestTimeForTaskableBuilding(building, definitionService);
			durationText.text = UIUtils.FormatTime(harvestTime);
			rushCost = (uint)timeEventService.CalculateRushCostForTimer(harvestTime, global::Kampai.Game.RushActionType.HARVESTING);
			costText.text = string.Format("{0}", rushCost);
			SetMinionSliderState(global::Kampai.UI.View.MinionSliderState.Working);
		}

		internal void ChangeMinionCount(bool increase)
		{
			if (increase)
			{
				count++;
			}
			else
			{
				count--;
			}
			minionCount.text = count.ToString();
		}

		internal void setMinionText()
		{
			availableText.text = localService.GetString("ResourceAvailable");
		}

		internal void PurchaseSlot()
		{
			SetMinionSliderState(global::Kampai.UI.View.MinionSliderState.Available);
			minionID = -1;
		}

		internal void SetMinionSliderState(global::Kampai.UI.View.MinionSliderState i_state)
		{
			state = i_state;
			switch (state)
			{
			case global::Kampai.UI.View.MinionSliderState.Available:
				SetSliderAvailable();
				break;
			case global::Kampai.UI.View.MinionSliderState.Locked:
				SetSliderLocked();
				break;
			case global::Kampai.UI.View.MinionSliderState.Harvestable:
				SetSliderHarvestable();
				break;
			case global::Kampai.UI.View.MinionSliderState.Working:
				SetSliderWorking();
				break;
			}
			SetIdleMinionCount();
		}

		private void SetSliderAvailable()
		{
			callButton.gameObject.SetActive(true);
			harvestButton.gameObject.SetActive(false);
			ResetAndHideRushButton();
			ClockPanel.SetActive(false);
			AvailableMinionPanel.SetActive(true);
			ResetAndHideLockedButton();
			HarvestPanel.SetActive(false);
			if (modalSettings.enableCallThrob)
			{
				SetCallHighlight(true);
			}
		}

		private void SetSliderLocked()
		{
			callButton.gameObject.SetActive(false);
			ResetAndHideRushButton();
			harvestButton.gameObject.SetActive(false);
			ClockPanel.SetActive(false);
			AvailableMinionPanel.SetActive(false);
			lockedPanel.SetActive(true);
			HarvestPanel.SetActive(false);
			if (modalSettings.enableLockedThrob && lockedButton.GetComponent<global::UnityEngine.UI.Button>().interactable)
			{
				SetLockedHighlight(true);
			}
		}

		private void SetSliderHarvestable()
		{
			harvestButton.gameObject.SetActive(true);
			callButton.gameObject.SetActive(false);
			ResetAndHideRushButton();
			ClockPanel.SetActive(false);
			AvailableMinionPanel.SetActive(false);
			ResetAndHideLockedButton();
			HarvestPanel.SetActive(true);
			harvestButton.GetComponent<global::UnityEngine.UI.Button>().interactable = modalSettings.enableHarvestButtons;
		}

		private void SetSliderWorking()
		{
			harvestButton.gameObject.SetActive(false);
			callButton.gameObject.SetActive(false);
			rushButton.gameObject.SetActive(true);
			rushButton.EnableDoubleConfirm();
			ClockPanel.SetActive(true);
			AvailableMinionPanel.SetActive(false);
			ResetAndHideLockedButton();
			HarvestPanel.SetActive(false);
			rushButton.GetComponent<global::UnityEngine.UI.Button>().interactable = modalSettings.enableRushButtons;
			if (modalSettings.enableRushThrob)
			{
				SetRushHighlight(true);
			}
		}

		private void ResetAndHideRushButton()
		{
			rushButton.ResetAnim();
			routineRunner.StartCoroutine(WaitAFrame(rushButton.gameObject));
		}

		private void ResetAndHideLockedButton()
		{
			lockedButton.ResetAnim();
			routineRunner.StartCoroutine(WaitAFrame(lockedPanel));
		}

		private global::System.Collections.IEnumerator WaitAFrame(global::UnityEngine.GameObject go)
		{
			yield return new global::UnityEngine.WaitForEndOfFrame();
			if (go != null)
			{
				go.SetActive(false);
			}
		}

		internal void SetRushHighlight(bool isHighlighted)
		{
			if (!rushButton.enabled)
			{
				return;
			}
			isLockedHighlighted = true;
			global::UnityEngine.Animator[] componentsInChildren = rushButton.GetComponentsInChildren<global::UnityEngine.Animator>();
			if (isHighlighted)
			{
				global::UnityEngine.Animator[] array = componentsInChildren;
				foreach (global::UnityEngine.Animator animator in array)
				{
					animator.enabled = false;
				}
				global::Kampai.Util.TweenUtil.Throb(rushButton.transform, 0.85f, 0.5f, out originalScale);
				return;
			}
			isLockedHighlighted = false;
			Go.killAllTweensWithTarget(rushButton.transform);
			rushButton.transform.localScale = originalScale;
			global::UnityEngine.Animator[] array2 = componentsInChildren;
			foreach (global::UnityEngine.Animator animator2 in array2)
			{
				animator2.enabled = true;
			}
		}

		internal void SetCallHighlight(bool isHighlighted)
		{
			if (!callButton.enabled)
			{
				return;
			}
			isLockedHighlighted = true;
			global::UnityEngine.Animator[] componentsInChildren = callButton.GetComponentsInChildren<global::UnityEngine.Animator>();
			if (isHighlighted)
			{
				global::UnityEngine.Animator[] array = componentsInChildren;
				foreach (global::UnityEngine.Animator animator in array)
				{
					animator.enabled = false;
				}
				global::Kampai.Util.TweenUtil.Throb(callButton.transform, 0.85f, 0.5f, out originalScale);
				return;
			}
			isLockedHighlighted = false;
			Go.killAllTweensWithTarget(callButton.transform);
			callButton.transform.localScale = originalScale;
			global::UnityEngine.Animator[] array2 = componentsInChildren;
			foreach (global::UnityEngine.Animator animator2 in array2)
			{
				animator2.enabled = true;
			}
		}

		internal void SetLockedHighlight(bool isHighlighted)
		{
			if (!lockedButton.enabled)
			{
				return;
			}
			isLockedHighlighted = true;
			global::UnityEngine.Animator[] componentsInChildren = lockedButton.GetComponentsInChildren<global::UnityEngine.Animator>();
			if (isHighlighted)
			{
				global::UnityEngine.Animator[] array = componentsInChildren;
				foreach (global::UnityEngine.Animator animator in array)
				{
					animator.enabled = false;
				}
				global::Kampai.Util.TweenUtil.Throb(lockedButton.transform, 0.85f, 0.5f, out originalScale);
				return;
			}
			isLockedHighlighted = false;
			Go.killAllTweensWithTarget(lockedButton.transform);
			lockedButton.transform.localScale = originalScale;
			global::UnityEngine.Animator[] array2 = componentsInChildren;
			foreach (global::UnityEngine.Animator animator2 in array2)
			{
				animator2.enabled = true;
			}
		}

		internal uint GetRushCost()
		{
			return rushCost;
		}

		internal void SetRushCost()
		{
			rushCost = (uint)building.Definition.RushCost;
		}

		internal int GetPurchaseCost()
		{
			return building.GetSlotCostByIndex(identifier);
		}

		internal void SetModalSettings(global::Kampai.UI.View.ModalSettings modalSettings)
		{
			this.modalSettings = modalSettings;
		}
	}
}
