[global::System.AttributeUsage(global::System.AttributeTargets.Class, AllowMultiple = false)]
public class IntegrationTestAttribute : global::System.Attribute
{
	private string path;

	public IntegrationTestAttribute(string path)
	{
		if (path.EndsWith(".unity"))
		{
			path = path.Substring(0, path.Length - ".unity".Length);
		}
		this.path = path;
	}

	public bool IncludeOnScene(string scenePath)
	{
		if (scenePath == path)
		{
			return true;
		}
		string fileNameWithoutExtension = global::System.IO.Path.GetFileNameWithoutExtension(scenePath);
		return fileNameWithoutExtension == path;
	}
}
