namespace FMOD.Studio
{
	public class Bank : global::FMOD.Studio.HandleBase
	{
		public Bank(global::System.IntPtr raw)
			: base(raw)
		{
		}

		public global::FMOD.RESULT getID(out global::System.Guid id)
		{
			return FMOD_Studio_Bank_GetID(rawPtr, out id);
		}

		public global::FMOD.RESULT getPath(out string path)
		{
			path = null;
			byte[] array = new byte[256];
			int retrieved = 0;
			global::FMOD.RESULT rESULT = FMOD_Studio_Bank_GetPath(rawPtr, array, array.Length, out retrieved);
			if (rESULT == global::FMOD.RESULT.ERR_TRUNCATED)
			{
				array = new byte[retrieved];
				rESULT = FMOD_Studio_Bank_GetPath(rawPtr, array, array.Length, out retrieved);
			}
			if (rESULT == global::FMOD.RESULT.OK)
			{
				path = global::System.Text.Encoding.UTF8.GetString(array, 0, retrieved - 1);
			}
			return rESULT;
		}

		public global::FMOD.RESULT unload()
		{
			global::FMOD.RESULT rESULT = FMOD_Studio_Bank_Unload(rawPtr);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			rawPtr = global::System.IntPtr.Zero;
			return global::FMOD.RESULT.OK;
		}

		public global::FMOD.RESULT loadSampleData()
		{
			return FMOD_Studio_Bank_LoadSampleData(rawPtr);
		}

		public global::FMOD.RESULT unloadSampleData()
		{
			return FMOD_Studio_Bank_UnloadSampleData(rawPtr);
		}

		public global::FMOD.RESULT getLoadingState(out global::FMOD.Studio.LOADING_STATE state)
		{
			return FMOD_Studio_Bank_GetLoadingState(rawPtr, out state);
		}

		public global::FMOD.RESULT getSampleLoadingState(out global::FMOD.Studio.LOADING_STATE state)
		{
			return FMOD_Studio_Bank_GetSampleLoadingState(rawPtr, out state);
		}

		public global::FMOD.RESULT getStringCount(out int count)
		{
			return FMOD_Studio_Bank_GetStringCount(rawPtr, out count);
		}

		public global::FMOD.RESULT getStringInfo(int index, out global::System.Guid id, out string path)
		{
			path = null;
			byte[] array = new byte[256];
			int retrieved = 0;
			global::FMOD.RESULT rESULT = FMOD_Studio_Bank_GetStringInfo(rawPtr, index, out id, array, array.Length, out retrieved);
			if (rESULT == global::FMOD.RESULT.ERR_TRUNCATED)
			{
				array = new byte[retrieved];
				rESULT = FMOD_Studio_Bank_GetStringInfo(rawPtr, index, out id, array, array.Length, out retrieved);
			}
			if (rESULT == global::FMOD.RESULT.OK)
			{
				path = global::System.Text.Encoding.UTF8.GetString(array, 0, retrieved - 1);
			}
			return global::FMOD.RESULT.OK;
		}

		public global::FMOD.RESULT getEventCount(out int count)
		{
			return FMOD_Studio_Bank_GetEventCount(rawPtr, out count);
		}

