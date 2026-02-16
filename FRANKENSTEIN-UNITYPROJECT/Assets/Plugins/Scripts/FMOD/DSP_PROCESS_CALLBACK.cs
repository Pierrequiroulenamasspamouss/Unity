namespace FMOD
{
	public delegate global::FMOD.RESULT DSP_PROCESS_CALLBACK(ref global::FMOD.DSP_STATE dsp_state, uint length, ref global::FMOD.DSP_BUFFER_ARRAY inbufferarray, ref global::FMOD.DSP_BUFFER_ARRAY outbufferarray, bool inputsidle, global::FMOD.DSP_PROCESS_OPERATION op);
}
