namespace ICSharpCode.SharpZipLib.Tar
{
	public class InvalidHeaderException : global::ICSharpCode.SharpZipLib.Tar.TarException
	{
		public InvalidHeaderException()
		{
		}

		public InvalidHeaderException(string message)
			: base(message)
		{
		}

		public InvalidHeaderException(string message, global::System.Exception exception)
			: base(message, exception)
		{
		}
	}
}
