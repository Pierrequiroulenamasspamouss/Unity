namespace FMOD
{
	public struct DSP_PARAMETER_FFT
	{
		public int length;

		public int numchannels;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32)]
		private global::System.IntPtr[] spectrum_internal;

		public float[][] spectrum
		{
			get
			{
				float[][] array = new float[numchannels][];
				for (int i = 0; i < numchannels; i++)
				{
					array[i] = new float[length];
					global::System.Runtime.InteropServices.Marshal.Copy(spectrum_internal[i], array[i], 0, length);
				}
				return array;
			}
		}
	}
}
