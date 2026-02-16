namespace FMOD.Studio
{
	public class EventDescription : global::FMOD.Studio.HandleBase
	{
		public EventDescription(global::System.IntPtr raw)
			: base(raw)
		{
		}

		public global::FMOD.RESULT getID(out global::System.Guid id)
		{
			return FMOD_Studio_EventDescription_GetID(rawPtr, out id);
		}

		public global::FMOD.RESULT getPath(out string path)
		{
			path = null;
			byte[] array = new byte[256];
			int retrieved = 0;
			global::FMOD.RESULT rESULT = FMOD_Studio_EventDescription_GetPath(rawPtr, array, array.Length, out retrieved);
			if (rESULT == global::FMOD.RESULT.ERR_TRUNCATED)
			{
				array = new byte[retrieved];
				rESULT = FMOD_Studio_EventDescription_GetPath(rawPtr, array, array.Length, out retrieved);
			}
			if (rESULT == global::FMOD.RESULT.OK)
			{
				path = global::System.Text.Encoding.UTF8.GetString(array, 0, retrieved - 1);
			}
			return rESULT;
		}

		public global::FMOD.RESULT getParameterCount(out int count)
		{
			return FMOD_Studio_EventDescription_GetParameterCount(rawPtr, out count);
		}

		public global::FMOD.RESULT getParameterByIndex(int index, out global::FMOD.Studio.PARAMETER_DESCRIPTION parameter)
		{
			parameter = default(global::FMOD.Studio.PARAMETER_DESCRIPTION);
			global::FMOD.Studio.PARAMETER_DESCRIPTION_INTERNAL parameter2;
			global::FMOD.RESULT rESULT = FMOD_Studio_EventDescription_GetParameterByIndex(rawPtr, index, out parameter2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			parameter2.assign(out parameter);
			return rESULT;
		}

		public global::FMOD.RESULT getParameter(string name, out global::FMOD.Studio.PARAMETER_DESCRIPTION parameter)
		{
			parameter = default(global::FMOD.Studio.PARAMETER_DESCRIPTION);
			global::FMOD.Studio.PARAMETER_DESCRIPTION_INTERNAL parameter2;
			global::FMOD.RESULT rESULT = FMOD_Studio_EventDescription_GetParameter(rawPtr, global::System.Text.Encoding.UTF8.GetBytes(name + '\0'), out parameter2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			parameter2.assign(out parameter);
			return rESULT;
		}

		public global::FMOD.RESULT getUserPropertyCount(out int count)
		{
			return FMOD_Studio_EventDescription_GetUserPropertyCount(rawPtr, out count);
		}

		public global::FMOD.RESULT getUserPropertyByIndex(int index, out global::FMOD.Studio.USER_PROPERTY property)
		{
			global::FMOD.Studio.USER_PROPERTY_INTERNAL property2;
			global::FMOD.RESULT rESULT = FMOD_Studio_EventDescription_GetUserPropertyByIndex(rawPtr, index, out property2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				property = default(global::FMOD.Studio.USER_PROPERTY);
				return rESULT;
			}
			property = property2.createPublic();
			return global::FMOD.RESULT.OK;
		}

		public global::FMOD.RESULT getUserProperty(string name, out global::FMOD.Studio.USER_PROPERTY property)
		{
			global::FMOD.Studio.USER_PROPERTY_INTERNAL property2;
			global::FMOD.RESULT rESULT = FMOD_Studio_EventDescription_GetUserProperty(rawPtr, global::System.Text.Encoding.UTF8.GetBytes(name + '\0'), out property2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				property = default(global::FMOD.Studio.USER_PROPERTY);
				return rESULT;
			}
			property = property2.createPublic();
			return global::FMOD.RESULT.OK;
		}

		public global::FMOD.RESULT getLength(out int length)
		{
			return FMOD_Studio_EventDescription_GetLength(rawPtr, out length);
		}

		public global::FMOD.RESULT getMinimumDistance(out float distance)
		{
			return FMOD_Studio_EventDescription_GetMinimumDistance(rawPtr, out distance);
		}

		public global::FMOD.RESULT getMaximumDistance(out float distance)
		{
			return FMOD_Studio_EventDescription_GetMaximumDistance(rawPtr, out distance);
		}

		public global::FMOD.RESULT isOneshot(out bool oneshot)
		{
			return FMOD_Studio_EventDescription_IsOneshot(rawPtr, out oneshot);
		}

		public global::FMOD.RESULT isStream(out bool isStream)
		{
			return FMOD_Studio_EventDescription_IsStream(rawPtr, out isStream);
		}

		public global::FMOD.RESULT is3D(out bool is3D)
		{
			return FMOD_Studio_EventDescription_Is3D(rawPtr, out is3D);
		}

		public global::FMOD.RESULT createInstance(out global::FMOD.Studio.EventInstance instance)
		{
			instance = null;
			global::System.IntPtr instance2 = default(global::System.IntPtr);
			global::FMOD.RESULT rESULT = FMOD_Studio_EventDescription_CreateInstance(rawPtr, out instance2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			instance = new global::FMOD.Studio.EventInstance(instance2);
			return rESULT;
		}

		public global::FMOD.RESULT getInstanceCount(out int count)
		{
			return FMOD_Studio_EventDescription_GetInstanceCount(rawPtr, out count);
		}

		public global::FMOD.RESULT getInstanceList(out global::FMOD.Studio.EventInstance[] array)
		{
			array = null;
			int count;
			global::FMOD.RESULT rESULT = FMOD_Studio_EventDescription_GetInstanceCount(rawPtr, out count);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			if (count == 0)
			{
				array = new global::FMOD.Studio.EventInstance[0];
				return rESULT;
			}
			global::System.IntPtr[] array2 = new global::System.IntPtr[count];
			int count2;
			rESULT = FMOD_Studio_EventDescription_GetInstanceList(rawPtr, array2, count, out count2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			if (count2 > count)
			{
				count2 = count;
			}
			array = new global::FMOD.Studio.EventInstance[count2];
			for (int i = 0; i < count2; i++)
			{
				array[i] = new global::FMOD.Studio.EventInstance(array2[i]);
			}
			return global::FMOD.RESULT.OK;
		}

		public global::FMOD.RESULT loadSampleData()
		{
			return FMOD_Studio_EventDescription_LoadSampleData(rawPtr);
		}

		public global::FMOD.RESULT unloadSampleData()
		{
			return FMOD_Studio_EventDescription_UnloadSampleData(rawPtr);
		}

		public global::FMOD.RESULT getSampleLoadingState(out global::FMOD.Studio.LOADING_STATE state)
		{
			return FMOD_Studio_EventDescription_GetSampleLoadingState(rawPtr, out state);
		}

		public global::FMOD.RESULT releaseAllInstances()
		{
			return FMOD_Studio_EventDescription_ReleaseAllInstances(rawPtr);
		}

		public global::FMOD.RESULT setCallback(global::FMOD.Studio.EVENT_CALLBACK callback, global::FMOD.Studio.EVENT_CALLBACK_TYPE callbackmask = global::FMOD.Studio.EVENT_CALLBACK_TYPE.ALL)
		{
			return FMOD_Studio_EventDescription_SetCallback(rawPtr, callback, callbackmask);
		}

		public global::FMOD.RESULT getUserData(out global::System.IntPtr userData)
		{
			return FMOD_Studio_EventDescription_GetUserData(rawPtr, out userData);
		}

		public global::FMOD.RESULT setUserData(global::System.IntPtr userData)
		{
			return FMOD_Studio_EventDescription_SetUserData(rawPtr, userData);
		}

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern bool FMOD_Studio_EventDescription_IsValid(global::System.IntPtr eventdescription);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_GetID(global::System.IntPtr eventdescription, out global::System.Guid id);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_GetPath(global::System.IntPtr eventdescription, [global::System.Runtime.InteropServices.Out] byte[] path, int size, out int retrieved);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_GetParameterCount(global::System.IntPtr eventdescription, out int count);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_GetParameterByIndex(global::System.IntPtr eventdescription, int index, out global::FMOD.Studio.PARAMETER_DESCRIPTION_INTERNAL parameter);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_GetParameter(global::System.IntPtr eventdescription, byte[] name, out global::FMOD.Studio.PARAMETER_DESCRIPTION_INTERNAL parameter);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_GetUserPropertyCount(global::System.IntPtr eventdescription, out int count);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_GetUserPropertyByIndex(global::System.IntPtr eventdescription, int index, out global::FMOD.Studio.USER_PROPERTY_INTERNAL property);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_GetUserProperty(global::System.IntPtr eventdescription, byte[] name, out global::FMOD.Studio.USER_PROPERTY_INTERNAL property);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_GetLength(global::System.IntPtr eventdescription, out int length);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_GetMinimumDistance(global::System.IntPtr eventdescription, out float distance);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_GetMaximumDistance(global::System.IntPtr eventdescription, out float distance);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_IsOneshot(global::System.IntPtr eventdescription, out bool oneshot);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_IsStream(global::System.IntPtr eventdescription, out bool isStream);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_Is3D(global::System.IntPtr eventdescription, out bool is3D);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_CreateInstance(global::System.IntPtr eventdescription, out global::System.IntPtr instance);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_GetInstanceCount(global::System.IntPtr eventdescription, out int count);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_GetInstanceList(global::System.IntPtr eventdescription, global::System.IntPtr[] array, int capacity, out int count);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_LoadSampleData(global::System.IntPtr eventdescription);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_UnloadSampleData(global::System.IntPtr eventdescription);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_GetSampleLoadingState(global::System.IntPtr eventdescription, out global::FMOD.Studio.LOADING_STATE state);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_ReleaseAllInstances(global::System.IntPtr eventdescription);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_SetCallback(global::System.IntPtr eventdescription, global::FMOD.Studio.EVENT_CALLBACK callback, global::FMOD.Studio.EVENT_CALLBACK_TYPE callbackmask);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_GetUserData(global::System.IntPtr eventdescription, out global::System.IntPtr userData);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_EventDescription_SetUserData(global::System.IntPtr eventdescription, global::System.IntPtr userData);

		protected override bool isValidInternal()
		{
			return FMOD_Studio_EventDescription_IsValid(rawPtr);
		}
	}
}
