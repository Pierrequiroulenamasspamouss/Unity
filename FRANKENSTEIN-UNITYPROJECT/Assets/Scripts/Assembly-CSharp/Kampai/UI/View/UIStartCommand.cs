namespace Kampai.UI.View
{
	public class UIStartCommand : global::strange.extensions.command.impl.Command
	{
		[Inject(global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW)]
		public global::UnityEngine.GameObject contextView { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetupCanvasSignal canvasSignal { get; set; }


		//[Inject(global::Kampai.Main.MainElement.CAMERA)]
		public global::UnityEngine.Camera mainCamera { get; set; }

		//[Inject(global::Kampai.UI.View.UIElement.CAMERA)]
		public global::UnityEngine.Camera uiCamera { get; set; }

		[Inject]
		public global::Kampai.UI.View.LoadGUISignal loadGUISignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.LoadUICompleteSignal loadUICompleteSignal { get; set; }

		[Inject]
		public global::Kampai.Main.SetupDeepLinkSignal setupDeepLinkSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.UI.View.AddFPSCounterSignal addFPSCounterSignal { get; set; }

		public override void Execute()
		{
			logger.EventStart("UIStartCommand.Execute");
			global::Kampai.Util.TimeProfiler.StartSection("ui");
			global::Kampai.Util.TimeProfiler.StartSection("canvas");
            
            // --- FIX: Manual Camera Lookup ---
            if (mainCamera == null)
            {
                global::UnityEngine.GameObject camObj = global::UnityEngine.GameObject.FindWithTag("MainCamera");
                if (camObj != null) mainCamera = camObj.GetComponent<global::UnityEngine.Camera>();
                if (mainCamera == null) mainCamera = global::UnityEngine.Camera.main;
            }

			canvasSignal.Dispatch(global::Kampai.Util.Tuple.Create("GlassCanvas", global::Kampai.Main.MainElement.UI_GLASSCANVAS, uiCamera));
			canvasSignal.Dispatch(global::Kampai.Util.Tuple.Create("WorldCanvas", global::Kampai.Main.MainElement.UI_WORLDCANVAS, mainCamera));
			global::Kampai.Util.TimeProfiler.EndSection("canvas");
			global::Kampai.Util.TimeProfiler.StartSection("gui");
			loadGUISignal.Dispatch();
			global::Kampai.Util.TimeProfiler.EndSection("gui");
			global::Kampai.Util.TimeProfiler.StartSection("complete");
			loadUICompleteSignal.Dispatch(contextView);
			global::Kampai.Util.TimeProfiler.EndSection("complete");
			setupDeepLinkSignal.Dispatch();
			global::Kampai.Util.TimeProfiler.EndSection("ui");
			logger.EventStop("UIStartCommand.Execute");
		}
	}
}
