namespace Kampai.Util
{
	public class EditorDeviceInspector : global::Kampai.Util.IDeviceInspector
	{
		public bool IsSupported(global::UnityEngine.RuntimePlatform platform)
		{
			return platform == global::UnityEngine.RuntimePlatform.OSXEditor || platform == global::UnityEngine.RuntimePlatform.WindowsEditor;
		}

		public global::Kampai.Util.TargetPerformance CaluclateTargetPerformance(global::Kampai.Util.DeviceInformation device)
		{
			return global::Kampai.Util.TargetPerformance.HIGH;
		}
	}
}
