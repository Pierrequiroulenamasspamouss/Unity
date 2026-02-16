namespace Kampai.Util
{
	public class IOSDeviceInspector : global::Kampai.Util.IDeviceInspector
	{
		private global::Kampai.Util.ILogger logger;

		private global::System.Collections.Generic.Dictionary<string, float> min = new global::System.Collections.Generic.Dictionary<string, float>
		{
			{ "iPhone", 4.1f },
			{ "iPad", 2.1f },
			{ "iPod", 5.1f }
		};

		private global::System.Collections.Generic.Dictionary<string, float> med = new global::System.Collections.Generic.Dictionary<string, float>
		{
			{ "iPhone", 5.1f },
			{ "iPad", 3.4f },
			{ "iPod", 6.1f }
		};

		private global::System.Collections.Generic.Dictionary<string, float> high = new global::System.Collections.Generic.Dictionary<string, float>
		{
			{ "iPhone", 6.1f },
			{ "iPad", 4.1f },
			{ "iPod", 7.1f }
		};

		public IOSDeviceInspector(global::Kampai.Util.ILogger logger)
		{
			this.logger = logger;
		}

		public bool IsSupported(global::UnityEngine.RuntimePlatform platform)
		{
			return platform == global::UnityEngine.RuntimePlatform.IPhonePlayer;
		}

		public global::Kampai.Util.TargetPerformance CaluclateTargetPerformance(global::Kampai.Util.DeviceInformation device)
		{
			string pattern = "([a-zA-Z]+)(\\d+),(\\d+)";
			global::System.Text.RegularExpressions.MatchCollection matchCollection = global::System.Text.RegularExpressions.Regex.Matches(device.model, pattern);
			if (matchCollection != null && matchCollection.Count == 1)
			{
				global::System.Collections.IEnumerator enumerator = matchCollection.GetEnumerator();
				enumerator.MoveNext();
				global::System.Text.RegularExpressions.Match match = enumerator.Current as global::System.Text.RegularExpressions.Match;
				global::System.Text.RegularExpressions.GroupCollection groups = match.Groups;
				string value = groups[1].Value;
				int num = global::System.Convert.ToInt32(groups[2].Value);
				int num2 = global::System.Convert.ToInt32(groups[3].Value);
				float num3 = (float)num + (float)num2 * 0.1f;
				if (!min.ContainsKey(value))
				{
					return NotFound(string.Format("Unknown family: {0} - {1}", value, num3.ToString()));
				}
				if (num3 < min[value])
				{
					return global::Kampai.Util.TargetPerformance.UNSUPPORTED;
				}
				if (num3 < med[value])
				{
					return global::Kampai.Util.TargetPerformance.LOW;
				}
				if (num3 < high[value])
				{
					return global::Kampai.Util.TargetPerformance.MED;
				}
				return global::Kampai.Util.TargetPerformance.HIGH;
			}
			return NotFound(string.Format("Unrecognized device model format: {0} count={1}", device.model, matchCollection.Count.ToString()));
		}

		private global::Kampai.Util.TargetPerformance NotFound(string message)
		{
			logger.Log(global::Kampai.Util.Logger.Level.Warning, "{0} - Defaulting to high performance", message);
			return global::Kampai.Util.TargetPerformance.HIGH;
		}
	}
}
