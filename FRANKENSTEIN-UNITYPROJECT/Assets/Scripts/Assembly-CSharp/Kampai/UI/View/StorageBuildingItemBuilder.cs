namespace Kampai.UI.View
{
	public static class StorageBuildingItemBuilder
	{
		public static global::Kampai.UI.View.StorageBuildingItemView Build(global::Kampai.Game.Item item, global::Kampai.Game.Definition definition, int count, global::Kampai.Util.ILogger logger)
		{
			if (definition == null)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.EX_NULL_ARG);
			}
			global::UnityEngine.GameObject original = global::Kampai.Util.KampaiResources.Load("cmp_StorageBuildingItem") as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(original) as global::UnityEngine.GameObject;
			global::Kampai.UI.View.StorageBuildingItemView component = gameObject.GetComponent<global::Kampai.UI.View.StorageBuildingItemView>();
			global::Kampai.Game.ItemDefinition itemDefinition = definition as global::Kampai.Game.ItemDefinition;
			global::UnityEngine.Sprite sprite = UIUtils.LoadSpriteFromPath(itemDefinition.Image);
			if (string.IsNullOrEmpty(itemDefinition.Mask))
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "Your ItemDefinition: {0} doesn't have a mask image defined for the item icon: {1}", itemDefinition.ID, itemDefinition.Image);
				itemDefinition.Mask = "btn_Circle01_mask";
			}
			global::UnityEngine.Sprite maskSprite = UIUtils.LoadSpriteFromPath(itemDefinition.Mask);
			component.StorageItem = item;
			component.ItemIcon.sprite = sprite;
			component.ItemIcon.maskSprite = maskSprite;
			component.ItemQuantity.text = count.ToString();
			return component;
		}
	}
}
