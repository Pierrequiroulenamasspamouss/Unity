namespace DeltaDNA.Messaging
{
	public class PopupException : global::System.Exception
	{
		public PopupException()
		{
		}

		public PopupException(string message)
			: base(message)
		{
		}

		public PopupException(string message, global::System.Exception inner)
			: base(message, inner)
		{
		}
	}
}
