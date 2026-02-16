namespace Kampai.Game.View
{
	public class FreezeTimeCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public int utc { get; set; }

		[Inject]
		public ILocalPersistanceService persistanceService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeEventService timedEventService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		public override void Execute()
		{
			if (utc > 0)
			{
				logger.Info("Freezing Time at {0}", utc);
				if (persistanceService.HasKey("freezeTime") && persistanceService.GetDataInt("freezeTime") > 0)
				{
					logger.Warning("Freezing time when time is already frozen");
				}
				playerService.SetFreezeTime(utc);
				guiService.AddToArguments(new global::Kampai.UI.DisableRushButtons());
				persistanceService.PutDataInt("freezeTime", utc);
				return;
			}
			persistanceService.PutDataInt("freezeTime", 0);
			persistanceService.DeleteKey("freezeTime");
			utc = timeService.GameTimeSeconds();
			logger.Info("Thawing Time to {0}", utc);
			playerService.SetFreezeTime(0);
			guiService.RemoveFromArguments(typeof(global::Kampai.UI.DisableRushButtons));
			foreach (global::Kampai.Game.Minion item in playerService.GetInstancesByType<global::Kampai.Game.Minion>())
			{
				if (item.UTCTaskStartTime > 0)
				{
					item.UTCTaskStartTime = utc;
				}
			}
			global::Kampai.Game.TimeEventService timeEventService = (global::Kampai.Game.TimeEventService)timedEventService;
			timeEventService.UnFreezeTime(utc);
		}
	}
}
