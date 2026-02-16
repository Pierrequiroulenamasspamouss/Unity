namespace FMOD
{
	public struct DSP_DESCRIPTION
	{
		public uint pluginsdkversion;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32)]
		public char[] name;

		public uint version;

		public int numinputbuffers;

		public int numoutputbuffers;

		public global::FMOD.DSP_CREATECALLBACK create;

		public global::FMOD.DSP_RELEASECALLBACK release;

		public global::FMOD.DSP_RESETCALLBACK reset;

		public global::FMOD.DSP_READCALLBACK read;

		public global::FMOD.DSP_PROCESS_CALLBACK process;

		public global::FMOD.DSP_SETPOSITIONCALLBACK setposition;

		public int numparameters;

		public global::System.IntPtr paramdesc;

		public global::FMOD.DSP_SETPARAM_FLOAT_CALLBACK setparameterfloat;

		public global::FMOD.DSP_SETPARAM_INT_CALLBACK setparameterint;

		public global::FMOD.DSP_SETPARAM_BOOL_CALLBACK setparameterbool;

		public global::FMOD.DSP_SETPARAM_DATA_CALLBACK setparameterdata;

		public global::FMOD.DSP_GETPARAM_FLOAT_CALLBACK getparameterfloat;

		public global::FMOD.DSP_GETPARAM_INT_CALLBACK getparameterint;

		public global::FMOD.DSP_GETPARAM_BOOL_CALLBACK getparameterbool;

		public global::FMOD.DSP_GETPARAM_DATA_CALLBACK getparameterdata;

		public global::FMOD.DSP_SHOULDIPROCESS_CALLBACK shouldiprocess;

		public global::System.IntPtr userdata;
	}
}
