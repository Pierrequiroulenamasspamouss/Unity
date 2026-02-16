namespace FMOD.Studio
{
	public class System : global::FMOD.Studio.HandleBase
	{
		public System(global::System.IntPtr raw)
			: base(raw)
		{
		}

		public static global::FMOD.RESULT create(out global::FMOD.Studio.System studiosystem)
		{
			global::FMOD.RESULT rESULT = global::FMOD.RESULT.OK;
			studiosystem = null;
			global::System.IntPtr studiosystem2;
			rESULT = FMOD_Studio_System_Create(out studiosystem2, 67075u);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			studiosystem = new global::FMOD.Studio.System(studiosystem2);
			return rESULT;
		}

		public global::FMOD.RESULT setAdvancedSettings(global::FMOD.Studio.ADVANCEDSETTINGS settings)
		{
			settings.cbSize = global::System.Runtime.InteropServices.Marshal.SizeOf(typeof(global::FMOD.Studio.ADVANCEDSETTINGS));
			return FMOD_Studio_System_SetAdvancedSettings(rawPtr, ref settings);
		}

		public global::FMOD.RESULT getAdvancedSettings(out global::FMOD.Studio.ADVANCEDSETTINGS settings)
		{
			settings.cbSize = global::System.Runtime.InteropServices.Marshal.SizeOf(typeof(global::FMOD.Studio.ADVANCEDSETTINGS));
			return FMOD_Studio_System_GetAdvancedSettings(rawPtr, out settings);
		}

		public global::FMOD.RESULT initialize(int maxchannels, global::FMOD.Studio.INITFLAGS studioFlags, global::FMOD.INITFLAGS flags, global::System.IntPtr extradriverdata)
		{
			return FMOD_Studio_System_Initialize(rawPtr, maxchannels, studioFlags, flags, extradriverdata);
		}

		public global::FMOD.RESULT release()
		{
			return FMOD_Studio_System_Release(rawPtr);
		}

		public global::FMOD.RESULT update()
		{
			return FMOD_Studio_System_Update(rawPtr);
		}

		public global::FMOD.RESULT getLowLevelSystem(out global::FMOD.System system)
		{
			system = null;
			global::System.IntPtr system2 = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_System_GetLowLevelSystem(rawPtr, out system2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			system = new global::FMOD.System(system2);
			return rESULT;
		}

		public global::FMOD.RESULT getEvent(string path, out global::FMOD.Studio.EventDescription _event)
		{
			_event = null;
			global::System.IntPtr description = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_System_GetEvent(rawPtr, global::System.Text.Encoding.UTF8.GetBytes(path + '\0'), out description);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			_event = new global::FMOD.Studio.EventDescription(description);
			return rESULT;
		}

		public global::FMOD.RESULT getBus(string path, out global::FMOD.Studio.Bus bus)
		{
			bus = null;
			global::System.IntPtr bus2 = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_System_GetBus(rawPtr, global::System.Text.Encoding.UTF8.GetBytes(path + '\0'), out bus2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			bus = new global::FMOD.Studio.Bus(bus2);
			return rESULT;
		}

		public global::FMOD.RESULT getVCA(string path, out global::FMOD.Studio.VCA vca)
		{
			vca = null;
			global::System.IntPtr vca2 = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_System_GetVCA(rawPtr, global::System.Text.Encoding.UTF8.GetBytes(path + '\0'), out vca2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			vca = new global::FMOD.Studio.VCA(vca2);
			return rESULT;
		}

		public global::FMOD.RESULT getBank(string path, out global::FMOD.Studio.Bank bank)
		{
			bank = null;
			global::System.IntPtr bank2 = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_System_GetBank(rawPtr, global::System.Text.Encoding.UTF8.GetBytes(path + '\0'), out bank2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			bank = new global::FMOD.Studio.Bank(bank2);
			return rESULT;
		}

		public global::FMOD.RESULT getEventByID(global::System.Guid guid, out global::FMOD.Studio.EventDescription _event)
		{
			_event = null;
			global::System.IntPtr description = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_System_GetEventByID(rawPtr, ref guid, out description);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			_event = new global::FMOD.Studio.EventDescription(description);
			return rESULT;
		}

		public global::FMOD.RESULT getBusByID(global::System.Guid guid, out global::FMOD.Studio.Bus bus)
		{
			bus = null;
			global::System.IntPtr bus2 = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_System_GetBusByID(rawPtr, ref guid, out bus2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			bus = new global::FMOD.Studio.Bus(bus2);
			return rESULT;
		}

		public global::FMOD.RESULT getVCAByID(global::System.Guid guid, out global::FMOD.Studio.VCA vca)
		{
			vca = null;
			global::System.IntPtr vca2 = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_System_GetVCAByID(rawPtr, ref guid, out vca2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			vca = new global::FMOD.Studio.VCA(vca2);
			return rESULT;
		}

		public global::FMOD.RESULT getBankByID(global::System.Guid guid, out global::FMOD.Studio.Bank bank)
		{
			bank = null;
			global::System.IntPtr bank2 = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_System_GetBankByID(rawPtr, ref guid, out bank2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			bank = new global::FMOD.Studio.Bank(bank2);
			return rESULT;
		}

		public global::FMOD.RESULT getSoundInfo(string key, out global::FMOD.Studio.SOUND_INFO info)
		{
			global::FMOD.Studio.SOUND_INFO_INTERNAL info2;
			global::FMOD.RESULT rESULT = FMOD_Studio_System_GetSoundInfo(rawPtr, global::System.Text.Encoding.UTF8.GetBytes(key + '\0'), out info2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				info = new global::FMOD.Studio.SOUND_INFO();
				return rESULT;
			}
			info2.assign(out info);
			return rESULT;
		}

		public global::FMOD.RESULT lookupID(string path, out global::System.Guid guid)
		{
			return FMOD_Studio_System_LookupID(rawPtr, global::System.Text.Encoding.UTF8.GetBytes(path + '\0'), out guid);
		}

		public global::FMOD.RESULT lookupPath(global::System.Guid guid, out string path)
		{
			path = null;
			byte[] array = new byte[256];
			int retrieved = 0;
			global::FMOD.RESULT rESULT = FMOD_Studio_System_LookupPath(rawPtr, ref guid, array, array.Length, out retrieved);
			if (rESULT == global::FMOD.RESULT.ERR_TRUNCATED)
			{
				array = new byte[retrieved];
				rESULT = FMOD_Studio_System_LookupPath(rawPtr, ref guid, array, array.Length, out retrieved);
			}
			if (rESULT == global::FMOD.RESULT.OK)
			{
				path = global::System.Text.Encoding.UTF8.GetString(array, 0, retrieved - 1);
			}
			return rESULT;
		}

		public global::FMOD.RESULT getNumListeners(out int numlisteners)
		{
			return FMOD_Studio_System_GetNumListeners(rawPtr, out numlisteners);
		}

		public global::FMOD.RESULT setNumListeners(int numlisteners)
		{
			return FMOD_Studio_System_SetNumListeners(rawPtr, numlisteners);
		}

		public global::FMOD.RESULT getListenerAttributes(int listener, out global::FMOD.Studio.ATTRIBUTES_3D attributes)
		{
			return FMOD_Studio_System_GetListenerAttributes(rawPtr, listener, out attributes);
		}

		public global::FMOD.RESULT setListenerAttributes(int listener, global::FMOD.Studio.ATTRIBUTES_3D attributes)
		{
			return FMOD_Studio_System_SetListenerAttributes(rawPtr, listener, ref attributes);
		}

		public global::FMOD.RESULT loadBankFile(string name, global::FMOD.Studio.LOAD_BANK_FLAGS flags, out global::FMOD.Studio.Bank bank)
		{
			bank = null;
			global::System.IntPtr bank2 = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_System_LoadBankFile(rawPtr, global::System.Text.Encoding.UTF8.GetBytes(name + '\0'), flags, out bank2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			bank = new global::FMOD.Studio.Bank(bank2);
			return rESULT;
		}

		public global::FMOD.RESULT loadBankMemory(byte[] buffer, global::FMOD.Studio.LOAD_BANK_FLAGS flags, out global::FMOD.Studio.Bank bank)
		{
			bank = null;
			global::System.IntPtr bank2 = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_System_LoadBankMemory(rawPtr, buffer, buffer.Length, global::FMOD.Studio.LOAD_MEMORY_MODE.LOAD_MEMORY, flags, out bank2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			bank = new global::FMOD.Studio.Bank(bank2);
			return rESULT;
		}

		public global::FMOD.RESULT loadBankCustom(global::FMOD.Studio.BANK_INFO info, global::FMOD.Studio.LOAD_BANK_FLAGS flags, out global::FMOD.Studio.Bank bank)
		{
			bank = null;
			info.size = global::System.Runtime.InteropServices.Marshal.SizeOf(info);
			global::System.IntPtr bank2 = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_System_LoadBankCustom(rawPtr, ref info, flags, out bank2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			bank = new global::FMOD.Studio.Bank(bank2);
			return rESULT;
		}

		public global::FMOD.RESULT unloadAll()
		{
			return FMOD_Studio_System_UnloadAll(rawPtr);
		}

		public global::FMOD.RESULT flushCommands()
		{
			return FMOD_Studio_System_FlushCommands(rawPtr);
		}

		public global::FMOD.RESULT startCommandCapture(string path, global::FMOD.Studio.COMMANDCAPTURE_FLAGS flags)
		{
			return FMOD_Studio_System_StartCommandCapture(rawPtr, global::System.Text.Encoding.UTF8.GetBytes(path + '\0'), flags);
		}

		public global::FMOD.RESULT stopCommandCapture()
		{
			return FMOD_Studio_System_StopCommandCapture(rawPtr);
		}

		public global::FMOD.RESULT loadCommandReplay(string path, global::FMOD.Studio.COMMANDREPLAY_FLAGS flags, out global::FMOD.Studio.CommandReplay replay)
		{
			replay = null;
			global::System.IntPtr commandReplay = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_System_LoadCommandReplay(rawPtr, global::System.Text.Encoding.UTF8.GetBytes(path + '\0'), flags, out commandReplay);
			if (rESULT == global::FMOD.RESULT.OK)
			{
				replay = new global::FMOD.Studio.CommandReplay(commandReplay);
			}
			return rESULT;
		}

		public global::FMOD.RESULT getBankCount(out int count)
		{
			return FMOD_Studio_System_GetBankCount(rawPtr, out count);
		}

		public global::FMOD.RESULT getBankList(out global::FMOD.Studio.Bank[] array)
		{
			array = null;
			int count;
			global::FMOD.RESULT rESULT = FMOD_Studio_System_GetBankCount(rawPtr, out count);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			if (count == 0)
			{
				array = new global::FMOD.Studio.Bank[0];
				return rESULT;
			}
			global::System.IntPtr[] array2 = new global::System.IntPtr[count];
			int count2;
			rESULT = FMOD_Studio_System_GetBankList(rawPtr, array2, count, out count2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			if (count2 > count)
			{
				count2 = count;
			}
			array = new global::FMOD.Studio.Bank[count2];
			for (int i = 0; i < count2; i++)
			{
				array[i] = new global::FMOD.Studio.Bank(array2[i]);
			}
			return global::FMOD.RESULT.OK;
		}

		public global::FMOD.RESULT getCPUUsage(out global::FMOD.Studio.CPU_USAGE usage)
		{
			return FMOD_Studio_System_GetCPUUsage(rawPtr, out usage);
		}

		public global::FMOD.RESULT getBufferUsage(out global::FMOD.Studio.BUFFER_USAGE usage)
		{
			return FMOD_Studio_System_GetBufferUsage(rawPtr, out usage);
		}

		public global::FMOD.RESULT resetBufferUsage()
		{
			return FMOD_Studio_System_ResetBufferUsage(rawPtr);
		}

		public global::FMOD.RESULT setCallback(global::FMOD.Studio.SYSTEM_CALLBACK callback, global::FMOD.Studio.SYSTEM_CALLBACK_TYPE callbackmask = global::FMOD.Studio.SYSTEM_CALLBACK_TYPE.ALL)
		{
			return FMOD_Studio_System_SetCallback(rawPtr, callback, callbackmask);
		}

		public global::FMOD.RESULT getUserData(out global::System.IntPtr userData)
		{
			return FMOD_Studio_System_GetUserData(rawPtr, out userData);
		}

		public global::FMOD.RESULT setUserData(global::System.IntPtr userData)
		{
			return FMOD_Studio_System_SetUserData(rawPtr, userData);
		}

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_Create(out global::System.IntPtr studiosystem, uint headerversion);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern bool FMOD_Studio_System_IsValid(global::System.IntPtr studiosystem);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_SetAdvancedSettings(global::System.IntPtr studiosystem, ref global::FMOD.Studio.ADVANCEDSETTINGS settings);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_GetAdvancedSettings(global::System.IntPtr studiosystem, out global::FMOD.Studio.ADVANCEDSETTINGS settings);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_Initialize(global::System.IntPtr studiosystem, int maxchannels, global::FMOD.Studio.INITFLAGS studioFlags, global::FMOD.INITFLAGS flags, global::System.IntPtr extradriverdata);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_Release(global::System.IntPtr studiosystem);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_Update(global::System.IntPtr studiosystem);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_GetLowLevelSystem(global::System.IntPtr studiosystem, out global::System.IntPtr system);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_GetEvent(global::System.IntPtr studiosystem, byte[] path, out global::System.IntPtr description);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_GetBus(global::System.IntPtr studiosystem, byte[] path, out global::System.IntPtr bus);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_GetVCA(global::System.IntPtr studiosystem, byte[] path, out global::System.IntPtr vca);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_GetBank(global::System.IntPtr studiosystem, byte[] path, out global::System.IntPtr bank);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_GetEventByID(global::System.IntPtr studiosystem, ref global::System.Guid guid, out global::System.IntPtr description);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_GetBusByID(global::System.IntPtr studiosystem, ref global::System.Guid guid, out global::System.IntPtr bus);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_GetVCAByID(global::System.IntPtr studiosystem, ref global::System.Guid guid, out global::System.IntPtr vca);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_GetBankByID(global::System.IntPtr studiosystem, ref global::System.Guid guid, out global::System.IntPtr bank);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_GetSoundInfo(global::System.IntPtr studiosystem, byte[] key, out global::FMOD.Studio.SOUND_INFO_INTERNAL info);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_LookupID(global::System.IntPtr studiosystem, byte[] path, out global::System.Guid guid);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_LookupPath(global::System.IntPtr studiosystem, ref global::System.Guid guid, [global::System.Runtime.InteropServices.Out] byte[] path, int size, out int retrieved);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_GetNumListeners(global::System.IntPtr studiosystem, out int numlisteners);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_SetNumListeners(global::System.IntPtr studiosystem, int numlisteners);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_GetListenerAttributes(global::System.IntPtr studiosystem, int listener, out global::FMOD.Studio.ATTRIBUTES_3D attributes);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_SetListenerAttributes(global::System.IntPtr studiosystem, int listener, ref global::FMOD.Studio.ATTRIBUTES_3D attributes);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_LoadBankFile(global::System.IntPtr studiosystem, byte[] filename, global::FMOD.Studio.LOAD_BANK_FLAGS flags, out global::System.IntPtr bank);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_LoadBankMemory(global::System.IntPtr studiosystem, byte[] buffer, int length, global::FMOD.Studio.LOAD_MEMORY_MODE mode, global::FMOD.Studio.LOAD_BANK_FLAGS flags, out global::System.IntPtr bank);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_LoadBankCustom(global::System.IntPtr studiosystem, ref global::FMOD.Studio.BANK_INFO info, global::FMOD.Studio.LOAD_BANK_FLAGS flags, out global::System.IntPtr bank);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_UnloadAll(global::System.IntPtr studiosystem);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_FlushCommands(global::System.IntPtr studiosystem);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_StartCommandCapture(global::System.IntPtr studiosystem, byte[] path, global::FMOD.Studio.COMMANDCAPTURE_FLAGS flags);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_StopCommandCapture(global::System.IntPtr studiosystem);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_LoadCommandReplay(global::System.IntPtr studiosystem, byte[] path, global::FMOD.Studio.COMMANDREPLAY_FLAGS flags, out global::System.IntPtr commandReplay);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_GetBankCount(global::System.IntPtr studiosystem, out int count);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_GetBankList(global::System.IntPtr studiosystem, global::System.IntPtr[] array, int capacity, out int count);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_GetCPUUsage(global::System.IntPtr studiosystem, out global::FMOD.Studio.CPU_USAGE usage);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_GetBufferUsage(global::System.IntPtr studiosystem, out global::FMOD.Studio.BUFFER_USAGE usage);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_ResetBufferUsage(global::System.IntPtr studiosystem);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_SetCallback(global::System.IntPtr studiosystem, global::FMOD.Studio.SYSTEM_CALLBACK callback, global::FMOD.Studio.SYSTEM_CALLBACK_TYPE callbackmask);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_GetUserData(global::System.IntPtr studiosystem, out global::System.IntPtr userData);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_System_SetUserData(global::System.IntPtr studiosystem, global::System.IntPtr userData);

		protected override bool isValidInternal()
		{
			return FMOD_Studio_System_IsValid(rawPtr);
		}
	}
}
