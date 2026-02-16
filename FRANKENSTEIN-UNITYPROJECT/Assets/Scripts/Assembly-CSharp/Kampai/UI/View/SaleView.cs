namespace Kampai.UI.View
{
	public class SaleView : global::Kampai.UI.View.PopupMenuView
	{
		public global::UnityEngine.UI.Text title;

		public global::UnityEngine.UI.Text description;

		public global::UnityEngine.UI.Text cost;

		public global::UnityEngine.RectTransform scrollView;

		public global::Kampai.UI.View.ButtonView closeButton;

		public global::Kampai.UI.View.ButtonView purchaseButton;

		private global::Kampai.Game.IDefinitionService definitionService;

		private global::Kampai.Main.ILocalizationService localization;

		internal global::System.Collections.Generic.List<global::UnityEngine.GameObject> viewList = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();

		internal void Init(global::Kampai.Game.SaleDefinition sale, global::Kampai.Game.Transaction.TransactionDefinition transaction, global::Kampai.Game.IDefinitionService defService, global::Kampai.Main.ILocalizationService local, global::Kampai.Util.ILogger logger)
		{
			if (transaction.Inputs.Count != 1)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.DS_INVALID_SALE_INPUT_COUNT);
				return;
			}
			base.Init();
			definitionService = defService;
			localization = local;
			title.text = localization.GetString(sale.Title);
			description.text = localization.GetString(sale.Description);
			cost.text = transaction.Inputs[0].Quantity.ToString();
			PopulateScrollView(transaction.Outputs);
			Open();
		}

		private void PopulateScrollView(global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> rewards)
		{
			global::UnityEngine.GameObject gameObject = global::Kampai.Util.KampaiResources.Load("cmp_RewardSlider") as global::UnityEngine.GameObject;
			float x = (gameObject.transform as global::UnityEngine.RectTransform).sizeDelta.x;
			for (int i = 0; i < rewards.Count; i++)
			{
				global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(gameObject) as global::UnityEngine.GameObject;
				gameObject2.transform.SetParent(scrollView.transform, false);
				gameObject2.transform.localPosition = global::UnityEngine.Vector3.zero;
				gameObject2.transform.localScale = global::UnityEngine.Vector3.one;
				SetSliderValue(gameObject2, rewards[i]);
				(gameObject2.transform as global::UnityEngine.RectTransform).offsetMin = new global::UnityEngine.Vector2(x * (float)i, 0f);
				(gameObject2.transform as global::UnityEngine.RectTransform).offsetMax = new global::UnityEngine.Vector2(x * (float)(i + 1), 0f);
				viewList.Add(gameObject2);
			}
			int num = 3 * (int)x;
			int num2 = rewards.Count * (int)x;
			int num3 = 0;
			if (num2 > num)
			{
				num3 = (num2 - num) / 2;
			}
			(scrollView.transform as global::UnityEngine.RectTransform).sizeDelta = new global::UnityEngine.Vector2(num2, 0f);
			(scrollView.transform as global::UnityEngine.RectTransform).localPosition = new global::UnityEngine.Vector2(num3, (scrollView.transform as global::UnityEngine.RectTransform).localPosition.y);
		}

		private void SetSliderValue(global::UnityEngine.GameObject go, global::Kampai.Util.QuantityItem item)
		{
			global::Kampai.UI.View.RewardSliderView component = go.GetComponent<global::Kampai.UI.View.RewardSliderView>();
			component.itemQuantity.text = item.Quantity.ToString();
			global::Kampai.Game.ItemDefinition itemDefinition = definitionService.Get<global::Kampai.Game.ItemDefinition>(item.ID);
			component.description.text = localization.GetString(itemDefinition.LocalizedKey);
			component.icon.sprite = UIUtils.LoadSpriteFromPath(itemDefinition.Image);
			component.icon.maskSprite = UIUtils.LoadSpriteFromPath(itemDefinition.Mask);
		}
	}
}
