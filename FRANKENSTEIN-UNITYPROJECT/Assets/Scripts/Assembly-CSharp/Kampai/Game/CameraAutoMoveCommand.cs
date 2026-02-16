namespace Kampai.Game
{
	public class CameraAutoMoveCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::UnityEngine.Vector3 position { get; set; }

		[Inject]
		public float zoomPercentage { get; set; }

		[Inject]
		public global::Kampai.Game.CameraMovementSettings modalInfo { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoZoomSignal autoZoomSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoPanSignal autoPanSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CameraModel model { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject(global::Kampai.UI.View.UIElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable uiContext { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal playSoundFXSignal { get; set; }

		[Inject]
		public global::Kampai.Common.PickControllerModel pickModel { get; set; }

		[Inject]
		public global::Kampai.Game.ShowHiddenBuildingsSignal showHiddenBuildingsSignal { get; set; }

		public override void Execute()
		{
			showHiddenBuildingsSignal.Dispatch();
			if ((model.CurrentBehaviours & 8) != 8 && pickModel.CameraControlEnabled)
			{
				pickModel.PanningCameraBlocked = true;
				pickModel.ZoomingCameraBlocked = true;
				autoPanSignal.Dispatch(position, modalInfo.settings, new global::Kampai.Util.Boxed<global::Kampai.Game.Building>(modalInfo.building), new global::Kampai.Util.Boxed<global::Kampai.Game.Quest>(modalInfo.quest));
				autoZoomSignal.Dispatch(zoomPercentage);
				if (pickModel.PanningCameraBlocked || pickModel.ZoomingCameraBlocked)
				{
					playSoundFXSignal.Dispatch("Play_low_woosh_01");
				}
				if (modalInfo.settings != global::Kampai.Game.CameraMovementSettings.Settings.KeepUIOpen)
				{
					GetUISignal<global::Kampai.UI.View.CloseAllOtherMenuSignal>().Dispatch(null);
				}
			}
		}

		private T GetUISignal<T>()
		{
			return uiContext.injectionBinder.GetInstance<T>();
		}
	}
}
