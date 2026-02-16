using UnityEngine;

namespace Kampai.Game.View
{
	/// <summary>
	/// Simple mouse drag camera controller.
	/// Attach directly to the Game Camera GameObject.
	/// Does NOT use StrangeIoC mediation to avoid ContextView hierarchy issues.
	/// </summary>
	public class SimpleMouseDragCamera : MonoBehaviour
	{
		private Vector3 lastMousePosition;
		private bool isDragging;
		private Camera mainCamera;

		void Start()
		{
			mainCamera = GetComponent<Camera>();
			if (mainCamera == null)
			{
				mainCamera = Camera.main;
			}
			
			Debug.Log("[SimpleMouseDragCamera] Initialized on " + gameObject.name);
		}

		void Update()
	{
		Vector3 mousePosition = Input.mousePosition;

		// Handle zoom with scroll wheel
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		if (scroll != 0f && mainCamera != null)
		{
			// Zoom by moving camera up/down
			Vector3 pos = mainCamera.transform.position;
			pos.y = Mathf.Clamp(pos.y - scroll * 50f, 5f, 100f); // Limit zoom range
			mainCamera.transform.position = pos;
		}

		// Check for left mouse button down
		if (Input.GetMouseButtonDown(0))
		{
			isDragging = true;
			lastMousePosition = mousePosition;
			//Debug.Log("[SimpleMouseDragCamera] Start drag at " + mousePosition);
		}
		// Check for left mouse button held
		else if (Input.GetMouseButton(0) && isDragging)
		{
			UpdateDrag(mousePosition);
		}
		// Check for left mouse button up
		else if (Input.GetMouseButtonUp(0) || !Input.GetMouseButton(0))
		{
			if (isDragging)
			{
				isDragging = false;
				//Debug.Log("[SimpleMouseDragCamera] End drag");
			}
		}
	}

	private void UpdateDrag(Vector3 mousePosition)
	{
		if (!isDragging || mainCamera == null)
		{
			return;
		}

		// Convert screen positions to world coordinates at ground level (y=0)
		Ray oldRay = mainCamera.ScreenPointToRay(lastMousePosition);
		Ray newRay = mainCamera.ScreenPointToRay(mousePosition);
		
		// Find where rays hit ground plane (y=0)
		Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
		float oldDist, newDist;
		
		Vector3 worldDelta = Vector3.zero;
		if (groundPlane.Raycast(oldRay, out oldDist) && groundPlane.Raycast(newRay, out newDist))
		{
			Vector3 oldWorldPos = oldRay.GetPoint(oldDist);
			Vector3 newWorldPos = newRay.GetPoint(newDist);
			worldDelta = oldWorldPos - newWorldPos; // Reversed for drag feel
		}

		// Apply movement (no bounds!)
		mainCamera.transform.position += worldDelta;

		lastMousePosition = mousePosition;
	}
	}
}
