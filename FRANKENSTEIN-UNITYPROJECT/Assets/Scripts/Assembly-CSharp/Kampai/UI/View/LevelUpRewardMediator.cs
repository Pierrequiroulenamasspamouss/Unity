namespace Kampai.UI.View
{
	public class LevelUpRewardMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.LevelUpRewardView>
	{
		private global::System.Collections.Generic.List<global::Kampai.Game.View.RewardQuantity> quantityChange;

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSkrim { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.UI.View.SpawnDooberSignal tweenSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShouldRateAppSignal shouldRateAppSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal playSoundFX { get; set; }

		[Inject]
		public global::Kampai.UI.View.FTUELevelUpOpenSignal FTUEOpenSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.FTUELevelUpCloseSignal FTUECloseSignal { get; set; }

		[Inject]
		public global::Kampai.Game.UnlockMinionsSignal unlockMinionsSignal { get; set; }

		[Inject(global::Kampai.UI.View.UIElement.CAMERA)]
		public global::UnityEngine.Camera uiCamera { get; set; }

		[Inject]
		public global::Kampai.UI.View.TogglePopupForHUDSignal HUDSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetLevelSignal setLevelSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localizationService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.UI.View.CloseLevelUpRewardSignal closeLevelRewardSignal { get; set; }

		[Inject]
		public global::Kampai.Game.UpdatePlayerDLCTierSignal playerDLCTierSignal { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			playSoundFX.Dispatch("Play_levelUp_01");
			playSoundFX.Dispatch("Play_UI_levelUp_first_01");
			Init();
			base.view.closeSignal.AddListener(Close);
			base.view.tweenSignal.AddListener(TweenCurrency);
			base.view.StartUnlockMinionSignal.AddListener(BeginUnlock);
		}

		public override void OnRemove()
		{
			base.OnRemove();
			base.view.closeSignal.RemoveListener(Close);
			base.view.tweenSignal.RemoveListener(TweenCurrency);
			base.view.StartUnlockMinionSignal.RemoveListener(BeginUnlock);
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			quantityChange = args.Get<global::System.Collections.Generic.List<global::Kampai.Game.View.RewardQuantity>>();
			Register(quantityChange);
		}

		private void Init()
		{
			base.closeAllOtherMenuSignal.Dispatch(base.view.gameObject);
			FTUEOpenSignal.Dispatch();
			base.view.Init(playerService, definitionService, localService, playSoundFX);
			setLevelSignal.Dispatch();
		}

		private void Register(global::System.Collections.Generic.List<global::Kampai.Game.View.RewardQuantity> quantityChange)
		{
			base.view.Display(quantityChange);
		}

		private void TweenCurrency()
		{
			DooberUtil.CheckForTween(base.view.rewardViewList, false, uiCamera, tweenSignal, definitionService);
		}

		protected override void OnCloseAllMenu(global::UnityEngine.GameObject exception)
		{
		}

		protected override void Close()
		{
			shouldRateAppSignal.Dispatch(global::Kampai.Game.ConfigurationDefinition.RateAppAfterEvent.LevelUp);
			FTUECloseSignal.Dispatch();
			playerDLCTierSignal.Dispatch();
			closeLevelRewardSignal.Dispatch();
		}

		private void BeginUnlock()
		{
			unlockMinionsSignal.Dispatch();
		}
	}
}
