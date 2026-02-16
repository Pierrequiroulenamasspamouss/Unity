namespace FMOD
{
	public delegate global::FMOD.RESULT DSP_PAN_SUM_SURROUND_MATRIX(ref global::FMOD.DSP_STATE dsp_state, int sourceSpeakerMode, int targetSpeakerMode, float direction, float extent, float rotation, float lowFrequencyGain, float overallGain, int matrixHop, global::System.IntPtr matrix, global::FMOD.DSP_PAN_SURROUND_FLAGS flags);
}
