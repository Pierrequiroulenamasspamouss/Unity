namespace Facebook
{
	public class IOSFacebookLoader : FB.CompiledFacebookLoader
	{
		protected override global::Facebook.IFacebook fb
		{
			get
			{
				return global::Facebook.FBComponentFactory.GetComponent<global::Facebook.IOSFacebook>();
			}
		}
	}
}
