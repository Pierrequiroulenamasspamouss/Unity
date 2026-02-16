namespace ICSharpCode.SharpZipLib.LZW
{
	public class LzwException : global::ICSharpCode.SharpZipLib.SharpZipBaseException
	{
		public LzwException()
		{
		}

		public LzwException(string message)
			: base(message)
		{
		}

		public LzwException(string message, global::System.Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
