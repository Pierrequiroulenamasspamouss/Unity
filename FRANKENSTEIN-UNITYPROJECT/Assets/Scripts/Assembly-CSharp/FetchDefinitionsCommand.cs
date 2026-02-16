public class FetchDefinitionsCommand : global::strange.extensions.command.impl.Command
{
	private global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> downloadResponseSignal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();

	private string definitionPath;

	[Inject]
	public global::Kampai.Util.ILogger logger { get; set; }

	[Inject]
	public global::Kampai.Game.ConfigurationDefinition config { get; set; }

	[Inject]
	public global::Kampai.Download.IDownloadService downloadService { get; set; }

	[Inject]
	public ILocalPersistanceService localPersistanceService { get; set; }

	[Inject]
	public global::Kampai.Game.LoadDefinitionsSignal loadDefinitionsSignal { get; set; }

	[Inject]
	public global::Kampai.Common.NetworkConnectionLostSignal networkConnectionLostSignal { get; set; }

	public override void Execute()
	{
		logger.EventStart("FetchDefinitionsCommand.Execute");
		if (global::Kampai.Util.ABTestModel.abtestEnabled && global::Kampai.Util.ABTestModel.definitionURL != null)
		{
			config.definitions = global::Kampai.Util.ABTestModel.definitionURL;
		}
		definitionPath = GetDefinitionsPath();
		downloadResponseSignal.AddListener(DownloadResponseHandler);
		logger.Info("FetchDefinitionsCommand:: Definitions URL: {0}", config.definitions);
		global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.FileDownloadRequest(config.definitions).WithOutputFile(definitionPath).WithGzip(true).WithResponseSignal(downloadResponseSignal)
			.WithRetry();
		logger.Info("FetchDefinitionsCommand:Execute config.definitions=" + config.definitions);
		downloadService.Perform(request);
	}

	public static string GetDefinitionsPath()
	{
		return global::System.IO.Path.Combine(global::Kampai.Util.GameConstants.PERSISTENT_DATA_PATH, "definitions.json");
	}

	private void DownloadResponseHandler(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
	{
		logger.Debug("FetchDefinitionsCommand:DownloadResponseHandler received response");
		if (!response.Success)
		{
			logger.FatalNoThrow(global::Kampai.Util.FatalCode.GS_ERROR_FETCH_DEFINITIONS, "GET {0} : status code {1}", response.Request.Uri, response.Code);
			return;
		}
        
        // Validate Downloaded File
        try
        {
            string content = global::System.IO.File.ReadAllText(definitionPath);
            if (!content.TrimStart().StartsWith("{"))
            {
                logger.Error("Invalid JSON received from " + response.Request.Uri);
                 if (global::System.IO.File.Exists(definitionPath))
                {
                    global::System.IO.File.Delete(definitionPath);
                }
                return;
            }
        }
        catch (global::System.Exception e) {
             logger.Error("Error validating definitions: " + e.Message);
             return;
        }

		localPersistanceService.PutData("DefinitionsUrl", response.Request.Uri);
		logger.Debug("Load definitions");
		LoadDefinitionsCommand.LoadDefinitionsData loadDefinitionsData = new LoadDefinitionsCommand.LoadDefinitionsData();
		loadDefinitionsData.Path = GetDefinitionsPath();
		loadDefinitionsSignal.Dispatch(loadDefinitionsData);
		logger.EventStop("FetchDefinitionsCommand.Execute");
	}
}
