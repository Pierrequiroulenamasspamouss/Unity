namespace Kampai.Util
{
	public interface ICoroutineProgressMonitor
	{
		bool HasRunningTasks();

		int GetRunningTasksCount();

		int StartTask(string tag);

		void FinishTask(int id);
	}
}
