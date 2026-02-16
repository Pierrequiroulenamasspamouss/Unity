namespace FMOD
{
	public class Sound : global::FMOD.HandleBase
	{
		public Sound(global::System.IntPtr raw)
			: base(raw)
		{
		}

		public global::FMOD.RESULT release()
		{
			global::FMOD.RESULT rESULT = FMOD5_Sound_Release(rawPtr);
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
			global::FMOD.RESULT result = FMOD5_Sound_GetSystemObject(rawPtr, out system2);
			system = new global::FMOD.System(system2);
			return result;
		}

		public global::FMOD.RESULT @lock(uint offset, uint length, out global::System.IntPtr ptr1, out global::System.IntPtr ptr2, out uint len1, out uint len2)
		{
			return FMOD5_Sound_Lock(rawPtr, offset, length, out ptr1, out ptr2, out len1, out len2);
		}

		public global::FMOD.RESULT unlock(global::System.IntPtr ptr1, global::System.IntPtr ptr2, uint len1, uint len2)
		{
			return FMOD5_Sound_Unlock(rawPtr, ptr1, ptr2, len1, len2);
		}

		public global::FMOD.RESULT setDefaults(float frequency, int priority)
		{
			return FMOD5_Sound_SetDefaults(rawPtr, frequency, priority);
		}

		public global::FMOD.RESULT getDefaults(out float frequency, out int priority)
		{
			return FMOD5_Sound_GetDefaults(rawPtr, out frequency, out priority);
		}

		public global::FMOD.RESULT set3DMinMaxDistance(float min, float max)
		{
			return FMOD5_Sound_Set3DMinMaxDistance(rawPtr, min, max);
		}

		public global::FMOD.RESULT get3DMinMaxDistance(out float min, out float max)
		{
			return FMOD5_Sound_Get3DMinMaxDistance(rawPtr, out min, out max);
		}

		public global::FMOD.RESULT set3DConeSettings(float insideconeangle, float outsideconeangle, float outsidevolume)
		{
			return FMOD5_Sound_Set3DConeSettings(rawPtr, insideconeangle, outsideconeangle, outsidevolume);
		}

		public global::FMOD.RESULT get3DConeSettings(out float insideconeangle, out float outsideconeangle, out float outsidevolume)
		{
			return FMOD5_Sound_Get3DConeSettings(rawPtr, out insideconeangle, out outsideconeangle, out outsidevolume);
		}

		public global::FMOD.RESULT set3DCustomRolloff(ref global::FMOD.VECTOR points, int numpoints)
		{
			return FMOD5_Sound_Set3DCustomRolloff(rawPtr, ref points, numpoints);
		}

		public global::FMOD.RESULT get3DCustomRolloff(out global::System.IntPtr points, out int numpoints)
		{
			return FMOD5_Sound_Get3DCustomRolloff(rawPtr, out points, out numpoints);
		}

		public global::FMOD.RESULT setSubSound(int index, global::FMOD.Sound subsound)
		{
			global::System.IntPtr raw = subsound.getRaw();
			return FMOD5_Sound_SetSubSound(rawPtr, index, raw);
		}

		public global::FMOD.RESULT getSubSound(int index, out global::FMOD.Sound subsound)
		{
			subsound = null;
			global::System.IntPtr subsound2;
			global::FMOD.RESULT result = FMOD5_Sound_GetSubSound(rawPtr, index, out subsound2);
			subsound = new global::FMOD.Sound(subsound2);
			return result;
		}

		public global::FMOD.RESULT getSubSoundParent(out global::FMOD.Sound parentsound)
		{
			parentsound = null;
			global::System.IntPtr parentsound2;
			global::FMOD.RESULT result = FMOD5_Sound_GetSubSoundParent(rawPtr, out parentsound2);
			parentsound = new global::FMOD.Sound(parentsound2);
			return result;
		}

		public global::FMOD.RESULT getName(global::System.Text.StringBuilder name, int namelen)
		{
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(name.Capacity);
			global::FMOD.RESULT result = FMOD5_Sound_GetName(rawPtr, intPtr, namelen);
			global::FMOD.StringMarshalHelper.NativeToBuilder(name, intPtr);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return result;
		}

		public global::FMOD.RESULT getLength(out uint length, global::FMOD.TIMEUNIT lengthtype)
		{
			return FMOD5_Sound_GetLength(rawPtr, out length, lengthtype);
		}

		public global::FMOD.RESULT getFormat(out global::FMOD.SOUND_TYPE type, out global::FMOD.SOUND_FORMAT format, out int channels, out int bits)
		{
			return FMOD5_Sound_GetFormat(rawPtr, out type, out format, out channels, out bits);
		}

		public global::FMOD.RESULT getNumSubSounds(out int numsubsounds)
		{
			return FMOD5_Sound_GetNumSubSounds(rawPtr, out numsubsounds);
		}

		public global::FMOD.RESULT getNumTags(out int numtags, out int numtagsupdated)
		{
			return FMOD5_Sound_GetNumTags(rawPtr, out numtags, out numtagsupdated);
		}

		public global::FMOD.RESULT getTag(string name, int index, out global::FMOD.TAG tag)
		{
			return FMOD5_Sound_GetTag(rawPtr, name, index, out tag);
		}

		public global::FMOD.RESULT getOpenState(out global::FMOD.OPENSTATE openstate, out uint percentbuffered, out bool starving, out bool diskbusy)
		{
			return FMOD5_Sound_GetOpenState(rawPtr, out openstate, out percentbuffered, out starving, out diskbusy);
		}

		public global::FMOD.RESULT readData(global::System.IntPtr buffer, uint lenbytes, out uint read)
		{
			return FMOD5_Sound_ReadData(rawPtr, buffer, lenbytes, out read);
		}

		public global::FMOD.RESULT seekData(uint pcm)
		{
			return FMOD5_Sound_SeekData(rawPtr, pcm);
		}

		public global::FMOD.RESULT setSoundGroup(global::FMOD.SoundGroup soundgroup)
		{
			return FMOD5_Sound_SetSoundGroup(rawPtr, soundgroup.getRaw());
		}

		public global::FMOD.RESULT getSoundGroup(out global::FMOD.SoundGroup soundgroup)
		{
			soundgroup = null;
			global::System.IntPtr soundgroup2;
			global::FMOD.RESULT result = FMOD5_Sound_GetSoundGroup(rawPtr, out soundgroup2);
			soundgroup = new global::FMOD.SoundGroup(soundgroup2);
			return result;
		}

		public global::FMOD.RESULT getNumSyncPoints(out int numsyncpoints)
		{
			return FMOD5_Sound_GetNumSyncPoints(rawPtr, out numsyncpoints);
		}

		public global::FMOD.RESULT getSyncPoint(int index, out global::System.IntPtr point)
		{
			return FMOD5_Sound_GetSyncPoint(rawPtr, index, out point);
		}

		public global::FMOD.RESULT getSyncPointInfo(global::System.IntPtr point, global::System.Text.StringBuilder name, int namelen, out uint offset, global::FMOD.TIMEUNIT offsettype)
		{
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(name.Capacity);
			global::FMOD.RESULT result = FMOD5_Sound_GetSyncPointInfo(rawPtr, point, intPtr, namelen, out offset, offsettype);
			global::FMOD.StringMarshalHelper.NativeToBuilder(name, intPtr);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return result;
		}

		public global::FMOD.RESULT addSyncPoint(uint offset, global::FMOD.TIMEUNIT offsettype, string name, out global::System.IntPtr point)
		{
			return FMOD5_Sound_AddSyncPoint(rawPtr, offset, offsettype, name, out point);
		}

		public global::FMOD.RESULT deleteSyncPoint(global::System.IntPtr point)
		{
			return FMOD5_Sound_DeleteSyncPoint(rawPtr, point);
		}

		public global::FMOD.RESULT setMode(global::FMOD.MODE mode)
		{
			return FMOD5_Sound_SetMode(rawPtr, mode);
		}

		public global::FMOD.RESULT getMode(out global::FMOD.MODE mode)
		{
			return FMOD5_Sound_GetMode(rawPtr, out mode);
		}

		public global::FMOD.RESULT setLoopCount(int loopcount)
		{
			return FMOD5_Sound_SetLoopCount(rawPtr, loopcount);
		}

		public global::FMOD.RESULT getLoopCount(out int loopcount)
		{
			return FMOD5_Sound_GetLoopCount(rawPtr, out loopcount);
		}

		public global::FMOD.RESULT setLoopPoints(uint loopstart, global::FMOD.TIMEUNIT loopstarttype, uint loopend, global::FMOD.TIMEUNIT loopendtype)
		{
			return FMOD5_Sound_SetLoopPoints(rawPtr, loopstart, loopstarttype, loopend, loopendtype);
		}

		public global::FMOD.RESULT getLoopPoints(out uint loopstart, global::FMOD.TIMEUNIT loopstarttype, out uint loopend, global::FMOD.TIMEUNIT loopendtype)
		{
			return FMOD5_Sound_GetLoopPoints(rawPtr, out loopstart, loopstarttype, out loopend, loopendtype);
		}

		public global::FMOD.RESULT getMusicNumChannels(out int numchannels)
		{
			return FMOD5_Sound_GetMusicNumChannels(rawPtr, out numchannels);
		}

		public global::FMOD.RESULT setMusicChannelVolume(int channel, float volume)
		{
			return FMOD5_Sound_SetMusicChannelVolume(rawPtr, channel, volume);
		}

		public global::FMOD.RESULT getMusicChannelVolume(int channel, out float volume)
		{
			return FMOD5_Sound_GetMusicChannelVolume(rawPtr, channel, out volume);
		}

		public global::FMOD.RESULT setMusicSpeed(float speed)
		{
			return FMOD5_Sound_SetMusicSpeed(rawPtr, speed);
		}

		public global::FMOD.RESULT getMusicSpeed(out float speed)
		{
			return FMOD5_Sound_GetMusicSpeed(rawPtr, out speed);
		}

		public global::FMOD.RESULT setUserData(global::System.IntPtr userdata)
		{
			return FMOD5_Sound_SetUserData(rawPtr, userdata);
		}

		public global::FMOD.RESULT getUserData(out global::System.IntPtr userdata)
		{
			return FMOD5_Sound_GetUserData(rawPtr, out userdata);
		}

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_Release(global::System.IntPtr sound);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetSystemObject(global::System.IntPtr sound, out global::System.IntPtr system);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_Lock(global::System.IntPtr sound, uint offset, uint length, out global::System.IntPtr ptr1, out global::System.IntPtr ptr2, out uint len1, out uint len2);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_Unlock(global::System.IntPtr sound, global::System.IntPtr ptr1, global::System.IntPtr ptr2, uint len1, uint len2);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_SetDefaults(global::System.IntPtr sound, float frequency, int priority);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetDefaults(global::System.IntPtr sound, out float frequency, out int priority);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_Set3DMinMaxDistance(global::System.IntPtr sound, float min, float max);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_Get3DMinMaxDistance(global::System.IntPtr sound, out float min, out float max);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_Set3DConeSettings(global::System.IntPtr sound, float insideconeangle, float outsideconeangle, float outsidevolume);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_Get3DConeSettings(global::System.IntPtr sound, out float insideconeangle, out float outsideconeangle, out float outsidevolume);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_Set3DCustomRolloff(global::System.IntPtr sound, ref global::FMOD.VECTOR points, int numpoints);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_Get3DCustomRolloff(global::System.IntPtr sound, out global::System.IntPtr points, out int numpoints);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_SetSubSound(global::System.IntPtr sound, int index, global::System.IntPtr subsound);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetSubSound(global::System.IntPtr sound, int index, out global::System.IntPtr subsound);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetSubSoundParent(global::System.IntPtr sound, out global::System.IntPtr parentsound);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetName(global::System.IntPtr sound, global::System.IntPtr name, int namelen);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetLength(global::System.IntPtr sound, out uint length, global::FMOD.TIMEUNIT lengthtype);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetFormat(global::System.IntPtr sound, out global::FMOD.SOUND_TYPE type, out global::FMOD.SOUND_FORMAT format, out int channels, out int bits);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetNumSubSounds(global::System.IntPtr sound, out int numsubsounds);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetNumTags(global::System.IntPtr sound, out int numtags, out int numtagsupdated);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetTag(global::System.IntPtr sound, string name, int index, out global::FMOD.TAG tag);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetOpenState(global::System.IntPtr sound, out global::FMOD.OPENSTATE openstate, out uint percentbuffered, out bool starving, out bool diskbusy);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_ReadData(global::System.IntPtr sound, global::System.IntPtr buffer, uint lenbytes, out uint read);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_SeekData(global::System.IntPtr sound, uint pcm);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_SetSoundGroup(global::System.IntPtr sound, global::System.IntPtr soundgroup);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetSoundGroup(global::System.IntPtr sound, out global::System.IntPtr soundgroup);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetNumSyncPoints(global::System.IntPtr sound, out int numsyncpoints);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetSyncPoint(global::System.IntPtr sound, int index, out global::System.IntPtr point);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetSyncPointInfo(global::System.IntPtr sound, global::System.IntPtr point, global::System.IntPtr name, int namelen, out uint offset, global::FMOD.TIMEUNIT offsettype);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_AddSyncPoint(global::System.IntPtr sound, uint offset, global::FMOD.TIMEUNIT offsettype, string name, out global::System.IntPtr point);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_DeleteSyncPoint(global::System.IntPtr sound, global::System.IntPtr point);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_SetMode(global::System.IntPtr sound, global::FMOD.MODE mode);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetMode(global::System.IntPtr sound, out global::FMOD.MODE mode);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_SetLoopCount(global::System.IntPtr sound, int loopcount);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetLoopCount(global::System.IntPtr sound, out int loopcount);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_SetLoopPoints(global::System.IntPtr sound, uint loopstart, global::FMOD.TIMEUNIT loopstarttype, uint loopend, global::FMOD.TIMEUNIT loopendtype);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetLoopPoints(global::System.IntPtr sound, out uint loopstart, global::FMOD.TIMEUNIT loopstarttype, out uint loopend, global::FMOD.TIMEUNIT loopendtype);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetMusicNumChannels(global::System.IntPtr sound, out int numchannels);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_SetMusicChannelVolume(global::System.IntPtr sound, int channel, float volume);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetMusicChannelVolume(global::System.IntPtr sound, int channel, out float volume);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_SetMusicSpeed(global::System.IntPtr sound, float speed);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetMusicSpeed(global::System.IntPtr sound, out float speed);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_SetUserData(global::System.IntPtr sound, global::System.IntPtr userdata);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Sound_GetUserData(global::System.IntPtr sound, out global::System.IntPtr userdata);
	}
}
