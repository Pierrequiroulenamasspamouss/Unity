namespace FMOD
{
	public struct ERRORCALLBACK_INFO
	{
		public global::FMOD.RESULT result;

		public global::FMOD.ERRORCALLBACK_INSTANCETYPE instancetype;

		public global::System.IntPtr instance;

		private global::System.IntPtr functionname_internal;

		private global::System.IntPtr functionparams_internal;

		public string functionname
		{
			get
			{
				return global::System.Runtime.InteropServices.Marshal.PtrToStringAnsi(functionname_internal);
			}
		}

		public string functionparams
		{
			get
			{
				return global::System.Runtime.InteropServices.Marshal.PtrToStringAnsi(functionparams_internal);
			}
		}
	}
}
