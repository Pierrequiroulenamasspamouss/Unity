namespace Kampai.UI.Controller
{
	public class ShowOfflinePopupCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public bool isShow { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetupCanvasSignal setupCanvasSignal { get; set; }

		public override void Execute()
		{
			global::UnityEngine.Canvas overlayCanvas = GetOverlayCanvas();
			if (overlayCanvas == null && !isShow)
			{
				return;
			}
			global::Kampai.UI.View.OfflineView[] componentsInChildren = overlayCanvas.gameObject.GetComponentsInChildren<global::Kampai.UI.View.OfflineView>(true);
			if (componentsInChildren.Length == 0)
			{
				if (isShow)
				{
					string path = "UI/popup_Error_LostConnectivity";
					global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(global::UnityEngine.Resources.Load(path)) as global::UnityEngine.GameObject;
					gameObject.transform.SetParent(overlayCanvas.transform, false);
				}
			}
			else if (!isShow)
			{
				global::UnityEngine.Object.Destroy(componentsInChildren[0].gameObject);
			}
			else if (!componentsInChildren[0].gameObject.activeSelf)
			{
				componentsInChildren[0].gameObject.SetActive(true);
			}
		}

		private global::UnityEngine.Canvas GetOverlayCanvas()
		{
			global::UnityEngine.GameObject gameObject = null;
			global::strange.extensions.injector.api.IInjectionBinding binding = base.injectionBinder.GetBinding<global::UnityEngine.GameObject>(global::Kampai.Main.MainElement.UI_OVERLAY_CANVAS);
			if (binding == null || binding.value == null || ((global::UnityEngine.GameObject)binding.value).gameObject == null)
			{
				if (binding != null)
				{
					base.injectionBinder.Unbind(binding);
				}
				if (isShow)
				{
					setupCanvasSignal.Dispatch(global::Kampai.Util.Tuple.Create<string, global::Kampai.Main.MainElement, global::UnityEngine.Camera>("OverlayCanvas", global::Kampai.Main.MainElement.UI_OVERLAY_CANVAS, null));
					gameObject = base.injectionBinder.GetInstance<global::UnityEngine.GameObject>(global::Kampai.Main.MainElement.UI_OVERLAY_CANVAS);
				}
			}
			else
			{
				gameObject = (global::UnityEngine.GameObject)binding.value;
			}
			return (!(gameObject != null)) ? null : gameObject.GetComponent<global::UnityEngine.Canvas>();
		}
	}
}
