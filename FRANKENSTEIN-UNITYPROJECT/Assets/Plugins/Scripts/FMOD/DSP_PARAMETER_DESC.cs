namespace FMOD
{
	public struct DSP_PARAMETER_DESC
	{
		public global::FMOD.DSP_PARAMETER_TYPE type;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16)]
		public char[] name;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16)]
		public char[] label;

		public string description;

		private global::FMOD.DSP_PARAMETER_DESC_UNION desc;
	}
}
