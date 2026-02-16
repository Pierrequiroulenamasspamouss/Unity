namespace Kampai.Game
{
	public class PanAndOpenQuestCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public int questID { get; set; }

		[Inject(global::Kampai.Game.GameElement.BUILDING_MANAGER)]
		public global::UnityEngine.GameObject buildingManager { get; set; }

		[Inject(global::Kampai.Game.GameElement.NAMED_CHARACTER_MANAGER)]
		public global::UnityEngine.GameObject namedCharacterManager { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoMoveSignal autoMoveSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.Quest byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Quest>(questID);
			if (byInstanceId == null)
			{
				logger.Error("No quest found with id: {0}", questID);
				return;
			}
			global::UnityEngine.Vector3 zero = global::UnityEngine.Vector3.zero;
			global::Kampai.Game.View.BuildingManagerView component = buildingManager.GetComponent<global::Kampai.Game.View.BuildingManagerView>();
			global::Kampai.Game.View.BuildingObject buildingObject = component.GetBuildingObject(byInstanceId.QuestIconTrackedInstanceId);
			if (buildingObject != null)
			{
				zero = buildingObject.transform.position;
			}
			else
			{
				global::Kampai.Game.View.NamedCharacterManagerView component2 = namedCharacterManager.GetComponent<global::Kampai.Game.View.NamedCharacterManagerView>();
				global::Kampai.Game.View.NamedCharacterObject namedCharacterObject = component2.Get(byInstanceId.QuestIconTrackedInstanceId);
				if (!(namedCharacterObject != null))
				{
					logger.Warning("Unsupported type of object to pan to");
					return;
				}
				zero = namedCharacterObject.transform.position;
			}
			logger.Info("Pan and open quest id: {0} - {1}", questID, zero);
			global::Kampai.Game.CameraOffset cameraOffset = null;
			global::Kampai.Game.TaskableBuilding byInstanceId2 = playerService.GetByInstanceId<global::Kampai.Game.TaskableBuilding>(byInstanceId.QuestIconTrackedInstanceId);
			if (byInstanceId2 != null)
			{
				cameraOffset = byInstanceId2.Definition.ModalOffset;
			}
			if (cameraOffset != null)
			{
				global::UnityEngine.Vector3 type = new global::UnityEngine.Vector3(zero.x + cameraOffset.x, zero.y, zero.z + cameraOffset.z);
				autoMoveSignal.Dispatch(type, cameraOffset.zoom, new global::Kampai.Game.CameraMovementSettings(global::Kampai.Game.CameraMovementSettings.Settings.Quest, byInstanceId2, byInstanceId));
			}
			else
			{
				autoMoveSignal.Dispatch(zero, 0.1f, new global::Kampai.Game.CameraMovementSettings(global::Kampai.Game.CameraMovementSettings.Settings.Quest, null, byInstanceId));
			}
		}
	}
}
