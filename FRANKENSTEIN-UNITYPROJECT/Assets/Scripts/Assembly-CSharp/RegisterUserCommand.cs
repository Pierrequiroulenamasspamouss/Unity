public class RegisterUserCommand : global::strange.extensions.command.impl.Command
{
	public const string REGISTER_ENDPOINT = "/rest/user/register";

	[Inject("game.server.host")]
	public string ServerUrl { get; set; }

	[Inject]
	public global::Kampai.Download.IDownloadService DownloadService { get; set; }

	[Inject]
	public global::Kampai.Game.IUserSessionService UserSessionService { get; set; }

	[Inject]
	public global::Kampai.Game.ISynergyService synergyService { get; set; }

	[Inject]
	public global::Kampai.Util.ILogger logger { get; set; }

	public override void Execute()
	{
		logger.Log(global::Kampai.Util.Logger.Level.Info, "[RegisterUserCommand] Execution started");
		try
		{
			global::Kampai.Game.UserRegisterRequest userRegisterRequest = new global::Kampai.Game.UserRegisterRequest();
			
			string synId = "unknown_synergy";
			if (synergyService != null) {
				synId = synergyService.userID;
			} else {
				logger.Log(global::Kampai.Util.Logger.Level.Warning, "[RegisterUserCommand] synergyService is NULL!");
			}
			userRegisterRequest.SynergyID = synId;
			
			logger.Log(global::Kampai.Util.Logger.Level.Info, string.Format("[RegisterUserCommand] Built request for SynergyID: {0}", synId));

			global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
			signal.AddListener(UserSessionService.RegisterRequestCallback);
			string uri = ServerUrl + "/rest/user/register";
			
			logger.Log(global::Kampai.Util.Logger.Level.Info, string.Format("[RegisterUserCommand] Sending to URI: {0}", uri));

			global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(uri).WithContentType("application/json").WithMethod(global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest.METHOD_POST).WithEntity(userRegisterRequest)
				.WithResponseSignal(signal);
			
			if (DownloadService == null) logger.Log(global::Kampai.Util.Logger.Level.Error, "[RegisterUserCommand] DownloadService is NULL!");
			DownloadService.Perform(request);
			logger.Log(global::Kampai.Util.Logger.Level.Info, "[RegisterUserCommand] Request submitted to DownloadService");
		}
		catch (global::System.Exception e)
		{
			logger.Log(global::Kampai.Util.Logger.Level.Error, "[RegisterUserCommand] EXCEPTION: " + e.ToString());
		}
	}
}
