using System.Collections.Generic;
using UnityEngine;

// MOCK de NimbleBridge_OperationalTelemetryDispatch
// Signature publique IDENTIQUE à l’original
public class NimbleBridge_OperationalTelemetryDispatch
{
    public const string EVENTTYPE_TRACKING_SYNERGY_PAYLOADS = "com.ea.nimble.trackingimpl.synergy";
    public const string EVENTTYPE_NETWORK_METRICS = "com.ea.nimble.network";
    public const string NOTIFICATION_OT_EVENT_THRESHOLD_WARNING = "nimble.notification.ot.eventthresholdwarning";

    private NimbleBridge_OperationalTelemetryDispatch()
    {
    }

    public static NimbleBridge_OperationalTelemetryDispatch GetComponent()
    {
        return new NimbleBridge_OperationalTelemetryDispatch();
    }

    public NimbleBridge_OperationalTelemetryEvent[] GetEvents(string type)
    {
        Debug.LogWarning("[MOCK NIMBLE] GetEvents called for type:" +type+". Returning empty array.");
        return new NimbleBridge_OperationalTelemetryEvent[0];
    }

    public void SetMaxEventCount(string type, int count)
    {
        Debug.Log("[MOCK NIMBLE] SetMaxEventCount called for type: "+type+", count: {count}. Ignored.");
    }
}
