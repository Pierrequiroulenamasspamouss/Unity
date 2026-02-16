namespace Kampai.Game
{
	public class FBDownloadPicture
	{
		public delegate void OnCompleteFn(global::UnityEngine.Texture texture, string id);

		public global::Kampai.Util.ILogger logger;

		protected global::Kampai.Game.FBDownloadPicture.OnCompleteFn OnComplete;

		public string fbID;

		public FBDownloadPicture(global::Kampai.Util.ILogger l)
		{
			logger = l;
		}

		public global::System.Collections.IEnumerator GetPicture(string id, string accessToken, int width, int height, global::Kampai.Game.FBDownloadPicture.OnCompleteFn fn)
		{
			OnComplete = fn;
			fbID = id;
			string url = string.Format("https://graph.facebook.com/{0}/picture?width={1}&height={2}&access_token={3}", id, width, height, accessToken);
			global::UnityEngine.WWW www = new global::UnityEngine.WWW(url);
			yield return www;
			if (logger != null)
			{
				logger.Info("FBDownloadPicture: " + url);
			}
			if (www.texture != null && www.texture.height > 8 && www.texture.height > 8)
			{
				OnComplete(www.texture, fbID);
				yield break;
			}
			if (logger != null)
			{
				logger.Info("Facebook: Download picture failed with error " + www.error);
			}
			OnComplete(null, fbID);
		}
	}
}
