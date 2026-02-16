namespace Kampai.UI.View
{
	public class CreateNewSellView : global::strange.extensions.mediation.impl.View
	{
		internal readonly int MIN_ITEM_COUNT = 1;

		internal readonly int MAX_ITEM_COUNT = 6;

		internal readonly int MIN_ITEM_PRICE = 10;

		internal readonly int MAX_ITEM_PRICE = 300;

		internal readonly float MOVE_TIME = 0.15f;

		public global::Kampai.UI.View.SellQuantityButtonView AddItemCountButton;

		public global::Kampai.UI.View.SellQuantityButtonView MinusItemCountButton;

		public global::Kampai.UI.View.SellQuantityButtonView AddPriceButton;

		public global::Kampai.UI.View.SellQuantityButtonView MinusPriceButton;

		public global::Kampai.UI.View.ButtonView PutOnSaleButton;

		public global::Kampai.UI.View.ButtonView ResetPriceButton;

		public global::UnityEngine.UI.Text ItemCountText;

		public global::UnityEngine.UI.Text PriceText;

		public global::Kampai.UI.View.KampaiImage ItemIconImage;

		public global::Kampai.UI.View.KampaiImage CurrencyImage;

		public global::Kampai.UI.View.KampaiImage MarketValueImage;

		public global::Kampai.UI.View.KampaiImage MarketValueArrowImage;

		public global::Kampai.UI.View.KampaiImage QuantityImage;

		internal global::Kampai.Game.Item item;

		internal global::Kampai.Game.MarketplaceItemDefinition marketplaceDef;

		internal global::strange.extensions.signal.impl.Signal ClosePanelSignal = new global::strange.extensions.signal.impl.Signal();

		private int m_itemCount = 1;

		private int m_price = 100;

		private int m_maxItemCount;

		private int m_minPrice;

		private int m_maxPrice;

		internal bool IsOpen { get; private set; }

		internal int MaxPrice
		{
			get
			{
				return m_maxPrice * m_itemCount;
			}
		}

		internal int MinPrice
		{
			get
			{
				return m_minPrice * m_itemCount;
			}
		}

		internal int ItemCount
		{
			get
			{
				return m_itemCount;
			}
			set
			{
				m_price /= m_itemCount;
				if (MIN_ITEM_COUNT == m_maxItemCount)
				{
					SetButtonIteractable(AddItemCountButton, false);
					SetButtonIteractable(MinusItemCountButton, false);
					m_itemCount = MIN_ITEM_COUNT;
				}
				else if (value <= MIN_ITEM_COUNT)
				{
					m_itemCount = MIN_ITEM_COUNT;
					SetButtonIteractable(AddItemCountButton, true);
					SetButtonIteractable(MinusItemCountButton, false);
				}
				else if (value >= m_maxItemCount)
				{
					m_itemCount = m_maxItemCount;
					SetButtonIteractable(AddItemCountButton, false);
					SetButtonIteractable(MinusItemCountButton, true);
				}
				else
				{
					m_itemCount = value;
					SetButtonIteractable(AddItemCountButton, true);
					SetButtonIteractable(MinusItemCountButton, true);
				}
				m_price *= m_itemCount;
				SetItemCountText();
				UpdatePriceRange();
				UpdateCountRange();
				Price = m_price;
			}
		}

		internal int Price
		{
			get
			{
				return m_price;
			}
			set
			{
				float num = global::UnityEngine.Mathf.Floor(value);
				if (MinPrice == MaxPrice)
				{
					SetButtonIteractable(AddPriceButton, false);
					SetButtonIteractable(MinusPriceButton, false);
					m_price = MinPrice;
				}
				else if (num <= (float)MinPrice)
				{
					m_price = MinPrice;
					SetButtonIteractable(AddPriceButton, true);
					SetButtonIteractable(MinusPriceButton, false);
				}
				else if (num >= (float)MaxPrice)
				{
					m_price = MaxPrice;
					SetButtonIteractable(AddPriceButton, false);
					SetButtonIteractable(MinusPriceButton, true);
				}
				else
				{
					m_price = value;
					SetButtonIteractable(AddPriceButton, true);
					SetButtonIteractable(MinusPriceButton, true);
				}
				SetSellPriceText();
			}
		}

		private void ChangeMarketValueDisplay(global::UnityEngine.Color backgroundColor, global::UnityEngine.Color arrowColor, global::UnityEngine.Color textColor, global::UnityEngine.Vector3 direction)
		{
			MarketValueImage.color = backgroundColor;
			MarketValueArrowImage.color = arrowColor;
			MarketValueArrowImage.transform.localEulerAngles = direction;
			PriceText.color = textColor;
		}

		internal void Init()
		{
			global::UnityEngine.RectTransform rectTransform = MarketValueImage.transform as global::UnityEngine.RectTransform;
			if (!(rectTransform == null))
			{
				AddItemCountButton.SetSize(rectTransform.rect.height);
				AddPriceButton.SetSize(rectTransform.rect.height);
				MinusItemCountButton.SetSize(rectTransform.rect.height);
				MinusPriceButton.SetSize(rectTransform.rect.height);
				UpdatePriceRange();
				UpdateCountRange();
			}
		}

		internal void UpdatePriceRange()
		{
			global::Kampai.UI.View.SellQuantityButtonView addPriceButton = AddPriceButton;
			addPriceButton.MaxValue = MaxPrice;
			addPriceButton.MinValue = MinPrice;
			addPriceButton.IsPriceButton = true;
			addPriceButton = MinusPriceButton;
			addPriceButton.MaxValue = MaxPrice;
			addPriceButton.MinValue = MinPrice;
			addPriceButton.IsPriceButton = true;
		}

		internal void UpdateCountRange()
		{
			global::Kampai.UI.View.SellQuantityButtonView addItemCountButton = AddItemCountButton;
			addItemCountButton.MaxValue = m_maxItemCount;
			addItemCountButton.MinValue = MIN_ITEM_COUNT;
			addItemCountButton.IsPriceButton = false;
			addItemCountButton = MinusItemCountButton;
			addItemCountButton.MaxValue = m_maxItemCount;
			addItemCountButton.MinValue = MIN_ITEM_COUNT;
			addItemCountButton.IsPriceButton = false;
		}

		internal void SetForSaleItem(global::Kampai.Game.MarketplaceItemDefinition itemDefinition, global::Kampai.Game.Item itemInstance, int maxSellQuantity, bool resetItem, bool coppaRestricted)
		{
			if (item != null && item.ID == itemInstance.ID && !resetItem)
			{
				return;
			}
			item = itemInstance;
			marketplaceDef = itemDefinition;
			if (maxSellQuantity == 0)
			{
				maxSellQuantity = MAX_ITEM_COUNT;
			}
			if (item == null)
			{
				m_maxItemCount = maxSellQuantity;
				ItemCount = 0;
				SetButtonIteractable(MinusItemCountButton, false);
				SetButtonIteractable(AddItemCountButton, false);
				SetButtonIteractable(MinusPriceButton, false);
				SetButtonIteractable(AddPriceButton, false);
				return;
			}
			m_maxItemCount = global::System.Math.Min((int)item.Quantity, maxSellQuantity);
			ItemCount = (int)item.Quantity / 2;
			if (marketplaceDef != null)
			{
				m_minPrice = marketplaceDef.MinStrikePrice;
				m_maxPrice = marketplaceDef.MaxStrikePrice;
				Price = marketplaceDef.StartingStrikePrice * m_itemCount;
				SetMarketplaceValue((!coppaRestricted) ? itemDefinition.PriceTrend : 0);
			}
			else
			{
				m_minPrice = MIN_ITEM_PRICE;
				m_maxPrice = MAX_ITEM_PRICE;
				Price = 100;
				SetMarketplaceValue(0);
			}
			if (!resetItem)
			{
				global::UnityEngine.Sprite sprite = UIUtils.LoadSpriteFromPath(item.Definition.Image);
				ItemIconImage.sprite = sprite;
				global::UnityEngine.Sprite maskSprite = UIUtils.LoadSpriteFromPath(item.Definition.Mask);
				ItemIconImage.maskSprite = maskSprite;
			}
		}

		private void SetButtonIteractable(global::Kampai.UI.View.ButtonView buttonView, bool interactable)
		{
			global::UnityEngine.UI.Button component = buttonView.GetComponent<global::UnityEngine.UI.Button>();
			if (component != null)
			{
				component.interactable = interactable;
			}
		}

		internal void SetItemCountText()
		{
			if (!(ItemCountText == null))
			{
				ItemCountText.text = string.Format("{0}", m_itemCount);
			}
		}

		internal void SetSellPriceText()
		{
			if (!(PriceText == null))
			{
				int num = global::UnityEngine.Mathf.FloorToInt(m_price);
				PriceText.text = string.Format("{0}", num);
			}
		}

		internal void OpenPanel()
		{
			IsOpen = true;
			base.gameObject.SetActive(true);
		}

		internal void ClosePanel()
		{
			IsOpen = false;
			marketplaceDef = null;
			item = null;
		}

		private void SetMarketplaceValue(int marketplaceValue)
		{
			global::UnityEngine.Color backgroundColor;
			global::UnityEngine.Color color;
			global::UnityEngine.Color textColor;
			global::UnityEngine.Vector3 direction;
			if (marketplaceValue > 0)
			{
				backgroundColor = new global::UnityEngine.Color(0.9098039f, 0.95686275f, 0.827451f);
				color = new global::UnityEngine.Color(0.5921569f, 0.80784315f, 0.23529412f);
				textColor = color;
				direction = new global::UnityEngine.Vector3(0f, 0f, 90f);
			}
			else if (marketplaceValue < 0)
			{
				backgroundColor = new global::UnityEngine.Color(47f / 51f, 0.8235294f, 0.81960785f);
				color = new global::UnityEngine.Color(48f / 85f, 2f / 15f, 2f / 15f);
				textColor = color;
				direction = new global::UnityEngine.Vector3(0f, 0f, -90f);
			}
			else
			{
				backgroundColor = QuantityImage.color;
				textColor = global::UnityEngine.Color.white;
				color = new global::UnityEngine.Color(0f, 0f, 0f, 0f);
				direction = new global::UnityEngine.Vector3(0f, 0f, 90f);
			}
			ChangeMarketValueDisplay(backgroundColor, color, textColor, direction);
		}
	}
}
