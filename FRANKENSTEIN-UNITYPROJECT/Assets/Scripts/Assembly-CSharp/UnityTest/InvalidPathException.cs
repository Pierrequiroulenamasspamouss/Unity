namespace UnityTest
{
	public class InvalidPathException : global::System.Exception
	{
		public InvalidPathException(string path)
			: base("Invalid path part " + path)
		{
		}
	}
}
