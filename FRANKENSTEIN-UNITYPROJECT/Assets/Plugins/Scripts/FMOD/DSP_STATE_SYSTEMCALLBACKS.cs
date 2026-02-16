namespace FMOD
{
	public struct DSP_STATE_SYSTEMCALLBACKS
	{
		private global::FMOD.MEMORY_ALLOC_CALLBACK alloc;

		private global::FMOD.MEMORY_REALLOC_CALLBACK realloc;

		private global::FMOD.MEMORY_FREE_CALLBACK free;

		private global::FMOD.DSP_SYSTEM_GETSAMPLERATE getsamplerate;

		private global::FMOD.DSP_SYSTEM_GETBLOCKSIZE getblocksize;

		private global::System.IntPtr dft;

		private global::System.IntPtr pancallbacks;
	}
}
