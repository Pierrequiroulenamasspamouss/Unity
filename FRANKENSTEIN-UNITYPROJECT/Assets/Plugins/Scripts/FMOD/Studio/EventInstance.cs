namespace FMOD.Studio
{
	public class EventInstance : global::FMOD.Studio.HandleBase
	{
		public EventInstance(global::System.IntPtr raw)
			: base(raw)
		{
		}

		public global::FMOD.RESULT getDescription(out global::FMOD.Studio.EventDescription description)
		{
			description = null;
			global::System.IntPtr description2;
			global::FMOD.RESULT rESULT = FMOD_Studio_EventInstance_GetDescription(rawPtr, out description2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			description = new global::FMOD.Studio.EventDescription(description2);
			return rESULT;
		}

		public global::FMOD.RESULT getVolume(out float volume)
		{
			return FMOD_Studio_EventInstance_GetVolume(rawPtr, out volume);
		}

		public global::FMOD.RESULT setVolume(float volume)
		{
			return FMOD_Studio_EventInstance_SetVolume(rawPtr, volume);
		}

		public global::FMOD.RESULT getPitch(out float pitch)
		{
			return FMOD_Studio_EventInstance_GetPitch(rawPtr, out pitch);
		}

		public global::FMOD.RESULT setPitch(float pitch)
		{
			return FMOD_Studio_EventInstance_SetPitch(rawPtr, pitch);
		}

		public global::FMOD.RESULT get3DAttributes(out global::FMOD.Studio.ATTRIBUTES_3D attributes)
		{
			return FMOD_Studio_EventInstance_Get3DAttributes(rawPtr, out attributes);
		}

		public global::FMOD.RESULT set3DAttributes(global::FMOD.Studio.ATTRIBUTES_3D attributes)
		{
			return FMOD_Studio_EventInstance_Set3DAttributes(rawPtr, ref attributes);
		}

		public global::FMOD.RESULT getProperty(global::FMOD.Studio.EVENT_PROPERTY index, out float value)
		{
			return FMOD_Studio_EventInstance_GetProperty(rawPtr, index, out value);
		}

		public global::FMOD.RESULT setProperty(global::FMOD.Studio.EVENT_PROPERTY index, float value)
		{
			return FMOD_Studio_EventInstance_SetProperty(rawPtr, index, value);
		}

		public global::FMOD.RESULT getPaused(out bool paused)
		{
			return FMOD_Studio_EventInstance_GetPaused(rawPtr, out paused);
		}

		public global::FMOD.RESULT setPaused(bool paused)
		{
			return FMOD_Studio_EventInstance_SetPaused(rawPtr, paused);
		}

		public global::FMOD.RESULT start()
		{
			return FMOD_Studio_EventInstance_Start(rawPtr);
		}

		public global::FMOD.RESULT stop(global::FMOD.Studio.STOP_MODE mode)
		{
			return FMOD_Studio_EventInstance_Stop(rawPtr, mode);
		}

		public global::FMOD.RESULT getTimelinePosition(out int position)
		{
			return FMOD_Studio_EventInstance_GetTimelinePosition(rawPtr, out position);
		}

		public global::FMOD.RESULT setTimelinePosition(int position)
		{
			return FMOD_Studio_EventInstance_SetTimelinePosition(rawPtr, position);
		}

		public global::FMOD.RESULT getPlaybackState(out global::FMOD.Studio.PLAYBACK_STATE state)
		{
			return FMOD_Studio_EventInstance_GetPlaybackState(rawPtr, out state);
		}

		public global::FMOD.RESULT getChannelGroup(out global::FMOD.ChannelGroup group)
		{
			group = null;
			global::System.IntPtr group2 = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_EventInstance_GetChannelGroup(rawPtr, out group2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			group = new global::FMOD.ChannelGroup(group2);
			return rESULT;
		}

		public global::FMOD.RESULT release()
		{
			return FMOD_Studio_EventInstance_Release(rawPtr);
		}

		public global::FMOD.RESULT isVirtual(out bool virtualState)
		{
			return FMOD_Studio_EventInstance_IsVirtual(rawPtr, out virtualState);
		}

		public global::FMOD.RESULT getParameter(string name, out global::FMOD.Studio.ParameterInstance instance)
		{
			instance = null;
			global::System.IntPtr parameter = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_EventInstance_GetParameter(rawPtr, global::System.Text.Encoding.UTF8.GetBytes(name + '\0'), out parameter);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			instance = new global::FMOD.Studio.ParameterInstance(parameter);
			return rESULT;
		}

		public global::FMOD.RESULT getParameterCount(out int count)
		{
			return FMOD_Studio_EventInstance_GetParameterCount(rawPtr, out count);
		}

		public global::FMOD.RESULT getParameterByIndex(int index, out global::FMOD.Studio.ParameterInstance instance)
		{
			instance = null;
			global::System.IntPtr parameter = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_EventInstance_GetParameterByIndex(rawPtr, index, out parameter);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			instance = new global::FMOD.Studio.ParameterInstance(parameter);
			return rESULT;
		}

		public global::FMOD.RESULT setParameterValue(string name, float value)
		{
			return FMOD_Studio_EventInstance_SetParameterValue(rawPtr, global::System.Text.Encoding.UTF8.GetBytes(name + '\0'), value);
		}

		public global::FMOD.RESULT setParameterValueByIndex(int index, float value)
		{
			return FMOD_Studio_EventInstance_SetParameterValueByIndex(rawPtr, index, value);
		}

		public global::FMOD.RESULT getCue(string name, out global::FMOD.Studio.CueInstance instance)
		{
			instance = null;
			global::System.IntPtr cue = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_EventInstance_GetCue(rawPtr, global::System.Text.Encoding.UTF8.GetBytes(name + '\0'), out cue);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			instance = new global::FMOD.Studio.CueInstance(cue);
			return rESULT;
		}

		public global::FMOD.RESULT getCueByIndex(int index, out global::FMOD.Studio.CueInstance instance)
		{
			instance = null;
			global::System.IntPtr cue = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_EventInstance_GetCueByIndex(rawPtr, index, out cue);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			instance = new global::FMOD.Studio.CueInstance(cue);
			return rESULT;
		}

		public global::FMOD.RESULT getCueCount(out int count)
		{
			return FMOD_Studio_EventInstance_GetCueCount(rawPtr, out count);
		}

		public global::FMOD.RESULT setCallback(global::FMOD.Studio.EVENT_CALLBACK callback, global::FMOD.Studio.EVENT_CALLBACK_TYPE callbackmask = global::FMOD.Studio.EVENT_CALLBACK_TYPE.ALL)
		{
			return FMOD_Studio_EventInstance_SetCallback(rawPtr, callback, callbackmask);
		}

		public global::FMOD.RESULT getUserData(out global::System.IntPtr userData)
		{
			return FMOD_Studio_EventInstance_GetUserData(rawPtr, out userData);
		}

		public global::FMOD.RESULT setUserData(global::System.IntPtr userData)
		{
			return FMOD_Studio_EventInstance_SetUserData(rawPtr, userData);
		}

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern bool FMOD_Studio_EventInstance_IsValid(global::System.IntPtr _event);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_GetDescription(global::System.IntPtr _event, out global::System.IntPtr description);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_GetVolume(global::System.IntPtr _event, out float volume);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_SetVolume(global::System.IntPtr _event, float volume);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_GetPitch(global::System.IntPtr _event, out float pitch);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_SetPitch(global::System.IntPtr _event, float pitch);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_Get3DAttributes(global::System.IntPtr _event, out global::FMOD.Studio.ATTRIBUTES_3D attributes);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_Set3DAttributes(global::System.IntPtr _event, ref global::FMOD.Studio.ATTRIBUTES_3D attributes);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_GetProperty(global::System.IntPtr _event, global::FMOD.Studio.EVENT_PROPERTY index, out float value);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_SetProperty(global::System.IntPtr _event, global::FMOD.Studio.EVENT_PROPERTY index, float value);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_GetPaused(global::System.IntPtr _event, out bool paused);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_SetPaused(global::System.IntPtr _event, bool paused);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_Start(global::System.IntPtr _event);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_Stop(global::System.IntPtr _event, global::FMOD.Studio.STOP_MODE mode);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_GetTimelinePosition(global::System.IntPtr _event, out int position);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_SetTimelinePosition(global::System.IntPtr _event, int position);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_GetPlaybackState(global::System.IntPtr _event, out global::FMOD.Studio.PLAYBACK_STATE state);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_GetChannelGroup(global::System.IntPtr _event, out global::System.IntPtr group);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_Release(global::System.IntPtr _event);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_IsVirtual(global::System.IntPtr _event, out bool virtualState);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_GetParameter(global::System.IntPtr _event, byte[] name, out global::System.IntPtr parameter);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_GetParameterByIndex(global::System.IntPtr _event, int index, out global::System.IntPtr parameter);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_GetParameterCount(global::System.IntPtr _event, out int count);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_SetParameterValue(global::System.IntPtr _event, byte[] name, float value);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_SetParameterValueByIndex(global::System.IntPtr _event, int index, float value);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_GetCue(global::System.IntPtr _event, byte[] name, out global::System.IntPtr cue);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_GetCueByIndex(global::System.IntPtr _event, int index, out global::System.IntPtr cue);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_GetCueCount(global::System.IntPtr _event, out int count);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_SetCallback(global::System.IntPtr _event, global::FMOD.Studio.EVENT_CALLBACK callback, global::FMOD.Studio.EVENT_CALLBACK_TYPE callbackmask);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_GetUserData(global::System.IntPtr _event, out global::System.IntPtr userData);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventInstance_SetUserData(global::System.IntPtr _event, global::System.IntPtr userData);

		protected override bool isValidInternal()
		{
			return FMOD_Studio_EventInstance_IsValid(rawPtr);
		}
	}
}
