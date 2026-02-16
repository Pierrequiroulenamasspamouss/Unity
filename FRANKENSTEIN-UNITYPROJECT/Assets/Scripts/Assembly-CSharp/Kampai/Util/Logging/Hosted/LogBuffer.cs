namespace Kampai.Util.Logging.Hosted
{
	public class LogBuffer : global::Kampai.Util.Logging.Hosted.ILogBuffer
	{
		private int maxByteCount;

		private volatile int _totalLogCount;

		private volatile int _totalByteCount;

		private volatile bool _isBufferFull;

		private global::System.Collections.Generic.IList<global::Kampai.Util.Logging.Hosted.Log> _tempLogStorage = new global::System.Collections.Generic.List<global::Kampai.Util.Logging.Hosted.Log>();

		private global::System.Text.StringBuilder _processedLogs = new global::System.Text.StringBuilder();

		private object logsLock = new object();

		private object processingLock = new object();

		int global::Kampai.Util.Logging.Hosted.ILogBuffer.ByteCount
		{
			get
			{
				return _totalByteCount;
			}
		}

		public event global::System.EventHandler BufferFull;

		void global::Kampai.Util.Logging.Hosted.ILogBuffer.Add(global::Kampai.Util.Logging.Hosted.Log log)
		{
			if (log != null && !_isBufferFull)
			{
				lock (logsLock)
				{
					_tempLogStorage.Add(log);
				}
			}
		}

		void global::Kampai.Util.Logging.Hosted.ILogBuffer.Process(global::System.Func<global::Kampai.Util.Logging.Hosted.Log, global::Kampai.Util.Logging.Hosted.LogglyDto> dtoBuilder)
		{
			global::System.Collections.Generic.IList<global::Kampai.Util.Logging.Hosted.Log> logsToProcess = new global::System.Collections.Generic.List<global::Kampai.Util.Logging.Hosted.Log>();
			lock (logsLock)
			{
				logsToProcess = GetLogsForProcessing();
			}
			if (!_isBufferFull)
			{
				lock (processingLock)
				{
					ProcessLogs(logsToProcess, dtoBuilder);
				}
			}
		}

		global::Kampai.Util.Logging.Hosted.ProcessedLogs global::Kampai.Util.Logging.Hosted.ILogBuffer.GetProcessedLogsAndClear(global::System.Func<global::Kampai.Util.Logging.Hosted.Log, global::Kampai.Util.Logging.Hosted.LogglyDto> dtoBuilder)
		{
			lock (logsLock)
			{
				lock (processingLock)
				{
					global::System.Collections.Generic.IList<global::Kampai.Util.Logging.Hosted.Log> logsForProcessing = GetLogsForProcessing();
					ProcessLogs(logsForProcessing, dtoBuilder);
					string json = _processedLogs.ToString();
					int totalLogCount = _totalLogCount;
					Clear();
					global::Kampai.Util.Logging.Hosted.ProcessedLogs processedLogs = new global::Kampai.Util.Logging.Hosted.ProcessedLogs();
					processedLogs.Json = json;
					processedLogs.Count = totalLogCount;
					return processedLogs;
				}
			}
		}

		void global::Kampai.Util.Logging.Hosted.ILogBuffer.Clear()
		{
			lock (logsLock)
			{
				lock (processingLock)
				{
					Clear();
				}
			}
		}

		void global::Kampai.Util.Logging.Hosted.ILogBuffer.SetMaxByteCount(int maxByteCount)
		{
			this.maxByteCount = maxByteCount;
		}

		private global::System.Collections.Generic.IList<global::Kampai.Util.Logging.Hosted.Log> GetLogsForProcessing()
		{
			global::System.Collections.Generic.IList<global::Kampai.Util.Logging.Hosted.Log> tempLogStorage = _tempLogStorage;
			_tempLogStorage = new global::System.Collections.Generic.List<global::Kampai.Util.Logging.Hosted.Log>();
			return tempLogStorage;
		}

		private void ProcessLogs(global::System.Collections.Generic.IList<global::Kampai.Util.Logging.Hosted.Log> logsToProcess, global::System.Func<global::Kampai.Util.Logging.Hosted.Log, global::Kampai.Util.Logging.Hosted.LogglyDto> dtoBuilder)
		{
			foreach (global::Kampai.Util.Logging.Hosted.Log item in logsToProcess)
			{
				global::Kampai.Util.Logging.Hosted.LogglyDto value = dtoBuilder(item);
				string text = global::Newtonsoft.Json.JsonConvert.SerializeObject(value);
				int byteCount = global::System.Text.Encoding.UTF8.GetByteCount(text);
				if (_totalByteCount + byteCount < maxByteCount)
				{
					_processedLogs.Append(text).Append(global::System.Environment.NewLine);
					_totalLogCount++;
					_totalByteCount += byteCount;
					continue;
				}
				OnBufferFull();
				break;
			}
		}

		private void Clear()
		{
			_tempLogStorage = new global::System.Collections.Generic.List<global::Kampai.Util.Logging.Hosted.Log>();
			_processedLogs = new global::System.Text.StringBuilder();
			_totalLogCount = 0;
			_totalByteCount = 0;
			_isBufferFull = false;
		}

		private void OnBufferFull()
		{
			if (!_isBufferFull)
			{
				global::System.EventHandler bufferFull = this.BufferFull;
				if (bufferFull != null)
				{
					bufferFull(this, global::System.EventArgs.Empty);
				}
				_isBufferFull = true;
			}
		}
	}
}
