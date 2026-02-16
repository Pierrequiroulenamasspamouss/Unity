namespace ICSharpCode.SharpZipLib.Tar
{
	public class TarException : global::ICSharpCode.SharpZipLib.SharpZipBaseException
	{
		public TarException()
		{
		}

		public TarException(string message)
			: base(message)
		{
		}

		public TarException(string message, global::System.Exception exception)
			: base(message, exception)
		{
		}
	}
}
