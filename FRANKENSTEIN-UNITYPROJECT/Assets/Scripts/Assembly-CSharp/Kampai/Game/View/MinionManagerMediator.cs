namespace Kampai.Game.View
{
	public class MinionManagerMediator : global::strange.extensions.mediation.impl.EventMediator
	{
		private bool animationsLoaded;

		[Inject]
		public global::Kampai.Game.View.MinionManagerView view { get; set; }

		[Inject]
		public global::Kampai.Game.MinionMoveToSignal minionMoveToSignal { get; set; }

		[Inject]
		public global::Kampai.Game.AddMinionSignal addMinionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.MinionWalkPathSignal minionWalkPathSignal { get; set; }

		[Inject]
		public global::Kampai.Game.MinionRunPathSignal minionRunPathSignal { get; set; }

		[Inject]
		public global::Kampai.Game.MinionAppearSignal minionAppearSignal { get; set; }

		[Inject]
		public global::Kampai.Game.AnimateSelectedMinionSignal animateSelectedMinionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.MinionStateChangeSignal stateChangeSignal { get; set; }

		[Inject]
		public global::Kampai.Game.StartMinionRouteSignal startMinionRouteSignal { get; set; }

		[Inject]
		public global::Kampai.Game.StartTeleportTaskSignal startTeleportTaskSignal { get; set; }

		[Inject]
		public global::Kampai.Game.StartTaskSignal startTaskSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SignalActionSignal stopTaskSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RelocateCharacterSignal relocateSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Common.IRandomService randomService { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService characterService { get; set; }

		[Inject]
		public global::Kampai.Game.StartGroupGachaSignal startGroupGachaSignal { get; set; }

		[Inject]
		public global::Kampai.Common.DeselectAllMinionsSignal deselectAllMinionsSignal { get; set; }

		[Inject]
		public global::Kampai.Game.StartIncidentalAnimationSignal startIncidentalAnimationSignal { get; set; }

		[Inject]
		public global::Kampai.Game.MinionAcknowledgeSignal minionAcknowledgeSignal { get; set; }

		[Inject]
		public global::Kampai.Util.PathFinder pathFinder { get; set; }

		[Inject]
		public global::Kampai.Common.PickControllerModel model { get; set; }

		[Inject(global::Kampai.Game.GameElement.BUILDING_MANAGER)]
		public global::UnityEngine.GameObject buildingManager { get; set; }

		[Inject]
		public global::Kampai.Game.UpdateTaskedMinionSignal updateTaskedMinionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RestoreMinionStateSignal restoreMinionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ReadyAnimationSignal readyAnimationSignal { get; set; }

		[Inject]
		public global::Kampai.Game.MinionReactSignal reactSignal { get; set; }

		[Inject]
		public global::Kampai.Game.EnableMinionRendererSignal enableRendererSignal { get; set; }

		[Inject]
		public global::Kampai.Game.MoveMinionFinishedSignal moveMinionFinishedSignal { get; set; }

		[Inject]
		public global::Kampai.Game.PlayMinionNoAnimAudioSignal playMinionNoAnimAudioSignal { get; set; }

		[Inject]
		public global::Kampai.Game.AddMinionToTikiBarSignal addMinionTikiBarSignal { get; set; }

		[Inject]
		public global::Kampai.Game.MinionSeekPositionSignal minionSeekPositionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SetPartyStatesSignal setPartyStateSignal { get; set; }

		[Inject]
		public global::Kampai.Game.TapMinionSignal tapMinionSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ICoroutineProgressMonitor coroutineProgressMonitor { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		public override void OnRegister()
		{
			view.Init();
			SetupSignals();
			routineRunner.StartCoroutine(WaitForDefinitionsAndInit());
		}

		private global::System.Collections.IEnumerator WaitForDefinitionsAndInit()
		{
			// [FIX] Wait for DefinitionService to be ready
			while (!definitionService.IsReady)
			{
				yield return new global::UnityEngine.WaitForSeconds(0.1f);
			}

			global::Kampai.Game.PartyDefinition partyDefinition = definitionService.GetPartyDefinition();
			if (partyDefinition != null)
			{
				view.SetPartyLocation(new global::Kampai.Util.Boxed<global::UnityEngine.Vector3>((global::UnityEngine.Vector3)partyDefinition.Center), partyDefinition.Radius);
			}
			else
			{
				logger.Error("[MinionManagerMediator] PartyDefinition is null even after waiting!");
			}

			yield return routineRunner.StartCoroutine(CacheAnimations());
		}

		private global::System.Collections.IEnumerator CacheAnimations()
		{
			while (coroutineProgressMonitor.GetRunningTasksCount() > 1)
			{
				yield return null;
			}
			int id = coroutineProgressMonitor.StartTask("cache minion anims");
			global::System.Collections.Generic.List<global::Kampai.Game.Transaction.WeightedDefinition> weights = definitionService.GetAllGachaDefinitions();
			for (int i = 0; i < weights.Count; i++)
			{
				global::System.Collections.Generic.IList<global::Kampai.Game.Transaction.WeightedQuantityItem> gachaChoices = weights[i].Entities;
				global::System.Collections.Generic.List<global::Kampai.Game.AnimationDefinition> gachas = new global::System.Collections.Generic.List<global::Kampai.Game.AnimationDefinition>();
				for (int j = 0; j < gachaChoices.Count; j++)
				{
					gachas.Add(definitionService.Get<global::Kampai.Game.GachaAnimationDefinition>(gachaChoices[j].ID));
				}
				yield return routineRunner.StartCoroutine(view.CacheAnimationsCoroutine(gachas));
			}
			global::System.Collections.Generic.IList<global::Kampai.Game.MinionAnimationDefinition> anims = definitionService.GetAll<global::Kampai.Game.MinionAnimationDefinition>();
			global::System.Collections.Generic.List<global::Kampai.Game.AnimationDefinition> animDefs = new global::System.Collections.Generic.List<global::Kampai.Game.AnimationDefinition>();
			foreach (global::Kampai.Game.MinionAnimationDefinition mad in anims)
			{
				global::Kampai.Game.AnimationDefinition ad = mad;
				animDefs.Add(ad);
			}
			yield return routineRunner.StartCoroutine(view.CacheAnimationsCoroutine(animDefs));
			coroutineProgressMonitor.FinishTask(id);
			animationsLoaded = true;
		}

		private void SetupSignals()
		{
			view.idleMinionSignal.AddListener(IdleMinion);
			addMinionSignal.AddListener(AddMinion);
			minionWalkPathSignal.AddListener(WalkPath);
			minionRunPathSignal.AddListener(RunPath);
			startMinionRouteSignal.AddListener(StartMinionRoute);
			minionAppearSignal.AddListener(MinionAppear);
			animateSelectedMinionSignal.AddListener(SelectMinion);
			startGroupGachaSignal.AddListener(StartGroupGacha);
			startIncidentalAnimationSignal.AddListener(StartIncidentalAnimation);
			minionAcknowledgeSignal.AddListener(MinionAcknowledgement);
			updateTaskedMinionSignal.AddListener(UpdateTaskedMinion);
			readyAnimationSignal.AddListener(SetMinionReady);
			reactSignal.AddListener(MinionReact);
			enableRendererSignal.AddListener(EnableMinionRenderer);
			startTeleportTaskSignal.AddListener(MinionTeleport);
			playMinionNoAnimAudioSignal.AddListener(PlayMinionAudio);
			minionSeekPositionSignal.AddListener(SeekPosition);
			setPartyStateSignal.AddListener(SetPartyStates);
			tapMinionSignal.AddListener(TapMinion);
		}

		public override void OnRemove()
		{
			view.idleMinionSignal.RemoveListener(IdleMinion);
			addMinionSignal.RemoveListener(AddMinion);
			minionWalkPathSignal.RemoveListener(WalkPath);
			minionRunPathSignal.RemoveListener(RunPath);
			startMinionRouteSignal.RemoveListener(StartMinionRoute);
			minionAppearSignal.RemoveListener(MinionAppear);
			animateSelectedMinionSignal.RemoveListener(SelectMinion);
			startGroupGachaSignal.RemoveListener(StartGroupGacha);
			startIncidentalAnimationSignal.RemoveListener(StartIncidentalAnimation);
			minionAcknowledgeSignal.RemoveListener(MinionAcknowledgement);
			updateTaskedMinionSignal.RemoveListener(UpdateTaskedMinion);
			readyAnimationSignal.RemoveListener(SetMinionReady);
			reactSignal.RemoveListener(MinionReact);
			enableRendererSignal.RemoveListener(EnableMinionRenderer);
			startTeleportTaskSignal.RemoveListener(MinionTeleport);
			playMinionNoAnimAudioSignal.RemoveListener(PlayMinionAudio);
			minionSeekPositionSignal.RemoveListener(SeekPosition);
			setPartyStateSignal.RemoveListener(SetPartyStates);
			tapMinionSignal.RemoveListener(TapMinion);
		}

		private void StartGroupGacha(global::Kampai.Game.MinionAnimationInstructions instructions)
		{
			global::System.Collections.Generic.HashSet<int> minionIds = instructions.MinionIds;
			int count = minionIds.Count;
			global::Kampai.Game.Transaction.WeightedDefinition gachaWeightsForNumMinions = definitionService.GetGachaWeightsForNumMinions(count, instructions.Party);
			global::Kampai.Game.Transaction.WeightedInstance weightedInstance = playerService.GetWeightedInstance(gachaWeightsForNumMinions.ID);
			global::Kampai.Util.QuantityItem quantityItem = weightedInstance.NextPick(randomService);
			if (quantityItem.ID > 0)
			{
				global::Kampai.Game.GachaAnimationDefinition gachaPick = definitionService.Get<global::Kampai.Game.GachaAnimationDefinition>(quantityItem.ID);
				view.StartGroupGacha(gachaPick, minionIds, instructions.Center.Value, pathFinder);
			}
			else
			{
				deselectAllMinionsSignal.Dispatch();
			}
		}

		private void AddMinion(global::Kampai.Game.View.MinionObject minionObj)
		{
			view.Add(minionObj);
			restoreMinionSignal.Dispatch(minionObj.ID);
		}

		private void WalkPath(int minionID, global::System.Collections.Generic.IList<global::UnityEngine.Vector3> path, float speed, bool muteStatus)
		{
			view.StartPathing(minionID, path, speed, muteStatus, moveMinionFinishedSignal);
		}

		private void RunPath(int minionID, global::System.Collections.Generic.IList<global::UnityEngine.Vector3> path, float timeout, bool muteStatus)
		{
			view.StartPathing(minionID, path, 4.5f, muteStatus, moveMinionFinishedSignal);
		}

		private void StartMinionRoute(global::Kampai.Game.View.RouteInstructions routing)
		{
			global::Kampai.Game.Minion byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Minion>(routing.MinionId);
			global::Kampai.Game.TaskableBuilding taskableBuilding = routing.TargetBuilding as global::Kampai.Game.TaskableBuilding;
			if (taskableBuilding == null)
			{
				logger.Error("Trying to task a minion to a no-taskable building.");
			}
			view.StartMinionTask(byInstanceId, taskableBuilding, startTaskSignal, stopTaskSignal, relocateSignal, routing.Path, routing.Rotation);
		}

		private void PlayMinionAudio(int MinionID, string audioEvent)
		{
			view.playMinionAudio(MinionID, audioEvent);
		}

		private void EnableMinionRenderer(int minionID, bool enable)
		{
			view.EnableRenderer(minionID, enable);
		}

		private void MinionTeleport(global::Kampai.Game.Minion minion, global::Kampai.Game.TaskableBuilding building)
		{
			view.TeleportMinionTask(minion, building, startTaskSignal, stopTaskSignal, relocateSignal);
		}

		private void UpdateTaskedMinion(int minionID, global::Kampai.Game.View.MinionTaskInfo taskInfo)
		{
			view.UpdateTaskedMinion(minionID, taskInfo);
		}

		private void MinionAppear(int minionID, global::UnityEngine.Vector3 pos)
		{
			view.MinionAppear(minionID, pos);
		}

		private void SelectMinion(global::Kampai.Game.SelectMinionState state)
		{
			global::Kampai.Game.GachaAnimationDefinition gachaAnimationDefinition = null;
			if (state.triggerIncidentalAnimation)
			{
				gachaAnimationDefinition = GetNextGacha(false);
			}
			global::Kampai.Game.MinionAnimationDefinition minionAnimDef = null;
			if (gachaAnimationDefinition != null)
			{
				minionAnimDef = definitionService.Get<global::Kampai.Game.MinionAnimationDefinition>(gachaAnimationDefinition.AnimationID);
			}
			view.SelectMinion(state.minionID, minionAnimDef, state.runLocation, minionMoveToSignal, state.muteStatus);
		}

		private void TapMinion(int minionID)
		{
			global::Kampai.Game.Minion byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Minion>(minionID);
			if (byInstanceId == null)
			{
				logger.Debug("MinionManagerMediator:TapMinion - KAMPAI 7668 minion was null");
			}
			else
			{
				if (byInstanceId.State != global::Kampai.Game.MinionState.Idle)
				{
					return;
				}
				global::Kampai.Game.GachaAnimationDefinition nextGacha = GetNextGacha(false);
				if (nextGacha == null)
				{
					logger.Debug("MinionManagerMediator:TapMinion - KAMPAI 7668 gacha was null");
					return;
				}
				global::Kampai.Game.MinionAnimationDefinition minionAnimationDefinition = definitionService.Get<global::Kampai.Game.MinionAnimationDefinition>(nextGacha.AnimationID);
				if (minionAnimationDefinition == null)
				{
					logger.Debug("MinionManagerMediator:TapMinion - KAMPAI 7668 minionAnimDef was null");
				}
				else
				{
					view.AnimateMinion(minionID, minionAnimationDefinition, false);
				}
			}
		}

		private global::Kampai.Game.GachaAnimationDefinition GetNextGacha(bool party)
		{
			global::Kampai.Game.Transaction.WeightedInstance weightedInstance = playerService.GetWeightedInstance(definitionService.GetGachaWeightsForNumMinions(1, party).ID);
			return definitionService.Get<global::Kampai.Game.GachaAnimationDefinition>(weightedInstance.NextPick(randomService).ID);
		}

		private void IdleMinion(int minionID)
		{
			global::Kampai.Game.Minion byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Minion>(minionID);
			global::Kampai.Game.MinionState state = byInstanceId.State;
			if (byInstanceId.PrestigeCharacter != null)
			{
				global::System.Collections.Generic.IList<global::Kampai.Game.Instance> instancesByDefinition = playerService.GetInstancesByDefinition<global::Kampai.Game.TikiBarBuildingDefinition>();
				if (instancesByDefinition != null && instancesByDefinition.Count != 0)
				{
					global::Kampai.Game.TikiBarBuilding tikiBarBuilding = instancesByDefinition[0] as global::Kampai.Game.TikiBarBuilding;
					addMinionTikiBarSignal.Dispatch(tikiBarBuilding, byInstanceId, byInstanceId.PrestigeCharacter, tikiBarBuilding.GetMinionSlotIndex(byInstanceId.PrestigeCharacter.Definition.ID));
				}
				return;
			}
			switch (state)
			{
			case global::Kampai.Game.MinionState.WaitingOnMagnetFinger:
				break;
			default:
				view.SetMinionMute(minionID, false);
				stateChangeSignal.Dispatch(minionID, global::Kampai.Game.MinionState.Idle);
				view.Wander(minionID);
				break;
			case global::Kampai.Game.MinionState.Idle:
			case global::Kampai.Game.MinionState.Selected:
				switch (state)
				{
				case global::Kampai.Game.MinionState.Idle:
					view.Wander(minionID);
					break;
				case global::Kampai.Game.MinionState.Selected:
					view.SetMinionReady(minionID, true);
					break;
				}
				break;
			}
		}

		private void StartIncidentalAnimation(int minionID, int animationDefinitionId)
		{
			view.StartMinionAnimation(minionID, definitionService.Get<global::Kampai.Game.MinionAnimationDefinition>(animationDefinitionId), true);
		}

		private void MinionAcknowledgement(int minionID, float rotateTo, int animationDefinitionId)
		{
			if (animationsLoaded)
			{
				view.MinionAcknowledgement(minionID, rotateTo, definitionService.Get<global::Kampai.Game.MinionAnimationDefinition>(animationDefinitionId));
			}
		}

		private void SetMinionReady(int minionId)
		{
			view.SetMinionReady(minionId);
		}

		private void SeekPosition(int minionID, global::UnityEngine.Vector3 pos, float threshold)
		{
			view.SeekPosition(minionID, pos, threshold);
		}

		private void MinionReact(global::System.Collections.Generic.ICollection<int> minionIds, global::Kampai.Util.Boxed<global::UnityEngine.Vector3> buildingPos)
		{
			if (!animationsLoaded)
			{
				return;
			}
			global::Kampai.Game.Transaction.WeightedInstance weightedInstance = playerService.GetWeightedInstance(4005);
			global::Kampai.Util.QuantityItem quantityItem = weightedInstance.NextPick(randomService);
			if (quantityItem.ID > 0)
			{
				global::Kampai.Game.GachaAnimationDefinition gachaAnimationDefinition = definitionService.Get<global::Kampai.Game.GachaAnimationDefinition>(quantityItem.ID);
				if (gachaAnimationDefinition == null)
				{
					logger.Log(global::Kampai.Util.Logger.Level.Error, "Bad Gacha ID: {0}", quantityItem.ID);
				}
				else
				{
					view.MinionReact(gachaAnimationDefinition, minionIds, buildingPos);
				}
			}
		}

		private void SetPartyStates(bool gameIsStarting)
		{
			global::System.Collections.Generic.List<global::Kampai.Game.Minion> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.Minion>();
			for (int i = 0; i < instancesByType.Count; i++)
			{
				if (instancesByType[i].State == global::Kampai.Game.MinionState.Tasking)
				{
					continue;
				}
				global::Kampai.Game.MinionAnimationDefinition partyStartAnimation = null;
				if (gameIsStarting)
				{
					global::Kampai.Game.PartyDefinition partyDefinition = definitionService.GetPartyDefinition();
					if (partyDefinition.StartAnimations > 0)
					{
						global::Kampai.Game.Transaction.WeightedInstance weightedInstance = playerService.GetWeightedInstance(partyDefinition.StartAnimations);
						partyStartAnimation = definitionService.Get<global::Kampai.Game.MinionAnimationDefinition>(weightedInstance.NextPick(randomService).ID);
					}
				}
				view.SetPartyState(instancesByType[i].ID, instancesByType[i].Partying, gameIsStarting, partyStartAnimation);
			}
		}
	}
}
