namespace Kampai.UI.View
{
	public class OrderBoardTicketDetailView : global::strange.extensions.mediation.impl.View
	{
		private const string requiredItemPrefabPath = "cmp_OrderBoardTicketRequiredItem";

		public global::UnityEngine.UI.Text TicketName;

		public global::UnityEngine.UI.Text OrderInstruction;

		public global::UnityEngine.RectTransform ScrollWindow;

		public global::UnityEngine.RectTransform ScrollView;

		public global::UnityEngine.GameObject RequiredItemListBackground;

		public global::UnityEngine.RectTransform RewardPanel;

		public global::UnityEngine.GameObject OrderPanel;

		public global::UnityEngine.GameObject PrestigePanel;

		public global::UnityEngine.GameObject GlowAnimation;

		public global::UnityEngine.RectTransform PrestigeProgressBarFill;

		public global::UnityEngine.UI.Text ProgressBarText;

		public global::UnityEngine.UI.Text XPReward;

		public global::UnityEngine.UI.Text GrindReward;

		public global::UnityEngine.UI.Text PrestigeLevel;

		public global::Kampai.UI.View.MinionSlotModal MinionSlot;

		public global::Kampai.UI.View.KampaiImage FTUEBacking;

		private global::UnityEngine.GameObject requiredItemPrefab;

		private global::Kampai.Game.View.DummyCharacterObject dummyCharacterObject;

		private float height;

		private global::System.Collections.Generic.List<global::Kampai.UI.View.OrderBoardRequiredItemView> itemList;

		private int count;

		private global::Kampai.Main.ILocalizationService localizationService;

		public void Init(global::Kampai.Main.ILocalizationService localService)
		{
			localizationService = localService;
			requiredItemPrefab = global::Kampai.Util.KampaiResources.Load("cmp_OrderBoardTicketRequiredItem") as global::UnityEngine.GameObject;
			global::UnityEngine.RectTransform rectTransform = requiredItemPrefab.transform as global::UnityEngine.RectTransform;
			height = rectTransform.sizeDelta.y;
			itemList = new global::System.Collections.Generic.List<global::Kampai.UI.View.OrderBoardRequiredItemView>();
			dummyCharacterObject = null;
		}

		internal void ClearDummyObject()
		{
			if (dummyCharacterObject != null)
			{
				dummyCharacterObject.RemoveCoroutine();
				global::UnityEngine.Object.Destroy(dummyCharacterObject.gameObject);
				dummyCharacterObject = null;
			}
		}

		internal global::System.Collections.Generic.List<global::Kampai.UI.View.OrderBoardRequiredItemView> GetItemList()
		{
			return itemList;
		}

		internal void SetFTUEText(string title)
		{
			PrestigePanel.SetActive(false);
			RewardPanel.gameObject.SetActive(false);
			ScrollWindow.gameObject.SetActive(false);
			RequiredItemListBackground.SetActive(false);
			OrderInstruction.text = title;
			OrderInstruction.gameObject.SetActive(true);
			TicketName.gameObject.SetActive(false);
			FTUEBacking.gameObject.SetActive(false);
		}

		internal void SetSlotFullText(string message)
		{
			ScrollWindow.gameObject.SetActive(false);
			RewardPanel.gameObject.SetActive(false);
			OrderInstruction.text = message;
			OrderInstruction.gameObject.SetActive(true);
		}

		internal void SetTitle(string title)
		{
			TicketName.text = title;
		}

		internal void SetPrestigeProgress(float currentPrestigePoint, int neededPrestigePoints)
		{
			float x = currentPrestigePoint / (float)neededPrestigePoints;
			PrestigeProgressBarFill.anchorMax = new global::UnityEngine.Vector2(x, 1f);
			ProgressBarText.text = localizationService.GetString("PrestigeProgress", global::UnityEngine.Mathf.FloorToInt(currentPrestigePoint), neededPrestigePoints);
		}

