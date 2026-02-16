namespace Kampai.UI
{
	public class PositionService : global::Kampai.UI.IPositionService
	{
		private global::UnityEngine.Vector3[] viewportBoundaryCorners = new global::UnityEngine.Vector3[4];

		[Inject(global::Kampai.Main.MainElement.CAMERA)]
		public global::UnityEngine.Camera MainCamera { get; set; }

		[Inject(global::Kampai.UI.View.UIElement.CAMERA)]
		public global::UnityEngine.Camera UICamera { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger Logger { get; set; }

		public global::System.Collections.Generic.List<global::UnityEngine.GameObject> HUDElementsToAvoid { get; set; }

		public global::Kampai.UI.PositionData GetPositionData(global::UnityEngine.Vector3 worldPosition)
		{
			global::UnityEngine.Vector3 vector = MainCamera.WorldToViewportPoint(worldPosition);
			global::UnityEngine.Vector3 worldPositionInUI = UICamera.ViewportToWorldPoint(vector);
			return new global::Kampai.UI.PositionData(worldPositionInUI, vector);
		}

		public global::Kampai.UI.SnappablePositionData GetSnappablePositionData(global::Kampai.UI.PositionData normalPositionData, global::Kampai.UI.ViewportBoundary boundary, bool avoidHudElements = false)
		{
			global::UnityEngine.Vector3 viewportPosition = normalPositionData.ViewportPosition;
			global::UnityEngine.Vector3 vector = ClampViewportPosition(viewportPosition, boundary, avoidHudElements);
			global::UnityEngine.Vector3 clampedWorldPositionInUI = UICamera.ViewportToWorldPoint(vector);
			return new global::Kampai.UI.SnappablePositionData(normalPositionData.WorldPositionInUI, clampedWorldPositionInUI, viewportPosition, vector);
		}

		private global::Kampai.UI.ViewportBoundary GetViewportBoundary(global::UnityEngine.RectTransform transform)
		{
			transform.GetWorldCorners(viewportBoundaryCorners);
			global::UnityEngine.Vector2 vector = UICamera.WorldToViewportPoint(viewportBoundaryCorners[0]);
			global::UnityEngine.Vector2 vector2 = UICamera.WorldToViewportPoint(viewportBoundaryCorners[2]);
			global::UnityEngine.Vector2 vector3 = new global::UnityEngine.Vector2(vector.x + (vector2.x - vector.x) / 2f, vector.y + (vector2.y - vector.y) / 2f);
			if (vector3.x <= 0.5f && vector3.y <= 0.5f)
			{
				return new global::Kampai.UI.ViewportBoundary(0f, 0f, vector2.y, vector2.x);
			}
			if ((double)vector3.x <= 0.5 && vector3.y > 0.5f)
			{
				return new global::Kampai.UI.ViewportBoundary(vector.y, 0f, 1f, vector2.x);
			}
			if (vector3.x > 0.5f && vector3.y > 0.5f)
			{
				return new global::Kampai.UI.ViewportBoundary(vector.y, vector.x, 1f, 1f);
			}
			return new global::Kampai.UI.ViewportBoundary(0f, vector.x, vector2.y, 1f);
		}

		private global::UnityEngine.Vector3 ClampViewportPosition(global::UnityEngine.Vector3 viewportPosition, global::Kampai.UI.ViewportBoundary boundary, bool avoidHudElements)
		{
			float x = viewportPosition.x;
			float left = boundary.Left;
			float right = boundary.Right;
			viewportPosition.x = ((x < left) ? left : ((!(x > right)) ? x : right));
			float y = viewportPosition.y;
			float top = boundary.Top;
			float bottom = boundary.Bottom;
			viewportPosition.y = ((y < bottom) ? bottom : ((!(y > top)) ? y : top));
			if (avoidHudElements && HUDElementsToAvoid != null)
			{
				global::System.Collections.Generic.List<global::UnityEngine.GameObject>.Enumerator enumerator = HUDElementsToAvoid.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						global::UnityEngine.GameObject current = enumerator.Current;
						if (current != null && current.activeInHierarchy)
						{
							global::Kampai.UI.ViewportBoundary viewportBoundary = GetViewportBoundary(current.transform as global::UnityEngine.RectTransform);
							if (viewportBoundary.Contains(viewportPosition))
							{
								viewportPosition = ClampViewportPositionForBoundary(x, y, viewportBoundary);
								break;
							}
						}
					}
				}
				finally
				{
					enumerator.Dispose();
				}
			}
			return viewportPosition;
		}

		private global::UnityEngine.Vector2 ClampViewportPositionForBoundary(float originalX, float originalY, global::Kampai.UI.ViewportBoundary boundary)
		{
			bool flag = global::UnityEngine.Mathf.Approximately(boundary.Bottom, 0f);
			bool flag2 = global::UnityEngine.Mathf.Approximately(boundary.Left, 0f);
			bool flag3 = global::UnityEngine.Mathf.Approximately(boundary.Top, 1f);
			float x = global::UnityEngine.Mathf.Clamp(originalX, boundary.Left, boundary.Right);
			float y = global::UnityEngine.Mathf.Clamp(originalY, boundary.Bottom, boundary.Top);
			if (flag2 && (flag || flag3))
			{
				if (originalX <= boundary.Right)
				{
					return new global::UnityEngine.Vector2(x, (!flag) ? boundary.Bottom : boundary.Top);
				}
				return new global::UnityEngine.Vector2(boundary.Right, y);
			}
			if (originalX >= boundary.Left)
			{
				return new global::UnityEngine.Vector2(x, (!flag) ? boundary.Bottom : boundary.Top);
			}
			return new global::UnityEngine.Vector2(boundary.Left, y);
		}
	}
}
