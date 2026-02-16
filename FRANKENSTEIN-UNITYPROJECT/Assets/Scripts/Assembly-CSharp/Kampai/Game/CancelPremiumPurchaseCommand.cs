namespace Kampai.Game
{
	public class CancelPremiumPurchaseCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public string ExternalIdentifier { get; set; }

		[Inject]
		public uint errorCode { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.UI.View.PopupMessageSignal popupMessageSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowBillingNotAvailablePanelSignal showBillingNotAvailablePanelSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SavePlayerSignal savePlayerSignal { get; set; }

		public override void Execute()
		{
			playerService.CancelPendingTransaction(ExternalIdentifier);
			if (errorCode == 20000)
			{
				showBillingNotAvailablePanelSignal.Dispatch();
			}
			else if (errorCode != 20019)
			{
				string type = localService.GetString("CancelTransaction");
				popupMessageSignal.Dispatch(type);
			}
			savePlayerSignal.Dispatch(new global::Kampai.Util.Tuple<string, string, bool>("remote", string.Empty, false));
		}
	}
}
