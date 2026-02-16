namespace Kampai.Game
{
	public class TimeEvent
	{
		private int instanceId;

		private global::strange.extensions.signal.impl.Signal<int> timeEventSignal;

		public int StartTime { get; set; }

		public int EventTime { get; set; }

		public TimeEvent(int instanceId, int startTime, int eventTime, global::strange.extensions.signal.impl.Signal<int> signal)
		{
			this.instanceId = instanceId;
			StartTime = startTime;
			EventTime = eventTime;
			timeEventSignal = signal;
		}

		public void Dispatch()
		{
			if (timeEventSignal != null)
			{
				timeEventSignal.Dispatch(instanceId);
			}
		}

		public void ClearSignal()
		{
			timeEventSignal = null;
		}
	}
}
