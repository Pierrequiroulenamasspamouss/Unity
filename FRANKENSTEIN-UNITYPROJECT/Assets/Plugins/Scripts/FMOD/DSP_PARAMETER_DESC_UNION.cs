namespace FMOD
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
	public struct DSP_PARAMETER_DESC_UNION
	{
		[global::System.Runtime.InteropServices.FieldOffset(0)]
		private global::FMOD.DSP_PARAMETER_DESC_FLOAT floatdesc;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		private global::FMOD.DSP_PARAMETER_DESC_INT intdesc;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		private global::FMOD.DSP_PARAMETER_DESC_BOOL booldesc;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		private global::FMOD.DSP_PARAMETER_DESC_DATA datadesc;
	}
}
