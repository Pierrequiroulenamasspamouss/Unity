namespace Kampai.Game
{
	public class TouchInput : global::Kampai.Game.IInput
	{
		private global::UnityEngine.Vector3 position;

		private int touchCount;

		private bool pressed;

		private bool isDeviceSamsung;

		private bool wasStylusActive;

		[Inject(global::strange.extensions.context.api.ContextKeys.CONTEXT_DISPATCHER)]
		public global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher dispatcher { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Common.IPickService pickService { get; set; }

		[Inject]
		public global::Kampai.Game.Mignette.IMignetteService mignetteService { get; set; }

		[Inject]
		public global::Kampai.Util.DeviceInformation deviceInformation { get; set; }

		[PostConstruct]
		public void PostConstruct()
		{
			isDeviceSamsung = deviceInformation.IsSamsung();
			routineRunner.StartCoroutine(Update());
		}

		private global::System.Collections.IEnumerator Update()
		{
			yield return null;
			while (true)
			{
				position = global::UnityEngine.Vector3.zero;
				touchCount = global::UnityEngine.Input.touchCount;
				if (touchCount > 0)
				{
					global::UnityEngine.Touch touch = global::UnityEngine.Input.GetTouch(0);
					position = touch.position;
					if (touch.phase == global::UnityEngine.TouchPhase.Began)
					{
						pressed = true;
					}
					else if (touch.phase == global::UnityEngine.TouchPhase.Ended)
					{
						pressed = false;
					}
				}
				else if (isDeviceSamsung)
				{
					bool isStylusActive = global::UnityEngine.Input.GetMouseButton(0);
					if (isStylusActive || wasStylusActive)
					{
						position = global::UnityEngine.Input.mousePosition;
						touchCount = 1;
						pressed = (wasStylusActive = isStylusActive);
					}
				}
				pickService.OnGameInput(position, touchCount, pressed);
				mignetteService.OnGameInput(position, touchCount, pressed);
				yield return null;
			}
		}
	}
}
