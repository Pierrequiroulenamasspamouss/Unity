namespace Kampai.Game
{
	public class PlayerDurationService : global::Kampai.Game.IPlayerDurationService
	{
		private int lastGameplaySecondsUpdate;

		int global::Kampai.Game.IPlayerDurationService.TotalSecondsSinceLevelUp
		{
			get
			{
				logger.Debug("LEVELUP TELEMETRY: TotalSecondsSinceLevelUp");
				logger.Debug(string.Format("LEVELUP TELEMETRY: ActualUTC: {0}", timeService.DeviceTimeSeconds()));
				logger.Debug(string.Format("LEVELUP TELEMETRY: LevelUpUTC: {0}", playerService.LevelUpUTC));
				return timeService.DeviceTimeSeconds() - playerService.LevelUpUTC;
			}
		}

		int global::Kampai.Game.IPlayerDurationService.GameplaySecondsSinceLevelUp
		{
			get
			{
				logger.Debug("LEVELUP TELEMETRY: GameplaySecondsSinceLevelUp");
				int num = timeService.SecondsSinceAppStart() - lastGameplaySecondsUpdate;
				logger.Debug(string.Format("LEVELUP TELEMETRY: SecondsSinceAppStart: {0}", timeService.SecondsSinceAppStart()));
				logger.Debug(string.Format("LEVELUP TELEMETRY: lastGameplaySecondsUpdate: {0}", lastGameplaySecondsUpdate));
				logger.Debug(string.Format("LEVELUP TELEMETRY: GameplaySecondsSinceLevelUp: {0}", playerService.GameplaySecondsSinceLevelUp));
				logger.Debug(string.Format("LEVELUP TELEMETRY: currentGameplaySeconds: {0}", num));
				return playerService.GameplaySecondsSinceLevelUp + num;
			}
		}

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		void global::Kampai.Game.IPlayerDurationService.InitLevelUpUTC()
		{
			logger.Debug("LEVELUP TELEMETRY: InitLevelUpUTC");
			logger.Debug(string.Format("LEVELUP TELEMETRY: SecondsSinceAppStart: {0}", timeService.SecondsSinceAppStart()));
			if (playerService.LevelUpUTC == 0)
			{
				logger.Debug(string.Format("LEVELUP TELEMETRY: Initializing LevelUpUTC: {0}", timeService.DeviceTimeSeconds()));
				playerService.LevelUpUTC = timeService.DeviceTimeSeconds();
			}
			lastGameplaySecondsUpdate = timeService.SecondsSinceAppStart();
		}

		void global::Kampai.Game.IPlayerDurationService.MarkLevelUpUTC()
		{
			logger.Debug("LEVELUP TELEMETRY: MarkLevelUpUTC");
			playerService.LevelUpUTC = timeService.DeviceTimeSeconds();
			playerService.GameplaySecondsSinceLevelUp = 0;
			lastGameplaySecondsUpdate = timeService.SecondsSinceAppStart();
		}

		void global::Kampai.Game.IPlayerDurationService.SetLastGameStartUTC()
		{
			playerService.LastGameStartUTC = timeService.DeviceTimeSeconds();
		}

		void global::Kampai.Game.IPlayerDurationService.UpdateGameplayDuration()
		{
			logger.Debug("LEVELUP TELEMETRY: UpdateGameplayDuration");
			logger.Debug(string.Format("LEVELUP TELEMETRY: lastGameplaySecondsUpdate: {0}", lastGameplaySecondsUpdate));
			logger.Debug(string.Format("LEVELUP TELEMETRY: SecondsSinceAppStart: {0}", timeService.SecondsSinceAppStart()));
			int num = timeService.SecondsSinceAppStart() - lastGameplaySecondsUpdate;
			logger.Debug(string.Format("LEVELUP TELEMETRY: gameplaySecondsSinceLastUpdate: {0}", num));
			playerService.GameplaySecondsSinceLevelUp += num;
			logger.Debug(string.Format("LEVELUP TELEMETRY: GameplaySecondsSinceLevelUp: {0}", playerService.GameplaySecondsSinceLevelUp));
			lastGameplaySecondsUpdate = timeService.SecondsSinceAppStart();
		}
	}
}
