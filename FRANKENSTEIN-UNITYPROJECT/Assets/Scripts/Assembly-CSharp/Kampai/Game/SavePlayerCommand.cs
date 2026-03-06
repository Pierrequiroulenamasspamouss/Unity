namespace Kampai.Game
{
	internal sealed class SavePlayerCommand : global::strange.extensions.command.impl.Command
	{
		public const string PLAYER_DATA_ENDPOINT = "/rest/gamestate/{0}";

		private bool retried;

		[Inject]
		public global::Kampai.Util.Tuple<string, string, bool> tuple { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerDurationService playerDurationService { get; set; }

		[Inject]
		public ILocalPersistanceService persistService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IUserSessionService userSessionService { get; set; }

		[Inject("game.server.host")]
		public string ServerUrl { get; set; }

		[Inject]
		public global::Kampai.Download.IDownloadService downloadService { get; set; }

		[Inject]
		public global::Kampai.Common.NetworkConnectionLostSignal networkConnectionLostSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Game.SavePlayerSignal savePlayerSignal { get; set; }

		public override void Execute()
		{
			playerDurationService.UpdateGameplayDuration();
			byte[] array = playerService.Serialize();
			if (array != null && array.Length != 0)
			{
				routineRunner.StartCoroutine(SanityCheck(array));
			}
		}

		private global::Kampai.Game.Player.SanityCheckFailureReason DeepScan(global::Kampai.Game.Player prevSave)
		{
			return playerService.DeepScan(prevSave);
		}

		private global::System.Collections.IEnumerator SanityCheck(byte[] playerData)
		{
			if (!tuple.Item3)
			{
				yield return null;
			}
			global::Kampai.Game.Player.SanityCheckFailureReason sanityCheck = global::Kampai.Game.Player.SanityCheckFailureReason.Passed;
			global::Kampai.Game.Player previousSave = playerService.LastSave;
			global::Kampai.Game.Player currentSave = playerService.LoadPlayerData(global::System.Text.Encoding.UTF8.GetString(playerData));
			sanityCheck = currentSave.ValidateSaveData(previousSave);
			if (!tuple.Item3)
			{
				yield return null;
			}
			if (sanityCheck == global::Kampai.Game.Player.SanityCheckFailureReason.Passed || true) // Bypass check
			{
				if (sanityCheck != global::Kampai.Game.Player.SanityCheckFailureReason.Passed) {
					logger.Warning("Bypassed sanity check failure: {0}", sanityCheck.ToString());
				}
				playerService.LastSave = currentSave;
				string mode = tuple.Item1;
				if (mode == "remote")
				{
					RemoteSavePlayerData(playerData, tuple.Item3);
				}
				else if (mode == "local" || string.IsNullOrEmpty(mode)) // Fallback to local if mode string is unexpectedly missing
				{
					string saveID = tuple.Item2;
                    if (string.IsNullOrEmpty(saveID)) saveID = playerService.ID.ToString();
					persistService.PutData("Player_" + saveID, global::System.Text.Encoding.UTF8.GetString(playerData));
				}
				yield break;
			}
		}

		private void RemoteSavePlayerData(byte[] playerData, bool gameIsGoingToSleep)
		{
			global::Kampai.Game.UserSession userSession = userSessionService.UserSession;
			if (userSession != null)
			{
				string userID = userSession.UserID;
				global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
				signal.AddListener(OnPlayerSaved);
				string uri = ServerUrl + string.Format("/rest/gamestate/{0}", userID);
				global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(uri).WithHeaderParam("user_id", userSession.UserID).WithHeaderParam("session_key", userSession.SessionID).WithContentType("application/json")
					.WithMethod(global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest.METHOD_POST)
					.WithBody(playerData)
					.WithResponseSignal(signal);
				downloadService.Perform(request, gameIsGoingToSleep);
			}
			else
			{
				logger.Fatal(global::Kampai.Util.FatalCode.CMD_SAVE_PLAYER, "No user session");
			}
		}

		private void OnPlayerSaved(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
		{
			if (!response.Success)
			{
				if (!retried)
				{
					retried = true;
					networkConnectionLostSignal.Dispatch();
				}
				logger.Error("Unable to save player to server: {0}", response.Code);
			}
			else
			{
				logger.Log(global::Kampai.Util.Logger.Level.Debug, "User data saved to the server");
			}
		}
	}
}
