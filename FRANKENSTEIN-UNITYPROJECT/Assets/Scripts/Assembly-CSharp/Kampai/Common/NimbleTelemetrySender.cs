namespace Kampai.Common
{
	public class NimbleTelemetrySender : global::Kampai.Common.ITelemetrySender
	{
		[Inject]
		public ILocalPersistanceService localPersistance { get; set; }

		[Inject]
		public global::Kampai.Common.ICoppaService coppaService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public virtual void COPPACompliance()
		{
			string text = null;
			global::System.DateTime birthdate;
			if (!coppaService.GetBirthdate(out birthdate))
			{
				birthdate = global::System.DateTime.Now;
			}
			text = DateAsString(birthdate.Year, birthdate.Month);
			NimbleBridge_Tracking.GetComponent().AddCustomSessionValue("ageGateDob", text);
		}

		public void SharingUsage(bool enabled)
		{
			//logger.Info("=======================================================================================================================================================================================");
			//logger.Info("=======================================================================================================================================================================================");
			//logger.Info("#                                                                                                                                                                                     #");
			logger.Info("#                  setting Nimble sharingusage to {0}                                                                                                                             #", enabled);
			//logger.Info("#                                                                                                                                                                                     #");
			//logger.Info("=======================================================================================================================================================================================");
			NimbleBridge_Tracking.GetComponent().SetEnabled(enabled);
		}

		protected string DateAsString(int year, int month)
		{
			return year + "-" + ((month >= 10) ? month.ToString() : ("0" + month));
		}

		public virtual void SendEvent(global::Kampai.Common.TelemetryEvent gameEvent)
		{
			NimbleBridge_Tracking.GetComponent().LogEvent("SYNERGYTRACKING::CUSTOM", getNimbleParameters(gameEvent));
			NimbleBridge_Log.GetComponent();
		}

		public global::System.Collections.Generic.Dictionary<string, string> getNimbleParameters(global::Kampai.Common.TelemetryEvent telemetryEvent)
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			dictionary.Add("eventType", ((int)telemetryEvent.Type).ToString());
			for (int i = 0; i < telemetryEvent.Parameters.Count; i++)
			{
				global::Kampai.Common.TelemetryParameter telemetryParameter = telemetryEvent.Parameters[i];
				if (telemetryParameter.keyType.Length > 0)
				{
					dictionary.Add("keyType" + (i + 1).ToString().PadLeft(2, '0'), ((int)global::System.Enum.Parse(typeof(SynergyTrackingEventKey), telemetryParameter.keyType)).ToString());
					dictionary.Add("keyValue" + (i + 1).ToString().PadLeft(2, '0'), (telemetryParameter.value != null) ? telemetryParameter.value.ToString() : string.Empty);
				}
			}
			return dictionary;
		}
	}
}
