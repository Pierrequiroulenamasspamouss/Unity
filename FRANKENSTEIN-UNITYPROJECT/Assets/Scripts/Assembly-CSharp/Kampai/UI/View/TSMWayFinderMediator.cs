namespace Kampai.UI.View
{
	public class TSMWayFinderMediator : global::Kampai.UI.View.AbstractQuestWayFinderMediator
	{
		[Inject]
		public global::Kampai.UI.View.TSMWayFinderView TSMWayFinderView { get; set; }

		public override global::Kampai.UI.View.IWayFinderView View
		{
			get
			{
				return TSMWayFinderView;
			}
		}
	}
}
