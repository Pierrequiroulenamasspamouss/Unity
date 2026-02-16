namespace Kampai.Game
{
    public class UserSession
    {
        [global::Newtonsoft.Json.JsonProperty("userId")]
        public string UserID { get; set; }

        [global::Newtonsoft.Json.JsonProperty("sessionId")]
        public string SessionID { get; set; }

        [global::Newtonsoft.Json.JsonProperty("synergyId")]
        public string SynergyID { get; set; }

        [global::Newtonsoft.Json.JsonProperty("socialIdentities")]
        public global::System.Collections.Generic.IList<global::Kampai.Game.UserIdentity> SocialIdentities { get; set; }

        [global::Newtonsoft.Json.JsonProperty("logEnabled")]
        public bool LogEnabled { get; set; }

        [global::Newtonsoft.Json.JsonProperty("logLevel")]
        public int LogLevel { get; set; }

        // =================================================================
        // AJOUTS MANUELS POUR CORRIGER LES ERREURS DE COMPILATION
        // =================================================================

        // Indique au jeu si c'est un nouveau joueur (pour lancer la vidéo)
        public bool IsNewUser { get; set; }

        // Indique si le joueur est un testeur (pour passer les protections NDA/GDPR)
        public bool IsTester { get; set; }

        // Indique si le joueur est un développeur
        public bool IsDeveloper { get; set; }

        // Force le pays (pour éviter les blocages régionaux)
        public string Country { get; set; }
    }
}