namespace FMOD
{
	public class SoundGroup : global::FMOD.HandleBase
	{
		public SoundGroup(global::System.IntPtr raw)
			: base(raw)
		{
		}

		public global::FMOD.RESULT release()
		{
			global::FMOD.RESULT rESULT = FMOD5_SoundGroup_Release(getRaw());
			if (rESULT == global::FMOD.RESULT.OK)
			{
				rawPtr = global::System.IntPtr.Zero;
			}
			return rESULT;
		}

		public global::FMOD.RESULT getSystemObject(out global::FMOD.System system)
		{
			system = null;
			global::System.IntPtr system2;
			global::FMOD.RESULT result = FMOD5_SoundGroup_GetSystemObject(rawPtr, out system2);
			system = new global::FMOD.System(system2);
			return result;
		}

		public global::FMOD.RESULT setMaxAudible(int maxaudible)
		{
			return FMOD5_SoundGroup_SetMaxAudible(rawPtr, maxaudible);
		}

		public global::FMOD.RESULT getMaxAudible(out int maxaudible)
		{
			return FMOD5_SoundGroup_GetMaxAudible(rawPtr, out maxaudible);
		}

		public global::FMOD.RESULT setMaxAudibleBehavior(global::FMOD.SOUNDGROUP_BEHAVIOR behavior)
		{
			return FMOD5_SoundGroup_SetMaxAudibleBehavior(rawPtr, behavior);
		}

		public global::FMOD.RESULT getMaxAudibleBehavior(out global::FMOD.SOUNDGROUP_BEHAVIOR behavior)
		{
			return FMOD5_SoundGroup_GetMaxAudibleBehavior(rawPtr, out behavior);
		}

		public global::FMOD.RESULT setMuteFadeSpeed(float speed)
		{
			return FMOD5_SoundGroup_SetMuteFadeSpeed(rawPtr, speed);
		}

		public global::FMOD.RESULT getMuteFadeSpeed(out float speed)
		{
			return FMOD5_SoundGroup_GetMuteFadeSpeed(rawPtr, out speed);
		}

		public global::FMOD.RESULT setVolume(float volume)
		{
			return FMOD5_SoundGroup_SetVolume(rawPtr, volume);
		}

		public global::FMOD.RESULT getVolume(out float volume)
		{
			return FMOD5_SoundGroup_GetVolume(rawPtr, out volume);
		}

		public global::FMOD.RESULT stop()
		{
			return FMOD5_SoundGroup_Stop(rawPtr);
		}

		public global::FMOD.RESULT getName(global::System.Text.StringBuilder name, int namelen)
		{
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(name.Capacity);
			global::FMOD.RESULT result = FMOD5_SoundGroup_GetName(rawPtr, intPtr, namelen);
			global::FMOD.StringMarshalHelper.NativeToBuilder(name, intPtr);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return result;
		}

		public global::FMOD.RESULT getNumSounds(out int numsounds)
		{
			return FMOD5_SoundGroup_GetNumSounds(rawPtr, out numsounds);
		}

		public global::FMOD.RESULT getSound(int index, out global::FMOD.Sound sound)
		{
			sound = null;
			global::System.IntPtr sound2;
			global::FMOD.RESULT result = FMOD5_SoundGroup_GetSound(rawPtr, index, out sound2);
			sound = new global::FMOD.Sound(sound2);
			return result;
		}

		public global::FMOD.RESULT getNumPlaying(out int numplaying)
		{
			return FMOD5_SoundGroup_GetNumPlaying(rawPtr, out numplaying);
		}

		public global::FMOD.RESULT setUserData(global::System.IntPtr userdata)
		{
			return FMOD5_SoundGroup_SetUserData(rawPtr, userdata);
		}

		public global::FMOD.RESULT getUserData(out global::System.IntPtr userdata)
		{
			return FMOD5_SoundGroup_GetUserData(rawPtr, out userdata);
		}

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_SoundGroup_Release(global::System.IntPtr soundgroup);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_SoundGroup_GetSystemObject(global::System.IntPtr soundgroup, out global::System.IntPtr system);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_SoundGroup_SetMaxAudible(global::System.IntPtr soundgroup, int maxaudible);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_SoundGroup_GetMaxAudible(global::System.IntPtr soundgroup, out int maxaudible);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_SoundGroup_SetMaxAudibleBehavior(global::System.IntPtr soundgroup, global::FMOD.SOUNDGROUP_BEHAVIOR behavior);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_SoundGroup_GetMaxAudibleBehavior(global::System.IntPtr soundgroup, out global::FMOD.SOUNDGROUP_BEHAVIOR behavior);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_SoundGroup_SetMuteFadeSpeed(global::System.IntPtr soundgroup, float speed);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_SoundGroup_GetMuteFadeSpeed(global::System.IntPtr soundgroup, out float speed);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_SoundGroup_SetVolume(global::System.IntPtr soundgroup, float volume);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_SoundGroup_GetVolume(global::System.IntPtr soundgroup, out float volume);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_SoundGroup_Stop(global::System.IntPtr soundgroup);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_SoundGroup_GetName(global::System.IntPtr soundgroup, global::System.IntPtr name, int namelen);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_SoundGroup_GetNumSounds(global::System.IntPtr soundgroup, out int numsounds);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_SoundGroup_GetSound(global::System.IntPtr soundgroup, int index, out global::System.IntPtr sound);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_SoundGroup_GetNumPlaying(global::System.IntPtr soundgroup, out int numplaying);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_SoundGroup_SetUserData(global::System.IntPtr soundgroup, global::System.IntPtr userdata);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_SoundGroup_GetUserData(global::System.IntPtr soundgroup, out global::System.IntPtr userdata);
	}
}
