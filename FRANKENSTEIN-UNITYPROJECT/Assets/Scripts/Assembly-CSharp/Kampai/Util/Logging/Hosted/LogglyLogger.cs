namespace Kampai.Util.Logging.Hosted
{
	public class LogglyLogger : global::Kampai.Util.TelemetryLogger
	{
		[Inject]
		public global::Kampai.Util.Logging.Hosted.ILogglyService logglyService { get; set; }

		protected override void LogIt(global::Kampai.Util.Logger.Level level, string text, bool isFatal = false)
		{
			logglyService.Log(level, text, isFatal);
			base.LogIt(level, text);
		}
	}
}
