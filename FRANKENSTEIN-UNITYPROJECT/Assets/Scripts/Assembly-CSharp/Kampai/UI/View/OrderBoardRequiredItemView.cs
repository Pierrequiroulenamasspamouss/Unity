namespace Kampai.UI.View
{
	public class OrderBoardRequiredItemView : global::Kampai.UI.View.PopupInfoButtonView
	{
		public global::Kampai.UI.View.KampaiImage ItemIcon;

		public global::UnityEngine.UI.Text ItemCount;

		public global::UnityEngine.GameObject CheckMark;

		public global::UnityEngine.GameObject XMark;

		public new global::strange.extensions.signal.impl.Signal<int, global::UnityEngine.RectTransform> pointerDownSignal = new global::strange.extensions.signal.impl.Signal<int, global::UnityEngine.RectTransform>();

		public int ItemDefinitionID { get; set; }

		public void Init()
		{
		}

		public override void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			pointerDownSignal.Dispatch(ItemDefinitionID, ItemIcon.rectTransform);
		}
	}
}
