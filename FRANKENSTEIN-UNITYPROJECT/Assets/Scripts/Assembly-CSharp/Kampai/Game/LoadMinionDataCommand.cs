namespace Kampai.Game
{
	public class LoadMinionDataCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService player { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService characterService { get; set; }

		[Inject]
		public global::Kampai.Game.AllMinionLoadedSignal allMinionLoadedSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ICoroutineProgressMonitor coroutineProgressMonitor { get; set; }

		public override void Execute()
		{
			logger.EventStart("LoadMinionDataCommand.Execute");
			global::System.Collections.Generic.ICollection<global::Kampai.Game.Minion> instancesByType = player.GetInstancesByType<global::Kampai.Game.Minion>();
			global::System.Collections.Generic.List<global::Kampai.Game.Minion> minions = new global::System.Collections.Generic.List<global::Kampai.Game.Minion>(instancesByType);
			SortMinions(minions);
			characterService.Initialize();
			routineRunner.StartCoroutine(LoadMinions(minions));
		}

		public void SortMinions(global::System.Collections.Generic.List<global::Kampai.Game.Minion> minions)
		{
			minions.Sort((global::Kampai.Game.Minion x, global::Kampai.Game.Minion y) => x.UTCTaskStartTime.CompareTo(y.UTCTaskStartTime));
		}

		private global::System.Collections.IEnumerator LoadMinions(global::System.Collections.Generic.ICollection<global::Kampai.Game.Minion> minions)
		{
			while (coroutineProgressMonitor.GetRunningTasksCount() > 1)
			{
				yield return null;
			}
			int id = coroutineProgressMonitor.StartTask("load minions");
			global::Kampai.Game.CreateMinionSignal createMinion = base.injectionBinder.GetInstance<global::Kampai.Game.CreateMinionSignal>();
			foreach (global::Kampai.Game.Minion m in minions)
			{
				createMinion.Dispatch(m);
				yield return null;
			}
			yield return null;
			coroutineProgressMonitor.FinishTask(id);
			allMinionLoadedSignal.Dispatch();
			logger.EventStop("LoadMinionDataCommand.Execute");
		}
	}
}
