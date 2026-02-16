namespace FMOD.Studio
{
	public class ParameterInstance : global::FMOD.Studio.HandleBase
	{
		public ParameterInstance(global::System.IntPtr raw)
			: base(raw)
		{
		}

		public global::FMOD.RESULT getDescription(out global::FMOD.Studio.PARAMETER_DESCRIPTION description)
		{
			description = default(global::FMOD.Studio.PARAMETER_DESCRIPTION);
			global::FMOD.Studio.PARAMETER_DESCRIPTION_INTERNAL description2;
			global::FMOD.RESULT rESULT = FMOD_Studio_ParameterInstance_GetDescription(rawPtr, out description2);
			if (rESULT != global::FMOD.RESULT.OK)
			{
				return rESULT;
			}
			description2.assign(out description);
			return rESULT;
		}

		public global::FMOD.RESULT getValue(out float value)
		{
			return FMOD_Studio_ParameterInstance_GetValue(rawPtr, out value);
		}

		public global::FMOD.RESULT setValue(float value)
		{
			return FMOD_Studio_ParameterInstance_SetValue(rawPtr, value);
		}

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern bool FMOD_Studio_ParameterInstance_IsValid(global::System.IntPtr parameter);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_ParameterInstance_GetDescription(global::System.IntPtr parameter, out global::FMOD.Studio.PARAMETER_DESCRIPTION_INTERNAL description);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_ParameterInstance_GetValue(global::System.IntPtr parameter, out float value);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_ParameterInstance_SetValue(global::System.IntPtr parameter, float value);

		protected override bool isValidInternal()
		{
			return FMOD_Studio_ParameterInstance_IsValid(rawPtr);
		}
	}
}
