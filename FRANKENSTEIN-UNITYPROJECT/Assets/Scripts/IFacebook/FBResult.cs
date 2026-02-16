public class FBResult : global::System.IDisposable
{
	private bool isWWWWrapper;

	private object data;

	private string error;

	public global::UnityEngine.Texture2D Texture
	{
		get
		{
			return (!isWWWWrapper) ? (data as global::UnityEngine.Texture2D) : ((global::UnityEngine.WWW)data).texture;
		}
	}

	public string Text
	{
		get
		{
			return (!isWWWWrapper) ? (data as string) : ((global::UnityEngine.WWW)data).text;
		}
	}

	public string Error
	{
		get
		{
			return (!isWWWWrapper) ? error : ((global::UnityEngine.WWW)data).error;
		}
	}

	public FBResult(global::UnityEngine.WWW www)
	{
		isWWWWrapper = true;
		data = www;
	}

	public FBResult(string data, string error = null)
	{
		this.data = data;
		this.error = error;
	}

	public void Dispose()
	{
		if (isWWWWrapper && data != null)
		{
			((global::UnityEngine.WWW)data).Dispose();
		}
	}

	~FBResult()
	{
		Dispose();
	}
}
