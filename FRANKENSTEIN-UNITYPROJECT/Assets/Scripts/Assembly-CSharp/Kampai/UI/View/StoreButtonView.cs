namespace Kampai.UI.View
{
	public class StoreButtonView : global::Kampai.UI.View.DoubleConfirmButtonView, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IDragHandler
	{
		public global::UnityEngine.UI.Text ItemName;

		public global::UnityEngine.UI.Text ItemDescription;

		public global::UnityEngine.UI.Text Capacity;

		public global::UnityEngine.UI.Text Cost;

		public global::Kampai.UI.View.StoreBadgeView ItemBadge;

		public global::Kampai.UI.View.KampaiImage ItemIcon;

		public global::Kampai.UI.View.KampaiImage MoneyIcon;

		public global::UnityEngine.UI.Text UnlockedAtLevel;

		public global::UnityEngine.GameObject Locked;

		public global::UnityEngine.GameObject Unlocked;

		public global::UnityEngine.GameObject Highlighted;

		public float PaddingInPixels;

		public global::UnityEngine.Transform BackingImage;

		public global::Kampai.UI.View.KampaiImage Arrow;

		public global::UnityEngine.Color LockedTopBackingColor;

		public new global::strange.extensions.signal.impl.Signal<global::Kampai.Game.Definition> ClickedSignal = new global::strange.extensions.signal.impl.Signal<global::Kampai.Game.Definition>();

		public global::strange.extensions.signal.impl.Signal BlockedSignal = new global::strange.extensions.signal.impl.Signal();

		public global::strange.extensions.signal.impl.Signal<global::UnityEngine.EventSystems.PointerEventData, global::Kampai.Game.Definition, global::Kampai.Game.Transaction.TransactionDefinition, bool> pointerDownSignal = new global::strange.extensions.signal.impl.Signal<global::UnityEngine.EventSystems.PointerEventData, global::Kampai.Game.Definition, global::Kampai.Game.Transaction.TransactionDefinition, bool>();

		public global::strange.extensions.signal.impl.Signal<global::UnityEngine.EventSystems.PointerEventData, global::Kampai.Game.Definition, global::Kampai.Game.Transaction.TransactionDefinition, int> pointerDragSignal = new global::strange.extensions.signal.impl.Signal<global::UnityEngine.EventSystems.PointerEventData, global::Kampai.Game.Definition, global::Kampai.Game.Transaction.TransactionDefinition, int>();

		public global::strange.extensions.signal.impl.Signal<global::UnityEngine.EventSystems.PointerEventData, global::Kampai.Game.Definition, global::Kampai.Game.Transaction.TransactionDefinition> pointerUpSignal = new global::strange.extensions.signal.impl.Signal<global::UnityEngine.EventSystems.PointerEventData, global::Kampai.Game.Definition, global::Kampai.Game.Transaction.TransactionDefinition>();

		private global::UnityEngine.Vector3 originalScale;

		private global::UnityEngine.Vector3 starOriginalScale;

		private int currentBadgeCount;

		private bool shouldBeRendered;

		private int currentCapacity;

		private int currentBuildingCount;

		private global::UnityEngine.Color defaultTopLeftBackingColor;

		private global::Kampai.UI.View.KampaiButton myButton;

		public global::Kampai.Game.Definition definition { get; set; }

		public global::Kampai.Game.Transaction.TransactionDefinition transactionDef { get; set; }

		public global::Kampai.Game.StoreItemDefinition storeItemDefinition { get; set; }

		public void init()
		{
			originalScale = Highlighted.transform.localScale;
			starOriginalScale = global::UnityEngine.Vector3.one;
			myButton = GetComponent<global::Kampai.UI.View.KampaiButton>();
			global::UnityEngine.UI.Image component = BackingImage.GetChild(0).GetComponent<global::UnityEngine.UI.Image>();
			if ((bool)component)
			{
				defaultTopLeftBackingColor = component.color;
			}
			else
			{
				defaultTopLeftBackingColor = global::UnityEngine.Color.gray;
			}
		}

		public void OnEnable()
		{
			ResetTapState();
		}

		public override void OnClickEvent()
		{
			ClickedSignal.Dispatch(definition);
		}

		public override void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (!isDoubleConfirmed())
			{
				base.OnClickEvent();
			}
			bool flag = isDoubleConfirmed();
			pointerDownSignal.Dispatch(eventData, definition, transactionDef, flag && currentBuildingCount < currentCapacity);
			if (flag)
			{
				ResetTapState();
			}
		}

		public void OnDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			pointerDragSignal.Dispatch(eventData, definition, transactionDef, currentBadgeCount);
		}

		public override void OnPointerUp(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			pointerUpSignal.Dispatch(eventData, definition, transactionDef);
		}

		internal void SetNewUnlockState(bool isNewThingUnlocked)
		{
			if (isNewThingUnlocked)
			{
				ItemBadge.SetNewUnlockCounter(1, false);
				global::Kampai.Util.TweenUtil.Throb(ItemBadge.transform, 0.85f, 0.5f, out starOriginalScale);
			}
			else
			{
				ItemBadge.HideNew();
				Go.killAllTweensWithTarget(ItemBadge.transform);
				ItemBadge.transform.localScale = starOriginalScale;
			}
		}

		internal void ChangeBuildingCount(bool isAdding)
		{
			Capacity.text = string.Format("{0}/{1}", (!isAdding) ? (--currentBuildingCount) : (++currentBuildingCount), currentCapacity);
			UpdateTextAndArrowColor();
		}

		internal void SetBuildingCount(int buildingCount)
		{
			currentBuildingCount = buildingCount;
			Capacity.text = string.Format("{0}/{1}", currentBuildingCount, currentCapacity);
			UpdateTextAndArrowColor();
		}

		internal void AdjustIncrementalCost()
		{
			global::Kampai.Game.BuildingDefinition buildingDefinition = definition as global::Kampai.Game.BuildingDefinition;
			if (buildingDefinition != null && buildingDefinition.IncrementalCost > 0)
			{
				Cost.text = (global::System.Convert.ToInt32(Cost.text) + buildingDefinition.IncrementalCost).ToString();
			}
		}

		internal void SetCapacity(int capacity)
		{
			currentCapacity = capacity;
			Capacity.text = string.Format("{0}/{1}", currentBuildingCount, currentCapacity);
			UpdateTextAndArrowColor();
		}

		private void UpdateTextAndArrowColor()
		{
			if (currentBuildingCount >= currentCapacity)
			{
				Capacity.color = global::Kampai.Util.GameConstants.UI.UI_TEXT_LIGHT_GREY;
				Arrow.color = global::Kampai.Util.GameConstants.UI.UI_TEXT_LIGHT_GREY;
			}
			else
			{
				Capacity.color = global::Kampai.Util.GameConstants.UI.UI_TEXT_LIGHT_BLUE;
				Arrow.color = global::Kampai.Util.GameConstants.UI.UI_TEXT_LIGHT_BLUE;
			}
		}

		internal void SetBadge(int badgeCount)
		{
			currentBadgeCount = badgeCount;
			ItemBadge.SetInventoryCount(badgeCount);
		}

		internal void SetHighlight(bool isHighlighted)
		{
			if (isHighlighted)
			{
				global::Kampai.Util.TweenUtil.Throb(Highlighted.transform, 0.85f, 0.5f, out originalScale);
				return;
			}
			Go.killAllTweensWithTarget(Highlighted.transform);
			Highlighted.transform.localScale = originalScale;
		}

		internal bool ChangeStateToUnlocked()
		{
			bool result = false;
			if (Locked.activeSelf)
			{
				SetNewUnlockState(true);
				Locked.SetActive(false);
				result = true;
			}
			Unlocked.SetActive(true);
			ChangeButtonBackingColor(defaultTopLeftBackingColor);
			myButton.interactable = true;
			SetButtonTeased(false);
			return result;
		}

		internal bool IsUnlocked()
		{
			return Unlocked.activeSelf;
		}

		internal void ChangeStateToLocked()
		{
			Unlocked.SetActive(false);
			Locked.SetActive(true);
			ChangeButtonBackingColor(LockedTopBackingColor);
			myButton.interactable = false;
			SetButtonTeased(false);
		}

		private void ChangeButtonBackingColor(global::UnityEngine.Color topLeftBackingColor)
		{
			for (int i = 0; i < BackingImage.childCount; i++)
			{
				global::UnityEngine.Transform child = BackingImage.GetChild(i);
				if (child != null)
				{
					child.GetComponent<global::UnityEngine.UI.Image>().color = topLeftBackingColor;
				}
			}
		}

		internal void SetButtonTeased(bool isTeased)
		{
			if (ItemIcon.gameObject.activeSelf != isTeased)
			{
				return;
			}
			ItemIcon.gameObject.SetActive(!isTeased);
			ItemName.gameObject.SetActive(!isTeased);
			ItemDescription.gameObject.SetActive(!isTeased);
			for (int i = 0; i < BackingImage.childCount; i++)
			{
				global::UnityEngine.Transform child = BackingImage.GetChild(i);
				if (child != null)
				{
					child.gameObject.SetActive(!isTeased);
				}
			}
		}

		public void SetShouldBerendered(bool value)
		{
			shouldBeRendered = value;
		}

		public bool ShouldBeRendered()
		{
			return shouldBeRendered;
		}
	}
}
