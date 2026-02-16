namespace Kampai.Common
{
	public class DeltaDNATelemetryEvent
	{
		private const string EVENTNAME_RAWRIVERJSON = "mobileLegacyEventJson";

		private const string EVENTNAME_MOBILEEVENT = "mobileLegacyEvent";

		private string eventName;

		private global::System.Collections.Generic.IList<global::System.Collections.Generic.KeyValuePair<string, object>> parameters;

		public string EventName
		{
			get
			{
				return eventName;
			}
		}

		public global::System.Collections.Generic.IList<global::System.Collections.Generic.KeyValuePair<string, object>> Parameters
		{
			get
			{
				return parameters;
			}
		}

		private DeltaDNATelemetryEvent(string eventName)
		{
			this.eventName = eventName;
			parameters = new global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<string, object>>();
		}

		public static global::Kampai.Common.DeltaDNATelemetryEvent RawRiverJsonEvent(string payload)
		{
			global::Kampai.Common.DeltaDNATelemetryEvent deltaDNATelemetryEvent = new global::Kampai.Common.DeltaDNATelemetryEvent("mobileLegacyEventJson");
			deltaDNATelemetryEvent.parameters.Add(new global::System.Collections.Generic.KeyValuePair<string, object>("jsonData", payload));
			return deltaDNATelemetryEvent;
		}

		public static global::Kampai.Common.DeltaDNATelemetryEvent MobileEvent()
		{
			return new global::Kampai.Common.DeltaDNATelemetryEvent("mobileLegacyEvent");
		}
	}
}
