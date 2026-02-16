namespace Kampai.Game.View
{
	public class CameraAutoMoveToBuildingDefCommand : global::strange.extensions.command.impl.Command
	{
		private const float ZOOM_LEVEL_BUILDING = 0.3f;

		private static readonly global::UnityEngine.Vector3 CAMERA_OFFSET_BUILDING = new global::UnityEngine.Vector3(14f, 0f, -14f);

		[Inject]
		public global::Kampai.Game.BuildingDefinition def { get; set; }

		[Inject]
		public global::Kampai.Game.Location pos { get; set; }

		[Inject]
		public global::Kampai.Game.PanInstructions panInstructions { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoMoveSignal autoMoveSignal { get; set; }

		public override void Execute()
		{
			global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(pos.x, 0f, pos.y);
			global::Kampai.Game.CameraOffset centerCameraOffset = def.CenterCameraOffset;
			float num;
			if (centerCameraOffset != null)
			{
				vector.x += centerCameraOffset.x;
				vector.z += centerCameraOffset.z;
				num = centerCameraOffset.zoom;
			}
			else
			{
				vector += CAMERA_OFFSET_BUILDING;
				num = 0.3f;
			}
			global::Kampai.Util.Boxed<global::UnityEngine.Vector3> offset = panInstructions.Offset;
			global::Kampai.Util.Boxed<float> zoomDistance = panInstructions.ZoomDistance;
			global::UnityEngine.Vector3 type = ((offset != null) ? (offset.Value + vector) : vector);
			num = ((zoomDistance != null) ? zoomDistance.Value : num);
			autoMoveSignal.Dispatch(type, num, new global::Kampai.Game.CameraMovementSettings(global::Kampai.Game.CameraMovementSettings.Settings.None, null, null));
		}
	}
}
