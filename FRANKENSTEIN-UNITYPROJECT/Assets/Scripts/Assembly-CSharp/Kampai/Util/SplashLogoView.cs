namespace Kampai.Util
{
	public class SplashLogoView : global::UnityEngine.MonoBehaviour
	{
		public global::UnityEngine.GameObject logo;

		public void Awake()
		{
			if (logo != null) logo.SetActive(false);
		}

		public void EnableLogo()
		{
			if (logo != null) logo.SetActive(true);
		}
	}
}
