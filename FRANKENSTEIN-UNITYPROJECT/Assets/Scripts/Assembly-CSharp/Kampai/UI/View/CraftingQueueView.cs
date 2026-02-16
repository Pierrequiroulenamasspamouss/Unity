namespace Kampai.UI.View
{
	public class CraftingQueueView : global::strange.extensions.mediation.impl.View, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IPointerEnterHandler, global::UnityEngine.EventSystems.IPointerExitHandler
	{
		public global::UnityEngine.RectTransform inProgressPanel;

		public global::UnityEngine.RectTransform availablePanel;

		public global::UnityEngine.RectTransform lockedPanel;

		public global::Kampai.UI.View.KampaiImage inProgressImage;

		public global::UnityEngine.UI.Text inProgressTime;

		public global::UnityEngine.UI.Text inProgressCost;

		public ScrollableButtonView inProgressRush;

		public global::Kampai.UI.View.KampaiImage availableImage;

		public global::UnityEngine.UI.Text availableText;

		public global::UnityEngine.UI.Text lockedCost;

		public ScrollableButtonView lockedPurchase;

		internal bool isLocked;

		internal global::Kampai.Game.IngredientsItemDefinition itemDef;

		internal bool inProduction;

		private global::Kampai.Game.ITimeEventService timeEventService;

		internal global::strange.extensions.signal.impl.Signal<global::UnityEngine.EventSystems.PointerEventData> onPointerEnterSignal = new global::strange.extensions.signal.impl.Signal<global::UnityEngine.EventSystems.PointerEventData>();

		internal global::strange.extensions.signal.impl.Signal<global::UnityEngine.EventSystems.PointerEventData> onPointerExitSignal = new global::strange.extensions.signal.impl.Signal<global::UnityEngine.EventSystems.PointerEventData>();

		public global::Kampai.Game.CraftingBuilding building { get; set; }

		public int index { get; set; }

		public int purchaseCost { get; set; }

		public int rushCost { get; set; }

		internal void Init(global::Kampai.Game.IDefinitionService definitionService, global::Kampai.Game.ITimeEventService timeEventService)
		{
			this.timeEventService = timeEventService;
			lockedPurchase.EnableDoubleConfirm();
			if (index >= building.RecipeInQueue.Count)
			{
				return;
			}
			itemDef = definitionService.Get<global::Kampai.Game.IngredientsItemDefinition>(building.RecipeInQueue[index]);
			if (index == 0)
			{
				inProduction = true;
				inProgressPanel.gameObject.SetActive(true);
				inProgressRush.ResetTapState();
				inProgressRush.EnableDoubleConfirm();
				availablePanel.gameObject.SetActive(false);
				lockedPanel.gameObject.SetActive(false);
				inProgressImage.sprite = UIUtils.LoadSpriteFromPath(itemDef.Image);
				inProgressImage.maskSprite = UIUtils.LoadSpriteFromPath(itemDef.Mask);
				Update();
			}
			else
			{
				inProgressPanel.gameObject.SetActive(false);
				availablePanel.gameObject.SetActive(true);
				lockedPanel.gameObject.SetActive(false);
				if (itemDef != null)
				{
					availableText.gameObject.SetActive(false);
					availableImage.gameObject.SetActive(true);
					availableImage.sprite = UIUtils.LoadSpriteFromPath(itemDef.Image);
					availableImage.maskSprite = UIUtils.LoadSpriteFromPath(itemDef.Mask);
				}
			}
		}

		public void Update()
		{
			if (index == 0 && inProduction)
			{
				int timeRemaining = timeEventService.GetTimeRemaining(building.ID);
				inProgressTime.text = UIUtils.FormatTime(timeRemaining);
				rushCost = timeEventService.CalculateRushCostForTimer(timeRemaining, global::Kampai.Game.RushActionType.CRAFTING);
				inProgressCost.text = rushCost.ToString();
			}
		}

		public void OnPointerEnter(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			onPointerEnterSignal.Dispatch(eventData);
		}

		public void OnPointerExit(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			onPointerExitSignal.Dispatch(eventData);
		}
	}
}
