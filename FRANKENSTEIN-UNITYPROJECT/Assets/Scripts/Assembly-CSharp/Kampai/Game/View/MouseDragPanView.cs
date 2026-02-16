namespace Kampai.Game.View
{
	public class MouseDragPanView : global::strange.extensions.mediation.impl.View
	{
		private global::UnityEngine.Vector3 lastMousePosition;
		private bool isDragging;
		private global::UnityEngine.Camera mainCamera;
		private global::Kampai.Game.View.CameraUtils cameraUtils;

		public global::System.Action UpdateCallback;

		protected override void Start()
		{
			base.Start();
			
			// Find camera manually
			mainCamera = GetComponent<global::UnityEngine.Camera>();
			if (mainCamera == null)
			{
				mainCamera = global::UnityEngine.Camera.main;
			}
			if (mainCamera == null)
			{
				global::UnityEngine.GameObject camGo = global::UnityEngine.GameObject.FindWithTag("MainCamera");
				if (camGo != null)
				{
					mainCamera = camGo.GetComponent<global::UnityEngine.Camera>();
				}
			}

			// Find CameraUtils manually (avoid injection timing issues)
			if (mainCamera != null)
			{
				cameraUtils = mainCamera.GetComponent<global::Kampai.Game.View.CameraUtils>();
			}
		}

		protected virtual void Update()
		{
			if (UpdateCallback != null)
			{
				UpdateCallback();
			}
		}

		public virtual void StartDrag(global::UnityEngine.Vector3 mousePosition)
		{
			isDragging = true;
			lastMousePosition = mousePosition;
		}

		public virtual void UpdateDrag(global::UnityEngine.Vector3 mousePosition)
		{
			if (!isDragging || mainCamera == null)
			{
				return;
			}

			// Simple drag: move camera based on screen space delta
			global::UnityEngine.Vector3 delta = mousePosition - lastMousePosition;
			
			// Convert screen delta to world delta
			// Scale based on camera distance from ground
			float distanceScale = mainCamera.transform.position.y * 0.1f;
			global::UnityEngine.Vector3 worldDelta = new global::UnityEngine.Vector3(-delta.x * distanceScale, 0f, -delta.y * distanceScale);
			
			// Rotate by camera angle
			global::UnityEngine.Quaternion rotation = global::UnityEngine.Quaternion.Euler(0f, mainCamera.transform.eulerAngles.y, 0f);
			worldDelta = rotation * worldDelta;

			// Apply movement
			mainCamera.transform.position += worldDelta;

			// Clamp to bounds
			global::UnityEngine.Vector3 pos = mainCamera.transform.position;
			pos.x = global::UnityEngine.Mathf.Clamp(pos.x, 10f, 100f);
			pos.z = global::UnityEngine.Mathf.Clamp(pos.z, -20f, 70f);
			mainCamera.transform.position = pos;

			lastMousePosition = mousePosition;
		}

		public virtual void EndDrag()
		{
			isDragging = false;
		}

		public bool IsDragging()
		{
			return isDragging;
		}
	}
}
