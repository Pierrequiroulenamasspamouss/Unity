namespace FMOD
{
	public class Debug
	{
		public static global::FMOD.RESULT Initialize(global::FMOD.DEBUG_FLAGS flags, global::FMOD.DEBUG_MODE mode, global::FMOD.DEBUG_CALLBACK callback, string filename)
		{
			return FMOD5_Debug_Initialize(flags, mode, callback, filename);
		}

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Debug_Initialize(global::FMOD.DEBUG_FLAGS flags, global::FMOD.DEBUG_MODE mode, global::FMOD.DEBUG_CALLBACK callback, string filename);
	}
}
