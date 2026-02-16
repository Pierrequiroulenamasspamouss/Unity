namespace FMOD.Studio
{
	public class VCA : global::FMOD.Studio.HandleBase
	{
		public VCA(global::System.IntPtr raw)
			: base(raw)
		{
		}

		public global::FMOD.RESULT getID(out global::System.Guid id)
		{
			return FMOD_Studio_VCA_GetID(rawPtr, out id);
		}

		public global::FMOD.RESULT getPath(out string path)
		{
			path = null;
			byte[] array = new byte[256];
			int retrieved = 0;
			global::FMOD.RESULT rESULT = FMOD_Studio_VCA_GetPath(rawPtr, array, array.Length, out retrieved);
			if (rESULT == global::FMOD.RESULT.ERR_TRUNCATED)
			{
				array = new byte[retrieved];
				rESULT = FMOD_Studio_VCA_GetPath(rawPtr, array, array.Length, out retrieved);
			}
			if (rESULT == global::FMOD.RESULT.OK)
			{
				path = global::System.Text.Encoding.UTF8.GetString(array, 0, retrieved - 1);
			}
			return rESULT;
		}

		public global::FMOD.RESULT getFaderLevel(out float volume)
		{
			return FMOD_Studio_VCA_GetFaderLevel(rawPtr, out volume);
		}

		public global::FMOD.RESULT setFaderLevel(float volume)
		{
			return FMOD_Studio_VCA_SetFaderLevel(rawPtr, volume);
		}

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern bool FMOD_Studio_VCA_IsValid(global::System.IntPtr vca);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_VCA_GetID(global::System.IntPtr vca, out global::System.Guid id);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_VCA_GetPath(global::System.IntPtr vca, [global::System.Runtime.InteropServices.Out] byte[] path, int size, out int retrieved);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_VCA_GetFaderLevel(global::System.IntPtr vca, out float value);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_VCA_SetFaderLevel(global::System.IntPtr vca, float value);

		protected override bool isValidInternal()
		{
			return FMOD_Studio_VCA_IsValid(rawPtr);
		}
	}
}
