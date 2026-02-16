namespace Ea.Sharkbite.HttpPlugin.Http.Api
{
	public class DownloadProgress
	{
		public uint TotalBytes { get; set; }

		public long DownloadedBytes { get; set; }

		public long Delta { get; set; }

		public global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.DownloadProgress, global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> NotifySignal { get; set; }

		public float GetProgress()
		{
			uint totalBytes = TotalBytes;
			if (totalBytes == 0)
			{
				return 0f;
			}
			return (float)DownloadedBytes / (float)totalBytes;
		}
	}
}
