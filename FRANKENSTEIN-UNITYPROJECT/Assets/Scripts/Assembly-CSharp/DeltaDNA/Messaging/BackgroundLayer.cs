namespace DeltaDNA.Messaging
{
	internal class BackgroundLayer : global::DeltaDNA.Messaging.Layer
	{
		private global::UnityEngine.Texture _texture;

		private global::UnityEngine.Rect _position;

		private float _scale;

		public global::UnityEngine.Rect Position
		{
			get
			{
				return _position;
			}
		}

		public float Scale
		{
			get
			{
				return _scale;
			}
		}

		public void Init(global::DeltaDNA.Messaging.IPopup popup, global::System.Collections.Generic.Dictionary<string, object> layout, global::UnityEngine.Texture texture, int depth)
		{
			_popup = popup;
			_texture = texture;
			_depth = depth;
			object value;
			if (layout.TryGetValue("background", out value))
			{
				global::System.Collections.Generic.Dictionary<string, object> dictionary = value as global::System.Collections.Generic.Dictionary<string, object>;
				object value2;
				if (dictionary.TryGetValue("action", out value2))
				{
					RegisterAction((global::System.Collections.Generic.Dictionary<string, object>)value2, "background");
				}
				else
				{
					RegisterAction();
				}
				object value3;
				if (dictionary.TryGetValue("cover", out value3))
				{
					_position = RenderAsCover((global::System.Collections.Generic.Dictionary<string, object>)value3);
				}
				else if (dictionary.TryGetValue("contain", out value3))
				{
					_position = RenderAsContain((global::System.Collections.Generic.Dictionary<string, object>)value3);
				}
				else
				{
					global::DeltaDNA.Logger.LogError("Invalid layout");
				}
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
				global::UnityEngine.GUI.DrawTexture(_position, _texture);
				if (global::UnityEngine.GUI.Button(_position, string.Empty, global::UnityEngine.GUIStyle.none) && _actions.Count > 0)
				{
					_actions[0]();
				}
			}
		}

		private global::UnityEngine.Rect RenderAsCover(global::System.Collections.Generic.Dictionary<string, object> rules)
		{
			_scale = global::System.Math.Max((float)global::UnityEngine.Screen.width / (float)_texture.width, (float)global::UnityEngine.Screen.height / (float)_texture.height);
			float num = (float)_texture.width * _scale;
			float num2 = (float)_texture.height * _scale;
			float top = (float)global::UnityEngine.Screen.height / 2f - num2 / 2f;
			float left = (float)global::UnityEngine.Screen.width / 2f - num / 2f;
			object value;
			if (rules.TryGetValue("valign", out value))
			{
				switch ((string)value)
				{
				case "top":
					top = 0f;
					break;
				case "bottom":
					top = (float)global::UnityEngine.Screen.height - num2;
					break;
				}
			}
			object value2;
			if (rules.TryGetValue("halign", out value2))
			{
				switch ((string)value2)
				{
				case "left":
					left = 0f;
					break;
				case "right":
					left = (float)global::UnityEngine.Screen.width - num;
					break;
				}
			}
			return new global::UnityEngine.Rect(left, top, num, num2);
		}

		private global::UnityEngine.Rect RenderAsContain(global::System.Collections.Generic.Dictionary<string, object> rules)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			object value;
			if (rules.TryGetValue("left", out value))
			{
				num = GetConstraintPixels((string)value, global::UnityEngine.Screen.width);
			}
			object value2;
			if (rules.TryGetValue("right", out value2))
			{
				num2 = GetConstraintPixels((string)value2, global::UnityEngine.Screen.width);
			}
			float val = ((float)global::UnityEngine.Screen.width - num - num2) / (float)_texture.width;
			object value3;
			if (rules.TryGetValue("top", out value3))
			{
				num3 = GetConstraintPixels((string)value3, global::UnityEngine.Screen.height);
			}
			object value4;
			if (rules.TryGetValue("bottom", out value4))
			{
				num4 = GetConstraintPixels((string)value4, global::UnityEngine.Screen.height);
			}
			float val2 = ((float)global::UnityEngine.Screen.height - num3 - num4) / (float)_texture.height;
			_scale = global::System.Math.Min(val, val2);
			float num5 = (float)_texture.width * _scale;
			float num6 = (float)_texture.height * _scale;
			float top = ((float)global::UnityEngine.Screen.height - num3 - num4) / 2f - num6 / 2f + num3;
			float left = ((float)global::UnityEngine.Screen.width - num - num2) / 2f - num5 / 2f + num;
			object value5;
			if (rules.TryGetValue("valign", out value5))
			{
				switch ((string)value5)
				{
				case "top":
					top = num3;
					break;
				case "bottom":
					top = (float)global::UnityEngine.Screen.height - num6 - num4;
					break;
				}
			}
			object value6;
			if (rules.TryGetValue("halign", out value6))
			{
				switch ((string)value6)
				{
				case "left":
					left = num;
					break;
				case "right":
					left = (float)global::UnityEngine.Screen.width - num5 - num2;
					break;
				}
			}
			return new global::UnityEngine.Rect(left, top, num5, num6);
		}

		private float GetConstraintPixels(string constraint, float edge)
		{
			float result = 0f;
			global::System.Text.RegularExpressions.Regex regex = new global::System.Text.RegularExpressions.Regex("(\\d+)(px|%)", global::System.Text.RegularExpressions.RegexOptions.IgnoreCase);
			global::System.Text.RegularExpressions.Match match = regex.Match(constraint);
			if (match != null && match.Success)
			{
				global::System.Text.RegularExpressions.GroupCollection groups = match.Groups;
				if (float.TryParse(groups[1].Value, out result))
				{
					if (groups[2].Value == "%")
					{
						return edge * result / 100f;
					}
					return result;
				}
			}
			return result;
		}
	}
}
