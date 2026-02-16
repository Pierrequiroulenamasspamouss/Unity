namespace Kampai.Game
{
	public class GameResumeCommand : global::strange.extensions.command.impl.Command
	{
		[Inject(global::Kampai.Main.MainElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable mainContext { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.CancelAllNotificationSignal signal { get; set; }

		[Inject]
		public global::Kampai.Game.RestoreCraftingBuildingsSignal restoreCraftingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.TeleportMinionToBuildingSignal teleportSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RefreshQueueSlotSignal updateQueueSignal { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingChangeStateSignal changeStateSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IDevicePrefsService devicePrefsService { get; set; }

		[Inject]
		public global::Kampai.Game.ICurrencyService currencyService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public ILocalPersistanceService localPersistence { get; set; }

		[Inject]
		public global::Kampai.Common.Service.HealthMetrics.IClientHealthService clientHealthService { get; set; }

		[Inject]
		public global::Kampai.Main.IUpsightService upsightService { get; set; }

		[Inject]
		public global::Kampai.Game.MuteVolumeSignal muteVolumeSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Common.NimbleTelemetryEventsPostedSignal nimbleTelemetryEventsPostedSignal { get; set; }

		public override void Execute()
		{
			logger.Info("GameResumeCommand");
			nimbleTelemetryEventsPostedSignal.Dispatch();
			if (ShouldReloadGame())
			{
				int sleepTime = devicePrefsService.GetDevicePrefs().SleepTime;
				if (sleepTime > 0 && timeService.DeviceTimeSeconds() - sleepTime > 10)
				{
					mainContext.injectionBinder.GetInstance<global::Kampai.Main.ReloadGameSignal>().Dispatch();
					return;
				}
			}
			muteVolumeSignal.Dispatch();
			signal.Dispatch();
			foreach (global::Kampai.Game.Minion item in playerService.GetInstancesByType<global::Kampai.Game.Minion>())
			{
				if (item.State == global::Kampai.Game.MinionState.Tasking)
				{
					teleportSignal.Dispatch(item.ID);
				}
			}
			foreach (global::Kampai.Game.Building item2 in playerService.GetInstancesByType<global::Kampai.Game.Building>())
			{
				global::Kampai.Game.TaskableBuilding taskableBuilding = item2 as global::Kampai.Game.TaskableBuilding;
				if (taskableBuilding != null)
				{
					if (taskableBuilding.GetNumCompleteMinions() > 0)
					{
						changeStateSignal.Dispatch(item2.ID, global::Kampai.Game.BuildingState.Harvestable);
					}
					continue;
				}
				global::Kampai.Game.CraftingBuilding craftingBuilding = item2 as global::Kampai.Game.CraftingBuilding;
				if (craftingBuilding != null)
				{
					restoreCraftingSignal.Dispatch(craftingBuilding);
				}
			}
			updateQueueSignal.Dispatch(false);
			clientHealthService.MarkMeterEvent("AppFlow.Resume");
			currencyService.ResumeTransactionsHandling();
			upsightService.OnGameResumeCallback();
		}

		private bool ShouldReloadGame()
		{
			if (localPersistence.GetData("SocialInProgress").Equals("True"))
			{
				return false;
			}
			if (localPersistence.GetData("MtxPurchaseInProgress").Equals("True"))
			{
				return false;
			}
			if (localPersistence.GetData("ExternalLinkOpened").Equals("True"))
			{
				localPersistence.PutData("ExternalLinkOpened", "False");
				return false;
			}
			return true;
		}
	}
}
