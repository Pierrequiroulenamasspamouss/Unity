public class FBScreen
{
	public class Layout
	{
		public class OptionLeft : FBScreen.Layout
		{
			public float Amount;
		}

		public class OptionTop : FBScreen.Layout
		{
			public float Amount;
		}

		public class OptionCenterHorizontal : FBScreen.Layout
		{
		}

		public class OptionCenterVertical : FBScreen.Layout
		{
		}
	}

	private static bool resizable;

	public static bool FullScreen
	{
		get
		{
			return global::UnityEngine.Screen.fullScreen;
		}
		set
		{
			global::UnityEngine.Screen.fullScreen = value;
		}
	}

	public static bool Resizable
	{
		get
		{
			return resizable;
		}
	}

	public static int Width
	{
		get
		{
			return global::UnityEngine.Screen.width;
		}
	}

	public static int Height
	{
		get
		{
			return global::UnityEngine.Screen.height;
		}
	}

	public static void SetResolution(int width, int height, bool fullscreen, int preferredRefreshRate = 0, params FBScreen.Layout[] layoutParams)
	{
		global::UnityEngine.Screen.SetResolution(width, height, fullscreen, preferredRefreshRate);
	}

	public static void SetAspectRatio(int width, int height, params FBScreen.Layout[] layoutParams)
	{
		int width2 = global::UnityEngine.Screen.height / height * width;
		global::UnityEngine.Screen.SetResolution(width2, global::UnityEngine.Screen.height, global::UnityEngine.Screen.fullScreen);
	}

	public static void SetUnityPlayerEmbedCSS(string key, string value)
	{
	}

	public static FBScreen.Layout.OptionLeft Left(float amount)
	{
		FBScreen.Layout.OptionLeft optionLeft = new FBScreen.Layout.OptionLeft();
		optionLeft.Amount = amount;
		return optionLeft;
	}

	public static FBScreen.Layout.OptionTop Top(float amount)
	{
		FBScreen.Layout.OptionTop optionTop = new FBScreen.Layout.OptionTop();
		optionTop.Amount = amount;
		return optionTop;
	}

	public static FBScreen.Layout.OptionCenterHorizontal CenterHorizontal()
	{
		return new FBScreen.Layout.OptionCenterHorizontal();
	}

	public static FBScreen.Layout.OptionCenterVertical CenterVertical()
	{
		return new FBScreen.Layout.OptionCenterVertical();
	}

	private static void SetLayout(global::System.Collections.Generic.IEnumerable<FBScreen.Layout> parameters)
	{
	}
}
