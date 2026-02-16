namespace Kampai.Game
{
	public class PanAndOpenModalCommand : global::strange.extensions.command.impl.Command
	{
		private global::UnityEngine.Vector3 genericBuildingOffset = new global::UnityEngine.Vector3(14f, 0f, -14f);

		[Inject]
		public int buildingID { get; set; }

		[Inject(global::Kampai.Game.GameElement.BUILDING_MANAGER)]
		public global::UnityEngine.GameObject buildingManager { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoMoveSignal autoMoveSignal { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.View.BuildingManagerView component = buildingManager.GetComponent<global::Kampai.Game.View.BuildingManagerView>();
			global::Kampai.Game.View.BuildingObject buildingObject = component.GetBuildingObject(buildingID);
			if (buildingObject == null)
			{
				return;
			}
			global::UnityEngine.Vector3 zero = global::UnityEngine.Vector3.zero;
			bool flag = false;
			global::Kampai.Game.View.ScaffoldingBuildingObject scaffoldingBuildingObject = buildingObject as global::Kampai.Game.View.ScaffoldingBuildingObject;
			if (scaffoldingBuildingObject != null)
			{
				flag = true;
			}
			zero = buildingObject.transform.position;
			global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(buildingID);
			if (byInstanceId != null)
			{
				global::Kampai.Game.CameraOffset modalOffset = byInstanceId.Definition.ModalOffset;
				float type = 0.5f;
				if (modalOffset != null)
				{
					zero.x += modalOffset.x;
					zero.z += modalOffset.z;
					type = modalOffset.zoom;
				}
				else
				{
					zero += genericBuildingOffset;
				}
				global::Kampai.Game.CameraMovementSettings.Settings settings = ((!flag) ? global::Kampai.Game.CameraMovementSettings.Settings.ShowMenu : global::Kampai.Game.CameraMovementSettings.Settings.None);
				autoMoveSignal.Dispatch(zero, type, new global::Kampai.Game.CameraMovementSettings(settings, byInstanceId, null));
			}
		}
	}
}