		internal void SetReward(int grind, int xp)
		{
			XPReward.text = xp.ToString();
			GrindReward.text = grind.ToString();
		}

		internal void SetCharacter(global::Kampai.Game.View.DummyCharacterObject characterObject)
		{
			if (dummyCharacterObject != null)
			{
				dummyCharacterObject.RemoveCoroutine();
				global::UnityEngine.Object.Destroy(dummyCharacterObject.gameObject);
			}
			dummyCharacterObject = characterObject;
		}

		internal void SetPanelState(bool isPrestige, int prestigeLevel = 0, global::Kampai.Game.Prestige character = null)
		{
			OrderInstruction.gameObject.SetActive(false);
			TicketName.gameObject.SetActive(true);
			FTUEBacking.gameObject.SetActive(true);
			if (isPrestige)
			{
				string title;
				if (prestigeLevel > 0)
				{
					title = ((character == null) ? localizationService.GetString("RePrestigeText") : localizationService.GetString("RePrestigeText", localizationService.GetString(character.Definition.LocalizedKey)));
					PrestigeLevel.text = (prestigeLevel + 1).ToString();
				}
				else
				{
					title = ((character == null) ? localizationService.GetString("PrestigeText") : localizationService.GetString("PrestigeText", localizationService.GetString(character.Definition.LocalizedKey)));
					PrestigeLevel.text = (prestigeLevel + 1).ToString();
				}
				SetTitle(title);
			}
			OrderPanel.SetActive(!isPrestige);
			PrestigePanel.SetActive(isPrestige);
			ScrollWindow.gameObject.SetActive(true);
			RewardPanel.gameObject.SetActive(true);
			RequiredItemListBackground.SetActive(true);
		}

		internal global::Kampai.UI.View.OrderBoardRequiredItemView CreateRequiredItem(int index, uint itemQuantity, uint itemInInventory, global::UnityEngine.Sprite icon, global::UnityEngine.Sprite mask)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(requiredItemPrefab) as global::UnityEngine.GameObject;
			global::Kampai.UI.View.OrderBoardRequiredItemView component = gameObject.GetComponent<global::Kampai.UI.View.OrderBoardRequiredItemView>();
			component.transform.SetParent(ScrollWindow, false);
			int num = index / 2;
			int num2 = index % 2;
			global::UnityEngine.RectTransform rectTransform = component.transform as global::UnityEngine.RectTransform;
			rectTransform.anchorMin = new global::UnityEngine.Vector2(0.5f * (float)num2, 0f);
			rectTransform.anchorMax = new global::UnityEngine.Vector2(0.5f * (float)(num2 + 1), 0f);
			rectTransform.offsetMin = new global::UnityEngine.Vector2(0f, height * (float)count - height * (float)(num + 1));
			rectTransform.offsetMax = new global::UnityEngine.Vector2(0f, height * (float)count - height * (float)num);
			rectTransform.localPosition = new global::UnityEngine.Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, 0f);
			rectTransform.localScale = global::UnityEngine.Vector3.one;
			bool flag = itemQuantity <= itemInInventory;
			component.CheckMark.SetActive(flag);
			component.XMark.SetActive(!flag);
			component.ItemCount.text = string.Format("{0}/{1}", itemInInventory, itemQuantity);
			component.ItemIcon.sprite = icon;
			component.ItemIcon.maskSprite = mask;
			itemList.Add(component);
			return component;
		}

		internal void SetupItemCount(int count)
		{
			if (itemList.Count != 0)
			{
				foreach (global::Kampai.UI.View.OrderBoardRequiredItemView item in itemList)
				{
					global::UnityEngine.Object.Destroy(item.gameObject);
				}
				itemList.Clear();
			}
			this.count = count;
			ScrollWindow.offsetMin = new global::UnityEngine.Vector2(0f, (0f - height) * (float)count);
			ScrollWindow.offsetMax = new global::UnityEngine.Vector2(0f, 0f);
		}
	}
}
