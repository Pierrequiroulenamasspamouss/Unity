public class LoadConfigurationsCommand : global::strange.extensions.command.impl.Command
{
	private global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> downloadResponse = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();

	[Inject]
	public bool init { get; set; }

	[Inject]
	public global::Kampai.Download.IDownloadService downloadService { get; set; }

	[Inject]
	public global::Kampai.Game.IConfigurationsService ConfigurationsService { get; set; }

	[Inject]
	public ILocalPersistanceService localPersistService { get; set; }

	[Inject]
	public global::Kampai.Util.ILogger logger { get; set; }

	public override void Execute()
	{
		//logger.EventStart("LoadConfigurationsCommand.Execute");
		//logger.Info("Executing LoadConfigurationsCommand...");
		global::Kampai.Util.TimeProfiler.StartSection("retrieve config");
		string configURL = ConfigurationsService.GetConfigURL();
		//logger.Info("ClientConfigUrl: {0}", configURL);
		localPersistService.PutData("configURL", configURL);
		downloadResponse.AddListener(ConfigurationsService.GetConfigurationCallback);
		ConfigurationsService.setInitonCallback(init);
		global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(configURL).WithResponseSignal(downloadResponse).WithRetry();
		downloadService.Perform(request);
        //logger.EventStop("LoadConfigurationsCommand.Execute");
        logger.EventStop("Succssfully finished loading the configuration ! ");
	}
}
