namespace FMOD.Studio
{
	public class CommandReplay : global::FMOD.Studio.HandleBase
	{
		public CommandReplay(global::System.IntPtr raw)
			: base(raw)
		{
		}

		public global::FMOD.RESULT getSystem(out global::FMOD.Studio.System system)
		{
			system = null;
			global::System.IntPtr system2 = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_CommandReplay_GetSystem(rawPtr, out system2);
			if (rESULT == global::FMOD.RESULT.OK)
			{
				system = new global::FMOD.Studio.System(system2);
			}
			return rESULT;
		}

		public global::FMOD.RESULT getLength(out float totalTime)
		{
			return FMOD_Studio_CommandReplay_GetLength(rawPtr, out totalTime);
		}

		public global::FMOD.RESULT getCommandCount(out int count)
		{
			return FMOD_Studio_CommandReplay_GetCommandCount(rawPtr, out count);
		}

		public global::FMOD.RESULT getCommandInfo(int commandIndex, out global::FMOD.Studio.COMMAND_INFO info)
		{
			global::FMOD.Studio.COMMAND_INFO_INTERNAL info2 = default(global::FMOD.Studio.COMMAND_INFO_INTERNAL);
			global::FMOD.RESULT rESULT = FMOD_Studio_CommandReplay_GetCommandInfo(rawPtr, commandIndex, out info2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				info = default(global::FMOD.Studio.COMMAND_INFO);
				return rESULT;
			}
			info = info2.createPublic();
			return rESULT;
		}

		public global::FMOD.RESULT getCommandString(int commandIndex, out string description)
		{
			description = null;
			byte[] array = new byte[8];
			global::FMOD.RESULT rESULT;
			while (true)
			{
				rESULT = FMOD_Studio_CommandReplay_GetCommandString(rawPtr, commandIndex, array, array.Length);
				switch (rESULT)
				{
				case global::FMOD.RESULT.ERR_TRUNCATED:
					goto IL_0023;
				case global::FMOD.RESULT.OK:
				{
					int i;
					for (i = 0; array[i] != 0; i++)
					{
					}
					description = global::System.Text.Encoding.UTF8.GetString(array, 0, i);
					break;
				}
				}
				break;
				IL_0023:
				array = new byte[2 * array.Length];
			}
			return rESULT;
		}

		public global::FMOD.RESULT getCommandAtTime(float time, out int commandIndex)
		{
			return FMOD_Studio_CommandReplay_GetCommandAtTime(rawPtr, time, out commandIndex);
		}

		public global::FMOD.RESULT setBankPath(string bankPath)
		{
			return FMOD_Studio_CommandReplay_SetBankPath(rawPtr, global::System.Text.Encoding.UTF8.GetBytes(bankPath + '\0'));
		}

		public global::FMOD.RESULT start()
		{
			return FMOD_Studio_CommandReplay_Start(rawPtr);
		}

		public global::FMOD.RESULT stop()
		{
			return FMOD_Studio_CommandReplay_Stop(rawPtr);
		}

		public global::FMOD.RESULT seekToTime(float time)
		{
			return FMOD_Studio_CommandReplay_SeekToTime(rawPtr, time);
		}

		public global::FMOD.RESULT seekToCommand(int commandIndex)
		{
			return FMOD_Studio_CommandReplay_SeekToCommand(rawPtr, commandIndex);
		}

		public global::FMOD.RESULT getPaused(out bool paused)
		{
			return FMOD_Studio_CommandReplay_GetPaused(rawPtr, out paused);
		}

		public global::FMOD.RESULT setPaused(bool paused)
		{
			return FMOD_Studio_CommandReplay_SetPaused(rawPtr, paused);
		}

		public global::FMOD.RESULT getPlaybackState(out global::FMOD.Studio.PLAYBACK_STATE state)
		{
			return FMOD_Studio_CommandReplay_GetPlaybackState(rawPtr, out state);
		}

		public global::FMOD.RESULT getCurrentCommand(out int commandIndex, out float currentTime)
		{
			return FMOD_Studio_CommandReplay_GetCurrentCommand(rawPtr, out commandIndex, out currentTime);
		}

		public global::FMOD.RESULT release()
		{
			return FMOD_Studio_CommandReplay_Release(rawPtr);
		}

		public global::FMOD.RESULT setFrameCallback(global::FMOD.Studio.COMMANDREPLAY_FRAME_CALLBACK callback)
		{
			return FMOD_Studio_CommandReplay_SetFrameCallback(rawPtr, callback);
		}

		public global::FMOD.RESULT setLoadBankCallback(global::FMOD.Studio.COMMANDREPLAY_LOAD_BANK_CALLBACK callback)
		{
			return FMOD_Studio_CommandReplay_SetLoadBankCallback(rawPtr, callback);
		}

		public global::FMOD.RESULT setCreateInstanceCallback(global::FMOD.Studio.COMMANDREPLAY_CREATE_INSTANCE_CALLBACK callback)
		{
			return FMOD_Studio_CommandReplay_SetCreateInstanceCallback(rawPtr, callback);
		}

		public global::FMOD.RESULT getUserData(out global::System.IntPtr userData)
		{
			return FMOD_Studio_CommandReplay_GetUserData(rawPtr, out userData);
		}

		public global::FMOD.RESULT setUserData(global::System.IntPtr userData)
		{
			return FMOD_Studio_CommandReplay_SetUserData(rawPtr, userData);
		}

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern bool FMOD_Studio_CommandReplay_IsValid(global::System.IntPtr replay);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_GetSystem(global::System.IntPtr replay, out global::System.IntPtr system);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_GetLength(global::System.IntPtr replay, out float totalTime);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_GetCommandCount(global::System.IntPtr replay, out int count);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_GetCommandInfo(global::System.IntPtr replay, int commandIndex, out global::FMOD.Studio.COMMAND_INFO_INTERNAL info);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_GetCommandString(global::System.IntPtr replay, int commandIndex, [global::System.Runtime.InteropServices.Out] byte[] description, int capacity);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_GetCommandAtTime(global::System.IntPtr replay, float time, out int commandIndex);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_SetBankPath(global::System.IntPtr replay, byte[] bankPath);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_Start(global::System.IntPtr replay);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_Stop(global::System.IntPtr replay);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_SeekToTime(global::System.IntPtr replay, float time);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_SeekToCommand(global::System.IntPtr replay, int commandIndex);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_GetPaused(global::System.IntPtr replay, out bool paused);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_SetPaused(global::System.IntPtr replay, bool paused);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_GetPlaybackState(global::System.IntPtr replay, out global::FMOD.Studio.PLAYBACK_STATE state);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_GetCurrentCommand(global::System.IntPtr replay, out int commandIndex, out float currentTime);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_Release(global::System.IntPtr replay);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_SetFrameCallback(global::System.IntPtr replay, global::FMOD.Studio.COMMANDREPLAY_FRAME_CALLBACK callback);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_SetLoadBankCallback(global::System.IntPtr replay, global::FMOD.Studio.COMMANDREPLAY_LOAD_BANK_CALLBACK callback);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_SetCreateInstanceCallback(global::System.IntPtr replay, global::FMOD.Studio.COMMANDREPLAY_CREATE_INSTANCE_CALLBACK callback);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_GetUserData(global::System.IntPtr replay, out global::System.IntPtr userdata);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CommandReplay_SetUserData(global::System.IntPtr replay, global::System.IntPtr userdata);

		protected override bool isValidInternal()
		{
			return FMOD_Studio_CommandReplay_IsValid(rawPtr);
		}
	}
}
