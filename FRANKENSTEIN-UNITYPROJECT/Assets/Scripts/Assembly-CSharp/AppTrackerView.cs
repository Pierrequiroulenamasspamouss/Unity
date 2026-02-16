public class AppTrackerView : global::strange.extensions.mediation.impl.View
{
	[Inject]
	public global::Kampai.Common.AppPauseSignal pauseSignal { get; set; }

	[Inject]
	public global::Kampai.Common.AppResumeSignal resumeSignal { get; set; }

	[Inject]
	public global::Kampai.Common.AppQuitSignal quitSignal { get; set; }

	[Inject]
	public global::Kampai.Common.AppFocusGainedSignal focusGainedSignal { get; set; }

	[Inject]
	public global::Kampai.Util.ILogger logger { get; set; }

	public void OnApplicationPause(bool isPausing)
	{
		if (isPausing)
		{
			pauseSignal.Dispatch();
		}
		else
		{
			resumeSignal.Dispatch();
		}
		global::Kampai.Util.TimeProfiler.Flush();
	}

	public void OnApplicationQuit()
	{
		quitSignal.Dispatch();
		global::Kampai.Util.TimeProfiler.Flush();
	}

	public void OnApplicationFocus(bool hasFocus)
	{
		if (hasFocus && focusGainedSignal != null)
		{
			focusGainedSignal.Dispatch();
		}
	}
}
