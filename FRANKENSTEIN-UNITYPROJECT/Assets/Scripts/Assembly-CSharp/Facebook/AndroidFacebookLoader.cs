namespace Facebook
{
	public class AndroidFacebookLoader : FB.CompiledFacebookLoader
	{
		protected override global::Facebook.IFacebook fb
		{
			get
			{
				return global::Facebook.FBComponentFactory.GetComponent<global::Facebook.AndroidFacebook>();
			}
		}
	}
}
