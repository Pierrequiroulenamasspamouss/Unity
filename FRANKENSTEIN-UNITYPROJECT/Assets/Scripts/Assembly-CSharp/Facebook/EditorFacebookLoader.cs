namespace Facebook
{
	public class EditorFacebookLoader : FB.CompiledFacebookLoader
	{
		protected override global::Facebook.IFacebook fb
		{
			get
			{
				return global::Facebook.FBComponentFactory.GetComponent<global::Facebook.EditorFacebook>();
			}
		}
	}
}
