namespace Kampai.Game
{
	public class CameraAutoMoveToInstanceCommand : global::strange.extensions.command.impl.Command
	{
		private const float ZOOM_LEVEL_ACTIONABLE_OBJECT = 0.6f;

		private global::UnityEngine.Vector3 CAMERA_OFFSET_VILLAIN = new global::UnityEngine.Vector3(-2f, 0f, 1.3f);

		[Inject]
		public global::Kampai.Game.PanInstructions panInstructions { get; set; }

		[Inject(global::Kampai.Game.GameElement.VILLAIN_MANAGER)]
		public global::UnityEngine.GameObject villainManager { get; set; }

		[Inject(global::Kampai.Game.GameElement.MINION_MANAGER)]
		public global::UnityEngine.GameObject minionManager { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoMoveSignal autoMoveSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CameraInstanceFocusSignal buildingFocusSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoMoveToBuildingSignal buildingMoveSignal { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.Instance instance = panInstructions.Instance;
			if (instance == null)
			{
				instance = playerService.GetByInstanceId<global::Kampai.Game.Instance>(panInstructions.InstanceId);
			}
			global::Kampai.Game.Building building = instance as global::Kampai.Game.Building;
			if (building != null)
			{
				buildingMoveSignal.Dispatch(building, panInstructions);
				return;
			}
			global::Kampai.Game.View.ActionableObject fromAllObjects = global::Kampai.Game.View.ActionableObjectManagerView.GetFromAllObjects(instance.ID);
			if (fromAllObjects == null)
			{
				logger.Error("CameraAutoMoveToInstanceCommand: Cannot find object {0} {1}", instance, instance.GetType());
				return;
			}
			global::Kampai.Util.Boxed<global::UnityEngine.Vector3> offset = panInstructions.Offset;
			global::Kampai.Util.Boxed<float> zoomDistance = panInstructions.ZoomDistance;
			bool flag = false;
			global::UnityEngine.Vector3 vector = fromAllObjects.transform.position;
			global::Kampai.Game.View.CharacterObject characterObject = fromAllObjects as global::Kampai.Game.View.CharacterObject;
			if (characterObject != null)
			{
				flag = true;
				vector = ((!(characterObject is global::Kampai.Game.View.VillainView)) ? characterObject.GetIndicatorPosition() : (characterObject.GetIndicatorPosition() + CAMERA_OFFSET_VILLAIN));
			}
			global::UnityEngine.Vector3 type = vector + ((offset != null) ? offset.Value : global::Kampai.Util.GameConstants.CAMERA_OFFSET_ACTIONABLE_OBJECT);
			float type2 = ((zoomDistance != null) ? zoomDistance.Value : 0.6f);
			global::Kampai.Game.CameraMovementSettings type3 = ((panInstructions.CameraMovementSettings != null) ? panInstructions.CameraMovementSettings : new global::Kampai.Game.CameraMovementSettings(global::Kampai.Game.CameraMovementSettings.Settings.None, null, null));
			autoMoveSignal.Dispatch(type, type2, type3);
			if (!flag)
			{
				buildingFocusSignal.Dispatch(-1, vector);
			}
		}
	}
}
