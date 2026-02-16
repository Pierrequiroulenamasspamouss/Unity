public class LoadDefinitionsCommand : global::strange.extensions.command.impl.Command
{
	public class LoadDefinitionsData
	{
		public string Path { get; set; }

		public string Json { get; set; }
	}

	private string path;

	[Inject]
	public LoadDefinitionsCommand.LoadDefinitionsData defData { get; set; }

	[Inject]
	public global::Kampai.Util.ILogger logger { get; set; }

	[Inject]
	public global::Kampai.Game.IDefinitionService service { get; set; }

	[Inject]
	public global::Kampai.Game.DefinitionsChangedSignal definitionsChangedSignal { get; set; }

	[Inject]
	public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

	[Inject]
	public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

	[Inject]
	public global::Kampai.Util.IInvokerService invokerService { get; set; }

	[Inject]
	public global::Kampai.Game.IPlayerService playerService { get; set; }

	[Inject]
	public global::Kampai.Splash.SplashProgressUpdateSignal splashProgressUpdateSignal { get; set; }

	public override void Execute()
	{
		logger.EventStart("LoadDefinitionsCommand.Execute");
		path = FetchDefinitionsCommand.GetDefinitionsPath();
		string json = defData.Json;
		if (json == null)
		{
			try
			{
				logger.Debug("LoadDefinitionsCommand:Execute reading from path " + path);
				json = global::System.IO.File.ReadAllText(path);
			}
			catch (global::System.Exception ex)
			{
				logger.Log(global::Kampai.Util.Logger.Level.Warning, "Reading definitions file failed: {0}. Error: {1}. Proceeding with fallback.", path, ex.Message);
			}
		}
		if (json == null)
		{
			// Try StreamingAssets
			string streamingPath = global::System.IO.Path.Combine(global::UnityEngine.Application.streamingAssetsPath, "definitions.json");
			if (global::System.IO.File.Exists(streamingPath))
			{
				logger.Debug("LoadDefinitions: Loading from StreamingAssets: " + streamingPath);
				try
				{
					json = global::System.IO.File.ReadAllText(streamingPath);
				}
				catch (global::System.Exception ex)
				{
					logger.Error("Available definitions.json in StreamingAssets reading failed? " + ex.Message);
					json = null;
				}
			}
		}

		if (json == null)
		{
			logger.Debug("LoadDefinitionsCommand:Execute reading from Resources 'definitions'");
			global::UnityEngine.TextAsset textAsset = global::UnityEngine.Resources.Load<global::UnityEngine.TextAsset>("definitions");
			if (textAsset != null)
			{
				json = textAsset.text;
			}
		}

		telemetryService.Send_Telemetry_EVT_USER_GAME_LOAD_FUNNEL("80 - Loaded Definitions", playerService.SWRVEGroup);
		if (json == null)
		{
			logger.FatalNoThrow(global::Kampai.Util.FatalCode.DS_UNABLE_TO_LOAD);
		}
		else
		{
			logger.Debug("LoadDefinitions: Starting deserialization");
			routineRunner.StartAsyncConditionTask(() => DeserializeDefinitions(json), delegate
			{
				logger.Debug("LoadDefinitions: Deserialized successfully");
                //global::Kampai.Common.TelemetryService telemetryService = this.telemetryService as global::Kampai.Common.TelemetryService;
                //if (telemetryService != null)
                //{
                //	telemetryService.SetDefinitionServiceReference(service);
                //}
                var concreteTelemetryService =
    this.telemetryService as global::Kampai.Common.TelemetryService;

                if (concreteTelemetryService != null)
                {
                    concreteTelemetryService.SetDefinitionServiceReference(service);
                }

                definitionsChangedSignal.Dispatch();
				splashProgressUpdateSignal.Dispatch(35, 10f);
			});
		}
		logger.EventStop("LoadDefinitionsCommand.Execute");
	}

	private bool DeserializeDefinitions(string json)
	{
		try
		{
			logger.Debug("LoadDefinitionsCommand: About to call service.Deserialize");
			service.Deserialize(json);
			logger.Debug("LoadDefinitionsCommand: Finished calling service.Deserialize");
			return true;
		}
		catch (global::Kampai.Util.FatalException ex)
		{
			global::Kampai.Util.FatalException ex2 = ex;
			global::Kampai.Util.FatalException e = ex2;
			logger.Error("Can't deserialize: {0}", e);
			invokerService.Add(delegate
			{
				logger.FatalNoThrow(e.FatalCode, e.ReferencedId, "Message: {0}, Reason: {1}", e.Message, e.InnerException ?? e);
			});
		}
		catch (global::System.Exception ex3)
		{
			global::System.Exception ex4 = ex3;
			global::System.Exception e2 = ex4;
			logger.Error("Can't deserialize: {0}", e2);
			invokerService.Add(delegate
			{
				logger.FatalNoThrow(global::Kampai.Util.FatalCode.DS_PARSE_ERROR, 0, "Reason: {0}", e2);
			});
		}
		return false;
	}
}
