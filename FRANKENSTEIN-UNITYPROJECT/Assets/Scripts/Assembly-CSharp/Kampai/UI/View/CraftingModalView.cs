namespace Kampai.UI.View
{
	public class CraftingModalView : global::Kampai.UI.View.PopupMenuView
	{
		public global::UnityEngine.UI.Text title;

		public global::Kampai.UI.View.ButtonView backArrow;

		public global::Kampai.UI.View.ButtonView forwardArrow;

		public global::System.Collections.Generic.List<global::UnityEngine.RectTransform> recipeLocations;

		public global::UnityEngine.RectTransform oneOffCraftableLocation;

		public global::UnityEngine.RectTransform queueScrollView;

		private global::Kampai.Game.IDefinitionService definitionService;

		private global::Kampai.Game.IQuestService questService;

		internal global::Kampai.Game.CraftingBuilding building;

		private global::System.Collections.Generic.List<global::UnityEngine.GameObject> recipeIcons = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();

		private global::System.Collections.Generic.List<global::Kampai.UI.View.CraftingQueueView> queueIcons = new global::System.Collections.Generic.List<global::Kampai.UI.View.CraftingQueueView>();

		private float queueWidth;

		private float queuePadding;

		internal void Init(global::Kampai.Game.IDefinitionService definitionService, global::Kampai.Game.IQuestService questService, global::Kampai.Game.CraftingBuilding building)
		{
			base.Init();
			this.definitionService = definitionService;
			this.questService = questService;
			this.building = building;
			PopulateRecipeIcons();
			PopulateQueueIcons();
			Open();
		}

		internal void SetTitle(string localizedString)
		{
			title.text = localizedString;
		}

		internal void RePopulateModal(global::Kampai.Game.CraftingBuilding building)
		{
			this.building = building;
			CleanupRecipeIcons();
			PopulateRecipeIcons();
			RefreshQueue();
		}

		internal void RefreshQueue()
		{
			CleanupQueueIcons();
			PopulateQueueIcons();
		}

		private void PopulateRecipeIcons()
		{
			global::UnityEngine.GameObject original = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.GameObject>("cmp_CraftingRecipe");
			int num = 0;
			for (int i = 0; i < building.Definition.RecipeDefinitions.Count; i++)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(original) as global::UnityEngine.GameObject;
				global::Kampai.UI.View.CraftingRecipeView component = gameObject.GetComponent<global::Kampai.UI.View.CraftingRecipeView>();
				int itemID = building.Definition.RecipeDefinitions[i].ItemID;
				global::Kampai.Game.DynamicIngredientsDefinition definition;
				if (definitionService.TryGet<global::Kampai.Game.DynamicIngredientsDefinition>(itemID, out definition))
				{
					if (!IsDynamicRecipeAvailable(building, definition))
					{
						global::UnityEngine.Object.Destroy(gameObject);
						continue;
					}
					gameObject.transform.SetParent(oneOffCraftableLocation, false);
				}
				else
				{
					gameObject.transform.SetParent(recipeLocations[num], false);
					num++;
				}
				component.recipeID = itemID;
				component.instanceID = building.ID;
				recipeIcons.Add(gameObject);
			}
		}

		private void CleanupRecipeIcons()
		{
			foreach (global::UnityEngine.GameObject recipeIcon in recipeIcons)
			{
				global::UnityEngine.Object.Destroy(recipeIcon);
			}
		}

		private void PopulateQueueIcons()
		{
			if (building == null)
			{
				return;
			}
			int num = building.Slots + 1;
			if (num > building.Definition.MaxQueueSlots)
			{
				num = building.Definition.MaxQueueSlots;
			}
			global::UnityEngine.GameObject gameObject = global::Kampai.Util.KampaiResources.Load("cmp_CraftQueue") as global::UnityEngine.GameObject;
			queueWidth = (gameObject.transform as global::UnityEngine.RectTransform).sizeDelta.x;
			queuePadding = queueWidth / 5f;
			for (int i = 0; i < num; i++)
			{
				global::UnityEngine.GameObject gameObject2 = SetupQueueView(i, gameObject);
				global::UnityEngine.RectTransform rectTransform = gameObject2.transform as global::UnityEngine.RectTransform;
				if (i == 0)
				{
					rectTransform.offsetMin = new global::UnityEngine.Vector2(queueWidth * (float)i, 0f);
					rectTransform.offsetMax = new global::UnityEngine.Vector2(queueWidth * (float)(i + 1), 0f);
				}
				else
				{
					rectTransform.offsetMin = new global::UnityEngine.Vector2(queueWidth * (float)i + queuePadding, 0f);
					rectTransform.offsetMax = new global::UnityEngine.Vector2(queueWidth * (float)(i + 1) + queuePadding, 0f);
				}
			}
			int num2 = 3 * (int)queueWidth;
			int num3 = num * (int)queueWidth + (int)queuePadding;
			int num4 = 0;
			if (num3 > num2)
			{
				num4 = (num3 - num2) / 2;
			}
			queueScrollView.sizeDelta = new global::UnityEngine.Vector2(num3, 0f);
			queueScrollView.localPosition = new global::UnityEngine.Vector2(num4, queueScrollView.localPosition.y);
		}

		private global::UnityEngine.GameObject SetupQueueView(int index, global::UnityEngine.GameObject prefab)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(prefab) as global::UnityEngine.GameObject;
			gameObject.transform.SetParent(queueScrollView);
			global::UnityEngine.RectTransform rectTransform = gameObject.transform as global::UnityEngine.RectTransform;
			rectTransform.localPosition = new global::UnityEngine.Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, 0f);
			rectTransform.localScale = global::UnityEngine.Vector3.one;
			global::Kampai.UI.View.CraftingQueueView component = gameObject.GetComponent<global::Kampai.UI.View.CraftingQueueView>();
			component.index = index;
			component.building = building;
			if (index < building.Slots)
			{
				if (index > 0)
				{
					rectTransform.localScale *= 0.8f;
				}
				else
				{
					rectTransform.pivot = global::UnityEngine.Vector2.one / 2f;
				}
				component.inProgressPanel.gameObject.SetActive(false);
				component.availablePanel.gameObject.SetActive(true);
				component.lockedPanel.gameObject.SetActive(false);
			}
			else
			{
				rectTransform.localScale *= 0.8f;
				component.isLocked = true;
				component.availablePanel.gameObject.SetActive(false);
				component.inProgressPanel.gameObject.SetActive(false);
				component.lockedPanel.gameObject.SetActive(true);
				component.purchaseCost = building.getNextIncrementalCost();
				component.lockedCost.text = component.purchaseCost.ToString();
			}
			queueIcons.Add(component);
			return gameObject;
		}

		private void CleanupQueueIcons()
		{
			foreach (global::Kampai.UI.View.CraftingQueueView queueIcon in queueIcons)
			{
				if (queueIcon != null && queueIcon.gameObject != null)
				{
					global::UnityEngine.Object.Destroy(queueIcon.gameObject);
				}
			}
		}

		internal void UpdateQueuePosition()
		{
		}

		internal void SetArrowButtonState(bool enable)
		{
			backArrow.GetComponent<global::UnityEngine.UI.Button>().interactable = enable;
			forwardArrow.GetComponent<global::UnityEngine.UI.Button>().interactable = enable;
		}

		private bool IsDynamicRecipeAvailable(global::Kampai.Game.CraftingBuilding building, global::Kampai.Game.DynamicIngredientsDefinition definition)
		{
			if (!questService.IsOneOffCraftableDisplayable(definition.QuestDefinitionUnlockId, definition.ID))
			{
				return false;
			}
			int num = 0;
			foreach (int dynamicCraft in building.DynamicCrafts)
			{
				if (dynamicCraft == definition.ID)
				{
					num++;
				}
			}
			return num < definition.MaxCraftable;
		}

		internal void ResetDoubleTap(int viewId)
		{
			foreach (global::Kampai.UI.View.CraftingQueueView queueIcon in queueIcons)
			{
				if (queueIcon.index != viewId)
				{
					if (queueIcon.inProduction && queueIcon.inProgressRush != null)
					{
						queueIcon.inProgressRush.ResetTapState();
						queueIcon.inProgressRush.ResetAnim();
					}
					else if (queueIcon.isLocked && queueIcon.lockedPurchase != null)
					{
						queueIcon.lockedPurchase.ResetTapState();
						queueIcon.lockedPurchase.ResetAnim();
					}
				}
			}
		}
	}
}
