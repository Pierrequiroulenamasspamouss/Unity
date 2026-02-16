namespace Kampai.Main
{
	public class PostDownloadManifestCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Common.SetupManifestSignal setupManifestSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Common.ReconcileDLCSignal reconcileDLCSignal { get; set; }

		[Inject]
		public global::Kampai.Download.DLCModel dlcModel { get; set; }

		[Inject]
		public global::Kampai.Common.IManifestService manifestService { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Common.CheckAvailableStorageSignal checkAvailableStorageSignal { get; set; }

		[Inject]
		public global::Kampai.Game.LoginUserSignal loginSignal { get; set; }

		[Inject]
		public global::Kampai.Download.ShowDLCPanelSignal showDLCPanelSignal { get; set; }

		[Inject]
		public global::Kampai.Download.LaunchDownloadSignal launchDownloadSignal { get; set; }

		[Inject]
		public global::Kampai.Common.IVideoService videoService { get; set; }

		[Inject]
		public global::Kampai.Download.IBackgroundDownloadDlcService backgroundDownloadDlcService { get; set; }

		[Inject]
		public global::Kampai.Game.IConfigurationsService configurationsService { get; set; }

		public override void Execute()
		{
			global::Kampai.Util.TimeProfiler.EndSection("retrieve manifest");
			logger.Info("PostDownloadManifestCommand setup manifest");
			telemetryService.Send_Telemetry_EVT_USER_GAME_LOAD_FUNNEL("30 - Loaded DLC Manifest", playerService.SWRVEGroup);
			setupManifestSignal.Dispatch();
			CompleteManifestDownload();
		}

		private void CompleteManifestDownload()
		{
			logger.Debug("[Manifest] CompleteManifestDownload");
			routineRunner.StartCoroutine(WaitAFrame(delegate
			{
				reconcileDLCSignal.Dispatch(true);
				if (dlcModel.NeededBundles.Count > 0)
				{
					logger.Debug("[Manifest] Bundles Needed: {0}", dlcModel.NeededBundles.Count);
					checkAvailableStorageSignal.Dispatch(global::Kampai.Util.GameConstants.PERSISTENT_DATA_PATH, dlcModel.TotalSize, TryPlayVideoStartDownloadDLC);
				}
				else
				{
					if (ShouldPlayVideo())
					{
						PlayVideo(null);
					}
					loginSignal.Dispatch();
				}
			}));
		}

		private void TryPlayVideoStartDownloadDLC()
		{
			logger.Debug("[Manifest] TryPlayVideoStartDownloadDLC");
			if (ShouldPlayVideo())
			{
				logger.Debug("[Manifest] Starting video stream");
				PlayVideo(VideoStartedPlayingCallback);
			}
			else
			{
				logger.Debug("[Manifest] DownloadDlcInForeground");
				DownloadDlcInForeground();
			}
		}

		private void VideoStartedPlayingCallback()
		{
			logger.Info("[Manifest] Video playing; starting background DLC");
			backgroundDownloadDlcService.Init();
			global::System.Threading.ThreadPool.QueueUserWorkItem(delegate
			{
				backgroundDownloadDlcService.Run();
			});
			logger.Info("[Manifest] Waiting for video to finish");
			routineRunner.StartCoroutine(StopBackgroundDlcDownloading(6));
		}

		private void DownloadDlcInForeground()
		{
			logger.Info("[Manifest] Downloading DLC in foreground");
			showDLCPanelSignal.Dispatch(true);
			launchDownloadSignal.Dispatch(false);
		}

		private global::System.Collections.IEnumerator StopBackgroundDlcDownloading(int frames)
		{
			while (frames-- > 0)
			{
				yield return new global::UnityEngine.WaitForEndOfFrame();
			}
			logger.Info("[Manifest] PostDownloadManifestCommand.StopBackgroundDlcDownloading(): stopping downloading and wait until it finished");
			backgroundDownloadDlcService.Stop();
			while (!backgroundDownloadDlcService.Stopped)
			{
				yield return null;
			}
			logger.Info("[Manifest] PostDownloadManifestCommand.StopBackgroundDlcDownloading(): background downloading finished, reconcile DLC again.");
			CompleteManifestDownload();
		}

		private bool ShouldPlayVideo()
		{
			if (!global::UnityEngine.PlayerPrefs.HasKey("intro_video_played"))
			{
				logger.Info("[Manifest] PostDownloadManifestCommand.ShouldPlayVideo: {0}", true);
				return true;
			}
			int num = global::UnityEngine.PlayerPrefs.GetInt("intro_video_played");
			logger.Info("[Manifest] PostDownloadManifestCommand.ShouldPlayVideo: {0}", num == 0);
			return num == 0;
		}

		private void PlayVideo(global::System.Action callback)
		{
			global::UnityEngine.PlayerPrefs.SetInt("intro_video_played", 1);
			bool dEBUG_ENABLED = global::Kampai.Util.GameConstants.StaticConfig.DEBUG_ENABLED;
			logger.Debug("[Manifest] PostDownloadManifestCommand.PlayVideo skippable: {0}", dEBUG_ENABLED);
			videoService.playIntro(false, dEBUG_ENABLED, callback, configurationsService.GetConfigurations().videoUri);
		}

		private global::System.Collections.IEnumerator WaitAFrame(global::System.Action a)
		{
			yield return null;
			a();
		}

		private global::System.Collections.IEnumerator CopyStreamingAssets()
		{
			global::System.Collections.Generic.HashSet<global::Kampai.Util.BundleInfo> bundles = manifestService.GetBundles();
			logger.Info("[Manifest] Copying streaming assets: {0}", bundles.Count.ToString());
			string StreamingAssetsDLCPath = global::System.IO.Path.Combine(global::UnityEngine.Application.streamingAssetsPath, "dlc");
			if (!global::System.IO.Directory.Exists(global::Kampai.Util.GameConstants.DLC_PATH))
			{
				global::System.IO.Directory.CreateDirectory(global::Kampai.Util.GameConstants.DLC_PATH);
			}
			foreach (global::Kampai.Util.BundleInfo bundle in bundles)
			{
				string dlcPathName = global::System.IO.Path.Combine(global::Kampai.Util.GameConstants.DLC_PATH, bundle.name + ".unity3d");
				if (!global::System.IO.File.Exists(dlcPathName))
				{
					byte[] results = null;
					string filePath = global::System.IO.Path.Combine(StreamingAssetsDLCPath, bundle.originalName);
					logger.Debug("[Manifest] Copying: {0}", filePath);
					if (filePath.Contains("://"))
					{
						global::UnityEngine.WWW www = new global::UnityEngine.WWW(filePath);
						yield return www;
						if (www.error == null)
						{
							results = www.bytes;
						}
						else
						{
							logger.Info("[Manifest] Error copying {0} from WWW", filePath);
						}
					}
					else if (global::System.IO.File.Exists(filePath))
					{
						results = global::System.IO.File.ReadAllBytes(filePath);
					}
					if (results != null)
					{
						logger.Debug("[Manifest] Saving {0} to {1} ({2} bytes)", bundle.originalName, dlcPathName, results.Length);
						global::System.IO.FileStream file = new global::System.IO.FileStream(dlcPathName, global::System.IO.FileMode.Create, global::System.IO.FileAccess.Write);
						file.Write(results, 0, results.Length);
						file.Close();
						manifestService.AddBundle(bundle);
					}
					else
					{
						logger.Info("[Manifest] No data for {0}", bundle.originalName);
					}
				}
				else
				{
					logger.Info("[Manifest] {0} already exists.", bundle.name);
				}
				yield return null;
			}
			CompleteManifestDownload();
		}
	}
}
