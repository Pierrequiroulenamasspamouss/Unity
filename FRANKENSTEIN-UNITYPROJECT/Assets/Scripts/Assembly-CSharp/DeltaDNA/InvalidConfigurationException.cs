namespace DeltaDNA
{
	public class InvalidConfigurationException : global::System.Exception
	{
		public InvalidConfigurationException()
		{
		}

		public InvalidConfigurationException(string message)
			: base(message)
		{
		}

		public InvalidConfigurationException(string message, global::System.Exception inner)
			: base(message, inner)
		{
		}
	}
}
