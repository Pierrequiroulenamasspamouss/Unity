namespace FMOD
{
	public class System : global::FMOD.HandleBase
	{
		public System(global::System.IntPtr raw)
			: base(raw)
		{
		}

		public global::FMOD.RESULT release()
		{
			global::FMOD.RESULT rESULT = FMOD5_System_Release(rawPtr);
			if (rESULT == global::FMOD.RESULT.OK)
			{
				rawPtr = global::System.IntPtr.Zero;
			}
			return rESULT;
		}

		public global::FMOD.RESULT setOutput(global::FMOD.OUTPUTTYPE output)
		{
			return FMOD5_System_SetOutput(rawPtr, output);
		}

		public global::FMOD.RESULT getOutput(out global::FMOD.OUTPUTTYPE output)
		{
			return FMOD5_System_GetOutput(rawPtr, out output);
		}

		public global::FMOD.RESULT getNumDrivers(out int numdrivers)
		{
			return FMOD5_System_GetNumDrivers(rawPtr, out numdrivers);
		}

		public global::FMOD.RESULT getDriverInfo(int id, global::System.Text.StringBuilder name, int namelen, out global::System.Guid guid, out int systemrate, out global::FMOD.SPEAKERMODE speakermode, out int speakermodechannels)
		{
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(name.Capacity);
			global::FMOD.RESULT result = FMOD5_System_GetDriverInfo(rawPtr, id, intPtr, namelen, out guid, out systemrate, out speakermode, out speakermodechannels);
			global::FMOD.StringMarshalHelper.NativeToBuilder(name, intPtr);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return result;
		}

		public global::FMOD.RESULT setDriver(int driver)
		{
			return FMOD5_System_SetDriver(rawPtr, driver);
		}

		public global::FMOD.RESULT getDriver(out int driver)
		{
			return FMOD5_System_GetDriver(rawPtr, out driver);
		}

		public global::FMOD.RESULT setSoftwareChannels(int numsoftwarechannels)
		{
			return FMOD5_System_SetSoftwareChannels(rawPtr, numsoftwarechannels);
		}

		public global::FMOD.RESULT getSoftwareChannels(out int numsoftwarechannels)
		{
			return FMOD5_System_GetSoftwareChannels(rawPtr, out numsoftwarechannels);
		}

		public global::FMOD.RESULT setSoftwareFormat(int samplerate, global::FMOD.SPEAKERMODE speakermode, int numrawspeakers)
		{
			return FMOD5_System_SetSoftwareFormat(rawPtr, samplerate, speakermode, numrawspeakers);
		}

		public global::FMOD.RESULT getSoftwareFormat(out int samplerate, out global::FMOD.SPEAKERMODE speakermode, out int numrawspeakers)
		{
			return FMOD5_System_GetSoftwareFormat(rawPtr, out samplerate, out speakermode, out numrawspeakers);
		}

		public global::FMOD.RESULT setDSPBufferSize(uint bufferlength, int numbuffers)
		{
			return FMOD5_System_SetDSPBufferSize(rawPtr, bufferlength, numbuffers);
		}

		public global::FMOD.RESULT getDSPBufferSize(out uint bufferlength, out int numbuffers)
		{
			return FMOD5_System_GetDSPBufferSize(rawPtr, out bufferlength, out numbuffers);
		}

		public global::FMOD.RESULT setFileSystem(global::FMOD.FILE_OPENCALLBACK useropen, global::FMOD.FILE_CLOSECALLBACK userclose, global::FMOD.FILE_READCALLBACK userread, global::FMOD.FILE_SEEKCALLBACK userseek, global::FMOD.FILE_ASYNCREADCALLBACK userasyncread, global::FMOD.FILE_ASYNCCANCELCALLBACK userasynccancel, int blockalign)
		{
			return FMOD5_System_SetFileSystem(rawPtr, useropen, userclose, userread, userseek, userasyncread, userasynccancel, blockalign);
		}

		public global::FMOD.RESULT attachFileSystem(global::FMOD.FILE_OPENCALLBACK useropen, global::FMOD.FILE_CLOSECALLBACK userclose, global::FMOD.FILE_READCALLBACK userread, global::FMOD.FILE_SEEKCALLBACK userseek)
		{
			return FMOD5_System_AttachFileSystem(rawPtr, useropen, userclose, userread, userseek);
		}

		public global::FMOD.RESULT setAdvancedSettings(ref global::FMOD.ADVANCEDSETTINGS settings)
		{
			settings.cbSize = global::System.Runtime.InteropServices.Marshal.SizeOf(settings);
			return FMOD5_System_SetAdvancedSettings(rawPtr, ref settings);
		}

		public global::FMOD.RESULT getAdvancedSettings(ref global::FMOD.ADVANCEDSETTINGS settings)
		{
			settings.cbSize = global::System.Runtime.InteropServices.Marshal.SizeOf(settings);
			return FMOD5_System_GetAdvancedSettings(rawPtr, ref settings);
		}

		public global::FMOD.RESULT setCallback(global::FMOD.SYSTEM_CALLBACK callback, global::FMOD.SYSTEM_CALLBACK_TYPE callbackmask)
		{
			return FMOD5_System_SetCallback(rawPtr, callback, callbackmask);
		}

		public global::FMOD.RESULT setPluginPath(string path)
		{
			return FMOD5_System_SetPluginPath(rawPtr, global::System.Text.Encoding.UTF8.GetBytes(path + '\0'));
		}

		public global::FMOD.RESULT loadPlugin(string filename, out uint handle, uint priority)
		{
			return FMOD5_System_LoadPlugin(rawPtr, global::System.Text.Encoding.UTF8.GetBytes(filename + '\0'), out handle, priority);
		}

		public global::FMOD.RESULT loadPlugin(string filename, out uint handle)
		{
			return loadPlugin(filename, out handle, 0u);
		}

		public global::FMOD.RESULT unloadPlugin(uint handle)
		{
			return FMOD5_System_UnloadPlugin(rawPtr, handle);
		}

		public global::FMOD.RESULT getNumPlugins(global::FMOD.PLUGINTYPE plugintype, out int numplugins)
		{
			return FMOD5_System_GetNumPlugins(rawPtr, plugintype, out numplugins);
		}

		public global::FMOD.RESULT getPluginHandle(global::FMOD.PLUGINTYPE plugintype, int index, out uint handle)
		{
			return FMOD5_System_GetPluginHandle(rawPtr, plugintype, index, out handle);
		}

		public global::FMOD.RESULT getPluginInfo(uint handle, out global::FMOD.PLUGINTYPE plugintype, global::System.Text.StringBuilder name, int namelen, out uint version)
		{
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(name.Capacity);
			global::FMOD.RESULT result = FMOD5_System_GetPluginInfo(rawPtr, handle, out plugintype, intPtr, namelen, out version);
			global::FMOD.StringMarshalHelper.NativeToBuilder(name, intPtr);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return result;
		}

		public global::FMOD.RESULT setOutputByPlugin(uint handle)
		{
			return FMOD5_System_SetOutputByPlugin(rawPtr, handle);
		}

		public global::FMOD.RESULT getOutputByPlugin(out uint handle)
		{
			return FMOD5_System_GetOutputByPlugin(rawPtr, out handle);
		}

		public global::FMOD.RESULT createDSPByPlugin(uint handle, out global::FMOD.DSP dsp)
		{
			dsp = null;
			global::System.IntPtr dsp2;
			global::FMOD.RESULT result = FMOD5_System_CreateDSPByPlugin(rawPtr, handle, out dsp2);
			dsp = new global::FMOD.DSP(dsp2);
			return result;
		}

		public global::FMOD.RESULT getDSPInfoByPlugin(uint handle, out global::System.IntPtr description)
		{
			return FMOD5_System_GetDSPInfoByPlugin(rawPtr, handle, out description);
		}

		public global::FMOD.RESULT registerDSP(ref global::FMOD.DSP_DESCRIPTION description, out uint handle)
		{
			return FMOD5_System_RegisterDSP(rawPtr, ref description, out handle);
		}

		public global::FMOD.RESULT init(int maxchannels, global::FMOD.INITFLAGS flags, global::System.IntPtr extradriverdata)
		{
			return FMOD5_System_Init(rawPtr, maxchannels, flags, extradriverdata);
		}

		public global::FMOD.RESULT close()
		{
			return FMOD5_System_Close(rawPtr);
		}

		public global::FMOD.RESULT update()
		{
			return FMOD5_System_Update(rawPtr);
		}

		public global::FMOD.RESULT setSpeakerPosition(global::FMOD.SPEAKER speaker, float x, float y, bool active)
		{
			return FMOD5_System_SetSpeakerPosition(rawPtr, speaker, x, y, active);
		}

		public global::FMOD.RESULT getSpeakerPosition(global::FMOD.SPEAKER speaker, out float x, out float y, out bool active)
		{
			return FMOD5_System_GetSpeakerPosition(rawPtr, speaker, out x, out y, out active);
		}

		public global::FMOD.RESULT setStreamBufferSize(uint filebuffersize, global::FMOD.TIMEUNIT filebuffersizetype)
		{
			return FMOD5_System_SetStreamBufferSize(rawPtr, filebuffersize, filebuffersizetype);
		}

		public global::FMOD.RESULT getStreamBufferSize(out uint filebuffersize, out global::FMOD.TIMEUNIT filebuffersizetype)
		{
			return FMOD5_System_GetStreamBufferSize(rawPtr, out filebuffersize, out filebuffersizetype);
		}

		public global::FMOD.RESULT set3DSettings(float dopplerscale, float distancefactor, float rolloffscale)
		{
			return FMOD5_System_Set3DSettings(rawPtr, dopplerscale, distancefactor, rolloffscale);
		}

		public global::FMOD.RESULT get3DSettings(out float dopplerscale, out float distancefactor, out float rolloffscale)
		{
			return FMOD5_System_Get3DSettings(rawPtr, out dopplerscale, out distancefactor, out rolloffscale);
		}

		public global::FMOD.RESULT set3DNumListeners(int numlisteners)
		{
			return FMOD5_System_Set3DNumListeners(rawPtr, numlisteners);
		}

		public global::FMOD.RESULT get3DNumListeners(out int numlisteners)
		{
			return FMOD5_System_Get3DNumListeners(rawPtr, out numlisteners);
		}

		public global::FMOD.RESULT set3DListenerAttributes(int listener, ref global::FMOD.VECTOR pos, ref global::FMOD.VECTOR vel, ref global::FMOD.VECTOR forward, ref global::FMOD.VECTOR up)
		{
			return FMOD5_System_Set3DListenerAttributes(rawPtr, listener, ref pos, ref vel, ref forward, ref up);
		}

		public global::FMOD.RESULT get3DListenerAttributes(int listener, out global::FMOD.VECTOR pos, out global::FMOD.VECTOR vel, out global::FMOD.VECTOR forward, out global::FMOD.VECTOR up)
		{
			return FMOD5_System_Get3DListenerAttributes(rawPtr, listener, out pos, out vel, out forward, out up);
		}

		public global::FMOD.RESULT set3DRolloffCallback(global::FMOD.CB_3D_ROLLOFFCALLBACK callback)
		{
			return FMOD5_System_Set3DRolloffCallback(rawPtr, callback);
		}

		public global::FMOD.RESULT mixerSuspend()
		{
			return FMOD5_System_MixerSuspend(rawPtr);
		}

		public global::FMOD.RESULT mixerResume()
		{
			return FMOD5_System_MixerResume(rawPtr);
		}

		public global::FMOD.RESULT getDefaultMixMatrix(global::FMOD.SPEAKERMODE sourcespeakermode, global::FMOD.SPEAKERMODE targetspeakermode, float[] matrix, int matrixhop)
		{
			return FMOD5_System_GetDefaultMixMatrix(rawPtr, sourcespeakermode, targetspeakermode, matrix, matrixhop);
		}

		public global::FMOD.RESULT getSpeakerModeChannels(global::FMOD.SPEAKERMODE mode, out int channels)
		{
			return FMOD5_System_GetSpeakerModeChannels(rawPtr, mode, out channels);
		}

		public global::FMOD.RESULT getVersion(out uint version)
		{
			return FMOD5_System_GetVersion(rawPtr, out version);
		}

		public global::FMOD.RESULT getOutputHandle(out global::System.IntPtr handle)
		{
			return FMOD5_System_GetOutputHandle(rawPtr, out handle);
		}

		public global::FMOD.RESULT getChannelsPlaying(out int channels)
		{
			return FMOD5_System_GetChannelsPlaying(rawPtr, out channels);
		}

		public global::FMOD.RESULT getCPUUsage(out float dsp, out float stream, out float geometry, out float update, out float total)
		{
			return FMOD5_System_GetCPUUsage(rawPtr, out dsp, out stream, out geometry, out update, out total);
		}

		public global::FMOD.RESULT getSoundRAM(out int currentalloced, out int maxalloced, out int total)
		{
			return FMOD5_System_GetSoundRAM(rawPtr, out currentalloced, out maxalloced, out total);
		}

		public global::FMOD.RESULT createSound(string name, global::FMOD.MODE mode, ref global::FMOD.CREATESOUNDEXINFO exinfo, out global::FMOD.Sound sound)
		{
			sound = null;
			byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(name + '\0');
			exinfo.cbsize = global::System.Runtime.InteropServices.Marshal.SizeOf(exinfo);
			global::System.IntPtr sound2;
			global::FMOD.RESULT result = FMOD5_System_CreateSound(rawPtr, bytes, mode, ref exinfo, out sound2);
			sound = new global::FMOD.Sound(sound2);
			return result;
		}

		public global::FMOD.RESULT createSound(byte[] data, global::FMOD.MODE mode, ref global::FMOD.CREATESOUNDEXINFO exinfo, out global::FMOD.Sound sound)
		{
			sound = null;
			exinfo.cbsize = global::System.Runtime.InteropServices.Marshal.SizeOf(exinfo);
			global::System.IntPtr sound2;
			global::FMOD.RESULT result = FMOD5_System_CreateSound(rawPtr, data, mode, ref exinfo, out sound2);
			sound = new global::FMOD.Sound(sound2);
			return result;
		}

		public global::FMOD.RESULT createSound(string name, global::FMOD.MODE mode, out global::FMOD.Sound sound)
		{
			global::FMOD.CREATESOUNDEXINFO exinfo = default(global::FMOD.CREATESOUNDEXINFO);
			exinfo.cbsize = global::System.Runtime.InteropServices.Marshal.SizeOf(exinfo);
			return createSound(name, mode, ref exinfo, out sound);
		}

		public global::FMOD.RESULT createStream(string name, global::FMOD.MODE mode, ref global::FMOD.CREATESOUNDEXINFO exinfo, out global::FMOD.Sound sound)
		{
			sound = null;
			byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(name + '\0');
			exinfo.cbsize = global::System.Runtime.InteropServices.Marshal.SizeOf(exinfo);
			global::System.IntPtr sound2;
			global::FMOD.RESULT result = FMOD5_System_CreateStream(rawPtr, bytes, mode, ref exinfo, out sound2);
			sound = new global::FMOD.Sound(sound2);
			return result;
		}

		public global::FMOD.RESULT createStream(byte[] data, global::FMOD.MODE mode, ref global::FMOD.CREATESOUNDEXINFO exinfo, out global::FMOD.Sound sound)
		{
			sound = null;
			exinfo.cbsize = global::System.Runtime.InteropServices.Marshal.SizeOf(exinfo);
			global::System.IntPtr sound2;
			global::FMOD.RESULT result = FMOD5_System_CreateStream(rawPtr, data, mode, ref exinfo, out sound2);
			sound = new global::FMOD.Sound(sound2);
			return result;
		}

		public global::FMOD.RESULT createStream(string name, global::FMOD.MODE mode, out global::FMOD.Sound sound)
		{
			global::FMOD.CREATESOUNDEXINFO exinfo = default(global::FMOD.CREATESOUNDEXINFO);
			exinfo.cbsize = global::System.Runtime.InteropServices.Marshal.SizeOf(exinfo);
			return createStream(name, mode, ref exinfo, out sound);
		}

		public global::FMOD.RESULT createDSP(ref global::FMOD.DSP_DESCRIPTION description, out global::FMOD.DSP dsp)
		{
			dsp = null;
			global::System.IntPtr dsp2;
			global::FMOD.RESULT result = FMOD5_System_CreateDSP(rawPtr, ref description, out dsp2);
			dsp = new global::FMOD.DSP(dsp2);
			return result;
		}

		public global::FMOD.RESULT createDSPByType(global::FMOD.DSP_TYPE type, out global::FMOD.DSP dsp)
		{
			dsp = null;
			global::System.IntPtr dsp2;
			global::FMOD.RESULT result = FMOD5_System_CreateDSPByType(rawPtr, type, out dsp2);
			dsp = new global::FMOD.DSP(dsp2);
			return result;
		}

		public global::FMOD.RESULT createChannelGroup(string name, out global::FMOD.ChannelGroup channelgroup)
		{
			channelgroup = null;
			byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(name + '\0');
			global::System.IntPtr channelgroup2;
			global::FMOD.RESULT result = FMOD5_System_CreateChannelGroup(rawPtr, bytes, out channelgroup2);
			channelgroup = new global::FMOD.ChannelGroup(channelgroup2);
			return result;
		}

		public global::FMOD.RESULT createSoundGroup(string name, out global::FMOD.SoundGroup soundgroup)
		{
			soundgroup = null;
			byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(name + '\0');
			global::System.IntPtr soundgroup2;
			global::FMOD.RESULT result = FMOD5_System_CreateSoundGroup(rawPtr, bytes, out soundgroup2);
			soundgroup = new global::FMOD.SoundGroup(soundgroup2);
			return result;
		}

		public global::FMOD.RESULT createReverb3D(out global::FMOD.Reverb3D reverb)
		{
			global::System.IntPtr reverb2;
			global::FMOD.RESULT result = FMOD5_System_CreateReverb3D(rawPtr, out reverb2);
			reverb = new global::FMOD.Reverb3D(reverb2);
			return result;
		}

		public global::FMOD.RESULT playSound(global::FMOD.Sound sound, global::FMOD.ChannelGroup channelGroup, bool paused, out global::FMOD.Channel channel)
		{
			channel = null;
			global::System.IntPtr channelGroup2 = ((!(channelGroup != null)) ? global::System.IntPtr.Zero : channelGroup.getRaw());
			global::System.IntPtr channel2;
			global::FMOD.RESULT result = FMOD5_System_PlaySound(rawPtr, sound.getRaw(), channelGroup2, paused, out channel2);
			channel = new global::FMOD.Channel(channel2);
			return result;
		}

		public global::FMOD.RESULT playDSP(global::FMOD.DSP dsp, global::FMOD.ChannelGroup channelGroup, bool paused, out global::FMOD.Channel channel)
		{
			channel = null;
			global::System.IntPtr channelGroup2 = ((!(channelGroup != null)) ? global::System.IntPtr.Zero : channelGroup.getRaw());
			global::System.IntPtr channel2;
			global::FMOD.RESULT result = FMOD5_System_PlayDSP(rawPtr, dsp.getRaw(), channelGroup2, paused, out channel2);
			channel = new global::FMOD.Channel(channel2);
			return result;
		}

		public global::FMOD.RESULT getChannel(int channelid, out global::FMOD.Channel channel)
		{
			channel = null;
			global::System.IntPtr channel2;
			global::FMOD.RESULT result = FMOD5_System_GetChannel(rawPtr, channelid, out channel2);
			channel = new global::FMOD.Channel(channel2);
			return result;
		}

		public global::FMOD.RESULT getMasterChannelGroup(out global::FMOD.ChannelGroup channelgroup)
		{
			channelgroup = null;
			global::System.IntPtr channelgroup2;
			global::FMOD.RESULT result = FMOD5_System_GetMasterChannelGroup(rawPtr, out channelgroup2);
			channelgroup = new global::FMOD.ChannelGroup(channelgroup2);
			return result;
		}

		public global::FMOD.RESULT getMasterSoundGroup(out global::FMOD.SoundGroup soundgroup)
		{
			soundgroup = null;
			global::System.IntPtr soundgroup2;
			global::FMOD.RESULT result = FMOD5_System_GetMasterSoundGroup(rawPtr, out soundgroup2);
			soundgroup = new global::FMOD.SoundGroup(soundgroup2);
			return result;
		}

		public global::FMOD.RESULT attachChannelGroupToPort(uint portType, ulong portIndex, global::FMOD.ChannelGroup channelgroup, bool passThru = false)
		{
			return FMOD5_System_AttachChannelGroupToPort(rawPtr, portType, portIndex, channelgroup.getRaw(), passThru);
		}

		public global::FMOD.RESULT detachChannelGroupFromPort(global::FMOD.ChannelGroup channelgroup)
		{
			return FMOD5_System_DetachChannelGroupFromPort(rawPtr, channelgroup.getRaw());
		}

		public global::FMOD.RESULT setReverbProperties(int instance, ref global::FMOD.REVERB_PROPERTIES prop)
		{
			return FMOD5_System_SetReverbProperties(rawPtr, instance, ref prop);
		}

		public global::FMOD.RESULT getReverbProperties(int instance, out global::FMOD.REVERB_PROPERTIES prop)
		{
			return FMOD5_System_GetReverbProperties(rawPtr, instance, out prop);
		}

		public global::FMOD.RESULT lockDSP()
		{
			return FMOD5_System_LockDSP(rawPtr);
		}

		public global::FMOD.RESULT unlockDSP()
		{
			return FMOD5_System_UnlockDSP(rawPtr);
		}

		public global::FMOD.RESULT getRecordNumDrivers(out int numdrivers, out int numconnected)
		{
			return FMOD5_System_GetRecordNumDrivers(rawPtr, out numdrivers, out numconnected);
		}

		public global::FMOD.RESULT getRecordDriverInfo(int id, global::System.Text.StringBuilder name, int namelen, out global::System.Guid guid, out int systemrate, out global::FMOD.SPEAKERMODE speakermode, out int speakermodechannels, out global::FMOD.DRIVER_STATE state)
		{
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(name.Capacity);
			global::FMOD.RESULT result = FMOD5_System_GetRecordDriverInfo(rawPtr, id, intPtr, namelen, out guid, out systemrate, out speakermode, out speakermodechannels, out state);
			global::FMOD.StringMarshalHelper.NativeToBuilder(name, intPtr);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return result;
		}

		public global::FMOD.RESULT getRecordPosition(int id, out uint position)
		{
			return FMOD5_System_GetRecordPosition(rawPtr, id, out position);
		}

		public global::FMOD.RESULT recordStart(int id, global::FMOD.Sound sound, bool loop)
		{
			return FMOD5_System_RecordStart(rawPtr, id, sound.getRaw(), loop);
		}

		public global::FMOD.RESULT recordStop(int id)
		{
			return FMOD5_System_RecordStop(rawPtr, id);
		}

		public global::FMOD.RESULT isRecording(int id, out bool recording)
		{
			return FMOD5_System_IsRecording(rawPtr, id, out recording);
		}

		public global::FMOD.RESULT createGeometry(int maxpolygons, int maxvertices, out global::FMOD.Geometry geometry)
		{
			geometry = null;
			global::System.IntPtr geometry2;
			global::FMOD.RESULT result = FMOD5_System_CreateGeometry(rawPtr, maxpolygons, maxvertices, out geometry2);
			geometry = new global::FMOD.Geometry(geometry2);
			return result;
		}

		public global::FMOD.RESULT setGeometrySettings(float maxworldsize)
		{
			return FMOD5_System_SetGeometrySettings(rawPtr, maxworldsize);
		}

		public global::FMOD.RESULT getGeometrySettings(out float maxworldsize)
		{
			return FMOD5_System_GetGeometrySettings(rawPtr, out maxworldsize);
		}

		public global::FMOD.RESULT loadGeometry(global::System.IntPtr data, int datasize, out global::FMOD.Geometry geometry)
		{
			geometry = null;
			global::System.IntPtr geometry2;
			global::FMOD.RESULT result = FMOD5_System_LoadGeometry(rawPtr, data, datasize, out geometry2);
			geometry = new global::FMOD.Geometry(geometry2);
			return result;
		}

		public global::FMOD.RESULT getGeometryOcclusion(ref global::FMOD.VECTOR listener, ref global::FMOD.VECTOR source, out float direct, out float reverb)
		{
			return FMOD5_System_GetGeometryOcclusion(rawPtr, ref listener, ref source, out direct, out reverb);
		}

		public global::FMOD.RESULT setNetworkProxy(string proxy)
		{
			return FMOD5_System_SetNetworkProxy(rawPtr, global::System.Text.Encoding.UTF8.GetBytes(proxy + '\0'));
		}

		public global::FMOD.RESULT getNetworkProxy(global::System.Text.StringBuilder proxy, int proxylen)
		{
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(proxy.Capacity);
			global::FMOD.RESULT result = FMOD5_System_GetNetworkProxy(rawPtr, intPtr, proxylen);
			global::FMOD.StringMarshalHelper.NativeToBuilder(proxy, intPtr);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return result;
		}

		public global::FMOD.RESULT setNetworkTimeout(int timeout)
		{
			return FMOD5_System_SetNetworkTimeout(rawPtr, timeout);
		}

		public global::FMOD.RESULT getNetworkTimeout(out int timeout)
		{
			return FMOD5_System_GetNetworkTimeout(rawPtr, out timeout);
		}

		public global::FMOD.RESULT setUserData(global::System.IntPtr userdata)
		{
			return FMOD5_System_SetUserData(rawPtr, userdata);
		}

		public global::FMOD.RESULT getUserData(out global::System.IntPtr userdata)
		{
			return FMOD5_System_GetUserData(rawPtr, out userdata);
		}

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_Release(global::System.IntPtr system);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_SetOutput(global::System.IntPtr system, global::FMOD.OUTPUTTYPE output);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetOutput(global::System.IntPtr system, out global::FMOD.OUTPUTTYPE output);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetNumDrivers(global::System.IntPtr system, out int numdrivers);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetDriverInfo(global::System.IntPtr system, int id, global::System.IntPtr name, int namelen, out global::System.Guid guid, out int systemrate, out global::FMOD.SPEAKERMODE speakermode, out int speakermodechannels);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_SetDriver(global::System.IntPtr system, int driver);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetDriver(global::System.IntPtr system, out int driver);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_SetSoftwareChannels(global::System.IntPtr system, int numsoftwarechannels);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetSoftwareChannels(global::System.IntPtr system, out int numsoftwarechannels);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_SetSoftwareFormat(global::System.IntPtr system, int samplerate, global::FMOD.SPEAKERMODE speakermode, int numrawspeakers);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetSoftwareFormat(global::System.IntPtr system, out int samplerate, out global::FMOD.SPEAKERMODE speakermode, out int numrawspeakers);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_SetDSPBufferSize(global::System.IntPtr system, uint bufferlength, int numbuffers);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetDSPBufferSize(global::System.IntPtr system, out uint bufferlength, out int numbuffers);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_SetFileSystem(global::System.IntPtr system, global::FMOD.FILE_OPENCALLBACK useropen, global::FMOD.FILE_CLOSECALLBACK userclose, global::FMOD.FILE_READCALLBACK userread, global::FMOD.FILE_SEEKCALLBACK userseek, global::FMOD.FILE_ASYNCREADCALLBACK userasyncread, global::FMOD.FILE_ASYNCCANCELCALLBACK userasynccancel, int blockalign);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_AttachFileSystem(global::System.IntPtr system, global::FMOD.FILE_OPENCALLBACK useropen, global::FMOD.FILE_CLOSECALLBACK userclose, global::FMOD.FILE_READCALLBACK userread, global::FMOD.FILE_SEEKCALLBACK userseek);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_SetPluginPath(global::System.IntPtr system, byte[] path);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_LoadPlugin(global::System.IntPtr system, byte[] filename, out uint handle, uint priority);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_UnloadPlugin(global::System.IntPtr system, uint handle);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetNumPlugins(global::System.IntPtr system, global::FMOD.PLUGINTYPE plugintype, out int numplugins);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetPluginHandle(global::System.IntPtr system, global::FMOD.PLUGINTYPE plugintype, int index, out uint handle);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetPluginInfo(global::System.IntPtr system, uint handle, out global::FMOD.PLUGINTYPE plugintype, global::System.IntPtr name, int namelen, out uint version);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_CreateDSPByPlugin(global::System.IntPtr system, uint handle, out global::System.IntPtr dsp);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_SetOutputByPlugin(global::System.IntPtr system, uint handle);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetOutputByPlugin(global::System.IntPtr system, out uint handle);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetDSPInfoByPlugin(global::System.IntPtr system, uint handle, out global::System.IntPtr description);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_RegisterDSP(global::System.IntPtr system, ref global::FMOD.DSP_DESCRIPTION description, out uint handle);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_Init(global::System.IntPtr system, int maxchannels, global::FMOD.INITFLAGS flags, global::System.IntPtr extradriverdata);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_Close(global::System.IntPtr system);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_Update(global::System.IntPtr system);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_SetAdvancedSettings(global::System.IntPtr system, ref global::FMOD.ADVANCEDSETTINGS settings);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetAdvancedSettings(global::System.IntPtr system, ref global::FMOD.ADVANCEDSETTINGS settings);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_Set3DRolloffCallback(global::System.IntPtr system, global::FMOD.CB_3D_ROLLOFFCALLBACK callback);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_MixerSuspend(global::System.IntPtr system);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_MixerResume(global::System.IntPtr system);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetDefaultMixMatrix(global::System.IntPtr system, global::FMOD.SPEAKERMODE sourcespeakermode, global::FMOD.SPEAKERMODE targetspeakermode, float[] matrix, int matrixhop);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetSpeakerModeChannels(global::System.IntPtr system, global::FMOD.SPEAKERMODE mode, out int channels);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_SetCallback(global::System.IntPtr system, global::FMOD.SYSTEM_CALLBACK callback, global::FMOD.SYSTEM_CALLBACK_TYPE callbackmask);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_SetSpeakerPosition(global::System.IntPtr system, global::FMOD.SPEAKER speaker, float x, float y, bool active);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetSpeakerPosition(global::System.IntPtr system, global::FMOD.SPEAKER speaker, out float x, out float y, out bool active);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_Set3DSettings(global::System.IntPtr system, float dopplerscale, float distancefactor, float rolloffscale);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_Get3DSettings(global::System.IntPtr system, out float dopplerscale, out float distancefactor, out float rolloffscale);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_Set3DNumListeners(global::System.IntPtr system, int numlisteners);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_Get3DNumListeners(global::System.IntPtr system, out int numlisteners);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_Set3DListenerAttributes(global::System.IntPtr system, int listener, ref global::FMOD.VECTOR pos, ref global::FMOD.VECTOR vel, ref global::FMOD.VECTOR forward, ref global::FMOD.VECTOR up);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_Get3DListenerAttributes(global::System.IntPtr system, int listener, out global::FMOD.VECTOR pos, out global::FMOD.VECTOR vel, out global::FMOD.VECTOR forward, out global::FMOD.VECTOR up);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_SetStreamBufferSize(global::System.IntPtr system, uint filebuffersize, global::FMOD.TIMEUNIT filebuffersizetype);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetStreamBufferSize(global::System.IntPtr system, out uint filebuffersize, out global::FMOD.TIMEUNIT filebuffersizetype);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetVersion(global::System.IntPtr system, out uint version);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetOutputHandle(global::System.IntPtr system, out global::System.IntPtr handle);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetChannelsPlaying(global::System.IntPtr system, out int channels);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetCPUUsage(global::System.IntPtr system, out float dsp, out float stream, out float geometry, out float update, out float total);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetSoundRAM(global::System.IntPtr system, out int currentalloced, out int maxalloced, out int total);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_CreateSound(global::System.IntPtr system, byte[] name_or_data, global::FMOD.MODE mode, ref global::FMOD.CREATESOUNDEXINFO exinfo, out global::System.IntPtr sound);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_CreateStream(global::System.IntPtr system, byte[] name_or_data, global::FMOD.MODE mode, ref global::FMOD.CREATESOUNDEXINFO exinfo, out global::System.IntPtr sound);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_CreateDSP(global::System.IntPtr system, ref global::FMOD.DSP_DESCRIPTION description, out global::System.IntPtr dsp);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_CreateDSPByType(global::System.IntPtr system, global::FMOD.DSP_TYPE type, out global::System.IntPtr dsp);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_CreateChannelGroup(global::System.IntPtr system, byte[] name, out global::System.IntPtr channelgroup);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_CreateSoundGroup(global::System.IntPtr system, byte[] name, out global::System.IntPtr soundgroup);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_CreateReverb3D(global::System.IntPtr system, out global::System.IntPtr reverb);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_PlaySound(global::System.IntPtr system, global::System.IntPtr sound, global::System.IntPtr channelGroup, bool paused, out global::System.IntPtr channel);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_PlayDSP(global::System.IntPtr system, global::System.IntPtr dsp, global::System.IntPtr channelGroup, bool paused, out global::System.IntPtr channel);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetChannel(global::System.IntPtr system, int channelid, out global::System.IntPtr channel);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetMasterChannelGroup(global::System.IntPtr system, out global::System.IntPtr channelgroup);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetMasterSoundGroup(global::System.IntPtr system, out global::System.IntPtr soundgroup);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_AttachChannelGroupToPort(global::System.IntPtr system, uint portType, ulong portIndex, global::System.IntPtr channelgroup, bool passThru);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_DetachChannelGroupFromPort(global::System.IntPtr system, global::System.IntPtr channelgroup);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_SetReverbProperties(global::System.IntPtr system, int instance, ref global::FMOD.REVERB_PROPERTIES prop);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetReverbProperties(global::System.IntPtr system, int instance, out global::FMOD.REVERB_PROPERTIES prop);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_LockDSP(global::System.IntPtr system);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_UnlockDSP(global::System.IntPtr system);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetRecordNumDrivers(global::System.IntPtr system, out int numdrivers, out int numconnected);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetRecordDriverInfo(global::System.IntPtr system, int id, global::System.IntPtr name, int namelen, out global::System.Guid guid, out int systemrate, out global::FMOD.SPEAKERMODE speakermode, out int speakermodechannels, out global::FMOD.DRIVER_STATE state);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetRecordPosition(global::System.IntPtr system, int id, out uint position);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_RecordStart(global::System.IntPtr system, int id, global::System.IntPtr sound, bool loop);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_RecordStop(global::System.IntPtr system, int id);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_IsRecording(global::System.IntPtr system, int id, out bool recording);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_CreateGeometry(global::System.IntPtr system, int maxpolygons, int maxvertices, out global::System.IntPtr geometry);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_SetGeometrySettings(global::System.IntPtr system, float maxworldsize);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetGeometrySettings(global::System.IntPtr system, out float maxworldsize);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_LoadGeometry(global::System.IntPtr system, global::System.IntPtr data, int datasize, out global::System.IntPtr geometry);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetGeometryOcclusion(global::System.IntPtr system, ref global::FMOD.VECTOR listener, ref global::FMOD.VECTOR source, out float direct, out float reverb);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_SetNetworkProxy(global::System.IntPtr system, byte[] proxy);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetNetworkProxy(global::System.IntPtr system, global::System.IntPtr proxy, int proxylen);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_SetNetworkTimeout(global::System.IntPtr system, int timeout);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetNetworkTimeout(global::System.IntPtr system, out int timeout);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_SetUserData(global::System.IntPtr system, global::System.IntPtr userdata);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_GetUserData(global::System.IntPtr system, out global::System.IntPtr userdata);
	}
}
