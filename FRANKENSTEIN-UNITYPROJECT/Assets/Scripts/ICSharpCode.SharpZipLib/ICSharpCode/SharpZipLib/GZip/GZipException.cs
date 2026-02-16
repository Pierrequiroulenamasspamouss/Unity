namespace ICSharpCode.SharpZipLib.GZip
{
	public class GZipException : global::ICSharpCode.SharpZipLib.SharpZipBaseException
	{
		public GZipException()
		{
		}

		public GZipException(string message)
			: base(message)
		{
		}

		public GZipException(string message, global::System.Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
