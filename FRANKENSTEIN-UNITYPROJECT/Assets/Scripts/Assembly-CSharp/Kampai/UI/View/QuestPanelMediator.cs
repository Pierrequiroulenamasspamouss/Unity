namespace Kampai.UI.View
{
	public class QuestPanelMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.QuestPanelView>
	{
		private global::Kampai.UI.View.ModalSettings modalSettings = new global::Kampai.UI.View.ModalSettings();

		private int showQuestRewardID;

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.UI.View.QuestDetailIDSignal idSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CloseAllOtherMenuSignal closeSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CloseQuestBookSignal closeQuestSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateQuestPanelWithNewQuestSignal updateQuestPanelSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateQuestLineProgressSignal updateQuestLineProgressSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal playSoundFXSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSkrimSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowQuestRewardSignal questRewardSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.FTUEProgressSignal FTUEsignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.FTUEQuestPanelCloseSignal FTUECloseMenuSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.UI.View.QuestUIModel questUIModel { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowHUDSignal showHUDSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IZoomCameraModel zoomCameraModel { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService prestigeService { get; set; }

		[Inject]
		public global::Kampai.UI.IFancyUIService fancyUIService { get; set; }

		[Inject]
		public global::Kampai.Main.MoveAudioListenerSignal toggleCharacterAudioSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowSettingsButtonSignal showSettingsButtonSignal { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			FTUEsignal.Dispatch();
			base.view.OnMenuClose.AddListener(OnMenuClose);
			idSignal.AddListener(RegisterID);
			updateQuestPanelSignal.AddListener(UpdateQuestSteps);
			closeSignal.AddListener(Exit);
			closeQuestSignal.AddListener(Close);
			showSettingsButtonSignal.Dispatch(false);
		}

		public override void OnRemove()
		{
			showSettingsButtonSignal.Dispatch(true);
			base.OnRemove();
			base.view.OnMenuClose.RemoveListener(OnMenuClose);
			idSignal.RemoveListener(RegisterID);
			updateQuestPanelSignal.RemoveListener(UpdateQuestSteps);
			closeSignal.RemoveListener(Exit);
			closeQuestSignal.RemoveListener(Close);
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			int id = args.Get<int>();
			modalSettings.enableGotoThrob = args.Contains<global::Kampai.UI.ThrobGotoButton>();
			modalSettings.enableDeliverThrob = args.Contains<global::Kampai.UI.ThrobDeliverButton>();
			modalSettings.enablePurchaseButtons = !args.Contains<global::Kampai.UI.DisablePurchaseButton>();
			base.view.Init();
			base.view.localizationService = localService;
			base.view.timeService = timeService;
			base.view.modalSettings = modalSettings;
			toggleCharacterAudioSignal.Dispatch(false, base.view.currentQuestView.MinionSlot.transform);
			RegisterID(id);
		}

		private void RegisterID(int id)
		{
			playSoundFXSignal.Dispatch("Play_menu_popUp_01");
			global::System.Collections.Generic.ICollection<global::Kampai.Game.Quest> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.Quest>();
			global::Kampai.Game.Quest byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Quest>(id);
			InitQuestTabs(instancesByType, byInstanceId, false);
			if (byInstanceId.state == global::Kampai.Game.QuestState.Harvestable)
			{
				showQuestRewardID = byInstanceId.ID;
			}
			global::Kampai.Game.Transaction.TransactionDefinition reward = byInstanceId.GetActiveDefinition().GetReward(definitionService);
			if (reward != null)
			{
				base.view.CreateQuestSteps(byInstanceId, reward);
				base.view.Open();
			}
			updateQuestLineProgressSignal.Dispatch(id);
		}

		private void UpdateQuestSteps(int questId)
		{
			global::Kampai.Game.Quest byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Quest>(questId);
			global::System.Collections.Generic.ICollection<global::Kampai.Game.Quest> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.Quest>();
			InitQuestTabs(instancesByType, byInstanceId);
			global::Kampai.Game.Transaction.TransactionDefinition reward = byInstanceId.GetActiveDefinition().GetReward(definitionService);
			if (reward != null)
			{
				base.view.CreateQuestSteps(byInstanceId, reward);
			}
		}

		private void InitQuestTabs(global::System.Collections.Generic.ICollection<global::Kampai.Game.Quest> quests, global::Kampai.Game.Quest selectedQuest, bool isUpdate = true)
		{
			int lastSelectedQuestID = questUIModel.lastSelectedQuestID;
			questUIModel.lastSelectedQuestID = selectedQuest.ID;
			global::Kampai.Game.Prestige prestige = prestigeService.GetPrestige(selectedQuest.GetActiveDefinition().SurfaceID);
			global::Kampai.UI.DummyCharacterType characterType = global::Kampai.UI.DummyCharacterType.Minion;
			if (prestige.Definition.Type == global::Kampai.Game.PrestigeType.Minion)
			{
				global::Kampai.Game.Definition definition = definitionService.Get<global::Kampai.Game.Definition>(prestige.Definition.TrackedDefinitionID);
				if (definition is global::Kampai.Game.NamedCharacterDefinition)
				{
					characterType = global::Kampai.UI.DummyCharacterType.NamedCharacter;
				}
			}
			else
			{
				characterType = global::Kampai.UI.DummyCharacterType.NamedCharacter;
			}
			if (isUpdate)
			{
				base.view.SetCurrentQuestImage(selectedQuest, characterType);
			}
			else
			{
				base.view.InitCurrentQuestImage(selectedQuest, fancyUIService, characterType);
			}
			if (isUpdate)
			{
				global::Kampai.Game.Quest byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Quest>(lastSelectedQuestID);
				base.view.SwapQuest(byInstanceId, selectedQuest.ID);
			}
			else
			{
				base.view.InitQuestTabs(quests, selectedQuest.ID);
			}
		}

		private void OnMenuClose()
		{
			FTUECloseMenuSignal.Dispatch();
			hideSkrimSignal.Dispatch("QuestPanelSkrim");
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "screen_QuestPanel");
			if (showQuestRewardID != 0)
			{
				questRewardSignal.Dispatch(showQuestRewardID);
			}
		}

		private void Exit(global::UnityEngine.GameObject ignore)
		{
			soundFXSignal.Dispatch("Play_button_click_01");
			Close();
		}

		protected override void Close()
		{
			toggleCharacterAudioSignal.Dispatch(true, null);
			playSoundFXSignal.Dispatch("Play_menu_disappear_01");
			if (zoomCameraModel.ZoomedIn && zoomCameraModel.LastZoomBuildingType == global::Kampai.Game.BuildingZoomType.TIKIBAR)
			{
				showHUDSignal.Dispatch(false);
			}
			base.view.CloseView();
		}
	}
}
