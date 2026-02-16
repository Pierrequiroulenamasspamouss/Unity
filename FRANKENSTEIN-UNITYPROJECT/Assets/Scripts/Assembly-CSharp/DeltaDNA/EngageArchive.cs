namespace DeltaDNA
{
	internal sealed class EngageArchive
	{
		private global::System.Collections.Hashtable _table = new global::System.Collections.Hashtable();

		private object _lock = new object();

		private static readonly string FILENAME = "ENGAGEMENTS";

		private string _path;

		public string this[string decisionPoint]
		{
			get
			{
				return _table[decisionPoint] as string;
			}
			set
			{
				lock (_lock)
				{
					_table[decisionPoint] = value;
				}
			}
		}

		public EngageArchive(string path)
		{
			Load(path);
			_path = path;
		}

		public bool Contains(string decisionPoint)
		{
			global::DeltaDNA.Logger.LogDebug("Does Engage contain " + decisionPoint);
			return _table.ContainsKey(decisionPoint);
		}

		private void Load(string path)
		{
			lock (_lock)
			{
				try
				{
					string text = global::System.IO.Path.Combine(path, FILENAME);
					global::DeltaDNA.Logger.LogDebug("Loading Engage from " + text);
					if (!global::System.IO.File.Exists(text))
					{
						return;
					}
					using (global::System.IO.Stream stream = global::DeltaDNA.Utils.OpenStream(text))
					{
						string key = null;
						string text2 = null;
						int num = 0;
						byte[] array = new byte[4];
						while (stream.Read(array, 0, array.Length) > 0)
						{
							int num2 = global::System.BitConverter.ToInt32(array, 0);
							byte[] array2 = new byte[num2];
							stream.Read(array2, 0, array2.Length);
							if (num % 2 == 0)
							{
								key = global::System.Text.Encoding.UTF8.GetString(array2, 0, array2.Length);
							}
							else
							{
								text2 = global::System.Text.Encoding.UTF8.GetString(array2, 0, array2.Length);
								_table.Add(key, text2);
							}
							num++;
						}
					}
				}
				catch (global::System.Exception ex)
				{
					global::DeltaDNA.Logger.LogWarning("Unable to load Engagement archive: " + ex.Message);
				}
			}
		}

		public void Save()
		{
			lock (_lock)
			{
				try
				{
					if (!global::System.IO.Directory.Exists(_path))
					{
						global::DeltaDNA.Utils.CreateDirectory(_path);
					}
					global::System.Collections.Generic.List<byte> list = new global::System.Collections.Generic.List<byte>();
					foreach (global::System.Collections.DictionaryEntry item in _table)
					{
						byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(item.Key as string);
						byte[] bytes2 = global::System.BitConverter.GetBytes(bytes.Length);
						byte[] bytes3 = global::System.Text.Encoding.UTF8.GetBytes(item.Value as string);
						byte[] bytes4 = global::System.BitConverter.GetBytes(bytes3.Length);
						list.AddRange(bytes2);
						list.AddRange(bytes);
						list.AddRange(bytes4);
						list.AddRange(bytes3);
					}
					byte[] array = list.ToArray();
					string path = global::System.IO.Path.Combine(_path, FILENAME);
					using (global::System.IO.Stream stream = global::DeltaDNA.Utils.CreateStream(path))
					{
						stream.SetLength(0L);
						stream.Seek(0L, global::System.IO.SeekOrigin.Begin);
						stream.Write(array, 0, array.Length);
					}
				}
				catch (global::System.Exception ex)
				{
					global::DeltaDNA.Logger.LogWarning("Unable to save Engagement archive: " + ex.Message);
				}
			}
		}

		public void Clear()
		{
			lock (_lock)
			{
				_table.Clear();
			}
		}
	}
}
