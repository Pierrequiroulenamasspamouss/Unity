namespace Kampai.UI.View
{
	public class TSMWayFinderView : global::Kampai.UI.View.AbstractQuestWayFinderView
	{
		protected override string UIName
		{
			get
			{
				return "TSMWayFinder";
			}
		}

		protected override string WayFinderDefaultIcon
		{
			get
			{
				return wayFinderDefinition.TSMDefaultIcon;
			}
		}

		protected override bool CanUpdateQuestIcon()
		{
			return false;
		}
	}
}
