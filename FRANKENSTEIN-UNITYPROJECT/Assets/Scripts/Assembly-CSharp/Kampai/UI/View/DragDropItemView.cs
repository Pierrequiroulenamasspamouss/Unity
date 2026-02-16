namespace Kampai.UI.View
{
	public class DragDropItemView : global::strange.extensions.mediation.impl.View, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IPointerDownHandler, global::UnityEngine.EventSystems.IDragHandler, global::UnityEngine.EventSystems.IBeginDragHandler, global::UnityEngine.EventSystems.IEndDragHandler
	{
		public global::UnityEngine.Transform ObjectToMove;

		internal global::strange.extensions.signal.impl.Signal<global::UnityEngine.EventSystems.PointerEventData> OnDragSignal = new global::strange.extensions.signal.impl.Signal<global::UnityEngine.EventSystems.PointerEventData>();

		internal global::strange.extensions.signal.impl.Signal<global::UnityEngine.EventSystems.PointerEventData> OnDropSignal = new global::strange.extensions.signal.impl.Signal<global::UnityEngine.EventSystems.PointerEventData>();

		internal global::strange.extensions.signal.impl.Signal<global::UnityEngine.EventSystems.PointerEventData> OnStartSignal = new global::strange.extensions.signal.impl.Signal<global::UnityEngine.EventSystems.PointerEventData>();

		private global::UnityEngine.CanvasGroup canvasGroup;

		internal void Init()
		{
			canvasGroup = base.gameObject.GetComponent<global::UnityEngine.CanvasGroup>();
			if (canvasGroup == null)
			{
				canvasGroup = base.gameObject.AddComponent<global::UnityEngine.CanvasGroup>();
			}
		}

		public void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			OnStartSignal.Dispatch(eventData);
		}

		public void OnBeginDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			canvasGroup.blocksRaycasts = false;
		}

		public void OnDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			OnDragSignal.Dispatch(eventData);
		}

		public void OnEndDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			canvasGroup.blocksRaycasts = true;
			OnDropSignal.Dispatch(eventData);
		}
	}
}
