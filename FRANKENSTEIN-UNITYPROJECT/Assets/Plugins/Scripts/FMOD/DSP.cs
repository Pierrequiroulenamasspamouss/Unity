namespace FMOD
{
	public class DSP : global::FMOD.HandleBase
	{
		public DSP(global::System.IntPtr raw)
			: base(raw)
		{
		}

		public global::FMOD.RESULT release()
		{
			global::FMOD.RESULT rESULT = FMOD5_DSP_Release(getRaw());
			if (rESULT == global::FMOD.RESULT.OK)
			{
				rawPtr = global::System.IntPtr.Zero;
			}
			return rESULT;
		}

		public global::FMOD.RESULT getSystemObject(out global::FMOD.System system)
		{
			system = null;
			global::System.IntPtr system2;
			global::FMOD.RESULT result = FMOD5_DSP_GetSystemObject(rawPtr, out system2);
			system = new global::FMOD.System(system2);
			return result;
		}

		public global::FMOD.RESULT addInput(global::FMOD.DSP target, out global::FMOD.DSPConnection connection, global::FMOD.DSPCONNECTION_TYPE type)
		{
			connection = null;
			global::System.IntPtr connection2;
			global::FMOD.RESULT result = FMOD5_DSP_AddInput(rawPtr, target.getRaw(), out connection2, type);
			connection = new global::FMOD.DSPConnection(connection2);
			return result;
		}

		public global::FMOD.RESULT disconnectFrom(global::FMOD.DSP target, global::FMOD.DSPConnection connection)
		{
			return FMOD5_DSP_DisconnectFrom(rawPtr, target.getRaw(), connection.getRaw());
		}

		public global::FMOD.RESULT disconnectAll(bool inputs, bool outputs)
		{
			return FMOD5_DSP_DisconnectAll(rawPtr, inputs, outputs);
		}

		public global::FMOD.RESULT getNumInputs(out int numinputs)
		{
			return FMOD5_DSP_GetNumInputs(rawPtr, out numinputs);
		}

		public global::FMOD.RESULT getNumOutputs(out int numoutputs)
		{
			return FMOD5_DSP_GetNumOutputs(rawPtr, out numoutputs);
		}

		public global::FMOD.RESULT getInput(int index, out global::FMOD.DSP input, out global::FMOD.DSPConnection inputconnection)
		{
			input = null;
			inputconnection = null;
			global::System.IntPtr input2;
			global::System.IntPtr inputconnection2;
			global::FMOD.RESULT result = FMOD5_DSP_GetInput(rawPtr, index, out input2, out inputconnection2);
			input = new global::FMOD.DSP(input2);
			inputconnection = new global::FMOD.DSPConnection(inputconnection2);
			return result;
		}

		public global::FMOD.RESULT getOutput(int index, out global::FMOD.DSP output, out global::FMOD.DSPConnection outputconnection)
		{
			output = null;
			outputconnection = null;
			global::System.IntPtr output2;
			global::System.IntPtr outputconnection2;
			global::FMOD.RESULT result = FMOD5_DSP_GetOutput(rawPtr, index, out output2, out outputconnection2);
			output = new global::FMOD.DSP(output2);
			outputconnection = new global::FMOD.DSPConnection(outputconnection2);
			return result;
		}

		public global::FMOD.RESULT setActive(bool active)
		{
			return FMOD5_DSP_SetActive(rawPtr, active);
		}

		public global::FMOD.RESULT getActive(out bool active)
		{
			return FMOD5_DSP_GetActive(rawPtr, out active);
		}

		public global::FMOD.RESULT setBypass(bool bypass)
		{
			return FMOD5_DSP_SetBypass(rawPtr, bypass);
		}

		public global::FMOD.RESULT getBypass(out bool bypass)
		{
			return FMOD5_DSP_GetBypass(rawPtr, out bypass);
		}

		public global::FMOD.RESULT setWetDryMix(float wet, float dry)
		{
			return FMOD5_DSP_SetWetDryMix(rawPtr, wet, dry);
		}

		public global::FMOD.RESULT getWetDryMix(out float wet, out float dry)
		{
			return FMOD5_DSP_GetWetDryMix(rawPtr, out wet, out dry);
		}

		public global::FMOD.RESULT setChannelFormat(global::FMOD.CHANNELMASK channelmask, int numchannels, global::FMOD.SPEAKERMODE source_speakermode)
		{
			return FMOD5_DSP_SetChannelFormat(rawPtr, channelmask, numchannels, source_speakermode);
		}

		public global::FMOD.RESULT getChannelFormat(out global::FMOD.CHANNELMASK channelmask, out int numchannels, out global::FMOD.SPEAKERMODE source_speakermode)
		{
			return FMOD5_DSP_GetChannelFormat(rawPtr, out channelmask, out numchannels, out source_speakermode);
		}

		public global::FMOD.RESULT getOutputChannelFormat(global::FMOD.CHANNELMASK inmask, int inchannels, global::FMOD.SPEAKERMODE inspeakermode, out global::FMOD.CHANNELMASK outmask, out int outchannels, out global::FMOD.SPEAKERMODE outspeakermode)
		{
			return FMOD5_DSP_GetOutputChannelFormat(rawPtr, inmask, inchannels, inspeakermode, out outmask, out outchannels, out outspeakermode);
		}

		public global::FMOD.RESULT reset()
		{
			return FMOD5_DSP_Reset(rawPtr);
		}

		public global::FMOD.RESULT setParameterFloat(int index, float value)
		{
			return FMOD5_DSP_SetParameterFloat(rawPtr, index, value);
		}

		public global::FMOD.RESULT setParameterInt(int index, int value)
		{
			return FMOD5_DSP_SetParameterInt(rawPtr, index, value);
		}

		public global::FMOD.RESULT setParameterBool(int index, bool value)
		{
			return FMOD5_DSP_SetParameterBool(rawPtr, index, value);
		}

		public global::FMOD.RESULT setParameterData(int index, byte[] data)
		{
			return FMOD5_DSP_SetParameterData(rawPtr, index, global::System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(data, 0), (uint)data.Length);
		}

		public global::FMOD.RESULT getParameterFloat(int index, out float value)
		{
			global::System.IntPtr zero = global::System.IntPtr.Zero;
			return FMOD5_DSP_GetParameterFloat(rawPtr, index, out value, zero, 0);
		}

		public global::FMOD.RESULT getParameterInt(int index, out int value)
		{
			global::System.IntPtr zero = global::System.IntPtr.Zero;
			return FMOD5_DSP_GetParameterInt(rawPtr, index, out value, zero, 0);
		}

		public global::FMOD.RESULT getParameterBool(int index, out bool value)
		{
			return FMOD5_DSP_GetParameterBool(rawPtr, index, out value, global::System.IntPtr.Zero, 0);
		}

		public global::FMOD.RESULT getParameterData(int index, out global::System.IntPtr data, out uint length)
		{
			return FMOD5_DSP_GetParameterData(rawPtr, index, out data, out length, global::System.IntPtr.Zero, 0);
		}

		public global::FMOD.RESULT getNumParameters(out int numparams)
		{
			return FMOD5_DSP_GetNumParameters(rawPtr, out numparams);
		}

		public global::FMOD.RESULT getParameterInfo(int index, out global::FMOD.DSP_PARAMETER_DESC desc)
		{
			global::System.IntPtr desc2;
			global::FMOD.RESULT rESULT = FMOD5_DSP_GetParameterInfo(rawPtr, index, out desc2);
			if (rESULT == global::FMOD.RESULT.OK)
			{
				desc = (global::FMOD.DSP_PARAMETER_DESC)global::System.Runtime.InteropServices.Marshal.PtrToStructure(desc2, typeof(global::FMOD.DSP_PARAMETER_DESC));
			}
			else
			{
				desc = default(global::FMOD.DSP_PARAMETER_DESC);
			}
			return rESULT;
		}

		public global::FMOD.RESULT getDataParameterIndex(int datatype, out int index)
		{
			return FMOD5_DSP_GetDataParameterIndex(rawPtr, datatype, out index);
		}

		public global::FMOD.RESULT showConfigDialog(global::System.IntPtr hwnd, bool show)
		{
			return FMOD5_DSP_ShowConfigDialog(rawPtr, hwnd, show);
		}

		public global::FMOD.RESULT getInfo(global::System.Text.StringBuilder name, out uint version, out int channels, out int configwidth, out int configheight)
		{
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(32);
			global::FMOD.RESULT result = FMOD5_DSP_GetInfo(rawPtr, intPtr, out version, out channels, out configwidth, out configheight);
			global::FMOD.StringMarshalHelper.NativeToBuilder(name, intPtr);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return result;
		}

		public global::FMOD.RESULT getType(out global::FMOD.DSP_TYPE type)
		{
			return FMOD5_DSP_GetType(rawPtr, out type);
		}

		public global::FMOD.RESULT getIdle(out bool idle)
		{
			return FMOD5_DSP_GetIdle(rawPtr, out idle);
		}

		public global::FMOD.RESULT setUserData(global::System.IntPtr userdata)
		{
			return FMOD5_DSP_SetUserData(rawPtr, userdata);
		}

		public global::FMOD.RESULT getUserData(out global::System.IntPtr userdata)
		{
			return FMOD5_DSP_GetUserData(rawPtr, out userdata);
		}

		public global::FMOD.RESULT setMeteringEnabled(bool inputEnabled, bool outputEnabled)
		{
			return FMOD5_DSP_SetMeteringEnabled(rawPtr, inputEnabled, outputEnabled);
		}

		public global::FMOD.RESULT getMeteringEnabled(out bool inputEnabled, out bool outputEnabled)
		{
			return FMOD5_DSP_GetMeteringEnabled(rawPtr, out inputEnabled, out outputEnabled);
		}

		public global::FMOD.RESULT getMeteringInfo(out global::FMOD.DSP_METERING_INFO info)
		{
			return FMOD5_DSP_GetMeteringInfo(rawPtr, out info);
		}

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_Release(global::System.IntPtr dsp);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetSystemObject(global::System.IntPtr dsp, out global::System.IntPtr system);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_AddInput(global::System.IntPtr dsp, global::System.IntPtr target, out global::System.IntPtr connection, global::FMOD.DSPCONNECTION_TYPE type);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_DisconnectFrom(global::System.IntPtr dsp, global::System.IntPtr target, global::System.IntPtr connection);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_DisconnectAll(global::System.IntPtr dsp, bool inputs, bool outputs);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetNumInputs(global::System.IntPtr dsp, out int numinputs);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetNumOutputs(global::System.IntPtr dsp, out int numoutputs);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetInput(global::System.IntPtr dsp, int index, out global::System.IntPtr input, out global::System.IntPtr inputconnection);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetOutput(global::System.IntPtr dsp, int index, out global::System.IntPtr output, out global::System.IntPtr outputconnection);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_SetActive(global::System.IntPtr dsp, bool active);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetActive(global::System.IntPtr dsp, out bool active);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_SetBypass(global::System.IntPtr dsp, bool bypass);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetBypass(global::System.IntPtr dsp, out bool bypass);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_SetWetDryMix(global::System.IntPtr dsp, float wet, float dry);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetWetDryMix(global::System.IntPtr dsp, out float wet, out float dry);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_SetChannelFormat(global::System.IntPtr dsp, global::FMOD.CHANNELMASK channelmask, int numchannels, global::FMOD.SPEAKERMODE source_speakermode);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetChannelFormat(global::System.IntPtr dsp, out global::FMOD.CHANNELMASK channelmask, out int numchannels, out global::FMOD.SPEAKERMODE source_speakermode);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetOutputChannelFormat(global::System.IntPtr dsp, global::FMOD.CHANNELMASK inmask, int inchannels, global::FMOD.SPEAKERMODE inspeakermode, out global::FMOD.CHANNELMASK outmask, out int outchannels, out global::FMOD.SPEAKERMODE outspeakermode);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_Reset(global::System.IntPtr dsp);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_SetParameterFloat(global::System.IntPtr dsp, int index, float value);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_SetParameterInt(global::System.IntPtr dsp, int index, int value);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_SetParameterBool(global::System.IntPtr dsp, int index, bool value);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_SetParameterData(global::System.IntPtr dsp, int index, global::System.IntPtr data, uint length);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetParameterFloat(global::System.IntPtr dsp, int index, out float value, global::System.IntPtr valuestr, int valuestrlen);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetParameterInt(global::System.IntPtr dsp, int index, out int value, global::System.IntPtr valuestr, int valuestrlen);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetParameterBool(global::System.IntPtr dsp, int index, out bool value, global::System.IntPtr valuestr, int valuestrlen);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetParameterData(global::System.IntPtr dsp, int index, out global::System.IntPtr data, out uint length, global::System.IntPtr valuestr, int valuestrlen);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetNumParameters(global::System.IntPtr dsp, out int numparams);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetParameterInfo(global::System.IntPtr dsp, int index, out global::System.IntPtr desc);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetDataParameterIndex(global::System.IntPtr dsp, int datatype, out int index);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_ShowConfigDialog(global::System.IntPtr dsp, global::System.IntPtr hwnd, bool show);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetInfo(global::System.IntPtr dsp, global::System.IntPtr name, out uint version, out int channels, out int configwidth, out int configheight);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetType(global::System.IntPtr dsp, out global::FMOD.DSP_TYPE type);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetIdle(global::System.IntPtr dsp, out bool idle);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_SetUserData(global::System.IntPtr dsp, global::System.IntPtr userdata);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_DSP_GetUserData(global::System.IntPtr dsp, out global::System.IntPtr userdata);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		public static extern global::FMOD.RESULT FMOD5_DSP_SetMeteringEnabled(global::System.IntPtr dsp, bool inputEnabled, bool outputEnabled);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		public static extern global::FMOD.RESULT FMOD5_DSP_GetMeteringEnabled(global::System.IntPtr dsp, out bool inputEnabled, out bool outputEnabled);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		public static extern global::FMOD.RESULT FMOD5_DSP_GetMeteringInfo(global::System.IntPtr dsp, out global::FMOD.DSP_METERING_INFO dspInfo);
	}
}
