public class LoadPlayerCommand : global::strange.extensions.command.impl.Command
{
	public const string PLAYER_DATA_ENDPOINT = "/rest/gamestate/{0}";

	private bool retried;

	private bool loadedFromLocal;

	[Inject]
	public global::Kampai.Util.ILogger logger { get; set; }

	[Inject]
	public IResourceService resourceService { get; set; }

	[Inject]
	public ILocalPersistanceService localPersistService { get; set; }

	[Inject]
	public global::Kampai.Game.IUserSessionService userSessionService { get; set; }

	[Inject("game.server.host")]
	public string ServerUrl { get; set; }

	[Inject]
	public global::Kampai.Download.IDownloadService downloadService { get; set; }

	[Inject]
	public global::Kampai.Game.LoadedPlayerDataSignal loadedPlayerDataSignal { get; set; }

	[Inject]
	public global::Kampai.Game.IDefinitionService defService { get; set; }

	[Inject]
	public global::Kampai.Game.ITimedSocialEventService socialEventService { get; set; }

	[Inject]
	public global::Kampai.Game.ITimeService timeService { get; set; }

	[Inject]
	public global::Kampai.Common.NetworkConnectionLostSignal networkConnectionLostSignal { get; set; }

	[Inject]
	public global::Kampai.Game.SocialEventResponseSignal socialEventResponseSignal { get; set; }

	[Inject]
	public global::Kampai.Splash.SplashProgressUpdateSignal splashProgressUpdateSignal { get; set; }

	private string TryLoadLocalPlayer()
	{
		string path = global::UnityEngine.Application.persistentDataPath + "/player_save.json";

		if (!global::System.IO.File.Exists(path))
			return null;

		try
		{
			logger.Info("[LoadPlayerCommand] Loading local save: " + path);
			byte[] bytes = global::System.IO.File.ReadAllBytes(path);
			string json = System.Text.Encoding.UTF8.GetString(bytes);

			return string.IsNullOrEmpty(json) ? null : json;
		}
		catch (System.Exception e)
		{
			logger.Error("[LoadPlayerCommand] Failed to read local save: " + e);
			return null;
		}
	}
	private string LoadLegacyLocal()
	{
		string id = localPersistService.GetData("LocalID");
		return string.IsNullOrEmpty(id)
			? null
			: localPersistService.GetData("Player_" + id);
	}

	private string LoadFile()
	{
		string path = localPersistService.GetData("LocalFileName");
		return string.IsNullOrEmpty(path)
			? null
			: resourceService.LoadText(path);
	}

		public override void Execute()
	{
		logger.EventStart("LoadPlayerCommand.Execute");
		global::Kampai.Util.TimeProfiler.StartSection("load player");

		loadedFromLocal = false;
        logger.Info("[LoadPlayerCommand] Forcing purely ONLINE mode. Ignoring local saves.");
		RemoteLoadPlayerData();

		logger.EventStop("LoadPlayerCommand.Execute");
	}

	private void RemoteLoadPlayerData()
	{
		global::Kampai.Game.UserSession userSession = userSessionService.UserSession;

		if (userSession != null)
		{
			string userID = userSession.UserID;
			LoadCurrentSocialTeam();
			global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
			signal.AddListener(OnPlayerLoaded);
			string uri = ServerUrl + string.Format("/rest/gamestate/{0}", userID);
			global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(uri).WithHeaderParam("user_id", userSession.UserID).WithHeaderParam("session_key", userSession.SessionID).WithResponseSignal(signal);
			downloadService.Perform(request);
			logger.Debug("LoadPlayerCommand: Requesting player data with user id {0}", userSession.UserID);
            return;
		}

		logger.Warning("LoadPlayerCommand: No user session found. Falling back to Initial Player.");
		string initialPlayerJson = defService.GetInitialPlayer();

		if (string.IsNullOrEmpty(initialPlayerJson))
		{
            initialPlayerJson = "{ \"inventory\": [], \"version\": 1 }";
            logger.Error("LoadPlayerCommand: Initial player data was empty, using emergency backup.");
		}

		loadedPlayerDataSignal.Dispatch(initialPlayerJson);
		splashProgressUpdateSignal.Dispatch(35, 10f);
	}

    private void LoadCurrentSocialTeam()
	{
		global::Kampai.Game.TimedSocialEventDefinition currentSocialEvent = socialEventService.GetCurrentSocialEvent();
		if (currentSocialEvent != null)
		{
			socialEventService.GetSocialEventState(currentSocialEvent.ID, socialEventResponseSignal);
		}
	}

	private void SetCultureCode(string serverCountryCode)
	{
		global::Kampai.Game.TimeService timeService = this.timeService as global::Kampai.Game.TimeService;
		if (timeService != null)
		{
			string languageCode = global::DeltaDNA.ClientInfo.LanguageCode;
			timeService.SetCultureInfo(languageCode, serverCountryCode);
		}
	}

	private void OnPlayerLoaded(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
	{
		logger.Debug("LoadPlayerCommand: Received player data with response code {0}", response.Code);
		global::Kampai.Util.TimeProfiler.EndSection("load player");
		
		if (loadedFromLocal)
		{
			logger.Warning("[LoadPlayerCommand] Ignoring server player data because local data was already loaded");
			return;
		}

		if (!response.Success)
		{
			int code = response.Code;
			if (code != 404)
			{
				if (!retried)
				{
					retried = true;
					logger.Error("OnPlayerLoaded failed with response code {0}", code);
					networkConnectionLostSignal.Dispatch();
				}
				else
				{
					logger.Fatal(global::Kampai.Util.FatalCode.GS_ERROR_LOAD_PLAYER, "Response code {0}", code);
				}
				return;
			}
		}
		string text = string.Empty;
		global::System.Collections.Generic.IDictionary<string, string> headers = response.Headers;
		if (headers != null && headers.ContainsKey("Date"))
		{
			string text2 = headers["Date"];
			logger.Info("New game time from server: {0}", text2);
			global::Kampai.Game.TimeService timeService = this.timeService as global::Kampai.Game.TimeService;
			timeService.SetServerTime(text2);
		}
		else
		{
			logger.Error("Unable to set server time; using device time.");
		}
		if (headers != null && headers.ContainsKey("X-Kampai-Country"))
		{
			string text3 = headers["X-Kampai-Country"];
			logger.Info("New country code from server: {0}", text3);
			SetCultureCode(text3);
		}
		else
		{
			logger.Info("No country code provided by the server. Setting to US by default");
			SetCultureCode(null);
		}
		if (response.Success)
		{
			text = response.Body;
			logger.Info("[LoadPlayerCommand] Raw Body from Server: " + text);
			
			
			if (string.IsNullOrEmpty(text))
			{
				logger.Fatal(global::Kampai.Util.FatalCode.PS_EMPTY_SERVER_JSON);
				return;
			}
		}
		loadedPlayerDataSignal.Dispatch(text);
	}
}
