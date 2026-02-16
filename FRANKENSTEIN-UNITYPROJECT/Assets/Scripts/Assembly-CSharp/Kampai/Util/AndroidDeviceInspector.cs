namespace Kampai.Util
{
	public class AndroidDeviceInspector : global::Kampai.Util.IDeviceInspector
	{
		public bool IsSupported(global::UnityEngine.RuntimePlatform platform)
		{
			return platform == global::UnityEngine.RuntimePlatform.Android;
		}

		public global::Kampai.Util.TargetPerformance CaluclateTargetPerformance(global::Kampai.Util.DeviceInformation device)
		{
			double num = device.graphicsPixelFillrate;
			if (num < 0.0)
			{
				num = ((device.graphicsShaderLevel < 10) ? 1000.0 : ((device.graphicsShaderLevel < 20) ? 1300.0 : ((device.graphicsShaderLevel >= 30) ? 3000.0 : 2000.0)));
				num *= (double)device.processorCount / 4.0;
				num *= (double)device.ram / 1024.0;
				num *= (double)device.vram / 256.0;
			}
			double num2 = (double)(device.screenWidth * device.screenHeight + 120000) * 3E-05;
			global::Kampai.Util.TargetPerformance result = global::Kampai.Util.TargetPerformance.UNSUPPORTED;
			double num3 = num / num2;
			double[] array = new double[4] { 50.0, 100.0, 250.0, 1000.0 };
			for (int i = 0; i < array.Length && num3 > array[i]; i++)
			{
				result = (global::Kampai.Util.TargetPerformance)i;
			}
			if (device.model.Contains("Nexus 9"))
			{
				result = global::Kampai.Util.TargetPerformance.HIGH;
			}
			return result;
		}
	}
}
