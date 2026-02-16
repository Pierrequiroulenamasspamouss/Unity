using System.Collections.Generic;


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

            // MOCK: On force toujours le succès, même si le serveur tousse
            // if (response.Success) 
            {
                string body = response.Body;
                logger.Info("[MOCK LOGIN] Body: " + body);

                try
                {
                    // On essaie de parser, sinon on crée un fake
                    if (!string.IsNullOrEmpty(body))
                        UserSession = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Kampai.Game.UserSession>(body);
                }
                catch { }

                if (UserSession == null)
                {
                    UserSession = new global::Kampai.Game.UserSession();
                    UserSession.UserID = global::UnityEngine.PlayerPrefs.GetString("MOCK_UserID", "1001");
                }
                if (UserSession.UserID.Length > 20)
                {
                    logger.Warning("[FIX] Found massive UserID (" + UserSession.UserID.Length + " chars). Resetting to 1001.");
                    UserSession.UserID = "1001";
                }
                // Sauvegarde de secours
                global::UnityEngine.PlayerPrefs.SetString("MOCK_UserID", UserSession.UserID);
                global::UnityEngine.PlayerPrefs.Save();

                // 1. On dit que la session est bonne
                userSessionGrantedSignal.Dispatch();
                LocalPersistService.PutData("LoadMode", "remote");

                // 2. IMPORTANT : ON LANCE LE CHARGEMENT DU JOUEUR ICI
                logger.Info("[MOCK] Triggering LoadPlayerSignal manually...");
                loadPlayerSignal.Dispatch();

                // La suite standard...
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

        // ==============================================================================
        // FIX RADICAL: Sauvegarde directe dans PlayerPrefs sans passer par les services
        // ==============================================================================
        public void RegisterRequestCallback(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
        {
            global::Kampai.Util.TimeProfiler.EndSection("register");
            if (response.Success)
            {
                string body = response.Body;
                logger.Info("[MOCK REGISTER] Raw Body: " + body);

                global::Kampai.Game.UserIdentity userIdentity = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Kampai.Game.UserIdentity>(body);

                // Parsing manuel pour récupérer les clés manquantes
                string sSecret = "mock_secret";
                string sKey = "mock_key";
                try
                {
                    var rawData = global::Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(body);
                    if (rawData.ContainsKey("secret")) sSecret = rawData["secret"].ToString();
                    if (rawData.ContainsKey("sessionKey")) sKey = rawData["sessionKey"].ToString();
                }
                catch { }

                string sID = userIdentity.UserID;
                if (sID.Length > 20)
                {
                    logger.Warning("[FIX] Registered massive UserID. Resetting to 1001.");
                    sID = "1001";
                    userIdentity.UserID = sID;
                }
                logger.Info(string.Format("[MOCK REGISTER] FORCE SAVING -> ID: {0}, Secret: {1}, Key: {2}", sID, sSecret, sKey));

                // ECRITURE DIRECTE (On bypass LocalPersistService)
                global::UnityEngine.PlayerPrefs.SetString("MOCK_UserID", sID);
                global::UnityEngine.PlayerPrefs.SetString("MOCK_Secret", sSecret);
                global::UnityEngine.PlayerPrefs.SetString("MOCK_AnonID", sKey);
                global::UnityEngine.PlayerPrefs.Save(); // Sauvegarde physique immédiate

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