namespace Kampai.Download
{
	public class DownloadRoot : global::strange.extensions.context.impl.ContextView
	{
		public global::UnityEngine.GameObject PanelPrefab;

		private void Awake()
		{
			context = new global::Kampai.Download.DownloadContext(this, true);
			context.Start();
		}
	}
}
