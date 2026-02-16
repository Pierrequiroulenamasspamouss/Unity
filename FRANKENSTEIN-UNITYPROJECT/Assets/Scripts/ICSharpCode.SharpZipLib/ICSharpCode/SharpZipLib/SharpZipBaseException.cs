namespace ICSharpCode.SharpZipLib
{
	public class SharpZipBaseException : global::System.ApplicationException
	{
		public SharpZipBaseException()
		{
		}

		public SharpZipBaseException(string message)
			: base(message)
		{
		}

		public SharpZipBaseException(string message, global::System.Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
