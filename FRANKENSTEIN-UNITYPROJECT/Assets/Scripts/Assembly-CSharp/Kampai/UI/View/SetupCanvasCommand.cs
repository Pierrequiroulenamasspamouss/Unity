namespace Kampai.UI.View
{
	public class SetupCanvasCommand : global::strange.extensions.command.impl.Command
	{
		private string name;

		private global::Kampai.Main.MainElement mainElement;

		private global::UnityEngine.Camera worldCamera;

		[Inject]
		public global::Kampai.Util.Tuple<string, global::Kampai.Main.MainElement, global::UnityEngine.Camera> args { get; set; }

		public override void Execute()
		{
			name = args.Item1;
			mainElement = args.Item2;
			worldCamera = args.Item3;
			global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject(name);
			switch (mainElement)
			{
			case global::Kampai.Main.MainElement.UI_GLASSCANVAS:
			case global::Kampai.Main.MainElement.UI_OVERLAY_CANVAS:
			{
				gameObject.layer = 5;
				bool flag = mainElement == global::Kampai.Main.MainElement.UI_OVERLAY_CANVAS;
				if (!flag)
				{
					gameObject.AddComponent<global::UnityEngine.CanvasRenderer>();
				}
				AddCanvas(gameObject, (!flag) ? global::UnityEngine.RenderMode.ScreenSpaceCamera : global::UnityEngine.RenderMode.ScreenSpaceOverlay);
				AddCanvasScaler(gameObject);
				base.injectionBinder.injector.Inject(gameObject.AddComponent<global::Kampai.UI.View.KampaiRaycaster>());
				break;
			}
			case global::Kampai.Main.MainElement.UI_WORLDCANVAS:
				gameObject.transform.localPosition = global::UnityEngine.Vector3.zero;
				gameObject.AddComponent<global::UnityEngine.CanvasRenderer>();
				AddCanvas(gameObject, global::UnityEngine.RenderMode.WorldSpace);
				gameObject.AddComponent<global::Kampai.UI.View.KampaiWorldRaycaster>();
				break;
			}
			global::strange.extensions.injector.api.IInjectionBinding binding = base.injectionBinder.GetBinding<global::strange.extensions.context.api.ICrossContextCapable>(global::Kampai.UI.View.UIElement.CONTEXT);
			global::UnityEngine.Transform transform = gameObject.transform;
			global::strange.extensions.injector.api.IInjectionBinder obj;
			if (binding != null)
			{
				global::strange.extensions.injector.api.IInjectionBinder injectionBinder = ((global::strange.extensions.context.api.ICrossContextCapable)binding.value).injectionBinder;
				obj = injectionBinder;
			}
			else
			{
				obj = base.injectionBinder;
			}
			transform.SetParent(obj.GetInstance<global::UnityEngine.GameObject>(global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW).transform, false);
			base.injectionBinder.Bind<global::UnityEngine.GameObject>().ToValue(gameObject).ToName(mainElement)
				.CrossContext()
				.Weak();
		}

		private void AddCanvas(global::UnityEngine.GameObject canvasGO, global::UnityEngine.RenderMode renderMode)
		{
			global::UnityEngine.Canvas canvas = canvasGO.AddComponent<global::UnityEngine.Canvas>();
			canvas.renderMode = renderMode;
			canvas.planeDistance = 25f;
			if (worldCamera != null)
			{
				canvas.worldCamera = worldCamera;
			}
			if (mainElement == global::Kampai.Main.MainElement.UI_OVERLAY_CANVAS)
			{
				canvas.sortingOrder = 32767;
			}
		}

		private void AddCanvasScaler(global::UnityEngine.GameObject canvasGO)
		{
			global::UnityEngine.UI.CanvasScaler canvasScaler = canvasGO.AddComponent<global::UnityEngine.UI.CanvasScaler>();
			canvasScaler.uiScaleMode = global::UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
			canvasScaler.matchWidthOrHeight = 1f;
			canvasScaler.referenceResolution = new global::UnityEngine.Vector2(960f, 640f);
		}
	}
}
