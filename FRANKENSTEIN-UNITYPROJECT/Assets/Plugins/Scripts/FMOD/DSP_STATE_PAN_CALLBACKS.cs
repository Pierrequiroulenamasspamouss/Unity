namespace FMOD
{
	public struct DSP_STATE_PAN_CALLBACKS
	{
		public global::FMOD.DSP_PAN_SUM_MONO_MATRIX summonomatrix;

		public global::FMOD.DSP_PAN_SUM_STEREO_MATRIX sumstereomatrix;

		public global::FMOD.DSP_PAN_SUM_SURROUND_MATRIX sumsurroundmatrix;

		public global::FMOD.DSP_PAN_SUM_MONO_TO_SURROUND_MATRIX summonotosurroundmatrix;

		public global::FMOD.DSP_PAN_SUM_STEREO_TO_SURROUND_MATRIX sumstereotosurroundmatrix;

		public global::FMOD.DSP_PAN_3D_GET_ROLLOFF_GAIN getrolloffgain;
	}
}
