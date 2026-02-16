namespace Kampai.UI.View
{
	public class ProceduralQuestView : global::Kampai.UI.View.PopupMenuView
	{
		public global::UnityEngine.UI.Text Title;

		public global::UnityEngine.GameObject BackGround;

		public global::UnityEngine.UI.Text ItemAmountText;

		public global::UnityEngine.UI.Text CurrencyAmountText;

		public global::UnityEngine.UI.Text YesSellButtonText;

		public global::UnityEngine.UI.Text NoSellButtonText;

		public global::Kampai.UI.View.KampaiImage InputIconImage;

		public global::Kampai.UI.View.KampaiImage OutputIconImage;

		public global::UnityEngine.UI.Button YesSellButton;

		public global::UnityEngine.UI.Button NoSellButton;

		public global::UnityEngine.GameObject CheckMarkImage;

		public global::UnityEngine.GameObject CrossMarkImage;

		private global::Kampai.Main.ILocalizationService localService;

		internal void Init(global::Kampai.Main.ILocalizationService localService)
		{
			base.Init();
			this.localService = localService;
			Open();
		}

		internal void InitSellView(int requiredCount, int inventoryCount, int rewardCount, global::Kampai.Game.ItemDefinition inputItem, global::Kampai.Game.ItemDefinition outputItem)
		{
			if (requiredCount > inventoryCount)
			{
				CheckMarkImage.SetActive(false);
				CrossMarkImage.SetActive(true);
			}
			else
			{
				CheckMarkImage.SetActive(true);
				CrossMarkImage.SetActive(false);
			}
			ItemAmountText.text = string.Format("{0}/{1}", global::System.Math.Min(inventoryCount, requiredCount), requiredCount);
			CurrencyAmountText.text = rewardCount.ToString();
			YesSellButtonText.text = localService.GetString("Yes");
			NoSellButtonText.text = localService.GetString("No");
			Title.text = localService.GetString("SellTitle");
			InputIconImage.sprite = UIUtils.LoadSpriteFromPath(inputItem.Image);
			InputIconImage.maskSprite = UIUtils.LoadSpriteFromPath(inputItem.Mask);
			OutputIconImage.sprite = UIUtils.LoadSpriteFromPath(outputItem.Image);
			OutputIconImage.maskSprite = UIUtils.LoadSpriteFromPath(outputItem.Mask);
		}
	}
}
