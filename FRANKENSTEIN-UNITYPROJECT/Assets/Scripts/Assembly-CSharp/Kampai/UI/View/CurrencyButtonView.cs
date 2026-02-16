namespace Kampai.UI.View
{
	public class CurrencyButtonView : global::Kampai.UI.View.ButtonView
	{
		public global::UnityEngine.UI.Text Description;

		public global::UnityEngine.UI.Text ItemPrice;

		public global::UnityEngine.UI.Text ItemWorth;

		public global::Kampai.UI.View.KampaiImage ItemImage;

		public global::Kampai.UI.View.KampaiImage CostCurrencyIcon;

		public global::UnityEngine.Transform VFXRoot;

		public float WidthInAnchor;

		public float HeightInAnchor;

		public new global::strange.extensions.signal.impl.Signal<global::Kampai.Game.StoreItemDefinition> ClickedSignal = new global::strange.extensions.signal.impl.Signal<global::Kampai.Game.StoreItemDefinition>();

		public global::Kampai.Game.StoreItemDefinition Definition { get; set; }

		public bool isCOPPAGated { get; set; }

		public override void OnClickEvent()
		{
			base.OnClickEvent();
			ClickedSignal.Dispatch(Definition);
		}
	}
}
