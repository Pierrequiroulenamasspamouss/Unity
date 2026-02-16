namespace Kampai.UI.View
{
	public static class CurrencyButtonBuilder
	{
		public static global::Kampai.UI.View.CurrencyButtonView Build(global::Kampai.Main.ILocalizationService localService, global::Kampai.Game.CurrencyItemDefinition definition, string inputStr, string outputStr, global::UnityEngine.Transform i_parent, global::Kampai.Game.StoreItemType type)
		{
			if (definition == null)
			{
				throw new global::System.ArgumentNullException("definition", "CurrencyButtonBuilder: You are passing in null definitions!");
			}
			global::UnityEngine.GameObject original = global::Kampai.Util.KampaiResources.Load("cmp_purchaseCurrencyButton") as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(original) as global::UnityEngine.GameObject;
			global::Kampai.UI.View.CurrencyButtonView component = gameObject.GetComponent<global::Kampai.UI.View.CurrencyButtonView>();
			global::UnityEngine.GameObject original2 = global::Kampai.Util.KampaiResources.Load(definition.VFX) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(original2) as global::UnityEngine.GameObject;
			gameObject2.transform.SetParent(component.VFXRoot, false);
			component.Description.text = localService.GetString(definition.LocalizedKey);
			component.isCOPPAGated = definition.COPPAGated;
			component.ItemWorth.text = outputStr;
			if (type == global::Kampai.Game.StoreItemType.GrindCurrency)
			{
				component.CostCurrencyIcon.gameObject.SetActive(false);
			}
			else
			{
				component.CostCurrencyIcon.gameObject.SetActive(false);
			}
			global::UnityEngine.Sprite sprite = UIUtils.LoadSpriteFromPath(definition.Image);
			component.ItemImage.sprite = sprite;
			global::UnityEngine.Sprite maskSprite = UIUtils.LoadSpriteFromPath(definition.Mask);
			component.ItemImage.maskSprite = maskSprite;
			component.ItemPrice.text = inputStr;
			global::UnityEngine.RectTransform rectTransform = gameObject.transform as global::UnityEngine.RectTransform;
			rectTransform.SetParent(i_parent);
			rectTransform.SetAsFirstSibling();
			rectTransform.anchorMin = new global::UnityEngine.Vector2(0f, 0f);
			rectTransform.anchorMax = new global::UnityEngine.Vector2(0f, 0f);
			rectTransform.pivot = new global::UnityEngine.Vector2(0f, 0f);
			rectTransform.localPosition = new global::UnityEngine.Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, 0f);
			rectTransform.localScale = global::UnityEngine.Vector3.one;
			gameObject.SetActive(false);
			return component;
		}
	}
}
