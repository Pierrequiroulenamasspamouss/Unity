namespace FMOD
{
	internal class StringMarshalHelper
	{
		internal static void NativeToBuilder(global::System.Text.StringBuilder builder, global::System.IntPtr nativeMem)
		{
			byte[] array = new byte[builder.Capacity];
			global::System.Runtime.InteropServices.Marshal.Copy(nativeMem, array, 0, builder.Capacity);
			int num = global::System.Array.IndexOf(array, (byte)0);
			if (num > 0)
			{
				string value = global::System.Text.Encoding.UTF8.GetString(array, 0, num);
				builder.Append(value);
			}
		}
	}
}
