namespace Kampai.UI.View
{
	public static class StoreTabBuilder
	{
		public static global::Kampai.UI.View.StoreTabView Build(global::Kampai.UI.View.StoreTab tab, global::UnityEngine.Transform i_parent, global::Kampai.Util.ILogger logger)
		{
			if (tab == null)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.EX_NULL_ARG);
			}
			global::UnityEngine.GameObject original = global::Kampai.Util.KampaiResources.Load("cmp_MainMenuItem") as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(original) as global::UnityEngine.GameObject;
			global::Kampai.UI.View.StoreTabView component = gameObject.GetComponent<global::Kampai.UI.View.StoreTabView>();
			component.Type = tab.Type;
			component.TabIcon.maskSprite = SetTabIcon(tab.Type);
			component.TabName.text = tab.LocalizedName;
			global::UnityEngine.RectTransform rectTransform = gameObject.transform as global::UnityEngine.RectTransform;
			rectTransform.SetParent(i_parent);
			rectTransform.SetAsFirstSibling();
			rectTransform.localPosition = new global::UnityEngine.Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, 0f);
			rectTransform.localScale = global::UnityEngine.Vector3.one;
			gameObject.SetActive(false);
			return component;
		}

		public static global::UnityEngine.Sprite SetTabIcon(global::Kampai.Game.StoreItemType type)
		{
			string path = string.Empty;
			switch (type)
			{
			case global::Kampai.Game.StoreItemType.BaseResource:
				path = "icn_build_mask_cat_make";
				break;
			case global::Kampai.Game.StoreItemType.Crafting:
				path = "icn_build_mask_cat_mix";
				break;
			case global::Kampai.Game.StoreItemType.Decoration:
				path = "icn_build_mask_cat_decor";
				break;
			case global::Kampai.Game.StoreItemType.Leisure:
				path = "icn_build_mask_cat_leisure";
				break;
			}
			return UIUtils.LoadSpriteFromPath(path);
		}
	}
}
