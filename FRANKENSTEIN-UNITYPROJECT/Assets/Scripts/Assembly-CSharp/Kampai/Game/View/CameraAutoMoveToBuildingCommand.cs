namespace Kampai.Game.View
{
	public class CameraAutoMoveToBuildingCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.Building building { get; set; }

		[Inject]
		public global::Kampai.Game.PanInstructions panInstructions { get; set; }

		[Inject(global::Kampai.Game.GameElement.BUILDING_MANAGER)]
		public global::UnityEngine.GameObject buildingManager { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoMoveToBuildingDefSignal autoMoveSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CameraInstanceFocusSignal focusSignal { get; set; }

		public override void Execute()
		{
			int iD = building.ID;
			global::Kampai.Game.View.BuildingManagerView component = buildingManager.GetComponent<global::Kampai.Game.View.BuildingManagerView>();
			global::Kampai.Game.View.BuildingObject buildingObject = component.GetBuildingObject(iD);
			if (buildingObject == null)
			{
				global::Kampai.Game.View.ScaffoldingBuildingObject scaffoldingBuildingObject = component.GetScaffoldingBuildingObject(iD);
				if (scaffoldingBuildingObject == null)
				{
					return;
				}
				buildingObject = scaffoldingBuildingObject;
			}
			global::UnityEngine.Vector3 center = buildingObject.Center;
			autoMoveSignal.Dispatch(building.Definition, building.Location, panInstructions);
			focusSignal.Dispatch(building.ID, center);
		}
	}
}
