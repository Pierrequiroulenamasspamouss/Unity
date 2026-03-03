public class LoadedPlayerDataCommand : global::strange.extensions.command.impl.Command
{
	[Inject]
	public string playerJSON { get; set; }

	[Inject]
	public global::Kampai.Game.IPlayerService playerService { get; set; }

	[Inject]
	public global::Kampai.Game.IPlayerDurationService playerDurationService { get; set; }

	[Inject]
	public global::Kampai.Util.ILogger logger { get; set; }

	[Inject]
	public ILocalPersistanceService localPersistService { get; set; }

	[Inject]
	public IResourceService resourceService { get; set; }

	[Inject("devPlayerPath")]
	public string devPlayerFilename { get; set; }

	[Inject]
	public global::Kampai.Main.MainCompleteSignal completeSignal { get; set; }

	[Inject]
	public global::Kampai.Main.ReloadGameSignal reloadGameSignal { get; set; }

	[Inject]
	public global::Kampai.Game.IDefinitionService defService { get; set; }

	[Inject]
	public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

	[Inject]
	public global::Kampai.Common.ISwrveService swrveService { get; set; }

	[Inject]
	public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

	[Inject]
	public global::Kampai.Util.IInvokerService invokerService { get; set; }

	public override void Execute()
	{
		string text = localPersistService.GetData("LoadMode");
		if (text == "remote")
		{
			if (string.IsNullOrEmpty(playerJSON))
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "Error Loading player data from server");
				playerJSON = defService.GetInitialPlayer();
			}
		}
		else if (text == "externalLogin")
		{
			if (!string.IsNullOrEmpty(playerJSON))
			{
				reloadGameSignal.Dispatch();
				return;
			}
			logger.Log(global::Kampai.Util.Logger.Level.Error, "Error Loading player data from server");
			playerJSON = defService.GetInitialPlayer();
		}
		this.telemetryService.Send_Telemetry_EVT_USER_GAME_LOAD_FUNNEL("90 - Loaded Player Data", playerService.SWRVEGroup);
		if (playerJSON.Length != 0)
		{
			if (DeserializePlayerData(playerJSON))
			{
				global::Kampai.Common.TelemetryService telemetryService = this.telemetryService as global::Kampai.Common.TelemetryService;
				if (telemetryService != null)
				{
					telemetryService.SetPlayerServiceReference(playerService);
					telemetryService.SetPlayerDurationServiceReference(playerDurationService);
				}
				if (this.swrveService != null)
				{
					this.swrveService.SetPlayerServiceReference(playerService);
				}
				else
				{
					logger.Log(global::Kampai.Util.Logger.Level.Error, "SwrveService was not setup properly");
				}
				this.swrveService.SendUserStatsUpdate();
				completeSignal.Dispatch();
			}
			else
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "DeserializingPlayerData returned false");
			}
		}
		else
		{
			logger.FatalNoThrow(global::Kampai.Util.FatalCode.CMD_LOAD_PLAYER);
		}
	}

	private bool DeserializePlayerData(string json)
	{
		try
		{
			global::Kampai.Util.TimeProfiler.StartSection("read player");
			playerService.Deserialize(json);
			global::Kampai.Util.TimeProfiler.EndSection("read player");
			return true;
		}
		catch (global::Kampai.Util.FatalException ex)
		{
			logger.Error("LoadedPlayerDataCommand().DeserializePlayerData: FatalException. Json: {0}", json);
			logger.FatalNoThrow(ex.FatalCode, ex.ReferencedId, "Message: {0}, Reason: {1}", ex.Message, (ex.InnerException == null) ? ex.ToString() : ex.InnerException.ToString());
		}
		catch (global::System.Exception ex2)
		{
			logger.Error("Unexpected player deser-n error. Json: {0}", json);
			logger.FatalNoThrow(global::Kampai.Util.FatalCode.PS_JSON_PARSE_ERR, 0, "Reason: {0}", ex2);
		}
		return false;
	}
}