		public global::FMOD.RESULT getEventList(out global::FMOD.Studio.EventDescription[] array)
		{
			array = null;
			int count;
			global::FMOD.RESULT rESULT = FMOD_Studio_Bank_GetEventCount(rawPtr, out count);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			if (count == 0)
			{
				array = new global::FMOD.Studio.EventDescription[0];
				return rESULT;
			}
			global::System.IntPtr[] array2 = new global::System.IntPtr[count];
			int count2;
			rESULT = FMOD_Studio_Bank_GetEventList(rawPtr, array2, count, out count2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			if (count2 > count)
			{
				count2 = count;
			}
			array = new global::FMOD.Studio.EventDescription[count2];
			for (int i = 0; i < count2; i++)
			{
				array[i] = new global::FMOD.Studio.EventDescription(array2[i]);
			}
			return global::FMOD.RESULT.OK;
		}

		public global::FMOD.RESULT getBusCount(out int count)
		{
			return FMOD_Studio_Bank_GetBusCount(rawPtr, out count);
		}

		public global::FMOD.RESULT getBusList(out global::FMOD.Studio.Bus[] array)
		{
			array = null;
			int count;
			global::FMOD.RESULT rESULT = FMOD_Studio_Bank_GetBusCount(rawPtr, out count);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			if (count == 0)
			{
				array = new global::FMOD.Studio.Bus[0];
				return rESULT;
			}
			global::System.IntPtr[] array2 = new global::System.IntPtr[count];
			int count2;
			rESULT = FMOD_Studio_Bank_GetBusList(rawPtr, array2, count, out count2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			if (count2 > count)
			{
				count2 = count;
			}
			array = new global::FMOD.Studio.Bus[count2];
			for (int i = 0; i < count2; i++)
			{
				array[i] = new global::FMOD.Studio.Bus(array2[i]);
			}
			return global::FMOD.RESULT.OK;
		}

		public global::FMOD.RESULT getVCACount(out int count)
		{
			return FMOD_Studio_Bank_GetVCACount(rawPtr, out count);
		}

		public global::FMOD.RESULT getVCAList(out global::FMOD.Studio.VCA[] array)
		{
			array = null;
			int count;
			global::FMOD.RESULT rESULT = FMOD_Studio_Bank_GetVCACount(rawPtr, out count);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			if (count == 0)
			{
				array = new global::FMOD.Studio.VCA[0];
				return rESULT;
			}
			global::System.IntPtr[] array2 = new global::System.IntPtr[count];
			int count2;
			rESULT = FMOD_Studio_Bank_GetVCAList(rawPtr, array2, count, out count2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			if (count2 > count)
			{
				count2 = count;
			}
			array = new global::FMOD.Studio.VCA[count2];
			for (int i = 0; i < count2; i++)
			{
				array[i] = new global::FMOD.Studio.VCA(array2[i]);
			}
			return global::FMOD.RESULT.OK;
		}

		public global::FMOD.RESULT getUserData(out global::System.IntPtr userData)
		{
			return FMOD_Studio_Bank_GetUserData(rawPtr, out userData);
		}

		public global::FMOD.RESULT setUserData(global::System.IntPtr userData)
		{
			return FMOD_Studio_Bank_SetUserData(rawPtr, userData);
		}

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern bool FMOD_Studio_Bank_IsValid(global::System.IntPtr bank);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bank_GetID(global::System.IntPtr bank, out global::System.Guid id);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bank_GetPath(global::System.IntPtr bank, [global::System.Runtime.InteropServices.Out] byte[] path, int size, out int retrieved);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bank_Unload(global::System.IntPtr bank);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bank_LoadSampleData(global::System.IntPtr bank);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bank_UnloadSampleData(global::System.IntPtr bank);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bank_GetLoadingState(global::System.IntPtr bank, out global::FMOD.Studio.LOADING_STATE state);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bank_GetSampleLoadingState(global::System.IntPtr bank, out global::FMOD.Studio.LOADING_STATE state);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bank_GetStringCount(global::System.IntPtr bank, out int count);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bank_GetStringInfo(global::System.IntPtr bank, int index, out global::System.Guid id, [global::System.Runtime.InteropServices.Out] byte[] path, int size, out int retrieved);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bank_GetEventCount(global::System.IntPtr bank, out int count);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bank_GetEventList(global::System.IntPtr bank, global::System.IntPtr[] array, int capacity, out int count);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bank_GetBusCount(global::System.IntPtr bank, out int count);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bank_GetBusList(global::System.IntPtr bank, global::System.IntPtr[] array, int capacity, out int count);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bank_GetVCACount(global::System.IntPtr bank, out int count);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bank_GetVCAList(global::System.IntPtr bank, global::System.IntPtr[] array, int capacity, out int count);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bank_GetUserData(global::System.IntPtr studiosystem, out global::System.IntPtr userData);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_Bank_SetUserData(global::System.IntPtr studiosystem, global::System.IntPtr userData);

		protected override bool isValidInternal()
		{
			return FMOD_Studio_Bank_IsValid(rawPtr);
		}
	}
}
