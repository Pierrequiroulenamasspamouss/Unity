namespace FMOD
{
	public struct ASYNCREADINFO
	{
		public global::System.IntPtr handle;

		public uint offset;

		public uint sizebytes;

		public int priority;

		public global::System.IntPtr userdata;

		public global::System.IntPtr buffer;

		public uint bytesread;

		public global::FMOD.ASYNCREADINFO_DONE_CALLBACK done;
	}
}
