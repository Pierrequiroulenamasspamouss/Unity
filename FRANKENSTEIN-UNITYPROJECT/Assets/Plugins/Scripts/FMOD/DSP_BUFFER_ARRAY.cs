namespace FMOD
{
	public struct DSP_BUFFER_ARRAY
	{
		public int numbuffers;

		public int[] buffernumchannels;

		public uint[] bufferchannelmask;

		public global::System.IntPtr[] buffers;

		public global::FMOD.SPEAKERMODE speakermode;
	}
}
