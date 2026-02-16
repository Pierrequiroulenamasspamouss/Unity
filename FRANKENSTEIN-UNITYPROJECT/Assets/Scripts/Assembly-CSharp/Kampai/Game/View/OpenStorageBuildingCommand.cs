namespace Kampai.Game.View
{
	public class OpenStorageBuildingCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.StorageBuilding building { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingChangeStateSignal stateChangeSignal { get; set; }

		[Inject]
		public bool directOpenMenu { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			stateChangeSignal.Dispatch(building.ID, global::Kampai.Game.BuildingState.Working);
			if (directOpenMenu)
			{
				OpenGUI();
			}
			else
			{
				routineRunner.StartCoroutine(DelayOpenGUI());
			}
		}

		private void OpenGUI()
		{
			global::Kampai.UI.View.IGUICommand iGUICommand = guiService.BuildCommand(global::Kampai.UI.View.GUIOperation.Load, "screen_StorageBuilding");
			iGUICommand.skrimScreen = "StorageSkrim";
			iGUICommand.darkSkrim = true;
			iGUICommand.Args.Add(building);
			iGUICommand.Args.Add(global::Kampai.UI.View.StorageBuildingModalTypes.STORAGE);
			guiService.Execute(iGUICommand);
		}

		private global::System.Collections.IEnumerator DelayOpenGUI()
		{
			yield return new global::UnityEngine.WaitForSeconds(0f);
			OpenGUI();
		}
	}
}
