namespace Kampai.Download
{
	public interface IBackgroundDownloadDlcService
	{
		bool Stopped { get; }

		void Stop();

		void Init();

		void Run();
	}
}
