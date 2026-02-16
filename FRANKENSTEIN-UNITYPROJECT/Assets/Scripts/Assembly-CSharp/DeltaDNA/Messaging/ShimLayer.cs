namespace DeltaDNA.Messaging
{
	internal class ShimLayer : global::DeltaDNA.Messaging.Layer
	{
		private const byte _dimmedMaskAlpha = 128;

		private global::UnityEngine.Texture2D _texture;

		public void Init(global::DeltaDNA.Messaging.IPopup popup, global::System.Collections.Generic.Dictionary<string, object> config, int depth)
		{
			_popup = popup;
			_depth = depth;
			object value;
			if (config.TryGetValue("mask", out value))
			{
				bool flag = true;
				global::UnityEngine.Color32[] array = new global::UnityEngine.Color32[1];
				switch ((string)value)
				{
				default:
				{
					int num;
							num = 1; // Added this line, to remove use of unassigned variable 
					if (num == 1)
					{
						array[0] = new global::UnityEngine.Color32(0, 0, 0, 0);
					}
					else
					{
						flag = false;
					}
					break;
				}
				case "dimmed":
					array[0] = new global::UnityEngine.Color32(0, 0, 0, 128);
					break;
				}
				if (flag)
				{
					_texture = new global::UnityEngine.Texture2D(1, 1);
					_texture.SetPixels32(array);
					_texture.Apply();
				}
			}
			object value2;
			if (config.TryGetValue("action", out value2))
			{
				RegisterAction((global::System.Collections.Generic.Dictionary<string, object>)value2, "shim");
			}
			else
			{
				RegisterAction();
			}
		}

		public void OnGUI()
		{
			global::UnityEngine.GUI.depth = _depth;
			if ((bool)_texture)
			{
				global::UnityEngine.Rect position = new global::UnityEngine.Rect(0f, 0f, global::UnityEngine.Screen.width, global::UnityEngine.Screen.height);
				global::UnityEngine.GUI.DrawTexture(position, _texture);
				if (global::UnityEngine.GUI.Button(position, string.Empty, global::UnityEngine.GUIStyle.none) && _actions.Count > 0)
				{
					_actions[0]();
				}
			}
		}
	}
}
