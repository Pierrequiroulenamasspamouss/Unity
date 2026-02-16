namespace DeltaDNA.Messaging
{
	internal class ButtonsLayer : global::DeltaDNA.Messaging.Layer
	{
		private global::System.Collections.Generic.List<global::UnityEngine.Texture> _textures = new global::System.Collections.Generic.List<global::UnityEngine.Texture>();

		private global::System.Collections.Generic.List<global::UnityEngine.Rect> _positions = new global::System.Collections.Generic.List<global::UnityEngine.Rect>();

		public void Init(global::DeltaDNA.Messaging.IPopup popup, global::System.Collections.Generic.Dictionary<string, object> orientation, global::System.Collections.Generic.List<global::UnityEngine.Texture> textures, global::DeltaDNA.Messaging.BackgroundLayer content, int depth)
		{
			_popup = popup;
			_depth = depth;
			object value;
			if (!orientation.TryGetValue("buttons", out value))
			{
				return;
			}
			global::System.Collections.Generic.List<object> list = value as global::System.Collections.Generic.List<object>;
			for (int i = 0; i < list.Count; i++)
			{
				global::System.Collections.Generic.Dictionary<string, object> dictionary = list[i] as global::System.Collections.Generic.Dictionary<string, object>;
				float left = 0f;
				float top = 0f;
				object value2;
				if (dictionary.TryGetValue("x", out value2))
				{
					left = (float)(int)(long)value2 * content.Scale + content.Position.xMin;
				}
				object value3;
				if (dictionary.TryGetValue("y", out value3))
				{
					top = (float)(int)(long)value3 * content.Scale + content.Position.yMin;
				}
				_positions.Add(new global::UnityEngine.Rect(left, top, (float)textures[i].width * content.Scale, (float)textures[i].height * content.Scale));
				object value4;
				if (dictionary.TryGetValue("action", out value4))
				{
					RegisterAction((global::System.Collections.Generic.Dictionary<string, object>)value4, "button" + (i + 1));
				}
				else
				{
					RegisterAction();
				}
			}
			_textures = textures;
		}

		public void OnGUI()
		{
			global::UnityEngine.GUI.depth = _depth;
			for (int i = 0; i < _textures.Count; i++)
			{
				global::UnityEngine.GUI.DrawTexture(_positions[i], _textures[i]);
				if (global::UnityEngine.GUI.Button(_positions[i], string.Empty, global::UnityEngine.GUIStyle.none))
				{
					_actions[i]();
				}
			}
		}
	}
}
