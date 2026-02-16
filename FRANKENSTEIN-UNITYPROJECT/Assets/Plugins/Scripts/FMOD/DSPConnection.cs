namespace FMOD
{
	public class DSPConnection : global::FMOD.HandleBase
	{
		public DSPConnection(global::System.IntPtr raw)
			: base(raw)
		{
		}

		public global::FMOD.RESULT getInput(out global::FMOD.DSP input)
		{
			input = null;
			global::System.IntPtr input2;
			global::FMOD.RESULT result = FMOD5_DSPConnection_GetInput(rawPtr, out input2);
			input = new global::FMOD.DSP(input2);
			return result;
		}

		public global::FMOD.RESULT getOutput(out global::FMOD.DSP output)
		{
			output = null;
			global::System.IntPtr output2;
			global::FMOD.RESULT result = FMOD5_DSPConnection_GetOutput(rawPtr, out output2);
			output = new global::FMOD.DSP(output2);
			return result;
		}

		public global::FMOD.RESULT setMix(float volume)
		{
			return FMOD5_DSPConnection_SetMix(rawPtr, volume);
		}

		public global::FMOD.RESULT getMix(out float volume)
		{
			return FMOD5_DSPConnection_GetMix(rawPtr, out volume);
		}

		public global::FMOD.RESULT setMixMatrix(float[] matrix, int outchannels, int inchannels, int inchannel_hop)
		{
			return FMOD5_DSPConnection_SetMixMatrix(rawPtr, matrix, outchannels, inchannels, inchannel_hop);
		}

		public global::FMOD.RESULT getMixMatrix(float[] matrix, out int outchannels, out int inchannels, int inchannel_hop)
		{
			return FMOD5_DSPConnection_GetMixMatrix(rawPtr, matrix, out outchannels, out inchannels, inchannel_hop);
		}

		public global::FMOD.RESULT getType(out global::FMOD.DSPCONNECTION_TYPE type)
		{
			return FMOD5_DSPConnection_GetType(rawPtr, out type);
		}

		public global::FMOD.RESULT setUserData(global::System.IntPtr userdata)
		{
			return FMOD5_DSPConnection_SetUserData(rawPtr, userdata);
		}

		public global::FMOD.RESULT getUserData(out global::System.IntPtr userdata)
		{
			return FMOD5_DSPConnection_GetUserData(rawPtr, out userdata);
		}

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSPConnection_GetInput(global::System.IntPtr dspconnection, out global::System.IntPtr input);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSPConnection_GetOutput(global::System.IntPtr dspconnection, out global::System.IntPtr output);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSPConnection_SetMix(global::System.IntPtr dspconnection, float volume);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSPConnection_GetMix(global::System.IntPtr dspconnection, out float volume);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSPConnection_SetMixMatrix(global::System.IntPtr dspconnection, float[] matrix, int outchannels, int inchannels, int inchannel_hop);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSPConnection_GetMixMatrix(global::System.IntPtr dspconnection, float[] matrix, out int outchannels, out int inchannels, int inchannel_hop);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSPConnection_GetType(global::System.IntPtr dspconnection, out global::FMOD.DSPCONNECTION_TYPE type);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSPConnection_SetUserData(global::System.IntPtr dspconnection, global::System.IntPtr userdata);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSPConnection_GetUserData(global::System.IntPtr dspconnection, out global::System.IntPtr userdata);
	}
}
