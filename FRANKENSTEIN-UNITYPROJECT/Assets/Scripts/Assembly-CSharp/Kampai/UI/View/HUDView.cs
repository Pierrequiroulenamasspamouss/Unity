namespace Kampai.UI.View
{
	public class HUDView : global::strange.extensions.mediation.impl.View
	{
		public global::UnityEngine.ParticleSystem XPStarVFX;

		public global::UnityEngine.ParticleSystem XPImageVFX;

		public global::UnityEngine.ParticleSystem PremiumStarVFX;

		public global::UnityEngine.ParticleSystem PremiumImageVFX;

		public global::UnityEngine.ParticleSystem GrindStarVFX;

		public global::UnityEngine.ParticleSystem GrindImageVFX;

		public global::UnityEngine.ParticleSystem StorageStarVFX;

		public global::UnityEngine.ParticleSystem StorageImageVFX;

		public global::Kampai.UI.View.ButtonView PremiumMenuButton;

		public global::Kampai.UI.View.ButtonView PremiumIconButton;

		public global::Kampai.UI.View.ButtonView PremiumTextButton;

		public global::Kampai.UI.View.ButtonView GrindMenuButton;

		public global::Kampai.UI.View.ButtonView GrindIconButton;

		public global::Kampai.UI.View.ButtonView GrindTextButton;

		public global::Kampai.UI.View.ButtonView StorageButton;

		public global::UnityEngine.GameObject SettingsButton;

		public global::Kampai.UI.View.ButtonView BackgroundButton;

		public global::UnityEngine.RectTransform CurrencyStore;

		public global::UnityEngine.RectTransform FTUEHighlight;

		public global::Kampai.UI.View.CurrencyMenuView CurrencyMenu;

		public global::UnityEngine.RectTransform FillImage;

		public global::Kampai.UI.View.WayFinderPanelView WayFinder;

		public global::Kampai.UI.View.BuildMenuView BuildMenu;

		public global::UnityEngine.UI.Image XPFillBar;

		public global::UnityEngine.UI.Text XPAmount;

		public global::UnityEngine.UI.Text GrindCurrency;

		public global::UnityEngine.UI.Text PremiumCurrency;

		public global::UnityEngine.UI.Text Level;

		public global::UnityEngine.UI.Text StorageAmount;

		public global::UnityEngine.GameObject DarkSkrim;

		public global::strange.extensions.signal.impl.Signal<bool> MenuMoved = new global::strange.extensions.signal.impl.Signal<bool>();

		private global::UnityEngine.Animator animator;

		private int darkSkrimCount;

		private bool lastPopupState;

		internal bool grindOpen;

		internal bool premiumOpen;

		private int popupsOpened;

		private bool expIsTweening;

		internal bool expTweenAudio = true;

		private bool playStorageVFX;

		private global::Kampai.UI.View.HUDChangedSiblingIndexSignal hudChangedSiblingIndexSignal;

		internal global::strange.extensions.signal.impl.Signal checkLevelSignal = new global::strange.extensions.signal.impl.Signal();

		internal global::strange.extensions.signal.impl.Signal animateXP = new global::strange.extensions.signal.impl.Signal();

		public int expTweenCount { get; set; }

		public int premiumTweenCount { get; set; }

		public int grindTweenCount { get; set; }

		public void Init(global::Kampai.UI.View.HUDChangedSiblingIndexSignal hudChangedSiblingIndexSignal, global::Kampai.Main.ILocalizationService localization, global::System.Func<bool> COPPACompliant)
		{
			this.hudChangedSiblingIndexSignal = hudChangedSiblingIndexSignal;
			(base.transform as global::UnityEngine.RectTransform).offsetMin = global::UnityEngine.Vector2.zero;
			(base.transform as global::UnityEngine.RectTransform).offsetMax = global::UnityEngine.Vector2.zero;
			CurrencyMenu.Init(localization, COPPACompliant);
			animator = GetComponent<global::UnityEngine.Animator>();
			DisableSoundForMenuButtons();
		}

		private void DisableSoundForMenuButtons()
		{
			PremiumMenuButton.PlaySoundOnClick = (PremiumIconButton.PlaySoundOnClick = (PremiumTextButton.PlaySoundOnClick = false));
			GrindMenuButton.PlaySoundOnClick = (GrindIconButton.PlaySoundOnClick = (GrindTextButton.PlaySoundOnClick = false));
			StorageButton.PlaySoundOnClick = false;
			BackgroundButton.PlaySoundOnClick = false;
			global::Kampai.UI.View.ButtonView component = SettingsButton.GetComponent<global::Kampai.UI.View.ButtonView>();
			if (component != null)
			{
				component.PlaySoundOnClick = false;
			}
		}

		internal void SetStorage(uint current, uint max)
		{
			StorageAmount.text = string.Format("{0}/{1}", current, max);
		}

		public void SetXP(uint xp, uint maxXP)
		{
			SetXPFromString();
			Go.to(this, 1f, new GoTweenConfig().intProp("expTweenCount", global::UnityEngine.Mathf.Min((int)xp, (int)maxXP)).onUpdate(delegate
			{
				SetXPText((uint)expTweenCount, maxXP);
			}));
			Go.to(FillImage, 1f, new GoTweenConfig().vector2Prop("anchorMax", new global::UnityEngine.Vector2((float)xp / (float)maxXP, 1f)).onComplete(delegate
			{
				if (xp > maxXP)
				{
					xp = maxXP;
				}
				checkLevelSignal.Dispatch();
				expTweenAudio = false;
			}));
		}

		private void SetXPFromString()
		{
			string text = XPAmount.text;
			string s = text.Substring(0, text.Length - (text.Length - text.IndexOf('/')));
			int result;
			int.TryParse(s, out result);
			expTweenCount = result;
		}

		internal void SetXPText(uint xp, uint maxXP)
		{
			XPAmount.text = string.Format("{0}/{1}", xp, maxXP);
		}

		public void SetLevel(uint level)
		{
			Level.text = level.ToString();
			FillImage.anchorMax = new global::UnityEngine.Vector2(0f, 1f);
			animateXP.Dispatch();
		}

		public void SetGrindCurrency(uint amount)
		{
			Go.to(this, 1f, new GoTweenConfig().intProp("grindTweenCount", (int)amount).onUpdate(delegate
			{
				GrindCurrency.text = grindTweenCount.ToString();
			}));
		}

		public void SetPremiumCurrency(uint amount)
		{
			if (amount < premiumTweenCount)
			{
				PremiumCurrency.text = amount.ToString();
				return;
			}
			Go.to(this, 1f, new GoTweenConfig().intProp("premiumTweenCount", (int)amount).onUpdate(delegate
			{
				PremiumCurrency.text = premiumTweenCount.ToString();
			}));
		}

		public void MoveMenu(global::Kampai.Game.StoreItemType type = global::Kampai.Game.StoreItemType.PremiumCurrency)
		{
			if (!CurrencyMenu.isOpen)
			{
				MoveMenu(true, type);
			}
			else
			{
				SwitchMenu(type);
			}
		}

		public void SwitchMenu(global::Kampai.Game.StoreItemType type)
		{
			SetActive(true, type);
		}

		public void ActivateBackgroundButton()
		{
			if (darkSkrimCount == 0)
			{
				BackgroundButton.gameObject.SetActive(true);
				ToggleDarkSkrim(true);
			}
		}

		public void MoveMenu(bool show, global::Kampai.Game.StoreItemType type = global::Kampai.Game.StoreItemType.PremiumCurrency)
		{
			if (show && !CurrencyMenu.isOpen)
			{
				BringToForeground();
				MenuMoved.Dispatch(true);
				CurrencyMenu.MoveMenu(show, type);
				animator.SetBool("OnHide", false);
				animator.SetBool("OnPopup", false);
			}
			else if (!show & CurrencyMenu.isOpen)
			{
				BackgroundButton.gameObject.SetActive(false);
				BringToBackground();
				MenuMoved.Dispatch(false);
				CurrencyMenu.MoveMenu(show, type);
				animator.SetBool("OnPopup", lastPopupState);
				ToggleDarkSkrim(false);
			}
		}

		internal void SetActive(bool isActive, global::Kampai.Game.StoreItemType type = global::Kampai.Game.StoreItemType.PremiumCurrency)
		{
			CurrencyMenu.SetActive(isActive, type);
		}

		internal void ToggleDarkSkrim(bool show)
		{
			if (show)
			{
				darkSkrimCount++;
			}
			else
			{
				darkSkrimCount--;
				darkSkrimCount = global::UnityEngine.Mathf.Max(0, darkSkrimCount);
			}
			DarkSkrim.SetActive((darkSkrimCount > 0) ? true : false);
		}

		internal void TogglePopup(bool show)
		{
			if (show)
			{
				popupsOpened++;
				ToggleDarkSkrim(true);
				if (popupsOpened == 1)
				{
					BringToForeground();
				}
			}
			else
			{
				popupsOpened--;
				popupsOpened = global::UnityEngine.Mathf.Max(0, popupsOpened);
				ToggleDarkSkrim(false);
				if (popupsOpened == 0)
				{
					BringToBackground();
				}
			}
			lastPopupState = popupsOpened > 0;
			animator.SetBool("OnPopup", lastPopupState);
		}

		internal void Toggle(bool show)
		{
			animator.SetBool("OnHide", !show);
		}

		internal void ToggleSettings(bool show)
		{
			SettingsButton.SetActive(show);
		}

		internal bool IsHiding()
		{
			return animator.GetBool("OnHide");
		}

		public void SetButtonsVisible(bool visible)
		{
			GrindMenuButton.gameObject.SetActive(visible);
			PremiumMenuButton.gameObject.SetActive(visible);
			SettingsButton.SetActive(visible);
		}

		public void ShowFTUEXP(bool show)
		{
			FTUEHighlight.gameObject.SetActive(show);
		}

		internal void PlayXPVFX()
		{
			XPStarVFX.Play();
			XPImageVFX.Play();
			if (!expIsTweening)
			{
				Go.to(XPFillBar, 0.5f, new GoTweenConfig().colorProp("color", global::UnityEngine.Color.white).setIterations(2, GoLoopType.PingPong).onBegin(delegate
				{
					expIsTweening = true;
					ShowFTUEXP(true);
				})
					.onComplete(delegate
					{
						expIsTweening = false;
						ShowFTUEXP(false);
					}));
			}
		}

		internal void PlayPremiumVFX()
		{
			PremiumStarVFX.Play();
			PremiumImageVFX.Play();
		}

		internal void PlayGrindVFX()
		{
			GrindStarVFX.Play();
			GrindImageVFX.Play();
		}

		internal void PlayStorageVFX()
		{
			if (playStorageVFX)
			{
				StorageStarVFX.Play();
				StorageImageVFX.Play();
			}
			playStorageVFX = true;
		}

		private void BringToForeground()
		{
			base.transform.SetAsLastSibling();
			hudChangedSiblingIndexSignal.Dispatch(base.transform.GetSiblingIndex());
			BuildMenu.transform.SetSiblingIndex(WayFinder.transform.GetSiblingIndex() + 1);
		}

		private void BringToBackground()
		{
			base.transform.SetAsFirstSibling();
			hudChangedSiblingIndexSignal.Dispatch(base.transform.GetSiblingIndex());
			BuildMenu.transform.SetAsLastSibling();
		}
	}
}
