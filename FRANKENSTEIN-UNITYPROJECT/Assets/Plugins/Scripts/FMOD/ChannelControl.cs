namespace FMOD
{
	public class ChannelControl : global::FMOD.HandleBase
	{
		protected ChannelControl(global::System.IntPtr raw)
			: base(raw)
		{
		}

		public global::FMOD.RESULT getSystemObject(out global::FMOD.System system)
		{
			system = null;
			global::System.IntPtr system2;
			global::FMOD.RESULT result = FMOD5_ChannelGroup_GetSystemObject(rawPtr, out system2);
			system = new global::FMOD.System(system2);
			return result;
		}

		public global::FMOD.RESULT stop()
		{
			return FMOD5_ChannelGroup_Stop(rawPtr);
		}

		public global::FMOD.RESULT setPaused(bool paused)
		{
			return FMOD5_ChannelGroup_SetPaused(rawPtr, paused);
		}

		public global::FMOD.RESULT getPaused(out bool paused)
		{
			return FMOD5_ChannelGroup_GetPaused(rawPtr, out paused);
		}

		public global::FMOD.RESULT setVolume(float volume)
		{
			return FMOD5_ChannelGroup_SetVolume(rawPtr, volume);
		}

		public global::FMOD.RESULT getVolume(out float volume)
		{
			return FMOD5_ChannelGroup_GetVolume(rawPtr, out volume);
		}

		public global::FMOD.RESULT setVolumeRamp(bool ramp)
		{
			return FMOD5_ChannelGroup_SetVolumeRamp(rawPtr, ramp);
		}

		public global::FMOD.RESULT getVolumeRamp(out bool ramp)
		{
			return FMOD5_ChannelGroup_GetVolumeRamp(rawPtr, out ramp);
		}

		public global::FMOD.RESULT getAudibility(out float audibility)
		{
			return FMOD5_ChannelGroup_GetAudibility(rawPtr, out audibility);
		}

		public global::FMOD.RESULT setPitch(float pitch)
		{
			return FMOD5_ChannelGroup_SetPitch(rawPtr, pitch);
		}

		public global::FMOD.RESULT getPitch(out float pitch)
		{
			return FMOD5_ChannelGroup_GetPitch(rawPtr, out pitch);
		}

		public global::FMOD.RESULT setMute(bool mute)
		{
			return FMOD5_ChannelGroup_SetMute(rawPtr, mute);
		}

		public global::FMOD.RESULT getMute(out bool mute)
		{
			return FMOD5_ChannelGroup_GetMute(rawPtr, out mute);
		}

		public global::FMOD.RESULT setReverbProperties(int instance, float wet)
		{
			return FMOD5_ChannelGroup_SetReverbProperties(rawPtr, instance, wet);
		}

		public global::FMOD.RESULT getReverbProperties(int instance, out float wet)
		{
			return FMOD5_ChannelGroup_GetReverbProperties(rawPtr, instance, out wet);
		}

		public global::FMOD.RESULT setLowPassGain(float gain)
		{
			return FMOD5_ChannelGroup_SetLowPassGain(rawPtr, gain);
		}

		public global::FMOD.RESULT getLowPassGain(out float gain)
		{
			return FMOD5_ChannelGroup_GetLowPassGain(rawPtr, out gain);
		}

		public global::FMOD.RESULT setMode(global::FMOD.MODE mode)
		{
			return FMOD5_ChannelGroup_SetMode(rawPtr, mode);
		}

		public global::FMOD.RESULT getMode(out global::FMOD.MODE mode)
		{
			return FMOD5_ChannelGroup_GetMode(rawPtr, out mode);
		}

		public global::FMOD.RESULT setCallback(global::FMOD.CHANNEL_CALLBACK callback)
		{
			return FMOD5_ChannelGroup_SetCallback(rawPtr, callback);
		}

		public global::FMOD.RESULT isPlaying(out bool isplaying)
		{
			return FMOD5_ChannelGroup_IsPlaying(rawPtr, out isplaying);
		}

		public global::FMOD.RESULT setPan(float pan)
		{
			return FMOD5_ChannelGroup_SetPan(rawPtr, pan);
		}

		public global::FMOD.RESULT setMixLevelsOutput(float frontleft, float frontright, float center, float lfe, float surroundleft, float surroundright, float backleft, float backright)
		{
			return FMOD5_ChannelGroup_SetMixLevelsOutput(rawPtr, frontleft, frontright, center, lfe, surroundleft, surroundright, backleft, backright);
		}

		public global::FMOD.RESULT setMixLevelsInput(float[] levels, int numlevels)
		{
			return FMOD5_ChannelGroup_SetMixLevelsInput(rawPtr, levels, numlevels);
		}

		public global::FMOD.RESULT setMixMatrix(float[] matrix, int outchannels, int inchannels, int inchannel_hop)
		{
			return FMOD5_ChannelGroup_SetMixMatrix(rawPtr, matrix, outchannels, inchannels, inchannel_hop);
		}

		public global::FMOD.RESULT getMixMatrix(float[] matrix, out int outchannels, out int inchannels, int inchannel_hop)
		{
			return FMOD5_ChannelGroup_GetMixMatrix(rawPtr, matrix, out outchannels, out inchannels, inchannel_hop);
		}

		public global::FMOD.RESULT getDSPClock(out ulong dspclock, out ulong parentclock)
		{
			return FMOD5_ChannelGroup_GetDSPClock(rawPtr, out dspclock, out parentclock);
		}

		public global::FMOD.RESULT setDelay(ulong dspclock_start, ulong dspclock_end, bool stopchannels)
		{
			return FMOD5_ChannelGroup_SetDelay(rawPtr, dspclock_start, dspclock_end, stopchannels);
		}

		public global::FMOD.RESULT getDelay(out ulong dspclock_start, out ulong dspclock_end, out bool stopchannels)
		{
			return FMOD5_ChannelGroup_GetDelay(rawPtr, out dspclock_start, out dspclock_end, out stopchannels);
		}

		public global::FMOD.RESULT addFadePoint(ulong dspclock, float volume)
		{
			return FMOD5_ChannelGroup_AddFadePoint(rawPtr, dspclock, volume);
		}

		public global::FMOD.RESULT setFadePointRamp(ulong dspclock, float volume)
		{
			return FMOD5_ChannelGroup_SetFadePointRamp(rawPtr, dspclock, volume);
		}

		public global::FMOD.RESULT removeFadePoints(ulong dspclock_start, ulong dspclock_end)
		{
			return FMOD5_ChannelGroup_RemoveFadePoints(rawPtr, dspclock_start, dspclock_end);
		}

		public global::FMOD.RESULT getFadePoints(ref uint numpoints, ulong[] point_dspclock, float[] point_volume)
		{
			return FMOD5_ChannelGroup_GetFadePoints(rawPtr, ref numpoints, point_dspclock, point_volume);
		}

		public global::FMOD.RESULT getDSP(int index, out global::FMOD.DSP dsp)
		{
			dsp = null;
			global::System.IntPtr dsp2;
			global::FMOD.RESULT result = FMOD5_ChannelGroup_GetDSP(rawPtr, index, out dsp2);
			dsp = new global::FMOD.DSP(dsp2);
			return result;
		}

		public global::FMOD.RESULT addDSP(int index, global::FMOD.DSP dsp)
		{
			return FMOD5_ChannelGroup_AddDSP(rawPtr, index, dsp.getRaw());
		}

		public global::FMOD.RESULT removeDSP(global::FMOD.DSP dsp)
		{
			return FMOD5_ChannelGroup_RemoveDSP(rawPtr, dsp.getRaw());
		}

		public global::FMOD.RESULT getNumDSPs(out int numdsps)
		{
			return FMOD5_ChannelGroup_GetNumDSPs(rawPtr, out numdsps);
		}

		public global::FMOD.RESULT setDSPIndex(global::FMOD.DSP dsp, int index)
		{
			return FMOD5_ChannelGroup_SetDSPIndex(rawPtr, dsp.getRaw(), index);
		}

		public global::FMOD.RESULT getDSPIndex(global::FMOD.DSP dsp, out int index)
		{
			return FMOD5_ChannelGroup_GetDSPIndex(rawPtr, dsp.getRaw(), out index);
		}

		public global::FMOD.RESULT overridePanDSP(global::FMOD.DSP pan)
		{
			return FMOD5_ChannelGroup_OverridePanDSP(rawPtr, pan.getRaw());
		}

		public global::FMOD.RESULT set3DAttributes(ref global::FMOD.VECTOR pos, ref global::FMOD.VECTOR vel, ref global::FMOD.VECTOR alt_pan_pos)
		{
			return FMOD5_ChannelGroup_Set3DAttributes(rawPtr, ref pos, ref vel, ref alt_pan_pos);
		}

		public global::FMOD.RESULT get3DAttributes(out global::FMOD.VECTOR pos, out global::FMOD.VECTOR vel, out global::FMOD.VECTOR alt_pan_pos)
		{
			return FMOD5_ChannelGroup_Get3DAttributes(rawPtr, out pos, out vel, out alt_pan_pos);
		}

		public global::FMOD.RESULT set3DMinMaxDistance(float mindistance, float maxdistance)
		{
			return FMOD5_ChannelGroup_Set3DMinMaxDistance(rawPtr, mindistance, maxdistance);
		}

		public global::FMOD.RESULT get3DMinMaxDistance(out float mindistance, out float maxdistance)
		{
			return FMOD5_ChannelGroup_Get3DMinMaxDistance(rawPtr, out mindistance, out maxdistance);
		}

		public global::FMOD.RESULT set3DConeSettings(float insideconeangle, float outsideconeangle, float outsidevolume)
		{
			return FMOD5_ChannelGroup_Set3DConeSettings(rawPtr, insideconeangle, outsideconeangle, outsidevolume);
		}

		public global::FMOD.RESULT get3DConeSettings(out float insideconeangle, out float outsideconeangle, out float outsidevolume)
		{
			return FMOD5_ChannelGroup_Get3DConeSettings(rawPtr, out insideconeangle, out outsideconeangle, out outsidevolume);
		}

		public global::FMOD.RESULT set3DConeOrientation(ref global::FMOD.VECTOR orientation)
		{
			return FMOD5_ChannelGroup_Set3DConeOrientation(rawPtr, ref orientation);
		}

		public global::FMOD.RESULT get3DConeOrientation(out global::FMOD.VECTOR orientation)
		{
			return FMOD5_ChannelGroup_Get3DConeOrientation(rawPtr, out orientation);
		}

		public global::FMOD.RESULT set3DCustomRolloff(ref global::FMOD.VECTOR points, int numpoints)
		{
			return FMOD5_ChannelGroup_Set3DCustomRolloff(rawPtr, ref points, numpoints);
		}

		public global::FMOD.RESULT get3DCustomRolloff(out global::System.IntPtr points, out int numpoints)
		{
			return FMOD5_ChannelGroup_Get3DCustomRolloff(rawPtr, out points, out numpoints);
		}

		public global::FMOD.RESULT set3DOcclusion(float directocclusion, float reverbocclusion)
		{
			return FMOD5_ChannelGroup_Set3DOcclusion(rawPtr, directocclusion, reverbocclusion);
		}

		public global::FMOD.RESULT get3DOcclusion(out float directocclusion, out float reverbocclusion)
		{
			return FMOD5_ChannelGroup_Get3DOcclusion(rawPtr, out directocclusion, out reverbocclusion);
		}

		public global::FMOD.RESULT set3DSpread(float angle)
		{
			return FMOD5_ChannelGroup_Set3DSpread(rawPtr, angle);
		}

		public global::FMOD.RESULT get3DSpread(out float angle)
		{
			return FMOD5_ChannelGroup_Get3DSpread(rawPtr, out angle);
		}

		public global::FMOD.RESULT set3DLevel(float level)
		{
			return FMOD5_ChannelGroup_Set3DLevel(rawPtr, level);
		}

		public global::FMOD.RESULT get3DLevel(out float level)
		{
			return FMOD5_ChannelGroup_Get3DLevel(rawPtr, out level);
		}

		public global::FMOD.RESULT set3DDopplerLevel(float level)
		{
			return FMOD5_ChannelGroup_Set3DDopplerLevel(rawPtr, level);
		}

		public global::FMOD.RESULT get3DDopplerLevel(out float level)
		{
			return FMOD5_ChannelGroup_Get3DDopplerLevel(rawPtr, out level);
		}

		public global::FMOD.RESULT set3DDistanceFilter(bool custom, float customLevel, float centerFreq)
		{
			return FMOD5_ChannelGroup_Set3DDistanceFilter(rawPtr, custom, customLevel, centerFreq);
		}

		public global::FMOD.RESULT get3DDistanceFilter(out bool custom, out float customLevel, out float centerFreq)
		{
			return FMOD5_ChannelGroup_Get3DDistanceFilter(rawPtr, out custom, out customLevel, out centerFreq);
		}

		public global::FMOD.RESULT setUserData(global::System.IntPtr userdata)
		{
			return FMOD5_ChannelGroup_SetUserData(rawPtr, userdata);
		}

		public global::FMOD.RESULT getUserData(out global::System.IntPtr userdata)
		{
			return FMOD5_ChannelGroup_GetUserData(rawPtr, out userdata);
		}

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Stop(global::System.IntPtr channelgroup);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_SetPaused(global::System.IntPtr channelgroup, bool paused);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetPaused(global::System.IntPtr channelgroup, out bool paused);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetVolume(global::System.IntPtr channelgroup, out float volume);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_SetVolumeRamp(global::System.IntPtr channelgroup, bool ramp);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetVolumeRamp(global::System.IntPtr channelgroup, out bool ramp);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetAudibility(global::System.IntPtr channelgroup, out float audibility);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_SetPitch(global::System.IntPtr channelgroup, float pitch);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetPitch(global::System.IntPtr channelgroup, out float pitch);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_SetMute(global::System.IntPtr channelgroup, bool mute);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetMute(global::System.IntPtr channelgroup, out bool mute);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_SetReverbProperties(global::System.IntPtr channelgroup, int instance, float wet);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetReverbProperties(global::System.IntPtr channelgroup, int instance, out float wet);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_SetLowPassGain(global::System.IntPtr channelgroup, float gain);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetLowPassGain(global::System.IntPtr channelgroup, out float gain);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_SetMode(global::System.IntPtr channelgroup, global::FMOD.MODE mode);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetMode(global::System.IntPtr channelgroup, out global::FMOD.MODE mode);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_SetCallback(global::System.IntPtr channelgroup, global::FMOD.CHANNEL_CALLBACK callback);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_IsPlaying(global::System.IntPtr channelgroup, out bool isplaying);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_SetPan(global::System.IntPtr channelgroup, float pan);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_SetMixLevelsOutput(global::System.IntPtr channelgroup, float frontleft, float frontright, float center, float lfe, float surroundleft, float surroundright, float backleft, float backright);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_SetMixLevelsInput(global::System.IntPtr channelgroup, float[] levels, int numlevels);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_SetMixMatrix(global::System.IntPtr channelgroup, float[] matrix, int outchannels, int inchannels, int inchannel_hop);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetMixMatrix(global::System.IntPtr channelgroup, float[] matrix, out int outchannels, out int inchannels, int inchannel_hop);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetDSPClock(global::System.IntPtr channelgroup, out ulong dspclock, out ulong parentclock);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_SetDelay(global::System.IntPtr channelgroup, ulong dspclock_start, ulong dspclock_end, bool stopchannels);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetDelay(global::System.IntPtr channelgroup, out ulong dspclock_start, out ulong dspclock_end, out bool stopchannels);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_AddFadePoint(global::System.IntPtr channelgroup, ulong dspclock, float volume);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_SetFadePointRamp(global::System.IntPtr channelgroup, ulong dspclock, float volume);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_RemoveFadePoints(global::System.IntPtr channelgroup, ulong dspclock_start, ulong dspclock_end);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetFadePoints(global::System.IntPtr channelgroup, ref uint numpoints, ulong[] point_dspclock, float[] point_volume);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Set3DAttributes(global::System.IntPtr channelgroup, ref global::FMOD.VECTOR pos, ref global::FMOD.VECTOR vel, ref global::FMOD.VECTOR alt_pan_pos);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Get3DAttributes(global::System.IntPtr channelgroup, out global::FMOD.VECTOR pos, out global::FMOD.VECTOR vel, out global::FMOD.VECTOR alt_pan_pos);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Set3DMinMaxDistance(global::System.IntPtr channelgroup, float mindistance, float maxdistance);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Get3DMinMaxDistance(global::System.IntPtr channelgroup, out float mindistance, out float maxdistance);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Set3DConeSettings(global::System.IntPtr channelgroup, float insideconeangle, float outsideconeangle, float outsidevolume);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Get3DConeSettings(global::System.IntPtr channelgroup, out float insideconeangle, out float outsideconeangle, out float outsidevolume);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Set3DConeOrientation(global::System.IntPtr channelgroup, ref global::FMOD.VECTOR orientation);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Get3DConeOrientation(global::System.IntPtr channelgroup, out global::FMOD.VECTOR orientation);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Set3DCustomRolloff(global::System.IntPtr channelgroup, ref global::FMOD.VECTOR points, int numpoints);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Get3DCustomRolloff(global::System.IntPtr channelgroup, out global::System.IntPtr points, out int numpoints);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Set3DOcclusion(global::System.IntPtr channelgroup, float directocclusion, float reverbocclusion);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Get3DOcclusion(global::System.IntPtr channelgroup, out float directocclusion, out float reverbocclusion);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Set3DSpread(global::System.IntPtr channelgroup, float angle);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Get3DSpread(global::System.IntPtr channelgroup, out float angle);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Set3DLevel(global::System.IntPtr channelgroup, float level);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Get3DLevel(global::System.IntPtr channelgroup, out float level);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Set3DDopplerLevel(global::System.IntPtr channelgroup, float level);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Get3DDopplerLevel(global::System.IntPtr channelgroup, out float level);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Set3DDistanceFilter(global::System.IntPtr channelgroup, bool custom, float customLevel, float centerFreq);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_Get3DDistanceFilter(global::System.IntPtr channelgroup, out bool custom, out float customLevel, out float centerFreq);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetSystemObject(global::System.IntPtr channelgroup, out global::System.IntPtr system);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_SetVolume(global::System.IntPtr channelgroup, float volume);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetDSP(global::System.IntPtr channelgroup, int index, out global::System.IntPtr dsp);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_AddDSP(global::System.IntPtr channelgroup, int index, global::System.IntPtr dsp);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_RemoveDSP(global::System.IntPtr channelgroup, global::System.IntPtr dsp);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetNumDSPs(global::System.IntPtr channelgroup, out int numdsps);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_SetDSPIndex(global::System.IntPtr channelgroup, global::System.IntPtr dsp, int index);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetDSPIndex(global::System.IntPtr channelgroup, global::System.IntPtr dsp, out int index);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_OverridePanDSP(global::System.IntPtr channelgroup, global::System.IntPtr pan);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_SetUserData(global::System.IntPtr channelgroup, global::System.IntPtr userdata);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_ChannelGroup_GetUserData(global::System.IntPtr channelgroup, out global::System.IntPtr userdata);
	}
}
