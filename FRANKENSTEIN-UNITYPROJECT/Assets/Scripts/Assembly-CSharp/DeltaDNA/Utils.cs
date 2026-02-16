namespace DeltaDNA
{
	internal static class Utils
	{
		public static global::System.Collections.Generic.Dictionary<K, V> HashtableToDictionary<K, V>(global::System.Collections.Hashtable table)
		{
			global::System.Collections.Generic.Dictionary<K, V> dictionary = new global::System.Collections.Generic.Dictionary<K, V>();
			foreach (global::System.Collections.DictionaryEntry item in table)
			{
				dictionary.Add((K)item.Key, (V)item.Value);
			}
			return dictionary;
		}

		public static global::System.Collections.Generic.Dictionary<K, V> HashtableToDictionary<K, V>(global::System.Collections.Generic.Dictionary<K, V> dictionary)
		{
			return dictionary;
		}

		public static void CreateDirectory(string path)
		{
			global::System.IO.Directory.CreateDirectory(path);
		}

		public static global::System.IO.Stream CreateStream(string path)
		{
			global::DeltaDNA.Logger.LogDebug("Creating file based stream");
			return new global::System.IO.FileStream(path, global::System.IO.FileMode.OpenOrCreate, global::System.IO.FileAccess.ReadWrite);
		}

		public static global::System.IO.Stream OpenStream(string path)
		{
			global::DeltaDNA.Logger.LogDebug("Opening file based stream");
			return new global::System.IO.FileStream(path, global::System.IO.FileMode.Open, global::System.IO.FileAccess.Read);
		}
	}
}
