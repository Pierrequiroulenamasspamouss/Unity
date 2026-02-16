namespace FMOD.Studio
{
	public class SOUND_INFO
	{
		public byte[] name_or_data;

		public global::FMOD.MODE mode;

		public global::FMOD.CREATESOUNDEXINFO exinfo;

		public int subsoundIndex;

		public string name
		{
			get
			{
				if ((mode & (global::FMOD.MODE.OPENMEMORY | global::FMOD.MODE.OPENMEMORY_POINT)) == 0 && name_or_data != null)
				{
					int num = global::System.Array.IndexOf(name_or_data, (byte)0);
					if (num > 0)
					{
						return global::System.Text.Encoding.UTF8.GetString(name_or_data, 0, num);
					}
					return null;
				}
				return null;
			}
		}

		~SOUND_INFO()
		{
			if (exinfo.inclusionlist != global::System.IntPtr.Zero)
			{
				global::System.Runtime.InteropServices.Marshal.FreeHGlobal(exinfo.inclusionlist);
			}
		}
	}
}
