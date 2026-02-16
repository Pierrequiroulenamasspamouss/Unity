namespace DeltaDNA
{
	public class NotStartedException : global::System.Exception
	{
		public NotStartedException()
		{
		}

		public NotStartedException(string message)
			: base(message)
		{
		}

		public NotStartedException(string message, global::System.Exception inner)
			: base(message, inner)
		{
		}
	}
}
