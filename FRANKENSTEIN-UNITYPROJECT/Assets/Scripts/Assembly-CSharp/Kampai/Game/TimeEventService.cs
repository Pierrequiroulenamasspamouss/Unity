namespace Kampai.Game
{
	public class TimeEventService : global::UnityEngine.MonoBehaviour, global::Kampai.Game.ITimeEventService
	{
		private struct TimeEventWithID
		{
			public int TimeEventInstanceID;

			public global::Kampai.Game.TimeEvent TimeEventInstance;
		}

		private global::System.Collections.Generic.List<global::Kampai.Game.TimeEventService.TimeEventWithID> timeEventList;

		private global::System.Collections.Generic.List<global::Kampai.Game.TimeEventService.TimeEventWithID> dispatchList = new global::System.Collections.Generic.List<global::Kampai.Game.TimeEventService.TimeEventWithID>();

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetPremiumCurrencySignal setPremiumCurrencySignal { get; set; }

		public TimeEventService()
		{
			timeEventList = new global::System.Collections.Generic.List<global::Kampai.Game.TimeEventService.TimeEventWithID>();
		}

		public bool AddEvent(int instanceId, int startTime, int eventTime, global::strange.extensions.signal.impl.Signal<int> timeEventSignal)
		{
			global::Kampai.Game.TimeEvent timeEventInstance = new global::Kampai.Game.TimeEvent(instanceId, startTime, eventTime, timeEventSignal);
			global::Kampai.Game.TimeEventService.TimeEventWithID item = new global::Kampai.Game.TimeEventService.TimeEventWithID
			{
				TimeEventInstanceID = instanceId,
				TimeEventInstance = timeEventInstance
			};
			timeEventList.Add(item);
			logger.Log(global::Kampai.Util.Logger.Level.Info, string.Format("Add Time Event: {0}\tStartTime: {1}\tTime: {2}\tSignal: {3}", instanceId, timeService.GameTimeSeconds(), eventTime, timeEventSignal));
			return true;
		}

		public void RushEvent(int instanceId)
		{
			global::System.Collections.Generic.List<global::Kampai.Game.TimeEventService.TimeEventWithID> list = new global::System.Collections.Generic.List<global::Kampai.Game.TimeEventService.TimeEventWithID>();
			foreach (global::Kampai.Game.TimeEventService.TimeEventWithID timeEvent in timeEventList)
			{
				if (timeEvent.TimeEventInstanceID == instanceId)
				{
					list.Add(timeEvent);
				}
			}
			foreach (global::Kampai.Game.TimeEventService.TimeEventWithID item in list)
			{
				item.TimeEventInstance.Dispatch();
				timeEventList.Remove(item);
			}
			setPremiumCurrencySignal.Dispatch();
		}

		public void RemoveEvent(int instanceId)
		{
			global::System.Collections.Generic.List<global::Kampai.Game.TimeEventService.TimeEventWithID> list = new global::System.Collections.Generic.List<global::Kampai.Game.TimeEventService.TimeEventWithID>();
			foreach (global::Kampai.Game.TimeEventService.TimeEventWithID timeEvent in timeEventList)
			{
				if (timeEvent.TimeEventInstanceID == instanceId)
				{
					list.Add(timeEvent);
				}
			}
			foreach (global::Kampai.Game.TimeEventService.TimeEventWithID item in list)
			{
				item.TimeEventInstance.ClearSignal();
				timeEventList.Remove(item);
			}
		}

		public int GetTimeRemaining(int instanceId)
		{
			foreach (global::Kampai.Game.TimeEventService.TimeEventWithID timeEvent in timeEventList)
			{
				if (timeEvent.TimeEventInstanceID == instanceId)
				{
					int num = timeEvent.TimeEventInstance.EventTime - (timeService.GameTimeSeconds() - timeEvent.TimeEventInstance.StartTime);
					if (num <= 0)
					{
						return 0;
					}
					return num;
				}
			}
			return -1;
		}

		public int GetEventDuration(int instanceId)
		{
			foreach (global::Kampai.Game.TimeEventService.TimeEventWithID timeEvent in timeEventList)
			{
				if (timeEvent.TimeEventInstanceID == instanceId)
				{
					return timeEvent.TimeEventInstance.EventTime;
				}
			}
			return 0;
		}

		public int CalculateRushCostForTimer(int timerDurationInSecond, global::Kampai.Game.RushActionType rushActionType)
		{
			if (timerDurationInSecond <= 0)
			{
				return 0;
			}
			global::Kampai.Game.RushTimeBandDefinition rushTimeBandForTime = definitionService.GetRushTimeBandForTime(timerDurationInSecond);
			return rushTimeBandForTime.GetCostForRushActionType(rushActionType);
		}

		public void Update()
		{
			global::System.Collections.Generic.List<global::Kampai.Game.TimeEventService.TimeEventWithID>.Enumerator enumerator = timeEventList.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					global::Kampai.Game.TimeEventService.TimeEventWithID current = enumerator.Current;
					if (IsEventExpired(current.TimeEventInstance))
					{
						dispatchList.Add(current);
					}
				}
			}
			finally
			{
				enumerator.Dispose();
			}
			if (dispatchList.Count <= 0)
			{
				return;
			}
			enumerator = dispatchList.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					global::Kampai.Game.TimeEventService.TimeEventWithID current2 = enumerator.Current;
					current2.TimeEventInstance.Dispatch();
					logger.Debug(string.Format("Dispatching ID: {0}", current2.TimeEventInstanceID));
					timeEventList.Remove(current2);
				}
				dispatchList.Clear();
			}
			finally
			{
				enumerator.Dispose();
			}
		}

		private bool IsEventExpired(global::Kampai.Game.TimeEvent timeEvent)
		{
			return timeService.GameTimeSeconds() - timeEvent.StartTime >= timeEvent.EventTime;
		}

		public void UnFreezeTime(int utc)
		{
			foreach (global::Kampai.Game.TimeEventService.TimeEventWithID timeEvent in timeEventList)
			{
				global::Kampai.Game.TimeEvent timeEventInstance = timeEvent.TimeEventInstance;
				logger.Info("Adjusting time event {0} -> {1}", timeEventInstance.StartTime, utc);
				timeEventInstance.StartTime = utc;
			}
		}

		public bool HasEventID(int id)
		{
			foreach (global::Kampai.Game.TimeEventService.TimeEventWithID timeEvent in timeEventList)
			{
				if (timeEvent.TimeEventInstanceID == id)
				{
					return true;
				}
			}
			return false;
		}
	}
}
