namespace Kampai.UI.View
{
	public class CurrencyWarningDialogView : global::strange.extensions.mediation.impl.View
	{
		public global::UnityEngine.UI.Text CurrencyNeededLabel;

		public global::UnityEngine.UI.Text CurrencyNeededButtonLabel;

		public global::Kampai.UI.View.ButtonView CancelButton;

		public global::Kampai.UI.View.DoubleConfirmButtonView PurchaseButton;

		internal void SetCurrencyNeeded(int cost, int amountNeeded)
		{
			CurrencyNeededLabel.text = amountNeeded.ToString();
			CurrencyNeededButtonLabel.text = cost.ToString();
		}
	}
}
