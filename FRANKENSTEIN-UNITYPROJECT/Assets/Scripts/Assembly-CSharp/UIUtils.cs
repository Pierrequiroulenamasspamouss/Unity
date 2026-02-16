public static class UIUtils
{
	private const int BASE_SCREEN_HEIGHT = 640;

	private const int BASE_SCREEN_WIDTH = 960;

	private static float widthScale;

	private static float heightScale;

	public static float GetScreenScale()
	{
		float num = GetWidthScale();
		float num2 = GetHeightScale();
		return (!(num > num2)) ? num : num2;
	}

	public static void ScaleFonts(global::UnityEngine.GameObject gameobject)
	{
		float screenScale = GetScreenScale();
		global::UnityEngine.UI.Text[] componentsInChildren = gameobject.GetComponentsInChildren<global::UnityEngine.UI.Text>();
		global::UnityEngine.UI.Text[] array = componentsInChildren;
		foreach (global::UnityEngine.UI.Text text in array)
		{
			text.fontSize = (int)((float)text.fontSize * screenScale);
		}
	}

	public static void MultiplyFontSize(global::UnityEngine.GameObject gameObject, float multipler)
	{
		global::UnityEngine.UI.Text[] componentsInChildren = gameObject.GetComponentsInChildren<global::UnityEngine.UI.Text>();
		global::UnityEngine.UI.Text[] array = componentsInChildren;
		foreach (global::UnityEngine.UI.Text text in array)
		{
			text.fontSize = (int)((float)text.fontSize * multipler);
		}
	}

	public static float GetWidthScale()
	{
		if (widthScale.CompareTo(0f) == 0)
		{
			widthScale = (float)global::UnityEngine.Screen.width / 960f;
		}
		return widthScale;
	}

	public static float GetHeightScale()
	{
		if (heightScale.CompareTo(0f) == 0)
		{
			heightScale = (float)global::UnityEngine.Screen.height / 640f;
		}
		return heightScale;
	}

	public static int GetReferencedScreenHeight()
	{
		return 640;
	}

	public static int GetReferencedScreenWidth()
	{
		return 960;
	}

	public static string FormatTime(int time)
	{
		int num = time / 3600;
		int num2 = time / 60 % 60;
		int num3 = time % 60;
		return num.ToString("00") + ":" + num2.ToString("00") + ":" + num3.ToString("00");
	}

	public static string FormatTimeInStringFormat(int time)
	{
		int num = time / 3600;
		int num2 = time / 60 % 60;
		int num3 = time % 60;
		string text = string.Empty;
		if (num != 0)
		{
			text = text + num + "h";
		}
		if (num2 != 0)
		{
			if (text.Length != 0)
			{
				text += " ";
			}
			text = text + num2 + "m";
		}
		if (num3 != 0)
		{
			if (text.Length != 0)
			{
				text += " ";
			}
			text = text + num3 + "s";
		}
		return text;
	}

	public static global::UnityEngine.Sprite LoadSpriteFromPath(string path, string defaultImage = "btn_Circle01_mask")
	{
		global::UnityEngine.Sprite sprite = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.Sprite>(path);
		if (sprite == null)
		{
			sprite = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.Sprite>(defaultImage);
		}
		return sprite;
	}
}
