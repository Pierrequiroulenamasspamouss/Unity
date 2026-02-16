namespace FMOD
{
	public class Memory
	{
		public static global::FMOD.RESULT Initialize(global::System.IntPtr poolmem, int poollen, global::FMOD.MEMORY_ALLOC_CALLBACK useralloc, global::FMOD.MEMORY_REALLOC_CALLBACK userrealloc, global::FMOD.MEMORY_FREE_CALLBACK userfree, global::FMOD.MEMORY_TYPE memtypeflags)
		{
			return FMOD5_Memory_Initialize(poolmem, poollen, useralloc, userrealloc, userfree, memtypeflags);
		}

		public static global::FMOD.RESULT GetStats(out int currentalloced, out int maxalloced)
		{
			return GetStats(out currentalloced, out maxalloced, false);
		}

		public static global::FMOD.RESULT GetStats(out int currentalloced, out int maxalloced, bool blocking)
		{
			return FMOD5_Memory_GetStats(out currentalloced, out maxalloced, blocking);
		}

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Memory_Initialize(global::System.IntPtr poolmem, int poollen, global::FMOD.MEMORY_ALLOC_CALLBACK useralloc, global::FMOD.MEMORY_REALLOC_CALLBACK userrealloc, global::FMOD.MEMORY_FREE_CALLBACK userfree, global::FMOD.MEMORY_TYPE memtypeflags);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Memory_GetStats(out int currentalloced, out int maxalloced, bool blocking);
	}
}
