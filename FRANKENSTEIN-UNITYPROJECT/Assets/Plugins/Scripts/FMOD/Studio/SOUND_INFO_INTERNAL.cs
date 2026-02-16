namespace FMOD.Studio
{
	public struct SOUND_INFO_INTERNAL
	{
		private global::System.IntPtr name_or_data;

		private global::FMOD.MODE mode;

		private global::FMOD.CREATESOUNDEXINFO exinfo;

		private int subsoundIndex;

		public void assign(out global::FMOD.Studio.SOUND_INFO publicInfo)
		{
			publicInfo = new global::FMOD.Studio.SOUND_INFO();
			publicInfo.mode = mode;
			publicInfo.exinfo = exinfo;
			publicInfo.exinfo.inclusionlist = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(global::System.Runtime.InteropServices.Marshal.SizeOf(typeof(int)));
			global::System.Runtime.InteropServices.Marshal.WriteInt32(publicInfo.exinfo.inclusionlist, subsoundIndex);
			publicInfo.exinfo.inclusionlistnum = 1;
			publicInfo.subsoundIndex = subsoundIndex;
			if (name_or_data != global::System.IntPtr.Zero)
			{
				int num;
				int num2;
				if ((mode & (global::FMOD.MODE.OPENMEMORY | global::FMOD.MODE.OPENMEMORY_POINT)) != global::FMOD.MODE.DEFAULT)
				{
					publicInfo.mode = (global::FMOD.MODE)(((uint)publicInfo.mode & 0xEFFFFFFFu) | 0x800);
					num = (int)exinfo.fileoffset;
					publicInfo.exinfo.fileoffset = 0u;
					num2 = (int)exinfo.length;
				}
				else
				{
					num = 0;
					num2 = global::FMOD.Studio.MarshallingHelper.stringLengthUtf8(name_or_data) + 1;
				}
				publicInfo.name_or_data = new byte[num2];
				global::System.Runtime.InteropServices.Marshal.Copy(new global::System.IntPtr(name_or_data.ToInt64() + num), publicInfo.name_or_data, 0, num2);
			}
			else
			{
				publicInfo.name_or_data = null;
			}
		}
	}
}
