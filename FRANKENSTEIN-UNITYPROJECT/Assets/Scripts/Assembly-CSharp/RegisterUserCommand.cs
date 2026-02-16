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
		global::Kampai.Game.UserRegisterRequest userRegisterRequest = new global::Kampai.Game.UserRegisterRequest();
		userRegisterRequest.SynergyID = synergyService.userID;
		global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
		signal.AddListener(UserSessionService.RegisterRequestCallback);
		string uri = ServerUrl + "/rest/user/register";
		global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(uri).WithContentType("application/json").WithMethod(global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest.METHOD_POST).WithEntity(userRegisterRequest)
			.WithResponseSignal(signal);
		DownloadService.Perform(request);
	}
}
