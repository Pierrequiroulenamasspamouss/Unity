namespace Kampai.UI.View
{
	public static class StoreButtonBuilder
	{
		private static readonly string[] Textures = new string[4] { "icn_currency_Grind_fill", "icn_currency_Grind_mask", "icn_currency_premium_fill", "icn_currency_premium_mask" };

		public static global::Kampai.UI.View.StoreButtonView Build(global::Kampai.Game.Definition definition, global::Kampai.Game.Transaction.TransactionDefinition transaction, global::Kampai.Game.StoreItemDefinition storeItemDefinition, global::UnityEngine.Transform i_parent, global::Kampai.Main.ILocalizationService localService, global::Kampai.Game.IDefinitionService definitionService, global::Kampai.Util.ILogger logger)
		{
			if (definition == null)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.EX_NULL_ARG);
			}
			global::UnityEngine.GameObject original = global::Kampai.Util.KampaiResources.Load("cmp_SubMenuItem") as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(original) as global::UnityEngine.GameObject;
			global::Kampai.UI.View.StoreButtonView component = gameObject.GetComponent<global::Kampai.UI.View.StoreButtonView>();
			component.init();
			component.ItemName.text = localService.GetString(definition.LocalizedKey);
			component.definition = definition;
			component.transactionDef = transaction;
			component.storeItemDefinition = storeItemDefinition;
			global::Kampai.Game.DisplayableDefinition displayableDefinition = definition as global::Kampai.Game.DisplayableDefinition;
			if (displayableDefinition != null)
			{
				component.ItemDescription.text = localService.GetString(displayableDefinition.Description);
				if (string.IsNullOrEmpty(displayableDefinition.Mask))
				{
					logger.Log(global::Kampai.Util.Logger.Level.Error, "Your Building Definition: {0} doesn' have a mask image defined for the building icon: {1}", displayableDefinition.ID, displayableDefinition.Image);
					displayableDefinition.Mask = "btn_Circle01_mask";
				}
				global::UnityEngine.Sprite maskSprite = UIUtils.LoadSpriteFromPath(displayableDefinition.Mask);
				component.ItemIcon.maskSprite = maskSprite;
				component.UnlockedAtLevel.text = localService.GetString("UnlockAt", definitionService.GetLevelItemUnlocksAt(displayableDefinition.ID));
			}
			global::UnityEngine.RectTransform rectTransform = gameObject.transform as global::UnityEngine.RectTransform;
			rectTransform.SetParent(i_parent);
			rectTransform.SetAsFirstSibling();
			rectTransform.localPosition = new global::UnityEngine.Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, 0f);
			rectTransform.localScale = global::UnityEngine.Vector3.one;
			gameObject.SetActive(false);
			return component;
		}

		public static bool DetermineUnlock(global::Kampai.UI.View.StoreButtonView view, global::Kampai.Game.IPlayerService playerService, global::System.Collections.Generic.Dictionary<int, int> countMap, global::Kampai.Game.IDefinitionService definitionService, global::Kampai.Util.ILogger logger)
		{
			bool result = false;
			int iD = view.definition.ID;
			int quantity = (int)playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID);
			global::Kampai.Game.DisplayableDefinition displayableDefinition = view.definition as global::Kampai.Game.DisplayableDefinition;
			if (definitionService.GetLevelItemUnlocksAt(iD) > quantity)
			{
				ItemLocked(view);
			}
			else
			{
				CheckBadge(view);
				result = CheckLocked(view);
				if (string.IsNullOrEmpty(displayableDefinition.Image))
				{
					logger.Log(global::Kampai.Util.Logger.Level.Error, "Your Building Definition: {0} doesn' have a image defined", displayableDefinition.ID);
					displayableDefinition.Image = "btn_Circle01_mask";
				}
				global::UnityEngine.Sprite sprite = UIUtils.LoadSpriteFromPath(displayableDefinition.Image);
				view.ItemIcon.sprite = sprite;
				int buildingCount = UpdateBuildingCount(view, iD, playerService, countMap);
				int incrementalCost = GetIncrementalCost(view, buildingCount, definitionService, playerService);
				CheckForTransactions(view, incrementalCost);
			}
			return result;
		}

		private static int UpdateBuildingCount(global::Kampai.UI.View.StoreButtonView view, int itemDefintionId, global::Kampai.Game.IPlayerService playerService, global::System.Collections.Generic.Dictionary<int, int> countMap)
		{
			int unlockedQuantityOfID = playerService.GetUnlockedQuantityOfID(itemDefintionId);
			view.SetCapacity(unlockedQuantityOfID);
			int num = 0;
			if (countMap.ContainsKey(itemDefintionId))
			{
				num = countMap[itemDefintionId];
				view.SetBuildingCount(num);
			}
			return num;
		}

		private static void CheckForTransactions(global::Kampai.UI.View.StoreButtonView view, int totalIncrementalCost)
		{
			if (global::Kampai.Game.Transaction.TransactionUtil.IsOnlyPremiumInputs(view.transactionDef))
			{
				view.Cost.text = (global::Kampai.Game.Transaction.TransactionUtil.SumOutputsForStaticItem(view.transactionDef, global::Kampai.Game.StaticItem.PREMIUM_CURRENCY_ID, true) + totalIncrementalCost).ToString();
				view.MoneyIcon.sprite = UIUtils.LoadSpriteFromPath(Textures[2]);
				view.MoneyIcon.maskSprite = UIUtils.LoadSpriteFromPath(Textures[3]);
			}
			else
			{
				view.Cost.text = (global::Kampai.Game.Transaction.TransactionUtil.SumOutputsForStaticItem(view.transactionDef, global::Kampai.Game.StaticItem.GRIND_CURRENCY_ID, true) + totalIncrementalCost).ToString();
				view.MoneyIcon.sprite = UIUtils.LoadSpriteFromPath(Textures[0]);
				view.MoneyIcon.maskSprite = UIUtils.LoadSpriteFromPath(Textures[1]);
				view.DisableDoubleConfirm();
			}
		}

		private static bool CheckLocked(global::Kampai.UI.View.StoreButtonView view)
		{
			return view.ChangeStateToUnlocked();
		}

		private static void CheckBadge(global::Kampai.UI.View.StoreButtonView view)
		{
			view.ItemBadge.HideNew();
		}

		private static void ItemLocked(global::Kampai.UI.View.StoreButtonView view)
		{
			view.ChangeStateToLocked();
		}

		private static int GetIncrementalCost(global::Kampai.UI.View.StoreButtonView view, int buildingCount, global::Kampai.Game.IDefinitionService definitionService, global::Kampai.Game.IPlayerService playerService)
		{
			int incrementalCost = definitionService.GetIncrementalCost(view.definition);
			int num = 0;
			if (incrementalCost > 0)
			{
				num = playerService.GetInventoryCountByDefinitionID(view.definition.ID);
			}
			return (buildingCount + num) * incrementalCost;
		}
	}
}
