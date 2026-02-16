namespace Kampai.Wrappers
{
	public static class KampaiNativeLib
	{
		public struct DebugData
	{
		public string name;

		public int line_number;
	}

	private class NativeMethods
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		[global::System.Runtime.InteropServices.DllImport("lua52")]
		public static extern global::System.IntPtr kampai_create_debug();

		[global::System.Runtime.InteropServices.DllImport("lua52")]
		public static extern void kampai_free_debug(global::System.IntPtr debug);

		[global::System.Runtime.InteropServices.DllImport("lua52")]
		public static extern global::Kampai.Wrappers.KampaiNativeLib.DebugData kampai_get_debug(global::System.IntPtr L, string what, global::System.IntPtr ar);

		[global::System.Runtime.InteropServices.DllImport("lua52")]
		public static extern int kampai_push_cfunction_from_lib(global::System.IntPtr L, string name, string function_name);
#endif
	}

	public const string dllString = "lua52";

	public static global::System.IntPtr kampai_create_debug()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return global::Kampai.Wrappers.KampaiNativeLib.NativeMethods.kampai_create_debug();
#else
		return global::System.IntPtr.Zero;
#endif
	}

	public static void kampai_free_debug(global::System.IntPtr debug)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		global::Kampai.Wrappers.KampaiNativeLib.NativeMethods.kampai_free_debug(debug);
#endif
	}

	public static global::Kampai.Wrappers.KampaiNativeLib.DebugData kampai_get_debug(global::Kampai.Wrappers.LuaState L, string what, global::System.IntPtr ar)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return global::Kampai.Wrappers.KampaiNativeLib.NativeMethods.kampai_get_debug(L.DangerousGetHandle(), what, ar);
#else
		return new DebugData { name = "dummy", line_number = 0 };
#endif
	}

	public static int kampai_push_cfunction_from_lib(global::Kampai.Wrappers.LuaState L, string name, string function_name)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return global::Kampai.Wrappers.KampaiNativeLib.NativeMethods.kampai_push_cfunction_from_lib(L.DangerousGetHandle(), name, function_name);
#else
		return 0;
#endif
	}
	}
}
