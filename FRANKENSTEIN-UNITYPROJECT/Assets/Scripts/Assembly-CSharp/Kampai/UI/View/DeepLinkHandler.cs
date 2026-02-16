namespace Kampai.UI.View
{
	public class DeepLinkHandler : global::UnityEngine.MonoBehaviour
	{
		private bool waitingToProcessLink;

		public global::Kampai.Util.ILogger logger { get; set; }

		public global::Kampai.UI.View.MoveBuildMenuSignal moveBuildMenuSignal { get; set; }

		public global::Kampai.UI.View.ShowPremiumStoreSignal showPremiumStoreSignal { get; set; }

		public global::Kampai.UI.View.ShowGrindStoreSignal showGrindStoreSignal { get; set; }

		private void Awake()
		{
			waitingToProcessLink = true;
			StartCoroutine(WaitToProcessLink());
		}

		public virtual void OnDeepLink(string uriString)
		{
			if (!waitingToProcessLink)
			{
				waitingToProcessLink = true;
				StartCoroutine(WaitToProcessLink());
			}
		}

		private global::System.Collections.IEnumerator WaitToProcessLink()
		{
			yield return new global::UnityEngine.WaitForSeconds(0.5f);
			ProcessDeepLink();
			waitingToProcessLink = false;
		}

		private void RemoveLinkFromPrefs()
		{
			global::UnityEngine.PlayerPrefs.DeleteKey("DeepLink");
			global::UnityEngine.PlayerPrefs.Save();
		}

		internal void ProcessDeepLink()
		{
			string text = global::UnityEngine.PlayerPrefs.GetString("DeepLink");
			if (text.Length == 0)
			{
				RemoveLinkFromPrefs();
				return;
			}
			global::System.Uri uri = new global::System.Uri(text);
			logger.Debug("uri.Host: {0}", uri.Host);
			if (uri.Host != "deeplink")
			{
				logger.Error("Not a deeplink url: {0}", text);
				RemoveLinkFromPrefs();
				return;
			}
			string absolutePath = uri.AbsolutePath;
			string[] array = absolutePath.Split('/');
			if (array.Length < 3)
			{
				logger.Error("Incorrect deeplink url: {0}", text);
				RemoveLinkFromPrefs();
				return;
			}
			string text2 = array[1];
			logger.Debug("action = {0}", text2);
			switch (text2)
			{
			case "view":
			{
				string text3 = array[2];
				logger.Debug("target = {0}", text3);
				switch (text3)
				{
				case "build_menu":
					moveBuildMenuSignal.Dispatch(true);
					break;
				case "grind_store":
					showGrindStoreSignal.Dispatch();
					break;
				case "premium_store":
					showPremiumStoreSignal.Dispatch();
					break;
				default:
					logger.Error("Unsupported target: {0}", text3);
					break;
				}
				break;
			}
			default:
				logger.Error("Unsupported action: {0}", text2);
				break;
			}
			RemoveLinkFromPrefs();
		}
	}
}
