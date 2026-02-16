public class NimbleBridge_NotificationListener : global::System.Runtime.InteropServices.SafeHandle
{
	private global::System.IntPtr m_callbackId;

	public override bool IsInvalid
	{
		get
		{
			return base.IsClosed || handle == global::System.IntPtr.Zero;
		}
	}

	private NimbleBridge_NotificationListener()
		: base(global::System.IntPtr.Zero, true)
	{
	}

	public NimbleBridge_NotificationListener(NotificationCallback callback)
		: this()
	{
		m_callbackId = NimbleBridge_CallbackHelper.Get().AddCallback(new NotificationCallbackConverter(callback, this));
		SetHandle(NimbleUnity_NotificationListener_NotificationListener(m_callbackId));
	}

	internal global::System.IntPtr GetCallbackId()
	{
		return m_callbackId;
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_NotificationListener_Dispose(NimbleBridge_NotificationListener listenerWrapper);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern global::System.IntPtr NimbleUnity_NotificationListener_NotificationListener(global::System.IntPtr callbackData);
#else
	private static global::System.IntPtr NimbleUnity_NotificationListener_NotificationListener(global::System.IntPtr callbackData) { return global::System.IntPtr.Zero; }
#endif

	protected override bool ReleaseHandle()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_NotificationListener_Dispose(this);
#endif
		return true;
	}
}
