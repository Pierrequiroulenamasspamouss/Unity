namespace Kampai.UI.View
{
	public class HUDMediator : global::strange.extensions.mediation.impl.EventMediator
	{
		private global::Kampai.Util.MutableBoxed<bool> currentPeekToken;

		private global::Kampai.Game.StorageBuilding storage;

		[Inject]
		public global::Kampai.UI.View.HUDView view { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.UI.View.CloseHUDSignal closeSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CloseAllOtherMenuSignal closeAllOtherMenusSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetLevelSignal setLevelSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetXPSignal setXPSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetGrindCurrencySignal setGrindCurrencySignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetPremiumCurrencySignal setPremiumCurrencySignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetStorageCapacitySignal setStorageSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal playSFXSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowPremiumStoreSignal showPremiumStoreSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowGrindStoreSignal showGrindStoreSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowHUDSignal showHUDSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowSettingsButtonSignal showSettingsButtonSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.PeekHUDSignal peekHUDSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.TogglePopupForHUDSignal togglePopupSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetHUDButtonsVisibleSignal setHUDButtonsVisibleSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ICurrencyService currencyService { get; set; }

		[Inject]
		public global::Kampai.UI.View.UIAddedSignal uiAddedSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.UIRemovedSignal uiRemovedSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.XPFTUEHighlightSignal ftueHighlightSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowAllWayFindersSignal showAllWayFindersSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideAllWayFindersSignal hideAllWayFindersSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowAllResourceIconsSignal showAllResourceIconsSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideAllResourceIconsSignal hideAllResourceIconsSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HUDChangedSiblingIndexSignal hudChangingSiblingIndexSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.FireXPVFXSignal fireXpSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.FirePremiumVFXSignal firePremiumSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.FireGrindVFXSignal fireGrindSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideDelayHUDSignal hideDelayHUDSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CheckForLevelSignal levelSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.StopAutopanSignal stopAutopanSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.UI.View.CurrencyDialogClosedSignal currencyDialogClosedSignal { get; set; }

		[Inject]
		public global::Kampai.Game.OpenStorageBuildingSignal OpenStorageBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Common.ICoppaService coppaService { get; set; }

		public override void OnRegister()
		{
			view.Init(hudChangingSiblingIndexSignal, localService, COPPACompliant);
			setStorageSignal.AddListener(SetStorage);
			setLevelSignal.AddListener(SetLevel);
			setXPSignal.AddListener(SetXP);
			closeAllOtherMenusSignal.AddListener(CloseAllMenu);
			closeSignal.AddListener(CloseMenu);
			view.MenuMoved.AddListener(MenuMoved);
			view.StorageButton.ClickedSignal.AddListener(OpenStorage);
			showHUDSignal.AddListener(ToggleHUD);
			showSettingsButtonSignal.AddListener(ToggleSettings);
			peekHUDSignal.AddListener(PeekHUD);
			hideDelayHUDSignal.AddListener(SetupHide);
			setHUDButtonsVisibleSignal.AddListener(SetButtonsVisible);
			togglePopupSignal.AddListener(TogglePopup);
			ftueHighlightSignal.AddListener(ShowFTUEXP);
			currencyService.ResumeTransactionsHandling();
			fireXpSignal.AddListener(PlayXPVFX);
			firePremiumSignal.AddListener(PlayPremiumVFX);
			fireGrindSignal.AddListener(PlayGrindVFX);
			currencyDialogClosedSignal.AddListener(OnCurrencyDone);
			RegisterCurrencySignals();
			RegisterViewSignals();
		}

		public override void OnRemove()
		{
			setStorageSignal.RemoveListener(SetStorage);
			setLevelSignal.RemoveListener(SetLevel);
			setXPSignal.RemoveListener(SetXP);
			closeAllOtherMenusSignal.RemoveListener(CloseAllMenu);
			closeSignal.RemoveListener(CloseMenu);
			view.MenuMoved.RemoveListener(MenuMoved);
			view.StorageButton.ClickedSignal.RemoveListener(OpenStorage);
			showHUDSignal.RemoveListener(ToggleHUD);
			showSettingsButtonSignal.RemoveListener(ToggleSettings);
			peekHUDSignal.RemoveListener(PeekHUD);
			hideDelayHUDSignal.RemoveListener(SetupHide);
			setHUDButtonsVisibleSignal.RemoveListener(SetButtonsVisible);
			togglePopupSignal.RemoveListener(TogglePopup);
			ftueHighlightSignal.RemoveListener(ShowFTUEXP);
			fireXpSignal.RemoveListener(PlayXPVFX);
			firePremiumSignal.RemoveListener(PlayPremiumVFX);
			fireGrindSignal.RemoveListener(PlayGrindVFX);
			currencyDialogClosedSignal.RemoveListener(OnCurrencyDone);
			RemoveCurrencySignals();
			RemoveViewSignals();
		}

		private void RegisterCurrencySignals()
		{
			setGrindCurrencySignal.AddListener(SetGrindCurrency);
			setPremiumCurrencySignal.AddListener(SetPremiumCurrency);
			showPremiumStoreSignal.AddListener(ShowPremiumStore);
			showGrindStoreSignal.AddListener(ShowGrindStore);
			view.PremiumMenuButton.ClickedSignal.AddListener(OnPremiumButtonClicked);
			view.PremiumIconButton.ClickedSignal.AddListener(OnPremiumButtonClicked);
			view.PremiumTextButton.ClickedSignal.AddListener(OnPremiumButtonClicked);
			view.GrindMenuButton.ClickedSignal.AddListener(OnGrindButtonClicked);
			view.GrindIconButton.ClickedSignal.AddListener(OnGrindButtonClicked);
			view.GrindTextButton.ClickedSignal.AddListener(OnGrindButtonClicked);
			view.BackgroundButton.ClickedSignal.AddListener(CloseMenuAndCurrency);
		}

		private void RemoveCurrencySignals()
		{
			setGrindCurrencySignal.RemoveListener(SetGrindCurrency);
			setPremiumCurrencySignal.RemoveListener(SetPremiumCurrency);
			showPremiumStoreSignal.RemoveListener(ShowPremiumStore);
			showGrindStoreSignal.RemoveListener(ShowGrindStore);
			view.PremiumMenuButton.ClickedSignal.RemoveListener(OnPremiumButtonClicked);
			view.PremiumIconButton.ClickedSignal.RemoveListener(OnPremiumButtonClicked);
			view.PremiumTextButton.ClickedSignal.RemoveListener(OnPremiumButtonClicked);
			view.GrindMenuButton.ClickedSignal.RemoveListener(OnGrindButtonClicked);
			view.GrindIconButton.ClickedSignal.RemoveListener(OnGrindButtonClicked);
			view.GrindTextButton.ClickedSignal.RemoveListener(OnGrindButtonClicked);
			view.BackgroundButton.ClickedSignal.RemoveListener(CloseMenuAndCurrency);
		}

		private bool COPPACompliant()
		{
			return coppaService.Restricted();
		}

		private void RegisterViewSignals()
		{
			view.checkLevelSignal.AddListener(CheckLevel);
			view.animateXP.AddListener(SetXP);
		}

		private void RemoveViewSignals()
		{
			view.checkLevelSignal.RemoveListener(CheckLevel);
			view.animateXP.RemoveListener(SetXP);
		}

		private void CheckLevel()
		{
			levelSignal.Dispatch();
		}

		private void DisplayAllWorldToGlassUI(bool display)
		{
			if (display)
			{
				showAllWayFindersSignal.Dispatch();
				showAllResourceIconsSignal.Dispatch();
			}
			else
			{
				hideAllWayFindersSignal.Dispatch();
				hideAllResourceIconsSignal.Dispatch();
			}
		}

		internal void ShowPremiumStore()
		{
			ShowStore(global::Kampai.Game.StoreItemType.PremiumCurrency);
		}

		internal void ShowGrindStore()
		{
			ShowStore(global::Kampai.Game.StoreItemType.GrindCurrency);
		}

		private void OpenStorage()
		{
			global::Kampai.Game.StorageBuilding byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.StorageBuilding>(314);
			if (byInstanceId.State != global::Kampai.Game.BuildingState.Broken && byInstanceId.State != global::Kampai.Game.BuildingState.Inaccessible)
			{
				OpenStorageBuildingSignal.Dispatch(byInstanceId, true);
			}
		}

		private void ShowStore(global::Kampai.Game.StoreItemType type)
		{
			closeAllOtherMenusSignal.Dispatch(base.gameObject);
			view.MoveMenu(type);
			view.ActivateBackgroundButton();
			if (playerService.GetHighestFtueCompleted() < 7)
			{
				questService.PauseQuestScripts();
			}
			DisplayAllWorldToGlassUI(false);
		}

		internal void CloseAllMenu(global::UnityEngine.GameObject exception)
		{
			if (base.gameObject != exception)
			{
				view.MoveMenu(false);
			}
		}

		private void SetStorage()
		{
			if (storage == null)
			{
				storage = playerService.GetFirstInstanceByDefintion<global::Kampai.Game.StorageBuilding, global::Kampai.Game.StorageBuildingDefinition>();
			}
			uint storageCapacity = storage.Definition.StorageUpgrades[storage.CurrentStorageBuildingLevel].StorageCapacity;
			uint storageCount = playerService.GetStorageCount();
			view.SetStorage(storageCount, storageCapacity);
			view.PlayStorageVFX();
		}

		internal void SetLevel()
		{
			view.SetLevel(playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID));
			view.SetXPText(playerService.GetQuantity(global::Kampai.Game.StaticItem.XP_ID), playerService.GetQuantity(global::Kampai.Game.StaticItem.XP_TO_LEVEL_UP_ID));
			SetXP();
		}

		internal void SetGrindCurrency()
		{
			view.SetGrindCurrency(playerService.GetQuantity(global::Kampai.Game.StaticItem.GRIND_CURRENCY_ID));
		}

		internal void SetPremiumCurrency()
		{
			view.SetPremiumCurrency(playerService.GetQuantity(global::Kampai.Game.StaticItem.PREMIUM_CURRENCY_ID));
		}

		internal void SetXP()
		{
			if (!view.expTweenAudio)
			{
				playSFXSignal.Dispatch("Play_bar_scale_01");
				view.expTweenAudio = true;
			}
			view.SetXP(playerService.GetQuantity(global::Kampai.Game.StaticItem.XP_ID), playerService.GetQuantity(global::Kampai.Game.StaticItem.XP_TO_LEVEL_UP_ID));
		}

		internal void CloseMenu(bool closeCurrency)
		{
			if (closeCurrency)
			{
				CloseMenuAndCurrency();
			}
			else
			{
				view.MoveMenu(false);
			}
		}

		internal void CloseMenuAndCurrency()
		{
			currencyService.CurrencyDialogClosed(false);
			view.MoveMenu(false);
			OnCurrencyDone();
		}

		internal void OnPremiumButtonClicked()
		{
			if (ButtonClicked() && !view.premiumOpen)
			{
				ShowPremiumStore();
				view.premiumOpen = true;
				view.grindOpen = false;
			}
		}

		internal void OnGrindButtonClicked()
		{
			if (ButtonClicked() && !view.grindOpen)
			{
				ShowGrindStore();
				view.grindOpen = true;
				view.premiumOpen = false;
			}
		}

		internal void OnCurrencyDone()
		{
			if (playerService.GetHighestFtueCompleted() < 7)
			{
				questService.ResumeQuestScripts();
			}
			view.premiumOpen = false;
			view.grindOpen = false;
			DisplayAllWorldToGlassUI(true);
		}

		internal bool ButtonClicked()
		{
			if (view.IsHiding())
			{
				return false;
			}
			stopAutopanSignal.Dispatch();
			return true;
		}

		internal void MenuMoved(bool show)
		{
			if (show)
			{
				uiAddedSignal.Dispatch(base.gameObject, delegate
				{
					CloseAllMenu(null);
				});
				playSFXSignal.Dispatch("Play_main_menu_open_01");
			}
			else
			{
				uiRemovedSignal.Dispatch(base.gameObject);
				playSFXSignal.Dispatch("Play_main_menu_close_01");
			}
		}

		internal void TogglePopup(bool enable)
		{
			DisplayAllWorldToGlassUI(!enable);
			view.TogglePopup(enable);
		}

		internal void ToggleHUD(bool enable)
		{
			CancelPeek();
			view.Toggle(enable);
			if (enable)
			{
				view.SettingsButton.GetComponent<global::UnityEngine.UI.Button>().interactable = true;
				currencyService.ResumeTransactionsHandling();
			}
			else
			{
				view.SettingsButton.GetComponent<global::UnityEngine.UI.Button>().interactable = false;
			}
		}

		internal void ToggleSettings(bool enable)
		{
			view.ToggleSettings(enable);
		}

		private void CancelPeek()
		{
			if (currentPeekToken != null)
			{
				currentPeekToken.Set(false);
				currentPeekToken = null;
			}
		}

		internal void PeekHUD(float seconds)
		{
			if (view.IsHiding())
			{
				CancelPeek();
				view.Toggle(true);
				SetupHide(seconds);
			}
		}

		internal void SetupHide(float seconds)
		{
			if (!view.IsHiding())
			{
				currentPeekToken = new global::Kampai.Util.MutableBoxed<bool>(true);
				routineRunner.StartCoroutine(HideAfterSeconds(seconds, currentPeekToken));
			}
		}

		private global::System.Collections.IEnumerator HideAfterSeconds(float seconds, global::Kampai.Util.Boxed<bool> shouldStillHide)
		{
			yield return new global::UnityEngine.WaitForSeconds(seconds);
			if (shouldStillHide.Value)
			{
				view.Toggle(false);
			}
		}

		private void SetButtonsVisible(bool visible)
		{
			view.SetButtonsVisible(visible);
		}

		internal void ShowFTUEXP(bool show)
		{
			view.ShowFTUEXP(show);
			view.ToggleDarkSkrim(false);
		}

		private void PlayXPVFX()
		{
			view.PlayXPVFX();
		}

		private void PlayPremiumVFX()
		{
			view.PlayPremiumVFX();
		}

		private void PlayGrindVFX()
		{
			view.PlayGrindVFX();
		}
	}
}
