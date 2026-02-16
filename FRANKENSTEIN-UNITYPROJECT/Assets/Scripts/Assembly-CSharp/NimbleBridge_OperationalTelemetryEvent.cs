public class NimbleBridge_OperationalTelemetryEvent : global::System.Runtime.InteropServices.SafeHandle
{
	public override bool IsInvalid
	{
		get
		{
			return base.IsClosed || handle == global::System.IntPtr.Zero;
		}
	}

	private NimbleBridge_OperationalTelemetryEvent()
		: base(global::System.IntPtr.Zero, true)
	{
	}

	internal NimbleBridge_OperationalTelemetryEvent(global::System.IntPtr handle)
		: base(global::System.IntPtr.Zero, true)
	{
		SetHandle(handle);
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_OperationalTelemetryEvent_Dispose(NimbleBridge_OperationalTelemetryEvent wrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_OperationalTelemetryEvent_getEventType(NimbleBridge_OperationalTelemetryEvent wrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern double NimbleBridge_OperationalTelemetryEvent_getLoggedTime(NimbleBridge_OperationalTelemetryEvent wrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern string NimbleBridge_OperationalTelemetryEvent_getEventDictionary(NimbleBridge_OperationalTelemetryEvent wrapper);
#endif

	protected override bool ReleaseHandle()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_OperationalTelemetryEvent_Dispose(this);
#endif
		return true;
	}

	public string GetEventType()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_OperationalTelemetryEvent_getEventType(this);
#else
        return "DummyEvent";
#endif
	}

	public double GetLoggedTime()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_OperationalTelemetryEvent_getLoggedTime(this);
#else
        return 0.0;
#endif
	}

	public string GetEventDictionary()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_OperationalTelemetryEvent_getEventDictionary(this);
#else
        return "{}";
#endif
	}
}
