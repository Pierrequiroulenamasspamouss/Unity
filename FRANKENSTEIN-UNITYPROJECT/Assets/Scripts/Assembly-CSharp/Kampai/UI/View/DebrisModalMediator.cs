namespace Kampai.UI.View
{
	public class DebrisModalMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.DebrisModalView>
	{
		private global::Kampai.Game.DebrisBuilding debrisBuilding;

		private global::Kampai.Game.Transaction.TransactionDefinition transactionDef;

		private int itemsRequired;

		private int itemDefId;

		[Inject]
		public global::Kampai.Game.CallMinionSignal CallMinionSignal { get; set; }

		[Inject]
		public global::Kampai.Common.IPickService PickService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService DefinitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService PlayerService { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetGrindCurrencySignal SetGrindCurrencySignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SpawnDooberSignal TweenSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal HideSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService GUIService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService LocalizationService { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable GameContext { get; set; }

		[Inject]
		public global::Kampai.UI.View.OnDropItemOverDropAreaSignal OnDropItemOverDropAreaSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.OnDragItemOverDropAreaSignal OnDragItemOverDropAreaSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RushDialogConfirmationSignal RushedSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowNeedXMinionsSignal ShowNeedXMinionsSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger Logger { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			base.view.OnMenuClose.AddListener(OnMenuClose);
			RushedSignal.AddListener(OnPurchaseItem);
			OnDragItemOverDropAreaSignal.AddListener(OnDragItemOverDropArea);
			OnDropItemOverDropAreaSignal.AddListener(OnDropItemOverDropArea);
			GameContext.injectionBinder.GetInstance<global::Kampai.Game.ToggleVignetteSignal>().Dispatch(true, 0f);
			GameContext.injectionBinder.GetInstance<global::Kampai.Game.IdleMinionSignal>().AddListener(UpdateMinionsAvailable);
		}

		public override void OnRemove()
		{
			base.OnRemove();
			base.view.OnMenuClose.RemoveListener(OnMenuClose);
			RushedSignal.RemoveListener(OnPurchaseItem);
			OnDragItemOverDropAreaSignal.RemoveListener(OnDragItemOverDropArea);
			OnDropItemOverDropAreaSignal.RemoveListener(OnDropItemOverDropArea);
			GameContext.injectionBinder.GetInstance<global::Kampai.Game.ToggleVignetteSignal>().Dispatch(false, null);
			GameContext.injectionBinder.GetInstance<global::Kampai.Game.IdleMinionSignal>().RemoveListener(UpdateMinionsAvailable);
		}

		private void UpdateMinionsAvailable()
		{
			int count = PlayerService.GetIdleMinions().Count;
			base.view.UpdateAvailableMinions(count);
		}

		private void OnDragItemOverDropArea(global::Kampai.UI.View.DragDropItemView dragDropItemView, bool success)
		{
			base.view.OnDragItemOverDropArea(dragDropItemView, success);
		}

		private void OnDropItemOverDropArea(global::Kampai.UI.View.DragDropItemView dragDropItemView, bool success)
		{
			base.view.OnDropItemOverDropArea(dragDropItemView, success);
			if (success)
			{
				if (base.view.MinionsAvailable < 1)
				{
					ShowNeedXMinionsSignal.Dispatch(1);
				}
				else
				{
					RunTransaction();
				}
			}
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			global::Kampai.Game.DebrisBuilding debrisBuilding = args.Get<global::Kampai.Game.DebrisBuilding>();
			if (debrisBuilding != null)
			{
				this.debrisBuilding = debrisBuilding;
				transactionDef = DefinitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(this.debrisBuilding.GetTransactionID(DefinitionService));
				int count = PlayerService.GetIdleMinions().Count;
				global::Kampai.Util.QuantityItem quantityItem = transactionDef.Inputs[0];
				itemDefId = quantityItem.ID;
				itemsRequired = (int)quantityItem.Quantity;
				int quantityByDefinitionId = (int)PlayerService.GetQuantityByDefinitionId(itemDefId);
				global::Kampai.Game.ItemDefinition itemDefinition = DefinitionService.Get<global::Kampai.Game.ItemDefinition>(itemDefId);
				string image = itemDefinition.Image;
				string mask = itemDefinition.Mask;
				base.view.Init(count, quantityByDefinitionId, itemsRequired, image, mask, LocalizationService, this.debrisBuilding.Definition);
				base.Initialize(args);
			}
		}

		private void OnPurchaseItem()
		{
			RunTransaction();
		}

		private void RunTransaction()
		{
			PlayerService.StartTransaction(transactionDef, global::Kampai.Game.TransactionTarget.CLEAR_DEBRIS, TransactionCallback, new global::Kampai.Game.TransactionArg(debrisBuilding.ID));
		}

		private void PerformTransactionSuccessAction()
		{
			CallMinions();
		}

		protected override void Close()
		{
			soundFXSignal.Dispatch("Play_menu_disappear_01");
			base.view.Close();
		}

		private void CallMinions()
		{
			int minionSlotsOwned = debrisBuilding.MinionSlotsOwned;
			for (int i = 0; i < minionSlotsOwned; i++)
			{
				CallMinionSignal.Dispatch(debrisBuilding, base.view.gameObject);
			}
		}

		private void TransactionCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			if (pct.Success)
			{
				PlayerService.FinishTransaction(pct.GetPendingTransaction(), global::Kampai.Game.TransactionTarget.CLEAR_DEBRIS, new global::Kampai.Game.TransactionArg(debrisBuilding.ID));
				PerformTransactionSuccessAction();
				Close();
			}
		}

		private void OnMenuClose()
		{
			HideSignal.Dispatch("DebrisSkrim");
			GUIService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "screen_ClearDebris");
		}
	}
}
