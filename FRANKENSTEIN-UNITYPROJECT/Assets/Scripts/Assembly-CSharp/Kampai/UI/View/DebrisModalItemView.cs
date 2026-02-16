namespace Kampai.UI.View
{
	public class DebrisModalItemView : global::strange.extensions.mediation.impl.View, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IDragHandler
	{
		public global::UnityEngine.GameObject BackingOn;

		public global::UnityEngine.GameObject BackingOff;

		public global::UnityEngine.UI.Text QuantityText;

		public global::UnityEngine.RectTransform DragContainer;

		public global::Kampai.UI.View.KampaiImage Image;

		public global::UnityEngine.GameObject GlowBacking;

		private global::UnityEngine.Vector2 initialPosition;

		private global::UnityEngine.Camera uiCamera;

		internal void Init(global::UnityEngine.Camera uiCamera)
		{
			this.uiCamera = uiCamera;
		}

		internal void Init(string image, string mask, int amountAvailble, int amountRequired)
		{
			Image.sprite = UIUtils.LoadSpriteFromPath(image);
			Image.maskSprite = UIUtils.LoadSpriteFromPath(mask);
			UpdateQuantity(amountAvailble, amountRequired);
			initialPosition = DragContainer.anchoredPosition;
			Highlight(false);
		}

		internal void UpdateQuantity(int quantity, int quantityRequired)
		{
			QuantityText.text = string.Format("{0}/{1}", quantity, quantityRequired);
			bool flag = quantity >= quantityRequired;
			BackingOn.SetActive(flag);
			BackingOff.SetActive(!flag);
		}

		public void OnDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			DragContainer.position = uiCamera.ScreenToWorldPoint(eventData.position);
			DragContainer.localPosition = VectorUtils.ZeroZ(DragContainer.localPosition);
		}

		public void ResetPosition(bool animate)
		{
			if (!animate)
			{
				DragContainer.anchoredPosition = initialPosition;
				return;
			}
			Go.killAllTweensWithTarget(base.transform);
			Go.to(DragContainer, 0.25f, new GoTweenConfig().setEaseType(GoEaseType.Linear).vector2Prop("anchoredPosition", initialPosition).onComplete(delegate(AbstractGoTween thisTween)
			{
				thisTween.destroy();
			}));
		}

		public void Highlight(bool enable)
		{
			GlowBacking.SetActive(enable);
		}
	}
}
