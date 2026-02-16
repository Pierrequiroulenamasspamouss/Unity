namespace Kampai.UI.View
{
	public class QuestDialogMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.QuestDialogView>
	{
		private global::Kampai.Game.QuestDialogSetting mySetting;

		private int currentQuestId;

		private int currentQuestStep;

		[Inject]
		public global::Kampai.UI.View.ShowPreviousQuestDialogSignal showPreviousDialog { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localizationService { get; set; }

		[Inject]
		public global::Kampai.UI.View.PromptReceivedSignal promptReceivedSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService prestigeService { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			base.view.Init(routineRunner, localizationService);
			base.view.QuestButton.ClickedSignal.AddListener(Close);
			showPreviousDialog.AddListener(ShowPreviousDialog);
		}

		public override void OnRemove()
		{
			base.OnRemove();
			base.view.QuestButton.ClickedSignal.RemoveListener(Close);
			showPreviousDialog.RemoveListener(ShowPreviousDialog);
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			string localizedKey = args.Get<string>();
			global::Kampai.Game.QuestDialogSetting setting = args.Get<global::Kampai.Game.QuestDialogSetting>();
			global::Kampai.Util.Tuple<int, int> tuple = args.Get<global::Kampai.Util.Tuple<int, int>>();
			ShowQuestDialog(localizedKey, setting, tuple);
		}

		internal void ShowPreviousDialog()
		{
			base.view.ShowPreviousDialog();
			if (mySetting.dialogSound.Length > 0)
			{
				soundFXSignal.Dispatch(mySetting.dialogSound);
			}
		}

		internal void ShowQuestDialog(string localizedKey, global::Kampai.Game.QuestDialogSetting setting, global::Kampai.Util.Tuple<int, int> tuple)
		{
			currentQuestId = tuple.Item1;
			currentQuestStep = tuple.Item2;
			base.gameObject.transform.SetAsLastSibling();
			string dialog;
			if (setting.additionalStringParameter.Length != 0)
			{
				string text = localizationService.GetString(setting.additionalStringParameter);
				text = timeService.GetCultureInfo().TextInfo.ToTitleCase(text.ToLower(timeService.GetCultureInfo()));
				dialog = localizationService.GetString(localizedKey, text);
			}
			else
			{
				dialog = localizationService.GetString(localizedKey);
			}
			CheckSettingUpdate(setting);
			if (mySetting.dialogSound.Length > 0)
			{
				soundFXSignal.Dispatch("Play_menu_popUp_02");
			}
			base.view.ShowDialog(dialog);
		}

		private void CheckSettingUpdate(global::Kampai.Game.QuestDialogSetting setting)
		{
			if (mySetting == null)
			{
				mySetting = new global::Kampai.Game.QuestDialogSetting();
			}
			if (mySetting.type != setting.type)
			{
				mySetting.type = setting.type;
			}
			if (mySetting.definitionID != setting.definitionID)
			{
				string dialogIcon = "icn_nav_story_mask";
				if (setting.definitionID != 0)
				{
					global::Kampai.Game.QuestResourceDefinition questResourceDefinition = prestigeService.DetermineQuestResourceDefinition(setting.definitionID, global::Kampai.Game.CharacterImageType.WayfinderIcon);
					if (questResourceDefinition != null && !string.IsNullOrEmpty(questResourceDefinition.maskPath))
					{
						dialogIcon = questResourceDefinition.maskPath;
					}
				}
				base.view.SetDialogIcon(dialogIcon);
				mySetting.definitionID = setting.definitionID;
			}
			if (mySetting.type == global::Kampai.UI.View.QuestDialogType.MINIONREWARD)
			{
				base.view.SetDialogIcon("icn_minionCall_mask");
			}
			else if (mySetting.type == global::Kampai.UI.View.QuestDialogType.NEWPRESTIGE)
			{
				base.view.SetDialogIcon("icn_nav_orderboard_mask");
			}
			mySetting.dialogSound = setting.dialogSound;
		}

		protected override void OnCloseAllMenu(global::UnityEngine.GameObject exception)
		{
		}

		protected override void Close()
		{
			global::Kampai.UI.View.IGUICommand command = guiService.BuildCommand(global::Kampai.UI.View.GUIOperation.Unload, "screen_Dialog");
			if (base.view.IsPageOver())
			{
				guiService.Execute(command);
				promptReceivedSignal.Dispatch(currentQuestId, currentQuestStep);
			}
			else
			{
				base.view.UpdateDialog();
			}
		}
	}
}
