namespace Kampai.Util.Logging.Hosted
{
	public interface ILogBuffer
	{
		int ByteCount { get; }

		event global::System.EventHandler BufferFull;

		void Add(global::Kampai.Util.Logging.Hosted.Log log);

		void Process(global::System.Func<global::Kampai.Util.Logging.Hosted.Log, global::Kampai.Util.Logging.Hosted.LogglyDto> dtobuilder);

		global::Kampai.Util.Logging.Hosted.ProcessedLogs GetProcessedLogsAndClear(global::System.Func<global::Kampai.Util.Logging.Hosted.Log, global::Kampai.Util.Logging.Hosted.LogglyDto> dtobuilder);

		void Clear();

		void SetMaxByteCount(int maxByteCount);
	}
}
