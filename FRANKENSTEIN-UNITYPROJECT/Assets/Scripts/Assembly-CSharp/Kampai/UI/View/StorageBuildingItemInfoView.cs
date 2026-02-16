namespace Kampai.UI.View
{
	public class StorageBuildingItemInfoView : global::Kampai.UI.View.PopupMenuView
	{
		public global::UnityEngine.UI.Text ItemText;

		public global::UnityEngine.UI.Text BuildingText;

		public global::UnityEngine.UI.Text CraftingTimeText;

		public global::Kampai.UI.View.KampaiImage ClocKampaiImage;

		public global::UnityEngine.RectTransform downArrow;

		public global::UnityEngine.RectTransform downOverlayArrow;

		internal global::Kampai.Game.ItemDefinition ItemDefinition;

		private global::Kampai.Main.ILocalizationService localizationService;

		private global::UnityEngine.Camera uiCamera;

		private global::UnityEngine.RectTransform itemTransform;

		[Inject(global::Kampai.Main.MainElement.UI_GLASSCANVAS)]
		public global::UnityEngine.GameObject glassCanvas { get; set; }

		internal void Init(global::Kampai.Main.ILocalizationService localizationService, global::UnityEngine.Camera camera)
		{
			base.Init();
			uiCamera = camera;
			global::UnityEngine.CanvasGroup component = GetComponent<global::UnityEngine.CanvasGroup>();
			if (component != null)
			{
				component.interactable = false;
				component.blocksRaycasts = false;
			}
			this.localizationService = localizationService;
		}

		internal void SetItem(global::Kampai.Game.ItemDefinition itemDefinition, global::UnityEngine.RectTransform itemCenter, global::UnityEngine.Vector3 center, global::Kampai.Game.IDefinitionService definitionService, global::Kampai.Game.ITimeService timeService)
		{
			ItemDefinition = itemDefinition;
			itemTransform = itemCenter;
			global::UnityEngine.RectTransform rectTransform = base.transform as global::UnityEngine.RectTransform;
			if (!(rectTransform == null))
			{
				UpdatePosition(center);
				setItemText();
				setBuildingText(definitionService, timeService);
				setCraftingTime();
				Open();
			}
		}

		private void UpdatePosition(global::UnityEngine.Vector3 center)
		{
			global::UnityEngine.RectTransform rectTransform = base.transform as global::UnityEngine.RectTransform;
			if (!(rectTransform == null))
			{
				rectTransform.localScale = global::UnityEngine.Vector3.one;
				global::UnityEngine.Vector2 anchorMin = (rectTransform.anchorMax = center);
				rectTransform.anchorMin = anchorMin;
				float x = glassCanvas.GetComponent<global::UnityEngine.UI.CanvasScaler>().referenceResolution.x;
				rectTransform.anchoredPosition3D = global::UnityEngine.Vector2.zero;
				float num = rectTransform.anchorMin.x - rectTransform.sizeDelta.x / x / 2f;
				if (num < 0.015f)
				{
					rectTransform.anchorMin = new global::UnityEngine.Vector2(center.x - num + 0.02f, center.y);
					rectTransform.anchorMax = new global::UnityEngine.Vector2(center.x - num + 0.02f, center.y);
					global::UnityEngine.RectTransform rectTransform2 = downOverlayArrow;
					global::UnityEngine.Vector2 anchoredPosition = new global::UnityEngine.Vector2(downArrow.anchoredPosition.x - num * x, downArrow.anchoredPosition.y);
					downArrow.anchoredPosition = anchoredPosition;
					rectTransform2.anchoredPosition = anchoredPosition;
				}
				rectTransform.localPosition = new global::UnityEngine.Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y + rectTransform.sizeDelta.y / 4f, rectTransform.localPosition.z);
			}
		}

		public global::UnityEngine.Vector3 GetCenter()
		{
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[4];
			itemTransform.GetWorldCorners(array);
			global::UnityEngine.Vector3 position = default(global::UnityEngine.Vector3);
			global::UnityEngine.Vector3[] array2 = array;
			foreach (global::UnityEngine.Vector3 vector in array2)
			{
				position += vector;
			}
			position /= 4f;
			return uiCamera.WorldToViewportPoint(position);
		}

		internal void setItemText()
		{
			if (ItemDefinition != null)
			{
				ItemText.text = localizationService.GetString(ItemDefinition.LocalizedKey);
			}
		}

		internal void setBuildingText(global::Kampai.Game.IDefinitionService definitionService, global::Kampai.Game.ITimeService timeService)
		{
			global::Kampai.Game.IngredientsItemDefinition ingredientsItemDefinition = ItemDefinition as global::Kampai.Game.IngredientsItemDefinition;
			if (ingredientsItemDefinition != null)
			{
				int buildingDefintionIDFromItemDefintionID = definitionService.GetBuildingDefintionIDFromItemDefintionID(ingredientsItemDefinition.ID);
				global::Kampai.Game.BuildingDefinition definition;
				if (definitionService.TryGet<global::Kampai.Game.BuildingDefinition>(buildingDefintionIDFromItemDefintionID, out definition))
				{
					BuildingText.text = string.Format(localizationService.GetString((definition.Type != BuildingType.BuildingTypeIdentifier.CRAFTING) ? "StorageBuildingTooltipHarvestBuilding" : "StorageBuildingTooltipCraftingBuilding"), timeService.GetCultureInfo().TextInfo.ToTitleCase(localizationService.GetString(definition.LocalizedKey).ToLower(timeService.GetCultureInfo())));
				}
				ClocKampaiImage.gameObject.SetActive(true);
				CraftingTimeText.gameObject.SetActive(true);
			}
			else
			{
				global::Kampai.Game.DropItemDefinition dropItemDefinition = ItemDefinition as global::Kampai.Game.DropItemDefinition;
				if (dropItemDefinition != null)
				{
					BuildingText.text = localizationService.GetString("StorageBuildingTooltipRandomDrop");
				}
				ClocKampaiImage.gameObject.SetActive(false);
				CraftingTimeText.gameObject.SetActive(false);
			}
		}

		internal void setCraftingTime()
		{
			global::Kampai.Game.IngredientsItemDefinition ingredientsItemDefinition = ItemDefinition as global::Kampai.Game.IngredientsItemDefinition;
			if (ingredientsItemDefinition != null)
			{
				global::System.TimeSpan timeSpan = global::System.TimeSpan.FromSeconds(ingredientsItemDefinition.TimeToHarvest);
				CraftingTimeText.text = string.Format("{0:g}", timeSpan);
			}
		}
	}
}
