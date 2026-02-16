namespace Kampai.Game
{
    public class ConfigurationDefinition : global::Kampai.Util.IFastJSONDeserializable
    {
        public enum RateAppAfterEvent
        {
            UnknownEvent = 0,
            LevelUp = 1,
            VillainCutscene = 2,
            XPPayout = 3
        }

        [global::Newtonsoft.Json.JsonProperty(NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool serverPushNotifications { get; set; }

        [global::Newtonsoft.Json.JsonProperty(NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
        public float minimumVersion { get; set; }

        [global::Kampai.Util.Deserializer("ReaderUtil.ReadRateAppTriggerConfig")]
        [global::Newtonsoft.Json.JsonProperty(NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
        public global::System.Collections.Generic.Dictionary<global::Kampai.Game.ConfigurationDefinition.RateAppAfterEvent, bool> rateAppAfter { get; set; }

        [global::Newtonsoft.Json.JsonProperty(NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
        public int maxRPS { get; set; }

        [global::Newtonsoft.Json.JsonProperty(NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
        public global::System.Collections.Generic.List<global::Kampai.Game.KillSwitch> killSwitches { get; set; }

        [global::Newtonsoft.Json.JsonProperty(NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
        public int msHeartbeat { get; set; }

        [global::Newtonsoft.Json.JsonProperty(NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
        public int fpsHeartbeat { get; set; }

        [global::Newtonsoft.Json.JsonProperty(NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
        public int logLevel { get; set; }

        [global::Newtonsoft.Json.JsonProperty(NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
        public int healthMetricPercentage { get; set; }

        [global::Newtonsoft.Json.JsonProperty(NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
        public int nudgeUpgradePercentage { get; set; }

        [global::Kampai.Util.Deserializer("ReaderUtil.ReadDictionaryString")]
        [global::Newtonsoft.Json.JsonProperty(NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
        public global::System.Collections.Generic.Dictionary<string, string> dlcManifests { get; set; }

        [global::Newtonsoft.Json.JsonProperty(NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
        public global::System.Collections.Generic.Dictionary<string, global::Kampai.Game.FeatureAccess> featureAccess { get; set; }

        [global::Newtonsoft.Json.JsonProperty(NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
        public global::Kampai.Util.TargetPerformance targetPerformance { get; set; }

        [global::Newtonsoft.Json.JsonProperty(NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
        public string definitionId { get; set; }

        [global::Newtonsoft.Json.JsonProperty(NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
        public string definitions { get; set; }

        [global::Newtonsoft.Json.JsonProperty(NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
        public global::System.Collections.Generic.IList<global::Kampai.Game.UpsightPromoTrigger> upsightPromoTriggers { get; set; }

        [global::Newtonsoft.Json.JsonProperty(NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
        public global::System.Collections.Generic.IList<string> allowedVersions { get; set; }

        [global::Newtonsoft.Json.JsonProperty(NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
        public global::System.Collections.Generic.IList<string> nudgeUpgradeVersions { get; set; }

        [global::Newtonsoft.Json.JsonProperty(NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
        public global::Kampai.Util.Logging.Hosted.LogglyConfiguration logglyConfig { get; set; }

        [global::Newtonsoft.Json.JsonProperty(NullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
        public string videoUri { get; set; }

        public virtual object Deserialize(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
            {
                reader.Read();
            }
            global::Kampai.Util.ReaderUtil.EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case global::Newtonsoft.Json.JsonToken.PropertyName:
                        {
                            string propertyName = ((string)reader.Value).ToUpper();
                            if (!DeserializeProperty(propertyName, reader, converters))
                            {
                                // FIX: Move to the value first, THEN skip it
                                // The original code tried to skip while positioned on the property name
                                reader.Read(); // Move to the value
                                reader.Skip(); // Now skip the value
                            }
                            break;
                        }
                    case global::Newtonsoft.Json.JsonToken.EndObject:
                        return this;
                    default:
                        throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, global::Kampai.Util.ReaderUtil.GetPositionInSource(reader)));
                    case global::Newtonsoft.Json.JsonToken.Comment:
                        break;
                }
            }
            throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
        }

        protected virtual bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
        {
            switch (propertyName)
            {
                case "SERVERPUSHNOTIFICATIONS":
                    reader.Read();
                    serverPushNotifications = global::System.Convert.ToBoolean(reader.Value);
                    break;
                case "MINIMUMVERSION":
                    reader.Read();
                    minimumVersion = global::System.Convert.ToSingle(reader.Value);
                    break;
                case "RATEAPPAFTER":
                    reader.Read();
                    rateAppAfter = global::Kampai.Util.ReaderUtil.ReadRateAppTriggerConfig(reader, converters);
                    break;
                case "MAXRPS":
                    reader.Read();
                    maxRPS = global::System.Convert.ToInt32(reader.Value);
                    break;
                case "KILLSWITCHES":
                    reader.Read();
                    killSwitches = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.KillSwitch>(reader, converters, global::Kampai.Util.ReaderUtil.ReadKillSwitch);
                    break;
                case "MSHEARTBEAT":
                    reader.Read();
                    msHeartbeat = global::System.Convert.ToInt32(reader.Value);
                    break;
                case "FPSHEARTBEAT":
                    reader.Read();
                    fpsHeartbeat = global::System.Convert.ToInt32(reader.Value);
                    break;
                case "LOGLEVEL":
                    reader.Read();
                    logLevel = global::System.Convert.ToInt32(reader.Value);
                    break;
                case "HEALTHMETRICPERCENTAGE":
                    reader.Read();
                    healthMetricPercentage = global::System.Convert.ToInt32(reader.Value);
                    break;
                case "NUDGEUPGRADEPERCENTAGE":
                    reader.Read();
                    nudgeUpgradePercentage = global::System.Convert.ToInt32(reader.Value);
                    break;
                case "DLCMANIFESTS":
                    reader.Read();
                    dlcManifests = global::Kampai.Util.ReaderUtil.ReadDictionaryString(reader, converters);
                    break;
                case "FEATUREACCESS":
                    reader.Read();
                    featureAccess = global::Kampai.Util.ReaderUtil.ReadDictionary<global::Kampai.Game.FeatureAccess>(reader, converters);
                    break;
                case "TARGETPERFORMANCE":
                    reader.Read();
                    targetPerformance = global::Kampai.Util.ReaderUtil.ReadEnum<global::Kampai.Util.TargetPerformance>(reader);
                    break;
                case "DEFINITIONID":
                    reader.Read();
                    definitionId = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
                    break;
                case "DEFINITIONS":
                    reader.Read();
                    definitions = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
                    break;
                case "UPSIGHTPROMOTRIGGERS":
                    reader.Read();
                    upsightPromoTriggers = global::Kampai.Util.ReaderUtil.PopulateList<UpsightPromoTrigger>(reader, converters, global::Kampai.Util.ReaderUtil.ReadUpsightPromoTrigger);
                    break;
                case "ALLOWEDVERSIONS":
                    reader.Read();
                    allowedVersions = global::Kampai.Util.ReaderUtil.PopulateList<string>(reader, converters, global::Kampai.Util.ReaderUtil.ReadString);
                    break;
                case "NUDGEUPGRADEVERSIONS":
                    reader.Read();
                    nudgeUpgradeVersions = global::Kampai.Util.ReaderUtil.PopulateList<string>(reader, converters, global::Kampai.Util.ReaderUtil.ReadString);
                    break;
                case "LOGGLYCONFIG":
                    reader.Read();
                    logglyConfig = global::Kampai.Util.ReaderUtil.ReadLogglyConfiguration(reader, converters);
                    break;
                case "VIDEOURI":
                    reader.Read();
                    videoUri = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
                    break;
                default:
                    return false;
            }
            return true;
        }
    }
}