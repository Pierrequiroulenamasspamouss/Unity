namespace FMOD.Studio
{
	internal class MarshallingHelper
	{
		public static int stringLengthUtf8(global::System.IntPtr nativeUtf8)
		{
			int i;
			for (i = 0; global::System.Runtime.InteropServices.Marshal.ReadByte(nativeUtf8, i) != 0; i++)
			{
			}
			return i;
		}

		public static string stringFromNativeUtf8(global::System.IntPtr nativeUtf8)
		{
			int num = stringLengthUtf8(nativeUtf8);
			if (num == 0)
			{
				return string.Empty;
			}
			byte[] array = new byte[num];
			global::System.Runtime.InteropServices.Marshal.Copy(nativeUtf8, array, 0, array.Length);
			return global::System.Text.Encoding.UTF8.GetString(array, 0, num);
		}
	}
}
