using UnityEngine;

namespace Kampai.Game.View
{
	public class MouseDragPanMediator : global::Kampai.Game.View.PanMediator
	{
		[Inject]
		public global::Kampai.Game.View.MouseDragPanView view { get; set; }

		private bool isMouseDown;

		public override void OnRegister()
		{
			base.OnRegister();
			Debug.Log("MouseDragPanMediator registered");
            // Subscribe to the view's update event
            // View should be injected by this point
            if (view != null)
            {
                Debug.Log("View injected correctly");
                view.UpdateCallback = HandleMouseInput;
            }
            else
            {
                Debug.LogError("View was NOT injected!");
            }
        }

		private void HandleMouseInput()
		{
			if (blocked)
			{
				return;
			}

			global::UnityEngine.Vector3 mousePosition = global::UnityEngine.Input.mousePosition;

			// Check for left mouse button down
			if (global::UnityEngine.Input.GetMouseButtonDown(0))
			{
				isMouseDown = true;
				view.StartDrag(mousePosition);
			}
			// Check for left mouse button held
			else if (global::UnityEngine.Input.GetMouseButton(0) && isMouseDown)
			{
				view.UpdateDrag(mousePosition);
			}
			// Check for left mouse button up
			else if (global::UnityEngine.Input.GetMouseButtonUp(0) || !global::UnityEngine.Input.GetMouseButton(0))
			{
				if (isMouseDown)
				{
					isMouseDown = false;
					view.EndDrag();
				}
			}
		}

		public override void OnGameInput(global::UnityEngine.Vector3 position, int input)
		{
			// Not used - we handle input via Update callback from view
		}

		public override void OnDisableBehaviour(int behaviour)
		{
			int panBehaviour = 8; // Pan behaviour flag
			if ((behaviour & panBehaviour) == panBehaviour)
			{
				if (!blocked)
				{
					blocked = true;
				}
				if ((base.model.CurrentBehaviours & panBehaviour) == panBehaviour)
				{
					base.model.CurrentBehaviours ^= panBehaviour;
				}
			}
		}

		public override void OnEnableBehaviour(int behaviour)
		{
			int panBehaviour = 8; // Pan behaviour flag
			if ((behaviour & panBehaviour) == panBehaviour)
			{
				if (blocked)
				{
					blocked = false;
				}
				if ((base.model.CurrentBehaviours & panBehaviour) != panBehaviour)
				{
					base.model.CurrentBehaviours ^= panBehaviour;
				}
			}
		}

		public override void Uninitialize()
		{
			if (view != null && view.IsDragging())
			{
				view.EndDrag();
			}
			isMouseDown = false;
		}

	}
}
