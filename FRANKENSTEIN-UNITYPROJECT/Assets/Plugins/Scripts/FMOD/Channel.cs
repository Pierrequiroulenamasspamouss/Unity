namespace FMOD
{
	public class Channel : global::FMOD.ChannelControl
	{
		public Channel(global::System.IntPtr raw)
			: base(raw)
		{
		}

		public global::FMOD.RESULT setFrequency(float frequency)
		{
			return FMOD5_Channel_SetFrequency(getRaw(), frequency);
		}

		public global::FMOD.RESULT getFrequency(out float frequency)
		{
			return FMOD5_Channel_GetFrequency(getRaw(), out frequency);
		}

		public global::FMOD.RESULT setPriority(int priority)
		{
			return FMOD5_Channel_SetPriority(getRaw(), priority);
		}

		public global::FMOD.RESULT getPriority(out int priority)
		{
			return FMOD5_Channel_GetPriority(getRaw(), out priority);
		}

		public global::FMOD.RESULT setPosition(uint position, global::FMOD.TIMEUNIT postype)
		{
			return FMOD5_Channel_SetPosition(getRaw(), position, postype);
		}

		public global::FMOD.RESULT getPosition(out uint position, global::FMOD.TIMEUNIT postype)
		{
			return FMOD5_Channel_GetPosition(getRaw(), out position, postype);
		}

		public global::FMOD.RESULT setChannelGroup(global::FMOD.ChannelGroup channelgroup)
		{
			return FMOD5_Channel_SetChannelGroup(getRaw(), channelgroup.getRaw());
		}

		public global::FMOD.RESULT getChannelGroup(out global::FMOD.ChannelGroup channelgroup)
		{
			channelgroup = null;
			global::System.IntPtr channelgroup2;
			global::FMOD.RESULT result = FMOD5_Channel_GetChannelGroup(getRaw(), out channelgroup2);
			channelgroup = new global::FMOD.ChannelGroup(channelgroup2);
			return result;
		}

		public global::FMOD.RESULT setLoopCount(int loopcount)
		{
			return FMOD5_Channel_SetLoopCount(getRaw(), loopcount);
		}

		public global::FMOD.RESULT getLoopCount(out int loopcount)
		{
			return FMOD5_Channel_GetLoopCount(getRaw(), out loopcount);
		}

		public global::FMOD.RESULT setLoopPoints(uint loopstart, global::FMOD.TIMEUNIT loopstarttype, uint loopend, global::FMOD.TIMEUNIT loopendtype)
		{
			return FMOD5_Channel_SetLoopPoints(getRaw(), loopstart, loopstarttype, loopend, loopendtype);
		}

		public global::FMOD.RESULT getLoopPoints(out uint loopstart, global::FMOD.TIMEUNIT loopstarttype, out uint loopend, global::FMOD.TIMEUNIT loopendtype)
		{
			return FMOD5_Channel_GetLoopPoints(getRaw(), out loopstart, loopstarttype, out loopend, loopendtype);
		}

		public global::FMOD.RESULT isVirtual(out bool isvirtual)
		{
			return FMOD5_Channel_IsVirtual(getRaw(), out isvirtual);
		}

		public global::FMOD.RESULT getCurrentSound(out global::FMOD.Sound sound)
		{
			sound = null;
			global::System.IntPtr sound2;
			global::FMOD.RESULT result = FMOD5_Channel_GetCurrentSound(getRaw(), out sound2);
			sound = new global::FMOD.Sound(sound2);
			return result;
		}

		public global::FMOD.RESULT getIndex(out int index)
		{
			return FMOD5_Channel_GetIndex(getRaw(), out index);
		}

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Channel_SetFrequency(global::System.IntPtr channel, float frequency);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Channel_GetFrequency(global::System.IntPtr channel, out float frequency);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Channel_SetPriority(global::System.IntPtr channel, int priority);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Channel_GetPriority(global::System.IntPtr channel, out int priority);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Channel_SetChannelGroup(global::System.IntPtr channel, global::System.IntPtr channelgroup);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Channel_GetChannelGroup(global::System.IntPtr channel, out global::System.IntPtr channelgroup);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Channel_IsVirtual(global::System.IntPtr channel, out bool isvirtual);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Channel_GetCurrentSound(global::System.IntPtr channel, out global::System.IntPtr sound);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Channel_GetIndex(global::System.IntPtr channel, out int index);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Channel_SetPosition(global::System.IntPtr channel, uint position, global::FMOD.TIMEUNIT postype);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Channel_GetPosition(global::System.IntPtr channel, out uint position, global::FMOD.TIMEUNIT postype);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Channel_SetMode(global::System.IntPtr channel, global::FMOD.MODE mode);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Channel_GetMode(global::System.IntPtr channel, out global::FMOD.MODE mode);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Channel_SetLoopCount(global::System.IntPtr channel, int loopcount);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Channel_GetLoopCount(global::System.IntPtr channel, out int loopcount);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Channel_SetLoopPoints(global::System.IntPtr channel, uint loopstart, global::FMOD.TIMEUNIT loopstarttype, uint loopend, global::FMOD.TIMEUNIT loopendtype);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Channel_GetLoopPoints(global::System.IntPtr channel, out uint loopstart, global::FMOD.TIMEUNIT loopstarttype, out uint loopend, global::FMOD.TIMEUNIT loopendtype);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Channel_SetUserData(global::System.IntPtr channel, global::System.IntPtr userdata);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Channel_GetUserData(global::System.IntPtr channel, out global::System.IntPtr userdata);
	}
}
