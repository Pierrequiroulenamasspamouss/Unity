namespace Kampai.UI.View
{
	public class QuestRewardMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.QuestRewardView>
	{
		private global::Kampai.Game.Quest q;

		private bool collected;

		private global::Kampai.UI.View.ModalSettings modalSettings = new global::Kampai.UI.View.ModalSettings();

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.UI.View.CloseAllOtherMenuSignal closeSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject(global::Kampai.UI.View.UIElement.CAMERA)]
		public global::UnityEngine.Camera uiCamera { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SpawnDooberSignal tweenSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.UI.View.FTUERewardClosed ftueRewardClosed { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSkrim { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideDelayHUDSignal hideDelayHUDSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IZoomCameraModel zoomCameraModel { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService prestigeService { get; set; }

		[Inject]
		public global::Kampai.UI.IFancyUIService fancyUIService { get; set; }

		[Inject]
		public global::Kampai.Main.MoveAudioListenerSignal toggleCharacterAudioSignal { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			soundFXSignal.Dispatch("Play_completeQuest_01");
			toggleCharacterAudioSignal.Dispatch(false, base.view.MinionSlot.transform);
			collected = false;
			base.view.collectButton.ClickedSignal.AddListener(Collect);
			base.view.OnMenuClose.AddListener(OnMenuClose);
		}

		public override void OnRemove()
		{
			base.OnRemove();
			base.view.collectButton.ClickedSignal.RemoveListener(Collect);
			base.view.OnMenuClose.RemoveListener(OnMenuClose);
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			int id = args.Get<int>();
			modalSettings.enableCollectThrob = args.Contains<global::Kampai.UI.ThrobCollectButton>();
			RegisterRewards(id);
		}

		private void RegisterRewards(int id)
		{
			q = playerService.GetByInstanceId<global::Kampai.Game.Quest>(id);
			if (q == null)
			{
				toggleCharacterAudioSignal.Dispatch(true, null);
				logger.Error("You are trying to show a quest reward with an empty quest: {0}. Something is wrong.", id);
				OnMenuClose();
			}
			else
			{
				base.view.Init(q, localService, definitionService, playerService, modalSettings, fancyUIService, questService);
			}
		}

		protected override void Close()
		{
			toggleCharacterAudioSignal.Dispatch(true, null);
			soundFXSignal.Dispatch("Play_menu_disappear_01");
			if (q != null)
			{
				Collect();
			}
		}

		private void Collect()
		{
			if (!collected)
			{
				collected = true;
				global::Kampai.Game.Transaction.TransactionDefinition reward = q.GetActiveDefinition().GetReward(definitionService);
				if (reward != null)
				{
					playerService.RunEntireTransaction(reward, global::Kampai.Game.TransactionTarget.NO_VISUAL, CollectTransactionCallback);
				}
			}
		}

		private void CollectTransactionCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			if (pct.Success)
			{
				soundFXSignal.Dispatch("Play_button_click_01");
			}
			DooberUtil.CheckForTween(base.view.transactionDefinition, base.view.viewList, true, uiCamera, tweenSignal, definitionService);
			base.view.CloseView();
			questService.GoToNextQuestState(q);
		}

		private void OnMenuClose()
		{
			if (base.view.rewardDisplay != null)
			{
				StopCoroutine(base.view.rewardDisplay);
			}
			ftueRewardClosed.Dispatch();
			if (q != null)
			{
				questService.GoToNextQuestState(q);
			}
			if (zoomCameraModel.ZoomedIn && zoomCameraModel.LastZoomBuildingType == global::Kampai.Game.BuildingZoomType.TIKIBAR)
			{
				hideDelayHUDSignal.Dispatch(3f);
			}
			hideSkrim.Dispatch("QuestRewardSkrim");
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "popup_QuestReward");
			toggleCharacterAudioSignal.Dispatch(true, null);
			closeSignal.Dispatch(base.view.gameObject);
		}
	}
}
