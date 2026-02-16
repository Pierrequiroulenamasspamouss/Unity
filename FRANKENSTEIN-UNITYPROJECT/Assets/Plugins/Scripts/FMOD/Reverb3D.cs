namespace FMOD
{
	public class Reverb3D : global::FMOD.HandleBase
	{
		public Reverb3D(global::System.IntPtr raw)
			: base(raw)
		{
		}

		public global::FMOD.RESULT release()
		{
			global::FMOD.RESULT rESULT = FMOD5_Reverb3D_Release(getRaw());
			if (rESULT == global::FMOD.RESULT.OK)
			{
				rawPtr = global::System.IntPtr.Zero;
			}
			return rESULT;
		}

		public global::FMOD.RESULT set3DAttributes(ref global::FMOD.VECTOR position, float mindistance, float maxdistance)
		{
			return FMOD5_Reverb3D_Set3DAttributes(rawPtr, ref position, mindistance, maxdistance);
		}

		public global::FMOD.RESULT get3DAttributes(ref global::FMOD.VECTOR position, ref float mindistance, ref float maxdistance)
		{
			return FMOD5_Reverb3D_Get3DAttributes(rawPtr, ref position, ref mindistance, ref maxdistance);
		}

		public global::FMOD.RESULT setProperties(ref global::FMOD.REVERB_PROPERTIES properties)
		{
			return FMOD5_Reverb3D_SetProperties(rawPtr, ref properties);
		}

		public global::FMOD.RESULT getProperties(ref global::FMOD.REVERB_PROPERTIES properties)
		{
			return FMOD5_Reverb3D_GetProperties(rawPtr, ref properties);
		}

		public global::FMOD.RESULT setActive(bool active)
		{
			return FMOD5_Reverb3D_SetActive(rawPtr, active);
		}

		public global::FMOD.RESULT getActive(out bool active)
		{
			return FMOD5_Reverb3D_GetActive(rawPtr, out active);
		}

		public global::FMOD.RESULT setUserData(global::System.IntPtr userdata)
		{
			return FMOD5_Reverb3D_SetUserData(rawPtr, userdata);
		}

		public global::FMOD.RESULT getUserData(out global::System.IntPtr userdata)
		{
			return FMOD5_Reverb3D_GetUserData(rawPtr, out userdata);
		}

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Reverb3D_Release(global::System.IntPtr reverb);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Reverb3D_Set3DAttributes(global::System.IntPtr reverb, ref global::FMOD.VECTOR position, float mindistance, float maxdistance);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Reverb3D_Get3DAttributes(global::System.IntPtr reverb, ref global::FMOD.VECTOR position, ref float mindistance, ref float maxdistance);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Reverb3D_SetProperties(global::System.IntPtr reverb, ref global::FMOD.REVERB_PROPERTIES properties);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Reverb3D_GetProperties(global::System.IntPtr reverb, ref global::FMOD.REVERB_PROPERTIES properties);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Reverb3D_SetActive(global::System.IntPtr reverb, bool active);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Reverb3D_GetActive(global::System.IntPtr reverb, out bool active);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Reverb3D_SetUserData(global::System.IntPtr reverb, global::System.IntPtr userdata);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Reverb3D_GetUserData(global::System.IntPtr reverb, out global::System.IntPtr userdata);
	}
}
