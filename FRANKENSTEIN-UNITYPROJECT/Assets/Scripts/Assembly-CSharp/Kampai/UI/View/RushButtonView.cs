namespace Kampai.UI.View
{
	public class RushButtonView : global::Kampai.UI.View.DoubleConfirmButtonView
	{
		public global::Kampai.Util.QuantityItem Item;

		public global::strange.extensions.signal.impl.Signal<int, global::Kampai.Util.QuantityItem, bool> RushButtonClickedSignal = new global::strange.extensions.signal.impl.Signal<int, global::Kampai.Util.QuantityItem, bool>();

		public int RushCost { get; set; }

		public bool SkipDoubleConfirm { get; set; }

		public override void OnClickEvent()
		{
			updateTapCount();
			if (!isDoubleConfirmed() && !SkipDoubleConfirm)
			{
				ShowConfirmMessage();
			}
			ClickedSignal.Dispatch();
			RushButtonClickedSignal.Dispatch(RushCost, Item, isDoubleConfirmed());
		}
	}
}
