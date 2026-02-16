namespace Kampai.Game
{
	public class UpdatePlayerDLCTierCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDLCService dlcService { get; set; }

		[Inject]
		public global::Kampai.Download.LaunchDownloadSignal launchDownloadSignal { get; set; }

		[Inject]
		public global::Kampai.Download.DLCModel dlcModel { get; set; }

		[Inject]
		public global::Kampai.Main.ReloadGameSignal reloadSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SavePlayerSignal saveSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			int quantity = (int)playerService.GetQuantity(global::Kampai.Game.StaticItem.TIER_ID);
			int quantity2 = (int)playerService.GetQuantity(global::Kampai.Game.StaticItem.TIER_GATE_ID);
			dlcService.SetPlayerDLCTier(quantity);
			if (dlcModel.HighestTierDownloaded < quantity2)
			{
				saveSignal.Dispatch(new global::Kampai.Util.Tuple<string, string, bool>("remote", string.Empty, false));
				routineRunner.StartCoroutine(WaitAFrame());
			}
			else
			{
				launchDownloadSignal.Dispatch(true);
			}
		}

		private global::System.Collections.IEnumerator WaitAFrame()
		{
			yield return null;
			logger.Debug("UpdatePlayerDLCTierCommand: Kicking player out of game.");
			reloadSignal.Dispatch();
		}
	}
}
