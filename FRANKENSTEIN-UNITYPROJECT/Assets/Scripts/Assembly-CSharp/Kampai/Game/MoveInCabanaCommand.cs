namespace Kampai.Game
{
	public class MoveInCabanaCommand : global::strange.extensions.command.impl.Command
	{
		private int villainId;

		private global::Kampai.Game.CabanaBuilding cabana;

		[Inject]
		public global::Kampai.Game.Prestige prestige { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Util.INamedCharacterBuilder builder { get; set; }

		[Inject(global::Kampai.Game.GameElement.VILLAIN_MANAGER)]
		public global::UnityEngine.GameObject manager { get; set; }

		[Inject]
		public global::Kampai.UI.View.PromptReceivedSignal receivedSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CruiseShipService shipService { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoMoveToInstanceSignal cameraMoveSignal { get; set; }

		[Inject]
		public global::Kampai.Game.KevinGreetVillainSignal kevinGreetSignal { get; set; }

		[Inject]
		public global::Kampai.Game.VillainPlayWelcomeSignal villainPlayWelcomeSignal { get; set; }

		[Inject]
		public global::Kampai.Game.VillainGotoCabanaSignal villainGotoCabanaSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ShowDialogSignal showDialogSignal { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingChangeStateSignal buildingStateSignal { get; set; }

		[Inject]
		public global::Kampai.Common.RecreateBuildingSignal recreateSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService prestigeService { get; set; }

		public override void Execute()
		{
			int trackedInstanceId = prestige.trackedInstanceId;
			global::Kampai.Game.Villain byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Villain>(trackedInstanceId);
			global::Kampai.Game.CabanaBuilding emptyCabana = prestigeService.GetEmptyCabana();
			if (emptyCabana != null)
			{
				MoveIn(emptyCabana, byInstanceId);
			}
		}

		private void MoveIn(global::Kampai.Game.CabanaBuilding building, global::Kampai.Game.Villain villain)
		{
			buildingStateSignal.Dispatch(building.ID, global::Kampai.Game.BuildingState.Working);
			recreateSignal.Dispatch(building);
			villain.CabanaBuildingId = building.ID;
			building.Occupied = true;
			shipService.MoveOut(villain.ID);
			RunWelcomeFlow(villain, building);
		}

		private void RunWelcomeFlow(global::Kampai.Game.Villain villain, global::Kampai.Game.CabanaBuilding building)
		{
			kevinGreetSignal.Dispatch(true);
			villainPlayWelcomeSignal.Dispatch(villain.ID);
			global::Kampai.Game.QuestDialogSetting questDialogSetting = new global::Kampai.Game.QuestDialogSetting();
			questDialogSetting.definitionID = prestige.Definition.ID;
			questDialogSetting.type = global::Kampai.UI.View.QuestDialogType.NORMAL;
			global::Kampai.Game.QuestDialogSetting type = questDialogSetting;
			villainId = villain.ID;
			cabana = building;
			receivedSignal.AddOnce(HandleReceived);
			showDialogSignal.Dispatch(villain.Definition.WelcomeDialogKey, type, new global::Kampai.Util.Tuple<int, int>(-1, -1));
		}

		private void HandleReceived(int questId, int stepId)
		{
			kevinGreetSignal.Dispatch(false);
			villainGotoCabanaSignal.Dispatch(villainId, cabana.ID);
			cameraMoveSignal.Dispatch(new global::Kampai.Game.PanInstructions(cabana.ID));
		}
	}
}
