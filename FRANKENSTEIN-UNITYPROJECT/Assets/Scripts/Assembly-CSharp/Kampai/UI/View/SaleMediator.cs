namespace Kampai.UI.View
{
	public class SaleMediator : global::strange.extensions.mediation.impl.Mediator
	{
		private global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition;

		[Inject]
		public global::Kampai.UI.View.SaleView view { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localization { get; set; }

		[Inject(global::Kampai.UI.View.UIElement.CAMERA)]
		public global::UnityEngine.Camera uiCamera { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SpawnDooberSignal tweenSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.UI.View.TogglePopupForHUDSignal HUDSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.InitializeSaleSignal initializeSignal { get; set; }

		public override void OnRegister()
		{
			initializeSignal.AddListener(StoreTransaction);
			view.closeButton.ClickedSignal.AddListener(Close);
			view.purchaseButton.ClickedSignal.AddListener(Purchase);
			view.OnMenuClose.AddListener(OnMenuClose);
			HUDSignal.Dispatch(true);
		}

		public override void OnRemove()
		{
			initializeSignal.RemoveListener(StoreTransaction);
			view.closeButton.ClickedSignal.RemoveListener(Close);
			view.purchaseButton.ClickedSignal.RemoveListener(Purchase);
			view.OnMenuClose.RemoveListener(OnMenuClose);
			HUDSignal.Dispatch(false);
		}

		private void StoreTransaction(global::Kampai.Game.SaleDefinition sale)
		{
			transactionDefinition = definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(sale.TransactionDefinitionID);
			view.Init(sale, transactionDefinition, definitionService, localization, logger);
		}

		private void Purchase()
		{
			playerService.RunEntireTransaction(transactionDefinition, global::Kampai.Game.TransactionTarget.QUEST_REWARD, CollectTransactionCallback);
		}

		private void Close()
		{
			soundFXSignal.Dispatch("Play_main_menu_close_01");
			view.Close();
		}

		private void OnMenuClose()
		{
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "popup_Sale");
		}

		private void CollectTransactionCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			if (pct.Success)
			{
				DooberUtil.CheckForTween(transactionDefinition, view.viewList, true, uiCamera, tweenSignal, definitionService);
				soundFXSignal.Dispatch("Play_button_click_01");
				Close();
			}
		}
	}
}
