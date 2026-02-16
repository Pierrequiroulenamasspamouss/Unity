namespace DeltaDNA.Messaging
{
	public interface IPopup
	{
		event global::System.EventHandler BeforePrepare;

		event global::System.EventHandler AfterPrepare;

		event global::System.EventHandler BeforeShow;

		event global::System.EventHandler BeforeClose;

		event global::System.EventHandler AfterClose;

		event global::System.EventHandler<global::DeltaDNA.Messaging.PopupEventArgs> Dismiss;

		event global::System.EventHandler<global::DeltaDNA.Messaging.PopupEventArgs> Action;

		void Prepare(global::System.Collections.Generic.Dictionary<string, object> configuration);

		void Show();

		void Close();

		void OnDismiss(global::DeltaDNA.Messaging.PopupEventArgs eventArgs);

		void OnAction(global::DeltaDNA.Messaging.PopupEventArgs eventArgs);
	}
}
