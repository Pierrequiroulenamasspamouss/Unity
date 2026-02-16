namespace Kampai.UI.View
{
	public class CurrencyMenuView : global::Kampai.UI.View.PopupMenuView
	{
		private const int COLUMN_COUNT = 3;

		private const int ROW_COUNT = 2;

		public global::UnityEngine.RectTransform ScrollViewParent;

		public global::UnityEngine.RectTransform CurrencyMenu;

		public global::UnityEngine.UI.Text title;

		public global::System.Collections.Generic.IList<global::Kampai.UI.View.CurrencyButtonView> premiumCurrencyButtons;

		private global::System.Collections.Generic.List<global::Kampai.UI.View.CurrencyButtonView> grindCurrencyButtons;

		internal bool isOpen;

		private float buttonWidth;

		private float buttonHeight;

		private float widthSpacing;

		private float heightSpacing;

		private global::Kampai.Main.ILocalizationService localService;

		private global::System.Func<bool> restricted;

		public float movingDistance { get; private set; }

		public void Init(global::Kampai.Main.ILocalizationService localization, global::System.Func<bool> COPPACompliant)
		{
			base.Init();
			restricted = COPPACompliant;
			localService = localization;
			premiumCurrencyButtons = new global::System.Collections.Generic.List<global::Kampai.UI.View.CurrencyButtonView>();
			grindCurrencyButtons = new global::System.Collections.Generic.List<global::Kampai.UI.View.CurrencyButtonView>();
			SetActive(false);
			movingDistance = CurrencyMenu.offsetMax.y - CurrencyMenu.offsetMin.y;
			CurrencyMenu.offsetMin = new global::UnityEngine.Vector2(CurrencyMenu.offsetMin.x, 0f);
			CurrencyMenu.offsetMax = new global::UnityEngine.Vector2(CurrencyMenu.offsetMax.x, movingDistance);
			base.gameObject.SetActive(false);
		}

		internal void AddCurrencyButton(global::Kampai.Game.StoreItemType type, global::Kampai.UI.View.CurrencyButtonView btnView)
		{
			buttonWidth = btnView.WidthInAnchor;
			buttonHeight = btnView.HeightInAnchor;
			widthSpacing = (1f - buttonWidth * 3f) / 2f;
			heightSpacing = (1f - buttonHeight * 2f) / 1f;
			global::System.Collections.Generic.List<global::Kampai.UI.View.CurrencyButtonView> list = grindCurrencyButtons;
			if (type == global::Kampai.Game.StoreItemType.PremiumCurrency)
			{
				list = premiumCurrencyButtons as global::System.Collections.Generic.List<global::Kampai.UI.View.CurrencyButtonView>;
			}
			int count = list.Count;
			list.Add(btnView);
			int num = count % 3;
			int num2 = 1 - count / 3;
			global::UnityEngine.RectTransform rectTransform = list[count].transform as global::UnityEngine.RectTransform;
			rectTransform.offsetMin = global::UnityEngine.Vector2.zero;
			rectTransform.offsetMax = global::UnityEngine.Vector2.zero;
			rectTransform.anchorMin = new global::UnityEngine.Vector2((float)num * (buttonWidth + widthSpacing), (float)num2 * (buttonHeight + heightSpacing));
			rectTransform.anchorMax = new global::UnityEngine.Vector2((float)num * (buttonWidth + widthSpacing) + buttonWidth, (float)num2 * (buttonHeight + heightSpacing) + buttonHeight);
			list[count].gameObject.SetActive(false);
		}

		internal void ReorganizeButtons()
		{
			int count = premiumCurrencyButtons.Count;
			int num = count % 3;
			if (num != 0)
			{
				int num2 = count / 3;
				ReorganizeList(premiumCurrencyButtons, num, num2, 1 - num2);
			}
			int count2 = grindCurrencyButtons.Count;
			int num3 = count2 % 3;
			if (num3 != 0)
			{
				int num4 = count2 / 3;
				ReorganizeList(grindCurrencyButtons, num3, num4, 1 - num4);
			}
		}

		private void ReorganizeList(global::System.Collections.Generic.IList<global::Kampai.UI.View.CurrencyButtonView> buttons, int numberOfButtonsLeftInARow, int rowIndex, int index)
		{
			if (numberOfButtonsLeftInARow == 1)
			{
				global::UnityEngine.RectTransform rectTransform = buttons[numberOfButtonsLeftInARow - 1 + rowIndex * 3].transform as global::UnityEngine.RectTransform;
				rectTransform.anchorMin = new global::UnityEngine.Vector2(0.5f - buttonWidth / 2f, (float)index * (buttonHeight + heightSpacing));
				rectTransform.anchorMax = new global::UnityEngine.Vector2(0.5f + buttonWidth / 2f, (float)index * (buttonHeight + heightSpacing) + buttonHeight);
			}
			if (numberOfButtonsLeftInARow == 2)
			{
				global::UnityEngine.RectTransform rectTransform2 = buttons[numberOfButtonsLeftInARow - 2 + rowIndex * 3].transform as global::UnityEngine.RectTransform;
				rectTransform2.anchorMin = new global::UnityEngine.Vector2(0.5f - buttonWidth - widthSpacing / 2f, (float)index * (buttonHeight + heightSpacing));
				rectTransform2.anchorMax = new global::UnityEngine.Vector2(0.5f - widthSpacing / 2f, (float)index * (buttonHeight + heightSpacing) + buttonHeight);
				rectTransform2 = buttons[numberOfButtonsLeftInARow - 1 + rowIndex * 3].transform as global::UnityEngine.RectTransform;
				rectTransform2.anchorMin = new global::UnityEngine.Vector2(0.5f + widthSpacing / 2f, (float)index * (buttonHeight + heightSpacing));
				rectTransform2.anchorMax = new global::UnityEngine.Vector2(0.5f + buttonWidth + widthSpacing / 2f, (float)index * (buttonHeight + heightSpacing) + buttonHeight);
			}
		}

		internal void MoveMenu(bool show, global::Kampai.Game.StoreItemType type = global::Kampai.Game.StoreItemType.PremiumCurrency)
		{
			if (show & !isOpen)
			{
				SetActive(show, type);
				isOpen = true;
				base.gameObject.SetActive(true);
				Open();
			}
			else if (!show & isOpen)
			{
				SetActive(show);
				isOpen = false;
				Close();
			}
		}

		internal void SetActive(bool isActive, global::Kampai.Game.StoreItemType type = global::Kampai.Game.StoreItemType.PremiumCurrency)
		{
			if (!isActive)
			{
				return;
			}
			if (type == global::Kampai.Game.StoreItemType.PremiumCurrency)
			{
				title.text = localService.GetString("CurrencyPremiumTitle");
				foreach (global::Kampai.UI.View.CurrencyButtonView premiumCurrencyButton in premiumCurrencyButtons)
				{
					if (premiumCurrencyButton.isCOPPAGated && restricted())
					{
						premiumCurrencyButton.gameObject.SetActive(false);
					}
					else
					{
						premiumCurrencyButton.gameObject.SetActive(true);
					}
				}
				{
					foreach (global::Kampai.UI.View.CurrencyButtonView grindCurrencyButton in grindCurrencyButtons)
					{
						grindCurrencyButton.gameObject.SetActive(false);
					}
					return;
				}
			}
			title.text = localService.GetString("CurrencyGrindTitle");
			foreach (global::Kampai.UI.View.CurrencyButtonView grindCurrencyButton2 in grindCurrencyButtons)
			{
				if (grindCurrencyButton2.isCOPPAGated && restricted())
				{
					grindCurrencyButton2.gameObject.SetActive(false);
				}
				else
				{
					grindCurrencyButton2.gameObject.SetActive(true);
				}
			}
			foreach (global::Kampai.UI.View.CurrencyButtonView premiumCurrencyButton2 in premiumCurrencyButtons)
			{
				premiumCurrencyButton2.gameObject.SetActive(false);
			}
		}
	}
}
