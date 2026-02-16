namespace Kampai.UI.View
{
	public class BuildMenuMediator : global::strange.extensions.mediation.impl.EventMediator
	{
		[Inject]
		public global::Kampai.UI.View.BuildMenuView view { get; set; }

		[Inject]
		public global::Kampai.UI.View.MoveBuildMenuSignal moveSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CloseAllOtherMenuSignal closeAllMenuSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.LoadDefinitionForUISignal loadDefinitionSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal playSFXSignal { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.UI.View.BuildMenuButtonClickedSignal openButtonClickedSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowStoreSignal showStoreSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.BuildMenuOpenedSignal buildMenuOpenedSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetNewUnlockForBuildMenuSignal setNewUnlockForBuildMenuSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetInventoryCountForBuildMenuSignal setInventoryCountForBuildMenuSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideStoreHighlightSignal hideStoreHighlightSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.UI.IBuildMenuService buildMenuService { get; set; }

		[Inject]
		public global::Kampai.UI.View.UIAddedSignal uiAddedSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.UIRemovedSignal uiRemovedSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetBuildMenuEnabledSignal setBuildMenuEnabledSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.StopAutopanSignal stopAutopanSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RemoveUnlockForBuildMenuSignal removeUnlockForBuildMenuSignal { get; set; }

		public override void OnRegister()
		{
			global::Kampai.Util.TimeProfiler.StartSection("BuildMenuMediator");
			view.Init();
			loadDefinitionSignal.Dispatch();
			view.MenuButton.ClickedSignal.AddListener(OnMenuButtonClicked);
			closeAllMenuSignal.AddListener(CloseAllMenu);
			moveSignal.AddListener(MoveMenu);
			setBuildMenuEnabledSignal.AddListener(SetBuildMenuEnabled);
			global::Kampai.Game.InventoryBuildingMovementSignal instance = gameContext.injectionBinder.GetInstance<global::Kampai.Game.InventoryBuildingMovementSignal>();
			instance.AddListener(ToInventory);
			showStoreSignal.AddListener(ToggleStore);
			setNewUnlockForBuildMenuSignal.AddListener(SetNewUnlock);
			removeUnlockForBuildMenuSignal.AddListener(RemoveUnlock);
			setInventoryCountForBuildMenuSignal.AddListener(SetInventoryCount);
			global::Kampai.Util.TimeProfiler.EndSection("BuildMenuMediator");
		}

		public override void OnRemove()
		{
			view.MenuButton.ClickedSignal.RemoveListener(OnMenuButtonClicked);
			moveSignal.RemoveListener(MoveMenu);
			global::Kampai.Game.InventoryBuildingMovementSignal instance = gameContext.injectionBinder.GetInstance<global::Kampai.Game.InventoryBuildingMovementSignal>();
			instance.RemoveListener(ToInventory);
			closeAllMenuSignal.RemoveListener(CloseAllMenu);
			setBuildMenuEnabledSignal.RemoveListener(SetBuildMenuEnabled);
			showStoreSignal.RemoveListener(ToggleStore);
			setNewUnlockForBuildMenuSignal.RemoveListener(SetNewUnlock);
			removeUnlockForBuildMenuSignal.RemoveListener(RemoveUnlock);
		}

		internal void ToInventory()
		{
			view.IncreaseBadgeCounter();
			buildMenuService.SetStoreUnlockCheckedState(false);
		}

		internal void RemoveUnlock(int count)
		{
			view.RemoveUnlockBadge(count);
		}

		internal void SetNewUnlock(int count)
		{
			view.SetUnlockBadge(count);
			buildMenuService.SetStoreUnlockCheckedState(false);
		}

		internal void SetInventoryCount(int count)
		{
			view.SetInventoryCount(count);
			buildMenuService.SetStoreUnlockCheckedState(false);
		}

		internal void CloseAllMenu(global::UnityEngine.GameObject exception)
		{
			if (base.gameObject != exception && view.isOpen)
			{
				moveSignal.Dispatch(false);
			}
		}

		internal void OnMenuButtonClicked()
		{
			if (global::UnityEngine.Input.touchCount <= 1)
			{
				telemetryService.Send_Telemetry_EVT_IGE_STORE_VISIT("Menu Button", "Building Menu");
				moveSignal.Dispatch(!view.isOpen);
				if (view.isOpen)
				{
					buildMenuService.SetStoreUnlockCheckedState(true);
					buildMenuOpenedSignal.Dispatch();
					closeAllMenuSignal.Dispatch(base.gameObject);
					global::UnityEngine.Input.multiTouchEnabled = true;
				}
				openButtonClickedSignal.Dispatch();
			}
		}

		internal void MoveMenu(bool show)
		{
			if (show != view.isOpen)
			{
				if (show)
				{
					stopAutopanSignal.Dispatch();
				}
				view.MoveMenu(show);
				if (show)
				{
					uiAddedSignal.Dispatch(view.gameObject, OnMenuButtonClicked);
					playSFXSignal.Dispatch("Play_main_menu_open_01");
				}
				else
				{
					uiRemovedSignal.Dispatch(view.gameObject);
					hideStoreHighlightSignal.Dispatch();
					playSFXSignal.Dispatch("Play_main_menu_close_01");
				}
			}
		}

		internal void ToggleStore(bool enable)
		{
			if (enable)
			{
				view.Show();
			}
			else
			{
				view.Hide();
			}
			view.MenuButton.GetComponent<global::UnityEngine.UI.Button>().enabled = enable;
		}

		private void SetBuildMenuEnabled(bool isEnabled)
		{
			view.SetBuildMenuButtonEnabled(isEnabled);
		}
	}
}
