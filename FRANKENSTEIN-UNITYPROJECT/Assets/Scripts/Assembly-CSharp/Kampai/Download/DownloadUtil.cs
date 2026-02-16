namespace Kampai.Download
{
	public static class DownloadUtil
	{
		public static string CreateBundleURL(string baseUrl, string name)
		{
			return string.Format("{0}/{1}.unity3d", baseUrl, name);
		}

		public static string CreateBundlePath(string baseDLCPath, string name)
		{
			return string.Format("{0}/{1}{2}.unity3d", baseDLCPath, global::System.IO.Path.DirectorySeparatorChar, name);
		}

		public static string GetBundleNameFromUrl(string url)
		{
			if (!url.EndsWith(".unity3d"))
			{
				return string.Empty;
			}
			int num = url.LastIndexOf('/') + 1;
			return url.Substring(num, url.Length - num - ".unity3d".Length);
		}
	}
}
