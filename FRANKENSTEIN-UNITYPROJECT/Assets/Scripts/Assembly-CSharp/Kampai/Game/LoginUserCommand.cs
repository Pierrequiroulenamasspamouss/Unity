namespace Kampai.Game
{
    public class LoginUserCommand : global::strange.extensions.command.impl.Command
    {
        public const string LOGIN_ENDPOINT = "/rest/user/session";

        [Inject] public ILocalPersistanceService LocalPersistService { get; set; }
        [Inject] public IEncryptionService encryptionService { get; set; }
        [Inject] public global::Kampai.Game.RegisterUserSignal RegisterUserSignal { get; set; }
        [Inject("game.server.host")] public string ServerUrl { get; set; }
        [Inject] public global::Kampai.Game.IUserSessionService UserSessionService { get; set; }
        [Inject] public global::Kampai.Util.ILogger logger { get; set; }
        [Inject] public global::Kampai.Download.IDownloadService downloadService { get; set; }

        public override void Execute()
        {
            logger.EventStart("LoginUserCommand.Execute");

            // =================================================================
            // LECTURE DIRECTE (BYPASS TOTAL)
            // =================================================================
            string rawID = global::UnityEngine.PlayerPrefs.GetString("MOCK_UserID");
            string rawSecret = global::UnityEngine.PlayerPrefs.GetString("MOCK_Secret");
            string rawKey = global::UnityEngine.PlayerPrefs.GetString("MOCK_AnonID");

            logger.Log(global::Kampai.Util.Logger.Level.Info, true, string.Format("[MOCK LOGIN] Direct Read -> ID: '{0}', Secret: '{1}', Key: '{2}'", rawID, rawSecret, rawKey));

            // VERIFICATION
            if (string.IsNullOrEmpty(rawID) || string.IsNullOrEmpty(rawSecret) || string.IsNullOrEmpty(rawKey))
            {
                logger.Log(global::Kampai.Util.Logger.Level.Info, true, "[MOCK LOGIN] Data Missing -> Triggering REGISTER");
                global::Kampai.Util.TimeProfiler.StartSection("register");
                RegisterUserSignal.Dispatch();
            }
            else
            {
                logger.Log(global::Kampai.Util.Logger.Level.Info, true, "[MOCK LOGIN] Data Found -> Triggering LOGIN");
                global::Kampai.Util.TimeProfiler.StartSection("login");

                global::Kampai.Game.UserLoginRequest userLoginRequest = new global::Kampai.Game.UserLoginRequest();
                userLoginRequest.UserID = rawID;
                userLoginRequest.AnonymousSecret = rawSecret;
                userLoginRequest.IdentityID = rawKey;

                global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
                signal.AddListener(UserSessionService.LoginRequestCallback);

                string url = ServerUrl + "/rest/user/session";
                global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(url)
                    .WithContentType("application/json")
                    .WithMethod(global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest.METHOD_POST)
                    .WithEntity(userLoginRequest)
                    .WithResponseSignal(signal);

                downloadService.Perform(request);
                logger.Log(global::Kampai.Util.Logger.Level.Debug, "LoginUserCommand: Using url {0}", url);
            }
            logger.EventStop("LoginUserCommand.Execute");
        }
    }
}