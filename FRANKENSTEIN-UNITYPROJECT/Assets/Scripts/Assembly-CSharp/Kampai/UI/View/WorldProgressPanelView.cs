namespace Kampai.UI.View
{
	public class WorldProgressPanelView : global::strange.extensions.mediation.impl.View
	{
		private global::System.Collections.Generic.Dictionary<int, global::Kampai.UI.View.ProgressBarView> trackedProgressBar;

		private global::Kampai.UI.View.MoveBuildingMenuView menuView;

		private global::Kampai.Util.ILogger logger;

		internal void Init(global::Kampai.Util.ILogger logger)
		{
			this.logger = logger;
			menuView = null;
			trackedProgressBar = new global::System.Collections.Generic.Dictionary<int, global::Kampai.UI.View.ProgressBarView>();
		}

		internal void Cleanup()
		{
			if (trackedProgressBar == null)
			{
				return;
			}
			foreach (global::Kampai.UI.View.ProgressBarView value in trackedProgressBar.Values)
			{
				if (value != null)
				{
					global::UnityEngine.Object.Destroy(value.gameObject);
				}
			}
			trackedProgressBar.Clear();
		}

		internal global::UnityEngine.GameObject CreateOrUpdateMoveBuildingMenu(global::Kampai.UI.View.MoveBuildingSetting moveBuildingSettings)
		{
			if (menuView == null)
			{
				menuView = global::Kampai.UI.View.WorldToGlassUIBuilder.Build<global::Kampai.UI.View.MoveBuildingMenuView>("screen_MoveBuilding", base.transform, moveBuildingSettings, logger);
			}
			menuView.SetButtonState(moveBuildingSettings.Mask);
			return menuView.gameObject;
		}

		internal void RemoveMoveBuildingMenu()
		{
			if (menuView != null)
			{
				global::UnityEngine.Object.Destroy(menuView.gameObject);
				menuView = null;
			}
		}

		internal void CreateProgressBar(global::Kampai.UI.View.ProgressBarSettings progressBarSettings)
		{
			int trackedId = progressBarSettings.TrackedId;
			logger.Info("Creating Progress Bar with id: {0}", trackedId);
			global::Kampai.UI.View.ProgressBarView progressBarView = null;
			if ((progressBarView = GetProgressBar(trackedId)) != null)
			{
				logger.Info("Progress Bar with id: {0} already exists, ignoring", trackedId);
			}
			else
			{
				progressBarView = global::Kampai.UI.View.WorldToGlassUIBuilder.Build<global::Kampai.UI.View.ProgressBarView>("cmp_BuildingProgress", base.transform, progressBarSettings, logger);
				trackedProgressBar.Add(trackedId, progressBarView);
			}
		}

		private bool ContainsProgressBar(int trackedId)
		{
			if (trackedProgressBar != null && trackedProgressBar.ContainsKey(trackedId))
			{
				return true;
			}
			return false;
		}

		private global::Kampai.UI.View.ProgressBarView GetProgressBar(int trackedId)
		{
			if (ContainsProgressBar(trackedId))
			{
				return trackedProgressBar[trackedId];
			}
			return null;
		}

		internal void RemoveProgressBar(int trackedId)
		{
			logger.Info("Removing Progress Bar with id: {0}", trackedId);
			if (ContainsProgressBar(trackedId))
			{
				global::Kampai.UI.View.ProgressBarView progressBarView = trackedProgressBar[trackedId];
				trackedProgressBar.Remove(trackedId);
				global::UnityEngine.Object.Destroy(progressBarView.gameObject);
			}
			else
			{
				logger.Warning("Progress Bar with id: {0} will not be removed since it doesn't exist!", trackedId);
			}
		}
	}
}
