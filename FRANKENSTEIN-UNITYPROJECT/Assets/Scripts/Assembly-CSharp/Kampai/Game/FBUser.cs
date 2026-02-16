namespace Kampai.Game
{
	public class FBUser : SocialUser
	{
		protected bool isDownloading;

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public string facebookID { get; set; }

		public string pictureURL { get; set; }

		public FBUser(string name, string id, string picture)
		{
			base.name = name;
			pictureURL = picture;
			facebookID = id;
		}

		public global::System.Collections.IEnumerator DownloadTexture()
		{
			if (base.image != null)
			{
				yield return null;
			}
			global::UnityEngine.WWW www = new global::UnityEngine.WWW(pictureURL);
			yield return www;
			if (www.texture != null)
			{
				base.image = www.texture;
			}
		}
	}
}
