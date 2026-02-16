namespace FMOD.Studio
{
	public struct BANK_INFO
	{
		public int size;

		public global::System.IntPtr userData;

		public int userDataLength;

		public global::FMOD.FILE_OPENCALLBACK openCallback;

		public global::FMOD.FILE_CLOSECALLBACK closeCallback;

		public global::FMOD.FILE_READCALLBACK readCallback;

		public global::FMOD.FILE_SEEKCALLBACK seekCallback;
	}
}
