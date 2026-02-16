namespace DeltaDNA
{
	public class EventStore : global::System.IDisposable
	{
		private static readonly string PF_KEY_IN_FILE = "DDSDK_EVENT_IN_FILE";

		private static readonly string PF_KEY_OUT_FILE = "DDSDK_EVENT_OUT_FILE";

		private static readonly string FILE_A = "A";

		private static readonly string FILE_B = "B";

		private static readonly long MAX_FILE_SIZE = 41943040L;

		private bool _initialised;

		private bool _disposed;

		private global::System.IO.Stream _infs;

		private global::System.IO.Stream _outfs;

		private static object _lock = new object();

		public EventStore(string dir)
		{
			global::DeltaDNA.Logger.LogInfo("Creating Event Store");
			if (InitialiseFileStreams(dir))
			{
				_initialised = true;
			}
			else
			{
				global::DeltaDNA.Logger.LogError("Failed to initialise event store in " + dir);
			}
		}

		public bool Push(string obj)
		{
			lock (_lock)
			{
				if (!_initialised)
				{
					global::DeltaDNA.Logger.LogError("Event Store not initialised");
					return false;
				}
				if (_infs.Length < MAX_FILE_SIZE)
				{
					PushEvent(obj, _infs);
					return true;
				}
				global::DeltaDNA.Logger.LogWarning("Event Store full");
				return false;
			}
		}

		public bool Swap()
		{
			lock (_lock)
			{
				if (_initialised && _outfs.Length == 0L)
				{
					SwapStreams(ref _infs, ref _outfs);
					string value = global::UnityEngine.PlayerPrefs.GetString(PF_KEY_IN_FILE);
					string value2 = global::UnityEngine.PlayerPrefs.GetString(PF_KEY_OUT_FILE);
					global::UnityEngine.PlayerPrefs.SetString(PF_KEY_IN_FILE, value2);
					global::UnityEngine.PlayerPrefs.SetString(PF_KEY_OUT_FILE, value);
					return true;
				}
				return false;
			}
		}

		public global::System.Collections.Generic.List<string> Read()
		{
			lock (_lock)
			{
				global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>();
				try
				{
					if (_initialised)
					{
						ReadEvents(_outfs, list);
					}
				}
				catch (global::System.Exception ex)
				{
					global::DeltaDNA.Logger.LogError("Problem reading events: " + ex.Message);
				}
				return list;
			}
		}

		public void ClearOut()
		{
			lock (_lock)
			{
				if (_initialised)
				{
					ClearStream(_outfs);
				}
			}
		}

		public void ClearAll()
		{
			lock (_lock)
			{
				if (_initialised)
				{
					ClearStream(_infs);
					ClearStream(_outfs);
				}
			}
		}

		public void FlushBuffers()
		{
			lock (_lock)
			{
				if (_initialised)
				{
					_infs.Flush();
					_outfs.Flush();
				}
			}
		}

		~EventStore()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			global::System.GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			global::DeltaDNA.Logger.LogDebug("Disposing EventStore " + this);
			try
			{
				if (_disposed || !disposing)
				{
					return;
				}
				global::DeltaDNA.Logger.LogDebug("Disposing filestreams");
				lock (_lock)
				{
					if (_infs != null)
					{
						_infs.Dispose();
					}
					if (_outfs != null)
					{
						_outfs.Dispose();
					}
				}
			}
			finally
			{
				_disposed = true;
			}
		}

		private bool InitialiseFileStreams(string dir)
		{
			if (!global::System.IO.Directory.Exists(dir))
			{
				global::DeltaDNA.Logger.LogDebug("Directory not found, creating");
				global::DeltaDNA.Utils.CreateDirectory(dir);
			}
			try
			{
				string text = global::UnityEngine.PlayerPrefs.GetString(PF_KEY_IN_FILE, FILE_A);
				string text2 = global::UnityEngine.PlayerPrefs.GetString(PF_KEY_OUT_FILE, FILE_B);
				string path = global::System.IO.Path.Combine(dir, text);
				string path2 = global::System.IO.Path.Combine(dir, text2);
				_infs = global::DeltaDNA.Utils.CreateStream(path);
				_infs.Seek(0L, global::System.IO.SeekOrigin.End);
				_outfs = global::DeltaDNA.Utils.CreateStream(path2);
				_outfs.Seek(0L, global::System.IO.SeekOrigin.Begin);
				global::UnityEngine.PlayerPrefs.SetString(PF_KEY_IN_FILE, text);
				global::UnityEngine.PlayerPrefs.SetString(PF_KEY_OUT_FILE, text2);
				return true;
			}
			catch (global::System.Exception ex)
			{
				global::DeltaDNA.Logger.LogError("Failed to initialise file stream: " + ex.Message);
			}
			return false;
		}

		public static void PushEvent(string obj, global::System.IO.Stream stream)
		{
			byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(obj);
			byte[] bytes2 = global::System.BitConverter.GetBytes(bytes.Length);
			global::System.Collections.Generic.List<byte> list = new global::System.Collections.Generic.List<byte>();
			list.AddRange(bytes2);
			list.AddRange(bytes);
			byte[] array = list.ToArray();
			stream.Write(array, 0, array.Length);
		}

		public static void ReadEvents(global::System.IO.Stream stream, global::System.Collections.Generic.IList<string> events)
		{
			byte[] array = new byte[4];
			while (stream.Read(array, 0, array.Length) > 0)
			{
				int num = global::System.BitConverter.ToInt32(array, 0);
				byte[] array2 = new byte[num];
				stream.Read(array2, 0, array2.Length);
				string item = global::System.Text.Encoding.UTF8.GetString(array2, 0, array2.Length);
				events.Add(item);
			}
			stream.Seek(0L, global::System.IO.SeekOrigin.Begin);
		}

		public static void SwapStreams(ref global::System.IO.Stream sin, ref global::System.IO.Stream sout)
		{
			sin.Flush();
			global::System.IO.Stream stream = sin;
			sin = sout;
			sout = stream;
			sin.Seek(0L, global::System.IO.SeekOrigin.Begin);
			sin.SetLength(0L);
			sout.Seek(0L, global::System.IO.SeekOrigin.Begin);
		}

		public static void ClearStream(global::System.IO.Stream stream)
		{
			stream.Seek(0L, global::System.IO.SeekOrigin.Begin);
			stream.SetLength(0L);
		}
	}
}
