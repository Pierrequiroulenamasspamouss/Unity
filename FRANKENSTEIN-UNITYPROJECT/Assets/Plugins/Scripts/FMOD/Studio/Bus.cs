namespace FMOD.Studio
{
	public class Bus : global::FMOD.Studio.HandleBase
	{
		public Bus(global::System.IntPtr raw)
			: base(raw)
		{
		}

		public global::FMOD.RESULT getID(out global::System.Guid id)
		{
			return FMOD_Studio_Bus_GetID(rawPtr, out id);
		}

		public global::FMOD.RESULT getPath(out string path)
		{
			path = null;
			byte[] array = new byte[256];
			int retrieved = 0;
			global::FMOD.RESULT rESULT = FMOD_Studio_Bus_GetPath(rawPtr, array, array.Length, out retrieved);
			if (rESULT == global::FMOD.RESULT.ERR_TRUNCATED)
			{
				array = new byte[retrieved];
				rESULT = FMOD_Studio_Bus_GetPath(rawPtr, array, array.Length, out retrieved);
			}
			if (rESULT == global::FMOD.RESULT.OK)
			{
				path = global::System.Text.Encoding.UTF8.GetString(array, 0, retrieved - 1);
			}
			return rESULT;
		}

		public global::FMOD.RESULT getFaderLevel(out float volume)
		{
			return FMOD_Studio_Bus_GetFaderLevel(rawPtr, out volume);
		}

		public global::FMOD.RESULT setFaderLevel(float volume)
		{
			return FMOD_Studio_Bus_SetFaderLevel(rawPtr, volume);
		}

		public global::FMOD.RESULT getPaused(out bool paused)
		{
			return FMOD_Studio_Bus_GetPaused(rawPtr, out paused);
		}

		public global::FMOD.RESULT setPaused(bool paused)
		{
			return FMOD_Studio_Bus_SetPaused(rawPtr, paused);
		}

		public global::FMOD.RESULT getMute(out bool mute)
		{
			return FMOD_Studio_Bus_GetMute(rawPtr, out mute);
		}

		public global::FMOD.RESULT setMute(bool mute)
		{
			return FMOD_Studio_Bus_SetMute(rawPtr, mute);
		}

		public global::FMOD.RESULT stopAllEvents(global::FMOD.Studio.STOP_MODE mode)
		{
			return FMOD_Studio_Bus_StopAllEvents(rawPtr, mode);
		}

		public global::FMOD.RESULT lockChannelGroup()
		{
			return FMOD_Studio_Bus_LockChannelGroup(rawPtr);
		}

		public global::FMOD.RESULT unlockChannelGroup()
		{
			return FMOD_Studio_Bus_UnlockChannelGroup(rawPtr);
		}

		public global::FMOD.RESULT getChannelGroup(out global::FMOD.ChannelGroup group)
		{
			group = null;
			global::System.IntPtr group2 = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_Bus_GetChannelGroup(rawPtr, out group2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			group = new global::FMOD.ChannelGroup(group2);
			return rESULT;
		}

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern bool FMOD_Studio_Bus_IsValid(global::System.IntPtr bus);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bus_GetID(global::System.IntPtr bus, out global::System.Guid id);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bus_GetPath(global::System.IntPtr bus, [global::System.Runtime.InteropServices.Out] byte[] path, int size, out int retrieved);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bus_GetFaderLevel(global::System.IntPtr bus, out float value);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bus_SetFaderLevel(global::System.IntPtr bus, float value);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bus_GetPaused(global::System.IntPtr bus, out bool paused);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bus_SetPaused(global::System.IntPtr bus, bool paused);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bus_GetMute(global::System.IntPtr bus, out bool mute);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bus_SetMute(global::System.IntPtr bus, bool mute);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bus_StopAllEvents(global::System.IntPtr bus, global::FMOD.Studio.STOP_MODE mode);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bus_LockChannelGroup(global::System.IntPtr bus);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bus_UnlockChannelGroup(global::System.IntPtr bus);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bus_GetChannelGroup(global::System.IntPtr bus, out global::System.IntPtr group);

		protected override bool isValidInternal()
		{
			return FMOD_Studio_Bus_IsValid(rawPtr);
		}
	}
}
