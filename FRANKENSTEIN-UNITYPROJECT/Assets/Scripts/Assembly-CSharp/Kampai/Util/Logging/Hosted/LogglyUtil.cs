namespace Kampai.Util.Logging.Hosted
{
	public static class LogglyUtil
	{
		public static string FormatDateTimeISO(global::System.DateTime dateTime)
		{
			return dateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssK");
		}
	}
}
