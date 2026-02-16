namespace Kampai.Util
{
	public class CoroutineProgressMonitor : global::Kampai.Util.ICoroutineProgressMonitor
	{
		private struct Task
		{
			public string Tag;

			public bool Running;

			public global::System.Diagnostics.Stopwatch timer;

			public Task(string tag)
			{
				Tag = tag;
				Running = true;
				timer = global::System.Diagnostics.Stopwatch.StartNew();
			}
		}

		private global::System.Collections.Generic.List<global::Kampai.Util.CoroutineProgressMonitor.Task> Tasks = new global::System.Collections.Generic.List<global::Kampai.Util.CoroutineProgressMonitor.Task>();

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Util.IUpdateRunner updateRunner { get; set; }

		public bool HasRunningTasks()
		{
			for (int i = 0; i < Tasks.Count; i++)
			{
				if (Tasks[i].Running)
				{
					return true;
				}
			}
			return false;
		}

		public int GetRunningTasksCount()
		{
			int num = 0;
			for (int i = 0; i < Tasks.Count; i++)
			{
				if (Tasks[i].Running)
				{
					num++;
				}
			}
			return num;
		}

		public int StartTask(string tag)
		{
			for (int i = 0; i < Tasks.Count; i++)
			{
				if (!Tasks[i].Running)
				{
					Tasks[i] = new global::Kampai.Util.CoroutineProgressMonitor.Task(tag);
					return i;
				}
			}
			Tasks.Add(new global::Kampai.Util.CoroutineProgressMonitor.Task(tag));
			return Tasks.Count - 1;
		}

		public void FinishTask(int id)
		{
			global::Kampai.Util.CoroutineProgressMonitor.Task task = Tasks[id];
			logger.Debug("Coroutine monitor task '{0}' finished in {1}", task.Tag, task.timer.Elapsed.ToString());
			Tasks[id] = default(global::Kampai.Util.CoroutineProgressMonitor.Task);
		}
	}
}
