namespace ICSharpCode.SharpZipLib.Zip
{
	public class ZipException : global::ICSharpCode.SharpZipLib.SharpZipBaseException
	{
		public ZipException()
		{
		}

		public ZipException(string message)
			: base(message)
		{
		}

		public ZipException(string message, global::System.Exception exception)
			: base(message, exception)
		{
		}
	}
}
