//namespace Kampai.Common
//{
//	public class NimbleTelemetryPostListener : global::System.IDisposable, global::Kampai.Common.INimbleTelemetryPostListener
//	{
//		private NimbleBridge_NotificationListener PostListener;

//		private static global::Kampai.Common.NimbleTelemetryPostListener s_Instance;

//		private bool _disposed;

//		[Inject]
//		public global::Kampai.Util.ILogger logger { get; set; }

//		[Inject]
//		public global::Kampai.Common.NimbleTelemetryEventsPostedSignal postSignal { get; set; }

//		[PostConstruct]
//		public void PostConstruct()
//		{
//			logger.Debug("NimbleTelemetryPostListener.PostConstruct");
//			CheckDisposed();
//			if (s_Instance != null)
//			{
//				logger.Debug("NimbleTelemetryPostListener.PostConstruct: Found existing instance, will dispose of it.");
//				s_Instance.Dispose();
//			}
//			s_Instance = this;
//		}

//		public void Setup()
//		{
//			CheckDisposed();
//			if (PostListener == null)
//			{
//				logger.Debug("NimbleTelemetryPostListener.Setup");
//				PostListener = new NimbleBridge_NotificationListener(OnNimbleTelemetryPosted);
//				NimbleBridge_NotificationCenter.RegisterListener("nimble.notification.trackingimpl.synergy.postingToServer", PostListener);
//			}
//		}

//		private void OnNimbleTelemetryPosted(string name, global::System.Collections.Generic.Dictionary<string, object> userData, NimbleBridge_NotificationListener listener)
//		{
//			if (!_disposed && s_Instance == this)
//			{
//				logger.Debug("NimbleTelemetryPostListener.OnNimbleTelemetryPosted from callback id {0}", listener.GetCallbackId().ToString());
//				CheckDisposed();
//				postSignal.Dispatch();
//			}
//		}

//		private void CheckDisposed()
//		{
//			if (_disposed)
//			{
//				throw new global::System.ObjectDisposedException(typeof(global::Kampai.Common.NimbleTelemetryPostListener).ToString());
//			}
//		}

//		protected virtual void Dispose(bool disposing)
//		{
//			if (disposing && PostListener != null)
//			{
//				NimbleBridge_NotificationCenter.UnregisterListener(PostListener);
//				PostListener.Dispose();
//				PostListener = null;
//				logger.Debug("NimbleTelemetryPostListener.Dispose");
//				if (s_Instance == this)
//				{
//					s_Instance = null;
//				}
//			}
//		}

//		public void Dispose()
//		{
//			CheckDisposed();
//			Dispose(true);
//			_disposed = true;
//		}

//		~NimbleTelemetryPostListener()
//		{
//			Dispose(false);
//		}
//	}
//}
namespace Kampai.Common
{
    // We keep the interface implementation to satisfy StrangeIoC
    public class NimbleTelemetryPostListener : global::System.IDisposable, global::Kampai.Common.INimbleTelemetryPostListener
    {
        // Removed: NimbleBridge_NotificationListener PostListener;
        // We don't need the bridge object anymore as it causes crashes.

        private static global::Kampai.Common.NimbleTelemetryPostListener s_Instance;
        private bool _disposed;

        [Inject]
        public global::Kampai.Util.ILogger logger { get; set; }

        [Inject]
        public global::Kampai.Common.NimbleTelemetryEventsPostedSignal postSignal { get; set; }

        [PostConstruct]
        public void PostConstruct()
        {
            // Safe logging
            if (logger != null) logger.Debug("MOCK: NimbleTelemetryPostListener.PostConstruct (Safe Mode)");

            CheckDisposed();

            // We keep the singleton logic just in case other parts check s_Instance
            if (s_Instance != null)
            {
                if (logger != null) logger.Debug("MOCK: Found existing instance, disposing.");
                s_Instance.Dispose();
            }
            s_Instance = this;
        }

        public void Setup()
        {
            CheckDisposed();

            // =================================================================
            // FIX: BYPASS NATIVE REGISTRATION
            // =================================================================
            // We intentionally do NOTHING here. 
            // Original code called NimbleBridge_NotificationCenter.RegisterListener...
            // that is what crashed your game.

            if (logger != null) logger.Debug("MOCK: NimbleTelemetryPostListener.Setup - Native Bridge bypassed.");
        }

        // This method is likely not called anymore since we didn't register the listener,
        // but we keep it to prevent compilation errors if it's referenced elsewhere.
        private void OnNimbleTelemetryPosted(string name, global::System.Collections.Generic.Dictionary<string, object> userData, object listener) // Changed listener type to object to avoid dependency
        {
            if (!_disposed && s_Instance == this)
            {
                if (logger != null) logger.Debug("MOCK: OnNimbleTelemetryPosted");
                CheckDisposed();

                // We can optionally dispatch the signal if the game waits for it
                if (postSignal != null) postSignal.Dispatch();
            }
        }

        private void CheckDisposed()
        {
            if (_disposed)
            {
                // Keep standard behavior
                throw new global::System.ObjectDisposedException(typeof(global::Kampai.Common.NimbleTelemetryPostListener).ToString());
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // =================================================================
                // FIX: BYPASS NATIVE UN-REGISTRATION
                // =================================================================
                // NimbleBridge_NotificationCenter.UnregisterListener(PostListener); // REMOVED

                if (logger != null) logger.Debug("MOCK: NimbleTelemetryPostListener.Dispose");

                if (s_Instance == this)
                {
                    s_Instance = null;
                }
            }
        }

        public void Dispose()
        {
            CheckDisposed();
            Dispose(true);
            _disposed = true;
        }

        ~NimbleTelemetryPostListener()
        {
            Dispose(false);
        }
    }
}