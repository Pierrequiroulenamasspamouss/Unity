namespace Kampai.Common
{
	public class NetworkConnectionLostCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Common.NetworkModel networkModel { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowOfflinePopupSignal showOfflinePopupSignal { get; set; }

		public override void Execute()
		{
			networkModel.isConnectionLost = true;
			showOfflinePopupSignal.Dispatch(true);
		}
	}
}
