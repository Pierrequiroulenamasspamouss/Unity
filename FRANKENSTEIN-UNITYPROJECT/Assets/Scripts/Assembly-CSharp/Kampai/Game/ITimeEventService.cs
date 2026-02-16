namespace Kampai.Game
{
	public interface ITimeEventService
	{
		bool AddEvent(int instanceId, int startTime, int eventTime, global::strange.extensions.signal.impl.Signal<int> timeEventSignal);

		void RushEvent(int instanceId);

		void RemoveEvent(int instanceId);

		int GetTimeRemaining(int instanceId);

		int GetEventDuration(int instanceId);

		int CalculateRushCostForTimer(int timerDurationInSecond, global::Kampai.Game.RushActionType rushActionType);

		bool HasEventID(int id);
	}
}
