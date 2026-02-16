namespace Kampai.UI
{
	internal sealed class KampaiFPSCounter : global::UnityEngine.MonoBehaviour
	{
		private const int filteringKernelSize = 4;

		private float[] filteringKernel = new float[4];

		private int currentSample;

		private global::Kampai.Util.FastTextStreamWriter fpsStream;

		public global::UnityEngine.UI.Text TextComponent { get; set; }

		private float getFilteredFrameTime()
		{
			int num = global::System.Math.Min(4, currentSample);
			float num2 = 0f;
			for (int i = 0; i < num; i++)
			{
				num2 += filteringKernel[i];
			}
			return num2 / (float)num;
		}

		private void Awake()
		{
			fpsStream = new global::Kampai.Util.FastTextStreamWriter(new global::System.IO.FileStream(global::System.IO.Path.Combine(global::Kampai.Util.GameConstants.PERSISTENT_DATA_PATH, "fps.log.txt"), global::System.IO.FileMode.Create, global::System.IO.FileAccess.Write));
		}

		private void Update()
		{
			filteringKernel[currentSample++ % 4] = global::UnityEngine.Time.deltaTime;
			int num = (int)(1f / getFilteredFrameTime());
			TextComponent.text = num.ToString();
			fpsStream.WriteLine(1.0 / (double)global::UnityEngine.Time.deltaTime);
		}

		public void OnDestroy()
		{
			fpsStream.Flush();
			fpsStream.Dispose();
		}
	}
}
