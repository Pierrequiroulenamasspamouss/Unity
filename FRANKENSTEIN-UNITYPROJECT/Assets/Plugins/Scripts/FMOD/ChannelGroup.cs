namespace FMOD
{
	public class ChannelGroup : global::FMOD.ChannelControl
	{
		public ChannelGroup(global::System.IntPtr raw)
			: base(raw)
		{
		}

		public global::FMOD.RESULT release()
		{
			global::FMOD.RESULT rESULT = FMOD5_ChannelGroup_Release(getRaw());
			if (rESULT == global::FMOD.RESULT.OK)
			{
				rawPtr = global::System.IntPtr.Zero;
			}
			return rESULT;
		}

		public global::FMOD.RESULT addGroup(global::FMOD.ChannelGroup group)
		{
			return FMOD5_ChannelGroup_AddGroup(getRaw(), group.getRaw());
		}

		public global::FMOD.RESULT getNumGroups(out int numgroups)
		{
			return FMOD5_ChannelGroup_GetNumGroups(getRaw(), out numgroups);
		}

		public global::FMOD.RESULT getGroup(int index, out global::FMOD.ChannelGroup group)
		{
			group = null;
			global::System.IntPtr group2;
			global::FMOD.RESULT result = FMOD5_ChannelGroup_GetGroup(getRaw(), index, out group2);
			group = new global::FMOD.ChannelGroup(group2);
			return result;
		}

		public global::FMOD.RESULT getParentGroup(out global::FMOD.ChannelGroup group)
		{
			group = null;
			global::System.IntPtr group2;
			global::FMOD.RESULT result = FMOD5_ChannelGroup_GetParentGroup(getRaw(), out group2);
			group = new global::FMOD.ChannelGroup(group2);
			return result;
		}

		public global::FMOD.RESULT getName(global::System.Text.StringBuilder name, int namelen)
		{
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(name.Capacity);
			global::FMOD.RESULT result = FMOD5_ChannelGroup_GetName(getRaw(), intPtr, namelen);
			global::FMOD.StringMarshalHelper.NativeToBuilder(name, intPtr);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return result;
		}

		public global::FMOD.RESULT getNumChannels(out int numchannels)
		{
			return FMOD5_ChannelGroup_GetNumChannels(getRaw(), out numchannels);
		}

		public global::FMOD.RESULT getChannel(int index, out global::FMOD.Channel channel)
		{
			channel = null;
			global::System.IntPtr channel2;
			global::FMOD.RESULT result = FMOD5_ChannelGroup_GetChannel(getRaw(), index, out channel2);
			channel = new global::FMOD.Channel(channel2);
			return result;
		}

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Release(global::System.IntPtr channelgroup);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_AddGroup(global::System.IntPtr channelgroup, global::System.IntPtr group);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetNumGroups(global::System.IntPtr channelgroup, out int numgroups);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetGroup(global::System.IntPtr channelgroup, int index, out global::System.IntPtr group);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetParentGroup(global::System.IntPtr channelgroup, out global::System.IntPtr group);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetName(global::System.IntPtr channelgroup, global::System.IntPtr name, int namelen);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetNumChannels(global::System.IntPtr channelgroup, out int numchannels);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetChannel(global::System.IntPtr channelgroup, int index, out global::System.IntPtr channel);
	}
}
