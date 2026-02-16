namespace Kampai.UI.View
{
	public class CurrencyMenuMediator : global::strange.extensions.mediation.impl.EventMediator
	{
		[Inject]
		public global::Kampai.UI.View.CurrencyMenuView view { get; set; }

		[Inject]
		public global::Kampai.UI.View.CurrencyMenuDefinitionLoadedSignal defLoadedSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.PremiumCurrencyCatalogUpdatedSignal premiumCurrencyCatalogUpdatedSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CurrencyButtonClickSignal clickedSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CloseHUDSignal closeSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ICurrencyService currencyService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.StartPremiumPurchaseSignal startPremiumPurchaseSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		public override void OnRegister()
		{
			defLoadedSignal.AddListener(OnDefinitionLoaded);
			premiumCurrencyCatalogUpdatedSignal.AddListener(OnPremiumCatalogUpdated);
			view.OnMenuClose.AddListener(OnMenuClose);
			view.Init();
			if (playerService.GetHighestFtueCompleted() < 7)
			{
				questService.PauseQuestScripts();
			}
		}

		public override void OnRemove()
		{
			defLoadedSignal.RemoveListener(OnDefinitionLoaded);
			premiumCurrencyCatalogUpdatedSignal.RemoveListener(OnPremiumCatalogUpdated);
			view.OnMenuClose.RemoveListener(OnMenuClose);
			if (playerService.GetHighestFtueCompleted() < 7)
			{
				questService.ResumeQuestScripts();
			}
		}

		internal void OnDefinitionLoaded(global::System.Collections.Generic.List<global::Kampai.Game.StoreItemDefinition> storeItemDefs)
		{
			for (int i = 0; i < storeItemDefs.Count; i++)
			{
				global::Kampai.Game.StoreItemDefinition storeItemDefinition = storeItemDefs[i];
				global::Kampai.Game.CurrencyItemDefinition currencyItemDefinition = definitionService.Get<global::Kampai.Game.CurrencyItemDefinition>(storeItemDefinition.ReferencedDefID);
				global::Kampai.Game.PremiumCurrencyItemDefinition premiumCurrencyItemDefinition = currencyItemDefinition as global::Kampai.Game.PremiumCurrencyItemDefinition;
				global::Kampai.Game.Transaction.TransactionDefinition transaction = definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(storeItemDefinition.TransactionID);
				string inputStr;
				string outputStr;
				if (premiumCurrencyItemDefinition != null)
				{
					inputStr = currencyService.GetPriceWithCurrencyAndFormat(premiumCurrencyItemDefinition.SKU);
					outputStr = ((storeItemDefinition.Type != global::Kampai.Game.StoreItemType.PremiumCurrency) ? global::Kampai.Game.Transaction.TransactionUtil.GetGrindOutputForTransaction(transaction).ToString() : global::Kampai.Game.Transaction.TransactionUtil.GetPremiumOutputForTransaction(transaction).ToString());
				}
				else
				{
					inputStr = global::Kampai.Game.Transaction.TransactionUtil.GetPremiumCostForTransaction(transaction).ToString();
					outputStr = global::Kampai.Game.Transaction.TransactionUtil.GetGrindOutputForTransaction(transaction).ToString();
				}
				global::Kampai.UI.View.CurrencyButtonView currencyButtonView = global::Kampai.UI.View.CurrencyButtonBuilder.Build(localService, currencyItemDefinition, inputStr, outputStr, view.ScrollViewParent, storeItemDefinition.Type);
				currencyButtonView.Definition = storeItemDefinition;
				view.AddCurrencyButton(storeItemDefinition.Type, currencyButtonView);
				currencyButtonView.ClickedSignal.AddListener(OnCurrencyButtonClicked);
			}
			view.ReorganizeButtons();
		}

		private void OnPremiumCatalogUpdated()
		{
			for (int i = 0; i < view.premiumCurrencyButtons.Count; i++)
			{
				global::Kampai.UI.View.CurrencyButtonView currencyButtonView = view.premiumCurrencyButtons[i];
				global::Kampai.Game.StoreItemDefinition definition = currencyButtonView.Definition;
				global::Kampai.Game.CurrencyItemDefinition currencyItemDefinition = definitionService.Get<global::Kampai.Game.CurrencyItemDefinition>(definition.ReferencedDefID);
				global::Kampai.Game.PremiumCurrencyItemDefinition premiumCurrencyItemDefinition = currencyItemDefinition as global::Kampai.Game.PremiumCurrencyItemDefinition;
				if (premiumCurrencyItemDefinition != null)
				{
					currencyButtonView.ItemPrice.text = currencyService.GetPriceWithCurrencyAndFormat(premiumCurrencyItemDefinition.SKU);
				}
			}
		}

		private void OnCurrencyButtonClicked(global::Kampai.Game.StoreItemDefinition definition)
		{
			closeSignal.Dispatch(false);
			routineRunner.DelayAction(new global::UnityEngine.WaitForSeconds((global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.Android) ? 0f : 0.35f), delegate
			{
				clickedSignal.Dispatch(definition);
			});
		}

		private void OnMenuClose()
		{
			view.gameObject.SetActive(false);
		}
	}
}
