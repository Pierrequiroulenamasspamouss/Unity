namespace Kampai.Game.View
{
	public class CameraCinematicMoveToBuildingCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public int buildingID { get; set; }

		[Inject]
		public float moveTime { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject(global::Kampai.Game.GameElement.BUILDING_MANAGER)]
		public global::UnityEngine.GameObject buildingManager { get; set; }

		[Inject]
		public global::Kampai.Game.CameraCinematicZoomSignal autoZoomSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CameraCinematicPanSignal autoPanSignal { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(buildingID);
			global::Kampai.Game.View.BuildingManagerView component = buildingManager.GetComponent<global::Kampai.Game.View.BuildingManagerView>();
			global::Kampai.Game.View.BuildingObject buildingObject = component.GetBuildingObject(buildingID);
			global::UnityEngine.Vector3 position = buildingObject.transform.position;
			global::Kampai.Game.CameraOffset centerCameraOffset = byInstanceId.Definition.CenterCameraOffset;
			float zoom = centerCameraOffset.zoom;
			global::UnityEngine.Vector3 first = new global::UnityEngine.Vector3(position.x + centerCameraOffset.x, position.y, position.z + centerCameraOffset.z);
			global::Kampai.Game.CameraMovementSettings cameraMovementSettings = new global::Kampai.Game.CameraMovementSettings(global::Kampai.Game.CameraMovementSettings.Settings.None, null, null);
			autoPanSignal.Dispatch(global::Kampai.Util.Tuple.Create(first, moveTime), cameraMovementSettings.settings, new global::Kampai.Util.Boxed<global::Kampai.Game.Building>(cameraMovementSettings.building), new global::Kampai.Util.Boxed<global::Kampai.Game.Quest>(cameraMovementSettings.quest));
			autoZoomSignal.Dispatch(global::Kampai.Util.Tuple.Create(zoom, moveTime));
		}
	}
}
