namespace Kampai.Download
{
	public class DownloadPanelView : global::strange.extensions.mediation.impl.View
	{
		public global::UnityEngine.GameObject progressBar;

		public global::UnityEngine.GameObject wiFiPopup;

		public void ShowNoWiFi(bool show)
		{
			progressBar.SetActive(!show);
			wiFiPopup.SetActive(show);
		}
	}
}
