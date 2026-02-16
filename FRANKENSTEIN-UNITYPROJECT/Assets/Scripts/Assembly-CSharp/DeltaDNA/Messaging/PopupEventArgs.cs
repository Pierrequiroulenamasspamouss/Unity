namespace DeltaDNA.Messaging
{
	public class PopupEventArgs : global::System.EventArgs
	{
		public string ID { get; set; }

		public string ActionType { get; set; }

		public string ActionValue { get; set; }

		public PopupEventArgs(string id, string type, string value = null)
		{
			ID = id;
			ActionType = type;
			ActionValue = value;
		}
	}
}
