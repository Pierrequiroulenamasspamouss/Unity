namespace Kampai.Game
{
	public class TimeService : global::Kampai.Game.ITimeService
	{
		private int startTime;

		private int serverTime;

		private global::System.Globalization.CultureInfo cultureInfo;

		[Inject]
		public ILocalPersistanceService persistanceService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public int SecondsSinceAppStart()
		{
			return (int)SecondsSinceAppStartAsFloat();
		}

		public float SecondsSinceAppStartAsFloat()
		{
			return ApplicationTime();
		}

		public int GameTimeSeconds()
		{
			int num = ServerOrDeviceTime();
			if (persistanceService.HasKey("freezeTime"))
			{
				int dataInt = persistanceService.GetDataInt("freezeTime");
				if (dataInt > 0 && dataInt <= num)
				{
					return dataInt;
				}
			}
			return num;
		}

		private int ServerOrDeviceTime()
		{
			if (serverTime > 0)
			{
				return serverTime + (int)ApplicationTime();
			}
			return DeviceTimeSeconds();
		}

		public int DeviceTimeSeconds()
		{
			global::System.DateTime epochStart = global::Kampai.Util.GameConstants.Timers.epochStart;
			double totalSeconds = (global::System.DateTime.UtcNow - epochStart).TotalSeconds;
			return global::System.Convert.ToInt32(global::System.Math.Round(totalSeconds));
		}

		public global::System.DateTime EpochToDateTime(int seconds)
		{
			return global::Kampai.Util.GameConstants.Timers.epochStart.AddSeconds(seconds);
		}

		public void SetCultureInfo(string languageCode, string cultureInfoStr)
		{
			try
			{
				cultureInfoStr = ((!languageCode.Contains("zh")) ? string.Format("{0}-{1}", languageCode, cultureInfoStr) : "zh-CN");
				cultureInfo = global::System.Globalization.CultureInfo.CreateSpecificCulture(cultureInfoStr);
			}
			catch (global::System.ArgumentException)
			{
				try
				{
					cultureInfo = global::System.Globalization.CultureInfo.CreateSpecificCulture(languageCode);
				}
				catch (global::System.ArgumentException ex2)
				{
					logger.Error("Could not create Culture Info from {0}, error: {1}", cultureInfo, ex2);
					cultureInfo = global::System.Globalization.CultureInfo.InvariantCulture;
				}
			}
		}

		public global::System.Globalization.CultureInfo GetCultureInfo()
		{
			return cultureInfo;
		}

		public void GameStarted()
		{
			startTime = GameTimeSeconds();
		}

		public int SecondsSinceGameStart()
		{
			return GameTimeSeconds() - startTime;
		}

		public void SetServerTime(string timeFromServer)
		{
			global::System.DateTimeOffset result;
			if (global::System.DateTimeOffset.TryParse(timeFromServer, out result))
			{
				global::System.DateTime dateTime = result.DateTime;
				double totalSeconds = (dateTime - global::Kampai.Util.GameConstants.Timers.epochStart).TotalSeconds;
				int num = global::System.Convert.ToInt32(global::System.Math.Round(totalSeconds));
				if (num > 0)
				{
					serverTime = num - (int)ApplicationTime();
					logger.Info("Adjusting game time to {0}", serverTime);
				}
				else
				{
					logger.Error("Negative server time? {0}", timeFromServer);
				}
			}
			else
			{
				logger.Error("Unable to parse server time '{0}'", timeFromServer);
			}
		}

		protected virtual float ApplicationTime()
		{
			return global::UnityEngine.Time.realtimeSinceStartup;
		}
	}
}
