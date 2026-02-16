namespace Kampai.UI.View
{
	public class TikiBarChildWayFinderView : global::Kampai.UI.View.AbstractChildWayFinderView
	{
		protected override string UIName
		{
			get
			{
				return "TikiBarChildWayFinder";
			}
		}

		protected override bool OnCanUpdate()
		{
			if (zoomCameraModel.ZoomedIn)
			{
				if (zoomCameraModel.LastZoomBuildingType != global::Kampai.Game.BuildingZoomType.TIKIBAR)
				{
					return false;
				}
				if (m_Prestige != null && !tikiBarService.IsCharacterSitting(m_Prestige))
				{
					return false;
				}
			}
			return true;
		}
	}
}
