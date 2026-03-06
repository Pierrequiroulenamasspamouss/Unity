namespace Kampai.Common
{
	public class VideoService : global::Kampai.Common.IVideoService
	{
		private global::Kampai.Common.VideoRequest request;

		[Inject]
		public global::Kampai.Util.IInvokerService invoker { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Util.IInvokerService invokerService { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.Splash.SplashProgressUpdateSignal splashProgressUpdateSignal { get; set; }

		[Inject]
		public global::Kampai.Splash.SplashProgressResetSignal splashProgressResetSignal { get; set; }

		public void playVideo(string urlOrFilename, bool showControls, bool closeOnTouch)
		{
			logger.Info("[Video] Playing {0}", urlOrFilename);
			global::UnityEngine.FullScreenMovieControlMode controlMode = global::UnityEngine.FullScreenMovieControlMode.Hidden;
			if (showControls)
			{
				controlMode = global::UnityEngine.FullScreenMovieControlMode.Minimal;
			}
			else if (closeOnTouch)
			{
				controlMode = global::UnityEngine.FullScreenMovieControlMode.CancelOnInput;
			}
#if UNITY_EDITOR || UNITY_STANDALONE
			global::UnityEngine.Application.OpenURL("file://" + urlOrFilename);
#else
			global::UnityEngine.Handheld.PlayFullScreenMovie(urlOrFilename, global::UnityEngine.Color.black, controlMode, global::UnityEngine.FullScreenMovieScalingMode.AspectFit);
#endif
		}

		public void playIntro(bool showControls, bool closeOnTouch, global::System.Action videoPlayingCallback, string videoUriTemplate = null)
		{
			if (request == null)
			{
				request = new global::Kampai.Common.VideoRequest();
				request.showControls = showControls;
				request.closeOnTouch = closeOnTouch;
				request.callback = videoPlayingCallback;
				request.videoUriTemplate = videoUriTemplate;
				string text = global::Kampai.Main.HALService.GetResourcePath(global::Kampai.Util.Native.GetDeviceLanguage());
				if (string.IsNullOrEmpty(text))
				{
					text = global::Kampai.Main.HALService.GetResourcePath("en");
				}
				if (string.IsNullOrEmpty(text))
				{
					text = "EN-US";
				}
				request.locale = text;
				fetchAndPlayIntroVideo();
			}
			else
			{
				logger.Error("[Video] Intro already playing");
			}
		}

		private string IntroVideoUri(string locale)
		{
			string format = ((request.videoUriTemplate != null) ? request.videoUriTemplate : "https://eaassets-a.akamaihd.net/cdn-kampai/videos/intro_{0}.mp4");
			return string.Format(format, locale);
		}

		private void fetchAndPlayIntroVideo()
		{
			if (request == null)
			{
				logger.Error("[Video] Null request for intro");
			}
			else if (IsIntroCached(request.locale))
			{
				logger.Info("[Video] Cached: {0}", request.locale);
				if (request.callback != null)
				{
					logger.Info("[Video] CALLBACK");
					request.callback();
				}
				bool showControls = request.showControls;
				bool closeOnTouch = request.closeOnTouch;
				request = null;
				routineRunner.StartCoroutine(ResetProgressBar());
				playVideo(global::Kampai.Util.GameConstants.VIDEO_PATH, showControls, closeOnTouch);
				telemetryService.Send_Telemetry_EVT_USER_GAME_LOAD_FUNNEL("50 - Played Intro Video", "anyVariant");
			}
			else if (request.retries-- > 0)
			{
				string text = IntroVideoUri(request.locale);
				logger.Info("[Video] Requesting: {0}", text);
				request.networkRequest = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.FileDownloadRequest(text).WithOutputFile(global::Kampai.Util.GameConstants.VIDEO_PATH);
				request.inFlight = true;
				request.progressBarStart = request.progressBarNow;
				request.networkRequest.Execute(RequestCallbackProxy);
				routineRunner.StartCoroutine(WaitForNetworkRequest());
			}
			else
			{
				logger.Error("[Video] Giving up video download");
				request = null;
			}
		}

		private global::System.Collections.IEnumerator ResetProgressBar()
		{
			yield return null;
			splashProgressResetSignal.Dispatch();
		}

		private global::System.Collections.IEnumerator WaitForNetworkRequest()
		{
			yield return new global::UnityEngine.WaitForEndOfFrame();
			if (request != null && request.inFlight)
			{
				UpdateProgressBar(request.networkRequest);
				routineRunner.StartCoroutine(WaitForNetworkRequest());
			}
		}

		private bool IsIntroCached(string resourcePath)
		{
			return global::UnityEngine.PlayerPrefs.HasKey("VideoCache") && IntroVideoUri(resourcePath).Equals(global::UnityEngine.PlayerPrefs.GetString("VideoCache"));
		}

		private void SetIntroCachedForLocale(string locale)
		{
			global::UnityEngine.PlayerPrefs.SetString("VideoCache", IntroVideoUri(locale));
		}

		private void RequestCallbackProxy(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
		{
			invoker.Add(delegate
			{
				RequestCallback(response);
			});
		}

		private void RequestCallback(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
		{
			request.inFlight = false;
			if (response.Success)
			{
				telemetryService.Send_Telemetry_EVT_USER_GAME_DOWNLOAD_FUNNEL(response.Request.Uri, response.DownloadTime, response.ContentLength);
				telemetryService.Send_Telemetry_EVT_USER_GAME_LOAD_FUNNEL("40 - Downloaded Intro Video", "anyVariant");
				SetIntroCachedForLocale(request.locale);
			}
			else
			{
				logger.Error("[Video] Error fetching video {0}", response.Code);
			}
			fetchAndPlayIntroVideo();
		}

		private void UpdateProgressBar(global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest networkRequest)
		{
			global::Ea.Sharkbite.HttpPlugin.Http.Impl.FileDownloadRequest fileDownloadRequest = networkRequest as global::Ea.Sharkbite.HttpPlugin.Http.Impl.FileDownloadRequest;
			if (fileDownloadRequest == null)
			{
				return;
			}
			uint contentLength = fileDownloadRequest.ContentLength;
			if (contentLength != 0)
			{
				float num = (float)fileDownloadRequest.DownloadedBytes / (float)contentLength;
				int num2 = request.progressBarStart + (int)((100f - (float)request.progressBarStart) * num);
				int num3 = num2 - request.progressBarNow;
				if (num3 > 0)
				{
					splashProgressUpdateSignal.Dispatch(num3, 1f);
					request.progressBarNow = num2;
				}
			}
			else
			{
				logger.Warning("[Video] No progress bar with unknown length");
			}
		}
	}
}
