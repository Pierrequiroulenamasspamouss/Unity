namespace Kampai.UI.View
{
	public class LandExpansionRushDialogMediator : global::Kampai.UI.View.RushDialogMediator<global::Kampai.UI.View.LandExpansionRushDialogView>
	{
		private global::Kampai.Game.LandExpansionConfig expansionDefinition;

		private global::Kampai.Game.BridgeBuilding bridgeBuilding;

		private global::Kampai.Game.Transaction.TransactionDefinition transactionDef;

		private global::System.Collections.IEnumerator PointerDownWait;

		[Inject]
		public global::Kampai.Game.ILandExpansionConfigService landExpansionConfigService { get; set; }

		[Inject]
		public global::Kampai.Game.PurchaseLandExpansionSignal purchaseSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RepairBridgeSignal repairBridgeSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.UI.View.DisplayItemPopupSignal displayItemPopupSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideItemPopupSignal hideItemPopupSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal playSFXSignal { get; set; }

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			dialogType = args.Get<global::Kampai.UI.View.RushDialogView.RushDialogType>();
			switch (dialogType)
			{
			case global::Kampai.UI.View.RushDialogView.RushDialogType.LAND_EXPANSION:
			{
				int expansion = args.Get<int>();
				expansionDefinition = landExpansionConfigService.GetExpansionConfig(expansion);
				transactionDef = base.definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(expansionDefinition.transactionId);
				break;
			}
			case global::Kampai.UI.View.RushDialogView.RushDialogType.BRIDGE_QUEST:
			{
				int id = args.Get<int>();
				bridgeBuilding = base.playerService.GetByInstanceId<global::Kampai.Game.BridgeBuilding>(id);
				if (bridgeBuilding.BridgeId == 0)
				{
					return;
				}
				global::Kampai.Game.BridgeDefinition bridgeDefinition = base.definitionService.Get(bridgeBuilding.BridgeId) as global::Kampai.Game.BridgeDefinition;
				transactionDef = base.definitionService.Get(bridgeDefinition.TransactionId) as global::Kampai.Game.Transaction.TransactionDefinition;
				questService.UpdateBridgeRepairTask(bridgeBuilding, global::Kampai.Game.QuestTaskTransition.Start);
				break;
			}
			default:
				base.logger.Error("Unsupported dialog type: {0}", dialogType);
				break;
			}
			LoadItems(transactionDef, dialogType);
		}

		protected override void OnMenuClose()
		{
			global::System.Collections.Generic.IList<global::Kampai.UI.View.RequiredItemView> itemList = base.view.GetItemList();
			if (itemList != null)
			{
				foreach (global::Kampai.UI.View.RequiredItemView item in itemList)
				{
					if (item != null)
					{
						item.pointerUpSignal.RemoveListener(PointerUp);
						item.pointerDownSignal.RemoveListener(PointerDown);
					}
				}
			}
			switch (dialogType)
			{
			case global::Kampai.UI.View.RushDialogView.RushDialogType.LAND_EXPANSION:
				base.hideSkrim.Dispatch("LandExpansionSkrim");
				break;
			case global::Kampai.UI.View.RushDialogView.RushDialogType.BRIDGE_QUEST:
				base.hideSkrim.Dispatch("BridgeSkrim");
				break;
			}
			base.guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "popup_Confirmation_Expansion");
		}

		private void PointerDown(int itemDefinitionID, global::UnityEngine.RectTransform rectTransform)
		{
			if (PointerDownWait != null)
			{
				routineRunner.StopCoroutine(PointerDownWait);
				PointerDownWait = null;
			}
			displayItemPopupSignal.Dispatch(itemDefinitionID, rectTransform, global::Kampai.UI.View.UIPopupType.GENERIC);
		}

		private void PointerUp()
		{
			if (PointerDownWait == null)
			{
				PointerDownWait = WaitASecond();
				routineRunner.StartCoroutine(PointerDownWait);
			}
		}

		private global::System.Collections.IEnumerator WaitASecond()
		{
			yield return new global::UnityEngine.WaitForSeconds(0.5f);
			hideItemPopupSignal.Dispatch();
		}

		protected override void PurchaseSuccess()
		{
			TryRunTheActualTransaction();
		}

		private void PerformTransactionSuccessAction()
		{
			switch (dialogType)
			{
			case global::Kampai.UI.View.RushDialogView.RushDialogType.LAND_EXPANSION:
				purchaseSignal.Dispatch(expansionDefinition.expansionId, true);
				break;
			case global::Kampai.UI.View.RushDialogView.RushDialogType.BRIDGE_QUEST:
				questService.UpdateBridgeRepairTask(bridgeBuilding, global::Kampai.Game.QuestTaskTransition.Complete);
				repairBridgeSignal.Dispatch(bridgeBuilding);
				break;
			}
			base.setGrindCurrencySignal.Dispatch();
		}

		protected override void Close()
		{
			if (dialogType == global::Kampai.UI.View.RushDialogView.RushDialogType.LAND_EXPANSION)
			{
				global::Kampai.Game.HighlightLandExpansionSignal instance = gameContext.injectionBinder.GetInstance<global::Kampai.Game.HighlightLandExpansionSignal>();
				instance.Dispatch(expansionDefinition.expansionId, false);
			}
			playSFXSignal.Dispatch("Play_menu_disappear_01");
			base.view.Close();
		}

		internal override global::Kampai.UI.View.RequiredItemView BuildItem(global::Kampai.Game.Definition definition, uint itemsInInventory, int itemsLack, bool hasCheckMark, global::Kampai.Main.ILocalizationService localService)
		{
			if (definition == null)
			{
				throw new global::System.ArgumentNullException("definition", "RequiredItemBuilder: You are passing in null definitions!");
			}
			global::UnityEngine.GameObject original = ((definition.ID != 0) ? (global::Kampai.Util.KampaiResources.Load("cmp_RequiredItem_ForShow") as global::UnityEngine.GameObject) : (global::Kampai.Util.KampaiResources.Load("cmp_CurrencyRequiredItem_ForShow") as global::UnityEngine.GameObject));
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(original) as global::UnityEngine.GameObject;
			global::Kampai.UI.View.RequiredItemView component = gameObject.GetComponent<global::Kampai.UI.View.RequiredItemView>();
			global::Kampai.Game.ItemDefinition itemDefinition = definition as global::Kampai.Game.ItemDefinition;
			global::UnityEngine.Sprite sprite = UIUtils.LoadSpriteFromPath(itemDefinition.Image);
			global::UnityEngine.Sprite maskSprite = UIUtils.LoadSpriteFromPath(itemDefinition.Mask);
			component.ItemIcon.sprite = sprite;
			component.ItemIcon.maskSprite = maskSprite;
			int num = (int)itemsInInventory + itemsLack;
			int num2 = ((itemsLack >= 0) ? itemsLack : 0);
			component.ItemNeeded = num;
			component.ItemDefinitionID = itemDefinition.ID;
			if (definition.ID == 0)
			{
				component.ItemQuantity.text = string.Format("{0}", num);
			}
			else
			{
				component.ItemQuantity.text = string.Format("{0}/{1}", itemsInInventory, num);
			}
			component.CheckMark.SetActive(hasCheckMark);
			component.PurchasePanel.SetActive(!hasCheckMark);
			if (!hasCheckMark)
			{
				component.ItemQuantity.color = global::UnityEngine.Color.red;
			}
			int num3 = global::UnityEngine.Mathf.FloorToInt(itemDefinition.BasePremiumCost * (float)num2);
			num3 = ((num3 == 0 && num2 > 0) ? 1 : num3);
			component.ItemCost.text = num3.ToString();
			if (!hasCheckMark)
			{
				global::Kampai.Util.QuantityItem item = new global::Kampai.Util.QuantityItem(itemDefinition.ID, (uint)num2);
				component.RushBtn.RushCost = num3;
				component.RushBtn.Item = item;
			}
			component.pointerUpSignal.AddListener(PointerUp);
			component.pointerDownSignal.AddListener(PointerDown);
			return component;
		}

		private global::Kampai.UI.View.RequiredItemView BuildGreyoutItem(global::Kampai.Game.Definition definition)
		{
			if (definition == null)
			{
				throw new global::System.ArgumentNullException("definition", "RequiredItemBuilder: You are passing in null definitions!");
			}
			global::UnityEngine.GameObject original = global::Kampai.Util.KampaiResources.Load("cmp_RequiredItem_ForShow") as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(original) as global::UnityEngine.GameObject;
			global::Kampai.UI.View.RequiredItemView component = gameObject.GetComponent<global::Kampai.UI.View.RequiredItemView>();
			global::Kampai.Game.ItemDefinition itemDefinition = definition as global::Kampai.Game.ItemDefinition;
			global::UnityEngine.Sprite sprite = UIUtils.LoadSpriteFromPath("img_fill_128");
			global::UnityEngine.Sprite maskSprite = UIUtils.LoadSpriteFromPath(itemDefinition.Mask);
			component.ItemIcon.sprite = sprite;
			component.ItemIcon.maskSprite = maskSprite;
			component.ItemIcon.color = global::UnityEngine.Color.gray;
			component.ItemQuantity.text = string.Format("{0}", 0);
			component.ItemDefinitionID = itemDefinition.ID;
			component.CheckMark.SetActive(false);
			component.PurchasePanel.SetActive(false);
			component.ItemCost.text = 0.ToString();
			component.pointerUpSignal.AddListener(PointerUp);
			component.pointerDownSignal.AddListener(PointerDown);
			return component;
		}

		protected override void RushTransactionCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			purchaseInProgress = false;
			if (pct.Success)
			{
				isClosing = true;
				PurchaseSuccess();
				base.soundFXSignal.Dispatch("Play_button_premium_01");
				base.setPremiumCurrencySignal.Dispatch();
				base.setGrindCurrencySignal.Dispatch();
			}
			else if (pct.ParentSuccess)
			{
				PurchaseButtonClicked();
			}
		}

		private void ExpansionTransactionCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			if (pct.Success)
			{
				PerformTransactionSuccessAction();
				Close();
			}
		}

		private void TryRunTheActualTransaction()
		{
			switch (dialogType)
			{
			case global::Kampai.UI.View.RushDialogView.RushDialogType.BRIDGE_QUEST:
				base.playerService.RunEntireTransaction(transactionDef, global::Kampai.Game.TransactionTarget.REPAIR_BRIDGE, ExpansionTransactionCallback);
				break;
			case global::Kampai.UI.View.RushDialogView.RushDialogType.LAND_EXPANSION:
				base.playerService.RunEntireTransaction(transactionDef, global::Kampai.Game.TransactionTarget.LAND_EXPANSION, ExpansionTransactionCallback);
				break;
			}
		}

		protected override void LoadItems(global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition, global::Kampai.UI.View.RushDialogView.RushDialogType type)
		{
			if (type == global::Kampai.UI.View.RushDialogView.RushDialogType.LAND_EXPANSION)
			{
				LoadExpansionItems(transactionDefinition, type);
			}
			else
			{
				LoadNormalItems(transactionDef, type);
			}
		}

		private void LoadExpansionItems(global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition, global::Kampai.UI.View.RushDialogView.RushDialogType type)
		{
			global::System.Collections.Generic.IList<int> list = new global::System.Collections.Generic.List<int>();
			list.Add(235);
			list.Add(290);
			list.Add(291);
			global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> inputs = transactionDefinition.Inputs;
			if (inputs == null)
			{
				return;
			}
			int count = list.Count;
			bool showPurchaseButton = false;
			requiredItems = new global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem>();
			requiredItemPremiumCosts = new global::System.Collections.Generic.List<int>();
			for (int i = 0; i < inputs.Count; i++)
			{
				int iD = inputs[i].ID;
				global::Kampai.Game.ItemDefinition definition = base.definitionService.Get<global::Kampai.Game.ItemDefinition>(iD);
				int num = list.IndexOf(iD);
				global::Kampai.Util.QuantityItem quantityItem = null;
				uint quantityByDefinitionId = base.playerService.GetQuantityByDefinitionId(iD);
				int num2 = (int)(inputs[i].Quantity - quantityByDefinitionId);
				bool flag = false;
				if (num2 <= 0)
				{
					flag = true;
				}
				else
				{
					flag = false;
					quantityItem = new global::Kampai.Util.QuantityItem(iD, (uint)num2);
					requiredItems.Add(quantityItem);
				}
				global::Kampai.UI.View.RequiredItemView requiredItemView = BuildItem(definition, quantityByDefinitionId, num2, flag, base.localService);
				if (!flag)
				{
					requiredItemView.RushBtn.RushButtonClickedSignal.AddListener(base.IndividualRushButtonClicked);
				}
				if (global::System.Convert.ToInt32(requiredItemView.ItemCost.text) != 0)
				{
					requiredItemPremiumCosts.Add(global::System.Convert.ToInt32(requiredItemView.ItemCost.text));
				}
				if (num != -1)
				{
					base.view.AddRequiredItem(requiredItemView, num, base.view.ScrollViewParent);
					list[num] = -1;
				}
				else
				{
					base.view.AddRequiredItem(requiredItemView, -1, base.view.CurrencyScrollViewParent);
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				int num3 = list[j];
				if (num3 != -1)
				{
					global::Kampai.Game.ItemDefinition definition2 = base.definitionService.Get<global::Kampai.Game.ItemDefinition>(num3);
					global::Kampai.UI.View.RequiredItemView requiredItemView2 = BuildGreyoutItem(definition2);
					base.view.AddRequiredItem(requiredItemView2, j, base.view.ScrollViewParent);
				}
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

		private void LoadNormalItems(global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition, global::Kampai.UI.View.RushDialogView.RushDialogType type)
		{
			global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> inputs = transactionDefinition.Inputs;
			if (inputs != null)
			{
				int count = inputs.Count;
				int num = -1;
				int index = 0;
				bool showPurchaseButton = false;
				requiredItems = new global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem>();
				requiredItemPremiumCosts = new global::System.Collections.Generic.List<int>();
				for (int i = 0; i < count; i++)
				{
					global::Kampai.Game.ItemDefinition itemDefinition = base.definitionService.Get<global::Kampai.Game.ItemDefinition>(inputs[i].ID);
					if (itemDefinition.ID == 0)
					{
						num = i;
					}
					global::Kampai.Util.QuantityItem quantityItem = null;
					uint quantityByDefinitionId = base.playerService.GetQuantityByDefinitionId(inputs[i].ID);
					int num2 = (int)(inputs[i].Quantity - quantityByDefinitionId);
					bool flag = false;
					if (num2 <= 0)
					{
						flag = true;
					}
					else
					{
						flag = false;
						quantityItem = new global::Kampai.Util.QuantityItem(inputs[i].ID, (uint)num2);
						requiredItems.Add(quantityItem);
					}
					global::Kampai.UI.View.RequiredItemView requiredItemView = BuildItem(itemDefinition, quantityByDefinitionId, num2, flag, base.localService);
					if (!flag)
					{
						requiredItemView.RushBtn.RushButtonClickedSignal.AddListener(base.IndividualRushButtonClicked);
					}
					if (global::System.Convert.ToInt32(requiredItemView.ItemCost.text) != 0)
					{
						requiredItemPremiumCosts.Add(global::System.Convert.ToInt32(requiredItemView.ItemCost.text));
					}
					if (num < 0)
					{
						index = i;
					}
					else if (num == i)
					{
						index = -1;
					}
					else if (i > num)
					{
						index = i - 1;
					}
					base.view.AddRequiredItem(requiredItemView, index, (num != i) ? base.view.ScrollViewParent : base.view.CurrencyScrollViewParent);
				}
				if (requiredItems.Count != 0)
				{
					rushCost = base.playerService.CalculateRushCost(requiredItems);
					base.view.SetupItemCost(rushCost);
					showPurchaseButton = true;
				}
				if (num < 0)
				{
					base.view.SetupItemCount(count);
				}
				else
				{
					base.view.SetupItemCount(count - 1);
				}
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
