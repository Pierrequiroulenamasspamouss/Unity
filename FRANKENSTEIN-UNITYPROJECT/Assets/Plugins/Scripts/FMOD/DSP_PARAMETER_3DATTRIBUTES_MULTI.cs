namespace FMOD
{
	public struct DSP_PARAMETER_3DATTRIBUTES_MULTI
	{
		public int numlisteners;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 5)]
		public global::FMOD.ATTRIBUTES_3D[] relative;

		public global::FMOD.ATTRIBUTES_3D absolute;
	}
}
