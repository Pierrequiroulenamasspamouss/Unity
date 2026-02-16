namespace Kampai.Util
{
	public static class ServiceSampleUtil
	{
		public enum ServiceType
		{
			ClientHealthMetrics = 0,
			Loggly = 1
		}

		private sealed class ServiceState
		{
			public bool IsServiceEnabled { get; set; }

			public bool IsServiceEnabledCalculated { get; set; }
		}

		private static global::System.Collections.Generic.Dictionary<global::Kampai.Util.ServiceSampleUtil.ServiceType, global::Kampai.Util.ServiceSampleUtil.ServiceState> serviceStateLookup = new global::System.Collections.Generic.Dictionary<global::Kampai.Util.ServiceSampleUtil.ServiceType, global::Kampai.Util.ServiceSampleUtil.ServiceState>();

		public static bool IsServiceEnabled(global::Kampai.Util.ServiceSampleUtil.ServiceType serviceType, int samplePercentage, global::Kampai.Game.IUserSessionService sessionService)
		{
			if (!serviceStateLookup.ContainsKey(serviceType))
			{
				serviceStateLookup.Add(serviceType, new global::Kampai.Util.ServiceSampleUtil.ServiceState());
			}
			global::Kampai.Util.ServiceSampleUtil.ServiceState serviceState = serviceStateLookup[serviceType];
			if (!serviceState.IsServiceEnabledCalculated)
			{
				if (sessionService.UserSession == null)
				{
					serviceState.IsServiceEnabled = true;
				}
				else
				{
					string userID = sessionService.UserSession.UserID;
					if (userID == null || userID.Length < 2)
					{
						serviceState.IsServiceEnabled = true;
					}
					else
					{
						string s = userID.Substring(userID.Length - 2, 2);
						int result;
						if (!int.TryParse(s, out result))
						{
							result = 0;
						}
						if (result < samplePercentage)
						{
							serviceState.IsServiceEnabled = true;
						}
						else
						{
							serviceState.IsServiceEnabled = false;
						}
					}
				}
				serviceState.IsServiceEnabledCalculated = true;
			}
			return serviceState.IsServiceEnabled;
		}

		public static void Reset()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<global::Kampai.Util.ServiceSampleUtil.ServiceType, global::Kampai.Util.ServiceSampleUtil.ServiceState> item in serviceStateLookup)
			{
				item.Value.IsServiceEnabledCalculated = false;
			}
		}
	}
}
