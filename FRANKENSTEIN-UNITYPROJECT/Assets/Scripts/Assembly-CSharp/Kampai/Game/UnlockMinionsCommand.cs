namespace Kampai.Game
{
	public class UnlockMinionsCommand : global::strange.extensions.command.impl.Command
	{
		[Inject(global::Kampai.UI.View.UIElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable uiContext { get; set; }

		[Inject(global::Kampai.Main.MainElement.CAMERA)]
		public global::UnityEngine.GameObject camera { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Common.IRandomService randomService { get; set; }

		[Inject]
		public global::Kampai.Game.PhilBeginIntroLoopSignal beginIntroLoopSignal { get; set; }

		[Inject]
		public global::Kampai.Game.UnleashCharacterAtShoreSignal unleashCharacterAtShoreSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CloseLevelUpRewardSignal levelCompleteSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.PromptReceivedSignal promptReceivedSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CameraZoomBeachSignal cameraZoomBeachSignal { get; set; }

		[Inject]
		public global::Kampai.Game.UnlockCharacterModel characterModel { get; set; }

		[Inject]
		public global::Kampai.Game.CreateNamedCharacterViewSignal createCharacterSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CreateMinionSignal createMinionSignal { get; set; }

		public override void Execute()
		{
			if (characterModel.characterUnlocks.Count == 0 && characterModel.minionUnlocks.Count == 0)
			{
				return;
			}
			global::System.Collections.Generic.IList<global::Kampai.Game.Character> list;
			if (characterModel.characterUnlocks.Count > 0)
			{
				global::System.Collections.Generic.IList<global::Kampai.Game.Character> characterUnlocks = characterModel.characterUnlocks;
				list = characterUnlocks;
			}
			else
			{
				list = characterModel.minionUnlocks;
			}
			global::System.Collections.Generic.IList<global::Kampai.Game.Character> characterList = list;
			int quantity = (int)playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID);
			global::Kampai.Game.LevelUpDefinition levelUpDef = definitionService.Get<global::Kampai.Game.LevelUpDefinition>(88888);
			int characterSetCount = GetCharacterSetCount(quantity, levelUpDef);
			string empty = string.Empty;
			empty = ((characterSetCount > 2) ? string.Format("AnnounceMinions{0}", randomService.NextInt(1, 20)) : string.Format("AnnounceMinionSet{0}", characterSetCount));
			bool isLevelUnlock = true;
			foreach (global::Kampai.Game.Character item in characterList)
			{
				global::Kampai.Game.Minion minion = item as global::Kampai.Game.Minion;
				global::Kampai.Game.NamedCharacter namedCharacter = item as global::Kampai.Game.NamedCharacter;
				if (minion != null)
				{
					createMinionSignal.Dispatch(minion);
					global::Kampai.Game.CostumeItemDefinition costumeItemDefinition = definitionService.Get<global::Kampai.Game.CostumeItemDefinition>(minion.CostumeID);
					if (costumeItemDefinition != null && costumeItemDefinition.ID != 99)
					{
						isLevelUnlock = false;
						empty = string.Format("AnnounceMinion{0}", costumeItemDefinition.LocalizedKey);
					}
				}
				else if (namedCharacter != null)
				{
					createCharacterSignal.Dispatch(namedCharacter);
					isLevelUnlock = false;
					empty = string.Format("AnnounceCharacter{0}", item.Name);
				}
				base.injectionBinder.GetInstance<global::Kampai.Game.UnveilCharacterSignal>().Dispatch(item);
			}
			if (isLevelUnlock)
			{
				cameraZoomBeachSignal.Dispatch();
			}
			BeginIntroLoop(characterList, empty);
			global::System.Action<int, int> callback = delegate
			{
				foreach (global::Kampai.Game.Character item2 in characterList)
				{
					unleashCharacterAtShoreSignal.Dispatch(item2, characterModel.routeIndex);
				}
				if (isLevelUnlock)
				{
					characterModel.minionUnlocks.Clear();
				}
				else
				{
					characterModel.routeIndex = -1;
					characterModel.characterUnlocks.Clear();
				}
				base.injectionBinder.GetInstance<global::Kampai.Common.DeselectAllMinionsSignal>().Dispatch();
			};
			promptReceivedSignal.AddOnce(callback);
		}

		private void BeginIntroLoop(global::System.Collections.Generic.IList<global::Kampai.Game.Character> characterList, string key)
		{
			global::Kampai.Game.QuestDialogSetting questDialogSetting = new global::Kampai.Game.QuestDialogSetting();
			questDialogSetting.type = global::Kampai.UI.View.QuestDialogType.MINIONREWARD;
			global::Kampai.Game.QuestDialogSetting type = questDialogSetting;
			global::Kampai.Util.Tuple<int, int> type2 = new global::Kampai.Util.Tuple<int, int>(-1, -1);
			base.injectionBinder.GetInstance<global::Kampai.Game.ShowDialogSignal>().Dispatch(key, type, type2);
			foreach (global::Kampai.Game.Character character in characterList)
			{
				base.injectionBinder.GetInstance<global::Kampai.Game.BeginCharacterIntroLoopSignal>().Dispatch(character);
			}
			beginIntroLoopSignal.Dispatch();
		}

		private int GetCharacterSetCount(int currentLevel, global::Kampai.Game.LevelUpDefinition levelUpDef)
		{
			int num = 0;
			for (int i = 0; i < currentLevel && i < levelUpDef.transactionList.Count; i++)
			{
				global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition = definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(levelUpDef.transactionList[i]);
				global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> outputs = transactionDefinition.Outputs;
				for (int j = 0; j < outputs.Count; j++)
				{
					if (outputs[j].ID == 5)
					{
						num++;
					}
				}
			}
			return num;
		}
	}
}
