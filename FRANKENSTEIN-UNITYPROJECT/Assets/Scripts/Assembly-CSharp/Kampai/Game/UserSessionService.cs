namespace Kampai.Game
{
    public class UserSessionService : global::Kampai.Game.IUserSessionService
    {
        private global::Kampai.Game.UserSession Session;
        private global::strange.extensions.signal.impl.Signal loginCallback;

        [Inject] public global::Kampai.Game.UserRegisteredSignal userRegisteredSignal { get; set; }
        [Inject] public global::Kampai.Util.ILogger logger { get; set; }
        [Inject] public global::Kampai.Main.SetupHockeyAppUserSignal setupHockeyAppUser { get; set; }
        [Inject] public ILocalPersistanceService LocalPersistService { get; set; }
        [Inject] public global::Kampai.Common.ITelemetryService telemetryService { get; set; }
        [Inject] public global::Kampai.Download.IDownloadService downloadService { get; set; }
        [Inject] public global::Kampai.Main.SetupSwrveSignal setupSwrveSignal { get; set; }
        [Inject] public global::Kampai.Main.SetupDataMitigationSignal SetupDataMitigation { get; set; }
        [Inject] public global::Kampai.Game.UpdateUserSignal updateUserSignal { get; set; }
        [Inject] public global::Kampai.Game.ISynergyService synergyService { get; set; }
        [Inject] public global::Kampai.Common.NetworkConnectionLostSignal networkConnectionLostSignal { get; set; }
        [Inject] public global::Kampai.Game.IPlayerService playerService { get; set; }
        [Inject] public global::Kampai.Game.UserSessionGrantedSignal userSessionGrantedSignal { get; set; }

        // --- AJOUT DU SIGNAL MANQUANT ---
        [Inject] public global::Kampai.Game.LoadPlayerSignal loadPlayerSignal { get; set; }

        public global::Kampai.Game.UserSession UserSession { get { return Session; } set { Session = value; } }

                public void LoginRequestCallback(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
        {
            global::Kampai.Util.TimeProfiler.EndSection("login");

            if (response.Success)
            {
                string body = response.Body;
                logger.Info("[MOCK LOGIN] Body: " + body);

                try
                {
                    if (!string.IsNullOrEmpty(body))
                    {
                        var dict = global::Kampai.Util.FastJsonParser.Deserialize(body) as System.Collections.Generic.Dictionary<string, object>;
                        if (dict != null)
                        {
                            UserSession = new global::Kampai.Game.UserSession();
                            if (dict.ContainsKey("userId")) UserSession.UserID = dict["userId"].ToString();
                            if (dict.ContainsKey("synergyId")) UserSession.SynergyID = dict["synergyId"].ToString();
                            if (dict.ContainsKey("sessionId")) UserSession.SessionID = dict["sessionId"].ToString();
                            if (dict.ContainsKey("isNewUser")) UserSession.IsNewUser = (bool)dict["isNewUser"];
                            if (dict.ContainsKey("isTester")) UserSession.IsTester = (bool)dict["isTester"];
                            if (dict.ContainsKey("country")) UserSession.Country = dict["country"].ToString();
                        }
                    }
                }
                catch { }

                if (UserSession == null || string.IsNullOrEmpty(UserSession.UserID))
                {
                    logger.Error("Login response did not provide a UserID.");
                    return;
                }
                
                // global::UnityEngine.PlayerPrefs.SetString("MOCK_UserID", UserSession.UserID);
                // global::UnityEngine.PlayerPrefs.Save();

                userSessionGrantedSignal.Dispatch();
                LocalPersistService.PutData("LoadMode", "remote");

                logger.Info("[MOCK] Triggering LoadPlayerSignal manually...");
                loadPlayerSignal.Dispatch();

                try { updateSynergyId(UserSession); } catch { }

                SetupDataMitigation.Dispatch(new global::Kampai.Common.SetupDataMitigationParameters { UserID = UserSession.UserID, ClientIP = "127.0.0.1" });

                if (loginCallback != null) { loginCallback.Dispatch(); return; }

                telemetryService.Send_Telemetry_EVT_USER_GAME_LOAD_FUNNEL("70 - User Login", playerService.SWRVEGroup);
                setupSwrveSignal.Dispatch(UserSession.UserID);

                global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
                signal.AddListener(CatchAuthenticationErrorResponse);
                downloadService.AddGlobalResponseListener(signal);
            }
        }

                public void RegisterRequestCallback(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
        {
            global::Kampai.Util.TimeProfiler.EndSection("register");
            if (response.Success)
            {
                string body = response.Body;
                logger.Info("[MOCK REGISTER] Raw Body: " + body);

                global::Kampai.Game.UserIdentity userIdentity = new global::Kampai.Game.UserIdentity();
                string sSecret = "";
                string sKey = "";
                string sID = "";
                
                try
                {
                    var rawData = global::Kampai.Util.FastJsonParser.Deserialize(body) as System.Collections.Generic.Dictionary<string, object>;
                    if (rawData != null) {
                        if (rawData.ContainsKey("secret")) sSecret = rawData["secret"].ToString();
                        if (rawData.ContainsKey("sessionKey")) sKey = rawData["sessionKey"].ToString();
                        if (rawData.ContainsKey("userId")) sID = rawData["userId"].ToString();
                    }
                }
                catch { }

                userIdentity.UserID = sID;
                userIdentity.Secret = sSecret;
                userIdentity.SessionKey = sKey;
                userIdentity.ExternalID = sSecret;
                userIdentity.ID = sKey;
                
                logger.Info(string.Format("[REGISTER] ID: {0}, Secret: {1}, Key: {2}", sID, sSecret, sKey));

                // global::UnityEngine.PlayerPrefs.SetString("MOCK_UserID", sID);
                // global::UnityEngine.PlayerPrefs.SetString("MOCK_Secret", sSecret);
                // global::UnityEngine.PlayerPrefs.SetString("MOCK_AnonID", sKey);
                // global::UnityEngine.PlayerPrefs.Save(); 

                setupHockeyAppUser.Dispatch(userIdentity.UserID);
                userRegisteredSignal.Dispatch(userIdentity);
            }
            else
            {
                logger.Fatal(global::Kampai.Util.FatalCode.GS_ERROR_LOGIN_3, "Response code {0}", response.Code);
            }
        }

        public void UserUpdateRequestCallback(string synergyID, global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
        {
            if (response.Success) { Session.SynergyID = synergyID; return; }
            logger.Log(global::Kampai.Util.Logger.Level.Error, "Failed to update user {0} with synergy ID {1}", UserSession.UserID, synergyID);
        }

        public void setLoginCallback(global::strange.extensions.signal.impl.Signal a) { loginCallback = a; }
        public void MoreHelp(string url) { global::UnityEngine.Application.OpenURL(url); }
        private void CatchAuthenticationErrorResponse(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response) { }
        private void updateSynergyId(global::Kampai.Game.UserSession session)
        {
            string userID = synergyService.userID;
            string synergyID = session.SynergyID;
            if (string.IsNullOrEmpty(synergyID) && !string.IsNullOrEmpty(userID)) updateUserSignal.Dispatch(userID);
            if (!string.IsNullOrEmpty(synergyID) && !synergyID.Equals(userID)) using (NimbleBridge_SynergyIdManager.GetComponent().Login(synergyID, session.UserID)) { }
        }
    }
}

