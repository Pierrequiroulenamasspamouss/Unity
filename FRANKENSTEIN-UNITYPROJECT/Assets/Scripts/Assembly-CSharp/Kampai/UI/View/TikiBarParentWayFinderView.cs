namespace Kampai.UI.View
{
	public class TikiBarParentWayFinderView : global::Kampai.UI.View.AbstractParentWayFinderView
	{
		private float zoomPercentage;

		protected override string WayFinderDefaultIcon
		{
			get
			{
				return wayFinderDefinition.TikibarDefaultIcon;
			}
		}

		protected override string UIName
		{
			get
			{
				return "TikiBarParentWayFinder";
			}
		}

		internal void UpdateZoomPercentage(float zoomPercentage)
		{
			this.zoomPercentage = zoomPercentage;
		}

		protected override bool OnCanUpdate()
		{
			if (zoomCameraModel.ZoomedIn)
			{
				foreach (global::Kampai.UI.View.IChildWayFinderView value in ChildrenWayFinders.Values)
				{
					value.SetForceHide(false);
				}
				return false;
			}
			if (zoomPercentage >= wayFinderDefinition.TikibarZoomViewEnabledAt)
			{
				bool flag = false;
				global::System.Collections.Generic.Dictionary<int, global::Kampai.UI.View.IChildWayFinderView>.Enumerator enumerator2 = ChildrenWayFinders.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.Value.IsTargetObjectVisible())
						{
							flag = true;
							break;
						}
					}
				}
				finally
				{
					enumerator2.Dispose();
				}
				if (flag)
				{
					global::System.Collections.Generic.Dictionary<int, global::Kampai.UI.View.IChildWayFinderView>.Enumerator enumerator3 = ChildrenWayFinders.GetEnumerator();
					try
					{
						while (enumerator3.MoveNext())
						{
							enumerator3.Current.Value.SetForceHide(false);
						}
					}
					finally
					{
						enumerator3.Dispose();
					}
					return false;
				}
			}
			global::System.Collections.Generic.Dictionary<int, global::Kampai.UI.View.IChildWayFinderView>.Enumerator enumerator4 = ChildrenWayFinders.GetEnumerator();
			try
			{
				while (enumerator4.MoveNext())
				{
					enumerator4.Current.Value.SetForceHide(true);
				}
			}
			finally
			{
				enumerator4.Dispose();
			}
			return true;
		}
	}
}
