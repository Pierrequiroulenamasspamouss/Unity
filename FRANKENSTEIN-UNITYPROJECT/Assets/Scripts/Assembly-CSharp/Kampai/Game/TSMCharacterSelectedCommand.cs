namespace Kampai.Game
{
	public class TSMCharacterSelectedCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Util.ILogger Logger { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService PlayerService { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoMoveToInstanceSignal CameraAutoMoveToInstanceSignal { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.Quest firstInstanceByDefinitionId = PlayerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.Quest>(77777);
			if (firstInstanceByDefinitionId == null)
			{
				Logger.Warning("Ignoring tsm selection since there is no quest available, he's probably walking away.");
				return;
			}
			global::Kampai.Game.PanInstructions panInstructions = new global::Kampai.Game.PanInstructions(301);
			panInstructions.CameraMovementSettings = new global::Kampai.Game.CameraMovementSettings(global::Kampai.Game.CameraMovementSettings.Settings.Quest, null, firstInstanceByDefinitionId);
			panInstructions.Offset = new global::Kampai.Util.Boxed<global::UnityEngine.Vector3>(global::Kampai.Util.GameConstants.TSM_MODAL_OFFSET);
			panInstructions.ZoomDistance = new global::Kampai.Util.Boxed<float>(0.8f);
			CameraAutoMoveToInstanceSignal.Dispatch(panInstructions);
		}
	}
}
