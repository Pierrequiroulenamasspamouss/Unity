namespace Kampai.UI.View
{
	public class SocialPartyRewardView : global::Kampai.UI.View.PopupMenuView
	{
		public global::Kampai.UI.View.ButtonView collectButton;

		public global::UnityEngine.UI.Text collectButtonText;

		public global::UnityEngine.UI.Text content;

		public global::UnityEngine.UI.Text title;

		public global::UnityEngine.RectTransform scrollViewTransform;

		public global::UnityEngine.Animator rewardAnimator;

		public float padding = 10f;

		internal global::Kampai.Game.IDefinitionService definitionService;

		private global::Kampai.Main.ILocalizationService localizedService;

		internal global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition;

		internal global::System.Collections.Generic.List<global::UnityEngine.GameObject> viewList = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();

		internal void Init(global::Kampai.Game.Transaction.TransactionDefinition tD, global::Kampai.Main.ILocalizationService localService, global::Kampai.Game.IDefinitionService defService, global::Kampai.Game.IPlayerService playerService)
		{
			base.Init();
			definitionService = defService;
			localizedService = localService;
			global::UnityEngine.RectTransform rectTransform = base.transform as global::UnityEngine.RectTransform;
			rectTransform.offsetMin = global::UnityEngine.Vector2.zero;
			rectTransform.offsetMax = global::UnityEngine.Vector2.zero;
			transactionDefinition = tD;
			if (transactionDefinition != null)
			{
				SetupScrollView(RewardUtil.GetRewardQuantityFromTransaction(transactionDefinition, definitionService, playerService));
			}
			Open();
			rewardAnimator.Play("Open");
		}

		internal void SetupScrollView(global::System.Collections.Generic.List<global::Kampai.Game.View.RewardQuantity> quantityChange)
		{
			global::UnityEngine.GameObject gameObject = global::Kampai.Util.KampaiResources.Load("cmp_RewardSlider") as global::UnityEngine.GameObject;
			float x = (gameObject.transform as global::UnityEngine.RectTransform).sizeDelta.x;
			int count = quantityChange.Count;
			for (int i = 0; i < count; i++)
			{
				global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(gameObject) as global::UnityEngine.GameObject;
				gameObject2.transform.SetParent(scrollViewTransform, false);
				global::UnityEngine.RectTransform rectTransform = gameObject2.transform as global::UnityEngine.RectTransform;
				rectTransform.localPosition = global::UnityEngine.Vector3.zero;
				rectTransform.localScale = global::UnityEngine.Vector3.one;
				rectTransform.offsetMin = new global::UnityEngine.Vector2(x * (float)i + padding * (float)i, 0f);
				rectTransform.offsetMax = new global::UnityEngine.Vector2(x * (float)(i + 1) + padding * (float)i, 0f);
				global::Kampai.UI.View.RewardSliderView component = gameObject2.GetComponent<global::Kampai.UI.View.RewardSliderView>();
				global::Kampai.Game.UnlockDefinition definition;
				global::Kampai.Game.DisplayableDefinition displayableDefinition = ((quantityChange[i].ID == 0 || quantityChange[i].ID == 1 || quantityChange[i].ID == 5 || !definitionService.TryGet<global::Kampai.Game.UnlockDefinition>(quantityChange[i].ID, out definition)) ? definitionService.Get<global::Kampai.Game.DisplayableDefinition>(quantityChange[i].ID) : definitionService.Get<global::Kampai.Game.DisplayableDefinition>(definition.ReferencedDefinitionID));
				if (displayableDefinition != null)
				{
					component.description.text = localizedService.GetString(displayableDefinition.LocalizedKey);
					component.icon.sprite = UIUtils.LoadSpriteFromPath(displayableDefinition.Image);
					component.icon.maskSprite = UIUtils.LoadSpriteFromPath(displayableDefinition.Mask);
					component.currencyQuantity.gameObject.SetActive(true);
					component.currencyQuantity.text = quantityChange[i].Quantity.ToString();
					viewList.Add(gameObject2);
				}
				else
				{
					base.logger.Warning("Social reward Item not valid {0}", i);
				}
			}
			int num = 3 * (int)x;
			int num2 = count * (int)x + (int)(padding * (float)count);
			int num3 = 0;
			if (num2 > num)
			{
				num3 = (num2 - num) / 2;
			}
			scrollViewTransform.sizeDelta = new global::UnityEngine.Vector2(num2, 0f);
			scrollViewTransform.localPosition = new global::UnityEngine.Vector2(num3, scrollViewTransform.localPosition.y);
		}
	}
}
