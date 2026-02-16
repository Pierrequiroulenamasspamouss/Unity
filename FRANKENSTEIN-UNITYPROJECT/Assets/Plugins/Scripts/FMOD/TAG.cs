namespace FMOD
{
	public struct TAG
	{
		public global::FMOD.TAGTYPE type;

		public global::FMOD.TAGDATATYPE datatype;

		private global::System.IntPtr name_internal;

		public global::System.IntPtr data;

		public uint datalen;

		public bool updated;

		public string name
		{
			get
			{
				return global::System.Runtime.InteropServices.Marshal.PtrToStringAnsi(name_internal);
			}
		}
	}
}
