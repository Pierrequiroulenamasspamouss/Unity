namespace FMOD.Studio
{
	public class CueInstance : global::FMOD.Studio.HandleBase
	{
		public CueInstance(global::System.IntPtr raw)
			: base(raw)
		{
		}

		public global::FMOD.RESULT trigger()
		{
			return FMOD_Studio_CueInstance_Trigger(rawPtr);
		}

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern bool FMOD_Studio_CueInstance_IsValid(global::System.IntPtr cue);

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_CueInstance_Trigger(global::System.IntPtr cue);

		protected override bool isValidInternal()
		{
			return FMOD_Studio_CueInstance_IsValid(rawPtr);
		}
	}
}
