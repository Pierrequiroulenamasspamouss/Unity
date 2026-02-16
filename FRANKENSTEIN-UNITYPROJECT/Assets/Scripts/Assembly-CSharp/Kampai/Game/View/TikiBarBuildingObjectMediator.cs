namespace Kampai.Game.View
{
	public class TikiBarBuildingObjectMediator : global::strange.extensions.mediation.impl.EventMediator
	{
		private global::strange.extensions.signal.impl.Signal<global::Kampai.Game.View.CharacterObject, int> addToTikiBarSignal;

		[Inject]
		public global::Kampai.Game.View.TikiBarBuildingObjectView view { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService prestigeService { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Game.RestoreMinionAtTikiBarSignal restoreMinionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.MinionStateChangeSignal minionChangeStateSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowQuestPanelSignal showPanelSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowQuestRewardSignal showQuestRewardSignal { get; set; }

		[Inject]
		public global::Kampai.Game.PathCharacterToTikiBarSignal pathCharacterToTikibarSignal { get; set; }

		[Inject]
		public global::Kampai.Game.TeleportCharacterToTikiBarSignal teleportCharacterToTikibarSignal { get; set; }

		[Inject]
		public global::Kampai.Game.UnveilCharacterObjectSignal unveilCharacterSignal { get; set; }

		[Inject]
		public global::Kampai.Game.BeginCharacterLoopAnimationSignal characterLoopAnimationSignal { get; set; }

		[Inject]
		public global::Kampai.Game.PopUnleashedCharacterToTikiBarSignal popUnleashedCharacterToTikibarSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ReleaseMinionFromTikiBarSignal releaseMinionFromTikiBarSignal { get; set; }

		[Inject]
		public global::Kampai.Game.NamedCharacterRemovedFromTikiBarSignal removedFromTikibarSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CharacterIntroCompleteSignal introCompleteSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CharacterDrinkingCompleteSignal drinkingCompleteSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.DisplayLevelUpRewardSignal displayLevelUpRewardSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ToggleStickerbookGlowSignal glowSignal { get; set; }

		[Inject]
		public global::Kampai.Game.GetNewQuestSignal getNewQuestSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject(global::Kampai.Main.MainElement.UI_WORLDCANVAS)]
		public global::UnityEngine.GameObject worldCanvas { get; set; }

		[Inject]
		public global::Kampai.Game.ToggleHitboxSignal toggleHitboxSignal { get; set; }

		[Inject]
		public global::Kampai.Game.TikiBarSetAnimParamSignal setAnimParamSignal { get; set; }

		[Inject]
		public global::Kampai.Game.UnlockCharacterModel characterModel { get; set; }

		[Inject]
		public global::Kampai.Game.RelocateCharacterSignal relocateCharacterSignal { get; set; }

		[Inject]
		public global::Kampai.Game.Environment environment { get; set; }

		public override void OnRegister()
		{
			addToTikiBarSignal = new global::strange.extensions.signal.impl.Signal<global::Kampai.Game.View.CharacterObject, int>();
			unveilCharacterSignal.AddListener(UnveilCharacter);
			characterLoopAnimationSignal.AddListener(BeginCharacterIntroLoop);
			popUnleashedCharacterToTikibarSignal.AddListener(UnleashCharacterToTikiBar);
			pathCharacterToTikibarSignal.AddListener(PathCharacterToTikiBar);
			teleportCharacterToTikibarSignal.AddListener(TeleportCharacterToTikiBar);
			introCompleteSignal.AddListener(CharacterIntroComplete);
			drinkingCompleteSignal.AddListener(CharacterDrinkingComplete);
			releaseMinionFromTikiBarSignal.AddListener(ReleaseMinionFromTikiBar);
			restoreMinionSignal.AddListener(RestoreMinion);
			routineRunner.StartCoroutine(Init());
			toggleHitboxSignal.AddListener(ToggleHitbox);
			setAnimParamSignal.AddListener(PlayAnimation);
			glowSignal.AddListener(ToggleStickerbookGlow);
			addToTikiBarSignal.AddListener(AddCharacterToTikiBar);
		}

		public override void OnRemove()
		{
			unveilCharacterSignal.RemoveListener(UnveilCharacter);
			characterLoopAnimationSignal.RemoveListener(BeginCharacterIntroLoop);
			popUnleashedCharacterToTikibarSignal.RemoveListener(UnleashCharacterToTikiBar);
			pathCharacterToTikibarSignal.RemoveListener(PathCharacterToTikiBar);
			introCompleteSignal.RemoveListener(CharacterIntroComplete);
			drinkingCompleteSignal.RemoveListener(CharacterDrinkingComplete);
			teleportCharacterToTikibarSignal.RemoveListener(TeleportCharacterToTikiBar);
			releaseMinionFromTikiBarSignal.RemoveListener(ReleaseMinionFromTikiBar);
			restoreMinionSignal.RemoveListener(RestoreMinion);
			toggleHitboxSignal.RemoveListener(ToggleHitbox);
			setAnimParamSignal.RemoveListener(PlayAnimation);
			glowSignal.RemoveListener(ToggleStickerbookGlow);
			addToTikiBarSignal.RemoveListener(AddCharacterToTikiBar);
		}

		private void AddCharacterToTikiBar(global::Kampai.Game.View.CharacterObject characterObject, int routeIndex)
		{
			view.AddCharacterToBuildingActions(characterObject, routeIndex);
			getNewQuestSignal.Dispatch();
		}

		private void ToggleHitbox(global::Kampai.Game.BuildingZoomType zoomBuildingType, bool enable)
		{
			if (zoomBuildingType == global::Kampai.Game.BuildingZoomType.TIKIBAR)
			{
				view.ToggleHitbox(enable);
			}
		}

		private void ReleaseMinionFromTikiBar(global::Kampai.Game.Character character)
		{
			if (character is global::Kampai.Game.KevinCharacter)
			{
				CharacterDrinkingComplete(character.ID);
			}
			else
			{
				view.RemoveCharacterFromTikiBar(character.ID);
			}
		}

		private void UnveilCharacter(global::Kampai.Game.View.CharacterObject characterObject)
		{
			view.SetupCharacter(characterObject);
		}

		private void BeginCharacterIntroLoop(global::Kampai.Game.View.CharacterObject characterObject)
		{
			view.BeginCharacterIntroLoop(characterObject);
		}

		private void UnleashCharacterToTikiBar(global::Kampai.Game.View.CharacterObject characterObject, int routeIndex)
		{
			view.BeginCharacterIntro(characterObject, routeIndex);
		}

		private void CharacterIntroComplete(global::Kampai.Game.View.CharacterObject characterObject, int routeIndex)
		{
			global::Kampai.Game.Location location = new global::Kampai.Game.Location(characterObject.transform.position);
			bool validLocation = environment.IsWalkable(location);
			view.EndCharacterIntro(characterObject, routeIndex, validLocation, relocateCharacterSignal);
			if (routeIndex >= 0)
			{
				global::Kampai.Game.Character byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Character>(characterObject.ID);
				prestigeService.AddMinionToTikiBarSlot(byInstanceId, routeIndex, view.tikiBar);
				if (characterModel.minionUnlocks.Count > 0)
				{
					displayLevelUpRewardSignal.Dispatch();
				}
			}
		}

		private void CharacterDrinkingComplete(int instanceID)
		{
			view.UntrackChild(instanceID);
		}

		private void PathCharacterToTikiBar(global::Kampai.Game.View.CharacterObject characterObject, global::Kampai.Game.View.RouteInstructions ri, int routeIndex)
		{
			view.PathCharacterToTikiBar(characterObject, ri.Path, ri.Rotation, routeIndex, addToTikiBarSignal);
		}

		private void TeleportCharacterToTikiBar(global::Kampai.Game.View.CharacterObject characterObject, int routeIndex)
		{
			if (!view.ContainsCharacter(characterObject.ID) && routeIndex <= 2)
			{
				if (characterObject.ID == 78)
				{
					global::Kampai.Game.Prestige prestige = prestigeService.GetPrestige(40000);
					prestigeService.ChangeToPrestigeState(prestige, global::Kampai.Game.PrestigeState.Questing);
				}
				AddCharacterToTikiBar(characterObject, routeIndex);
			}
		}

		public void RestoreMinion(global::Kampai.Game.Character character)
		{
			int iD = prestigeService.GetPrestigeFromMinionInstance(character).Definition.ID;
			int minionSlotIndex = view.tikiBar.GetMinionSlotIndex(iD);
			if (minionSlotIndex != -1)
			{
				prestigeService.AddMinionToTikiBarSlot(character, minionSlotIndex, view.tikiBar);
			}
		}

		private global::System.Collections.IEnumerator Init()
		{
			yield return null;
			if (view.tikiBar.minionQueue.Count == 0)
			{
				view.tikiBar.minionQueue.Add(40000);
				view.tikiBar.minionQueue.Add(-1);
				view.tikiBar.minionQueue.Add(-1);
			}
			view.SetupInjections(minionChangeStateSignal, removedFromTikibarSignal, introCompleteSignal, drinkingCompleteSignal);
		}

		public int GetQuestInstanceFromCharacter(global::Kampai.Game.Prestige character)
		{
			global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Quest> questMap = questService.GetQuestMap();
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::Kampai.Game.Quest> item in questMap)
			{
				global::Kampai.Game.Quest value = item.Value;
				if (value.QuestIconTrackedInstanceId == character.trackedInstanceId && (value.state != global::Kampai.Game.QuestState.Complete || value.state == global::Kampai.Game.QuestState.Notstarted))
				{
					return value.ID;
				}
			}
			logger.Log(global::Kampai.Util.Logger.Level.Error, "Either the quest ID does not match the character ID, or you are trying to start a completed quest.");
			return -1;
		}

		public void MinionButtonClicked(int questInstanceID)
		{
			global::Kampai.Game.Quest byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Quest>(questInstanceID);
			switch (byInstanceId.state)
			{
			case global::Kampai.Game.QuestState.Notstarted:
			case global::Kampai.Game.QuestState.RunningStartScript:
			case global::Kampai.Game.QuestState.RunningTasks:
			case global::Kampai.Game.QuestState.RunningCompleteScript:
				showPanelSignal.Dispatch(questInstanceID);
				break;
			case global::Kampai.Game.QuestState.Harvestable:
				showQuestRewardSignal.Dispatch(questInstanceID);
				break;
			}
		}

		private void PlayAnimation(string animation, global::System.Type type, object obj)
		{
			view.PlayAnimation(animation, type, obj);
		}

		private void ToggleStickerbookGlow(bool enable)
		{
			view.ToggleStickerbookGlow(enable);
		}
	}
}
