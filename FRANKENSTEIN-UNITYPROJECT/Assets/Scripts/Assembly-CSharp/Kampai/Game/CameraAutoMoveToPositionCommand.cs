namespace Kampai.Game
{
	public class CameraAutoMoveToPositionCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::UnityEngine.Vector3 position { get; set; }

		[Inject]
		public float zoom { get; set; }

		[Inject]
		public bool useOffset { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoMoveSignal autoMoveSignal { get; set; }

		public override void Execute()
		{
			global::UnityEngine.Vector3 type = position;
			if (useOffset)
			{
				type += global::Kampai.Util.GameConstants.CAMERA_OFFSET_ACTIONABLE_OBJECT;
			}
			autoMoveSignal.Dispatch(type, zoom, new global::Kampai.Game.CameraMovementSettings(global::Kampai.Game.CameraMovementSettings.Settings.None, null, null));
		}
	}
}
