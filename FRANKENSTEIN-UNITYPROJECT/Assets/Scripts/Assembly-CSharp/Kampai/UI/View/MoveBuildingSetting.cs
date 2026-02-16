namespace Kampai.UI.View
{
	public class MoveBuildingSetting : global::Kampai.UI.View.WorldToGlassUISettings
	{
		public int Mask { get; private set; }

		public MoveBuildingSetting(int trackedId, int Mask)
			: base(trackedId)
		{
			this.Mask = Mask;
		}
	}
}
