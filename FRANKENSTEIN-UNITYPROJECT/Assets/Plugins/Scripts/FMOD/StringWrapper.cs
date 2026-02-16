namespace FMOD
{
	public struct StringWrapper
	{
		private global::System.IntPtr nativeUtf8Ptr;

		public static implicit operator string(global::FMOD.StringWrapper fstring)
		{
			if (fstring.nativeUtf8Ptr == global::System.IntPtr.Zero)
			{
				return string.Empty;
			}
			int i;
			for (i = 0; global::System.Runtime.InteropServices.Marshal.ReadByte(fstring.nativeUtf8Ptr, i) != 0; i++)
			{
			}
			if (i > 0)
			{
				byte[] array = new byte[i];
				global::System.Runtime.InteropServices.Marshal.Copy(fstring.nativeUtf8Ptr, array, 0, i);
				return global::System.Text.Encoding.UTF8.GetString(array, 0, i);
			}
			return string.Empty;
		}
	}
}
