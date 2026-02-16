namespace Kampai.UI.View
{
	public class MoveBuildingMenuMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.MoveBuildingMenuView>
	{
		[global::System.Flags]
		public enum Buttons
		{
			None = 0,
			Inventory = 1,
			Accept = 4,
			Close = 8,
			All = 0x10
		}

		[Inject]
		public global::Kampai.Game.RotateBuildingSignal rotateSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal playSFXSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowMoveBuildingMenuSignal showMenuSignal { get; set; }

		[Inject]
		public global::Kampai.Game.UpdateMovementValidity updateSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideAllWayFindersSignal hideAllWayFindersSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowAllWayFindersSignal showAllWayFindersSignal { get; set; }

		[Inject]
		public global::Kampai.UI.IPositionService positionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localizationService { get; set; }

		[Inject]
		public global::Kampai.UI.View.DisableMoveToInventorySignal disableSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.UIModel model { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void OnRegister()
		{
			model.AllowMultiTouch = true;
			updateSignal.AddListener(UpdateValidity);
			disableSignal.AddListener(DisableInventory);
			base.view.Init(positionService, gameContext, logger, playerService, localizationService);
			base.view.InventoryButton.ClickedSignal.AddListener(OnInventory);
			base.view.AcceptButton.ClickedSignal.AddListener(OnAccept);
			base.view.CloseButton.ClickedSignal.AddListener(OnClose);
			base.OnRegister();
		}

		public override void OnRemove()
		{
			model.AllowMultiTouch = false;
			updateSignal.RemoveListener(UpdateValidity);
			base.view.InventoryButton.ClickedSignal.RemoveListener(OnInventory);
			base.view.AcceptButton.ClickedSignal.RemoveListener(OnAccept);
			base.view.CloseButton.ClickedSignal.RemoveListener(OnClose);
			disableSignal.RemoveListener(DisableInventory);
			base.OnRemove();
		}

		private void OnInventory()
		{
			playSFXSignal.Dispatch("Play_low_woosh_01");
			ToInventory();
		}

		private void OnAccept()
		{
			Confirm();
		}

		private void OnClose()
		{
			Cancel();
		}

		private void DisableInventory()
		{
			base.view.DisableInventory();
		}

		private void Cancel()
		{
			global::Kampai.Game.CancelBuildingMovementSignal instance = gameContext.injectionBinder.GetInstance<global::Kampai.Game.CancelBuildingMovementSignal>();
			instance.Dispatch(false);
		}

		private void Confirm()
		{
			global::Kampai.Game.ConfirmBuildingMovementSignal instance = gameContext.injectionBinder.GetInstance<global::Kampai.Game.ConfirmBuildingMovementSignal>();
			instance.Dispatch();
		}

		private void ToInventory()
		{
			global::Kampai.Game.InventoryBuildingMovementSignal instance = gameContext.injectionBinder.GetInstance<global::Kampai.Game.InventoryBuildingMovementSignal>();
			instance.Dispatch();
		}

		private void UpdateValidity(bool enable)
		{
			base.view.UpdateValidity(enable);
		}

		protected override void Close()
		{
			Cancel();
		}
	}
}
