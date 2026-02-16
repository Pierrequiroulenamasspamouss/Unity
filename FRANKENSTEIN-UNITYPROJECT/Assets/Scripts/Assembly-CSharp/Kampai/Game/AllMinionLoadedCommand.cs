namespace Kampai.Game
{
	public class AllMinionLoadedCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.Game.ReconcileLevelUnlocksSignal reconcileUnlocks { get; set; }

		[Inject]
		public global::Kampai.Game.OrderBoardUpdateTicketOnBoardSignal updateTicketOnBoardSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IOrderBoardService orderBoardService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Game.SetPartyStatesSignal setPartyStatesSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.PartySignal partySignal { get; set; }

		[Inject]
		public global::Kampai.Game.UpdatePrestigeListSignal updatePrestigeList { get; set; }

		[Inject]
		public global::Kampai.Util.ICoroutineProgressMonitor coroutineProgressMonitor { get; set; }

		public override void Execute()
		{
			routineRunner.StartCoroutine(FinishStartup());
		}

		private global::System.Collections.IEnumerator FinishStartup()
		{
			while (coroutineProgressMonitor.HasRunningTasks())
			{
				yield return null;
			}
			global::Kampai.Util.TimeProfiler.StartSection("AllMinionLoadedCommand");
			global::Kampai.Util.TimeProfiler.StartSection("Quests");
			questService.Initialize();
			global::Kampai.Util.TimeProfiler.EndSection("Quests");
			yield return null;
			global::Kampai.Util.TimeProfiler.StartSection("unlocks");
			reconcileUnlocks.Dispatch();
			global::Kampai.Util.TimeProfiler.EndSection("unlocks");
			global::Kampai.Util.TimeProfiler.StartSection("update prestige");
			updatePrestigeList.Dispatch();
			global::Kampai.Util.TimeProfiler.EndSection("update prestige");
			global::Kampai.Util.TimeProfiler.StartSection("select level band");
			updateTicketOnBoardSignal.Dispatch();
			global::Kampai.Util.TimeProfiler.EndSection("select level band");
			global::Kampai.Util.TimeProfiler.StartSection("time service");
			timeService.GameStarted();
			global::Kampai.Util.TimeProfiler.EndSection("time service");
			global::Kampai.Util.TimeProfiler.StartSection("party states");
			setPartyStatesSignal.Dispatch(true);
			global::Kampai.Util.TimeProfiler.EndSection("party states");
			global::Kampai.Util.TimeProfiler.EndSection("AllMinionLoadedCommand");
			routineRunner.StartTimer("StartingPartyOver", (float)definitionService.GetPartyDefinition().Duration + 1f, delegate
			{
				partySignal.Dispatch();
			});
		}
	}
}
