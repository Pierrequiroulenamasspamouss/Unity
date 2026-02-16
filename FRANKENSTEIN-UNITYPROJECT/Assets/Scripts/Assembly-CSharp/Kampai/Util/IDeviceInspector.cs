namespace Kampai.Util
{
	public interface IDeviceInspector
	{
		bool IsSupported(global::UnityEngine.RuntimePlatform platform);

		global::Kampai.Util.TargetPerformance CaluclateTargetPerformance(global::Kampai.Util.DeviceInformation device);
	}
}
