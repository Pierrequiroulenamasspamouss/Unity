namespace FMOD
{
	public struct DSP_METERING_INFO
	{
		public int numsamples;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32)]
		public float[] peaklevel;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32)]
		public float[] rmslevel;

		public short numchannels;
	}
}
