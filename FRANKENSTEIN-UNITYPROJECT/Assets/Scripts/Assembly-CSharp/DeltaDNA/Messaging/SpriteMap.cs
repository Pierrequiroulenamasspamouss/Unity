namespace DeltaDNA.Messaging
{
	internal class SpriteMap : global::UnityEngine.MonoBehaviour
	{
		private global::System.Collections.Generic.Dictionary<string, object> _spriteMapDict;

		private global::UnityEngine.Texture2D _spriteMap;

		public string URL { get; private set; }

		public int Width { get; private set; }

		public int Height { get; private set; }

		public void Init(global::System.Collections.Generic.Dictionary<string, object> message)
		{
			object value;
			object value2;
			object value3;
			if (message.TryGetValue("url", out value) && message.TryGetValue("width", out value2) && message.TryGetValue("height", out value3))
			{
				URL = (string)value;
				Width = (int)(long)value2;
				Height = (int)(long)value3;
			}
			else
			{
				global::DeltaDNA.Logger.LogError("Invalid image message format.");
			}
			object value4;
			if (message.TryGetValue("spritemap", out value4))
			{
				_spriteMapDict = (global::System.Collections.Generic.Dictionary<string, object>)value4;
			}
			else
			{
				global::DeltaDNA.Logger.LogError("Invalid message format, missing 'spritemap' object");
			}
		}

		public void LoadResource(global::System.Action callback)
		{
			_spriteMap = new global::UnityEngine.Texture2D(Width, Height);
			StartCoroutine(LoadResourceCoroutine(URL, callback));
		}

		public global::UnityEngine.Texture GetSpriteMap()
		{
			return _spriteMap;
		}

		public global::UnityEngine.Texture GetBackground()
		{
			object value;
			if (_spriteMapDict.TryGetValue("background", out value))
			{
				global::System.Collections.Generic.Dictionary<string, object> dictionary = value as global::System.Collections.Generic.Dictionary<string, object>;
				object value2;
				object value3;
				object value4;
				object value5;
				if (dictionary.TryGetValue("x", out value2) && dictionary.TryGetValue("y", out value3) && dictionary.TryGetValue("width", out value4) && dictionary.TryGetValue("height", out value5))
				{
					return GetSubRegion((int)(long)value2, (int)(long)value3, (int)(long)value4, (int)(long)value5);
				}
			}
			else
			{
				global::DeltaDNA.Logger.LogError("Background not found in spritemap object.");
			}
			return null;
		}

		public global::System.Collections.Generic.List<global::UnityEngine.Texture> GetButtons()
		{
			global::System.Collections.Generic.List<global::UnityEngine.Texture> list = new global::System.Collections.Generic.List<global::UnityEngine.Texture>();
			object value;
			if (_spriteMapDict.TryGetValue("buttons", out value))
			{
				global::System.Collections.Generic.List<object> list2 = value as global::System.Collections.Generic.List<object>;
				foreach (object item in list2)
				{
					global::System.Collections.Generic.Dictionary<string, object> dictionary = item as global::System.Collections.Generic.Dictionary<string, object>;
					object value2;
					object value3;
					object value4;
					object value5;
					if (dictionary.TryGetValue("x", out value2) && dictionary.TryGetValue("y", out value3) && dictionary.TryGetValue("width", out value4) && dictionary.TryGetValue("height", out value5))
					{
						list.Add(GetSubRegion((int)(long)value2, (int)(long)value3, (int)(long)value4, (int)(long)value5));
					}
				}
			}
			return list;
		}

		public global::UnityEngine.Texture2D GetSubRegion(int x, int y, int width, int height)
		{
			global::UnityEngine.Color[] pixels = _spriteMap.GetPixels(x, _spriteMap.height - y - height, width, height);
			global::UnityEngine.Texture2D texture2D = new global::UnityEngine.Texture2D(width, height, _spriteMap.format, false);
			texture2D.SetPixels(pixels);
			texture2D.Apply();
			return texture2D;
		}

		public global::UnityEngine.Texture2D GetSubRegion(global::UnityEngine.Rect rect)
		{
			return GetSubRegion(global::UnityEngine.Mathf.FloorToInt(rect.x), global::UnityEngine.Mathf.FloorToInt(rect.y), global::UnityEngine.Mathf.FloorToInt(rect.width), global::UnityEngine.Mathf.FloorToInt(rect.height));
		}

		private global::System.Collections.IEnumerator LoadResourceCoroutine(string url, global::System.Action callback)
		{
			global::UnityEngine.WWW www = new global::UnityEngine.WWW(url);
			yield return www;
			if (www.error == null)
			{
				www.LoadImageIntoTexture(_spriteMap);
				callback();
				yield break;
			}
			throw new global::DeltaDNA.Messaging.PopupException("Failed to load resource " + url + " " + www.error);
		}
	}
}
