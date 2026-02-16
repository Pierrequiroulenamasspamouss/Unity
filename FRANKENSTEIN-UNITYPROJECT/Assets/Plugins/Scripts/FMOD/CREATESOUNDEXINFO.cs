namespace FMOD
{
	public struct CREATESOUNDEXINFO
	{
		public int cbsize;

		public uint length;

		public uint fileoffset;

		public int numchannels;

		public int defaultfrequency;

		public global::FMOD.SOUND_FORMAT format;

		public uint decodebuffersize;

		public int initialsubsound;

		public int numsubsounds;

		public global::System.IntPtr inclusionlist;

		public int inclusionlistnum;

		public global::FMOD.SOUND_PCMREADCALLBACK pcmreadcallback;

		public global::FMOD.SOUND_PCMSETPOSCALLBACK pcmsetposcallback;

		public global::FMOD.SOUND_NONBLOCKCALLBACK nonblockcallback;

		public global::System.IntPtr dlsname;

		public global::System.IntPtr encryptionkey;

		public int maxpolyphony;

		public global::System.IntPtr userdata;

		public global::FMOD.SOUND_TYPE suggestedsoundtype;

		public global::FMOD.FILE_OPENCALLBACK fileuseropen;

		public global::FMOD.FILE_CLOSECALLBACK fileuserclose;

		public global::FMOD.FILE_READCALLBACK fileuserread;

		public global::FMOD.FILE_SEEKCALLBACK fileuserseek;

		public global::FMOD.FILE_ASYNCREADCALLBACK fileuserasyncread;

		public global::FMOD.FILE_ASYNCCANCELCALLBACK fileuserasynccancel;

		public global::System.IntPtr fileuserdata;

		public global::FMOD.CHANNELORDER channelorder;

		public global::FMOD.CHANNELMASK channelmask;

		public global::System.IntPtr initialsoundgroup;

		public uint initialseekposition;

		public global::FMOD.TIMEUNIT initialseekpostype;

		public int ignoresetfilesystem;

		public uint audioqueuepolicy;

		public uint minmidigranularity;

		public int nonblockthreadid;
	}
}
