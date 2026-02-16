namespace Kampai.Game
{
    public class ConfigurationsService : global::Kampai.Game.IConfigurationsService
    {
        private const string KILLSWITCH_PP_OVERRIDE_LEAD = "KS-";

        private global::Kampai.Game.ConfigurationDefinition config = new global::Kampai.Game.ConfigurationDefinition();

        private bool init = true;

        [Inject("game.server.environment")]
        public string ServerEnv { get; set; }

        [Inject("cdn.server.host")]
        public string ServerUrl { get; set; }

        [Inject]
        public global::Kampai.Util.ILogger logger { get; set; }

        [Inject]
        public global::Kampai.Game.ConfigurationsLoadedSignal configurationsLoadedSignal { get; set; }

        [Inject]
        public ILocalPersistanceService localPersistance { get; set; }

        [Inject]
        public global::Kampai.Util.ClientVersion clientVersion { get; set; }

        [Inject]
        public global::Kampai.Util.FPSUtil fpsUtil { get; set; }

        [Inject]
        public global::Kampai.Game.KillSwitchChangedSignal killSwitchChangedSignal { get; set; }

        private global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<global::Kampai.Game.KillSwitch, bool>> killswitchOverrides { get; set; }

        public global::Kampai.Game.ConfigurationDefinition GetConfigurations()
        {
            return config;
        }

        public void setInitonCallback(bool init)
        {
            this.init = init;
        }

        public void GetConfigurationCallback(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
        {
            global::Kampai.Util.TimeProfiler.EndSection("retrieve config");
            global::Kampai.Game.ConfigurationDefinition configDef = null;

            if (response.Success)
            {
                string jsonBody = response.Body;
                try
                {


                    // Remove BOM if present
                    if (!string.IsNullOrEmpty(jsonBody))
                    {
                        if (jsonBody[0] == '\uFEFF') jsonBody = jsonBody.Substring(1);
                        jsonBody = jsonBody.Trim().Replace("\0", "");
                    }

                    UnityEngine.Debug.LogWarning("JSON Length: " + (jsonBody != null ? jsonBody.Length.ToString() : "null"));

                    // Use JToken.Parse first to validate the JSON syntax strictly
                    var jToken = global::Newtonsoft.Json.Linq.JToken.Parse(jsonBody);
                    
                    // Convert the parsed JToken to the target object
                    configDef = jToken.ToObject<global::Kampai.Game.ConfigurationDefinition>();
                }
                catch (global::System.Exception ex)
                {
                    logger.Log(global::Kampai.Util.Logger.Level.Warning, "Warning Parsing (" + ex.Message + ")");
                    UnityEngine.Debug.LogWarning("Full parsing error: " + ex.ToString());
                    UnityEngine.Debug.LogWarning("JSON Body: " + jsonBody);
                    try {
                         // Sanity check: can we parse a simple valid JSON?
                         var testObj = global::Newtonsoft.Json.JsonConvert.DeserializeObject<object>("{\"test\":1}");
                         UnityEngine.Debug.LogWarning("Sanity check passed: simple JSON parsed.");
                    } catch (System.Exception sanityEx) {
                         UnityEngine.Debug.LogError("Sanity check failed: " + sanityEx.Message);
                    }
                }
            }

            if (configDef == null)
            {
                configDef = new global::Kampai.Game.ConfigurationDefinition();
            }

            // Initialize complex objects if null to prevent injection exceptions
            if (configDef.logglyConfig == null)
            {
                configDef.logglyConfig = new global::Kampai.Util.Logging.Hosted.LogglyConfiguration();
            }

            if (configDef.dlcManifests == null)
            {
                configDef.dlcManifests = new global::System.Collections.Generic.Dictionary<string, string>();
            }

            if (configDef.rateAppAfter == null)
            {
                configDef.rateAppAfter = new global::System.Collections.Generic.Dictionary<global::Kampai.Game.ConfigurationDefinition.RateAppAfterEvent, bool>();
            }

            if (configDef.featureAccess == null)
            {
                configDef.featureAccess = new global::System.Collections.Generic.Dictionary<string, global::Kampai.Game.FeatureAccess>();
            }

            if (configDef.killSwitches == null)
            {
                configDef.killSwitches = new global::System.Collections.Generic.List<global::Kampai.Game.KillSwitch>();
            }

            if (configDef.upsightPromoTriggers == null)
            {
                configDef.upsightPromoTriggers = new global::System.Collections.Generic.List<global::Kampai.Game.UpsightPromoTrigger>();
            }

            if (configDef.allowedVersions == null)
            {
                configDef.allowedVersions = new global::System.Collections.Generic.List<string>();
            }

            if (configDef.nudgeUpgradeVersions == null)
            {
                configDef.nudgeUpgradeVersions = new global::System.Collections.Generic.List<string>();
            }

            config = configDef;

            if (global::Kampai.Util.GameConstants.StaticConfig.DEBUG_ENABLED)
            {
                TryLoadKillswitchOverrides();
            }

            if (logger != null && config.logglyConfig != null)
            {
                logger.SetAllowedLevel(config.logglyConfig.logLevel);
            }

            if (fpsUtil != null)
            {
                fpsUtil.SetFpsHeartbeat(config.fpsHeartbeat);
            }

            if (configurationsLoadedSignal != null)
            {
                configurationsLoadedSignal.Dispatch(init);
            }
        }

        public string GetConfigURL()
        {
            string configVariant = GetConfigVariant();
            string clientPlatform = clientVersion.GetClientPlatform();
            string text = clientVersion.GetClientVersion();
            string text2 = global::UnityEngine.WWW.EscapeURL(clientVersion.GetClientDeviceType());
            return string.Format(ServerUrl + "/rest/config/{0}/{1}/{2}/{3}/{4}", ServerEnv.ToLower(), configVariant, clientPlatform, text2, text);
        }

        public string GetConfigVariant()
        {
            return (!global::Kampai.Util.ABTestModel.abtestEnabled) ? "anyVariant" : global::Kampai.Util.ABTestModel.configurationVariant;
        }

        public string GetDefinitionVariants()
        {
            if (global::Kampai.Util.ABTestModel.abtestEnabled && global::Kampai.Util.ABTestModel.definitionURL != null)
            {
                return global::Kampai.Util.ABTestModel.definitionVariants;
            }
            return string.Empty;
        }

        public void TryLoadKillswitchOverrides()
        {
            string empty = string.Empty;
            foreach (int value in global::System.Enum.GetValues(typeof(global::Kampai.Game.KillSwitch)))
            {
                empty = global::UnityEngine.PlayerPrefs.GetString("KS-" + (global::Kampai.Game.KillSwitch)value, null);
                if (!string.IsNullOrEmpty(empty))
                {
                    OverrideKillswitch((global::Kampai.Game.KillSwitch)value, global::System.Convert.ToBoolean(empty));
                }
            }
        }

        public bool isKillSwitchOn(global::Kampai.Game.KillSwitch killswitchType)
        {
            if (killswitchOverrides != null)
            {
                foreach (global::System.Collections.Generic.KeyValuePair<global::Kampai.Game.KillSwitch, bool> killswitchOverride in killswitchOverrides)
                {
                    if (killswitchOverride.Key == killswitchType)
                    {
                        return killswitchOverride.Value;
                    }
                }
            }
            return (config.killSwitches != null && config.killSwitches.Contains(killswitchType)) ? true : false;
        }

        public void OverrideKillswitch(global::Kampai.Game.KillSwitch killswitchType, bool killswitchValue)
        {
            if (killswitchOverrides == null)
            {
                killswitchOverrides = new global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<global::Kampai.Game.KillSwitch, bool>>();
            }
            foreach (global::System.Collections.Generic.KeyValuePair<global::Kampai.Game.KillSwitch, bool> killswitchOverride in killswitchOverrides)
            {
                if (killswitchOverride.Key == killswitchType)
                {
                    killswitchOverrides.Remove(killswitchOverride);
                    break;
                }
            }
            killswitchOverrides.Add(new global::System.Collections.Generic.KeyValuePair<global::Kampai.Game.KillSwitch, bool>(killswitchType, killswitchValue));
            global::UnityEngine.PlayerPrefs.SetString("KS-" + killswitchType, killswitchValue.ToString());
        }

        public void ClearKillswitchOverride(global::Kampai.Game.KillSwitch killswitchType)
        {
            if (killswitchOverrides == null)
            {
                return;
            }
            foreach (global::System.Collections.Generic.KeyValuePair<global::Kampai.Game.KillSwitch, bool> killswitchOverride in killswitchOverrides)
            {
                if (killswitchOverride.Key == killswitchType)
                {
                    killswitchOverrides.Remove(killswitchOverride);
                    global::UnityEngine.PlayerPrefs.DeleteKey("KS-" + killswitchType);
                    break;
                }
            }
        }

        public void ClearAllKillswitchOverrides()
        {
            if (killswitchOverrides != null)
            {
                for (int num = killswitchOverrides.Count - 1; num >= 0; num--)
                {
                    global::UnityEngine.PlayerPrefs.DeleteKey("KS-" + killswitchOverrides[num].Key);
                    killswitchOverrides.RemoveAt(num);
                }
            }
        }
    }
}