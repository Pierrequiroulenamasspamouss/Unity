namespace Kampai.UI.View
{
	public class GenericRushDialogMediator : global::Kampai.UI.View.RushDialogMediator<global::Kampai.UI.View.GenericRushDialogView>
	{
		protected override void SetHeadline(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			string transactionItemName = global::Kampai.Game.Transaction.TransactionUtil.GetTransactionItemName(pct.GetPendingTransaction(), base.definitionService);
			if (!string.IsNullOrEmpty(transactionItemName))
			{
				base.view.HeadlineTitle.text = base.localService.GetString(transactionItemName);
			}
			else if (dialogType == global::Kampai.UI.View.RushDialogView.RushDialogType.STORAGE_EXPAND)
			{
				base.view.HeadlineTitle.text = base.localService.GetString("ExpandStorage");
			}
			else if (pct.GetTransactionTarget() == global::Kampai.Game.TransactionTarget.CLEAR_DEBRIS)
			{
				base.view.HeadlineTitle.text = base.localService.GetString("ClearX", base.localService.GetString("Debris"));
			}
		}

		internal override global::Kampai.UI.View.RequiredItemView BuildItem(global::Kampai.Game.Definition definition, uint itemsInInventory, int itemsLack, bool hasCheckMark, global::Kampai.Main.ILocalizationService localService)
		{
			if (definition == null)
			{
				throw new global::System.ArgumentNullException("definition", "GenericRequiredItemBuilder: You are passing in null definitions!");
			}
			global::UnityEngine.GameObject original = global::Kampai.Util.KampaiResources.Load("cmp_CraftingResourceItem") as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(original) as global::UnityEngine.GameObject;
			global::Kampai.UI.View.GenericRequiredItemView component = gameObject.GetComponent<global::Kampai.UI.View.GenericRequiredItemView>();
			global::Kampai.Game.ItemDefinition itemDefinition = definition as global::Kampai.Game.ItemDefinition;
			global::UnityEngine.Sprite sprite = UIUtils.LoadSpriteFromPath(itemDefinition.Image);
			global::UnityEngine.Sprite maskSprite = UIUtils.LoadSpriteFromPath(itemDefinition.Mask);
			component.ItemIcon.sprite = sprite;
			component.ItemIcon.maskSprite = maskSprite;
			int num = ((itemsLack >= 0) ? itemsLack : 0);
			int num2 = (int)itemsInInventory + itemsLack;
			component.ItemQuantity.text = string.Format("{0}/{1}", itemsInInventory, num2);
			int num3 = global::UnityEngine.Mathf.FloorToInt(itemDefinition.BasePremiumCost * (float)num);
			num3 = ((num3 == 0 && num > 0) ? 1 : num3);
			component.Cost = num3;
			if (!hasCheckMark)
			{
				component.redBorder.gameObject.SetActive(true);
				component.ItemQuantity.color = global::UnityEngine.Color.red;
			}
			else
			{
				component.greenBorder.gameObject.SetActive(true);
				component.ItemQuantity.color = global::Kampai.Util.GameConstants.UI.UI_TEXT_GREY;
			}
			return component;
		}

		protected override void LoadItems(global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition, global::Kampai.UI.View.RushDialogView.RushDialogType type)
		{
			global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> inputs = transactionDefinition.Inputs;
			if (inputs != null)
			{
				int count = inputs.Count;
				bool showPurchaseButton = false;
				requiredItems = new global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem>();
				requiredItemPremiumCosts = new global::System.Collections.Generic.List<int>();
				for (int i = 0; i < count; i++)
				{
					global::Kampai.Game.ItemDefinition definition = base.definitionService.Get<global::Kampai.Game.ItemDefinition>(inputs[i].ID);
					global::Kampai.Util.QuantityItem quantityItem = null;
					uint quantityByDefinitionId = base.playerService.GetQuantityByDefinitionId(inputs[i].ID);
					int num = (int)(inputs[i].Quantity - quantityByDefinitionId);
					bool flag = false;
					if (num <= 0)
					{
						flag = true;
					}
					else
					{
						flag = false;
						quantityItem = new global::Kampai.Util.QuantityItem(inputs[i].ID, (uint)num);
						requiredItems.Add(quantityItem);
					}
					global::Kampai.UI.View.RequiredItemView requiredItemView = BuildItem(definition, quantityByDefinitionId, num, flag, base.localService);
					global::Kampai.UI.View.GenericRequiredItemView genericRequiredItemView = requiredItemView as global::Kampai.UI.View.GenericRequiredItemView;
					if (genericRequiredItemView.Cost != 0)
					{
						requiredItemPremiumCosts.Add(genericRequiredItemView.Cost);
					}
					base.view.AddRequiredItem(requiredItemView, i, base.view.ScrollViewParent);
				}
				if (requiredItems.Count != 0)
				{
					rushCost = base.playerService.CalculateRushCost(requiredItems);
					base.view.SetupItemCost(rushCost);
					showPurchaseButton = true;
				}
				base.view.SetupItemCount(count);
				base.view.SetupDialog(type, showPurchaseButton);
				base.gameObject.SetActive(true);
			}
			else
			{
				base.logger.Debug("Showing rush dialog without require items");
			}
		}
	}
}
