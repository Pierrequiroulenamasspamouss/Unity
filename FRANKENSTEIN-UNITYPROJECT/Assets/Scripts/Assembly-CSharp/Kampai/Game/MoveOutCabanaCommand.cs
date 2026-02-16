namespace Kampai.Game
{
	public class MoveOutCabanaCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.Prestige prestige { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService prestigeService { get; set; }

		[Inject(global::Kampai.Game.QuestRunnerLanguage.Lua)]
		public global::Kampai.Game.IQuestScriptRunner runner { get; set; }

		[Inject]
		public global::Kampai.Game.CruiseShipService shipService { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingChangeStateSignal buildingStateSignal { get; set; }

		[Inject]
		public global::Kampai.Common.RecreateBuildingSignal recreateSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ShowDialogSignal showDialogSignal { get; set; }

		public override void Execute()
		{
			int trackedInstanceId = prestige.trackedInstanceId;
			global::Kampai.Game.Villain byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Villain>(trackedInstanceId);
			global::Kampai.Game.CabanaBuilding byInstanceId2 = playerService.GetByInstanceId<global::Kampai.Game.CabanaBuilding>(byInstanceId.CabanaBuildingId);
			if (byInstanceId2 == null)
			{
				logger.Error("MoveOutCabanaCommand: Trying to move a villain out of it's cabana, but it doesn't have a cabana");
				return;
			}
			MoveOut(byInstanceId, byInstanceId2);
			PlayFarewellFlow(byInstanceId, byInstanceId2);
		}

		private void MoveOut(global::Kampai.Game.Villain villain, global::Kampai.Game.CabanaBuilding cabana)
		{
			cabana.Occupied = false;
			villain.CabanaBuildingId = -1;
		}

		private void PlayFarewellFlow(global::Kampai.Game.Villain villain, global::Kampai.Game.CabanaBuilding cabana)
		{
			string text = "Scripts/VillainFarewell";
			global::UnityEngine.TextAsset textAsset = global::UnityEngine.Resources.Load<global::UnityEngine.TextAsset>(text);
			if (textAsset == null)
			{
				logger.Error("MoveOutCabanaCommand: script {0} not found.", text);
				PostFlow(villain, cabana);
				return;
			}
			runner.OnQuestScriptComplete = delegate
			{
				PostFlow(villain, cabana);
			};
			global::Kampai.Game.ReturnValueContainer invokationValues = runner.InvokationValues;
			invokationValues.PushIndex().Set(villain.Definition.FarewellDialogKey);
			invokationValues.PushIndex().Set(villain.ID);
			invokationValues.PushIndex().Set(prestige.Definition.ID);
			runner.Start(new global::Kampai.Game.QuestScriptInstance(), textAsset.text, text + ".txt", "villainFarewell");
		}

		private void PostFlow(global::Kampai.Game.Villain villain, global::Kampai.Game.CabanaBuilding cabana)
		{
			RemoveVillain(villain);
			FindNextResident(cabana);
		}

		private void RemoveVillain(global::Kampai.Game.Villain villain)
		{
			shipService.MoveIn(villain.ID);
		}

		private void FindNextResident(global::Kampai.Game.CabanaBuilding cabana)
		{
			int num = playerService.PopVillain();
			if (num == -1)
			{
				buildingStateSignal.Dispatch(cabana.ID, global::Kampai.Game.BuildingState.Idle);
				recreateSignal.Dispatch(cabana);
			}
			else
			{
				global::Kampai.Game.Prestige byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Prestige>(num);
				prestigeService.ChangeToPrestigeState(byInstanceId, global::Kampai.Game.PrestigeState.Questing);
			}
		}
	}
}
