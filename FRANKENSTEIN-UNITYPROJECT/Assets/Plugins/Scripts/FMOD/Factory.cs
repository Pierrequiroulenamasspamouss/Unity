namespace FMOD
{
	public class Factory
	{
		public static global::FMOD.RESULT System_Create(out global::FMOD.System system)
		{
			system = null;
			global::FMOD.RESULT rESULT = global::FMOD.RESULT.OK;
			global::System.IntPtr system2 = default(global::System.IntPtr);
			rESULT = FMOD5_System_Create(out system2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			system = new global::FMOD.System(system2);
			return rESULT;
		}

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_System_Create(out global::System.IntPtr system);
	}
}
